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

Module Importa_Ambiente


    Public Sub Importa_Ambiente1()

        Dim cn As New OleDb.OleDbConnection(constring)
        Dim ds As DataSet
        Dim da As OleDbDataAdapter
        'Dim tables As DataTableCollection
        Dim source1 As New BindingSource



        ds = New DataSet
        tables5 = ds.Tables
        cn.Open()
        da = New OleDbDataAdapter("SELECT * FROM " & "cbx_PJ_ambiente", cn)
        da.Fill(ds, "Dati")
        cn.Close()
        Dim view_Amb As New DataView(tables5(0))



        numero_DS_amb = view_Amb.Count
        'dati per la tab iniziale
        For i As Integer = 0 To view_Amb.Count
            For j = 0 To 1
                DS_ambiente_lista(i, j) = Nothing
            Next
        Next


        'identifico il numero di revisione per ogni progetto
        For i As Integer = 0 To view_Amb.Count - 1
            DS_ambiente_lista(i, 0) = view_Amb(i).Item("ID").ToString
            DS_ambiente_lista(i, 1) = view_Amb(i).Item("Descrizione").ToString
        Next




    End Sub


End Module
