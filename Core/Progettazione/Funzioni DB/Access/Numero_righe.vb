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

Module Numero_righe


    Function getcount(NomeTabella) As Integer
        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()
            Dim cmd As New OleDb.OleDbCommand("Select COUNT(*) FROM " & NomeTabella, cn)
            Return cmd.ExecuteScalar()
        End Using
    End Function


End Module
