Imports System
Imports System.Diagnostics
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Public Class CustomDevForm
    Inherits Form

    Private Const ContactPhone As String = "+62 813-9118-2026"
    Private Const ContactWaLink As String = "https://wa.me/6282260276542?text=Halo%2C%20saya%20tertarik%20konsultasi%20layanan%20kustom%20atau%20integrasi%20ScanKilat."
    Private Const ContactEmail As String = "vicilaptopid@gmail.com"
    Private Const ContactEmailMailto As String = "mailto:vicilaptopid@gmail.com?subject=Konsultasi%20Layanan%20Kustom%20%26%20Integrasi%20ScanKilat"
    Private Const ContactWeb As String = "https://github.com/damianwap/Barcode2ScannerEXE"

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub CustomDevForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Using icoStream = Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ScanKilat.app.ico")
                If icoStream IsNot Nothing Then Me.Icon = New Icon(icoStream)
            End Using
        Catch
        End Try
    End Sub

    Private Sub pnlServices_Paint(sender As Object, e As PaintEventArgs) Handles pnlServices.Paint
        ControlPaint.DrawBorder(e.Graphics, pnlServices.ClientRectangle, Color.FromArgb(51, 65, 85), ButtonBorderStyle.Solid)
    End Sub

    Private Sub pnlContact_Paint(sender As Object, e As PaintEventArgs) Handles pnlContact.Paint
        ControlPaint.DrawBorder(e.Graphics, pnlContact.ClientRectangle, Color.FromArgb(51, 65, 85), ButtonBorderStyle.Solid)
    End Sub

    Private Sub lnkWa_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkWa.LinkClicked
        OpenUrl(ContactWaLink)
    End Sub

    Private Sub lnkEmail_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkEmail.LinkClicked
        OpenUrl(ContactEmailMailto)
    End Sub

    Private Sub lnkWeb_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        OpenUrl(ContactWeb)
    End Sub

    Private Sub btnCopyContact_Click(sender As Object, e As EventArgs) Handles btnCopyContact.Click
        Try
            Dim sb As New StringBuilder()
            sb.AppendLine("=== LAYANAN KUSTOM & INTEGRASI SOFTWARE ===")
            sb.AppendLine("Pengembang: ViciLaptop / ScanKilat")
            sb.AppendLine($"WhatsApp   : {ContactPhone} ({ContactWaLink})")
            sb.AppendLine($"Email      : {ContactEmail}")
            sb.AppendLine($"Portofolio : {ContactWeb}")
            sb.AppendLine()
            sb.AppendLine("Solusi yang Disediakan:")
            sb.AppendLine("1. Kustomisasi fitur ScanKilat (format barcode khusus, filter data, auto-type)")
            sb.AppendLine("2. Integrasi ke sistem yang sudah ada (POS/Kasir, ERP SAP/Accurate/Odoo, SQL Database, API)")
            sb.AppendLine("3. Pengembangan software baru (Aplikasi desktop Windows, web dashboard, mobile, inventaris)")
            sb.AppendLine("===========================================")

            Clipboard.SetText(sb.ToString())
            lblCopyStatus.Text = "✓ Kontak berhasil disalin ke clipboard!"

            Dim resetTimer As New Timer()
            resetTimer.Interval = 3500
            AddHandler resetTimer.Tick, Sub(tSender, tE)
                                            lblCopyStatus.Text = ""
                                            resetTimer.Stop()
                                            resetTimer.Dispose()
                                        End Sub
            resetTimer.Start()
        Catch ex As Exception
            MessageBox.Show("Gagal menyalin ke clipboard:" & vbCrLf & ex.Message, "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub OpenUrl(url As String)
        Try
            Dim psi As New ProcessStartInfo(url)
            psi.UseShellExecute = True
            Process.Start(psi)
        Catch ex As Exception
            Try
                Clipboard.SetText(url)
                lblCopyStatus.Text = "✓ Tautan disalin ke clipboard!"
            Catch
            End Try
            MessageBox.Show("Tidak dapat membuka browser secara otomatis. Tautan telah disalin:" & vbCrLf & url, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
End Class
