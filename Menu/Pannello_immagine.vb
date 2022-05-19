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
Imports System.Data.OleDb


Public Class pannello_immagine


    Private Sub Special_Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Location = New System.Drawing.Point(50, 50)

        'form_Archivio = New ArchivioDS
        'OpenChildForm(form_Archivio, sender)



        If mod_PJ_DS = 0 Then
            lblTitle.Text = "Allegato"
        Else
            lblTitle.Text = PJ_ref_star
        End If


        Me.Size = New System.Drawing.Size(1850, 1030)


        If mod_load_imm = 0 Then
            scegli_immagine()
            aggiorna_DB_sviluppo()
        Else

            open_image_sviluppi()
            mod_load_imm = 1
        End If
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

        If mod_archivio = 1 Then
            lblTitle.Text = "Allegato"
        Else
            lblTitle.Text = PJ_ref_star
        End If

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

        Form1.btnDeveloper.PerformClick()
        Me.Close()
    End Sub


    'Private Sub btnArchivio_Click(sender As Object, e As EventArgs) Handles btnArchivio.Click

    '    form_Archivio = New ArchivioDS
    '    OpenChildForm(form_Archivio, sender)

    '    If mod_PJ_DS = 0 Then
    '        lblTitle.Text = "Archivio"
    '    Else
    '        lblTitle.Text = PJ_ref_star
    '    End If

    '    Me.Size = New System.Drawing.Size(1850, 1030)

    'End Sub



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



    Dim conn As OleDbConnection = New OleDbConnection(constring)
    Dim cmd As OleDbCommand
    Dim sql As String
    Dim da As New OleDb.OleDbDataAdapter

    Dim arrImage() As Byte
    Dim mstream As New System.IO.MemoryStream()


    Public Sub executeQuery(sql As String)
        Try
            Dim arrImage() As Byte
            Dim mstream As New System.IO.MemoryStream()

            'SPECIFIES THE FILE FORMAT OF THE IMAGE
            PictureBox1.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)

            'RETURNS THE ARRAY OF UNSIGNED BYTES FROM WHICH THIS STREAM WAS CREATED
            arrImage = mstream.GetBuffer()

            'GET THE SIZE OF THE STREAM IN BYTES
            Dim FileSize As UInt32
            FileSize = mstream.Length
            'CLOSES THE CURRENT STREAM AND RELEASE ANY RESOURCES ASSOCIATED WITH THE CURRENT STREAM
            mstream.Close()

            conn.Open()

            cmd = New OleDbCommand
            With cmd
                .Connection = conn
                .CommandText = sql
                .Parameters.AddWithValue("@tbx_immSV", arrImage)
                .ExecuteNonQuery()
            End With

        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            conn.Close()

        End Try
    End Sub


    Public Sub aggiorna_DB_sviluppo()

        sql = "UPDATE Sviluppo SET tbx_immSV = @tbx_immSV WHERE ID=" & ID_SV
        executeQuery(sql)

    End Sub






    Public Sub scegli_immagine()

        Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog()

        Try
            With OpenFileDialog1

                'CHECK THE SELECTED FILE IF IT EXIST OTHERWISE THE DIALOG BOX WILL DISPLAY A WARNING.
                .CheckFileExists = True

                'CHECK THE SELECTED PATH IF IT EXIST OTHERWISE THE DIALOG BOX WILL DISPLAY A WARNING.
                .CheckPathExists = True

                'GET AND SET THE DEFAULT EXTENSION
                .DefaultExt = "jpg"

                'RETURN THE FILE LINKED TO THE LNK FILE
                .DereferenceLinks = True

                'SET THE FILE NAME TO EMPTY 
                .FileName = ""

                'FILTERING THE FILES
                .Filter = "(*.jpg)|*.jpg|(*.png)|*.png|(*.jpg)|*.jpg|All files|*.*"
                'SET THIS FOR ONE FILE SELECTION ONLY.
                .Multiselect = False

                'SET THIS TO PUT THE CURRENT FOLDER BACK TO WHERE IT HAS STARTED.
                .RestoreDirectory = True

                'SET THE TITLE OF THE DIALOG BOX.
                .Title = "Select a file to open"

                'ACCEPT ONLY THE VALID WIN32 FILE NAMES.
                .ValidateNames = True

                If .ShowDialog = DialogResult.OK Then
                    Try
                        PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
                    Catch fileException As Exception
                        Throw fileException
                    End Try
                End If

            End With
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try


    End Sub

    Private Sub btnDataSheet_Click(sender As Object, e As EventArgs) Handles btnDataSheet.Click
        scegli_immagine()
        aggiorna_DB_sviluppo()
        Form1.btnDeveloper.PerformClick()
        Me.BringToFront()
    End Sub





    Public Sub open_image_sviluppi()

        sql = "Select * from Sviluppo where ID=" & ID_SV
        conn.ConnectionString = constring
        conn.Open()

        cmd = New OleDbCommand
        With cmd
            .Connection = conn
            .CommandText = sql
        End With
        Dim arrImage() As Byte
        Dim publictable As New DataTable


        Try
            da.SelectCommand = cmd
            da.Fill(publictable)
            arrImage = publictable.Rows(0).Item(11)
            Dim mstream As New System.IO.MemoryStream(arrImage)
            PictureBox1.Image = Image.FromStream(mstream)
        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally

            da.Dispose()
            conn.Close()

        End Try


    End Sub








End Class