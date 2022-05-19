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

Module Numero_colonne_DS


    Public Sub Numero_colonne_DS1()


        Dim tableName = "PJ_DataBase"
        Dim filterValues = {Nothing, Nothing, tableName, Nothing}
        Dim i As Integer = 0

        Using conn = New OleDbConnection(constring)
            conn.Open()
            Dim columns = conn.GetSchema("Columns", filterValues)
            For Each row As DataRow In columns.Rows
                i = i + 1
                Nome_colonneDS(i) = row("column_name")

                Try
                    Descrizione_colonneDS(i) = row("Description")
                Catch ex As Exception
                    Descrizione_colonneDS(i) = ""
                End Try


            Next
        End Using

        Numero_colonneDS = i



    End Sub





End Module
