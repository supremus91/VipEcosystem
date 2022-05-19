<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CertificateMenu
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CertificateMenu))
        Me.Guna2GroupBox3 = New Guna.UI2.WinForms.Guna2GroupBox()
        Me.CheckedListBox3 = New System.Windows.Forms.CheckedListBox()
        Me.Guna2GroupBox2 = New Guna.UI2.WinForms.Guna2GroupBox()
        Me.CheckedListBox2 = New System.Windows.Forms.CheckedListBox()
        Me.Guna2GroupBox1 = New Guna.UI2.WinForms.Guna2GroupBox()
        Me.Guna2CircleQ1 = New Guna.UI2.WinForms.Guna2CircleButton()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.Guna2GroupBox4 = New Guna.UI2.WinForms.Guna2GroupBox()
        Me.CheckedListBox4 = New System.Windows.Forms.CheckedListBox()
        Me.Guna2GroupBox3.SuspendLayout()
        Me.Guna2GroupBox2.SuspendLayout()
        Me.Guna2GroupBox1.SuspendLayout()
        Me.Guna2GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2GroupBox3
        '
        Me.Guna2GroupBox3.Controls.Add(Me.CheckedListBox3)
        Me.Guna2GroupBox3.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2GroupBox3.ForeColor = System.Drawing.Color.Black
        Me.Guna2GroupBox3.Location = New System.Drawing.Point(645, 1)
        Me.Guna2GroupBox3.Name = "Guna2GroupBox3"
        Me.Guna2GroupBox3.ShadowDecoration.Parent = Me.Guna2GroupBox3
        Me.Guna2GroupBox3.Size = New System.Drawing.Size(195, 449)
        Me.Guna2GroupBox3.TabIndex = 8
        Me.Guna2GroupBox3.Text = "Hazardous Application"
        '
        'CheckedListBox3
        '
        Me.CheckedListBox3.CheckOnClick = True
        Me.CheckedListBox3.FormattingEnabled = True
        Me.CheckedListBox3.Items.AddRange(New Object() {"[TR-CU]", "[IECEX]", "[ATEX]", "[NEC 500]"})
        Me.CheckedListBox3.Location = New System.Drawing.Point(0, 40)
        Me.CheckedListBox3.Name = "CheckedListBox3"
        Me.CheckedListBox3.Size = New System.Drawing.Size(192, 422)
        Me.CheckedListBox3.TabIndex = 2
        '
        'Guna2GroupBox2
        '
        Me.Guna2GroupBox2.Controls.Add(Me.CheckedListBox2)
        Me.Guna2GroupBox2.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.Guna2GroupBox2.Location = New System.Drawing.Point(320, 1)
        Me.Guna2GroupBox2.Name = "Guna2GroupBox2"
        Me.Guna2GroupBox2.ShadowDecoration.Parent = Me.Guna2GroupBox2
        Me.Guna2GroupBox2.Size = New System.Drawing.Size(324, 449)
        Me.Guna2GroupBox2.TabIndex = 7
        Me.Guna2GroupBox2.Text = "Naval Certificates"
        '
        'CheckedListBox2
        '
        Me.CheckedListBox2.CheckOnClick = True
        Me.CheckedListBox2.FormattingEnabled = True
        Me.CheckedListBox2.Items.AddRange(New Object() {"American Bureau of Shipping [ABS]", "Bureau Veritas,France [BV]", "Det Norrske Verita, Norway [DNV]", "Germanischer Lloyd, Germany [GL]", "Korean Register of Shipping [KR]", "Lloyds Register of Shipping [LR]", "Registro Italiano Navale [RINA]", "Russian Maritime Register of Shipping [RS]"})
        Me.CheckedListBox2.Location = New System.Drawing.Point(0, 40)
        Me.CheckedListBox2.Name = "CheckedListBox2"
        Me.CheckedListBox2.Size = New System.Drawing.Size(324, 422)
        Me.CheckedListBox2.TabIndex = 1
        '
        'Guna2GroupBox1
        '
        Me.Guna2GroupBox1.Controls.Add(Me.Guna2CircleQ1)
        Me.Guna2GroupBox1.Controls.Add(Me.CheckedListBox1)
        Me.Guna2GroupBox1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.Guna2GroupBox1.Location = New System.Drawing.Point(1, 1)
        Me.Guna2GroupBox1.Name = "Guna2GroupBox1"
        Me.Guna2GroupBox1.ShadowDecoration.Parent = Me.Guna2GroupBox1
        Me.Guna2GroupBox1.Size = New System.Drawing.Size(318, 449)
        Me.Guna2GroupBox1.TabIndex = 6
        Me.Guna2GroupBox1.Text = "Meps "
        '
        'Guna2CircleQ1
        '
        Me.Guna2CircleQ1.Animated = True
        Me.Guna2CircleQ1.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.Guna2CircleQ1.BackgroundImage = Global.VipDesignerUM.My.Resources.Resources.Question1
        Me.Guna2CircleQ1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Guna2CircleQ1.CheckedState.Parent = Me.Guna2CircleQ1
        Me.Guna2CircleQ1.CustomImages.Parent = Me.Guna2CircleQ1
        Me.Guna2CircleQ1.FillColor = System.Drawing.Color.Transparent
        Me.Guna2CircleQ1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2CircleQ1.ForeColor = System.Drawing.Color.White
        Me.Guna2CircleQ1.HoverState.Parent = Me.Guna2CircleQ1
        Me.Guna2CircleQ1.ImageOffset = New System.Drawing.Point(0, 10)
        Me.Guna2CircleQ1.ImageSize = New System.Drawing.Size(15, 15)
        Me.Guna2CircleQ1.Location = New System.Drawing.Point(51, 12)
        Me.Guna2CircleQ1.Name = "Guna2CircleQ1"
        Me.Guna2CircleQ1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.Guna2CircleQ1.ShadowDecoration.Parent = Me.Guna2CircleQ1
        Me.Guna2CircleQ1.Size = New System.Drawing.Size(15, 15)
        Me.Guna2CircleQ1.TabIndex = 33
        Me.Guna2CircleQ1.Text = "Guna2CircleButton1"
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckedListBox1.CheckOnClick = True
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Items.AddRange(New Object() {"Argentina [IRAM 62405:2012]", "Brazil [ABNT NBR 17094 - 1]", "Chile [NCh 3086 of 2008]", "Colombia [RETIQ 2015]", "Ecuador [RTE INEN 145]", "Peru [Law 27345 - 2000]", "North America [ULCSA / NEMA Premium]", "UK [UKCA)]", "Australia [GEMS Act of 2019]", "Saudi Rabia [SASO 2893:2018]", "India [IS 12615:2018]", "South Korea [KS C IEC 60034]", "China [GB 30253-2013]", "China [GB 18613-2020]", "North America [UL]"})
        Me.CheckedListBox1.Location = New System.Drawing.Point(3, 40)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(315, 422)
        Me.CheckedListBox1.TabIndex = 0
        '
        'Guna2GroupBox4
        '
        Me.Guna2GroupBox4.Controls.Add(Me.CheckedListBox4)
        Me.Guna2GroupBox4.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Guna2GroupBox4.ForeColor = System.Drawing.Color.Black
        Me.Guna2GroupBox4.Location = New System.Drawing.Point(841, 1)
        Me.Guna2GroupBox4.Name = "Guna2GroupBox4"
        Me.Guna2GroupBox4.ShadowDecoration.Parent = Me.Guna2GroupBox4
        Me.Guna2GroupBox4.Size = New System.Drawing.Size(195, 449)
        Me.Guna2GroupBox4.TabIndex = 9
        Me.Guna2GroupBox4.Text = "Safety regulations"
        '
        'CheckedListBox4
        '
        Me.CheckedListBox4.CheckOnClick = True
        Me.CheckedListBox4.FormattingEnabled = True
        Me.CheckedListBox4.Items.AddRange(New Object() {"Europe [CE]", "China [CCC]", "Russia [EAC]"})
        Me.CheckedListBox4.Location = New System.Drawing.Point(0, 40)
        Me.CheckedListBox4.Name = "CheckedListBox4"
        Me.CheckedListBox4.Size = New System.Drawing.Size(192, 422)
        Me.CheckedListBox4.TabIndex = 2
        '
        'CertificateMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1037, 453)
        Me.Controls.Add(Me.Guna2GroupBox4)
        Me.Controls.Add(Me.Guna2GroupBox3)
        Me.Controls.Add(Me.Guna2GroupBox2)
        Me.Controls.Add(Me.Guna2GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CertificateMenu"
        Me.Text = "CertificateMenu"
        Me.Guna2GroupBox3.ResumeLayout(False)
        Me.Guna2GroupBox2.ResumeLayout(False)
        Me.Guna2GroupBox1.ResumeLayout(False)
        Me.Guna2GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2GroupBox3 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents CheckedListBox3 As CheckedListBox
    Friend WithEvents Guna2GroupBox2 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents CheckedListBox2 As CheckedListBox
    Friend WithEvents Guna2GroupBox1 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents Guna2CircleQ1 As Guna.UI2.WinForms.Guna2CircleButton
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents Guna2GroupBox4 As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents CheckedListBox4 As CheckedListBox
End Class
