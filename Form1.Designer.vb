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
    Friend WithEvents lblLog As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim dataGridViewCellStyle1 As New System.Windows.Forms.DataGridViewCellStyle()
        Dim dataGridViewCellStyle2 As New System.Windows.Forms.DataGridViewCellStyle()
        Dim dataGridViewCellStyle3 As New System.Windows.Forms.DataGridViewCellStyle()

        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.lblServerStatus = New System.Windows.Forms.Label()
        Me.lblPhoneStatus = New System.Windows.Forms.Label()

        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.cardQR = New System.Windows.Forms.Panel()
        Me.lblQRTitle = New System.Windows.Forms.Label()
        Me.lblIp = New System.Windows.Forms.Label()
        Me.cmbNetworkIP = New System.Windows.Forms.ComboBox()
        Me.lblSessionCode = New System.Windows.Forms.Label()
        Me.picQRCode = New System.Windows.Forms.PictureBox()
        Me.lnkUrl = New System.Windows.Forms.LinkLabel()
        Me.btnNewSession = New System.Windows.Forms.Button()
        Me.btnFirewall = New System.Windows.Forms.Button()

        Me.cardAutoType = New System.Windows.Forms.Panel()
        Me.lblAutoTypeTitle = New System.Windows.Forms.Label()
        Me.chkAutoType = New System.Windows.Forms.CheckBox()
        Me.chkBeep = New System.Windows.Forms.CheckBox()
        Me.lblSuffix = New System.Windows.Forms.Label()
        Me.cmbSuffix = New System.Windows.Forms.ComboBox()
        Me.lblTip = New System.Windows.Forms.Label()

        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.pnlStats = New System.Windows.Forms.Panel()
        Me.cardTotal = New System.Windows.Forms.Panel()
        Me.lblTotalTitle = New System.Windows.Forms.Label()
        Me.lblTotalVal = New System.Windows.Forms.Label()
        Me.cardLast = New System.Windows.Forms.Panel()
        Me.lblLastTitle = New System.Windows.Forms.Label()
        Me.lblLastVal = New System.Windows.Forms.Label()
        Me.cardDevice = New System.Windows.Forms.Panel()
        Me.lblDeviceTitle = New System.Windows.Forms.Label()
        Me.lblDeviceVal = New System.Windows.Forms.Label()

        Me.pnlDevicesBar = New System.Windows.Forms.Panel()
        Me.lblDevicesBarTitle = New System.Windows.Forms.Label()
        Me.flpDevices = New System.Windows.Forms.FlowLayoutPanel()
        Me.toolTipDevices = New System.Windows.Forms.ToolTip()

        Me.dgvScan = New System.Windows.Forms.DataGridView()
        Me.colNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDevice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBarcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFormat = New System.Windows.Forms.DataGridViewTextBoxColumn()

        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.btnExportCsv = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lblLog = New System.Windows.Forms.Label()

        Me.pnlHeader.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.cardQR.SuspendLayout()
        CType(Me.picQRCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cardAutoType.SuspendLayout()
        Me.pnlRight.SuspendLayout()
        Me.pnlStats.SuspendLayout()
        Me.cardTotal.SuspendLayout()
        Me.cardLast.SuspendLayout()
        Me.cardDevice.SuspendLayout()
        Me.pnlDevicesBar.SuspendLayout()
        CType(Me.dgvScan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()

        '
        ' pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(9, 13, 22)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.lblSubtitle)
        Me.pnlHeader.Controls.Add(Me.lblServerStatus)
        Me.pnlHeader.Controls.Add(Me.lblPhoneStatus)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1020, 68)
        Me.pnlHeader.TabIndex = 0

        '
        ' lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(16, 11)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(270, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "BARCODE 2 SCANNER PRO"

        '
        ' lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184)
        Me.lblSubtitle.Location = New System.Drawing.Point(17, 39)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(370, 15)
        Me.lblSubtitle.TabIndex = 1
        Me.lblSubtitle.Text = "Kamera HP sebagai Barcode Scanner Fisik & Auto-Type ke Aplikasi Lain"

        '
        ' lblServerStatus
        '
        Me.lblServerStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblServerStatus.BackColor = System.Drawing.Color.FromArgb(20, 30, 48)
        Me.lblServerStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblServerStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerStatus.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129)
        Me.lblServerStatus.Location = New System.Drawing.Point(540, 19)
        Me.lblServerStatus.Name = "lblServerStatus"
        Me.lblServerStatus.Padding = New System.Windows.Forms.Padding(6, 4, 6, 4)
        Me.lblServerStatus.Size = New System.Drawing.Size(230, 30)
        Me.lblServerStatus.TabIndex = 2
        Me.lblServerStatus.Text = "● Server: Memulai..."
        Me.lblServerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        '
        ' lblPhoneStatus
        '
        Me.lblPhoneStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPhoneStatus.BackColor = System.Drawing.Color.FromArgb(35, 30, 20)
        Me.lblPhoneStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPhoneStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhoneStatus.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11)
        Me.lblPhoneStatus.Location = New System.Drawing.Point(780, 19)
        Me.lblPhoneStatus.Name = "lblPhoneStatus"
        Me.lblPhoneStatus.Padding = New System.Windows.Forms.Padding(6, 4, 6, 4)
        Me.lblPhoneStatus.Size = New System.Drawing.Size(225, 30)
        Me.lblPhoneStatus.TabIndex = 3
        Me.lblPhoneStatus.Text = "● HP Belum Tersambung"
        Me.lblPhoneStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        '
        ' pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.FromArgb(15, 23, 42)
        Me.pnlLeft.Controls.Add(Me.cardAutoType)
        Me.pnlLeft.Controls.Add(Me.cardQR)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 68)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Padding = New System.Windows.Forms.Padding(12)
        Me.pnlLeft.Size = New System.Drawing.Size(340, 592)
        Me.pnlLeft.TabIndex = 1

        '
        ' cardQR
        '
        Me.cardQR.BackColor = System.Drawing.Color.FromArgb(30, 41, 59)
        Me.cardQR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardQR.Controls.Add(Me.lblQRTitle)
        Me.cardQR.Controls.Add(Me.lblIp)
        Me.cardQR.Controls.Add(Me.cmbNetworkIP)
        Me.cardQR.Controls.Add(Me.lblSessionCode)
        Me.cardQR.Controls.Add(Me.picQRCode)
        Me.cardQR.Controls.Add(Me.lnkUrl)
        Me.cardQR.Controls.Add(Me.btnNewSession)
        Me.cardQR.Controls.Add(Me.btnFirewall)
        Me.cardQR.Dock = System.Windows.Forms.DockStyle.Top
        Me.cardQR.Location = New System.Drawing.Point(12, 12)
        Me.cardQR.Name = "cardQR"
        Me.cardQR.Padding = New System.Windows.Forms.Padding(12)
        Me.cardQR.Size = New System.Drawing.Size(316, 368)
        Me.cardQR.TabIndex = 0

        '
        ' lblQRTitle
        '
        Me.lblQRTitle.AutoSize = True
        Me.lblQRTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQRTitle.ForeColor = System.Drawing.Color.White
        Me.lblQRTitle.Location = New System.Drawing.Point(10, 6)
        Me.lblQRTitle.Name = "lblQRTitle"
        Me.lblQRTitle.Size = New System.Drawing.Size(184, 19)
        Me.lblQRTitle.TabIndex = 0
        Me.lblQRTitle.Text = "Hubungkan HP (Scan QR)"

        '
        ' lblIp
        '
        Me.lblIp.AutoSize = True
        Me.lblIp.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIp.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184)
        Me.lblIp.Location = New System.Drawing.Point(10, 27)
        Me.lblIp.Name = "lblIp"
        Me.lblIp.Size = New System.Drawing.Size(95, 12)
        Me.lblIp.TabIndex = 1
        Me.lblIp.Text = "Jaringan / IP Laptop:"

        '
        ' cmbNetworkIP
        '
        Me.cmbNetworkIP.BackColor = System.Drawing.Color.FromArgb(15, 23, 42)
        Me.cmbNetworkIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNetworkIP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbNetworkIP.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNetworkIP.ForeColor = System.Drawing.Color.FromArgb(56, 189, 248)
        Me.cmbNetworkIP.FormattingEnabled = True
        Me.cmbNetworkIP.Location = New System.Drawing.Point(12, 42)
        Me.cmbNetworkIP.Name = "cmbNetworkIP"
        Me.cmbNetworkIP.Size = New System.Drawing.Size(290, 21)
        Me.cmbNetworkIP.TabIndex = 2

        '
        ' lblSessionCode
        '
        Me.lblSessionCode.BackColor = System.Drawing.Color.FromArgb(15, 23, 42)
        Me.lblSessionCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSessionCode.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSessionCode.ForeColor = System.Drawing.Color.FromArgb(56, 189, 248)
        Me.lblSessionCode.Location = New System.Drawing.Point(12, 69)
        Me.lblSessionCode.Name = "lblSessionCode"
        Me.lblSessionCode.Size = New System.Drawing.Size(290, 26)
        Me.lblSessionCode.TabIndex = 3
        Me.lblSessionCode.Text = "SESI: ......"
        Me.lblSessionCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        '
        ' picQRCode
        '
        Me.picQRCode.BackColor = System.Drawing.Color.White
        Me.picQRCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picQRCode.Location = New System.Drawing.Point(70, 99)
        Me.picQRCode.Name = "picQRCode"
        Me.picQRCode.Size = New System.Drawing.Size(175, 175)
        Me.picQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picQRCode.TabIndex = 4
        Me.picQRCode.TabStop = False

        '
        ' lnkUrl
        '
        Me.lnkUrl.ActiveLinkColor = System.Drawing.Color.FromArgb(165, 180, 252)
        Me.lnkUrl.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkUrl.LinkColor = System.Drawing.Color.FromArgb(129, 140, 248)
        Me.lnkUrl.Location = New System.Drawing.Point(12, 277)
        Me.lnkUrl.Name = "lnkUrl"
        Me.lnkUrl.Size = New System.Drawing.Size(290, 16)
        Me.lnkUrl.TabIndex = 5
        Me.lnkUrl.TabStop = True
        Me.lnkUrl.Text = "Salin URL Manual ke Clipboard"
        Me.lnkUrl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        '
        ' btnNewSession
        '
        Me.btnNewSession.BackColor = System.Drawing.Color.FromArgb(51, 65, 85)
        Me.btnNewSession.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNewSession.FlatAppearance.BorderSize = 0
        Me.btnNewSession.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewSession.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewSession.ForeColor = System.Drawing.Color.White
        Me.btnNewSession.Location = New System.Drawing.Point(12, 298)
        Me.btnNewSession.Name = "btnNewSession"
        Me.btnNewSession.Size = New System.Drawing.Size(140, 28)
        Me.btnNewSession.TabIndex = 6
        Me.btnNewSession.Text = "🔄 Sesi Baru"
        Me.btnNewSession.UseVisualStyleBackColor = False

        '
        ' btnFirewall
        '
        Me.btnFirewall.BackColor = System.Drawing.Color.FromArgb(15, 118, 110)
        Me.btnFirewall.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnFirewall.FlatAppearance.BorderSize = 0
        Me.btnFirewall.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFirewall.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFirewall.ForeColor = System.Drawing.Color.White
        Me.btnFirewall.Location = New System.Drawing.Point(160, 298)
        Me.btnFirewall.Name = "btnFirewall"
        Me.btnFirewall.Size = New System.Drawing.Size(142, 28)
        Me.btnFirewall.TabIndex = 7
        Me.btnFirewall.Text = "🛡 Buka Firewall"
        Me.btnFirewall.UseVisualStyleBackColor = False

        '
        ' cardAutoType
        '
        Me.cardAutoType.BackColor = System.Drawing.Color.FromArgb(30, 41, 59)
        Me.cardAutoType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardAutoType.Controls.Add(Me.lblAutoTypeTitle)
        Me.cardAutoType.Controls.Add(Me.chkAutoType)
        Me.cardAutoType.Controls.Add(Me.chkBeep)
        Me.cardAutoType.Controls.Add(Me.lblSuffix)
        Me.cardAutoType.Controls.Add(Me.cmbSuffix)
        Me.cardAutoType.Controls.Add(Me.lblTip)
        Me.cardAutoType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cardAutoType.Location = New System.Drawing.Point(12, 380)
        Me.cardAutoType.Name = "cardAutoType"
        Me.cardAutoType.Padding = New System.Windows.Forms.Padding(12)
        Me.cardAutoType.Size = New System.Drawing.Size(316, 233)
        Me.cardAutoType.TabIndex = 1

        '
        ' lblAutoTypeTitle
        '
        Me.lblAutoTypeTitle.AutoSize = True
        Me.lblAutoTypeTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoTypeTitle.ForeColor = System.Drawing.Color.White
        Me.lblAutoTypeTitle.Location = New System.Drawing.Point(10, 8)
        Me.lblAutoTypeTitle.Name = "lblAutoTypeTitle"
        Me.lblAutoTypeTitle.Size = New System.Drawing.Size(195, 19)
        Me.lblAutoTypeTitle.TabIndex = 0
        Me.lblAutoTypeTitle.Text = "Pengaturan Ketik Otomatis"

        '
        ' chkAutoType
        '
        Me.chkAutoType.AutoSize = True
        Me.chkAutoType.Checked = True
        Me.chkAutoType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAutoType.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkAutoType.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoType.ForeColor = System.Drawing.Color.FromArgb(52, 211, 153)
        Me.chkAutoType.Location = New System.Drawing.Point(12, 35)
        Me.chkAutoType.Name = "chkAutoType"
        Me.chkAutoType.Size = New System.Drawing.Size(262, 19)
        Me.chkAutoType.TabIndex = 1
        Me.chkAutoType.Text = "Ketik ke Aplikasi Aktif (Notepad/Excel/POS)"
        Me.chkAutoType.UseVisualStyleBackColor = True

        '
        ' chkBeep
        '
        Me.chkBeep.AutoSize = True
        Me.chkBeep.Checked = True
        Me.chkBeep.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBeep.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkBeep.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBeep.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225)
        Me.chkBeep.Location = New System.Drawing.Point(12, 60)
        Me.chkBeep.Name = "chkBeep"
        Me.chkBeep.Size = New System.Drawing.Size(189, 19)
        Me.chkBeep.TabIndex = 2
        Me.chkBeep.Text = "Bunyi Beep di Laptop saat Scan"
        Me.chkBeep.UseVisualStyleBackColor = True

        '
        ' lblSuffix
        '
        Me.lblSuffix.AutoSize = True
        Me.lblSuffix.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuffix.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184)
        Me.lblSuffix.Location = New System.Drawing.Point(10, 89)
        Me.lblSuffix.Name = "lblSuffix"
        Me.lblSuffix.Size = New System.Drawing.Size(117, 15)
        Me.lblSuffix.TabIndex = 3
        Me.lblSuffix.Text = "Tombol Akhir (Suffix):"

        '
        ' cmbSuffix
        '
        Me.cmbSuffix.BackColor = System.Drawing.Color.FromArgb(15, 23, 42)
        Me.cmbSuffix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSuffix.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSuffix.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSuffix.ForeColor = System.Drawing.Color.White
        Me.cmbSuffix.FormattingEnabled = True
        Me.cmbSuffix.Items.AddRange(New Object() {"Enter (Baris Baru)", "Tab (Kolom Baru)", "Tanpa Akhiran"})
        Me.cmbSuffix.Location = New System.Drawing.Point(135, 85)
        Me.cmbSuffix.Name = "cmbSuffix"
        Me.cmbSuffix.Size = New System.Drawing.Size(167, 23)
        Me.cmbSuffix.TabIndex = 4

        '
        ' lblTip
        '
        Me.lblTip.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTip.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184)
        Me.lblTip.Location = New System.Drawing.Point(10, 118)
        Me.lblTip.Name = "lblTip"
        Me.lblTip.Size = New System.Drawing.Size(292, 60)
        Me.lblTip.TabIndex = 5
        Me.lblTip.Text = "💡 Cara Pakai: Buka program kasir, Excel, atau Notepad, dan arahkan kursor ke sana. Hasil scan barcode HP akan langsung terketik otomatis seperti scanner fisik USB!"

        '
        ' pnlRight
        '
        Me.pnlRight.BackColor = System.Drawing.Color.FromArgb(15, 23, 42)
        Me.pnlRight.Controls.Add(Me.dgvScan)
        Me.pnlRight.Controls.Add(Me.pnlDevicesBar)
        Me.pnlRight.Controls.Add(Me.pnlStats)
        Me.pnlRight.Controls.Add(Me.pnlBottom)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRight.Location = New System.Drawing.Point(340, 68)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Padding = New System.Windows.Forms.Padding(0, 12, 12, 12)
        Me.pnlRight.Size = New System.Drawing.Size(680, 592)
        Me.pnlRight.TabIndex = 2

        '
        ' pnlStats
        '
        Me.pnlStats.Controls.Add(Me.cardTotal)
        Me.pnlStats.Controls.Add(Me.cardLast)
        Me.pnlStats.Controls.Add(Me.cardDevice)
        Me.pnlStats.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlStats.Location = New System.Drawing.Point(0, 12)
        Me.pnlStats.Name = "pnlStats"
        Me.pnlStats.Size = New System.Drawing.Size(668, 65)
        Me.pnlStats.TabIndex = 0

        '
        ' cardTotal
        '
        Me.cardTotal.BackColor = System.Drawing.Color.FromArgb(30, 41, 59)
        Me.cardTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardTotal.Controls.Add(Me.lblTotalVal)
        Me.cardTotal.Controls.Add(Me.lblTotalTitle)
        Me.cardTotal.Location = New System.Drawing.Point(0, 0)
        Me.cardTotal.Name = "cardTotal"
        Me.cardTotal.Size = New System.Drawing.Size(160, 58)
        Me.cardTotal.TabIndex = 0

        '
        ' lblTotalTitle
        '
        Me.lblTotalTitle.AutoSize = True
        Me.lblTotalTitle.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalTitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184)
        Me.lblTotalTitle.Location = New System.Drawing.Point(8, 6)
        Me.lblTotalTitle.Name = "lblTotalTitle"
        Me.lblTotalTitle.Size = New System.Drawing.Size(69, 12)
        Me.lblTotalTitle.TabIndex = 0
        Me.lblTotalTitle.Text = "TOTAL SCAN"

        '
        ' lblTotalVal
        '
        Me.lblTotalVal.AutoSize = True
        Me.lblTotalVal.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalVal.ForeColor = System.Drawing.Color.FromArgb(52, 211, 153)
        Me.lblTotalVal.Location = New System.Drawing.Point(6, 20)
        Me.lblTotalVal.Name = "lblTotalVal"
        Me.lblTotalVal.Size = New System.Drawing.Size(26, 30)
        Me.lblTotalVal.TabIndex = 1
        Me.lblTotalVal.Text = "0"

        '
        ' cardLast
        '
        Me.cardLast.BackColor = System.Drawing.Color.FromArgb(30, 41, 59)
        Me.cardLast.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardLast.Controls.Add(Me.lblLastVal)
        Me.cardLast.Controls.Add(Me.lblLastTitle)
        Me.cardLast.Location = New System.Drawing.Point(170, 0)
        Me.cardLast.Name = "cardLast"
        Me.cardLast.Size = New System.Drawing.Size(260, 58)
        Me.cardLast.TabIndex = 1

        '
        ' lblLastTitle
        '
        Me.lblLastTitle.AutoSize = True
        Me.lblLastTitle.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastTitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184)
        Me.lblLastTitle.Location = New System.Drawing.Point(8, 6)
        Me.lblLastTitle.Name = "lblLastTitle"
        Me.lblLastTitle.Size = New System.Drawing.Size(107, 12)
        Me.lblLastTitle.TabIndex = 0
        Me.lblLastTitle.Text = "BARCODE TERAKHIR"

        '
        ' lblLastVal
        '
        Me.lblLastVal.AutoEllipsis = True
        Me.lblLastVal.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastVal.ForeColor = System.Drawing.Color.FromArgb(56, 189, 248)
        Me.lblLastVal.Location = New System.Drawing.Point(6, 24)
        Me.lblLastVal.Name = "lblLastVal"
        Me.lblLastVal.Size = New System.Drawing.Size(245, 24)
        Me.lblLastVal.TabIndex = 1
        Me.lblLastVal.Text = "-"

        '
        ' cardDevice
        '
        Me.cardDevice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cardDevice.BackColor = System.Drawing.Color.FromArgb(30, 41, 59)
        Me.cardDevice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardDevice.Controls.Add(Me.lblDeviceVal)
        Me.cardDevice.Controls.Add(Me.lblDeviceTitle)
        Me.cardDevice.Location = New System.Drawing.Point(440, 0)
        Me.cardDevice.Name = "cardDevice"
        Me.cardDevice.Size = New System.Drawing.Size(228, 58)
        Me.cardDevice.TabIndex = 2

        '
        ' lblDeviceTitle
        '
        Me.lblDeviceTitle.AutoSize = True
        Me.lblDeviceTitle.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeviceTitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184)
        Me.lblDeviceTitle.Location = New System.Drawing.Point(8, 6)
        Me.lblDeviceTitle.Name = "lblDeviceTitle"
        Me.lblDeviceTitle.Size = New System.Drawing.Size(107, 12)
        Me.lblDeviceTitle.TabIndex = 0
        Me.lblDeviceTitle.Text = "PERANGKAT AKTIF"

        '
        ' lblDeviceVal
        '
        Me.lblDeviceVal.AutoEllipsis = True
        Me.lblDeviceVal.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeviceVal.ForeColor = System.Drawing.Color.FromArgb(244, 114, 182)
        Me.lblDeviceVal.Location = New System.Drawing.Point(6, 24)
        Me.lblDeviceVal.Name = "lblDeviceVal"
        Me.lblDeviceVal.Size = New System.Drawing.Size(215, 24)
        Me.lblDeviceVal.TabIndex = 1
        Me.lblDeviceVal.Text = "-"

        '
        ' pnlDevicesBar
        '
        Me.pnlDevicesBar.BackColor = System.Drawing.Color.FromArgb(20, 30, 48)
        Me.pnlDevicesBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDevicesBar.Controls.Add(Me.flpDevices)
        Me.pnlDevicesBar.Controls.Add(Me.lblDevicesBarTitle)
        Me.pnlDevicesBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDevicesBar.Location = New System.Drawing.Point(0, 77)
        Me.pnlDevicesBar.Name = "pnlDevicesBar"
        Me.pnlDevicesBar.Padding = New System.Windows.Forms.Padding(8, 3, 8, 3)
        Me.pnlDevicesBar.Size = New System.Drawing.Size(668, 36)
        Me.pnlDevicesBar.TabIndex = 1

        '
        ' lblDevicesBarTitle
        '
        Me.lblDevicesBarTitle.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblDevicesBarTitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDevicesBarTitle.ForeColor = System.Drawing.Color.FromArgb(56, 189, 248)
        Me.lblDevicesBarTitle.Location = New System.Drawing.Point(8, 3)
        Me.lblDevicesBarTitle.Name = "lblDevicesBarTitle"
        Me.lblDevicesBarTitle.Size = New System.Drawing.Size(130, 28)
        Me.lblDevicesBarTitle.TabIndex = 0
        Me.lblDevicesBarTitle.Text = "📱 HP Terhubung (0):"
        Me.lblDevicesBarTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        '
        ' flpDevices
        '
        Me.flpDevices.AutoScroll = True
        Me.flpDevices.BackColor = System.Drawing.Color.Transparent
        Me.flpDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpDevices.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight
        Me.flpDevices.Location = New System.Drawing.Point(138, 3)
        Me.flpDevices.Name = "flpDevices"
        Me.flpDevices.Size = New System.Drawing.Size(520, 28)
        Me.flpDevices.TabIndex = 1
        Me.flpDevices.WrapContents = False

        '
        ' dgvScan
        '
        Me.dgvScan.AllowUserToAddRows = False
        Me.dgvScan.AllowUserToDeleteRows = False
        Me.dgvScan.AllowUserToResizeRows = False
        Me.dgvScan.BackgroundColor = System.Drawing.Color.FromArgb(15, 23, 42)
        Me.dgvScan.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvScan.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvScan.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(9, 13, 22)
        dataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184)
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(9, 13, 22)
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(148, 163, 184)
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvScan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1
        Me.dgvScan.ColumnHeadersHeight = 36
        Me.dgvScan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvScan.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNo, Me.colTime, Me.colDevice, Me.colBarcode, Me.colFormat})
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(30, 41, 59)
        dataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(248, 250, 252)
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(79, 70, 229)
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White
        dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvScan.DefaultCellStyle = dataGridViewCellStyle2
        Me.dgvScan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvScan.EnableHeadersVisualStyles = False
        Me.dgvScan.GridColor = System.Drawing.Color.FromArgb(51, 65, 85)
        Me.dgvScan.Location = New System.Drawing.Point(0, 77)
        Me.dgvScan.MultiSelect = False
        Me.dgvScan.Name = "dgvScan"
        Me.dgvScan.ReadOnly = True
        Me.dgvScan.RowHeadersVisible = False
        dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(20, 30, 48)
        Me.dgvScan.RowsDefaultCellStyle = dataGridViewCellStyle3
        Me.dgvScan.RowTemplate.Height = 30
        Me.dgvScan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvScan.Size = New System.Drawing.Size(668, 458)
        Me.dgvScan.TabIndex = 1

        '
        ' colNo
        '
        Me.colNo.HeaderText = "#"
        Me.colNo.Name = "colNo"
        Me.colNo.ReadOnly = True
        Me.colNo.Width = 45

        '
        ' colTime
        '
        Me.colTime.HeaderText = "Waktu"
        Me.colTime.Name = "colTime"
        Me.colTime.ReadOnly = True
        Me.colTime.Width = 135

        '
        ' colDevice
        '
        Me.colDevice.HeaderText = "Perangkat HP"
        Me.colDevice.Name = "colDevice"
        Me.colDevice.ReadOnly = True
        Me.colDevice.Width = 140

        '
        ' colBarcode
        '
        Me.colBarcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colBarcode.HeaderText = "Hasil Barcode / Teks"
        Me.colBarcode.Name = "colBarcode"
        Me.colBarcode.ReadOnly = True

        '
        ' colFormat
        '
        Me.colFormat.HeaderText = "Format"
        Me.colFormat.Name = "colFormat"
        Me.colFormat.ReadOnly = True
        Me.colFormat.Width = 90

        '
        ' pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnCopy)
        Me.pnlBottom.Controls.Add(Me.btnExportCsv)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.lblLog)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 535)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(668, 45)
        Me.pnlBottom.TabIndex = 2

        '
        ' btnCopy
        '
        Me.btnCopy.BackColor = System.Drawing.Color.FromArgb(51, 65, 85)
        Me.btnCopy.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCopy.FlatAppearance.BorderSize = 0
        Me.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopy.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopy.ForeColor = System.Drawing.Color.White
        Me.btnCopy.Location = New System.Drawing.Point(0, 10)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(120, 30)
        Me.btnCopy.TabIndex = 0
        Me.btnCopy.Text = "📋 Salin Barcode"
        Me.btnCopy.UseVisualStyleBackColor = False

        '
        ' btnExportCsv
        '
        Me.btnExportCsv.BackColor = System.Drawing.Color.FromArgb(5, 150, 105)
        Me.btnExportCsv.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExportCsv.FlatAppearance.BorderSize = 0
        Me.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportCsv.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportCsv.ForeColor = System.Drawing.Color.White
        Me.btnExportCsv.Location = New System.Drawing.Point(130, 10)
        Me.btnExportCsv.Name = "btnExportCsv"
        Me.btnExportCsv.Size = New System.Drawing.Size(140, 30)
        Me.btnExportCsv.TabIndex = 1
        Me.btnExportCsv.Text = "📥 Export CSV"
        Me.btnExportCsv.UseVisualStyleBackColor = False

        '
        ' btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(71, 85, 105)
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Location = New System.Drawing.Point(280, 10)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(120, 30)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "🗑 Bersihkan"
        Me.btnClear.UseVisualStyleBackColor = False

        '
        ' lblLog
        '
        Me.lblLog.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLog.AutoEllipsis = True
        Me.lblLog.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLog.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184)
        Me.lblLog.Location = New System.Drawing.Point(410, 16)
        Me.lblLog.Name = "lblLog"
        Me.lblLog.Size = New System.Drawing.Size(250, 20)
        Me.lblLog.TabIndex = 3
        Me.lblLog.Text = "Siap menerima scan..."
        Me.lblLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight

        '
        ' Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(15, 23, 42)
        Me.ClientSize = New System.Drawing.Size(1020, 660)
        Me.Controls.Add(Me.pnlRight)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlHeader)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(950, 620)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Barcode 2 Scanner Pro — Wireless Server & Keystroke Auto-Typer"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.cardQR.ResumeLayout(False)
        Me.cardQR.PerformLayout()
        CType(Me.picQRCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cardAutoType.ResumeLayout(False)
        Me.cardAutoType.PerformLayout()
        Me.pnlRight.ResumeLayout(False)
        Me.pnlStats.ResumeLayout(False)
        Me.cardTotal.ResumeLayout(False)
        Me.cardTotal.PerformLayout()
        Me.cardLast.ResumeLayout(False)
        Me.cardLast.PerformLayout()
        Me.cardDevice.ResumeLayout(False)
        Me.cardDevice.PerformLayout()
        Me.pnlDevicesBar.ResumeLayout(False)
        CType(Me.dgvScan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
End Class
