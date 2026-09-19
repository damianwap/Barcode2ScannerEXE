Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Text

''' <summary>
''' Mengelola sertifikat TLS self-signed PER-INSTALASI yang dibuat dengan certreq.exe
''' (bawaan Windows) dan disimpan di certificate store CurrentUser\My.
''' Private key tidak pernah diekspor ke file dan tidak ada password hardcoded —
''' kunci dilindungi ACL key container Windows milik pengguna saat ini.
''' Thumbprint sertifikat dipersist di %APPDATA%\Barcode2Scanner\cert-thumbprint.txt.
''' </summary>
Public Module CertManager
    Private Const SubjectName As String = "CN=Barcode2Scanner Local"
    Private Const FriendlyName As String = "Barcode2Scanner Local"

    Private Function AppDataDir() As String
        Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Barcode2Scanner")
    End Function

    Private Function ThumbprintFile() As String
        Return Path.Combine(AppDataDir(), "cert-thumbprint.txt")
    End Function

    ''' <summary>
    ''' Mengembalikan sertifikat yang sudah ada, atau membuat + menginstal yang baru.
    ''' </summary>
    Public Function EnsureCertificate(allowedIPs As List(Of String)) As X509Certificate2
        Directory.CreateDirectory(AppDataDir())

        If File.Exists(ThumbprintFile()) Then
            Try
                Dim thumb = File.ReadAllText(ThumbprintFile()).Trim()
                Dim existing = FindInStore(thumb)
                If existing IsNot Nothing AndAlso existing.HasPrivateKey AndAlso
                   existing.NotAfter > DateTime.Now.AddDays(30) Then
                    Return existing
                End If
            Catch
                ' Thumbprint rusak / store tidak bisa dibaca — regenerate di bawah
            End Try
        End If

        Dim newCert = GenerateAndInstall(allowedIPs)
        File.WriteAllText(ThumbprintFile(), newCert.Thumbprint)
        Return newCert
    End Function

    Private Function FindInStore(thumbprint As String) As X509Certificate2
        Dim store As New X509Store(StoreName.My, StoreLocation.CurrentUser)
        Try
            store.Open(OpenFlags.ReadOnly)
            Dim found = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, False)
            If found.Count > 0 Then Return found(0)
        Finally
            store.Close()
        End Try
        Return Nothing
    End Function

    Private Function GenerateAndInstall(allowedIPs As List(Of String)) As X509Certificate2
        ' Susun SAN: DNS localhost + semua IP IPv4 aktif (deduplikasi)
        Dim sanParts As New List(Of String) From {"DNS=localhost"}
        Dim seenIPs As New HashSet(Of String)
        For Each ip In allowedIPs
            Dim parsed As IPAddress = Nothing
            If IPAddress.TryParse(ip, parsed) AndAlso parsed.AddressFamily = Sockets.AddressFamily.InterNetwork AndAlso seenIPs.Add(ip) Then
                sanParts.Add($"IPAddress={ip}")
            End If
        Next
        If seenIPs.Add("127.0.0.1") Then
            sanParts.Add("IPAddress=127.0.0.1")
        End If

        Dim sb As New StringBuilder()
        sb.AppendLine("[NewRequest]")
        sb.AppendLine($"Subject = ""{SubjectName}""")
        sb.AppendLine($"FriendlyName = ""{FriendlyName}""")
        sb.AppendLine("KeyLength = 2048")
        sb.AppendLine("KeyAlgorithm = RSA")
        sb.AppendLine("KeyUsage = 0xA0") ' digitalSignature | keyEncipherment
        sb.AppendLine("MachineKeySet = FALSE")
        sb.AppendLine("RequestType = Cert") ' self-signed, langsung diinstal ke store
        sb.AppendLine("HashAlgorithm = sha256")
        sb.AppendLine("ValidityPeriod = Years")
        sb.AppendLine("ValidityPeriodUnits = 10")
        sb.AppendLine("ProviderName = ""Microsoft Software Key Storage Provider""")
        sb.AppendLine()
        sb.AppendLine("[Extensions]")
        sb.AppendLine("2.5.29.37 = ""{text}1.3.6.1.5.5.7.3.1""") ' EKU serverAuth
        sb.AppendLine($"2.5.29.17 = ""{{text}}{String.Join("&", sanParts)}""") ' SAN

        Dim tmpDir = Path.Combine(AppDataDir(), "tmp")
        Directory.CreateDirectory(tmpDir)
        Dim infPath = Path.Combine(tmpDir, $"cert-{Guid.NewGuid():N}.inf")
        Dim cerPath = Path.ChangeExtension(infPath, ".cer")
        File.WriteAllText(infPath, sb.ToString(), Encoding.ASCII)

        Try
            Dim psi As New ProcessStartInfo("certreq.exe", $"-new -q ""{infPath}"" ""{cerPath}""")
            psi.UseShellExecute = False
            psi.CreateNoWindow = True
            psi.RedirectStandardOutput = True
            psi.RedirectStandardError = True

            Using proc = Process.Start(psi)
                proc.StandardOutput.ReadToEnd()
                proc.StandardError.ReadToEnd()
                If Not proc.WaitForExit(60000) Then
                    Try
                        proc.Kill()
                    Catch
                    End Try
                    Throw New InvalidOperationException("certreq.exe timeout saat membuat sertifikat TLS lokal.")
                End If
                If proc.ExitCode <> 0 OrElse Not File.Exists(cerPath) Then
                    Throw New InvalidOperationException($"certreq.exe gagal membuat sertifikat TLS lokal (exit code {proc.ExitCode}).")
                End If
            End Using

            Dim fileCert As New X509Certificate2(cerPath)
            Dim installed = FindInStore(fileCert.Thumbprint)
            If installed Is Nothing OrElse Not installed.HasPrivateKey Then
                Throw New InvalidOperationException("Sertifikat TLS lokal terinstal tidak ditemukan di store atau tidak memiliki private key.")
            End If
            Return installed
        Finally
            Try
                File.Delete(infPath)
            Catch
            End Try
            Try
                File.Delete(cerPath)
            Catch
            End Try
        End Try
    End Function
End Module
