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

Module SchemaDatabase


    Public Sub SchemaDatabase1()

        Dim i As Integer = 0

        Using conn As New OleDbConnection(constring)

            conn.Open()
            Dim dt As DataTable = conn.GetSchema("TABLES", {Nothing, Nothing, Nothing, "TABLE"})
            For Each dr As DataRow In dt.Rows

                SchemaDB(i) = dr("TABLE_NAME")

                i = i + 1
            Next
        End Using

        Numero_tabelle = i

    End Sub






End Module
