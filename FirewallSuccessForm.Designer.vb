<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FirewallSuccessForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    Friend WithEvents lblIcon As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblSubtitle As System.Windows.Forms.Label
    Friend WithEvents pnlStatusBox As System.Windows.Forms.Panel
    Friend WithEvents lblStatusTitle As System.Windows.Forms.Label
    Friend WithEvents lblStatusDetail As System.Windows.Forms.Label
    Friend WithEvents pnlInfoBox As System.Windows.Forms.Panel
    Friend WithEvents lblInfoTitle As System.Windows.Forms.Label
    Friend WithEvents lblInfoDetail As System.Windows.Forms.Label
    Friend WithEvents btnOk As System.Windows.Forms.Button

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblIcon = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.pnlStatusBox = New System.Windows.Forms.Panel()
        Me.lblStatusTitle = New System.Windows.Forms.Label()
        Me.lblStatusDetail = New System.Windows.Forms.Label()
        Me.pnlInfoBox = New System.Windows.Forms.Panel()
        Me.lblInfoTitle = New System.Windows.Forms.Label()
        Me.lblInfoDetail = New System.Windows.Forms.Label()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.pnlStatusBox.SuspendLayout()
        Me.pnlInfoBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblIcon
        '
        Me.lblIcon.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIcon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.lblIcon.Location = New System.Drawing.Point(18, 14)
        Me.lblIcon.Name = "lblIcon"
        Me.lblIcon.Size = New System.Drawing.Size(38, 38)
        Me.lblIcon.TabIndex = 0
        Me.lblIcon.Text = "🛡️"
        Me.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(60, 16)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(248, 21)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "Port Firewall Berhasil Diizinkan"
        '
        'lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblSubtitle.Location = New System.Drawing.Point(62, 40)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(356, 15)
        Me.lblSubtitle.TabIndex = 2
        Me.lblSubtitle.Text = "Aturan lalu lintas masuk (inbound) telah aktif di Windows Firewall."
        '
        'pnlStatusBox
        '
        Me.pnlStatusBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.pnlStatusBox.Controls.Add(Me.lblStatusTitle)
        Me.pnlStatusBox.Controls.Add(Me.lblStatusDetail)
        Me.pnlStatusBox.Location = New System.Drawing.Point(20, 68)
        Me.pnlStatusBox.Name = "pnlStatusBox"
        Me.pnlStatusBox.Padding = New System.Windows.Forms.Padding(12, 10, 12, 10)
        Me.pnlStatusBox.Size = New System.Drawing.Size(410, 96)
        Me.pnlStatusBox.TabIndex = 3
        '
        'lblStatusTitle
        '
        Me.lblStatusTitle.AutoSize = True
        Me.lblStatusTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.lblStatusTitle.Location = New System.Drawing.Point(12, 10)
        Me.lblStatusTitle.Name = "lblStatusTitle"
        Me.lblStatusTitle.Size = New System.Drawing.Size(262, 15)
        Me.lblStatusTitle.TabIndex = 0
        Me.lblStatusTitle.Text = "✅ Port 3443 (TCP) Siap Menerima Koneksi HP"
        '
        'lblStatusDetail
        '
        Me.lblStatusDetail.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.lblStatusDetail.Location = New System.Drawing.Point(12, 32)
        Me.lblStatusDetail.Name = "lblStatusDetail"
        Me.lblStatusDetail.Size = New System.Drawing.Size(386, 54)
        Me.lblStatusDetail.TabIndex = 1
        Me.lblStatusDetail.Text = "Aturan Firewall: ScanKilat Port 3443" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Protokol: TCP (Inbound) • Profil: Private, " &
    "Domain" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Cakupan: Subnet Lokal (Hanya perangkat di WiFi/LAN yang sama)"
        '
        'pnlInfoBox
        '
        Me.pnlInfoBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.pnlInfoBox.Controls.Add(Me.lblInfoTitle)
        Me.pnlInfoBox.Controls.Add(Me.lblInfoDetail)
        Me.pnlInfoBox.Location = New System.Drawing.Point(20, 174)
        Me.pnlInfoBox.Name = "pnlInfoBox"
        Me.pnlInfoBox.Padding = New System.Windows.Forms.Padding(12, 10, 12, 10)
        Me.pnlInfoBox.Size = New System.Drawing.Size(410, 90)
        Me.pnlInfoBox.TabIndex = 4
        '
        'lblInfoTitle
        '
        Me.lblInfoTitle.AutoSize = True
        Me.lblInfoTitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfoTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.lblInfoTitle.Location = New System.Drawing.Point(12, 8)
        Me.lblInfoTitle.Name = "lblInfoTitle"
        Me.lblInfoTitle.Size = New System.Drawing.Size(123, 15)
        Me.lblInfoTitle.TabIndex = 0
        Me.lblInfoTitle.Text = "Langkah Selanjutnya:"
        '
        'lblInfoDetail
        '
        Me.lblInfoDetail.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfoDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblInfoDetail.Location = New System.Drawing.Point(12, 28)
        Me.lblInfoDetail.Name = "lblInfoDetail"
        Me.lblInfoDetail.Size = New System.Drawing.Size(386, 52)
        Me.lblInfoDetail.TabIndex = 1
        Me.lblInfoDetail.Text = "• Pastikan HP dan laptop tersambung ke jaringan WiFi/LAN yang sama." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "• Scan QR Co" &
    "de dari HP untuk langsung membuka scanner kamera." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "• Hasil scan barcode akan oto" &
    "matis terketik di aplikasi kasir/Excel."
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.btnOk.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOk.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Location = New System.Drawing.Point(310, 278)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(120, 36)
        Me.btnOk.TabIndex = 5
        Me.btnOk.Text = "Mengerti"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'FirewallSuccessForm
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.CancelButton = Me.btnOk
        Me.ClientSize = New System.Drawing.Size(450, 328)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.pnlInfoBox)
        Me.Controls.Add(Me.pnlStatusBox)
        Me.Controls.Add(Me.lblSubtitle)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblIcon)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FirewallSuccessForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Firewall Diizinkan"
        Me.pnlStatusBox.ResumeLayout(False)
        Me.pnlStatusBox.PerformLayout()
        Me.pnlInfoBox.ResumeLayout(False)
        Me.pnlInfoBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
End Class
