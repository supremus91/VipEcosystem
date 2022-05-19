<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PanResume
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.PieChart3 = New LiveCharts.WinForms.PieChart()
        Me.PieChart2 = New LiveCharts.WinForms.PieChart()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PieChart1 = New LiveCharts.WinForms.PieChart()
        Me.ElementHost1 = New System.Windows.Forms.Integration.ElementHost()
        Me.CartesianChart1 = New LiveCharts.Wpf.CartesianChart()
        Me.ElementHost2 = New System.Windows.Forms.Integration.ElementHost()
        Me.CartesianChart2 = New LiveCharts.Wpf.CartesianChart()
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Guna2Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.PieChart3)
        Me.Panel1.Controls.Add(Me.PieChart2)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.PieChart1)
        Me.Panel1.Location = New System.Drawing.Point(12, 167)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(782, 701)
        Me.Panel1.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Franklin Gothic Medium Cond", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label12.Location = New System.Drawing.Point(576, 407)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(140, 34)
        Me.Label12.TabIndex = 26
        Me.Label12.Text = "Applicazione"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Franklin Gothic Medium Cond", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label11.Location = New System.Drawing.Point(584, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(109, 34)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "Ambiente"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Franklin Gothic Medium Cond", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label10.Location = New System.Drawing.Point(177, 100)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(166, 34)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "Tipo Ventilatore"
        '
        'PieChart3
        '
        Me.PieChart3.Location = New System.Drawing.Point(505, 440)
        Me.PieChart3.Name = "PieChart3"
        Me.PieChart3.Size = New System.Drawing.Size(259, 224)
        Me.PieChart3.TabIndex = 3
        Me.PieChart3.Text = "PieChart3"
        '
        'PieChart2
        '
        Me.PieChart2.Location = New System.Drawing.Point(505, 52)
        Me.PieChart2.Name = "PieChart2"
        Me.PieChart2.Size = New System.Drawing.Size(259, 224)
        Me.PieChart2.TabIndex = 2
        Me.PieChart2.Text = "PieChart2"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 70.0!)
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label7.Location = New System.Drawing.Point(121, 313)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(201, 107)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "100"
        '
        'PieChart1
        '
        Me.PieChart1.Location = New System.Drawing.Point(22, 76)
        Me.PieChart1.Name = "PieChart1"
        Me.PieChart1.Size = New System.Drawing.Size(459, 575)
        Me.PieChart1.TabIndex = 0
        Me.PieChart1.Text = "PieChart1"
        '
        'ElementHost1
        '
        Me.ElementHost1.Location = New System.Drawing.Point(809, 167)
        Me.ElementHost1.Name = "ElementHost1"
        Me.ElementHost1.Size = New System.Drawing.Size(810, 348)
        Me.ElementHost1.TabIndex = 1
        Me.ElementHost1.Text = "ElementHost1"
        Me.ElementHost1.Child = Me.CartesianChart1
        '
        'ElementHost2
        '
        Me.ElementHost2.Location = New System.Drawing.Point(809, 519)
        Me.ElementHost2.Name = "ElementHost2"
        Me.ElementHost2.Size = New System.Drawing.Size(810, 348)
        Me.ElementHost2.TabIndex = 2
        Me.ElementHost2.Text = "ElementHost2"
        Me.ElementHost2.Child = Me.CartesianChart2
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.Controls.Add(Me.Label6)
        Me.Guna2Panel1.Controls.Add(Me.Label3)
        Me.Guna2Panel1.Controls.Add(Me.Label4)
        Me.Guna2Panel1.Controls.Add(Me.Label2)
        Me.Guna2Panel1.Controls.Add(Me.Label1)
        Me.Guna2Panel1.CustomBorderColor = System.Drawing.Color.Silver
        Me.Guna2Panel1.CustomBorderThickness = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.Guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Guna2Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.ShadowDecoration.Parent = Me.Guna2Panel1
        Me.Guna2Panel1.Size = New System.Drawing.Size(1924, 100)
        Me.Guna2Panel1.TabIndex = 22
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Franklin Gothic Medium Cond", 30.0!)
        Me.Label6.Location = New System.Drawing.Point(26, 2)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(229, 47)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Gennaio 2020"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium Cond", 20.0!)
        Me.Label3.Location = New System.Drawing.Point(622, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(239, 34)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Progettazioni realizzate"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.Green
        Me.Label4.Location = New System.Drawing.Point(509, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 46)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "+ 76"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium Cond", 20.0!)
        Me.Label2.Location = New System.Drawing.Point(325, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 34)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Nuovi iscritti"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(236, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 46)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "+ 76"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Franklin Gothic Medium Cond", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label8.Location = New System.Drawing.Point(1150, 171)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(142, 34)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Progettazioni"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Franklin Gothic Medium Cond", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label9.Location = New System.Drawing.Point(1172, 523)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 34)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "Iscritti"
        '
        'PanResume
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1924, 1041)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.Controls.Add(Me.ElementHost2)
        Me.Controls.Add(Me.ElementHost1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "PanResume"
        Me.Text = "Statistica totale"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Guna2Panel1.ResumeLayout(False)
        Me.Guna2Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents PieChart1 As LiveCharts.WinForms.PieChart
    Friend WithEvents ElementHost1 As Integration.ElementHost
    Friend WithEvents ElementHost2 As Integration.ElementHost
    Friend CartesianChart1 As LiveCharts.Wpf.CartesianChart
    Friend CartesianChart2 As LiveCharts.Wpf.CartesianChart
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents PieChart3 As LiveCharts.WinForms.PieChart
    Friend WithEvents PieChart2 As LiveCharts.WinForms.PieChart
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
End Class
