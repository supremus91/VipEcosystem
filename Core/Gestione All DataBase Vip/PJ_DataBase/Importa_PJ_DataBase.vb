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


Module Importa_PJ_Database

    Public Sub Importa_PJ_Database1()


        Dim cn As New OleDb.OleDbConnection(constring)
        Dim ds As DataSet
        Dim da As OleDbDataAdapter
        'Dim tables As DataTableCollection
        Dim source1 As New BindingSource



        ds = New DataSet
        tables6 = ds.Tables
        cn.Open()



        If DB_pasticci_on = 0 Then 'SE LO CHIAMO DALLA MODALITà DATABASE
            da = New OleDbDataAdapter("SELECT * FROM " & "PJ_DataBase", cn)
        Else 'SE LO CHIAMO DALLA MODALITà ARCHIVIO
            da = New OleDbDataAdapter("SELECT * FROM " & "PJ_DataBasePasticci", cn)
        End If


        da.Fill(ds, "Dati")
        cn.Close()
        Dim view_PJ_DataBase As New DataView(tables6(0))
        Dim esiste_DS As Integer = 0


        numero_PJ_DataBase = view_PJ_DataBase.Count
        'dati per la tab iniziale
        For i As Integer = 0 To view_PJ_DataBase.Count
            For j = 0 To numero_colonne_PJ_DataBase - 1
                PJ_DataBase_lista(i, j) = Nothing
            Next
        Next


        'identifico il numero di revisione per ogni progetto
        For i As Integer = 0 To view_PJ_DataBase.Count - 1



            For j = 0 To numero_colonne_PJ_DataBase - 2

                If Nome_colonneDS(j + 1) = "cbx_PJ_configurazioni" Then

                    config_pos = j

                    'compilo il vettore globale
                    PJ_DataBase_lista(i, j) = view_PJ_DataBase(i).Item(Nome_colonneDS(j + 1)).ToString




                    'TRADUZIONE CONFIGURAZIONE
                    For k As Integer = 0 To numero_DS_conf - 1
                        If DS_config_lista(k, 0) = PJ_DataBase_lista(i, j) Then
                            PJ_DataBase_lista(i, j) = DS_config_lista(k, 1)
                            PJ_DataBase_lista(i, numero_colonne_PJ_DataBase - 1) = DS_config_lista(k, 2)
                        End If
                    Next


                ElseIf Nome_colonneDS(j + 1) = "cbx_PJ_ambiente" Then

                    amb_pos = j

                    PJ_DataBase_lista(i, j) = view_PJ_DataBase(i).Item(Nome_colonneDS(j + 1)).ToString

                    'TRADUZIONE AMBIENTE
                    For k As Integer = 0 To numero_DS_amb - 1
                        If DS_ambiente_lista(k, 0) = PJ_DataBase_lista(i, j) Then
                            PJ_DataBase_lista(i, j) = DS_ambiente_lista(k, 1)
                        End If
                    Next



                Else

                    If Nome_colonneDS(j + 1) = "cbx_tipo_motore_conf" Then
                        mot_pos = j
                    End If


                    If Nome_colonneDS(j + 1)(0) = "c" And Nome_colonneDS(j + 1)(1) = "b" And Nome_colonneDS(j + 1)(2) = "x" Then
                        Traduttore_PJ_DataBase1(view_PJ_DataBase(i).Item(Nome_colonneDS(j + 1)).ToString, Nome_colonneDS(j + 1))
                        PJ_DataBase_lista(i, j) = valore_DS_star
                    Else
                        PJ_DataBase_lista(i, j) = view_PJ_DataBase(i).Item(Nome_colonneDS(j + 1)).ToString
                    End If

                End If



            Next





        Next





    End Sub


End Module


