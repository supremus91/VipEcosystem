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


Module Genera_Segnale



    Public Sub Genera_Segnale1()


        File.WriteAllText(Folder_PC_storage & "\" & "User_ID_0.txt", "")
        File.AppendAllText(Folder_PC_storage & "\" & "User_ID_0.txt", user_err)

        Try

            'upload del file nel server ftp
            Dim clsRequest As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create(ftp & "/" & "User_ID_0.txt"), System.Net.FtpWebRequest)
            clsRequest.Timeout = 5000
            clsRequest.Credentials = New System.Net.NetworkCredential(user, pass)
            clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile
            Dim clsStream As System.IO.Stream = clsRequest.GetRequestStream()
            clsStream.Write(System.IO.File.ReadAllBytes(Folder_PC_storage & "\" & "User_ID_0.txt"), 0, System.IO.File.ReadAllBytes(Folder_PC_storage & "\" & "User_ID_0.txt").Length)
            clsStream.Close()


        Catch ex As WebException



        End Try




    End Sub



End Module
