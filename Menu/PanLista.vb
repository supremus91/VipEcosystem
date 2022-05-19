Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports System.Threading
Imports System
Imports System.IO
Imports System.Text
Imports System.Net
Imports System.Security.AccessControl
Imports System.Net.Mail
Imports System.Runtime.InteropServices


Public Class PanLista


    Private Sub FormProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTheme()
    End Sub
    Private Sub LoadTheme()
        For Each btns As Control In Me.Controls
            If btns.[GetType]() = GetType(Button) Then
                Dim btn As Button = CType(btns, Button)
                btn.BackColor = ThemeColor.PrimaryColor
                btn.ForeColor = Color.White
                btn.FlatAppearance.BorderColor = ThemeColor.PrimaryColor
            End If
        Next


        'ListView1.BackColor = ThemeColor.PrimaryColor
        'ListView1.ForeColor = ThemeColor.SecondaryColor

        'Label3.ForeColor = ThemeColor.PrimaryColor
        'Label4.ForeColor = ThemeColor.PrimaryColor


    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ListView1.FullRowSelect = True
        'Me.ListView1.GridLines = True

        abilita_grid_click = 0
        btn1_Click()


    End Sub


    Public Sub btn1_Click()


        tot_projects_DF_VD_client = 0
        tot_projects_PF_VD_client = 0
        tot_projects_PN_VD_client = 0
        tot_projects_SF_VD_client = 0
        tot_projects_ATX_client = 0
        tot_projects_IND_client = 0
        tot_projects_OFF_client = 0
        tot_projects_SEA_client = 0

        tot_projects_DF_VD_TOT = 0
        tot_projects_PF_VD_TOT = 0
        tot_projects_PN_VD_TOT = 0
        tot_projects_SF_VD_TOT = 0
        tot_projects_ATX_TOT = 0
        tot_projects_IND_TOT = 0
        tot_projects_OFF_TOT = 0
        tot_projects_SEA_TOT = 0



        Guna2DataGridView1.Rows.Clear()
        Guna2DataGridView1.Rows.Add(Num_ID)

        attivi = 0

        For i = 0 To Num_ID - 1


            Dim User_ID As Integer = i
            Dim text_DWN As String


            text_DWN = All_client_bk(i)



            Dim nome As String = text_DWN.Substring(text_DWN.IndexOf("_") + 1, text_DWN.IndexOf("_Cognome") - 5)
            Dim cognome As String = text_DWN.Substring(text_DWN.IndexOf("_Cognome") + 9)
            cognome = cognome.Substring(0, cognome.IndexOf("_Mail"))
            Dim Azienda As String = text_DWN.Substring(text_DWN.IndexOf("_Azienda") + 9)
            Azienda = Azienda.Substring(0, Azienda.IndexOf("_Stato"))
            Dim Nazione As String = text_DWN.Substring(text_DWN.IndexOf("_Stato") + 7)
            Nazione = Nazione.Substring(0, Nazione.IndexOf("_Password"))
            Dim Email As String = text_DWN.Substring(text_DWN.IndexOf("_Mail") + 6)
            Email = Email.Substring(0, Email.IndexOf("_Azienda"))
            Dim Target As String = text_DWN.Substring(text_DWN.IndexOf("_Password") + 10)
            Target = Target.Substring(0, Target.IndexOf("_Autorizzazione"))
            client_target(i) = Target


            client_DF(i) = text_DWN.Substring(text_DWN.IndexOf("_DF") + 3, text_DWN.IndexOf("_PF") - (text_DWN.IndexOf("_DF") + 3))
            client_PF(i) = text_DWN.Substring(text_DWN.IndexOf("_PF") + 3, text_DWN.IndexOf("_PN") - (text_DWN.IndexOf("_PF") + 3))
            client_PNF(i) = text_DWN.Substring(text_DWN.IndexOf("_PN") + 3, text_DWN.IndexOf("_ATX") - (text_DWN.IndexOf("_PN") + 3))
            client_ATX(i) = text_DWN.Substring(text_DWN.IndexOf("_ATX") + 4, text_DWN.IndexOf("_SF") - (text_DWN.IndexOf("_ATX") + 4))
            client_SF(i) = text_DWN.Substring(text_DWN.IndexOf("_SF") + 3, text_DWN.IndexOf("_IND") - (text_DWN.IndexOf("_SF") + 3))
            client_IND(i) = text_DWN.Substring(text_DWN.IndexOf("_IND") + 4, text_DWN.IndexOf("_OFF") - (text_DWN.IndexOf("_IND") + 4))
            client_OFF(i) = text_DWN.Substring(text_DWN.IndexOf("_OFF") + 4, text_DWN.IndexOf("_SEA") - (text_DWN.IndexOf("_OFF") + 4))
            client_SEA(i) = text_DWN.Substring(text_DWN.IndexOf("_SEA") + 4)



            Dim Stato_sw As String = text_DWN(text_DWN.Length - 1 - text_DWN.Substring(text_DWN.IndexOf("_stat")).Length)

            Dim item As New ListViewItem

            item.SubItems.Add(User_ID)
            item.SubItems.Add(nome)
            item.SubItems.Add(cognome)
            item.SubItems.Add(Azienda)
            item.SubItems.Add(Nazione)
            item.SubItems.Add(Email)

            Guna2DataGridView1.Rows(i).Cells(0).Value = i + 1
            Guna2DataGridView1.Rows(i).Cells(1).Value = My.Resources.Resources.HD_transparent_picture
            Guna2DataGridView1.Rows(i).Cells(2).Value = My.Resources.Resources.user

            If Target = "1" Then
                Guna2DataGridView1.Rows(i).Cells(1).Value = My.Resources.Resources.target
            ElseIf Target = "2" Then
                Guna2DataGridView1.Rows(i).Cells(1).Value = My.Resources.Resources.cloud
            End If

            Guna2DataGridView1.Rows(i).Cells(3).Value = nome
            Guna2DataGridView1.Rows(i).Cells(4).Value = cognome
            Guna2DataGridView1.Rows(i).Cells(5).Value = Azienda
            Guna2DataGridView1.Rows(i).Cells(6).Value = Nazione
            Guna2DataGridView1.Rows(i).Cells(7).Value = Email


            Guna2DataGridView1.Rows(i).Cells(9).Value = client_DF(i)
            Guna2DataGridView1.Rows(i).Cells(10).Value = client_PF(i)
            Guna2DataGridView1.Rows(i).Cells(11).Value = client_PNF(i)
            Guna2DataGridView1.Rows(i).Cells(12).Value = client_ATX(i)
            Guna2DataGridView1.Rows(i).Cells(13).Value = client_SF(i)
            Guna2DataGridView1.Rows(i).Cells(14).Value = client_IND(i)
            Guna2DataGridView1.Rows(i).Cells(15).Value = client_OFF(i)
            Guna2DataGridView1.Rows(i).Cells(16).Value = client_SEA(i)



            Guna2DataGridView1.Rows(i).Cells(17).Value = Convert.ToInt32(client_ATX(i)) + Convert.ToInt32(client_SF(i))



            'determinazione dell'utente che fa più progettazioni
            If i = 1 Then

                crown_max = Guna2DataGridView1.Rows(1).Cells(17).Value


            ElseIf i > 0 Then

                If Guna2DataGridView1.Rows(i).Cells(17).Value > crown_max Then

                    crown_max = Guna2DataGridView1.Rows(i).Cells(17).Value

                    crown_assign = i

                End If


            End If


            'Statistiche globali
            tot_projects_DF_VD_TOT = tot_projects_DF_VD_TOT + client_DF(i)
            tot_projects_PF_VD_TOT = tot_projects_PF_VD_TOT + client_PF(i)
            tot_projects_PN_VD_TOT = tot_projects_PN_VD_TOT + client_PNF(i)
            tot_projects_SF_VD_TOT = tot_projects_SF_VD_TOT + client_SF(i)
            tot_projects_ATX_TOT = tot_projects_ATX_TOT + client_ATX(i)
            tot_projects_IND_TOT = tot_projects_IND_TOT + client_IND(i)
            tot_projects_OFF_TOT = tot_projects_OFF_TOT + client_OFF(i)
            tot_projects_SEA_TOT = tot_projects_SEA_TOT + client_SEA(i)



            If Stato_sw = 1 Then

                item.SubItems.Add("Active")
                Guna2DataGridView1.Rows(i).Cells(8).Value = "Active"
                'Guna2DataGridView1.Rows(i).Cells(8).Style.BackColor = Color.Green
                Guna2DataGridView1.Rows(i).Cells(8).Style.ForeColor = Color.Green

            Else

                item.SubItems.Add("Denied")
                Guna2DataGridView1.Rows(i).Cells(8).Value = "Denied"
                'Guna2DataGridView1.Rows(i).Cells(8).Style.BackColor = Color.Red
                Guna2DataGridView1.Rows(i).Cells(8).Style.ForeColor = Color.Red

            End If


            If nome = "Fausto" And cognome = "Fasolini" Then

                'Guna2DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Blue
                'Guna2DataGridView1.Rows(i).Cells(4).Style.ForeColor = Color.Blue

                'Guna2DataGridView1.Rows(i).Cells(3).Style.Font = New Drawing.Font("Times New Roman", 12, FontStyle.Bold Or FontStyle.Italic)
                'Guna2DataGridView1.Rows(i).Cells(4).Style.Font = New Drawing.Font("Times New Roman", 12, FontStyle.Bold Or FontStyle.Italic)

                Guna2DataGridView1.Rows(i).Cells(2).Value = My.Resources.Fausto
            ElseIf nome = "Paolo" And cognome = "Caimi" Then

                'Guna2DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Blue
                'Guna2DataGridView1.Rows(i).Cells(4).Style.ForeColor = Color.Blue

                'Guna2DataGridView1.Rows(i).Cells(3).Style.Font = New Drawing.Font("Times New Roman", 12, FontStyle.Bold Or FontStyle.Italic)
                'Guna2DataGridView1.Rows(i).Cells(4).Style.Font = New Drawing.Font("Times New Roman", 12, FontStyle.Bold Or FontStyle.Italic)

                Guna2DataGridView1.Rows(i).Cells(2).Value = My.Resources.Paolo

            ElseIf nome = "Alberto" And cognome = "Vergani" Then

                'Guna2DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Blue
                'Guna2DataGridView1.Rows(i).Cells(4).Style.ForeColor = Color.Blue

                'Guna2DataGridView1.Rows(i).Cells(3).Style.Font = New Drawing.Font("Times New Roman", 12, FontStyle.Bold Or FontStyle.Italic)
                'Guna2DataGridView1.Rows(i).Cells(4).Style.Font = New Drawing.Font("Times New Roman", 12, FontStyle.Bold Or FontStyle.Italic)

                Guna2DataGridView1.Rows(i).Cells(2).Value = My.Resources.Alberto1





            ElseIf nome = "Stefano" And cognome = "Rossini" Then

                'Guna2DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Blue
                'Guna2DataGridView1.Rows(i).Cells(4).Style.ForeColor = Color.Blue

                'Guna2DataGridView1.Rows(i).Cells(3).Style.Font = New Drawing.Font("Times New Roman", 12, FontStyle.Bold Or FontStyle.Italic)
                'Guna2DataGridView1.Rows(i).Cells(4).Style.Font = New Drawing.Font("Times New Roman", 12, FontStyle.Bold Or FontStyle.Italic)

                Guna2DataGridView1.Rows(i).Cells(2).Value = My.Resources.Stefano
            ElseIf nome = "Lorenzo" And cognome = "Peretti" Then

                'Guna2DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Blue
                'Guna2DataGridView1.Rows(i).Cells(4).Style.ForeColor = Color.Blue

                'Guna2DataGridView1.Rows(i).Cells(3).Style.Font = New Drawing.Font("Times New Roman", 12, FontStyle.Bold Or FontStyle.Italic)
                'Guna2DataGridView1.Rows(i).Cells(4).Style.Font = New Drawing.Font("Times New Roman", 12, FontStyle.Bold Or FontStyle.Italic)

                Guna2DataGridView1.Rows(i).Cells(2).Value = My.Resources.Saylor

            ElseIf nome = "Roberto" And cognome = "Romanò" Then

            ElseIf nome = "Massimiliano" And cognome = "Mocchetti" Then

                Guna2DataGridView1.Rows(i).Cells(2).Value = My.Resources.Massimiliano
                'Guna2DataGridView1.Rows(i).Cells(3).Style.ForeColor = Color.Blue
                'Guna2DataGridView1.Rows(i).Cells(4).Style.ForeColor = Color.Blue

                'Guna2DataGridView1.Rows(i).Cells(3).Style.Font = New Drawing.Font("Times New Roman", 12, FontStyle.Bold Or FontStyle.Italic)
                'Guna2DataGridView1.Rows(i).Cells(4).Style.Font = New Drawing.Font("Times New Roman", 12, FontStyle.Bold Or FontStyle.Italic)

            End If




            If Stato_sw = 1 Then
                attivi = attivi + 1
            End If

        Next i



        Label1.Text = Num_ID + 1
        Label4.Text = attivi
        Label3.Text = Num_ID + 1 - attivi


        'Statistiche globali cliente
        tot_projects_DF_VD_client = tot_projects_DF_VD_TOT - tot_projects_DF_VD_Uff_tec
        tot_projects_PF_VD_client = tot_projects_PF_VD_TOT - tot_projects_PF_VD_Uff_tec
        tot_projects_PN_VD_client = tot_projects_PN_VD_TOT - tot_projects_PN_VD_Uff_tec
        tot_projects_SF_VD_client = tot_projects_SF_VD_TOT - tot_projects_SF_VD_Uff_tec
        tot_projects_ATX_client = tot_projects_ATX_TOT - tot_projects_ATX_Uff_tec
        tot_projects_IND_client = tot_projects_IND_TOT - tot_projects_IND_Uff_tec
        tot_projects_OFF_client = tot_projects_OFF_TOT - tot_projects_OFF_Uff_tec
        tot_projects_SEA_client = tot_projects_SEA_TOT - tot_projects_SEA_Uff_tec

        tot_projects = tot_projects_ATX_TOT + tot_projects_SF_VD_TOT


        Guna2DataGridView1.Rows(crown_assign).Cells(1).Value = My.Resources.Resources.crown



        Save_mese1()

    End Sub




    Private Sub ListView1__Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Guna2DataGridView1.Click

        abilita_grid_click = 1

        posizione = Guna2DataGridView1.CurrentRow.Index


        Utente_selezionato(6) = Guna2DataGridView1.Rows(posizione).Cells(0).Value 'ID
        Utente_selezionato(0) = Guna2DataGridView1.Rows(posizione).Cells(3).Value 'nome
        Utente_selezionato(1) = Guna2DataGridView1.Rows(posizione).Cells(4).Value 'cognome
        Utente_selezionato(2) = Guna2DataGridView1.Rows(posizione).Cells(5).Value 'azienda
        Utente_selezionato(3) = Guna2DataGridView1.Rows(posizione).Cells(6).Value 'nazione
        Utente_selezionato(4) = Guna2DataGridView1.Rows(posizione).Cells(7).Value 'email
        Utente_selezionato(5) = Guna2DataGridView1.Rows(posizione).Cells(8).Value 'stato

        Utente_selezionato(7) = Guna2DataGridView1.Rows(posizione).Cells(9).Value 'DF
        Utente_selezionato(8) = Guna2DataGridView1.Rows(posizione).Cells(10).Value 'PF
        Utente_selezionato(9) = Guna2DataGridView1.Rows(posizione).Cells(11).Value 'PNF
        Utente_selezionato(10) = Guna2DataGridView1.Rows(posizione).Cells(12).Value 'ATX
        Utente_selezionato(11) = Guna2DataGridView1.Rows(posizione).Cells(13).Value 'SF
        Utente_selezionato(12) = Guna2DataGridView1.Rows(posizione).Cells(14).Value 'IND
        Utente_selezionato(13) = Guna2DataGridView1.Rows(posizione).Cells(15).Value 'OFF
        Utente_selezionato(14) = Guna2DataGridView1.Rows(posizione).Cells(16).Value 'SEA


        'Dim posizione As Integer = ListView1.SelectedIndices(0)

        utente_sel = Utente_selezionato(6)


        If Utente_selezionato(5) = "Denied" Then


            stato_user = 0
            nome_cliente = Utente_selezionato(0)
            cognome_cliente = Utente_selezionato(1)
            Nazione_cliente = Utente_selezionato(3)
            email_send = Utente_selezionato(4)


        Else

            stato_user = 1

        End If

        Guna2Button1.Enabled = True
        Guna2Button2.Enabled = True



    End Sub

    Private Sub Mouse_cursor_hand(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Cursor = Cursors.Hand
        Me.Refresh()
    End Sub

    Private Sub Mouse_cursor_arrow(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Cursor = Cursors.Arrow
        Me.Refresh()
    End Sub

    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click


        If posizione <> 0 And abilita_grid_click = 1 Then

            'creo unerrore per obbligare il defender ad effettuare un aggiornamento di tutti i file
            user_err = "Nome_Lorenzo_Cognome_Peretti_Mail_l.peretti@vipblade.it_Azienda_Vip_Stato_Italy_Password_NaN_Autorizzazione__stat_DF" & Guna2DataGridView1.Rows(0).Cells(9).Value & "_PF" & Guna2DataGridView1.Rows(0).Cells(10).Value & "_PN" & Guna2DataGridView1.Rows(0).Cells(11).Value & "_ATX" & Guna2DataGridView1.Rows(0).Cells(12).Value & "_SF" & Guna2DataGridView1.Rows(0).Cells(13).Value & "_IND" & Guna2DataGridView1.Rows(0).Cells(14).Value & "_OFF" & Guna2DataGridView1.Rows(0).Cells(15).Value & "_SEA" & Guna2DataGridView1.Rows(0).Cells(16).Value  ' Autorizzazione_0 significa che l'utente non è abilitato
            Genera_Segnale1()


            Segnale_DA = 1
            If stato_user = 0 Then
                Accetta_rifiuta1()
                btn1_Click()
            End If


        End If


    End Sub


    Private Sub btn3_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click



        If posizione <> 0 And abilita_grid_click = 1 Then

            'creo unerrore per obbligare il defender ad effettuare un aggiornamento di tutti i file
            user_err = "Nome_Lorenzo_Cognome_Peretti_Mail_l.peretti@vipblade.it_Azienda_Vip_Stato_Italy_Password_NaN_Autorizzazione__stat_DF" & Guna2DataGridView1.Rows(0).Cells(9).Value & "_PF" & Guna2DataGridView1.Rows(0).Cells(10).Value & "_PN" & Guna2DataGridView1.Rows(0).Cells(11).Value & "_ATX" & Guna2DataGridView1.Rows(0).Cells(12).Value & "_SF" & Guna2DataGridView1.Rows(0).Cells(13).Value & "_IND" & Guna2DataGridView1.Rows(0).Cells(14).Value & "_OFF" & Guna2DataGridView1.Rows(0).Cells(15).Value & "_SEA" & Guna2DataGridView1.Rows(0).Cells(16).Value  ' Autorizzazione_0 significa che l'utente non è abilitato
            Genera_Segnale1()


            Segnale_DA = 0
            If stato_user = 1 Then
                Accetta_rifiuta1()
                btn1_Click()
            End If

        End If


    End Sub



    Private Sub delete_file(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim ftpr As FtpWebRequest = DirectCast(WebRequest.Create(file_del), FtpWebRequest)

        Try
            ftpr.Credentials = New System.Net.NetworkCredential(user, pass)
            ftpr.Method = WebRequestMethods.Ftp.DeleteFile
            Dim ftpResponse As FtpWebResponse = CType(ftpr.GetResponse(), FtpWebResponse)
            ftpResponse = ftpr.GetResponse()
            ftpResponse.Close()
        Catch ex As Exception


        End Try

    End Sub


    Private Sub Aggiorna_file(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim request As Net.FtpWebRequest = Net.FtpWebRequest.Create(ftp)
        Dim creds As Net.NetworkCredential = New Net.NetworkCredential(user, pass)
        request.Credentials = creds

        Dim resp As Net.FtpWebResponse = Nothing
        request.Method = Net.WebRequestMethods.Ftp.ListDirectoryDetails

        Dim response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)
        Dim responseStream As Stream = response.GetResponseStream()
        Dim reader As StreamReader = New StreamReader(responseStream)
        Dim content As String = reader.ReadToEnd()

        'Console.WriteLine(reader.ReadToEnd())
        Console.WriteLine($"Directory List Complete, status {response.StatusDescription}")
        reader.Close()
        response.Close()



        Dim nome_file As String = "User_ID_" & utente_sel & ".txt"
        Dim file_path As String = System.IO.Directory.GetCurrentDirectory() & "\" & nome_file
        'Dim file_path As String = w_directory & "\" & nome_file

        'Crea il file in locale
        File.AppendAllText(System.IO.Directory.GetCurrentDirectory() & "\" & nome_file, text_UP) ' Autorizzazione_0 significa che l'utente non è abilitato
        'File.AppendAllText(w_directory & "\" & nome_file, "Nome_" & User_name & "_Cognome_" & User_surname & "_Mail_" & User_mail & "_Azienda_" & User_company)

        Try
            'upload del file nel server ftp
            Dim clsRequest As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create(ftp & "/" & nome_file), System.Net.FtpWebRequest)
            clsRequest.Timeout = 5000
            clsRequest.Credentials = New System.Net.NetworkCredential(user, pass)
            clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile
            Dim clsStream As System.IO.Stream = clsRequest.GetRequestStream()
            clsStream.Write(System.IO.File.ReadAllBytes(file_path), 0, System.IO.File.ReadAllBytes(file_path).Length)
            clsStream.Close()


        Catch ex As WebException




        End Try




        Dim directory = System.IO.Directory.GetCurrentDirectory()


        For Each filename As String In IO.Directory.GetFiles(directory, "*")
            Dim fName As String = IO.Path.GetExtension(filename)

            If fName = ".txt" Then
                Dim filename_only As String = filename.Substring(directory.Length + 1)
                If filename_only.Length > 7 Then
                    Dim check_string = filename_only.Substring(0, 7)
                    If check_string = "User_ID" Then
                        System.IO.File.Delete(filename)
                    End If
                End If
            End If
        Next



    End Sub



    Private Sub Send_email_to_client(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim emailmessage As New MailMessage()

        Try

            'l.peretti@vipblabe.it

            emailmessage.From = New MailAddress("l.peretti@vipblade.it")
            emailmessage.To.Add(email_send)
            emailmessage.Subject = ("Accesso VipDesigner")

            Dim str1 As String
            Dim str2 As String
            Dim str3 As String

            If Nazione_cliente = "Italy" Then
                str1 = "Gentile " & nome_cliente & " " & cognome_cliente & ","
                str2 = "siamo lieti di informarla che il suo account per il software VipDesigner è stato attivato,"
                str3 = "Cordiali saluti"
            Else
                str1 = "Dear " & nome_cliente & " " & cognome_cliente & ","
                str2 = "We're proud to inform you that from now on your account is activated,"
                str3 = "Best regards"
            End If

            Dim str_tot As String = str1 + Environment.NewLine + str2 + Environment.NewLine + str3

            emailmessage.Body = str_tot

            Dim SMTP As New SmtpClient("smtp-mail.outlook.com")
            SMTP.Port = 587
            SMTP.EnableSsl = True
            SMTP.Credentials = New System.Net.NetworkCredential("l.peretti@vipblade.it", "$Vip2010")
            SMTP.Send(emailmessage)


        Catch ex As Exception


        End Try


    End Sub





    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click


        If abilita_grid_click = 1 Then

            'creo unerrore per obbligare il defender ad effettuare un aggiornamento di tutti i file
            user_err = "Nome_Lorenzo_Cognome_Peretti_Mail_l.peretti@vipblade.it_Azienda_Vip_Stato_Italy_Password_NaN_Autorizzazione__stat_DF" & Guna2DataGridView1.Rows(0).Cells(9).Value & "_PF" & Guna2DataGridView1.Rows(0).Cells(10).Value & "_PN" & Guna2DataGridView1.Rows(0).Cells(11).Value & "_ATX" & Guna2DataGridView1.Rows(0).Cells(12).Value & "_SF" & Guna2DataGridView1.Rows(0).Cells(13).Value & "_IND" & Guna2DataGridView1.Rows(0).Cells(14).Value & "_OFF" & Guna2DataGridView1.Rows(0).Cells(15).Value & "_SEA" & Guna2DataGridView1.Rows(0).Cells(16).Value  ' Autorizzazione_0 significa che l'utente non è abilitato
            Genera_Segnale1()

            Target1()
            btn1_Click()

        End If





    End Sub

    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick



        If e.ColumnIndex = 1 And abilita_grid_click = 1 And (client_target(posizione) = 2 Or client_target(posizione) = 1) Then

            All_project_user.Show()

        End If





    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick


        time_tick_del = time_tick_del + 100

        If time_tick_del > 20000 Then
            Try
                Dim fi2 As FileInfo = New FileInfo(Folder_PC_storage & "/" & file_name_target(posizione1))
                fi2.Delete()
            Catch ex As Exception

            End Try

            Timer1.Stop()
        End If

    End Sub

End Class