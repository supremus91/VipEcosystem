Imports System.IO.Ports
Imports System.Threading
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Net.Sockets
Imports System.Net
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Globalization


Public Class All_project_user



    'Dim ftp As String = "ftp://vip-soft18@80.88.87.182/ADAMS/"
    'Dim user As String = "vip-soft18"
    'Dim pass As String = "adm@vip18"
    'Dim client_name As String = client_name
    'Dim w_directory As String = System.IO.Directory.GetCurrentDirectory()
    'Dim User_client As String = client_name



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AddHandler Application.ApplicationExit, AddressOf OnApplicationExit
        folder_server_find_target(sender, e)

        abilita_grid_click1 = 0
    End Sub


    Private Sub folder_server_find_target(sender As Object, e As EventArgs)

        For i = 0 To 9999

            file_name_target(i) = ""
            file_data_target(i) = ""

        Next


        ftp1 = ftp & "/Clienti_target" & "/ID" & Utente_selezionato(2) & "_" & Utente_selezionato(0) & "_" & Utente_selezionato(1)

        numero_ventilatori = 0
        Dim esiste As Integer = 1

        Dim fwr As FtpWebRequest = FtpWebRequest.Create(ftp1)
        fwr.Credentials = New NetworkCredential(user, pass)

        Try
            fwr.Method = WebRequestMethods.Ftp.ListDirectoryDetails
            Dim SR As New StreamReader(fwr.GetResponse().GetResponseStream())

            Dim str As String = SR.ReadToEnd
            Dim str2 As String = str

            '--------------------------------Ricerca del numero di file presenti sul server ftp-------------------------------------------------------
            For i = 0 To str.Length - 4


                Try

                    If str(i) & str(i + 1) & str(i + 2) = "vip" Then


                        'file_name_target(numero_ventilatori) = str.Substring(i, str.IndexOf(".vip") - i + 4)

                        'str = str.Substring(str.IndexOf(".vip") + 4, str.Length - 1 - str.IndexOf(".vip") - 4)

                        numero_ventilatori = numero_ventilatori + 1

                    End If

                Catch ex As Exception

                End Try


            Next


            Dim count_vent_shadow As Integer = 0
            Dim first_cycle As Integer = 0
            Dim add_cycle As Integer = 0
            Dim add_cycle1 As Integer = 0

            For i = 0 To str.Length - 4

                Try

                    Dim str_sel As String = str(i) & str(i + 1) & str(i + 2) & str(i + 3)




                    If str(i) = ":" Or (str_sel = DateTime.Now.Year.ToString() Or str_sel = "2021" Or str_sel = "2022" Or str_sel = "2023" Or str_sel = "2024") Then


                        If count_vent_shadow = 0 Then



                            file_name_target(count_vent_shadow) = str2.Substring(i + add_cycle + add_cycle1 + 4, str2.IndexOf(".vip") - i + 4 - add_cycle - add_cycle1 - 4)
                            file_data_target(count_vent_shadow) = str2.Substring(i - 10 + add_cycle + add_cycle1, 13)
                            str2 = str2.Substring(str2.IndexOf(".vip") + 4 - add_cycle, str2.Length - 1 - str2.IndexOf(".vip") - 4 + add_cycle + 1)

                            count_vent_shadow = count_vent_shadow + 1

                            i = 0
                            first_cycle = 1
                            If add_cycle = 2 Then

                                add_cycle1 = 2

                            End If



                        ElseIf count_vent_shadow < numero_ventilatori - 1 Then


                            file_name_target(count_vent_shadow) = str2.Substring(i + add_cycle + add_cycle1 + 6, str2.IndexOf(".vip") - i + 4 - add_cycle - add_cycle1 - 6)
                            file_data_target(count_vent_shadow) = str2.Substring(i - 7 + add_cycle + add_cycle1, 13)
                            str2 = str2.Substring(str2.IndexOf(".vip") + 4 - add_cycle, str2.Length - 1 - str2.IndexOf(".vip") - 4 + add_cycle + 1)

                            count_vent_shadow = count_vent_shadow + 1

                            i = 0
                            first_cycle = 1
                            If add_cycle = 2 Then

                                add_cycle1 = 2

                            End If

                        Else

                            file_name_target(count_vent_shadow) = str2.Substring(i + add_cycle + add_cycle1 + 6, str2.IndexOf(".vip") - i + 4 - add_cycle - add_cycle1 - 6)
                            file_data_target(count_vent_shadow) = str2.Substring(i - 7 + add_cycle + add_cycle1, 12)
                            count_vent_shadow = count_vent_shadow + 1

                            i = str.Length

                        End If


                    End If


                Catch ex As Exception







                End Try




            Next


            numero_ventilatori = numero_ventilatori - 1
            '------------------------------------------------------------------------------------------------------------------------------------


            Dim start_date As String = ""
            Dim date1 As String = ""
            Dim name As String = ""
            Dim stringa_appoggio As String = ""
            Dim memory_date As String = ""

            Guna2DataGridView1.Rows.Add(numero_ventilatori + 1)


            ''Funzione di riordino dei vettori in ordine di data
            'For i = 0 To numero_ventilatori

            '    Dim month_vect As String = file_data_target(i)(1) & file_data_target(i)(2) & file_data_target(i)(3)
            '    Dim day_vect As String = file_data_target(i)(5) & file_data_target(i)(6)


            '    Select Case month_vect

            '        Case "Jan" '31

            '            For n = 0 To 31
            '                For u = 0 To numero_ventilatori
            '                    Dim month_vect1 As String = file_data_target(u)(1) & file_data_target(u)(2) & file_data_target(u)(3)
            '                    Dim day_vect1 As String = file_data_target(u)(5) & file_data_target(u)(6)



            '                Next
            '            Next

            '        Case "Feb" '29

            '        Case "Mar" '31

            '        Case "Apr" '30

            '        Case "May" '31

            '        Case "Jun" '30

            '        Case "Jul" '31

            '        Case "Aug" '31

            '        Case "Sep" '30

            '        Case "Oct" '31

            '        Case "Nov" '30

            '        Case "Dec" '31


            '    End Select




            'Next


            'For i = 1 To 365

            '    Dim month_vect As String
            '    Dim day_vect As String

            '    If i <= 31 Then
            '        day_vect = i

            '        If day_vect < 10 Then
            '            day_vect = "0" & i
            '        End If

            '        month_vect = "Jan"



            '    ElseIf i > 31 And i <= (31 + 29) Then
            '        day_vect = i - (31)

            '        If day_vect < 10 Then
            '            day_vect = "0" & i
            '        End If

            '        month_vect = "Feb"

            '    ElseIf i > (31 + 29) And i <= (31 + 29 + 31) Then
            '        day_vect = i - (31 + 29)

            '        If day_vect < 10 Then
            '            day_vect = "0" & i
            '        End If

            '        month_vect = "Mar"

            '    ElseIf i > (31 + 29 + 31) And i <= (31 + 29 + 31 + 30) Then
            '        day_vect = i - (31 + 29 + 31)

            '        If day_vect < 10 Then
            '            day_vect = "0" & i
            '        End If

            '        month_vect = "Apr"

            '    ElseIf i > (31 + 29 + 31 + 30) And i <= (31 + 29 + 31 + 30 + 31) Then
            '        day_vect = i - (31 + 29 + 31 + 30)

            '        If day_vect < 10 Then
            '            day_vect = "0" & i
            '        End If

            '        month_vect = "May"

            '    ElseIf i > (31 + 29 + 31 + 30 + 31) And i <= (31 + 29 + 31 + 30 + 31 + 30) Then
            '        day_vect = i - (31 + 29 + 31 + 30 + 31)

            '        If day_vect < 10 Then
            '            day_vect = "0" & i
            '        End If

            '        month_vect = "Jun"

            '    ElseIf i > (31 + 29 + 31 + 30 + 31 + 30) And i <= (31 + 29 + 31 + 30 + 31 + 30 + 31) Then
            '        day_vect = i - (31 + 29 + 31 + 30 + 31 + 30)

            '        If day_vect < 10 Then
            '            day_vect = "0" & i
            '        End If

            '        month_vect = "Jul"

            '    ElseIf i > (31 + 29 + 31 + 30 + 31 + 30 + 31) And i <= (31 + 29 + 31 + 30 + 31 + 30 + 31 + 31) Then
            '        day_vect = i - (31 + 29 + 31 + 30 + 31 + 30 + 31)

            '        If day_vect < 10 Then
            '            day_vect = "0" & i
            '        End If

            '        month_vect = "Aug"

            '    ElseIf i > (31 + 29 + 31 + 30 + 31 + 30 + 31 + 31) And i <= (31 + 29 + 31 + 30 + 31 + 30 + 31 + 31 + 30) Then
            '        day_vect = i - (31 + 29 + 31 + 30 + 31 + 30 + 31 + 31)

            '        If day_vect < 10 Then
            '            day_vect = "0" & i
            '        End If

            '        month_vect = "Sep"

            '    ElseIf i > (31 + 29 + 31 + 30 + 31 + 30 + 31 + 31 + 30) And i <= (31 + 29 + 31 + 30 + 31 + 30 + 31 + 31 + 30 + 31) Then
            '        day_vect = i - (31 + 29 + 31 + 30 + 31 + 30 + 31 + 31 + 30)

            '        If day_vect < 10 Then
            '            day_vect = "0" & i
            '        End If

            '        month_vect = "Oct"

            '    ElseIf i > (31 + 29 + 31 + 30 + 31 + 30 + 31 + 31 + 30 + 31) And i <= (31 + 29 + 31 + 30 + 31 + 30 + 31 + 31 + 30 + 31 + 30) Then
            '        day_vect = i - (31 + 29 + 31 + 30 + 31 + 30 + 31 + 31 + 30 + 31)

            '        If day_vect < 10 Then
            '            day_vect = "0" & i
            '        End If

            '        month_vect = "Nov"

            '    ElseIf i > (31 + 29 + 31 + 30 + 31 + 30 + 31 + 31 + 30 + 31 + 30) And i <= (31 + 29 + 31 + 30 + 31 + 30 + 31 + 31 + 30 + 31 + 30 + 31) Then
            '        day_vect = i - (31 + 29 + 31 + 30 + 31 + 30 + 31 + 31 + 30 + 31 + 30)

            '        If day_vect < 10 Then
            '            day_vect = "0" & i
            '        End If

            '        month_vect = "Dec"

            '    End If





            'Next




            For i = 0 To numero_ventilatori

                Guna2DataGridView1.Rows(i).Cells(0).Value = file_name_target(i)
                Guna2DataGridView1.Rows(i).Cells(1).Value = file_data_target(i)

            Next

            SR.Close()

        Catch ex As Exception

        End Try


    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ListView1_MouseHover(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.Hand
        Me.Refresh()
    End Sub

    Private Sub ListView1_MouseLeave(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.Arrow
        Me.Refresh()
    End Sub

    Private Sub ListView1__Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub OnApplicationExit(ByVal sender As Object, ByVal e As EventArgs)
        Close()
    End Sub

    Public Sub form2_hide(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Form2.Visible = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        '  Form2.nascondi_form1(sender, e)
    End Sub


    Private Sub button1_mouse_hover(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.Hand
    End Sub


    Private Sub button1_mouse_leave(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.Arrow
    End Sub




    Private Sub Guna2DataGridView1_CC(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick

        abilita_grid_click1 = 1

        posizione1 = Guna2DataGridView1.CurrentRow.Index




    End Sub





    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click


        Try

            If abilita_grid_click1 = 1 Then

                Dim selezione_grid As Integer = Guna2DataGridView1.CurrentRow.Index
                Dim scarica_file As String = Guna2DataGridView1.Rows(selezione_grid).Cells(0).Value 'Nome del file da scaricare


                Try
                    'DOWNLOAD DATABASE DATI
                    'Impostazioni generali del Client
                    Dim myFtpWebRequest As System.Net.FtpWebRequest
                    'myFtpWebRequest = System.Net.FtpWebRequest.Create(ftp1 & "/" & file_name_target(posizione1))

                    'Guna2DataGridView1.row


                    If scarica_file(0) = " " Then
                        scarica_file = scarica_file.Substring(1, scarica_file.Length - 1)
                    End If

                    myFtpWebRequest = System.Net.FtpWebRequest.Create(ftp1 & "/" & scarica_file)
                    myFtpWebRequest.Credentials = New NetworkCredential(user, pass)
                    myFtpWebRequest.UseBinary = True
                    myFtpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile

                    'NOTA.
                    'GetResponse ritorna un oggetto WebResponse.
                    'Per accedere alle proprietà specifiche di FTP, è necessario eseguire il cast 
                    'dell'oggetto WebResponse restituito da questo metodo alla classe FtpWebResponse.
                    Dim MyFtpWebResponse = CType(myFtpWebRequest.GetResponse(), FtpWebResponse)

                    'GetResponseStream recupera il flusso che contiene i dati della risposta inviati dal server FTP.
                    Dim MyResponseStream As Stream
                    MyResponseStream = MyFtpWebResponse.GetResponseStream()

                    'Ora creo un oggetto FileStream per poter creare il file in locale:
                    Dim MyFileStream As New FileStream(Folder_PC_storage & "/" & scarica_file, FileMode.Create, FileAccess.Write)

                    'Creo l’array di byte
                    Dim buffer(1024) As Byte

                    'Ciclo di scrittura nel fileStream e lettura di un nuovo buffer da MyResponseStream
                    Dim bytesRead As Integer = MyResponseStream.Read(buffer, 0, 1024)
                    While (bytesRead <> 0)
                        MyFileStream.Write(buffer, 0, bytesRead)
                        bytesRead = MyResponseStream.Read(buffer, 0, 1024)
                    End While

                    MyFileStream.Close()
                    MyFtpWebResponse.Close()

                Catch ex As Exception



                End Try


                Dim proc As New System.Diagnostics.Process()

                proc = Process.Start(Folder_PC_storage & "/" & scarica_file, "")


                time_tick_del = 0
                PanLista.Timer1.Start()

            End If


        Catch ex As Exception





        End Try




    End Sub







    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click


        Dim folderDlg As New FolderBrowserDialog

        folderDlg.ShowNewFolderButton = True

        Dim save_all_path As String
        If (folderDlg.ShowDialog() = DialogResult.OK) Then

            save_all_path = folderDlg.SelectedPath

            Dim root As Environment.SpecialFolder = folderDlg.RootFolder

        End If

        'Dim target_folder_copy As String = w_directory & "\" & User_client & "AAA" & ".xlsx"
        'Dim target_folder_paste As String = database_path & "\Database.xlsx"


        'FileCopy(target_folder_copy, target_folder_paste)


        For i = 0 To numero_ventilatori

            Try
                'DOWNLOAD DATABASE DATI
                'Impostazioni generali del Client
                Dim myFtpWebRequest As System.Net.FtpWebRequest
                myFtpWebRequest = System.Net.FtpWebRequest.Create(ftp1 & "/" & file_name_target(i))
                myFtpWebRequest.Credentials = New NetworkCredential(user, pass)
                myFtpWebRequest.UseBinary = True
                myFtpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile

                'NOTA.
                'GetResponse ritorna un oggetto WebResponse.
                'Per accedere alle proprietà specifiche di FTP, è necessario eseguire il cast 
                'dell'oggetto WebResponse restituito da questo metodo alla classe FtpWebResponse.
                Dim MyFtpWebResponse = CType(myFtpWebRequest.GetResponse(), FtpWebResponse)

                'GetResponseStream recupera il flusso che contiene i dati della risposta inviati dal server FTP.
                Dim MyResponseStream As Stream
                MyResponseStream = MyFtpWebResponse.GetResponseStream()

                'Ora creo un oggetto FileStream per poter creare il file in locale:
                Dim MyFileStream As New FileStream(save_all_path & "/" & file_name_target(i), FileMode.Create, FileAccess.Write)

                'Creo l’array di byte
                Dim buffer(1024) As Byte

                'Ciclo di scrittura nel fileStream e lettura di un nuovo buffer da MyResponseStream
                Dim bytesRead As Integer = MyResponseStream.Read(buffer, 0, 1024)
                While (bytesRead <> 0)
                    MyFileStream.Write(buffer, 0, bytesRead)
                    bytesRead = MyResponseStream.Read(buffer, 0, 1024)
                End While

                MyFileStream.Close()
                MyFtpWebResponse.Close()

            Catch ex As Exception



            End Try

        Next


    End Sub



End Class