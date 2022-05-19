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

Module Lettura_note

    Public Sub lettura_note1()

        Dim cn As New OleDb.OleDbConnection(constring)
        Dim ds As DataSet
        Dim da As OleDbDataAdapter
        'Dim tables As DataTableCollection
        Dim source1 As New BindingSource



        ds = New DataSet
        tables2 = ds.Tables
        cn.Open()
        da = New OleDbDataAdapter("SELECT * FROM " & "NoteProgetto", cn)
        da.Fill(ds, "Dati1")
        cn.Close()



    End Sub





End Module
