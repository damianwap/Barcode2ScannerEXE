Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms

Public Class NewSessionConfirmForm
    Inherits Form

    Private _activeDevices As List(Of ConnectedDeviceInfo)

    Public Sub New()
        Me.New(Nothing)
    End Sub

    Public Sub New(activeDevices As List(Of ConnectedDeviceInfo))
        InitializeComponent()
        _activeDevices = activeDevices
    End Sub

    Private Sub NewSessionConfirmForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Using icoStream = Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ScanKilat.app.ico")
                If icoStream IsNot Nothing Then Me.Icon = New Icon(icoStream)
            End Using
        Catch
        End Try

        Dim count = If(_activeDevices IsNot Nothing, _activeDevices.Count, 0)
        If count > 0 Then
            pnlWarningBox.BackColor = Color.FromArgb(45, 26, 10)
            lblWarningHeader.ForeColor = Color.FromArgb(251, 191, 36)
            lblWarningHeader.Text = $"⚠️ Perhatian: {count} HP sedang terhubung!"

            Dim names = _activeDevices.Select(Function(d) $"• {d.DeviceName} ({d.IP})").Take(3).ToList()
            If _activeDevices.Count > 3 Then
                names.Add($"• ... dan {_activeDevices.Count - 3} perangkat lainnya")
            End If
            lblWarningDetail.Text = "Koneksi perangkat berikut akan langsung terputus:" & vbCrLf & String.Join(vbCrLf, names)

            btnConfirm.BackColor = Color.FromArgb(220, 38, 38)
            btnConfirm.FlatAppearance.BorderColor = Color.FromArgb(239, 68, 68)
            btnConfirm.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 68, 68)
            btnConfirm.FlatAppearance.MouseDownBackColor = Color.FromArgb(185, 28, 28)
            btnConfirm.Text = "Tetap Buat Sesi"
        Else
            pnlWarningBox.BackColor = Color.FromArgb(30, 41, 59)
            lblWarningHeader.ForeColor = Color.FromArgb(56, 189, 248)
            lblWarningHeader.Text = "ℹ️ Tidak ada perangkat yang terhubung"
            lblWarningDetail.Text = "Tidak ada pemindai HP yang aktif. Sesi baru dapat dibuat dengan aman."

            btnConfirm.BackColor = Color.FromArgb(217, 119, 6)
            btnConfirm.FlatAppearance.BorderColor = Color.FromArgb(245, 158, 11)
            btnConfirm.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 158, 11)
            btnConfirm.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, 83, 9)
            btnConfirm.Text = "Buat Sesi Baru"
        End If
    End Sub

    Private Sub pnlWarningBox_Paint(sender As Object, e As PaintEventArgs) Handles pnlWarningBox.Paint
        Dim count = If(_activeDevices IsNot Nothing, _activeDevices.Count, 0)
        Dim borderColor = If(count > 0, Color.FromArgb(180, 83, 9), Color.FromArgb(51, 65, 85))
        ControlPaint.DrawBorder(e.Graphics, pnlWarningBox.ClientRectangle, borderColor, ButtonBorderStyle.Solid)
    End Sub

    Private Sub pnlConsequences_Paint(sender As Object, e As PaintEventArgs) Handles pnlConsequences.Paint
        ControlPaint.DrawBorder(e.Graphics, pnlConsequences.ClientRectangle, Color.FromArgb(51, 65, 85), ButtonBorderStyle.Solid)
    End Sub
End Class
