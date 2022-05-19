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

Module Target

    Public Sub Target1()


        Dim active_state As Integer = 1

        If Utente_selezionato(5) = "Denied" Then
            active_state = 0
        End If


        If client_target(posizione) = 1 Then
            All_client_bk(posizione) = "Nome_" & Utente_selezionato(0) & "_Cognome_" & Utente_selezionato(1) & "_Mail_" & Utente_selezionato(4) & "_Azienda_" & Utente_selezionato(2) & "_Stato_" & Utente_selezionato(3) & "_Password_" & "2" & "_Autorizzazione_" & active_state & "_stat_DF" & Utente_selezionato(7) & "_PF" & Utente_selezionato(8) & "_PN" & Utente_selezionato(9) & "_ATX" & Utente_selezionato(10) & "_SF" & Utente_selezionato(11) & "_IND" & Utente_selezionato(12) & "_OFF" & Utente_selezionato(13) & "_SEA" & Utente_selezionato(14)  ' Autorizzazione_0 significa che l'utente non è abilitato
        Else
            All_client_bk(posizione) = "Nome_" & Utente_selezionato(0) & "_Cognome_" & Utente_selezionato(1) & "_Mail_" & Utente_selezionato(4) & "_Azienda_" & Utente_selezionato(2) & "_Stato_" & Utente_selezionato(3) & "_Password_" & "1" & "_Autorizzazione_" & active_state & "_stat_DF" & Utente_selezionato(7) & "_PF" & Utente_selezionato(8) & "_PN" & Utente_selezionato(9) & "_ATX" & Utente_selezionato(10) & "_SF" & Utente_selezionato(11) & "_IND" & Utente_selezionato(12) & "_OFF" & Utente_selezionato(13) & "_SEA" & Utente_selezionato(14)  ' Autorizzazione_0 significa che l'utente non è abilitato
        End If




        For i = 0 To Num_ID - 1

            If i = 0 Then
                File.WriteAllText(Folder_PC_storage & "\" & file_tot_file, "")
            End If

            File.AppendAllText(Folder_PC_storage & "\" & file_tot_file, All_client_bk(i))

        Next


        Try

            'upload del file nel server ftp
            Dim clsRequest As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create(ftp & "/" & file_tot_file), System.Net.FtpWebRequest)
            clsRequest.Timeout = 5000
            clsRequest.Credentials = New System.Net.NetworkCredential(user, pass)
            clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile
            Dim clsStream As System.IO.Stream = clsRequest.GetRequestStream()
            clsStream.Write(System.IO.File.ReadAllBytes(Folder_PC_storage & "\" & file_tot_file), 0, System.IO.File.ReadAllBytes(Folder_PC_storage & "\" & file_tot_file).Length)
            clsStream.Close()


        Catch ex As WebException



        End Try



    End Sub


End Module
