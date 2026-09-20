Imports System
Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports QRCoder

Public Class Form1
    <DllImport("user32.dll")>
    Private Shared Sub keybd_event(bVk As Byte, bScan As UShort, dwFlags As UInteger, dwExtraInfo As UIntPtr)
    End Sub

    Private Const KEYEVENTF_KEYDOWN As UInteger = 0
    Private Const KEYEVENTF_KEYUP As UInteger = 2
    Private Const KEYEVENTF_UNICODE As UInteger = 4

    Private Const VK_RETURN As Byte = &HD
    Private Const VK_TAB As Byte = &H9

    Private WithEvents _server As ScannerServer
    Private _totalScans As Integer = 0
    Private _currentUrl As String = ""
    Private _easterEggBuffer As String = ""
    Private _qrRevealed As Boolean = False
    Private _qrHideTimer As Windows.Forms.Timer = Nothing

    ' Real-time output: file CSV/XML yang ditulis otomatis tiap scan masuk.
    ' - CSV: append per baris (selalu valid). Kalau file sedang dibuka/dikunci Excel,
    '   baris ditampung di _rtPending dan ditulis saat file bebas (tidak auto-mati).
    ' - XML (SpreadsheetML, terbuka di Excel tanpa library): ditulis ULANG utuh tiap
    '   scan (header + semua baris + footer) agar file selalu valid dan bisa dibuka
    '   kapan saja. Append mentah ke XML membuatnya corrupt (tanpa tag penutup).
    Private _rtOutputEnabled As Boolean = False
    Private _rtOutputPath As String = ""
    Private _rtGoogleUrl As String = ""
    Private _rtRows As New List(Of RtScanRow)()
    Private _rtPending As New List(Of RtScanRow)()
    Private _rtLock As New Object()

    Private Class RtScanRow
        Public Property No As Integer
        Public Property Waktu As DateTime
        Public Property Device As String
        Public Property Barcode As String
        Public Property Format As String
    End Class

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        cmbSuffix.SelectedIndex = 0

        Try
            Dim icoPath = Path.Combine(Application.StartupPath, "app.ico")
            If Not File.Exists(icoPath) Then
                icoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.ico")
            End If
            If File.Exists(icoPath) Then
                Me.Icon = New Icon(icoPath)
            End If
        Catch
        End Try

        ' Isi daftar IP adapter jaringan yang tersedia
        Dim ipList = ScannerServer.GetAvailableIPAddresses()
        cmbNetworkIP.Items.Clear()
        For Each info In ipList
            cmbNetworkIP.Items.Add(info)
        Next
        If cmbNetworkIP.Items.Count > 0 Then
            cmbNetworkIP.SelectedIndex = 0
        End If

        ' Cari path public directory
        Dim publicDir = Path.Combine(Application.StartupPath, "public")
        If Not Directory.Exists(publicDir) Then
            publicDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public")
        End If

        Try
            ' Sertifikat TLS self-signed per-instalasi (disimpan di CurrentUser\My,
            ' tanpa file PFX dan tanpa password hardcoded)
            Dim certIPs As New List(Of String) From {"127.0.0.1"}
            For Each info In ipList
                If Not certIPs.Contains(info.IP) Then certIPs.Add(info.IP)
            Next
            Dim certificate = CertManager.EnsureCertificate(certIPs)

            _server = New ScannerServer(3443, certificate, publicDir)
            _server.Start()

            SetQrRevealed(False)
            UpdateQRCode()
            UpdatePhoneStatusUI()

            ' Susun docking dan z-order agar tidak ada kontrol yang saling tumpang tindih:
            ' pnlStats dan pnlDevicesBar berada di bagian atas, dgvScan mengisi ruang tengah (Fill), pnlBottom di bawah
            dgvScan.Dock = DockStyle.Fill
            pnlBottom.SendToBack()
            pnlStats.SendToBack()
            pnlDevicesBar.BringToFront()
            dgvScan.BringToFront()

            lblPhoneStatus.Cursor = Cursors.Hand
            cardDevice.Cursor = Cursors.Hand
            lblDeviceVal.Cursor = Cursors.Hand
            pnlDevicesBar.Cursor = Cursors.Hand
            lblDevicesBarTitle.Cursor = Cursors.Hand

            ' KEAMANAN: tampilkan fingerprint SHA-256 agar user bisa verifikasi
            ' peringatan browser (self-signed) dan mendeteksi MITM ARP-spoof.
            lblLog.Text = $"Server HTTPS aktif :3443 — Cert SHA256: {CertManager.GetSha256Fingerprint(certificate)}"
        Catch ex As Exception
            MessageBox.Show("Gagal memulai server internal:" & vbCrLf & ex.Message, "Error Server", MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblServerStatus.Text = "● Server Gagal"
            lblServerStatus.ForeColor = Color.FromArgb(252, 165, 165)
            lblServerStatus.BackColor = Color.FromArgb(69, 10, 10)
        End Try
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            _server?.StopServer()
        Catch
        End Try
        Try
            If _qrHideTimer IsNot Nothing Then _qrHideTimer.Dispose()
        Catch
        End Try
    End Sub

    Private Sub UpdateQRCode()
        If _server Is Nothing Then Return

        Dim selectedInfo As NetworkIPInfo = TryCast(cmbNetworkIP.SelectedItem, NetworkIPInfo)
        Dim ip = If(selectedInfo IsNot Nothing, selectedInfo.IP, ScannerServer.GetLocalIPAddress())
        _currentUrl = $"https://{ip}:3443/scan.html?session={_server.CurrentSessionCode}&key={_server.CurrentJoinKey}"

        ApplyQrMask()
        lblServerStatus.Text = $"● Server: https://{ip}:3443"

        If Not _qrRevealed Then Return
        RenderQrImage()
    End Sub

    ''' <summary>
    ''' QR + kode sesi + URL disembunyikan by default agar tidak bisa difoto /
    ''' diintip dari layar (shoulder-surfing). Render gambar hanya saat user
    ''' menekan tombol Tampilkan, dan sembunyikan lagi otomatis setelah 60 detik.
    ''' </summary>
    Private Sub SetQrRevealed(revealed As Boolean)
        _qrRevealed = revealed

        If _qrHideTimer IsNot Nothing Then
            _qrHideTimer.Stop()
            _qrHideTimer.Dispose()
            _qrHideTimer = Nothing
        End If

        If _qrRevealed Then
            btnToggleQR.Text = "🙈 Sembunyikan QR"
            picQRCode.Visible = True
            lnkUrl.Visible = True
            cardQR.Size = New Size(316, 374)
            RenderQrImage()
            _qrHideTimer = New Windows.Forms.Timer()
            _qrHideTimer.Interval = 60000
            AddHandler _qrHideTimer.Tick, Sub()
                                              SetQrRevealed(False)
                                              lblLog.Text = "QR disembunyikan otomatis (60 detik). Tekan Tampilkan untuk scan ulang."
                                          End Sub
            _qrHideTimer.Start()
        Else
            btnToggleQR.Text = "👁 Tampilkan QR untuk Scan"
            picQRCode.Visible = False
            lnkUrl.Visible = False
            cardQR.Size = New Size(316, 190)
            If picQRCode.Image IsNot Nothing Then
                picQRCode.Image.Dispose()
                picQRCode.Image = Nothing
            End If
            ApplyQrMask()
        End If
    End Sub

    Private Sub ApplyQrMask()
        If _server Is Nothing OrElse _qrRevealed Then Return
        lblSessionCode.Text = "KODE SESI: ••••••"
        lnkUrl.Text = "📋 Salin Link Web HP"
    End Sub

    Private Sub RenderQrImage()
        If _server Is Nothing OrElse String.IsNullOrEmpty(_currentUrl) Then Return
        lblSessionCode.Text = $"KODE SESI: {_server.CurrentSessionCode}"
        lnkUrl.Text = "📋 Salin Link Web HP"
        Using qrGen As New QRCodeGenerator()
            Using qrData = qrGen.CreateQrCode(_currentUrl, QRCodeGenerator.ECCLevel.Q)
                Using qrCode As New QRCode(qrData)
                    Dim oldBmp = picQRCode.Image
                    picQRCode.Image = qrCode.GetGraphic(12, Color.Black, Color.White, True)
                    If oldBmp IsNot Nothing Then oldBmp.Dispose()
                End Using
            End Using
        End Using
    End Sub

    Private Sub btnToggleQR_Click(sender As Object, e As EventArgs) Handles btnToggleQR.Click
        SetQrRevealed(Not _qrRevealed)
    End Sub

    Private Sub UpdatePhoneStatusUI()
        Dim devices = If(_server IsNot Nothing, _server.ActiveDevices, New List(Of ConnectedDeviceInfo)())
        Dim count = devices.Count

        lblDevicesBarTitle.Text = $"📱 HP Terhubung ({count}):"
        flpDevices.Controls.Clear()

        If count = 0 Then
            lblPhoneStatus.Text = "● HP Belum Tersambung"
            lblPhoneStatus.ForeColor = Color.FromArgb(251, 191, 36)
            lblPhoneStatus.BackColor = Color.FromArgb(69, 26, 3)
            lblDeviceVal.Text = "-"
            toolTipDevices.SetToolTip(lblPhoneStatus, "Belum ada HP yang tersambung. Scan QR Code di sebelah kiri.")
            toolTipDevices.SetToolTip(cardDevice, "Belum ada HP yang tersambung. Scan QR Code di sebelah kiri.")

            Dim lblEmpty As New Label()
            lblEmpty.Text = "(Belum ada HP terhubung — scan QR Code di sebelah kiri untuk menghubungkan)"
            lblEmpty.Font = New Font("Segoe UI", 8.25!, FontStyle.Italic)
            lblEmpty.ForeColor = Color.FromArgb(148, 163, 184)
            lblEmpty.AutoSize = True
            lblEmpty.Margin = New Padding(3, 5, 3, 3)
            flpDevices.Controls.Add(lblEmpty)
        Else
            Dim deviceNames = devices.Select(Function(d) d.DeviceName).ToList()
            Dim phoneListStr = String.Join(", ", deviceNames)

            If count = 1 Then
                lblPhoneStatus.Text = $"● 1 HP: {devices(0).DeviceName}"
                lblDeviceVal.Text = devices(0).DeviceName
            Else
                lblPhoneStatus.Text = $"● {count} HP: {phoneListStr}"
                lblDeviceVal.Text = $"{count} HP: {phoneListStr}"
            End If

            lblPhoneStatus.ForeColor = Color.FromArgb(52, 211, 153)
            lblPhoneStatus.BackColor = Color.FromArgb(16, 45, 35)

            Dim tipLines As New List(Of String)()
            tipLines.Add($"📱 Perangkat Terhubung Aktif ({count}):")
            For i = 0 To devices.Count - 1
                tipLines.Add($"{i + 1}. {devices(i).DeviceName} (IP: {devices(i).IP}) - Sejak {devices(i).ConnectedAt:HH:mm:ss}")
            Next
            tipLines.Add("")
            tipLines.Add("👉 Klik untuk melihat rincian seluruh perangkat.")
            Dim tipText = String.Join(vbCrLf, tipLines)

            toolTipDevices.SetToolTip(lblPhoneStatus, tipText)
            toolTipDevices.SetToolTip(cardDevice, tipText)
            toolTipDevices.SetToolTip(lblDeviceVal, tipText)
            toolTipDevices.SetToolTip(pnlDevicesBar, tipText)
            toolTipDevices.SetToolTip(lblDevicesBarTitle, tipText)

            For Each dev In devices
                Dim badge As New Label()
                badge.Text = $"● {dev.DeviceName} ({dev.IP})"
                badge.Font = New Font("Segoe UI", 8.25!, FontStyle.Bold)
                badge.ForeColor = Color.FromArgb(163, 230, 53)
                badge.BackColor = Color.FromArgb(15, 30, 45)
                badge.BorderStyle = BorderStyle.FixedSingle
                badge.Padding = New Padding(8, 3, 8, 3)
                badge.Margin = New Padding(3, 2, 3, 2)
                badge.AutoSize = True
                badge.Cursor = Cursors.Hand
                toolTipDevices.SetToolTip(badge, $"Perangkat: {dev.DeviceName}" & vbCrLf & $"Alamat IP: {dev.IP}" & vbCrLf & $"Tersambung sejak: {dev.ConnectedAt:HH:mm:ss}" & vbCrLf & "Klik untuk melihat daftar lengkap.")
                AddHandler badge.Click, AddressOf ShowConnectedDevicesDialog
                flpDevices.Controls.Add(badge)
            Next
        End If
    End Sub

    Private Sub ShowConnectedDevicesDialog(Optional sender As Object = Nothing, Optional e As EventArgs = Nothing) Handles pnlDevicesBar.Click, pnlDevicesBar.DoubleClick, lblDevicesBarTitle.Click, lblDevicesBarTitle.DoubleClick, cardDevice.Click, lblDeviceVal.Click, lblPhoneStatus.Click
        Dim devices = If(_server IsNot Nothing, _server.ActiveDevices, New List(Of ConnectedDeviceInfo)())
        If devices.Count = 0 Then
            MessageBox.Show("Belum ada perangkat HP yang terhubung saat ini." & vbCrLf & vbCrLf &
                            "Cara Menghubungkan HP:" & vbCrLf &
                            "1. Pastikan HP dan Laptop berada di jaringan Wi-Fi yang sama." & vbCrLf &
                            "2. Scan QR Code di panel kiri menggunakan kamera HP Anda." & vbCrLf &
                            "3. Buka halaman scanner dan izinkan akses kamera HP.",
                            "Perangkat HP Belum Terhubung", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Dim sb As New StringBuilder()
            sb.AppendLine($"Daftar Perangkat HP yang Sedang Terhubung ({devices.Count} HP):")
            sb.AppendLine(New String("="c, 50))
            For i = 0 To devices.Count - 1
                Dim d = devices(i)
                sb.AppendLine($"{i + 1}. Nama Perangkat : {d.DeviceName}")
                sb.AppendLine($"   Alamat IP      : {d.IP}")
                sb.AppendLine($"   Tersambung Jam : {d.ConnectedAt:HH:mm:ss} ({d.ConnectedAt:dd/MM/yyyy})")
                If i < devices.Count - 1 Then sb.AppendLine(New String("-"c, 40))
            Next
            MessageBox.Show(sb.ToString(), "Daftar Perangkat HP Terhubung", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub _server_PhoneConnected(deviceName As String) Handles _server.PhoneConnected
        If Me.InvokeRequired Then
            Me.BeginInvoke(Sub() _server_PhoneConnected(deviceName))
            Return
        End If

        UpdatePhoneStatusUI()
        lblLog.Text = $"📱 Perangkat [{deviceName}] terhubung!"

        ' Bunyi notifikasi kecil
        Try
            System.Media.SystemSounds.Asterisk.Play()
        Catch
        End Try
    End Sub

    Private Sub _server_PhoneDisconnected(deviceName As String) Handles _server.PhoneDisconnected
        If Me.InvokeRequired Then
            Me.BeginInvoke(Sub() _server_PhoneDisconnected(deviceName))
            Return
        End If

        UpdatePhoneStatusUI()
        lblLog.Text = $"📴 Perangkat [{deviceName}] terputus"
    End Sub

    Private Sub _server_BarcodeScanned(text As String, format As String, deviceName As String) Handles _server.BarcodeScanned
        If Me.InvokeRequired Then
            Me.BeginInvoke(Sub() _server_BarcodeScanned(text, format, deviceName))
            Return
        End If

        _totalScans += 1
        lblTotalVal.Text = _totalScans.ToString()
        lblLastVal.Text = text
        lblLog.Text = $"[SCAN] {text} ({format}) dari {deviceName}"

        ' Tambahkan ke DataGridView di posisi paling atas
        dgvScan.Rows.Insert(0, _totalScans, DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), deviceName, text, format)

        If dgvScan.Rows.Count > 0 Then
            dgvScan.ClearSelection()
            dgvScan.Rows(0).Selected = True
            dgvScan.FirstDisplayedScrollingRowIndex = 0
        End If

        ' Suara Beep
        If chkBeep.Checked Then
            Dim soundThread As New Thread(Sub()
                Try
                    Console.Beep(1800, 90)
                Catch
                    System.Media.SystemSounds.Beep.Play()
                End Try
            End Sub)
            soundThread.IsBackground = True
            soundThread.Start()
        End If

        ' Auto-Type ke aplikasi yang sedang aktif (Notepad, Excel, POS, dll)
        If chkAutoType.Checked Then
            Dim suffixMode = If(cmbSuffix.SelectedItem IsNot Nothing, cmbSuffix.SelectedItem.ToString(), "Enter")
            SendTextToActiveWindow(text, suffixMode)
        End If

        AppendRealTimeOutput(DateTime.Now, deviceName, text, format)
        PostToGoogleSheets(DateTime.Now, deviceName, text, format)
    End Sub

    Private Sub _server_ServerLog(message As String) Handles _server.ServerLog
        If Me.InvokeRequired Then
            Me.BeginInvoke(Sub() _server_ServerLog(message))
            Return
        End If
        lblLog.Text = message
    End Sub

    ''' <summary>
    ''' Mengirimkan karakter secara langsung ke Windows Input Subsystem
    ''' sehingga otomatis terketik di aplikasi mana pun yang sedang aktif (Notepad, Excel, Software POS).
    ''' </summary>
    Private Sub SendTextToActiveWindow(text As String, suffixSetting As String)
        If String.IsNullOrEmpty(text) Then Return

        ' KEAMANAN: defense-in-depth — server sudah memanggil SanitizeScanText (max 512,
        ' allowlist alfanumerik + simbol barcode), tapi validasi ulang di sini karena
        ' event BarcodeScanned adalah trust boundary terakhir sebelum keybd_event.
        ' Auto-Type default NONAKTIF (lihat Designer) sehingga user harus opt-in eksplisit.
        text = ScannerServer.SanitizeScanText(text)
        If String.IsNullOrEmpty(text) Then Return

        Dim safeChars As New List(Of Char)(text.Length)
        For Each c In text
            safeChars.Add(c)
        Next

        Dim t As New Thread(Sub()
            Try
                ' Jeda 35ms agar window message pump stabil
                Thread.Sleep(35)

                For Each c As Char In safeChars
                    Dim scanCode As UShort = Convert.ToUInt16(c)
                    ' Key Down UNICODE
                    keybd_event(0, scanCode, KEYEVENTF_UNICODE, UIntPtr.Zero)
                    ' Key Up UNICODE
                    keybd_event(0, scanCode, KEYEVENTF_UNICODE Or KEYEVENTF_KEYUP, UIntPtr.Zero)
                    Thread.Sleep(2)
                Next

                ' Suffix tombol (Enter / Tab)
                If suffixSetting.StartsWith("Enter") Then
                    Thread.Sleep(10)
                    keybd_event(VK_RETURN, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero)
                    keybd_event(VK_RETURN, 0, KEYEVENTF_KEYUP, UIntPtr.Zero)
                ElseIf suffixSetting.StartsWith("Tab") Then
                    Thread.Sleep(10)
                    keybd_event(VK_TAB, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero)
                    keybd_event(VK_TAB, 0, KEYEVENTF_KEYUP, UIntPtr.Zero)
                End If
            Catch
            End Try
        End Sub)
        t.IsBackground = True
        t.Start()
    End Sub

    Private Sub btnNewSession_Click(sender As Object, e As EventArgs) Handles btnNewSession.Click
        If _server Is Nothing Then Return
        _server.GenerateNewSession()
        SetQrRevealed(False)
        UpdateQRCode()
        lblLog.Text = "Sesi baru dibuat & QR disembunyikan. Tekan Tampilkan untuk scan ulang dari HP."
    End Sub

    Private Sub cmbNetworkIP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNetworkIP.SelectedIndexChanged
        UpdateQRCode()
    End Sub

    Private Sub btnFirewall_Click(sender As Object, e As EventArgs) Handles btnFirewall.Click
        Try
            Dim psi As New ProcessStartInfo()
            psi.FileName = "netsh"
            psi.Arguments = "advfirewall firewall add rule name=""Barcode2Scanner Port 3443"" dir=in action=allow protocol=TCP localport=3443 profile=private,domain remoteip=localsubnet"
            psi.Verb = "runas"
            psi.UseShellExecute = True
            psi.WindowStyle = ProcessWindowStyle.Hidden
            Dim p = Process.Start(psi)
            p.WaitForExit()
            lblLog.Text = "Port 3443 diizinkan di Windows Defender Firewall."
            MessageBox.Show("Port 3443 berhasil diizinkan di Windows Defender Firewall!" & vbCrLf & "Sekarang HP dapat membuka halaman scanner dengan lancar.", "Firewall Diizinkan", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Permintaan izin administrator dibatalkan atau gagal:" & vbCrLf & ex.Message, "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub lnkUrl_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkUrl.LinkClicked
        If Not String.IsNullOrEmpty(_currentUrl) Then
            Try
                Clipboard.SetText(_currentUrl)
                lblLog.Text = "URL berhasil disalin ke clipboard!"
                MessageBox.Show("Link koneksi HP berhasil disalin ke clipboard:" & vbCrLf & _currentUrl & vbCrLf & vbCrLf &
                                "⚠️ Link ini berisi KUNCI koneksi — jangan dibagikan ke orang lain / grup chat. " &
                                "Siapa pun yang punya link bisa mengirim ketikan ke laptop Anda sampai sesi dirotasi.",
                                "Link Disalin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Catch ex As Exception
                MessageBox.Show("Gagal menyalin URL: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        If dgvScan.SelectedRows.Count > 0 Then
            Dim val = dgvScan.SelectedRows(0).Cells(3).Value?.ToString()
            If Not String.IsNullOrEmpty(val) Then
                Clipboard.SetText(val)
                lblLog.Text = $"Barcode '{val}' disalin ke clipboard!"
            End If
        Else
            MessageBox.Show("Pilih salah satu baris pada tabel barcode terlebih dahulu.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnExportCsv_Click(sender As Object, e As EventArgs) Handles btnExportCsv.Click
        If dgvScan.Rows.Count = 0 Then
            MessageBox.Show("Tidak ada data barcode untuk diekspor.", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using sfd As New SaveFileDialog()
            sfd.Filter = "CSV File (*.csv)|*.csv"
            sfd.FileName = $"Barcode_Scan_{DateTime.Now:yyyyMMdd_HHmmss}.csv"

            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    Using sw As New StreamWriter(sfd.FileName, False, Encoding.UTF8)
                        ' Header
                        sw.WriteLine("""No"",""Waktu"",""Perangkat"",""Barcode"",""Format""")

                        ' Data baris
                        For Each row As DataGridViewRow In dgvScan.Rows
                            Dim no = EscapeCsv(row.Cells(0).Value?.ToString())
                            Dim waktu = EscapeCsv(row.Cells(1).Value?.ToString())
                            Dim dev = EscapeCsv(row.Cells(2).Value?.ToString())
                            Dim barcode = EscapeCsv(row.Cells(3).Value?.ToString())
                            Dim fmt = EscapeCsv(row.Cells(4).Value?.ToString())

                            sw.WriteLine($"""{no}"",""{waktu}"",""{dev}"",""{barcode}"",""{fmt}""")
                        Next
                    End Using

                    lblLog.Text = "Data berhasil diekspor ke CSV!"
                    MessageBox.Show("Data barcode berhasil disimpan ke file CSV!" & vbCrLf & sfd.FileName, "Export Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Gagal menyimpan file CSV:" & vbCrLf & ex.Message, "Error Export", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Function EscapeCsv(val As String) As String
        If String.IsNullOrEmpty(val) Then Return ""
        Dim v = val.Replace("""", """""")
        ' Cegah CSV formula injection: nilai yang diawali =, +, -, @, Tab, atau CR
        ' bisa dieksekusi sebagai formula oleh Excel/LibreOffice — prefix dengan '
        Dim first = v(0)
        If first = "="c OrElse first = "+"c OrElse first = "-"c OrElse first = "@"c OrElse
           first = ChrW(9) OrElse first = ChrW(13) Then
            v = "'" & v
        End If
        Return v
    End Function

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If dgvScan.Rows.Count = 0 Then Return

        If MessageBox.Show("Apakah Anda yakin ingin mengosongkan riwayat barcode?", "Konfirmasi Bersihkan", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            dgvScan.Rows.Clear()
            _totalScans = 0
            lblTotalVal.Text = "0"
            lblLastVal.Text = "-"
            lblLog.Text = "Tabel barcode telah dibersihkan."
        End If
    End Sub

    ' ================= REAL-TIME CSV / XLSX =================

    Private Sub btnRealTime_Click(sender As Object, e As EventArgs) Handles btnRealTime.Click
        If Not _rtOutputEnabled Then
            Using sfd As New SaveFileDialog()
                sfd.Filter = "CSV File (*.csv)|*.csv|Excel XML (*.xml)|*.xml"
                sfd.FileName = $"Barcode_Live_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                If sfd.ShowDialog() <> DialogResult.OK Then Return
                _rtOutputPath = sfd.FileName
            End Using
            SyncLock _rtLock
                _rtRows.Clear()
                _rtPending.Clear()
            End SyncLock
            Try
                If _rtOutputPath.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) Then
                    File.WriteAllText(_rtOutputPath, BuildSpreadsheetMlHeader() & BuildSpreadsheetMlFooter(), Encoding.UTF8)
                Else
                    File.WriteAllText(_rtOutputPath, """No"",""Waktu"",""Perangkat"",""Barcode"",""Format""" & vbCrLf, Encoding.UTF8)
                End If
            Catch ex As Exception
                MessageBox.Show("Gagal menyiapkan file real-time:" & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try
            _rtOutputEnabled = True
            btnRealTime.Text = "⏺ Real-Time: ON"
            btnRealTime.BackColor = Color.FromArgb(185, 28, 28)
            lblOutputPath.Text = Path.GetFileName(_rtOutputPath)
            toolTipDevices.SetToolTip(lblOutputPath, _rtOutputPath)
            lblLog.Text = $"Real-time output AKTIF → {Path.GetFileName(_rtOutputPath)}"
        Else
            Try
                FlushRealTimeFile()
            Catch
            End Try
            _rtOutputEnabled = False
            btnRealTime.Text = "▶ Real-Time: OFF"
            btnRealTime.BackColor = Color.FromArgb(51, 65, 85)
            lblLog.Text = "Real-time output dimatikan."
        End If
    End Sub

    Private Sub AppendRealTimeOutput(waktu As DateTime, device As String, barcode As String, fmt As String)
        If Not _rtOutputEnabled OrElse String.IsNullOrEmpty(_rtOutputPath) Then Return
        Dim row As New RtScanRow With {
            .No = _totalScans,
            .Waktu = waktu,
            .Device = device,
            .Barcode = barcode,
            .Format = fmt
        }
        SyncLock _rtLock
            _rtPending.Add(row)
        End SyncLock
        Try
            FlushRealTimeFile()
            lblLog.Text = $"[SCAN] {barcode} ({fmt}) → tersimpan di {Path.GetFileName(_rtOutputPath)}"
        Catch ex As Exception
            lblLog.Text = $"[SCAN] {barcode} ({fmt}) → file terkunci, antre {_rtPending.Count} baris (tutup file di Excel untuk flush)"
        End Try
    End Sub

    ''' <summary>
    ''' Tulis antrean ke file. CSV: append baris baru (selalu valid).
    ''' XML: tulis ulang utuh (header + semua baris + footer) agar selalu bisa dibuka di Excel.
    ''' Melempar IOException jika file dikunci — baris tetap di antrean.
    ''' </summary>
    Private Sub FlushRealTimeFile()
        Dim pending As List(Of RtScanRow)
        Dim allRows As List(Of RtScanRow)
        SyncLock _rtLock
            If _rtPending.Count = 0 Then Return
            pending = New List(Of RtScanRow)(_rtPending)
            allRows = New List(Of RtScanRow)(_rtRows)
            allRows.AddRange(pending)
        End SyncLock

        If _rtOutputPath.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) Then
            Dim sb As New StringBuilder()
            sb.Append(BuildSpreadsheetMlHeader())
            For Each r In allRows
                sb.Append(BuildSpreadsheetMlRow(r))
            Next
            sb.Append(BuildSpreadsheetMlFooter())
            File.WriteAllText(_rtOutputPath, sb.ToString(), Encoding.UTF8)
        Else
            Dim sb As New StringBuilder()
            For Each r In pending
                sb.Append($"""{EscapeCsv(r.No.ToString())}"",""{EscapeCsv(r.Waktu.ToString("dd-MM-yyyy HH:mm:ss"))}"",""{EscapeCsv(r.Device)}"",""{EscapeCsv(r.Barcode)}"",""{EscapeCsv(r.Format)}""{vbCrLf}")
            Next
            AppendText(_rtOutputPath, sb.ToString())
        End If

        SyncLock _rtLock
            _rtRows.AddRange(pending)
            _rtPending.RemoveRange(0, pending.Count)
        End SyncLock
    End Sub

    Private Shared Sub AppendText(path As String, content As String)
        Using fs As New FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read)
            Using sw As New StreamWriter(fs, Encoding.UTF8)
                sw.Write(content)
            End Using
        End Using
    End Sub

    Private Shared Function XmlEsc(v As String) As String
        If String.IsNullOrEmpty(v) Then Return ""
        Return v.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("""", "&quot;")
    End Function

    Private Shared Function BuildSpreadsheetMlHeader() As String
        Return "<?xml version=""1.0"" encoding=""UTF-8""?>" & vbCrLf &
            "<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet"">" & vbCrLf &
            "<Worksheet ss:Name=""Scan"" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""><Table>" & vbCrLf &
            "<Row><Cell><Data ss:Type=""String"">No</Data></Cell>" &
            "<Cell><Data ss:Type=""String"">Waktu</Data></Cell>" &
            "<Cell><Data ss:Type=""String"">Perangkat</Data></Cell>" &
            "<Cell><Data ss:Type=""String"">Barcode</Data></Cell>" &
            "<Cell><Data ss:Type=""String"">Format</Data></Cell></Row>" & vbCrLf
    End Function

    Private Function BuildSpreadsheetMlRow(r As RtScanRow) As String
        Return $"<Row><Cell><Data ss:Type=""Number"">{r.No}</Data></Cell>" &
            $"<Cell><Data ss:Type=""String"">{XmlEsc(r.Waktu.ToString("dd-MM-yyyy HH:mm:ss"))}</Data></Cell>" &
            $"<Cell><Data ss:Type=""String"">{XmlEsc(r.Device)}</Data></Cell>" &
            $"<Cell><Data ss:Type=""String"">{XmlEsc(r.Barcode)}</Data></Cell>" &
            $"<Cell><Data ss:Type=""String"">{XmlEsc(r.Format)}</Data></Cell></Row>{vbCrLf}"
    End Function

    Private Shared Function BuildSpreadsheetMlFooter() As String
        Return "</Table></Worksheet></Workbook>" & vbCrLf
    End Function

    ' ================= GOOGLE SHEETS (via Apps Script webhook) =================

    Private Sub btnSheets_Click(sender As Object, e As EventArgs) Handles btnSheets.Click
        Dim url = InputBox("Tempel URL Web App Google Apps Script (berakhiran /exec):" & vbCrLf & vbCrLf &
                           "Lihat panduan di SHEETS_SETUP.md", "Google Sheets Webhook", _rtGoogleUrl)
        If String.IsNullOrWhiteSpace(url) Then Return
        url = url.Trim()
        If Not (url.StartsWith("https://script.google.com/", StringComparison.OrdinalIgnoreCase) AndAlso
                url.IndexOf("/exec", StringComparison.OrdinalIgnoreCase) >= 0) Then
            MessageBox.Show("URL tidak valid. Harus https://script.google.com/.../exec", "URL Salah", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        _rtGoogleUrl = url
        lblSheetsStatus.Text = "Sheets: ON"
        lblSheetsStatus.ForeColor = Color.FromArgb(52, 211, 153)
        lblLog.Text = "Google Sheets webhook AKTIF."
    End Sub

    Private Sub PostToGoogleSheets(waktu As DateTime, device As String, barcode As String, fmt As String)
        If String.IsNullOrEmpty(_rtGoogleUrl) Then Return
        Dim url = _rtGoogleUrl
        Dim payload = "{""waktu"":""" & waktu.ToString("dd-MM-yyyy HH:mm:ss").Replace("""", "\""") & """,""perangkat"":""" &
            device.Replace("""", "\""") & """,""barcode"":""" & barcode.Replace("""", "\""") & """,""format"":""" &
            fmt.Replace("""", "\""") & """,""key"":""" & _server.CurrentJoinKey & """}"
        Dim t As New Thread(Sub()
                                Try
                                    Dim req = CType(Net.WebRequest.Create(url), Net.HttpWebRequest)
                                    req.Method = "POST"
                                    req.ContentType = "application/json"
                                    req.Timeout = 8000
                                    Dim bytes = Encoding.UTF8.GetBytes(payload)
                                    Using rs = req.GetRequestStream()
                                        rs.Write(bytes, 0, bytes.Length)
                                    End Using
                                    Using resp = CType(req.GetResponse(), Net.HttpWebResponse)
                                    End Using
                                Catch
                                End Try
                            End Sub)
        t.IsBackground = True
        t.Start()
    End Sub

    ''' <summary>
    ''' Easter egg: Menekan tombol D W A P secara berurutan memunculkan messagebox "damian"
    ''' </summary>
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control OrElse e.Alt Then
            _easterEggBuffer = ""
            Return
        End If

        If e.KeyCode = Keys.ShiftKey OrElse e.KeyCode = Keys.LShiftKey OrElse e.KeyCode = Keys.RShiftKey Then
            Return
        End If

        Dim keyName As String = e.KeyCode.ToString().ToUpperInvariant()
        _easterEggBuffer &= keyName

        If _easterEggBuffer.Length > 20 Then
            _easterEggBuffer = _easterEggBuffer.Substring(_easterEggBuffer.Length - 10)
        End If

        If _easterEggBuffer.EndsWith("DWAP") Then
            _easterEggBuffer = ""
            e.SuppressKeyPress = True
            MessageBox.Show(Me, "damian")
        End If
    End Sub
End Class
