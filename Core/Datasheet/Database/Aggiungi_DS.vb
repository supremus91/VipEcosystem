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


Module Aggiungi_DS



    Public Sub Aggiungi_DS1()

        Using myconnection As New OleDbConnection(constring)
            myconnection.Open()
            Dim sqlQry As String = "INSERT INTO [Datasheets] ([Descrizione]) VALUES (@Descrizione)"

            Using cmd As New OleDbCommand(sqlQry, myconnection)

                cmd.Parameters.AddWithValue("@Descrizione", descrizione_fan)


                cmd.ExecuteNonQuery()

            End Using


        End Using


    End Sub







End Module
