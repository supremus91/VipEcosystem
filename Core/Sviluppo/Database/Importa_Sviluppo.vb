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


Module Importa_Sviluppo

    Public Sub Importa_Sviluppo1()


        Sviluppi_chiusi = 0
        Sviluppi_attesa = 0
        Sviluppi_waiting = 0
        Sviluppi_totali = 0

        Dim cn As New OleDb.OleDbConnection(constring)
        Dim ds As DataSet
        Dim da As OleDbDataAdapter
        'Dim tables As DataTableCollection
        Dim source1 As New BindingSource



        ds = New DataSet
        tables7 = ds.Tables
        cn.Open()
        da = New OleDbDataAdapter("SELECT * FROM " & "Sviluppo", cn)
        da.Fill(ds, "Dati")
        cn.Close()
        Dim view_sviluppo As New DataView(tables7(0))
        Dim esiste_DS As Integer = 0


        numero_Sviluppo = view_sviluppo.Count
        'dati per la tab iniziale
        For i As Integer = 0 To view_sviluppo.Count
            For j = 0 To N_colonne_sviluppo - 1
                DataBase_sviluppi(i, j) = Nothing
            Next
        Next

        numero_convalide_attese = 0
        stelle_medie = 0
        'identifico il numero di revisione per ogni progetto
        For i As Integer = 0 To view_sviluppo.Count - 1

            For j = 0 To N_colonne_sviluppo - 1

                DataBase_sviluppi(i, j) = view_sviluppo(i).Item(Nome_colonne_sviluppo(j + 1)).ToString

            Next

            Select Case DataBase_sviluppi(i, 1)

                Case "Attesa valutazione"
                    Sviluppi_attesa = Sviluppi_attesa + 1

                    If nome_macchina = view_sviluppo(i).Item("tbx_nomeSV").ToString Then
                        numero_convalide_attese = numero_convalide_attese + 1
                    End If

                Case "Chiuso"
                    Sviluppi_chiusi = Sviluppi_chiusi + 1
                Case "In sviluppo"
                    Sviluppi_waiting = Sviluppi_waiting + 1

            End Select


            Dim stato_rating As String = view_sviluppo(i).Item("cbx_statoSV").ToString
            Dim stella As Double = view_sviluppo(i).Item("tbx_valutazioneSV").ToString

            Dim user_rating As String = ""

            For u = 0 To 9
                user_rating = view_sviluppo(i).Item("tbx_nomeSV").ToString
                If user_rating = Vettore_stat_uffico_tecnico(u, 0) And stato_rating = "Chiuso" Then
                    Vettore_stat_uffico_tecnico(u, 4) = Vettore_stat_uffico_tecnico(u, 4) + 1
                End If
            Next


            stelle_medie = stelle_medie + stella


            Sviluppi_totali = i + 1

        Next

        If Sviluppi_chiusi = 0 Then
            stelle_medie = 0
        Else
            stelle_medie = stelle_medie / Sviluppi_chiusi
        End If






    End Sub


End Module


