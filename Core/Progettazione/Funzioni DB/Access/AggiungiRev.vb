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

Module AggiungiRev



    Public Sub AggiungiRev1()



        Using myconnection As New OleDbConnection(constring)
            myconnection.Open()
            Dim sqlQry As String = "INSERT INTO [Progetto] ([tbx_Progetto], [cbx_Revisione]) VALUES (@tbx_Progetto, @cbx_Revisione)"

            Using cmd As New OleDbCommand(sqlQry, myconnection)

                cmd.Parameters.AddWithValue("@tbx_Progetto", prog_rev(posizione_progetto, 0))
                cmd.Parameters.AddWithValue("@cbx_Revisione", prog_rev(posizione_progetto, 1) + 1)
                cmd.ExecuteNonQuery()

            End Using


        End Using




    End Sub




End Module
