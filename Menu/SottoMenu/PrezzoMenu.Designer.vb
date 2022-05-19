<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PrezzoMenu
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PrezzoMenu))
        Me.panelMenu = New System.Windows.Forms.Panel()
        Me.btnPrezzo = New Guna.UI2.WinForms.Guna2Button()
        Me.panelLogo = New System.Windows.Forms.Panel()
        Me.panelTitleBar = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnMaximize = New System.Windows.Forms.Button()
        Me.btnMinimize = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.panelDesktopPane = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.panelMenu.SuspendLayout()
        Me.panelTitleBar.SuspendLayout()
        Me.panelDesktopPane.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelMenu
        '
        Me.panelMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.panelMenu.Controls.Add(Me.btnPrezzo)
        Me.panelMenu.Controls.Add(Me.panelLogo)
        Me.panelMenu.Dock = System.Windows.Forms.DockStyle.Left
        Me.panelMenu.Location = New System.Drawing.Point(0, 0)
        Me.panelMenu.Name = "panelMenu"
        Me.panelMenu.Size = New System.Drawing.Size(186, 802)
        Me.panelMenu.TabIndex = 0
        '
        'btnPrezzo
        '
        Me.btnPrezzo.CheckedState.Parent = Me.btnPrezzo
        Me.btnPrezzo.CustomBorderColor = System.Drawing.Color.Black
        Me.btnPrezzo.CustomBorderThickness = New System.Windows.Forms.Padding(1)
        Me.btnPrezzo.CustomImages.Parent = Me.btnPrezzo
        Me.btnPrezzo.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnPrezzo.FillColor = System.Drawing.Color.Transparent
        Me.btnPrezzo.Font = New System.Drawing.Font("Franklin Gothic Medium Cond", 13.0!)
        Me.btnPrezzo.ForeColor = System.Drawing.Color.Black
        Me.btnPrezzo.HoverState.Parent = Me.btnPrezzo
        Me.btnPrezzo.Image = Global.VipDesignerUM.My.Resources.Resources.dollar
        Me.btnPrezzo.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnPrezzo.ImageOffset = New System.Drawing.Point(0, 2)
        Me.btnPrezzo.ImageSize = New System.Drawing.Size(40, 40)
        Me.btnPrezzo.Location = New System.Drawing.Point(0, 68)
        Me.btnPrezzo.Name = "btnPrezzo"
        Me.btnPrezzo.ShadowDecoration.Parent = Me.btnPrezzo
        Me.btnPrezzo.Size = New System.Drawing.Size(186, 61)
        Me.btnPrezzo.TabIndex = 150
        Me.btnPrezzo.Text = "   Prezzo"
        '
        'panelLogo
        '
        Me.panelLogo.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(131, Byte), Integer))
        Me.panelLogo.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelLogo.Location = New System.Drawing.Point(0, 0)
        Me.panelLogo.Name = "panelLogo"
        Me.panelLogo.Size = New System.Drawing.Size(186, 68)
        Me.panelLogo.TabIndex = 0
        '
        'panelTitleBar
        '
        Me.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(131, Byte), Integer))
        Me.panelTitleBar.Controls.Add(Me.lblTitle)
        Me.panelTitleBar.Controls.Add(Me.btnMaximize)
        Me.panelTitleBar.Controls.Add(Me.btnMinimize)
        Me.panelTitleBar.Controls.Add(Me.btnClose)
        Me.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelTitleBar.Location = New System.Drawing.Point(186, 0)
        Me.panelTitleBar.Name = "panelTitleBar"
        Me.panelTitleBar.Size = New System.Drawing.Size(1326, 68)
        Me.panelTitleBar.TabIndex = 1
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!)
        Me.lblTitle.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblTitle.Location = New System.Drawing.Point(526, 12)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(208, 46)
        Me.lblTitle.TabIndex = 8
        Me.lblTitle.Text = "DataSheet"
        '
        'btnMaximize
        '
        Me.btnMaximize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMaximize.BackgroundImage = Global.VipDesignerUM.My.Resources.Resources.resize
        Me.btnMaximize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnMaximize.FlatAppearance.BorderSize = 0
        Me.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMaximize.Location = New System.Drawing.Point(1274, 3)
        Me.btnMaximize.Name = "btnMaximize"
        Me.btnMaximize.Size = New System.Drawing.Size(23, 23)
        Me.btnMaximize.TabIndex = 7
        Me.btnMaximize.UseVisualStyleBackColor = True
        Me.btnMaximize.Visible = False
        '
        'btnMinimize
        '
        Me.btnMinimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMinimize.BackgroundImage = Global.VipDesignerUM.My.Resources.Resources.min
        Me.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnMinimize.FlatAppearance.BorderSize = 0
        Me.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMinimize.Location = New System.Drawing.Point(1272, 3)
        Me.btnMinimize.Name = "btnMinimize"
        Me.btnMinimize.Size = New System.Drawing.Size(23, 23)
        Me.btnMinimize.TabIndex = 6
        Me.btnMinimize.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackgroundImage = Global.VipDesignerUM.My.Resources.Resources.close
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(1300, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(23, 23)
        Me.btnClose.TabIndex = 5
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'panelDesktopPane
        '
        Me.panelDesktopPane.Controls.Add(Me.PictureBox1)
        Me.panelDesktopPane.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelDesktopPane.Location = New System.Drawing.Point(186, 68)
        Me.panelDesktopPane.Name = "panelDesktopPane"
        Me.panelDesktopPane.Size = New System.Drawing.Size(1326, 734)
        Me.panelDesktopPane.TabIndex = 2
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.VipDesignerUM.My.Resources.Resources.VipDesigner_icon
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(1326, 734)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'PrezzoMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1512, 802)
        Me.Controls.Add(Me.panelDesktopPane)
        Me.Controls.Add(Me.panelTitleBar)
        Me.Controls.Add(Me.panelMenu)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PrezzoMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PrezzoMenu"
        Me.panelMenu.ResumeLayout(False)
        Me.panelTitleBar.ResumeLayout(False)
        Me.panelTitleBar.PerformLayout()
        Me.panelDesktopPane.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panelMenu As Panel
    Friend WithEvents panelLogo As Panel
    Friend WithEvents panelTitleBar As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents btnMaximize As Button
    Friend WithEvents btnMinimize As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents panelDesktopPane As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnPrezzo As Guna.UI2.WinForms.Guna2Button
End Class
