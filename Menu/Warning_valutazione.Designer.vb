<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Warning_valutazione
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Warning_valutazione))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Guna2RatingStar1 = New Guna.UI2.WinForms.Guna2RatingStar()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Guna2Button1 = New Guna.UI2.WinForms.Guna2Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(199, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(229, 21)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Sei soddisfatto dello sviluppo?"
        '
        'Guna2RatingStar1
        '
        Me.Guna2RatingStar1.Location = New System.Drawing.Point(186, 54)
        Me.Guna2RatingStar1.Name = "Guna2RatingStar1"
        Me.Guna2RatingStar1.RatingColor = System.Drawing.Color.Gold
        Me.Guna2RatingStar1.Size = New System.Drawing.Size(254, 54)
        Me.Guna2RatingStar1.TabIndex = 3
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.VipDesignerUM.My.Resources.Resources.smile
        Me.PictureBox1.Location = New System.Drawing.Point(-86, -50)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(374, 279)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'Guna2Button1
        '
        Me.Guna2Button1.BorderThickness = 1
        Me.Guna2Button1.CheckedState.Parent = Me.Guna2Button1
        Me.Guna2Button1.CustomImages.Parent = Me.Guna2Button1
        Me.Guna2Button1.FillColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Guna2Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2Button1.ForeColor = System.Drawing.Color.Black
        Me.Guna2Button1.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(253, Byte), Integer))
        Me.Guna2Button1.HoverState.Parent = Me.Guna2Button1
        Me.Guna2Button1.Location = New System.Drawing.Point(205, 137)
        Me.Guna2Button1.Name = "Guna2Button1"
        Me.Guna2Button1.ShadowDecoration.Parent = Me.Guna2Button1
        Me.Guna2Button1.Size = New System.Drawing.Size(201, 27)
        Me.Guna2Button1.TabIndex = 4
        Me.Guna2Button1.Text = "Valuta"
        Me.Guna2Button1.Visible = False
        '
        'Warning_valutazione
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(493, 182)
        Me.Controls.Add(Me.Guna2Button1)
        Me.Controls.Add(Me.Guna2RatingStar1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Warning_valutazione"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rating"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Guna2RatingStar1 As Guna.UI2.WinForms.Guna2RatingStar
    Friend WithEvents Guna2Button1 As Guna.UI2.WinForms.Guna2Button
End Class

