Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class AboutForm
    Inherits Form

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub AboutForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim ver = If(Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString(), "1.0.0.0")
            lblVer.Text = "Versi " & ver
        Catch
        End Try

        Try
            Using icoStream = Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ScanKilat.app.ico")
                If icoStream IsNot Nothing Then Me.Icon = New Icon(icoStream)
            End Using
        Catch
        End Try

        Try
            Dim asm = Reflection.Assembly.GetExecutingAssembly()
            Using s = asm.GetManifestResourceStream("ScanKilat.public.logo.png")
                If s IsNot Nothing Then picLogo.Image = Image.FromStream(s)
            End Using
        Catch
        End Try
    End Sub

    Private Sub btnCustomDev_Click(sender As Object, e As EventArgs) Handles btnCustomDev.Click
        Using dlg As New CustomDevForm()
            dlg.ShowDialog(Me)
        End Using
    End Sub
End Class
