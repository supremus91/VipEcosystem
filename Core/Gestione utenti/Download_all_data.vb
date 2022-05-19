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

Module Download_all_data


    Public Sub Download_all_data1()


        For i = 0 To 9999
            All_client_bk(i) = ""
        Next


        Num_ID = 0
        attivi = 0


        Dim request As Net.FtpWebRequest = Net.FtpWebRequest.Create(ftp)
        Dim creds As Net.NetworkCredential = New Net.NetworkCredential(user, pass)
        request.Credentials = creds

        Dim resp As Net.FtpWebResponse = Nothing
        request.Method = Net.WebRequestMethods.Ftp.ListDirectoryDetails

        Dim response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)
        Dim responseStream As Stream = response.GetResponseStream()
        Dim reader As StreamReader = New StreamReader(responseStream)
        Dim content As String = reader.ReadToEnd()



        Dim client As WebClient = New WebClient()
        client.Credentials = New NetworkCredential(user, pass)

        Dim nome_file As String = ftp & "/" & file_tot_file
        Dim text_DWN As String = client.DownloadString(nome_file)
        Dim text_DWN_star As String = text_DWN.Substring(5, text_DWN.Length - 5)

        Dim lDWN As Integer = 0
        Dim total_BK_file As Integer = 0

        Dim i1 As Integer = 0



        Do While (text_DWN_star.Length > 10)


            If text_DWN_star.Length > 200 Then

                If i1 = 0 Then
                    All_client_bk(i1) = "Nome_" & text_DWN_star.Substring(0, text_DWN_star.IndexOf("Nome_"))
                Else
                    text_DWN_star = text_DWN_star.Substring(5, text_DWN_star.Length - 5)

                    All_client_bk(i1) = "Nome_" & text_DWN_star.Substring(0, text_DWN_star.IndexOf("Nome_"))

                End If

                Dim nomeAA As String = All_client_bk(i1).Substring(5, All_client_bk(i1).Length - 5)

                Dim intyyy As Integer = nomeAA.Length
                Dim intyyy1 As Integer = text_DWN_star.Length - intyyy

                text_DWN_star = text_DWN_star.Substring(intyyy, intyyy1)

                i1 = i1 + 1

            Else
                All_client_bk(i1) = text_DWN_star
                text_DWN_star = ""
            End If

            Num_ID = Num_ID + 1



            'If (text_DWN.Length - lDWN) > 200 Then




            '    All_client_bk(Num_ID) = "Nome_" & text_DWN_star.Substring(0, text_DWN_star.IndexOf("Nome_"))

            '    total_BK_file = text_DWN_star.Substring(total_BK_file, text_DWN_star.IndexOf("Nome_")).Length + 5

            '    text_DWN_star = text_DWN_star.Substring(total_BK_file, text_DWN_star.Length - total_BK_file)

            '    lDWN = lDWN + total_BK_file


            '    'vado a salvare le statistiche per l'utente ufficio tecnico
            '    Stat_UT_user1()

            'Else

            '    All_client_bk(Num_ID) = "Nome_" & text_DWN_star

            '    Exit Do

            'End If





        Loop





    End Sub





End Module
