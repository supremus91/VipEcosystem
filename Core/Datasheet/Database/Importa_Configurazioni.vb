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


Module Importa_Configurazioni

    Public Sub Importa_Configurazioni1()

        Dim cn As New OleDb.OleDbConnection(constring)
        Dim ds As DataSet
        Dim da As OleDbDataAdapter
        'Dim tables As DataTableCollection
        Dim source1 As New BindingSource



        ds = New DataSet
        tables4 = ds.Tables
        cn.Open()
        da = New OleDbDataAdapter("SELECT * FROM " & "cbx_PJ_configurazioni", cn)
        da.Fill(ds, "Dati")
        cn.Close()
        Dim view_Config As New DataView(tables4(0))
        Dim esiste_DS As Integer = 0


        numero_DS_conf = view_Config.Count
        'dati per la tab iniziale
        For i As Integer = 0 To view_Config.Count
            For j = 0 To 1
                DS_config_lista(i, j) = Nothing
            Next
        Next


        'identifico il numero di revisione per ogni progetto
        For i As Integer = 0 To view_Config.Count - 1
            DS_config_lista(i, 0) = view_Config(i).Item("ID").ToString
            DS_config_lista(i, 1) = view_Config(i).Item("Codice").ToString
            DS_config_lista(i, 2) = view_Config(i).Item("Descrizione").ToString
            DS_config_lista(i, 3) = view_Config(i).Item("Description").ToString
        Next





    End Sub



End Module
