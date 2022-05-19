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

Public Class FolderViewer

    Private Sub FolderViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblTitle.Text = NomeFolder_selezionata

        search_folder()


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
        Me.Close()
    End Sub


    Private Sub btnArchivio_Click(sender As Object, e As EventArgs) Handles btnArchivio.Click

        search_folder()

    End Sub



    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnMaximize_Click(sender As Object, e As EventArgs) Handles btnMaximize.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub





    Public Sub search_folder()


        Dim folders() As String = IO.Directory.GetDirectories(folders_directoryDS & "\" & NomeFolder_selezionata)
        Dim sommaFolder As Integer

        Guna2DataGridView1.Rows.Clear()

        For Each folder As String In folders


            Dim folder_name As String = Path.GetFileName(folder)

            Guna2DataGridView1.Rows.Add()

            Guna2DataGridView1.Rows(sommaFolder).Cells(0).Value = folder_name



            Dim files() As String = IO.Directory.GetFiles(folders_directoryDS & "\" & NomeFolder_selezionata & "\" & folder_name)



            Dim trovato_DisegnoPDF As Integer = 0
            Dim trovato_STEP As Integer = 0
            Dim trovato_DWG As Integer = 0
            Dim trovato_Ds As Integer = 0



            'porto subito pari a rosso lo stato dei disegni
            For i = 1 To 4
                Guna2DataGridView1.Rows(sommaFolder).Cells(i).Value = My.Resources.Resources.Red_L
            Next



            For Each file As String In files

                    Dim file_name As String = Path.GetFileName(file)
                    Dim file_estensione As String = Path.GetExtension(file)

                    If (file_name.Substring(0, 7) = "Drawing") Then
                        Guna2DataGridView1.Rows(sommaFolder).Cells(1).Value = My.Resources.Resources.Green_L
                        trovato_DisegnoPDF = 1
                    ElseIf (trovato_DisegnoPDF = 0) Then
                        Guna2DataGridView1.Rows(sommaFolder).Cells(1).Value = My.Resources.Resources.Red_L
                    End If


                    If file_estensione = ".STEP" Then
                        Guna2DataGridView1.Rows(sommaFolder).Cells(2).Value = My.Resources.Resources.Green_L
                        trovato_STEP = 1
                    ElseIf (trovato_STEP = 0) Then
                        Guna2DataGridView1.Rows(sommaFolder).Cells(2).Value = My.Resources.Resources.Red_L
                    End If


                    If file_estensione = ".DWG" Then
                        Guna2DataGridView1.Rows(sommaFolder).Cells(3).Value = My.Resources.Resources.Green_L
                        trovato_DWG = 1
                    ElseIf (trovato_DWG = 0) Then
                        Guna2DataGridView1.Rows(sommaFolder).Cells(3).Value = My.Resources.Resources.Red_L
                    End If

                    If file_estensione = ".pdf" And (file_name.Substring(0, 6) <> "Drawing") Then
                        Guna2DataGridView1.Rows(sommaFolder).Cells(4).Value = My.Resources.Resources.Green_L
                        trovato_Ds = 1
                    ElseIf (trovato_Ds = 0) Then
                        Guna2DataGridView1.Rows(sommaFolder).Cells(4).Value = My.Resources.Resources.Red_L
                    End If


                Next



                sommaFolder = sommaFolder + 1

            Next



    End Sub






    Private Sub Guna2DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellDoubleClick


        Process.Start("explorer.exe", folders_directoryDS & "\" & NomeFolder_selezionata & "\" & Guna2DataGridView1.Rows(Guna2DataGridView1.CurrentRow.Index).Cells(0).Value)

    End Sub


End Class