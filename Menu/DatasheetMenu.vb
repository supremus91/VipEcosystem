Imports System.Runtime.InteropServices
Imports System.Threading
Imports System
Imports System.IO
Imports System.Text
Imports System.Net
Imports System.Security.AccessControl
Imports System.Net.Mail
Imports System.IO.Ports
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Net.Sockets
Imports Microsoft.Office.Interop
Imports System.Globalization
Imports System.Windows.Forms.DataVisualization.Charting
Imports DevExpress.XtraReports.UI
Imports System.Collections
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Drawing
Imports MaterialSkin.Animations
Imports MaterialSkin.Controls
Imports MaterialSkin

Public Class DatasheetMenu

    Private Sub Special_Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Me.Location = New System.Drawing.Point(50, 50)

        form_datasheet = New Datasheet
        OpenChildForm(form_datasheet, sender)


        If mod_PJ_DS = 0 Then
            lblTitle.Text = "DataSheet"
        Else
            lblTitle.Text = PJ_ref_star
        End If


        Me.Size = New System.Drawing.Size(1850, 1030)


    End Sub



    'FIELDS'
    Private currentButton As Button
    Private random As Random
    Private tempIndex As Integer
    Private activeForm As Form
    'CONSTRUCTOR'
    Public Sub New()
        InitializeComponent()
        random = New Random()
        currentButton = New Button()
        'Me.btnCloseChildForm.Visible = False
        Me.Text = String.Empty
        Me.ControlBox = False
        Me.MaximizedBounds = Screen.FromHandle(Me.Handle).WorkingArea
    End Sub
    'METHODS'
    Private Function SelectThemeColor() As Color
        Dim index As Integer = random.[Next](ThemeColor.ColorList.Count)
        While tempIndex = index
            index = random.[Next](ThemeColor.ColorList.Count)
        End While
        tempIndex = index
        Dim color As String = ThemeColor.ColorList(index)
        Return ColorTranslator.FromHtml(color)
    End Function
    Private Sub ActivateButton(btnSender As Object)
        If btnSender IsNot Nothing Then
            'If currentButton.Name <> CType(btnSender, Button).Name Then
            '    DisableButton()
            '    Dim color As Color = SelectThemeColor()
            '    currentButton = CType(btnSender, Button)
            '    currentButton.BackColor = Color.WhiteSmoke
            '    currentButton.ForeColor = Color.Black
            '    currentButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte((0))))
            '    'panelTitleBar.BackColor = Color.FromArgb(0, 69, 131)
            '    'panelLogo.BackColor = Color.WhiteSmoke
            '    ThemeColor.PrimaryColor = Color.WhiteSmoke
            '    ThemeColor.SecondaryColor = Color.WhiteSmoke
            '    btnCloseChildForm.Visible = True
            'End If
        End If
    End Sub
    Private Sub DisableButton()
        For Each previousBtn As Control In panelMenu.Controls
            If previousBtn.[GetType]() = GetType(Button) Then
                previousBtn.BackColor = Color.FromArgb(213, 220, 248) '(51, 51, 76)
                previousBtn.ForeColor = Color.Black
                previousBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte((0))))
            End If
        Next
    End Sub
    Private Sub OpenChildForm(childForm As Form, btnSender As Object)

        Try
            If activeForm IsNot Nothing Then activeForm.Close()
            ActivateButton(btnSender)
            activeForm = childForm
            childForm.TopLevel = False
            childForm.FormBorderStyle = FormBorderStyle.None
            childForm.Dock = DockStyle.Fill
            Me.panelDesktopPane.Controls.Add(childForm)
            Me.panelDesktopPane.Tag = childForm
            childForm.BringToFront()
            childForm.Show()
        Catch ex As Exception

        End Try


    End Sub
    'Private Sub btnCloseChildForm_Click(sender As Object, e As EventArgs) Handles btnCloseChildForm.Click
    '    If (Not (activeForm) Is Nothing) Then
    '        activeForm.Close()
    '    End If
    '    Reset()
    'End Sub
    Private Sub Reset()
        DisableButton()
        lblTitle.Text = "Special Settings"
        'panelTitleBar.BackColor = Color.FromArgb(0, 69, 131) '(0, 150, 136)
        panelLogo.BackColor = Color.FromArgb(0, 69, 131)  '(39, 39, 58)
        currentButton = New Button()
        'btnCloseChildForm.Visible = False
    End Sub
    'DRAG FORM'
    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture()
    End Sub
    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As Integer)
    End Sub
    Private Sub panelTitleBar_MouseDown(sender As Object, e As MouseEventArgs) Handles panelTitleBar.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        memory_clean()
        Me.Close()
    End Sub

    Public Sub memory_clean()

        For Each item As Control In form_datasheet.Guna2GroupBox4.Controls
            'Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

            If item.GetType Is GetType(System.Windows.Forms.GroupBox) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                For Each item1 As Control In gb.Controls

                    If item1.GetType Is GetType(System.Windows.Forms.CheckBox) Then 'cerco le checkbox dentro la groupbox identificata

                        Dim chb As System.Windows.Forms.CheckBox = DirectCast(item1, System.Windows.Forms.CheckBox)

                        Dim imm_name As String = chb.Name.Substring(chb.Name.IndexOf("_") + 1, chb.Name.Length - chb.Name.IndexOf("_") - 1)

                        chb.BackgroundImage.Dispose()
                        chb.Dispose()
                        gb.Dispose()
                    End If

                Next

            End If
        Next

        For Each item As Control In form_datasheet.ParGen.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cb.Dispose()

            End If
        Next

        For Each item As Control In form_datasheet.ParGen.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)


                tb.Dispose()


            End If
        Next


        For Each item As Control In form_datasheet.ParGen.Controls
            If item.GetType Is GetType(Label) Then
                Dim lb As Label = DirectCast(item, Label)


                lb.Dispose()


            End If
        Next

        form_datasheet.Chart18.Dispose()
        form_datasheet.warning_ERP.Image.Dispose()
        form_datasheet.warning_mot.Image.Dispose()
        form_datasheet.warning_des.Image.Dispose()
        form_datasheet.warning_ERP.Dispose()
        form_datasheet.warning_mot.Dispose()
        form_datasheet.warning_des.Dispose()
        form_datasheet.Salva_DB.Dispose()
        form_datasheet.Stampa_DS.Dispose()
        form_datasheet.Apri_EXC.Dispose()


        'chiusura degli elementi del form
        PictureBox1.Dispose()
        panelDesktopPane.Dispose()
        panelMenu.Dispose()
        btnDataSheet.Dispose()
        panelLogo.Dispose()
        panelTitleBar.Dispose()





    End Sub



    Private Sub btnDataSheet_Click(sender As Object, e As EventArgs) Handles btnDataSheet.Click

        form_datasheet = New Datasheet
        OpenChildForm(form_datasheet, sender)



        lblTitle.Text = "Datasheets"
        btnDataSheet.Visible = True

        Me.Size = New System.Drawing.Size(1850, 1030)

    End Sub


    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnMaximize_Click(sender As Object, e As EventArgs) Handles btnMaximize.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub


    Private Sub lblTitle_Click(sender As Object, e As EventArgs) Handles lblTitle.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub


End Class