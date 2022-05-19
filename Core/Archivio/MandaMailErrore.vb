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


Module MandaMailErrore


    Public Sub MandaMailErrore1()


        Try
            email_owner = "l.peretti@vipblade.it"
            Dim client As New WebClient
            Dim test_owner As String

            '= "Buongiorno Lorenzo la rigenerazione del database ha presentato i seguenti errori: " & Environment.NewLine & TestoMail
            test_owner = ""
            For u1 = 0 To Count_Mail_Error - 1
                test_owner = test_owner & Error_log_ristampa(u1) & Environment.NewLine & Environment.NewLine
            Next

            Dim oggetto As String = "Errori rigenerazione archivio"

            Dim url_owner As String = "http://vipfan.ddns.net:9995/?cmd=send_mail_2&msg=" & test_owner & "&ogg=" & oggetto & "&dst=" & email_owner

            Dim risposta As String = client.DownloadString(url_owner)
        Catch ex As Exception

        End Try


    End Sub


End Module
