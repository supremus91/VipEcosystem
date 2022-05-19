Imports System.Runtime.InteropServices
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
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




Public Class Form1

    'FIELDS'
    Private currentButton As Button
    Private random As Random
    Private tempIndex As Integer
    Private activeForm As Form


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'scarico subito il file contenente tutte le statistiche
        Try
            Download_all_data1()
        Catch ex As Exception

        End Try


        btnProgette.PerformClick()


        Select Case nome_macchina
            Case "Andrea"
                userNum = 2
            Case "Stefano"
                userNum = 3
            Case "Paolo"
                userNum = 4
            Case "Roberto"
                userNum = 5
            Case "Fausto"
                userNum = 6
            Case "Lorenzo"
                userNum = 7
            Case "Alberto"
                userNum = 8
            Case "Alessandro"
                userNum = 9
            Case "Riccardo"
                userNum = 10
            Case "Rita"
                userNum = 11
        End Select


        data = Now.ToShortDateString
        ora = Now.ToShortTimeString


    End Sub



    'CONSTRUCTOR'
    Public Sub New()
        InitializeComponent()
        random = New Random()
        currentButton = New Button()
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
            If currentButton.Name <> CType(btnSender, Button).Name Then
                DisableButton()
                Dim color As Color = SelectThemeColor()
                currentButton = CType(btnSender, Button)
                currentButton.BackColor = color
                currentButton.ForeColor = Color.White
                currentButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte((0))))
                panelTitleBar.BackColor = color
                panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3)
                ThemeColor.PrimaryColor = color
                ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3)

            End If
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

        For Each previousBtn As Control In Panel1.Controls
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
        'lblTitle.Text = childForm.Text
    End Sub
    Private Sub btnCloseChildForm_Click(sender As Object, e As EventArgs)
        If (Not (activeForm) Is Nothing) Then
            activeForm.Close()
        End If
        Reset()
    End Sub
    Private Sub Reset()
        DisableButton()
        lblTitle.Text = "HOME"
        panelTitleBar.BackColor = Color.FromArgb(0, 69, 131) '(0, 150, 136)
        panelLogo.BackColor = Color.FromArgb(20, 45, 150)  '(39, 39, 58)
        currentButton = New Button()

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
    'EVENTS
    Private Sub btnProducts_Click(sender As Object, e As EventArgs) Handles BtnIscritti.Click 'iscritti
        OpenChildForm(New PanLista(), sender)
        btnParametri.Visible = False
    End Sub
    Private Sub btnOrders_Click(sender As Object, e As EventArgs) Handles btnParametri.Click

        lblTitle.Text = PJ_ref_star
        btnProgette.Image = My.Resources.exit1
        btnProgette.Text = "    Esci dal progetto"

        'OpenChildForm(New FormParametri(), sender)
        form_parametri = New FormParametri
        OpenChildForm(form_parametri, sender)

    End Sub
    Private Sub btnCustomers_Click(sender As Object, e As EventArgs) Handles btnDesigner.Click
        'OpenChildForm(New PanUfficio(), sender)
    End Sub
    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles BtnStat.Click 'statistiche totali

        OpenChildForm(New PanLista(), sender)
        btnParametri.Visible = False

        OpenChildForm(New PanResume(), sender)
        btnParametri.Visible = False

    End Sub
    Private Sub btnProgette_Click(sender As Object, e As EventArgs) Handles btnProgette.Click


        If lockPJ = 0 Or lockPJ = userNum Then
            save_manager()
        End If


        lblTitle.Text = "HOME"
        btnProgette.Image = My.Resources.Pr_list
        btnProgette.Text = "  PJ"
        Try
            ArchivioMenu.Close()
            DataBaseMenu.Close()
        Catch ex As Exception

        End Try

        mod_PJ_DS = 0

        'mod_archivio = 1
        Try
            If lockPJ = 0 Or lockPJ = userNum Then
                ConnStateUser1(0)
            End If
        Catch ex As Exception

        End Try

        numero_progetti = 0

        btnParametri.Visible = False
        OpenChildForm(New Progettazioni(), sender)

    End Sub


    'CLOSE, MAXIMIZE, MINIMIZE FORM MAIN'
    Private Sub bntMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
    Private Sub btnMaximize_Click(sender As Object, e As EventArgs) Handles btnMaximize.Click
        If (WindowState = FormWindowState.Normal) Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

        If lockPJ = 0 Or lockPJ = userNum Then
            save_manager()
        End If


        Try
            If lockPJ = 0 Or lockPJ = userNum Then
                ConnStateUser1(0)
            End If
        Catch ex As Exception

        End Try

        Try
            Application.Exit()
        Catch ex As Exception

        End Try


    End Sub


    Public Sub save_manager()


        If mod_PJ_DS = 1 Then
            Yes_No_Warning = 0
            Warning.Label1.Text = "       Do you want to save the project?"
            If Warning.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
                'attendo la risposta della box
            End If

            If Yes_No_Warning = 1 Then


                Try
                    form_parametri.Guna2Button2.PerformClick()
                Catch ex As Exception

                End Try

            End If

        End If


    End Sub



    Private Sub lblTitle_Click(sender As Object, e As EventArgs) Handles lblTitle.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub


    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean


        'If keyData = Keys.Back Then

        '    btnProgette.PerformClick()

        'End If


    End Function



    Private Sub Guna2CircleButton2_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton2.Click
        Special_Settings.Show()
    End Sub

    Private Sub Guna2CircleButton3_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton3.Click
        Special_Settings.Show()
    End Sub

    Private Sub Guna2CircleButton4_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton4.Click

    End Sub

    Private Sub ArchivioPJ_Click(sender As Object, e As EventArgs) Handles ArchivioPJ.Click


        Try
            ArchivioMenu.Close()
        Catch ex As Exception

        End Try

        load_var = 0

        ArchivioMenu.Show()


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnCatalogo.Click
        OpenChildForm(New Catalogo(), sender)
    End Sub

    Private Sub btnDeveloper_Click(sender As Object, e As EventArgs) Handles btnDeveloper.Click
        OpenChildForm(New Sviluppi(), sender)
    End Sub



    Private Sub btn_VipDesigner_Main_Click(sender As Object, e As EventArgs) Handles btn_VipDesigner_Main.Click

        'Codice di salvataggio progetto
        Dim str_save As String = ""


        Dim saveFileDialog1 As SaveFileDialog = New SaveFileDialog()

        saveFileDialog1.Filter = "vip files (*.vip)|*.vip*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True



        saveFileDialog1.FileName = "File_appoggio.vip"

        saveFileDialog1.DefaultExt = "vip"
        saveFileDialog1.AddExtension = True

        Dim nome_file_sv As String = "File_appoggio.vip"
        Dim path_save As String = Folder_PC_storage & "\" & nome_file_sv

        File.WriteAllText(path_save, str_save)



        'Apre il progetto appena salvato
        Dim proc As New System.Diagnostics.Process()
        proc = Process.Start(path_save, "")

        'Process.Start("C:\Users\" & nome_macchina & "\Desktop\VipDesigner_interno")
    End Sub

    Private Sub btn_SW_Click(sender As Object, e As EventArgs) Handles btn_SW.Click
        OpenChildForm(New SW_interface(), sender)
    End Sub

    Private Sub btnDatabase_Click(sender As Object, e As EventArgs) Handles btnDatabase.Click

        DB_pasticci_on = 0

        Try
            DataBaseMenu.Close()
        Catch ex As Exception

        End Try

        load_var = 0

        DataBaseMenu.Show()


    End Sub

End Class