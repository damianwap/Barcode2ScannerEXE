<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblSubtitle As System.Windows.Forms.Label
    Friend WithEvents lblServerStatus As System.Windows.Forms.Label
    Friend WithEvents lblPhoneStatus As System.Windows.Forms.Label

    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents cardQR As System.Windows.Forms.Panel
    Friend WithEvents lblQRTitle As System.Windows.Forms.Label
    Friend WithEvents lblIp As System.Windows.Forms.Label
    Friend WithEvents cmbNetworkIP As System.Windows.Forms.ComboBox
    Friend WithEvents lblSessionCode As System.Windows.Forms.Label
    Friend WithEvents btnToggleQR As System.Windows.Forms.Button
    Friend WithEvents picQRCode As System.Windows.Forms.PictureBox
    Friend WithEvents lnkUrl As System.Windows.Forms.LinkLabel
    Friend WithEvents btnNewSession As System.Windows.Forms.Button
    Friend WithEvents btnFirewall As System.Windows.Forms.Button

    Friend WithEvents cardAutoType As System.Windows.Forms.Panel
    Friend WithEvents lblAutoTypeTitle As System.Windows.Forms.Label
    Friend WithEvents chkAutoType As System.Windows.Forms.CheckBox
    Friend WithEvents chkBeep As System.Windows.Forms.CheckBox
    Friend WithEvents lblSuffix As System.Windows.Forms.Label
    Friend WithEvents cmbSuffix As System.Windows.Forms.ComboBox
    Friend WithEvents lblTip As System.Windows.Forms.Label
    Friend WithEvents btnAbout As System.Windows.Forms.Button
    Friend WithEvents btnCustomDev As System.Windows.Forms.Button

    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents pnlStats As System.Windows.Forms.Panel
    Friend WithEvents cardTotal As System.Windows.Forms.Panel
    Friend WithEvents lblTotalTitle As System.Windows.Forms.Label
    Friend WithEvents lblTotalVal As System.Windows.Forms.Label
    Friend WithEvents cardLast As System.Windows.Forms.Panel
    Friend WithEvents lblLastTitle As System.Windows.Forms.Label
    Friend WithEvents lblLastVal As System.Windows.Forms.Label
    Friend WithEvents cardDevice As System.Windows.Forms.Panel
    Friend WithEvents lblDeviceTitle As System.Windows.Forms.Label
    Friend WithEvents lblDeviceVal As System.Windows.Forms.Label

    Friend WithEvents pnlDevicesBar As System.Windows.Forms.Panel
    Friend WithEvents lblDevicesBarTitle As System.Windows.Forms.Label
    Friend WithEvents flpDevices As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents toolTipDevices As System.Windows.Forms.ToolTip

    Friend WithEvents dgvScan As System.Windows.Forms.DataGridView
    Friend WithEvents colNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDevice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBarcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFormat As System.Windows.Forms.DataGridViewTextBoxColumn

    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents btnExportCsv As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnRealTime As System.Windows.Forms.Button
    Friend WithEvents btnSheets As System.Windows.Forms.Button
    Friend WithEvents lblOutputPath As System.Windows.Forms.Label
    Friend WithEvents lblSheetsStatus As System.Windows.Forms.Label
    Friend WithEvents lblLog As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.lblServerStatus = New System.Windows.Forms.Label()
        Me.lblPhoneStatus = New System.Windows.Forms.Label()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.cardAutoType = New System.Windows.Forms.Panel()
        Me.lblAutoTypeTitle = New System.Windows.Forms.Label()
        Me.chkAutoType = New System.Windows.Forms.CheckBox()
        Me.chkBeep = New System.Windows.Forms.CheckBox()
        Me.lblSuffix = New System.Windows.Forms.Label()
        Me.cmbSuffix = New System.Windows.Forms.ComboBox()
        Me.lblTip = New System.Windows.Forms.Label()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.btnCustomDev = New System.Windows.Forms.Button()
        Me.cardQR = New System.Windows.Forms.Panel()
        Me.lblQRTitle = New System.Windows.Forms.Label()
        Me.lblIp = New System.Windows.Forms.Label()
        Me.cmbNetworkIP = New System.Windows.Forms.ComboBox()
        Me.lblSessionCode = New System.Windows.Forms.Label()
        Me.btnToggleQR = New System.Windows.Forms.Button()
        Me.btnNewSession = New System.Windows.Forms.Button()
        Me.btnFirewall = New System.Windows.Forms.Button()
        Me.picQRCode = New System.Windows.Forms.PictureBox()
        Me.lnkUrl = New System.Windows.Forms.LinkLabel()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.dgvScan = New System.Windows.Forms.DataGridView()
        Me.colNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDevice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBarcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFormat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlDevicesBar = New System.Windows.Forms.Panel()
        Me.flpDevices = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblDevicesBarTitle = New System.Windows.Forms.Label()
        Me.pnlStats = New System.Windows.Forms.Panel()
        Me.cardTotal = New System.Windows.Forms.Panel()
        Me.lblTotalVal = New System.Windows.Forms.Label()
        Me.lblTotalTitle = New System.Windows.Forms.Label()
        Me.cardLast = New System.Windows.Forms.Panel()
        Me.lblLastVal = New System.Windows.Forms.Label()
        Me.lblLastTitle = New System.Windows.Forms.Label()
        Me.cardDevice = New System.Windows.Forms.Panel()
        Me.lblDeviceVal = New System.Windows.Forms.Label()
        Me.lblDeviceTitle = New System.Windows.Forms.Label()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.btnExportCsv = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRealTime = New System.Windows.Forms.Button()
        Me.btnSheets = New System.Windows.Forms.Button()
        Me.lblOutputPath = New System.Windows.Forms.Label()
        Me.lblSheetsStatus = New System.Windows.Forms.Label()
        Me.lblLog = New System.Windows.Forms.Label()
        Me.toolTipDevices = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.cardAutoType.SuspendLayout()
        Me.cardQR.SuspendLayout()
        CType(Me.picQRCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRight.SuspendLayout()
        CType(Me.dgvScan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDevicesBar.SuspendLayout()
        Me.pnlStats.SuspendLayout()
        Me.cardTotal.SuspendLayout()
        Me.cardLast.SuspendLayout()
        Me.cardDevice.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(13, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.lblSubtitle)
        Me.pnlHeader.Controls.Add(Me.lblServerStatus)
        Me.pnlHeader.Controls.Add(Me.lblPhoneStatus)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1020, 64)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(16, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(139, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "⚡ SCANKILAT"
        '
        'lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblSubtitle.Location = New System.Drawing.Point(17, 36)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(403, 15)
        Me.lblSubtitle.TabIndex = 1
        Me.lblSubtitle.Text = "Ubah Kamera Smartphone Menjadi Alat Scanner Fisik. Tanpa Install Apapun" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'lblServerStatus
        '
        Me.lblServerStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblServerStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.lblServerStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblServerStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.lblServerStatus.Location = New System.Drawing.Point(532, 16)
        Me.lblServerStatus.Name = "lblServerStatus"
        Me.lblServerStatus.Padding = New System.Windows.Forms.Padding(8, 4, 8, 4)
        Me.lblServerStatus.Size = New System.Drawing.Size(238, 32)
        Me.lblServerStatus.TabIndex = 2
        Me.lblServerStatus.Text = "● Server: Memulai..."
        Me.lblServerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPhoneStatus
        '
        Me.lblPhoneStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPhoneStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.lblPhoneStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPhoneStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhoneStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.lblPhoneStatus.Location = New System.Drawing.Point(778, 16)
        Me.lblPhoneStatus.Name = "lblPhoneStatus"
        Me.lblPhoneStatus.Padding = New System.Windows.Forms.Padding(8, 4, 8, 4)
        Me.lblPhoneStatus.Size = New System.Drawing.Size(227, 32)
        Me.lblPhoneStatus.TabIndex = 3
        Me.lblPhoneStatus.Text = "● HP Belum Tersambung"
        Me.lblPhoneStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.pnlLeft.Controls.Add(Me.cardAutoType)
        Me.pnlLeft.Controls.Add(Me.cardQR)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 64)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Padding = New System.Windows.Forms.Padding(12)
        Me.pnlLeft.Size = New System.Drawing.Size(340, 596)
        Me.pnlLeft.TabIndex = 1
        '
        'cardAutoType
        '
        Me.cardAutoType.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.cardAutoType.Controls.Add(Me.lblAutoTypeTitle)
        Me.cardAutoType.Controls.Add(Me.chkAutoType)
        Me.cardAutoType.Controls.Add(Me.chkBeep)
        Me.cardAutoType.Controls.Add(Me.lblSuffix)
        Me.cardAutoType.Controls.Add(Me.cmbSuffix)
        Me.cardAutoType.Controls.Add(Me.lblTip)
        Me.cardAutoType.Controls.Add(Me.btnAbout)
        Me.cardAutoType.Controls.Add(Me.btnCustomDev)
        Me.cardAutoType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cardAutoType.Location = New System.Drawing.Point(12, 244)
        Me.cardAutoType.Name = "cardAutoType"
        Me.cardAutoType.Padding = New System.Windows.Forms.Padding(12)
        Me.cardAutoType.Size = New System.Drawing.Size(316, 340)
        Me.cardAutoType.TabIndex = 1
        '
        'lblAutoTypeTitle
        '
        Me.lblAutoTypeTitle.AutoSize = True
        Me.lblAutoTypeTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoTypeTitle.ForeColor = System.Drawing.Color.White
        Me.lblAutoTypeTitle.Location = New System.Drawing.Point(12, 10)
        Me.lblAutoTypeTitle.Name = "lblAutoTypeTitle"
        Me.lblAutoTypeTitle.Size = New System.Drawing.Size(189, 19)
        Me.lblAutoTypeTitle.TabIndex = 0
        Me.lblAutoTypeTitle.Text = "Pengaturan Ketik Otomatis"
        '
        'chkAutoType
        '
        Me.chkAutoType.AutoSize = True
        Me.chkAutoType.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkAutoType.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.chkAutoType.Location = New System.Drawing.Point(14, 36)
        Me.chkAutoType.Name = "chkAutoType"
        Me.chkAutoType.Size = New System.Drawing.Size(270, 19)
        Me.chkAutoType.TabIndex = 1
        Me.chkAutoType.Text = "Ketik ke Aplikasi Aktif (Notepad/Excel/POS)"
        Me.chkAutoType.UseVisualStyleBackColor = True
        '
        'chkBeep
        '
        Me.chkBeep.AutoSize = True
        Me.chkBeep.Checked = True
        Me.chkBeep.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBeep.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkBeep.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBeep.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.chkBeep.Location = New System.Drawing.Point(14, 60)
        Me.chkBeep.Name = "chkBeep"
        Me.chkBeep.Size = New System.Drawing.Size(190, 19)
        Me.chkBeep.TabIndex = 2
        Me.chkBeep.Text = "Bunyi Beep di Laptop saat Scan"
        Me.chkBeep.UseVisualStyleBackColor = True
        '
        'lblSuffix
        '
        Me.lblSuffix.AutoSize = True
        Me.lblSuffix.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuffix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblSuffix.Location = New System.Drawing.Point(12, 88)
        Me.lblSuffix.Name = "lblSuffix"
        Me.lblSuffix.Size = New System.Drawing.Size(122, 15)
        Me.lblSuffix.TabIndex = 3
        Me.lblSuffix.Text = "Tombol Akhir (Suffix):"
        '
        'cmbSuffix
        '
        Me.cmbSuffix.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.cmbSuffix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSuffix.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSuffix.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSuffix.ForeColor = System.Drawing.Color.White
        Me.cmbSuffix.FormattingEnabled = True
        Me.cmbSuffix.Items.AddRange(New Object() {"Enter (Baris Baru)", "Tab (Kolom Baru)", "Tanpa Akhiran"})
        Me.cmbSuffix.Location = New System.Drawing.Point(130, 84)
        Me.cmbSuffix.Name = "cmbSuffix"
        Me.cmbSuffix.Size = New System.Drawing.Size(162, 23)
        Me.cmbSuffix.TabIndex = 4
        '
        'lblTip
        '
        Me.lblTip.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblTip.Location = New System.Drawing.Point(12, 114)
        Me.lblTip.Name = "lblTip"
        Me.lblTip.Size = New System.Drawing.Size(292, 68)
        Me.lblTip.TabIndex = 5
        Me.lblTip.Text = "💡 Cara Pakai: Buka program kasir, Excel, atau Notepad, dan arahkan kursor ke san" &
    "a. Hasil scan barcode HP akan langsung terketik otomatis seperti scanner fisik U" &
    "SB!"
        '
        'btnAbout
        '
        Me.btnAbout.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnAbout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAbout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnAbout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.btnAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAbout.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAbout.ForeColor = System.Drawing.Color.White
        Me.btnAbout.Location = New System.Drawing.Point(12, 188)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(292, 30)
        Me.btnAbout.TabIndex = 6
        Me.btnAbout.Text = "ℹ Tentang Aplikasi"
        Me.btnAbout.UseVisualStyleBackColor = False
        '
        'btnCustomDev
        '
        Me.btnCustomDev.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.btnCustomDev.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCustomDev.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.btnCustomDev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(29, Byte), Integer), CType(CType(78, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.btnCustomDev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnCustomDev.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomDev.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomDev.ForeColor = System.Drawing.Color.White
        Me.btnCustomDev.Location = New System.Drawing.Point(12, 224)
        Me.btnCustomDev.Name = "btnCustomDev"
        Me.btnCustomDev.Size = New System.Drawing.Size(292, 32)
        Me.btnCustomDev.TabIndex = 7
        Me.btnCustomDev.Text = "💼 Layanan Custom & Integrasi"
        Me.btnCustomDev.UseVisualStyleBackColor = False
        '
        'cardQR
        '
        Me.cardQR.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.cardQR.Controls.Add(Me.lblQRTitle)
        Me.cardQR.Controls.Add(Me.lblIp)
        Me.cardQR.Controls.Add(Me.cmbNetworkIP)
        Me.cardQR.Controls.Add(Me.lblSessionCode)
        Me.cardQR.Controls.Add(Me.btnToggleQR)
        Me.cardQR.Controls.Add(Me.btnNewSession)
        Me.cardQR.Controls.Add(Me.btnFirewall)
        Me.cardQR.Controls.Add(Me.picQRCode)
        Me.cardQR.Controls.Add(Me.lnkUrl)
        Me.cardQR.Dock = System.Windows.Forms.DockStyle.Top
        Me.cardQR.Location = New System.Drawing.Point(12, 12)
        Me.cardQR.Name = "cardQR"
        Me.cardQR.Padding = New System.Windows.Forms.Padding(12)
        Me.cardQR.Size = New System.Drawing.Size(316, 190)
        Me.cardQR.TabIndex = 0
        '
        'lblQRTitle
        '
        Me.lblQRTitle.AutoSize = True
        Me.lblQRTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQRTitle.ForeColor = System.Drawing.Color.White
        Me.lblQRTitle.Location = New System.Drawing.Point(12, 10)
        Me.lblQRTitle.Name = "lblQRTitle"
        Me.lblQRTitle.Size = New System.Drawing.Size(179, 19)
        Me.lblQRTitle.TabIndex = 0
        Me.lblQRTitle.Text = "Hubungkan HP (Scan QR)"
        '
        'lblIp
        '
        Me.lblIp.AutoSize = True
        Me.lblIp.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblIp.Location = New System.Drawing.Point(12, 32)
        Me.lblIp.Name = "lblIp"
        Me.lblIp.Size = New System.Drawing.Size(115, 15)
        Me.lblIp.TabIndex = 1
        Me.lblIp.Text = "Jaringan / IP Laptop:"
        '
        'cmbNetworkIP
        '
        Me.cmbNetworkIP.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.cmbNetworkIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNetworkIP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbNetworkIP.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNetworkIP.ForeColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.cmbNetworkIP.FormattingEnabled = True
        Me.cmbNetworkIP.Location = New System.Drawing.Point(12, 49)
        Me.cmbNetworkIP.Name = "cmbNetworkIP"
        Me.cmbNetworkIP.Size = New System.Drawing.Size(292, 21)
        Me.cmbNetworkIP.TabIndex = 2
        '
        'lblSessionCode
        '
        Me.lblSessionCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblSessionCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSessionCode.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSessionCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.lblSessionCode.Location = New System.Drawing.Point(12, 76)
        Me.lblSessionCode.Name = "lblSessionCode"
        Me.lblSessionCode.Size = New System.Drawing.Size(292, 28)
        Me.lblSessionCode.TabIndex = 3
        Me.lblSessionCode.Text = "KODE SESI: ••••••"
        Me.lblSessionCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnToggleQR
        '
        Me.btnToggleQR.BackColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.btnToggleQR.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnToggleQR.FlatAppearance.BorderSize = 0
        Me.btnToggleQR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.btnToggleQR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.btnToggleQR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnToggleQR.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnToggleQR.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.btnToggleQR.Location = New System.Drawing.Point(12, 108)
        Me.btnToggleQR.Name = "btnToggleQR"
        Me.btnToggleQR.Size = New System.Drawing.Size(292, 34)
        Me.btnToggleQR.TabIndex = 4
        Me.btnToggleQR.Text = "👁 Tampilkan QR untuk Scan"
        Me.btnToggleQR.UseVisualStyleBackColor = False
        '
        'btnNewSession
        '
        Me.btnNewSession.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnNewSession.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNewSession.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnNewSession.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.btnNewSession.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnNewSession.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewSession.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewSession.ForeColor = System.Drawing.Color.White
        Me.btnNewSession.Location = New System.Drawing.Point(12, 148)
        Me.btnNewSession.Name = "btnNewSession"
        Me.btnNewSession.Size = New System.Drawing.Size(142, 30)
        Me.btnNewSession.TabIndex = 5
        Me.btnNewSession.Text = "🔄 Sesi Baru"
        Me.btnNewSession.UseVisualStyleBackColor = False
        '
        'btnFirewall
        '
        Me.btnFirewall.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnFirewall.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnFirewall.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnFirewall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.btnFirewall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnFirewall.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFirewall.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFirewall.ForeColor = System.Drawing.Color.White
        Me.btnFirewall.Location = New System.Drawing.Point(162, 148)
        Me.btnFirewall.Name = "btnFirewall"
        Me.btnFirewall.Size = New System.Drawing.Size(142, 30)
        Me.btnFirewall.TabIndex = 6
        Me.btnFirewall.Text = "🛡 Buka Firewall"
        Me.btnFirewall.UseVisualStyleBackColor = False
        '
        'picQRCode
        '
        Me.picQRCode.BackColor = System.Drawing.Color.White
        Me.picQRCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picQRCode.Location = New System.Drawing.Point(80, 186)
        Me.picQRCode.Name = "picQRCode"
        Me.picQRCode.Size = New System.Drawing.Size(156, 156)
        Me.picQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picQRCode.TabIndex = 7
        Me.picQRCode.TabStop = False
        Me.picQRCode.Visible = False
        '
        'lnkUrl
        '
        Me.lnkUrl.ActiveLinkColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.lnkUrl.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkUrl.LinkColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.lnkUrl.Location = New System.Drawing.Point(12, 344)
        Me.lnkUrl.Name = "lnkUrl"
        Me.lnkUrl.Size = New System.Drawing.Size(292, 14)
        Me.lnkUrl.TabIndex = 8
        Me.lnkUrl.TabStop = True
        Me.lnkUrl.Text = "Salin URL Manual ke Clipboard"
        Me.lnkUrl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lnkUrl.Visible = False
        '
        'pnlRight
        '
        Me.pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.pnlRight.Controls.Add(Me.dgvScan)
        Me.pnlRight.Controls.Add(Me.pnlDevicesBar)
        Me.pnlRight.Controls.Add(Me.pnlStats)
        Me.pnlRight.Controls.Add(Me.pnlBottom)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRight.Location = New System.Drawing.Point(340, 64)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Padding = New System.Windows.Forms.Padding(0, 12, 12, 12)
        Me.pnlRight.Size = New System.Drawing.Size(680, 596)
        Me.pnlRight.TabIndex = 2
        '
        'dgvScan
        '
        Me.dgvScan.AllowUserToAddRows = False
        Me.dgvScan.AllowUserToDeleteRows = False
        Me.dgvScan.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(52, Byte), Integer))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.dgvScan.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvScan.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.dgvScan.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvScan.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvScan.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(13, Byte), Integer), CType(CType(22, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(13, Byte), Integer), CType(CType(22, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvScan.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvScan.ColumnHeadersHeight = 38
        Me.dgvScan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvScan.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNo, Me.colTime, Me.colDevice, Me.colBarcode, Me.colFormat})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle4.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(53, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvScan.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgvScan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvScan.EnableHeadersVisualStyles = False
        Me.dgvScan.GridColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.dgvScan.Location = New System.Drawing.Point(0, 124)
        Me.dgvScan.MultiSelect = False
        Me.dgvScan.Name = "dgvScan"
        Me.dgvScan.ReadOnly = True
        Me.dgvScan.RowHeadersVisible = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.dgvScan.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvScan.RowTemplate.Height = 34
        Me.dgvScan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvScan.Size = New System.Drawing.Size(668, 376)
        Me.dgvScan.TabIndex = 2
        '
        'colNo
        '
        Me.colNo.HeaderText = "#"
        Me.colNo.Name = "colNo"
        Me.colNo.ReadOnly = True
        Me.colNo.Width = 48
        '
        'colTime
        '
        Me.colTime.HeaderText = "Waktu"
        Me.colTime.Name = "colTime"
        Me.colTime.ReadOnly = True
        Me.colTime.Width = 140
        '
        'colDevice
        '
        Me.colDevice.HeaderText = "Perangkat HP"
        Me.colDevice.Name = "colDevice"
        Me.colDevice.ReadOnly = True
        Me.colDevice.Width = 140
        '
        'colBarcode
        '
        Me.colBarcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Consolas", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colBarcode.DefaultCellStyle = DataGridViewCellStyle3
        Me.colBarcode.HeaderText = "Hasil Barcode / Teks"
        Me.colBarcode.Name = "colBarcode"
        Me.colBarcode.ReadOnly = True
        '
        'colFormat
        '
        Me.colFormat.HeaderText = "Format"
        Me.colFormat.Name = "colFormat"
        Me.colFormat.ReadOnly = True
        Me.colFormat.Width = 90
        '
        'pnlDevicesBar
        '
        Me.pnlDevicesBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.pnlDevicesBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDevicesBar.Controls.Add(Me.flpDevices)
        Me.pnlDevicesBar.Controls.Add(Me.lblDevicesBarTitle)
        Me.pnlDevicesBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDevicesBar.Location = New System.Drawing.Point(0, 82)
        Me.pnlDevicesBar.Name = "pnlDevicesBar"
        Me.pnlDevicesBar.Padding = New System.Windows.Forms.Padding(8, 4, 8, 4)
        Me.pnlDevicesBar.Size = New System.Drawing.Size(668, 42)
        Me.pnlDevicesBar.TabIndex = 1
        '
        'flpDevices
        '
        Me.flpDevices.AutoScroll = True
        Me.flpDevices.BackColor = System.Drawing.Color.Transparent
        Me.flpDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpDevices.Location = New System.Drawing.Point(146, 4)
        Me.flpDevices.Name = "flpDevices"
        Me.flpDevices.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
        Me.flpDevices.Size = New System.Drawing.Size(512, 32)
        Me.flpDevices.TabIndex = 1
        Me.flpDevices.WrapContents = False
        '
        'lblDevicesBarTitle
        '
        Me.lblDevicesBarTitle.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblDevicesBarTitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDevicesBarTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.lblDevicesBarTitle.Location = New System.Drawing.Point(8, 4)
        Me.lblDevicesBarTitle.Name = "lblDevicesBarTitle"
        Me.lblDevicesBarTitle.Size = New System.Drawing.Size(138, 32)
        Me.lblDevicesBarTitle.TabIndex = 0
        Me.lblDevicesBarTitle.Text = "📱 HP Terhubung (0):"
        Me.lblDevicesBarTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlStats
        '
        Me.pnlStats.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.pnlStats.Controls.Add(Me.cardTotal)
        Me.pnlStats.Controls.Add(Me.cardLast)
        Me.pnlStats.Controls.Add(Me.cardDevice)
        Me.pnlStats.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlStats.Location = New System.Drawing.Point(0, 12)
        Me.pnlStats.Name = "pnlStats"
        Me.pnlStats.Size = New System.Drawing.Size(668, 70)
        Me.pnlStats.TabIndex = 0
        '
        'cardTotal
        '
        Me.cardTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.cardTotal.Controls.Add(Me.lblTotalVal)
        Me.cardTotal.Controls.Add(Me.lblTotalTitle)
        Me.cardTotal.Location = New System.Drawing.Point(0, 0)
        Me.cardTotal.Name = "cardTotal"
        Me.cardTotal.Size = New System.Drawing.Size(160, 60)
        Me.cardTotal.TabIndex = 0
        '
        'lblTotalVal
        '
        Me.lblTotalVal.AutoSize = True
        Me.lblTotalVal.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.lblTotalVal.Location = New System.Drawing.Point(8, 22)
        Me.lblTotalVal.Name = "lblTotalVal"
        Me.lblTotalVal.Size = New System.Drawing.Size(26, 30)
        Me.lblTotalVal.TabIndex = 1
        Me.lblTotalVal.Text = "0"
        '
        'lblTotalTitle
        '
        Me.lblTotalTitle.AutoSize = True
        Me.lblTotalTitle.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblTotalTitle.Location = New System.Drawing.Point(10, 7)
        Me.lblTotalTitle.Name = "lblTotalTitle"
        Me.lblTotalTitle.Size = New System.Drawing.Size(73, 13)
        Me.lblTotalTitle.TabIndex = 0
        Me.lblTotalTitle.Text = "TOTAL SCAN"
        '
        'cardLast
        '
        Me.cardLast.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.cardLast.Controls.Add(Me.lblLastVal)
        Me.cardLast.Controls.Add(Me.lblLastTitle)
        Me.cardLast.Location = New System.Drawing.Point(170, 0)
        Me.cardLast.Name = "cardLast"
        Me.cardLast.Size = New System.Drawing.Size(264, 60)
        Me.cardLast.TabIndex = 1
        '
        'lblLastVal
        '
        Me.lblLastVal.AutoEllipsis = True
        Me.lblLastVal.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.lblLastVal.Location = New System.Drawing.Point(8, 26)
        Me.lblLastVal.Name = "lblLastVal"
        Me.lblLastVal.Size = New System.Drawing.Size(248, 24)
        Me.lblLastVal.TabIndex = 1
        Me.lblLastVal.Text = "-"
        '
        'lblLastTitle
        '
        Me.lblLastTitle.AutoSize = True
        Me.lblLastTitle.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblLastTitle.Location = New System.Drawing.Point(10, 7)
        Me.lblLastTitle.Name = "lblLastTitle"
        Me.lblLastTitle.Size = New System.Drawing.Size(113, 13)
        Me.lblLastTitle.TabIndex = 0
        Me.lblLastTitle.Text = "BARCODE TERAKHIR"
        '
        'cardDevice
        '
        Me.cardDevice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cardDevice.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.cardDevice.Controls.Add(Me.lblDeviceVal)
        Me.cardDevice.Controls.Add(Me.lblDeviceTitle)
        Me.cardDevice.Location = New System.Drawing.Point(444, 0)
        Me.cardDevice.Name = "cardDevice"
        Me.cardDevice.Size = New System.Drawing.Size(224, 60)
        Me.cardDevice.TabIndex = 2
        '
        'lblDeviceVal
        '
        Me.lblDeviceVal.AutoEllipsis = True
        Me.lblDeviceVal.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeviceVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.lblDeviceVal.Location = New System.Drawing.Point(8, 26)
        Me.lblDeviceVal.Name = "lblDeviceVal"
        Me.lblDeviceVal.Size = New System.Drawing.Size(208, 24)
        Me.lblDeviceVal.TabIndex = 1
        Me.lblDeviceVal.Text = "-"
        '
        'lblDeviceTitle
        '
        Me.lblDeviceTitle.AutoSize = True
        Me.lblDeviceTitle.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeviceTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblDeviceTitle.Location = New System.Drawing.Point(10, 7)
        Me.lblDeviceTitle.Name = "lblDeviceTitle"
        Me.lblDeviceTitle.Size = New System.Drawing.Size(105, 13)
        Me.lblDeviceTitle.TabIndex = 0
        Me.lblDeviceTitle.Text = "PERANGKAT AKTIF"
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.pnlBottom.Controls.Add(Me.btnCopy)
        Me.pnlBottom.Controls.Add(Me.btnExportCsv)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.btnRealTime)
        Me.pnlBottom.Controls.Add(Me.btnSheets)
        Me.pnlBottom.Controls.Add(Me.lblOutputPath)
        Me.pnlBottom.Controls.Add(Me.lblSheetsStatus)
        Me.pnlBottom.Controls.Add(Me.lblLog)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 500)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(668, 84)
        Me.pnlBottom.TabIndex = 3
        '
        'btnCopy
        '
        Me.btnCopy.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnCopy.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCopy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnCopy.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.btnCopy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopy.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopy.ForeColor = System.Drawing.Color.White
        Me.btnCopy.Location = New System.Drawing.Point(0, 8)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(124, 34)
        Me.btnCopy.TabIndex = 0
        Me.btnCopy.Text = "📋 Salin Barcode"
        Me.btnCopy.UseVisualStyleBackColor = False
        '
        'btnExportCsv
        '
        Me.btnExportCsv.BackColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.btnExportCsv.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExportCsv.FlatAppearance.BorderSize = 0
        Me.btnExportCsv.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.btnExportCsv.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportCsv.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportCsv.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.btnExportCsv.Location = New System.Drawing.Point(132, 8)
        Me.btnExportCsv.Name = "btnExportCsv"
        Me.btnExportCsv.Size = New System.Drawing.Size(142, 34)
        Me.btnExportCsv.TabIndex = 1
        Me.btnExportCsv.Text = "📥 Export CSV"
        Me.btnExportCsv.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Location = New System.Drawing.Point(282, 8)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(124, 34)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "🗑 Bersihkan"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnRealTime
        '
        Me.btnRealTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnRealTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRealTime.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnRealTime.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.btnRealTime.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnRealTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRealTime.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRealTime.ForeColor = System.Drawing.Color.White
        Me.btnRealTime.Location = New System.Drawing.Point(414, 8)
        Me.btnRealTime.Name = "btnRealTime"
        Me.btnRealTime.Size = New System.Drawing.Size(132, 34)
        Me.btnRealTime.TabIndex = 3
        Me.btnRealTime.Text = "▶ Real-Time: OFF"
        Me.btnRealTime.UseVisualStyleBackColor = False
        '
        'btnSheets
        '
        Me.btnSheets.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnSheets.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSheets.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnSheets.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.btnSheets.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnSheets.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSheets.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSheets.ForeColor = System.Drawing.Color.White
        Me.btnSheets.Location = New System.Drawing.Point(554, 8)
        Me.btnSheets.Name = "btnSheets"
        Me.btnSheets.Size = New System.Drawing.Size(114, 34)
        Me.btnSheets.TabIndex = 4
        Me.btnSheets.Text = "📊 Sheets Setup"
        Me.btnSheets.UseVisualStyleBackColor = False
        '
        'lblOutputPath
        '
        Me.lblOutputPath.AutoEllipsis = True
        Me.lblOutputPath.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOutputPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblOutputPath.Location = New System.Drawing.Point(414, 46)
        Me.lblOutputPath.Name = "lblOutputPath"
        Me.lblOutputPath.Size = New System.Drawing.Size(132, 14)
        Me.lblOutputPath.TabIndex = 5
        Me.lblOutputPath.Text = "Real-time: mati"
        '
        'lblSheetsStatus
        '
        Me.lblSheetsStatus.AutoEllipsis = True
        Me.lblSheetsStatus.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSheetsStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblSheetsStatus.Location = New System.Drawing.Point(554, 46)
        Me.lblSheetsStatus.Name = "lblSheetsStatus"
        Me.lblSheetsStatus.Size = New System.Drawing.Size(114, 14)
        Me.lblSheetsStatus.TabIndex = 6
        Me.lblSheetsStatus.Text = "Sheets: OFF"
        '
        'lblLog
        '
        Me.lblLog.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLog.AutoEllipsis = True
        Me.lblLog.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLog.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblLog.Location = New System.Drawing.Point(0, 64)
        Me.lblLog.Name = "lblLog"
        Me.lblLog.Size = New System.Drawing.Size(668, 16)
        Me.lblLog.TabIndex = 7
        Me.lblLog.Text = "Siap menerima scan..."
        Me.lblLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1020, 660)
        Me.Controls.Add(Me.pnlRight)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlHeader)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(950, 620)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScanKilat — Wireless Server & Keystroke Auto-Typer"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.cardAutoType.ResumeLayout(False)
        Me.cardAutoType.PerformLayout()
        Me.cardQR.ResumeLayout(False)
        Me.cardQR.PerformLayout()
        CType(Me.picQRCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRight.ResumeLayout(False)
        CType(Me.dgvScan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDevicesBar.ResumeLayout(False)
        Me.pnlStats.ResumeLayout(False)
        Me.cardTotal.ResumeLayout(False)
        Me.cardTotal.PerformLayout()
        Me.cardLast.ResumeLayout(False)
        Me.cardLast.PerformLayout()
        Me.cardDevice.ResumeLayout(False)
        Me.cardDevice.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
End Class
