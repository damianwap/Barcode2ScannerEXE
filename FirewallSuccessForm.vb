Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class FirewallSuccessForm
    Inherits Form

    Private _port As Integer = 3443

    Public Sub New()
        Me.New(3443)
    End Sub

    Public Sub New(port As Integer)
        InitializeComponent()
        _port = port
    End Sub

    Private Sub FirewallSuccessForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Using icoStream = Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ScanKilat.app.ico")
                If icoStream IsNot Nothing Then Me.Icon = New Icon(icoStream)
            End Using
        Catch
        End Try

        lblStatusTitle.Text = $"✅ Port {_port} (TCP) Siap Menerima Koneksi HP"
        lblStatusDetail.Text = $"Aturan Firewall: ScanKilat Port {_port}" & vbCrLf &
                               "Protokol: TCP (Inbound) • Profil: Private, Domain" & vbCrLf &
                               "Cakupan: Subnet Lokal (Hanya perangkat di WiFi/LAN yang sama)"
    End Sub

    Private Sub pnlStatusBox_Paint(sender As Object, e As PaintEventArgs) Handles pnlStatusBox.Paint
        ControlPaint.DrawBorder(e.Graphics, pnlStatusBox.ClientRectangle, Color.FromArgb(16, 185, 129), ButtonBorderStyle.Solid)
    End Sub

    Private Sub pnlInfoBox_Paint(sender As Object, e As PaintEventArgs) Handles pnlInfoBox.Paint
        ControlPaint.DrawBorder(e.Graphics, pnlInfoBox.ClientRectangle, Color.FromArgb(51, 65, 85), ButtonBorderStyle.Solid)
    End Sub
End Class
