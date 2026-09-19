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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        cmbSuffix.SelectedIndex = 0

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

            lblLog.Text = "Server HTTPS & WebSocket aktif pada port 3443"
        Catch ex As Exception
            MessageBox.Show("Gagal memulai server internal:" & vbCrLf & ex.Message, "Error Server", MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblServerStatus.Text = "● Server Gagal"
            lblServerStatus.ForeColor = Color.FromArgb(239, 68, 68)
        End Try
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            _server?.StopServer()
        Catch
        End Try
    End Sub

    Private Sub UpdateQRCode()
        If _server Is Nothing Then Return

        Dim selectedInfo As NetworkIPInfo = TryCast(cmbNetworkIP.SelectedItem, NetworkIPInfo)
        Dim ip = If(selectedInfo IsNot Nothing, selectedInfo.IP, ScannerServer.GetLocalIPAddress())
        _currentUrl = $"https://{ip}:3443/scan.html?session={_server.CurrentSessionCode}&key={_server.CurrentJoinKey}"

        lblSessionCode.Text = $"KODE SESI: {_server.CurrentSessionCode}"
        lblServerStatus.Text = $"● Server: https://{ip}:3443"
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

    Private Sub UpdatePhoneStatusUI()
        Dim devices = If(_server IsNot Nothing, _server.ActiveDevices, New List(Of ConnectedDeviceInfo)())
        Dim count = devices.Count

        lblDevicesBarTitle.Text = $"📱 HP Terhubung ({count}):"
        flpDevices.Controls.Clear()

        If count = 0 Then
            lblPhoneStatus.Text = "● HP Belum Tersambung"
            lblPhoneStatus.ForeColor = Color.FromArgb(245, 158, 11)
            lblPhoneStatus.BackColor = Color.FromArgb(35, 30, 20)
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
                badge.ForeColor = Color.FromArgb(52, 211, 153)
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

        ' Sanitasi teks: hapus karakter kontrol berbahaya (seperti \0, \b, \r, \n, \x1b)
        ' Batasi panjang maksimal 500 karakter untuk mencegah flooding / injection serangan keystroke
        Dim safeChars As New List(Of Char)()
        For Each c In text
            If Not Char.IsControl(c) OrElse c = vbTab Then
                safeChars.Add(c)
                If safeChars.Count >= 500 Then Exit For
            End If
        Next

        If safeChars.Count = 0 Then Return

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
        UpdateQRCode()
        lblLog.Text = "Sesi baru berhasil dibuat. Silakan scan ulang dari HP."
    End Sub

    Private Sub cmbNetworkIP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNetworkIP.SelectedIndexChanged
        UpdateQRCode()
    End Sub

    Private Sub btnFirewall_Click(sender As Object, e As EventArgs) Handles btnFirewall.Click
        Try
            Dim psi As New ProcessStartInfo()
            psi.FileName = "netsh"
            psi.Arguments = "advfirewall firewall add rule name=""Barcode2Scanner Port 3443"" dir=in action=allow protocol=TCP localport=3443 profile=private,domain"
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
                MessageBox.Show("Link koneksi HP berhasil disalin ke clipboard:" & vbCrLf & _currentUrl, "Link Disalin", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
