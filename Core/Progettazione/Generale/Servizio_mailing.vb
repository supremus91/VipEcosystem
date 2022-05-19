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
Imports Microsoft.Office.Interop.Excel


Module Servizio_mailing


    Public Sub listaPJ_utenti()

        Dim numero_utenti As Integer

        PJ_ritardo = 0

        ricerca_valore_tab1("cbx_Owner")
        numero_utenti = LL1

        Dim today_day As DayOfWeek = Today.DayOfWeek

        Dim date_update As String
        'cerco la prima data di aggiornamento utenti
        For o = 0 To numero_PJ_mail - 1
            If vettore_controllo_mail(o, 5) <> "" Then
                date_update = vettore_controllo_mail(o, 5)
            End If
        Next

        Dim date1 As Date = Convert.ToDateTime(date_update)
        Dim date2 As Date = Convert.ToDateTime(Today)
        Dim delta_date As Integer = DateDiff(DateInterval.Day, date1, date2)


        If delta_date > 6 And today_day = DayOfWeek.Monday Then
            For i = 0 To numero_utenti
                mail_review_owner(all_tables(i + 2, pos_vect1))
            Next
            Segnala_PJ_ritardo()
        End If

    End Sub




    Public Sub mail_review_owner(owner)

        Dim emailmessage As New MailMessage()
        identifica_mail(owner)

        Try
            Dim client As New WebClient
            Dim stato_PJ1 As String
            Dim numero_PJ_in_sospeso As Integer = 0

            'Conto il numero di mail rimaste indietro
            For j = 0 To numero_PJ_mail - 1

                Owner_PJ_mail = vettore_controllo_mail(j, 3)
                Stato_PJ_mail = vettore_controllo_mail(j, 4)

                ricerca_valore_tab1("cbx_Stato")
                stato_PJ1 = all_tables(Stato_PJ_mail + 1, pos_vect1)

                ricerca_valore_tab1("cbx_Owner")

                If (stato_PJ1 = "In lavorazione" Or stato_PJ1 = "In attesa risposta cliente") And all_tables(Owner_PJ_mail + 1, pos_vect1) = owner Then
                    numero_PJ_in_sospeso = numero_PJ_in_sospeso + 1
                End If

            Next

            Dim test_owner As String = "Buongiorno " & owner & "," & Environment.NewLine & Environment.NewLine & "hai " & numero_PJ_in_sospeso & " progettazioni rimaste in sospeso: " & Environment.NewLine
            Dim date1 As Date
            Dim date2 As Date

            'Stati PJ
            ricerca_valore_tab1("cbx_Stato")



            For j = 0 To numero_PJ_mail - 1

                Nome_PJ_mail = vettore_controllo_mail(j, 0)
                Data_PJ_mail = vettore_controllo_mail(j, 1)
                Rev_PJ_mail = vettore_controllo_mail(j, 2)
                Owner_PJ_mail = vettore_controllo_mail(j, 3)
                Stato_PJ_mail = vettore_controllo_mail(j, 4)
                Data_mail = vettore_controllo_mail(j, 5)
                cliente_mail = vettore_controllo_mail(j, 6)

                '*************************** Definizione email urgente --> se la PJ rimane aperta per piu di un mese viene inviata agli amministratori --> Paolo e Fausto ***********************

                Dim delta_date As Integer

                date1 = Convert.ToDateTime(Data_PJ_mail)
                date2 = Convert.ToDateTime(Today)
                delta_date = DateDiff(DateInterval.Day, date1, date2)

                ricerca_valore_tab1("cbx_Stato")
                stato_PJ1 = all_tables(Stato_PJ_mail + 1, pos_vect1)

                ricerca_valore_tab1("cbx_Owner")

                If delta_date >= 30 And (stato_PJ1 = "In lavorazione" Or stato_PJ1 = "In attesa risposta cliente" Or (stato_PJ1 = "In codifica" And (owner = "Paolo" Or owner = "Stefano" Or owner = "Fausto")) Or stato_PJ1 = "Codificata" Or stato_PJ1 = "Offerta effettuata") And all_tables(Owner_PJ_mail + 1, pos_vect1) = owner Then

                    mail_urgenti(PJ_ritardo, 1) = owner

                    ricerca_valore_tab1("cbx_Stato")
                    stato_PJ1 = all_tables(Stato_PJ_mail + 1, pos_vect1)

                    '----------------------------------------------CORREGGO CARATTERI SPECIALI DI CLIENTE------------------------------------------------
                    Dim correggi_cliente As String = ""

                    For h = 0 To cliente_mail.Length - 1

                        If cliente_mail(h) = "*" Or cliente_mail(h) = "&" Then
                            correggi_cliente = correggi_cliente & " "
                        Else
                            correggi_cliente = correggi_cliente & cliente_mail(h)
                        End If

                    Next
                    '-----------------------------------------------------------------------------------------------------------------------------------


                    mail_urgenti(PJ_ritardo, 0) = Nome_PJ_mail & "           Aperta il " & Data_PJ_mail & "            Stato:   " & stato_PJ1 & "             Cliente: " & correggi_cliente & ".    -->     Aperta da " & delta_date & " giorni"

                    PJ_ritardo = PJ_ritardo + 1


                End If


                '********************************************************************************************************************************************************************************

                ricerca_valore_tab1("cbx_Stato")
                stato_PJ1 = all_tables(Stato_PJ_mail + 1, pos_vect1)
                ricerca_valore_tab1("cbx_Owner")


                If (stato_PJ1 = "In lavorazione" Or stato_PJ1 = "In attesa risposta cliente" Or (stato_PJ1 = "In codifica" And (owner = "Paolo" Or owner = "Stefano" Or owner = "Fausto")) Or stato_PJ1 = "Codificata" Or stato_PJ1 = "Offerta effettuata") And all_tables(Owner_PJ_mail + 1, pos_vect1) = owner Then

                    date1 = Convert.ToDateTime(Data_PJ_mail)
                    date2 = Convert.ToDateTime(Today)
                    delta_date = DateDiff(DateInterval.Day, date1, date2)

                    '----------------------------------------------CORREGGO CARATTERI SPECIALI DI CLIENTE------------------------------------------------
                    Dim correggi_cliente As String = ""

                    For h = 0 To cliente_mail.Length - 1

                        If cliente_mail(h) = "*" Or cliente_mail(h) = "&" Then
                            correggi_cliente = correggi_cliente & " "
                        Else
                            correggi_cliente = correggi_cliente & cliente_mail(h)
                        End If

                    Next
                    '-----------------------------------------------------------------------------------------------------------------------------------
                    test_owner = test_owner & Environment.NewLine & Nome_PJ_mail & "  aperta il " & Data_PJ_mail & " nello stato:   " & stato_PJ1 & "    cliente: " & correggi_cliente & " ---> Aperta da " & delta_date & " giorni"

                    modifica_DB_mail()

                End If

            Next

            test_owner = test_owner & Environment.NewLine & Environment.NewLine & "Buona giornata!"

            If numero_PJ_in_sospeso > 0 Then

                Dim oggetto As String = "Lista Progettazioni in sospeso"
                Dim url_owner As String = "http://vipfan.ddns.net:9995/?cmd=send_mail_2&msg=" & test_owner & "&ogg=" & oggetto & "&dst=" & email_owner

                If date2.ToString("yyyy/MM/dd") <> Data_mail Then
                    Dim risposta As String = client.DownloadString(url_owner)
                End If

            End If

        Catch ex As Exception

            End Try


    End Sub


    Public Sub Segnala_PJ_ritardo()

        Try

            Dim client As New WebClient
            Dim owner_control As String
            Dim test_owner_Fausto As String = ""
            Dim test_owner_Paolo As String = ""


            test_owner_Fausto = "Buongiorno Fausto," & Environment.NewLine & Environment.NewLine & "ci sono " & PJ_ritardo & " progettazioni rimaste in sospeso da molto tempo: " & Environment.NewLine & Environment.NewLine
            test_owner_Paolo = "Buongiorno Paolo," & Environment.NewLine & Environment.NewLine & "ci sono " & PJ_ritardo & " progettazioni rimaste in sospeso da molto tempo: " & Environment.NewLine & Environment.NewLine

            owner_control = mail_urgenti(0, 1)

            For i = 0 To PJ_ritardo - 1

                If i = 0 Or owner_control <> mail_urgenti(i, 1) Then
                    test_owner_Fausto = test_owner_Fausto & "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" & Environment.NewLine
                    test_owner_Fausto = test_owner_Fausto & mail_urgenti(i, 1) & Environment.NewLine
                    test_owner_Paolo = test_owner_Paolo & "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------" & Environment.NewLine
                    test_owner_Paolo = test_owner_Paolo & mail_urgenti(i, 1) & Environment.NewLine
                End If

                test_owner_Fausto = test_owner_Fausto & mail_urgenti(i, 0) & Environment.NewLine
                test_owner_Paolo = test_owner_Paolo & mail_urgenti(i, 0) & Environment.NewLine



                owner_control = mail_urgenti(i, 1)

            Next

            test_owner_Fausto = test_owner_Fausto & Environment.NewLine & "Buona giornata!"
            test_owner_Paolo = test_owner_Paolo & Environment.NewLine & "Buona giornata!"


            If PJ_ritardo > 0 Then

                Dim oggetto As String
                Dim url_owner As String
                Dim risposta As String


                oggetto = "Lista Progettazioni Urgenti"
                url_owner = "http://vipfan.ddns.net:9995/?cmd=send_mail_2&msg=" & test_owner_Fausto & "&ogg=" & oggetto & "&dst=" & "f.fasolini@vipblade.it"
                risposta = client.DownloadString(url_owner)


                'url_owner = "http://vipfan.ddns.net:9995/?cmd=send_mail_2&msg=" & test_owner_Paolo & "&ogg=" & oggetto & "&dst=" & "p.caimi@vipblade.it"
                'risposta = client.DownloadString(url_owner)

            End If

        Catch ex As Exception



        End Try


    End Sub



    Public Sub messaggio_mail(codice_sollecito, owner, stato)

        'Stati PJ
        ricerca_valore_tab1("cbx_Owner")
        owner = all_tables(owner + 1, pos_vect1)

        identifica_mail(owner)

        Select Case codice_sollecito

            Case 0
                Codice0_Attesa_revisione_cliente(owner, stato)

        End Select

        modifica_DB_mail()

    End Sub



    Public Sub Codice0_Attesa_revisione_cliente(owner, stato)

        Dim emailmessage As New MailMessage()

        Try


            Dim client As New WebClient
            Dim test_owner As String = "Buongiorno " & owner & "," & Environment.NewLine & "ti ricordo che la progettazione " & Nome_PJ_mail & " è ancora in attesa di risposta da parte del cliente. " & Environment.NewLine & "Buona giornata!"

            Dim oggetto As String = Nome_PJ_mail & " - " & stato

            Dim url_owner As String = "http://vipfan.ddns.net:9995/?cmd=send_mail_2&msg=" & test_owner & "&ogg=" & oggetto & "&dst=" & email_owner

            Dim risposta As String = client.DownloadString(url_owner)


        Catch ex As Exception



        End Try

    End Sub






    Public Sub modifica_DB_mail()


        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()


            Dim cmd As New OleDb.OleDbCommand

            Dim stringa_modifica As String


            stringa_modifica = "Data_mail" & " = '" & Today & "'"

            cmd.CommandText = "UPDATE Progetto SET " & stringa_modifica & " WHERE tbx_Progetto = '" & Nome_PJ_mail & "'" & " AND cbx_Revisione = '" & Rev_PJ_mail & "'" & ";"

            cmd.CommandType = CommandType.Text
            cmd.Connection = cn

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()

        End Using


    End Sub


    Public Sub identifica_mail(owner)

        Select Case owner

            Case "Andrea"
                email_owner = "a.fasolini@vipblade.it"
            Case "Stefano"
                email_owner = "s.rossini@vipblade.it"
            Case "Paolo"
                email_owner = "p.caimi@vipblade.it"
            Case "Roberto"
                email_owner = "r.romano@vipblade.it"
            Case "Fausto"
                email_owner = "f.fasolini@vipblade.it"
            Case "Lorenzo"
                email_owner = "l.peretti@vipblade.it"
            Case "Alberto"
                email_owner = "a.vergani@vipblade.it"
            Case "Alessandro"
                email_owner = "f.fasolini@vipblade.it"
            Case "Riccardo"
                email_owner = "r.reato@vipblade.it"

        End Select


    End Sub

End Module
