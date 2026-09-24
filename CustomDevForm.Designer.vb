<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomDevForm
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
    Friend WithEvents pnlServices As System.Windows.Forms.Panel
    Friend WithEvents lblServicesTitle As System.Windows.Forms.Label
    Friend WithEvents lblService1 As System.Windows.Forms.Label
    Friend WithEvents lblService2 As System.Windows.Forms.Label
    Friend WithEvents lblService3 As System.Windows.Forms.Label
    Friend WithEvents pnlContact As System.Windows.Forms.Panel
    Friend WithEvents lblContactTitle As System.Windows.Forms.Label
    Friend WithEvents lblWaLabel As System.Windows.Forms.Label
    Friend WithEvents lnkWa As System.Windows.Forms.LinkLabel
    Friend WithEvents lblEmailLabel As System.Windows.Forms.Label
    Friend WithEvents lnkEmail As System.Windows.Forms.LinkLabel
    Friend WithEvents lblContactNote As System.Windows.Forms.Label
    Friend WithEvents lblCopyStatus As System.Windows.Forms.Label
    Friend WithEvents btnCopyContact As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblIcon = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.pnlServices = New System.Windows.Forms.Panel()
        Me.lblServicesTitle = New System.Windows.Forms.Label()
        Me.lblService1 = New System.Windows.Forms.Label()
        Me.lblService2 = New System.Windows.Forms.Label()
        Me.lblService3 = New System.Windows.Forms.Label()
        Me.pnlContact = New System.Windows.Forms.Panel()
        Me.lblContactTitle = New System.Windows.Forms.Label()
        Me.lblWaLabel = New System.Windows.Forms.Label()
        Me.lnkWa = New System.Windows.Forms.LinkLabel()
        Me.lblEmailLabel = New System.Windows.Forms.Label()
        Me.lnkEmail = New System.Windows.Forms.LinkLabel()
        Me.lblContactNote = New System.Windows.Forms.Label()
        Me.lblCopyStatus = New System.Windows.Forms.Label()
        Me.btnCopyContact = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlServices.SuspendLayout()
        Me.pnlContact.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblIcon
        '
        Me.lblIcon.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIcon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.lblIcon.Location = New System.Drawing.Point(18, 14)
        Me.lblIcon.Name = "lblIcon"
        Me.lblIcon.Size = New System.Drawing.Size(42, 42)
        Me.lblIcon.TabIndex = 0
        Me.lblIcon.Text = "💼"
        Me.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(66, 16)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(297, 21)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "Layanan Custom && Integrasi Software"
        '
        'lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblSubtitle.Location = New System.Drawing.Point(68, 40)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(415, 15)
        Me.lblSubtitle.TabIndex = 2
        Me.lblSubtitle.Text = "Solusi kustomisasi fitur, integrasi kasir/ERP, dan pembuatan software custom."
        '
        'pnlServices
        '
        Me.pnlServices.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.pnlServices.Controls.Add(Me.lblServicesTitle)
        Me.pnlServices.Controls.Add(Me.lblService1)
        Me.pnlServices.Controls.Add(Me.lblService2)
        Me.pnlServices.Controls.Add(Me.lblService3)
        Me.pnlServices.Location = New System.Drawing.Point(20, 68)
        Me.pnlServices.Name = "pnlServices"
        Me.pnlServices.Padding = New System.Windows.Forms.Padding(12, 10, 12, 10)
        Me.pnlServices.Size = New System.Drawing.Size(480, 156)
        Me.pnlServices.TabIndex = 3
        '
        'lblServicesTitle
        '
        Me.lblServicesTitle.AutoSize = True
        Me.lblServicesTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServicesTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.lblServicesTitle.Location = New System.Drawing.Point(12, 8)
        Me.lblServicesTitle.Name = "lblServicesTitle"
        Me.lblServicesTitle.Size = New System.Drawing.Size(272, 15)
        Me.lblServicesTitle.TabIndex = 0
        Me.lblServicesTitle.Text = "✨ Layanan Pengembangan yang Kami Sediakan:"
        '
        'lblService1
        '
        Me.lblService1.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblService1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.lblService1.Location = New System.Drawing.Point(12, 30)
        Me.lblService1.Name = "lblService1"
        Me.lblService1.Size = New System.Drawing.Size(456, 36)
        Me.lblService1.TabIndex = 1
        Me.lblService1.Text = "• Fitur Custom ScanKilat: Format output khusus, validasi / regex barcode, filteri" &
    "ng duplikat, auto-type multi-kolom, atau custom webhook."
        '
        'lblService2
        '
        Me.lblService2.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblService2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.lblService2.Location = New System.Drawing.Point(12, 70)
        Me.lblService2.Name = "lblService2"
        Me.lblService2.Size = New System.Drawing.Size(456, 36)
        Me.lblService2.TabIndex = 2
        Me.lblService2.Text = "• Integrasi ke Sistem yang Sudah Ada: Hubungkan langsung ke aplikasi POS/Kasir, s" &
    "oftware ERP/WMS (SAP, Accurate, Odoo), database SQL, atau API toko."
        '
        'lblService3
        '
        Me.lblService3.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblService3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.lblService3.Location = New System.Drawing.Point(12, 110)
        Me.lblService3.Name = "lblService3"
        Me.lblService3.Size = New System.Drawing.Size(456, 36)
        Me.lblService3.TabIndex = 3
        Me.lblService3.Text = "• Pengembangan Software Lainnya: Pembuatan aplikasi kasir, sistem inventaris / st" &
    "ok opname gudang, dashboard web, atau desktop tools custom."
        '
        'pnlContact
        '
        Me.pnlContact.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(29, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.pnlContact.Controls.Add(Me.lblContactTitle)
        Me.pnlContact.Controls.Add(Me.lblWaLabel)
        Me.pnlContact.Controls.Add(Me.lnkWa)
        Me.pnlContact.Controls.Add(Me.lblEmailLabel)
        Me.pnlContact.Controls.Add(Me.lnkEmail)
        Me.pnlContact.Controls.Add(Me.lblContactNote)
        Me.pnlContact.Location = New System.Drawing.Point(20, 234)
        Me.pnlContact.Name = "pnlContact"
        Me.pnlContact.Padding = New System.Windows.Forms.Padding(12, 10, 12, 10)
        Me.pnlContact.Size = New System.Drawing.Size(480, 133)
        Me.pnlContact.TabIndex = 4
        '
        'lblContactTitle
        '
        Me.lblContactTitle.AutoSize = True
        Me.lblContactTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.lblContactTitle.Location = New System.Drawing.Point(12, 10)
        Me.lblContactTitle.Name = "lblContactTitle"
        Me.lblContactTitle.Size = New System.Drawing.Size(289, 15)
        Me.lblContactTitle.TabIndex = 0
        Me.lblContactTitle.Text = "📞 Hubungi Pengembang untuk Diskusi & Konsultasi:"
        '
        'lblWaLabel
        '
        Me.lblWaLabel.AutoSize = True
        Me.lblWaLabel.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWaLabel.ForeColor = System.Drawing.Color.White
        Me.lblWaLabel.Location = New System.Drawing.Point(12, 36)
        Me.lblWaLabel.Name = "lblWaLabel"
        Me.lblWaLabel.Size = New System.Drawing.Size(83, 15)
        Me.lblWaLabel.TabIndex = 1
        Me.lblWaLabel.Text = "💬 WhatsApp:"
        '
        'lnkWa
        '
        Me.lnkWa.AutoSize = True
        Me.lnkWa.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkWa.LinkColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.lnkWa.Location = New System.Drawing.Point(105, 36)
        Me.lnkWa.Name = "lnkWa"
        Me.lnkWa.Size = New System.Drawing.Size(256, 15)
        Me.lnkWa.TabIndex = 2
        Me.lnkWa.TabStop = True
        Me.lnkWa.Text = "+62 822-6027-6542 (Klik untuk Chat WhatsApp)"
        '
        'lblEmailLabel
        '
        Me.lblEmailLabel.AutoSize = True
        Me.lblEmailLabel.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmailLabel.ForeColor = System.Drawing.Color.White
        Me.lblEmailLabel.Location = New System.Drawing.Point(12, 62)
        Me.lblEmailLabel.Name = "lblEmailLabel"
        Me.lblEmailLabel.Size = New System.Drawing.Size(55, 15)
        Me.lblEmailLabel.TabIndex = 3
        Me.lblEmailLabel.Text = "✉️ Email:"
        '
        'lnkEmail
        '
        Me.lnkEmail.AutoSize = True
        Me.lnkEmail.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkEmail.LinkColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.lnkEmail.Location = New System.Drawing.Point(105, 62)
        Me.lnkEmail.Name = "lnkEmail"
        Me.lnkEmail.Size = New System.Drawing.Size(230, 15)
        Me.lnkEmail.TabIndex = 4
        Me.lnkEmail.TabStop = True
        Me.lnkEmail.Text = "vicilaptopid@gmail.com / Klik Kirim Email"
        '
        'lblContactNote
        '
        Me.lblContactNote.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContactNote.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblContactNote.Location = New System.Drawing.Point(12, 92)
        Me.lblContactNote.Name = "lblContactNote"
        Me.lblContactNote.Size = New System.Drawing.Size(456, 20)
        Me.lblContactNote.TabIndex = 7
        Me.lblContactNote.Text = "💡 Diskusi gratis seputar kebutuhan sistem, studi kelayakan, atau estimasi biaya " &
    "pengerjaan."
        '
        'lblCopyStatus
        '
        Me.lblCopyStatus.AutoSize = True
        Me.lblCopyStatus.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.lblCopyStatus.Location = New System.Drawing.Point(20, 405)
        Me.lblCopyStatus.Name = "lblCopyStatus"
        Me.lblCopyStatus.Size = New System.Drawing.Size(0, 15)
        Me.lblCopyStatus.TabIndex = 5
        '
        'btnCopyContact
        '
        Me.btnCopyContact.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCopyContact.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.btnCopyContact.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCopyContact.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.btnCopyContact.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(29, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.btnCopyContact.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnCopyContact.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopyContact.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopyContact.ForeColor = System.Drawing.Color.White
        Me.btnCopyContact.Location = New System.Drawing.Point(260, 396)
        Me.btnCopyContact.Name = "btnCopyContact"
        Me.btnCopyContact.Size = New System.Drawing.Size(134, 34)
        Me.btnCopyContact.TabIndex = 6
        Me.btnCopyContact.Text = "📋 Salin Info Kontak"
        Me.btnCopyContact.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(404, 396)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(96, 34)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Tutup"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'CustomDevForm
        '
        Me.AcceptButton = Me.btnClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(520, 446)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnCopyContact)
        Me.Controls.Add(Me.lblCopyStatus)
        Me.Controls.Add(Me.pnlContact)
        Me.Controls.Add(Me.pnlServices)
        Me.Controls.Add(Me.lblSubtitle)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblIcon)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CustomDevForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Layanan Kustom & Integrasi Software"
        Me.pnlServices.ResumeLayout(False)
        Me.pnlServices.PerformLayout()
        Me.pnlContact.ResumeLayout(False)
        Me.pnlContact.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
End Class
