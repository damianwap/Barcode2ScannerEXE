<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewSessionConfirmForm
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
    Friend WithEvents pnlWarningBox As System.Windows.Forms.Panel
    Friend WithEvents lblWarningHeader As System.Windows.Forms.Label
    Friend WithEvents lblWarningDetail As System.Windows.Forms.Label
    Friend WithEvents pnlConsequences As System.Windows.Forms.Panel
    Friend WithEvents lblConsequencesTitle As System.Windows.Forms.Label
    Friend WithEvents lblConsequences As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnConfirm As System.Windows.Forms.Button

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblIcon = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.pnlWarningBox = New System.Windows.Forms.Panel()
        Me.lblWarningHeader = New System.Windows.Forms.Label()
        Me.lblWarningDetail = New System.Windows.Forms.Label()
        Me.pnlConsequences = New System.Windows.Forms.Panel()
        Me.lblConsequencesTitle = New System.Windows.Forms.Label()
        Me.lblConsequences = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnConfirm = New System.Windows.Forms.Button()
        Me.pnlWarningBox.SuspendLayout()
        Me.pnlConsequences.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblIcon
        '
        Me.lblIcon.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIcon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
        Me.lblIcon.Location = New System.Drawing.Point(18, 14)
        Me.lblIcon.Name = "lblIcon"
        Me.lblIcon.Size = New System.Drawing.Size(38, 38)
        Me.lblIcon.TabIndex = 0
        Me.lblIcon.Text = "⚠️"
        Me.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(60, 16)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(209, 21)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "Konfirmasi Buat Sesi Baru"
        '
        'lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblSubtitle.Location = New System.Drawing.Point(62, 40)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(350, 15)
        Me.lblSubtitle.TabIndex = 2
        Me.lblSubtitle.Text = "Sesi scanner saat ini akan ditutup dan digantikan dengan sesi baru."
        '
        'pnlWarningBox
        '
        Me.pnlWarningBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.pnlWarningBox.Controls.Add(Me.lblWarningHeader)
        Me.pnlWarningBox.Controls.Add(Me.lblWarningDetail)
        Me.pnlWarningBox.Location = New System.Drawing.Point(20, 68)
        Me.pnlWarningBox.Name = "pnlWarningBox"
        Me.pnlWarningBox.Padding = New System.Windows.Forms.Padding(12, 10, 12, 10)
        Me.pnlWarningBox.Size = New System.Drawing.Size(420, 106)
        Me.pnlWarningBox.TabIndex = 3
        '
        'lblWarningHeader
        '
        Me.lblWarningHeader.AutoSize = True
        Me.lblWarningHeader.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWarningHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(191, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.lblWarningHeader.Location = New System.Drawing.Point(12, 10)
        Me.lblWarningHeader.Name = "lblWarningHeader"
        Me.lblWarningHeader.Size = New System.Drawing.Size(107, 15)
        Me.lblWarningHeader.TabIndex = 0
        Me.lblWarningHeader.Text = "Status Perangkat..."
        '
        'lblWarningDetail
        '
        Me.lblWarningDetail.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWarningDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.lblWarningDetail.Location = New System.Drawing.Point(12, 30)
        Me.lblWarningDetail.Name = "lblWarningDetail"
        Me.lblWarningDetail.Size = New System.Drawing.Size(396, 66)
        Me.lblWarningDetail.TabIndex = 1
        Me.lblWarningDetail.Text = "Detail perangkat yang terhubung..."
        '
        'pnlConsequences
        '
        Me.pnlConsequences.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(29, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.pnlConsequences.Controls.Add(Me.lblConsequencesTitle)
        Me.pnlConsequences.Controls.Add(Me.lblConsequences)
        Me.pnlConsequences.Location = New System.Drawing.Point(20, 184)
        Me.pnlConsequences.Name = "pnlConsequences"
        Me.pnlConsequences.Padding = New System.Windows.Forms.Padding(12, 10, 12, 10)
        Me.pnlConsequences.Size = New System.Drawing.Size(420, 92)
        Me.pnlConsequences.TabIndex = 4
        '
        'lblConsequencesTitle
        '
        Me.lblConsequencesTitle.AutoSize = True
        Me.lblConsequencesTitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsequencesTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.lblConsequencesTitle.Location = New System.Drawing.Point(12, 8)
        Me.lblConsequencesTitle.Name = "lblConsequencesTitle"
        Me.lblConsequencesTitle.Size = New System.Drawing.Size(147, 15)
        Me.lblConsequencesTitle.TabIndex = 0
        Me.lblConsequencesTitle.Text = "Dampak pembuatan sesi:"
        '
        'lblConsequences
        '
        Me.lblConsequences.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsequences.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.lblConsequences.Location = New System.Drawing.Point(12, 28)
        Me.lblConsequences.Name = "lblConsequences"
        Me.lblConsequences.Size = New System.Drawing.Size(396, 54)
        Me.lblConsequences.TabIndex = 1
        Me.lblConsequences.Text = "• Semua HP yang aktif akan terputus otomatis." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "• QR Code dan kode sesi lama tidak berlaku lagi." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "• Operator harus memindai (scan) ulang QR Code baru di layar PC."
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(208, 290)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(95, 34)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Batal"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnConfirm
        '
        Me.btnConfirm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConfirm.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
        Me.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnConfirm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
        Me.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfirm.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfirm.ForeColor = System.Drawing.Color.White
        Me.btnConfirm.Location = New System.Drawing.Point(312, 290)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(128, 34)
        Me.btnConfirm.TabIndex = 6
        Me.btnConfirm.Text = "Buat Sesi Baru"
        Me.btnConfirm.UseVisualStyleBackColor = False
        '
        'NewSessionConfirmForm
        '
        Me.AcceptButton = Me.btnConfirm
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(460, 340)
        Me.Controls.Add(Me.btnConfirm)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.pnlConsequences)
        Me.Controls.Add(Me.pnlWarningBox)
        Me.Controls.Add(Me.lblSubtitle)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.lblIcon)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NewSessionConfirmForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Konfirmasi Sesi Baru"
        Me.pnlWarningBox.ResumeLayout(False)
        Me.pnlWarningBox.PerformLayout()
        Me.pnlConsequences.ResumeLayout(False)
        Me.pnlConsequences.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
End Class
