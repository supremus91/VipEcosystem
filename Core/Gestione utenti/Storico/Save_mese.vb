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

Module Save_mese



    Public Sub Save_mese1()



        Dim mese = Today.Month
        Dim anno = Today.Year

        Dim data1 As String = mese & "_" & anno & ";"
        Dim save_month_stat_cliente = tot_projects_DF_VD_client & ";" & tot_projects_PF_VD_client & ";" & tot_projects_PN_VD_client & ";" & tot_projects_SF_VD_client & ";" & tot_projects_ATX_client & ";" & tot_projects_IND_client & ";" & tot_projects_OFF_client & ";" & tot_projects_SEA_client & ";"
        Dim save_month_stat_uff_tec = tot_projects_DF_VD_Uff_tec & ";" & tot_projects_PF_VD_Uff_tec & ";" & tot_projects_PN_VD_Uff_tec & ";" & tot_projects_SF_VD_Uff_tec & ";" & tot_projects_ATX_Uff_tec & ";" & tot_projects_IND_Uff_tec & ";" & tot_projects_OFF_Uff_tec & ";" & tot_projects_SEA_Uff_tec & ";"
        Dim tot_clienti = Num_ID + 1 - 5

        Dim str_tot_save = data1 & save_month_stat_cliente & save_month_stat_uff_tec & tot_clienti

        Dim file_path As String = System.IO.Directory.GetCurrentDirectory() & "\" & file_storico

        File.WriteAllText(file_path, "")









        'Scarico il file dello storico dal server
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

        Dim nome_file As String = ftp & "/" & file_storico
        Dim text_DWN As String = client.DownloadString(nome_file)

        File.AppendAllText(System.IO.Directory.GetCurrentDirectory() & "\" & file_storico, text_DWN) ' Autorizzazione_0 significa che l'utente non è abilitato


        Dim i_line As Integer = 0
        Dim i_line1 As Integer = 0
        Dim i1 As Integer = 0
        num_lines = 0


        For Each filename As String In System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "*", System.IO.SearchOption.AllDirectories)


            Dim fname As String = System.IO.Path.GetExtension(filename)

            If (fname = ".txt") Then

                If filename = file_path Then




                    Dim lines As List(Of String) = New List(Of String)()
                    lines = File.ReadAllLines(file_path).ToList()


                    i_line = 0
                    i_line1 = 0
                    i1 = 0




                    For Each line As String In lines

                        line_storico(i1) = line



                        If (i1 + 1 = mese) Then

                            line_storico(i1) = str_tot_save

                        End If





                        num_lines = num_lines + 1

                        Dim mese_anno_comp As String


                        If line <> "" Then
                            mese_anno_comp = line.Substring(0, 6)
                        End If


                        If mese_anno_comp <> mese & "" & anno And line <> "" Then


                            Dim start As Integer = 0

                            For j = 0 To 17



                                If (j < 18 - 1) Then
                                    matrix_storico(i1, j) = line.Substring(start, line.IndexOf(";") - start)
                                    line = line.Substring(matrix_storico(i1, j).Length + 1, line.Length - (matrix_storico(i1, j).Length + 1))

                                Else

                                    matrix_storico(i1, j) = line

                                End If

                            Next



                            i1 = i1 + 1

                        End If

                    Next

                End If


            End If


        Next




        Dim text_upload_storico As String



        For u = 0 To num_lines - 1

            text_upload_storico = text_upload_storico + line_storico(u) + Environment.NewLine


        Next

        If num_lines < mese Then
            text_upload_storico = text_upload_storico + str_tot_save + Environment.NewLine
        End If




        File.WriteAllText(file_path, "")

        File.AppendAllText(System.IO.Directory.GetCurrentDirectory() & "\" & file_storico, text_upload_storico) ' Autorizzazione_0 significa che l'utente non è abilitato

        'Salvo il file nel cloud
        Try
            'upload del file nel server ftp
            Dim clsRequest As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create(ftp & "/" & file_storico), System.Net.FtpWebRequest)
            clsRequest.Timeout = 5000
            clsRequest.Credentials = New System.Net.NetworkCredential(user, pass)
            clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile
            Dim clsStream As System.IO.Stream = clsRequest.GetRequestStream()
            clsStream.Write(System.IO.File.ReadAllBytes(file_path), 0, System.IO.File.ReadAllBytes(file_path).Length)
            clsStream.Close()


        Catch ex As WebException




        End Try



    End Sub







End Module
