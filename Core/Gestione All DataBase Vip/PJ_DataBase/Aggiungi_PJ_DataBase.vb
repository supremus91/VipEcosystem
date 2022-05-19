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

Module Aggiungi_PJ_DataBase

    Public Sub Aggiungi_PJ_DataBase1(Codice, Ambiente)

        Using myconnection As New OleDbConnection(constring)
            myconnection.Open()


            Dim sqlQry As String
            If DB_pasticci_on = 0 Then
                sqlQry = "INSERT INTO [PJ_DataBase] ([cbx_PJ_configurazioni], [cbx_PJ_ambiente]) VALUES (@cbx_PJ_configurazioni, @cbx_PJ_ambiente)"
            Else
                sqlQry = "INSERT INTO [PJ_DataBasePasticci] ([cbx_PJ_configurazioni], [cbx_PJ_ambiente]) VALUES (@cbx_PJ_configurazioni, @cbx_PJ_ambiente)"
            End If

            Using cmd As New OleDbCommand(sqlQry, myconnection)

                cmd.Parameters.AddWithValue("@cbx_PJ_configurazioni", Codice)
                cmd.Parameters.AddWithValue("@cbx_PJ_ambiente", Ambiente)

                cmd.ExecuteNonQuery()

            End Using


        End Using


    End Sub


End Module
