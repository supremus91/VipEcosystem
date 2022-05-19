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

Module Lettura_TabDB


    Public Sub Lettura_TabDB1()




        Dim cn As New OleDb.OleDbConnection(constring)
        Dim ds As DataSet
        Dim da As OleDbDataAdapter
        Dim tables As DataTableCollection
        Dim source1 As New BindingSource



        ds = New DataSet
        tables = ds.Tables
        cn.Open()

        Dim LL_memo As Integer = 0
        Dim passo As Integer = 0

        For i = 0 To Numero_tabelle - 1

            'If SchemaDB(i) <> "Progetto" And SchemaDB(i) <> "NoteProgetto" Then

            If SchemaDB(i)(0) = "c" And SchemaDB(i)(1) = "b" And SchemaDB(i)(2) = "x" Then

                For k = 0 To 79

                    vettore_elemento_cbx(k) = ""

                Next
                L_vettore = 0


                da = New OleDbDataAdapter("SELECT * FROM " & SchemaDB(i), cn)
                da.Fill(ds, "Dati")


                Dim view As New DataView(tables(0))

                For j As Integer = LL_memo To view.Count - 1
                    vettore_elemento_cbx(j - LL_memo) = view(j).Item("Descrizione").ToString ' this ID is case sensetive
                Next

                L_vettore = view.Count - 1


                all_tables(0, passo) = SchemaDB(i)
                all_tables(1, passo) = L_vettore - LL_memo

                For j = 0 To L_vettore - LL_memo
                    all_tables(j + 2, passo) = vettore_elemento_cbx(j)
                Next



                LL_memo = view.Count




                passo = passo + 1

            End If

        Next



        cn.Close()





    End Sub





End Module



'Using cn As New OleDb.OleDbConnection(constring)
'    'provider to be used when working with access database
'    cn.Open()
'    Dim cmd As New OleDb.OleDbCommand("SELECT * where tbx_Progetto = " & xxxx & " AND cbx_Revisione =" & yyyy, cn)
'    Dim myreader As OleDbDataReader

'    myreader = cmd.ExecuteReader
'    myreader.Read()

'    Dim read_cell As String = myreader(NomeColonna)

'    Return read_cell

'    cn.Close()

'End Using