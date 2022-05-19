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

Module Importa_Catalogo


    Public Sub Importa_Catalogo1()

        Dim cn As New OleDb.OleDbConnection(constring)
        Dim ds As DataSet
        Dim da As OleDbDataAdapter
        'Dim tables As DataTableCollection
        Dim source1 As New BindingSource



        ds = New DataSet
        tables8 = ds.Tables
        cn.Open()
        da = New OleDbDataAdapter("SELECT * FROM " & "Catalogo", cn)
        da.Fill(ds, "Dati")
        cn.Close()
        Dim view_catalogo As New DataView(tables8(0))



        numero_Catalogo = view_catalogo.Count
        'dati per la tab iniziale
        For i As Integer = 0 To view_catalogo.Count
            For j = 0 To N_colonne_Catalogo - 1
                DataBase_catalogo(i, j) = Nothing
            Next
        Next


        For i As Integer = 0 To view_catalogo.Count - 1

            For j = 0 To N_colonne_Catalogo - 1

                DataBase_catalogo(i, j) = view_catalogo(i).Item(Nome_colonne_catalogo(j + 1)).ToString

            Next

        Next



    End Sub



End Module
