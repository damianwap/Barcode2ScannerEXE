Imports System
Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Net.Security
Imports System.Security.Authentication
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.Script.Serialization

Public Class ScannerServer
    Private _listener As TcpListener
    Private _port As Integer = 3443
    Private _certificate As X509Certificate2
    Private _isRunning As Boolean = False
    Private _publicDir As String
    Private _activePhones As New ConcurrentDictionary(Of String, ConnectedDeviceInfo)()
    Private _activeConnections As Integer = 0
    Private _cleanupTimer As System.Threading.Timer = Nothing
    Private Const MaxConcurrentConnections As Integer = 30

    Private Class IpRateLimitInfo
        Public Property FailedAttempts As Integer = 0
        Public Property BlockedUntil As DateTime = DateTime.MinValue
    End Class
    Private _ipRateLimits As New ConcurrentDictionary(Of String, IpRateLimitInfo)()

    Public Event PhoneConnected(deviceName As String)
    Public Event PhoneDisconnected(deviceName As String)
    Public Event BarcodeScanned(text As String, format As String, deviceName As String)
    Public Event ServerLog(message As String)

    Public Property CurrentSessionCode As String = ""
    Public Property CurrentJoinKey As String = ""

    Public ReadOnly Property ActiveDevices As List(Of ConnectedDeviceInfo)
        Get
            Return _activePhones.Values.OrderBy(Function(d) d.ConnectedAt).ToList()
        End Get
    End Property

    Public ReadOnly Property ActivePhones As List(Of String)
        Get
            Return _activePhones.Values.OrderBy(Function(d) d.ConnectedAt).Select(Function(d) d.DeviceName).ToList()
        End Get
    End Property

    Public ReadOnly Property ActivePhoneCount As Integer
        Get
            Return _activePhones.Count
        End Get
    End Property

    Public Sub New(port As Integer, certificate As X509Certificate2, publicDir As String)
        _port = port
        _publicDir = publicDir
        _certificate = certificate
    End Sub

    Public Sub Start()
        If _isRunning Then Return

        ' TLS wajib: server menolak berjalan tanpa sertifikat (tidak ada fallback plaintext)
        If _certificate Is Nothing Then
            Throw New InvalidOperationException("Sertifikat TLS tidak tersedia. Server menolak berjalan tanpa enkripsi.")
        End If

        GenerateNewSession()

        _listener = New TcpListener(IPAddress.Any, _port)
        _listener.Start()
        _isRunning = True

        ' Bersihkan entri rate-limit basi setiap 5 menit agar dictionary tidak tumbuh selamanya
        _cleanupTimer = New System.Threading.Timer(AddressOf SweepRateLimits, Nothing, 300000, 300000)

        Dim listenThread As New Thread(AddressOf ListenLoop)
        listenThread.IsBackground = True
        listenThread.Start()

        RaiseEvent ServerLog($"Server aktif pada port {_port} (TLS wajib)")
    End Sub

    Public Sub StopServer()
        _isRunning = False
        Try
            If _cleanupTimer IsNot Nothing Then _cleanupTimer.Dispose()
        Catch
        End Try
        Try
            _listener?.Stop()
        Catch
        End Try
    End Sub

    Public Sub GenerateNewSession()
        Using rng As New RNGCryptoServiceProvider()
            Dim buf(3) As Byte
            Dim code As Integer
            Do
                rng.GetBytes(buf)
                Dim val As UInteger = BitConverter.ToUInt32(buf, 0)
                Dim maxAllowed As UInteger = UInteger.MaxValue - (UInteger.MaxValue Mod 900000UI)
                If val < maxAllowed Then
                    code = CInt(100000UI + (val Mod 900000UI))
                    Exit Do
                End If
            Loop
            CurrentSessionCode = code.ToString()

            Dim keyBuf(7) As Byte
            rng.GetBytes(keyBuf)
            CurrentJoinKey = BitConverter.ToString(keyBuf).Replace("-", "").ToLowerInvariant()
        End Using
        _activePhones.Clear()
    End Sub

    Private Sub SweepRateLimits(state As Object)
        Try
            Dim now = DateTime.UtcNow
            For Each kvp In _ipRateLimits.ToArray()
                SyncLock kvp.Value
                    If kvp.Value.BlockedUntil < now Then
                        If kvp.Value.FailedAttempts >= 5 Then
                            ' Blokade sudah kedaluwarsa: beri window percobaan baru
                            kvp.Value.FailedAttempts = 0
                        ElseIf kvp.Value.FailedAttempts <= 0 Then
                            Dim removed As IpRateLimitInfo = Nothing
                            _ipRateLimits.TryRemove(kvp.Key, removed)
                        End If
                    End If
                End SyncLock
            Next
        Catch
        End Try
    End Sub

    Private Sub ListenLoop()
        While _isRunning
            Try
                Dim client = _listener.AcceptTcpClient()

                ' Hitung koneksi secara atomik SEBELUM spawn thread (tanpa race)
                If Interlocked.Increment(_activeConnections) > MaxConcurrentConnections Then
                    Interlocked.Decrement(_activeConnections)
                    Try
                        client.Close()
                    Catch
                    End Try
                    Continue While
                End If

                Try
                    Dim handlerThread As New Thread(Sub() HandleClient(client))
                    handlerThread.IsBackground = True
                    handlerThread.Start()
                Catch
                    Interlocked.Decrement(_activeConnections)
                    Try
                        client.Close()
                    Catch
                    End Try
                End Try
            Catch ex As Exception
                If Not _isRunning Then Exit While
            End Try
        End While
    End Sub

    Private Sub HandleClient(client As TcpClient)
        Dim sslStream As SslStream = Nothing
        Dim deviceName As String = "HP"
        Dim isPhoneRegistered As Boolean = False
        Dim remoteEndpointStr As String = ""

        Dim clientIp As String = "127.0.0.1"
        Dim failedAttemptsThisConn As Integer = 0

        Try
            remoteEndpointStr = client.Client.RemoteEndPoint.ToString()
            Dim ipEndPoint = TryCast(client.Client.RemoteEndPoint, IPEndPoint)
            If ipEndPoint IsNot Nothing Then
                clientIp = ipEndPoint.Address.ToString()
            End If

            client.ReceiveTimeout = 60000
            client.SendTimeout = 60000

            Dim netStream = client.GetStream()

            ' TLS selalu wajib — tidak ada jalur plaintext
            sslStream = New SslStream(netStream, False)
            ' Hanya izinkan TLS 1.2 untuk keamanan modern
            sslStream.AuthenticateAsServer(_certificate, False, SslProtocols.Tls12, False)

            Dim stream As Stream = sslStream

            ' Baca header HTTP
            Dim requestHeader = ReadHttpHeader(stream)
            If String.IsNullOrEmpty(requestHeader) Then
                client.Close()
                Return
            End If

            Dim lines = requestHeader.Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
            If lines.Length = 0 Then
                client.Close()
                Return
            End If

            Dim requestLine = lines(0)
            Dim isWebSocket = requestHeader.IndexOf("Upgrade: websocket", StringComparison.OrdinalIgnoreCase) >= 0

            If isWebSocket Then
                ' Cek apakah IP sedang diblokir sementara karena brute-force
                Dim existingLimitInfo As IpRateLimitInfo = Nothing
                If _ipRateLimits.TryGetValue(clientIp, existingLimitInfo) Then
                    SyncLock existingLimitInfo
                        If existingLimitInfo.BlockedUntil > DateTime.UtcNow Then
                            Dim waitSecs = Math.Max(1, CInt((existingLimitInfo.BlockedUntil - DateTime.UtcNow).TotalSeconds))
                            Dim blockedBody = SerializeJson(New Dictionary(Of String, Object) From {
                                {"type", "error"},
                                {"message", $"IP diblokir sementara ({waitSecs} detik) karena terlalu banyak percobaan gagal."}})
                            Dim blockedResp = "HTTP/1.1 429 Too Many Requests" & vbCrLf &
                                              "Content-Type: application/json" & vbCrLf &
                                              "Connection: close" & vbCrLf & vbCrLf &
                                              blockedBody
                            Dim blockedBytes = Encoding.UTF8.GetBytes(blockedResp)
                            stream.Write(blockedBytes, 0, blockedBytes.Length)
                            stream.Flush()
                            client.Close()
                            Return
                        End If
                    End SyncLock
                End If

                ' Validasi Origin ketat untuk mencegah Cross-Site WebSocket Hijacking:
                ' host Origin harus persis salah satu IP server ini atau localhost.
                ' Klien non-browser tanpa header Origin tetap wajib lolos autentikasi session+joinKey.
                Dim origin = GetHeaderValue(lines, "Origin")
                If Not String.IsNullOrEmpty(origin) AndAlso Not IsAllowedOrigin(origin) Then
                    Dim forbiddenResp = "HTTP/1.1 403 Forbidden" & vbCrLf & "Content-Length: 0" & vbCrLf & "Connection: close" & vbCrLf & vbCrLf
                    Dim forbiddenBytes = Encoding.UTF8.GetBytes(forbiddenResp)
                    stream.Write(forbiddenBytes, 0, forbiddenBytes.Length)
                    stream.Flush()
                    client.Close()
                    Return
                End If

                ' Handshake WebSocket
                Dim wsKey = GetHeaderValue(lines, "Sec-WebSocket-Key")
                If String.IsNullOrEmpty(wsKey) Then
                    client.Close()
                    Return
                End If

                Dim magic = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                Dim sha1 = System.Security.Cryptography.SHA1.Create()
                Dim acceptKey = Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(wsKey.Trim() & magic)))

                Dim responseHeader = "HTTP/1.1 101 Switching Protocols" & vbCrLf &
                                     "Upgrade: websocket" & vbCrLf &
                                     "Connection: Upgrade" & vbCrLf &
                                     "Sec-WebSocket-Accept: " & acceptKey & vbCrLf & vbCrLf
                Dim respBytes = Encoding.UTF8.GetBytes(responseHeader)
                stream.Write(respBytes, 0, respBytes.Length)
                stream.Flush()

                ' Masuk ke pembacaan frame WebSocket
                While _isRunning AndAlso client.Connected
                    Dim frameText = ReadWebSocketFrame(stream)
                    If frameText Is Nothing Then Exit While

                    ' Parse JSON dengan parser sungguhan (bukan regex)
                    Dim msg = ParseJsonMessage(frameText)
                    If msg Is Nothing Then Continue While
                    Dim msgType = GetJsonString(msg, "type")

                    If msgType = "register" Then
                        Dim session = GetJsonString(msg, "session")
                        Dim key = GetJsonString(msg, "key")
                        Dim name = GetJsonString(msg, "name")
                        If Not String.IsNullOrEmpty(name) Then
                            ' Sanitasi nama perangkat agar aman ditampilkan
                            deviceName = Regex.Replace(name, "[^\w\s\-\.\(\)]", "").Trim()
                            If deviceName.Length > 40 Then deviceName = deviceName.Substring(0, 40)
                            If String.IsNullOrEmpty(deviceName) Then deviceName = "HP"
                        End If

                        ' Join key WAJIB: tanpa key autentikasi hanya kode 6 digit yang bisa brute-force
                        Dim isKeyValid = Not String.IsNullOrEmpty(key) AndAlso
                                         Not String.IsNullOrEmpty(CurrentJoinKey) AndAlso
                                         key.Equals(CurrentJoinKey, StringComparison.OrdinalIgnoreCase)

                        If session = CurrentSessionCode AndAlso isKeyValid Then
                            isPhoneRegistered = True
                            Dim devInfo As New ConnectedDeviceInfo With {
                                .Endpoint = remoteEndpointStr,
                                .DeviceName = deviceName,
                                .IP = clientIp,
                                .ConnectedAt = DateTime.Now
                            }
                            _activePhones(remoteEndpointStr) = devInfo

                            ' Reset riwayat gagal untuk IP ini
                            Dim okInfo As IpRateLimitInfo = Nothing
                            If _ipRateLimits.TryGetValue(clientIp, okInfo) Then
                                SyncLock okInfo
                                    okInfo.FailedAttempts = 0
                                End SyncLock
                            End If

                            Dim regResponse = SerializeJson(New Dictionary(Of String, Object) From {
                                {"type", "registered"},
                                {"session", CurrentSessionCode},
                                {"role", "phone"},
                                {"approved", True},
                                {"deviceName", deviceName}})
                            SendWebSocketText(stream, regResponse)

                            RaiseEvent PhoneConnected(deviceName)
                            RaiseEvent ServerLog($"📱 {deviceName} terhubung!")
                        Else
                            ' Catat percobaan gagal dan terapkan rate limit anti brute-force
                            failedAttemptsThisConn += 1
                            Dim info = _ipRateLimits.GetOrAdd(clientIp, Function(k) New IpRateLimitInfo())
                            Dim isBlocked As Boolean = False
                            Dim blockDurationSecs As Integer = 60

                            SyncLock info
                                info.FailedAttempts += 1
                                If info.FailedAttempts >= 5 Then
                                    info.BlockedUntil = DateTime.UtcNow.AddSeconds(blockDurationSecs)
                                    isBlocked = True
                                End If
                            End SyncLock

                            ' Penalti delay untuk mencegah brute force berkecepatan tinggi
                            Thread.Sleep(600)

                            If isBlocked Then
                                RaiseEvent ServerLog($"⚠️ IP {clientIp} diblokir selama {blockDurationSecs} detik (5x salah kode sesi).")
                                Dim blockMsg = SerializeJson(New Dictionary(Of String, Object) From {
                                    {"type", "error"},
                                    {"message", $"Terlalu banyak percobaan salah. IP Anda diblokir sementara selama {blockDurationSecs} detik."}})
                                SendWebSocketText(stream, blockMsg)
                                Exit While
                            Else
                                Dim attemptsLeft = 0
                                SyncLock info
                                    attemptsLeft = Math.Max(0, 5 - info.FailedAttempts)
                                End SyncLock
                                Dim errResponse = SerializeJson(New Dictionary(Of String, Object) From {
                                    {"type", "error"},
                                    {"message", $"Kode sesi atau kunci koneksi tidak valid. Sisa percobaan: {attemptsLeft}"}})
                                SendWebSocketText(stream, errResponse)
                            End If

                            ' Putus koneksi jika sudah 3x salah dalam 1 sesi socket
                            If failedAttemptsThisConn >= 3 Then
                                Exit While
                            End If
                        End If

                    ElseIf msgType = "scan" Then
                        ' HANYA klien terautentikasi yang boleh mengirim scan.
                        ' Tanpa guard ini, siapa pun bisa menyuntik teks ke jendela aktif (Auto-Type).
                        If Not isPhoneRegistered Then
                            Dim unauthMsg = SerializeJson(New Dictionary(Of String, Object) From {
                                {"type", "error"},
                                {"message", "Tidak terautentikasi. Registrasi dengan kode sesi dan kunci koneksi diperlukan."}})
                            SendWebSocketText(stream, unauthMsg)
                            Exit While
                        End If

                        Dim scanText = GetJsonString(msg, "text")
                        Dim scanFormat = GetJsonString(msg, "format")
                        If String.IsNullOrEmpty(scanFormat) Then scanFormat = "BARCODE"
                        If String.IsNullOrEmpty(scanText) Then Continue While

                        SendWebSocketText(stream, "{""type"":""ack""}")

                        RaiseEvent BarcodeScanned(scanText, scanFormat, deviceName)
                        RaiseEvent ServerLog($"[SCAN] {scanText} ({scanFormat}) dari {deviceName}")
                    End If
                End While

            Else
                ' Layani File Statis (scan.html, vendor/html5-qrcode.min.js)
                Dim parts = requestLine.Split(" "c)
                Dim rawUrl = If(parts.Length > 1, parts(1).Split("?"c)(0), "/")
                Dim decodedUrl = WebUtility.UrlDecode(rawUrl)
                If String.IsNullOrEmpty(decodedUrl) OrElse decodedUrl = "/" OrElse decodedUrl.Equals("/scan.html", StringComparison.OrdinalIgnoreCase) Then
                    decodedUrl = "/scan.html"
                End If

                ' Normalisasi path relatif
                Dim relPath = decodedUrl.TrimStart("/"c, "\"c).Replace("/", Path.DirectorySeparatorChar).Replace("\", Path.DirectorySeparatorChar)
                Dim canonicalPublicDir = Path.GetFullPath(_publicDir)
                If Not canonicalPublicDir.EndsWith(Path.DirectorySeparatorChar.ToString()) Then
                    canonicalPublicDir &= Path.DirectorySeparatorChar
                End If

                Dim localPath = Path.GetFullPath(Path.Combine(canonicalPublicDir, relPath))

                ' Cegah Path Traversal: pastikan path absolut berada di dalam _publicDir
                If Not localPath.StartsWith(canonicalPublicDir, StringComparison.OrdinalIgnoreCase) Then
                    Dim forbidden = "HTTP/1.1 403 Forbidden" & vbCrLf &
                                    "Content-Type: text/plain; charset=utf-8" & vbCrLf &
                                    "Content-Length: 9" & vbCrLf &
                                    "Connection: close" & vbCrLf &
                                    "X-Content-Type-Options: nosniff" & vbCrLf & vbCrLf &
                                    "Forbidden"
                    Dim forbiddenBytes = Encoding.UTF8.GetBytes(forbidden)
                    stream.Write(forbiddenBytes, 0, forbiddenBytes.Length)
                    stream.Flush()
                ElseIf File.Exists(localPath) Then
                    Dim contentBytes = File.ReadAllBytes(localPath)
                    Dim contentType = "application/octet-stream"
                    If localPath.EndsWith(".html", StringComparison.OrdinalIgnoreCase) Then contentType = "text/html; charset=utf-8"
                    If localPath.EndsWith(".js", StringComparison.OrdinalIgnoreCase) Then contentType = "application/javascript; charset=utf-8"
                    If localPath.EndsWith(".css", StringComparison.OrdinalIgnoreCase) Then contentType = "text/css; charset=utf-8"
                    If localPath.EndsWith(".json", StringComparison.OrdinalIgnoreCase) Then contentType = "application/json"

                    Dim httpHeader = $"HTTP/1.1 200 OK" & vbCrLf &
                                     $"Content-Type: {contentType}" & vbCrLf &
                                     $"Content-Length: {contentBytes.Length}" & vbCrLf &
                                     $"Connection: close" & vbCrLf &
                                     $"X-Content-Type-Options: nosniff" & vbCrLf &
                                     $"X-Frame-Options: SAMEORIGIN" & vbCrLf &
                                     $"Referrer-Policy: no-referrer" & vbCrLf &
                                     $"Content-Security-Policy: default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline'; img-src 'self' data: blob:; media-src 'self' blob:; connect-src 'self' ws: wss:; object-src 'none'; base-uri 'self'; frame-ancestors 'self'" & vbCrLf & vbCrLf
                    Dim headerBytes = Encoding.UTF8.GetBytes(httpHeader)
                    stream.Write(headerBytes, 0, headerBytes.Length)
                    stream.Write(contentBytes, 0, contentBytes.Length)
                    stream.Flush()
                Else
                    Dim notFound = "HTTP/1.1 404 Not Found" & vbCrLf &
                                   "Content-Length: 0" & vbCrLf &
                                   "Connection: close" & vbCrLf &
                                   "X-Content-Type-Options: nosniff" & vbCrLf & vbCrLf
                    Dim notFoundBytes = Encoding.UTF8.GetBytes(notFound)
                    stream.Write(notFoundBytes, 0, notFoundBytes.Length)
                    stream.Flush()
                End If
                client.Close()
            End If

        Catch ex As Exception
            RaiseEvent ServerLog($"[Server Error] {ex.Message}")
        Finally
            If isPhoneRegistered AndAlso Not String.IsNullOrEmpty(remoteEndpointStr) Then
                Dim removedDevice As ConnectedDeviceInfo = Nothing
                _activePhones.TryRemove(remoteEndpointStr, removedDevice)
                RaiseEvent PhoneDisconnected(deviceName)
                RaiseEvent ServerLog($"📴 {deviceName} terputus")
            End If
            Try
                client.Close()
            Catch
            End Try
            Interlocked.Decrement(_activeConnections)
        End Try
    End Sub

    Private Function IsAllowedOrigin(origin As String) As Boolean
        Try
            Dim originUri As New Uri(origin)
            Dim host = originUri.Host
            If host.Equals("localhost", StringComparison.OrdinalIgnoreCase) OrElse
               host = "127.0.0.1" OrElse host = "[::1]" Then
                Return True
            End If
            ' Bandingkan dengan IP aktual server ini (bukan prefix range yang longgar)
            For Each info In GetAvailableIPAddresses()
                If host.Equals(info.IP, StringComparison.OrdinalIgnoreCase) Then Return True
            Next
        Catch
        End Try
        Return False
    End Function

    Private Function ReadHttpHeader(stream As Stream) As String
        Dim ms As New MemoryStream()
        Dim prevB As Integer = -1
        Dim b As Integer = 0
        Dim count As Integer = 0

        While count < 8192
            b = stream.ReadByte()
            If b = -1 Then Exit While
            ms.WriteByte(CByte(b))
            count += 1

            Dim bytes = ms.ToArray()
            If bytes.Length >= 4 Then
                Dim l = bytes.Length
                If bytes(l - 4) = 13 AndAlso bytes(l - 3) = 10 AndAlso bytes(l - 2) = 13 AndAlso bytes(l - 1) = 10 Then
                    Return Encoding.UTF8.GetString(bytes)
                End If
            End If
        End While

        Return Encoding.UTF8.GetString(ms.ToArray())
    End Function

    Private Function GetHeaderValue(lines As String(), headerName As String) As String
        For Each line In lines
            Dim idx = line.IndexOf(":"c)
            If idx > 0 Then
                Dim key = line.Substring(0, idx).Trim()
                If key.Equals(headerName, StringComparison.OrdinalIgnoreCase) Then
                    Return line.Substring(idx + 1).Trim()
                End If
            End If
        Next
        Return ""
    End Function

    Private Shared Function ReadExactly(stream As Stream, buffer As Byte(), count As Integer) As Boolean
        Dim offset = 0
        While offset < count
            Dim read = stream.Read(buffer, offset, count - offset)
            If read <= 0 Then Return False
            offset += read
        End While
        Return True
    End Function

    Private Function ReadWebSocketFrame(stream As Stream) As String
        While True
            Dim b1 = stream.ReadByte()
            If b1 = -1 Then Return Nothing
            Dim fin = (b1 And &H80) <> 0
            Dim rsv = b1 And &H70
            Dim opcode = b1 And &HF

            ' Reserved bits harus 0; frame terfragmentasi tidak didukung
            If rsv <> 0 OrElse Not fin Then Return Nothing

            Dim b2 = stream.ReadByte()
            If b2 = -1 Then Return Nothing
            Dim isMasked = (b2 And &H80) <> 0
            Dim payloadLen = CLng(b2 And &H7F)

            If payloadLen = 126 Then
                Dim ext(1) As Byte
                If Not ReadExactly(stream, ext, 2) Then Return Nothing
                payloadLen = (CInt(ext(0)) << 8) Or CInt(ext(1))
            ElseIf payloadLen = 127 Then
                Dim ext(7) As Byte
                If Not ReadExactly(stream, ext, 8) Then Return Nothing
                payloadLen = 0
                For i = 0 To 7
                    payloadLen = (payloadLen << 8) Or CLng(ext(i))
                Next
            End If

            ' RFC 6455 §5.1: frame dari klien WAJIB di-mask; tolak jika tidak
            If Not isMasked Then Return Nothing

            ' Batasi maksimal 64 KB per frame untuk mencegah OutOfMemory DoS
            If payloadLen > 65536 OrElse payloadLen < 0 Then
                Return Nothing
            End If

            Dim mask(3) As Byte
            If Not ReadExactly(stream, mask, 4) Then Return Nothing

            Dim len32 = CInt(payloadLen)
            Dim payload = New Byte(Math.Max(len32, 1) - 1) {}
            If len32 > 0 AndAlso Not ReadExactly(stream, payload, len32) Then Return Nothing

            For i = 0 To len32 - 1
                payload(i) = CByte(payload(i) Xor mask(i Mod 4))
            Next

            Select Case opcode
                Case 1 ' text
                    Return Encoding.UTF8.GetString(payload, 0, len32)
                Case 8 ' close
                    Return Nothing
                Case 9 ' ping → balas pong, lanjut baca
                    SendFrame(stream, 10, payload, len32)
                Case 10 ' pong → abaikan, lanjut baca
                    Continue While
                Case Else ' binary/continuation tidak didukung
                    Return Nothing
            End Select
            ' opcode 9 (ping): pong sudah dikirim, lanjut baca frame berikutnya
        End While

        Return Nothing
    End Function

    Public Sub SendWebSocketText(stream As Stream, text As String)
        Dim data = Encoding.UTF8.GetBytes(text)
        SendFrame(stream, 1, data, data.Length)
    End Sub

    Private Sub SendFrame(stream As Stream, opcode As Integer, data As Byte(), dataLength As Integer)
        Dim len As Long = dataLength

        If len <= 125 Then
            Dim header = {CByte(&H80 Or opcode), CByte(len)}
            stream.Write(header, 0, header.Length)
        ElseIf len <= 65535 Then
            Dim header = {CByte(&H80 Or opcode), CByte(126), CByte((len >> 8) And &HFF), CByte(len And &HFF)}
            stream.Write(header, 0, header.Length)
        Else
            Dim header(9) As Byte
            header(0) = CByte(&H80 Or opcode)
            header(1) = 127
            For i = 0 To 7
                header(9 - i) = CByte((len >> (i * 8)) And &HFF)
            Next
            stream.Write(header, 0, header.Length)
        End If

        If dataLength > 0 Then stream.Write(data, 0, dataLength)
        stream.Flush()
    End Sub

    Private Function ParseJsonMessage(json As String) As Dictionary(Of String, Object)
        Try
            Dim serializer As New JavaScriptSerializer()
            serializer.MaxJsonLength = 65536
            serializer.RecursionLimit = 20
            Return TryCast(serializer.DeserializeObject(json), Dictionary(Of String, Object))
        Catch
        End Try
        Return Nothing
    End Function

    Private Shared Function GetJsonString(dict As Dictionary(Of String, Object), key As String) As String
        Dim val As Object = Nothing
        If dict.TryGetValue(key, val) AndAlso val IsNot Nothing Then
            Return val.ToString()
        End If
        Return ""
    End Function

    Private Shared Function SerializeJson(dict As Dictionary(Of String, Object)) As String
        Dim serializer As New JavaScriptSerializer()
        Return serializer.Serialize(dict)
    End Function

    Public Shared Function GetAvailableIPAddresses() As List(Of NetworkIPInfo)
        Dim list As New List(Of NetworkIPInfo)()
        Try
            For Each ni In NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                If ni.OperationalStatus <> NetworkInformation.OperationalStatus.Up Then Continue For
                If ni.NetworkInterfaceType = NetworkInformation.NetworkInterfaceType.Loopback Then Continue For

                Dim props = ni.GetIPProperties()
                Dim hasGateway = False
                For Each gw In props.GatewayAddresses
                    If gw.Address.AddressFamily = Sockets.AddressFamily.InterNetwork AndAlso gw.Address.ToString() <> "0.0.0.0" Then
                        hasGateway = True
                        Exit For
                    End If
                Next

                Dim descLower = (ni.Name & " " & ni.Description).ToLowerInvariant()
                Dim isVirtual = descLower.Contains("virtual") OrElse descLower.Contains("pseudo") OrElse
                                descLower.Contains("loopback") OrElse descLower.Contains("wsl") OrElse
                                descLower.Contains("hyper-v") OrElse descLower.Contains("vmware") OrElse
                                descLower.Contains("virtualbox") OrElse descLower.Contains("wi-fi direct")

                For Each addr In props.UnicastAddresses
                    If addr.Address.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                        Dim ipStr = addr.Address.ToString()
                        If Not ipStr.StartsWith("169.254") AndAlso Not ipStr.StartsWith("127.") Then
                            Dim priority = 2
                            If hasGateway AndAlso Not isVirtual Then
                                priority = 1
                            ElseIf isVirtual Then
                                priority = 3
                            End If

                            list.Add(New NetworkIPInfo With {
                                .IP = ipStr,
                                .AdapterName = ni.Name,
                                .Description = ni.Description,
                                .HasGateway = hasGateway,
                                .IsVirtual = isVirtual,
                                .Priority = priority
                            })
                        End If
                    End If
                Next
            Next
        Catch
        End Try

        list.Sort(Function(a, b) a.Priority.CompareTo(b.Priority))

        If list.Count = 0 Then
            list.Add(New NetworkIPInfo With {
                .IP = "127.0.0.1",
                .AdapterName = "Localhost",
                .Description = "Loopback",
                .Priority = 99
            })
        End If

        Return list
    End Function

    Public Shared Function GetLocalIPAddress() As String
        Dim list = GetAvailableIPAddresses()
        Return list(0).IP
    End Function
End Class

Public Class NetworkIPInfo
    Public Property IP As String
    Public Property AdapterName As String
    Public Property Description As String
    Public Property HasGateway As Boolean
    Public Property IsVirtual As Boolean
    Public Property Priority As Integer

    Public Overrides Function ToString() As String
        Return $"{IP} ({AdapterName})"
    End Function
End Class

Public Class ConnectedDeviceInfo
    Public Property Endpoint As String = ""
    Public Property DeviceName As String = ""
    Public Property IP As String = ""
    Public Property ConnectedAt As DateTime = DateTime.Now

    Public Overrides Function ToString() As String
        Return $"{DeviceName} ({IP})"
    End Function
End Class
