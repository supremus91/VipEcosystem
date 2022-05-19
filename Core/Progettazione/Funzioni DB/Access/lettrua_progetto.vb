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

Module lettrua_progetto


    Public Sub lettura_progetto1()

        Dim cn As New OleDb.OleDbConnection(constring)
        Dim ds As DataSet
        Dim da As OleDbDataAdapter
        'Dim tables As DataTableCollection
        Dim source1 As New BindingSource



        ds = New DataSet
        tables1 = ds.Tables
        cn.Open()
        da = New OleDbDataAdapter("SELECT * FROM " & "Progetto", cn)
        da.Fill(ds, "Dati")
        cn.Close()
        Dim view_progetto As New DataView(tables1(0))
        Dim esiste_progetto As Integer = 0




        numeroPJ = 0

        For i As Integer = 0 To view_progetto.Count

            prog_rev(i, 0) = Nothing
            prog_rev(i, 1) = Nothing
            prog_rev(i, 2) = Nothing
            prog_rev(i, 3) = Nothing


        Next

        num_righeDB = view_progetto.Count - 1

        'identifico il numero di revisione per ogni progetto
        For i As Integer = 0 To view_progetto.Count - 1

            esiste_progetto = 0
            For j = 0 To 9999
                If prog_rev(j, 0) = view_progetto(i).Item("tbx_Progetto").ToString Then
                    esiste_progetto = 1
                    j = 10000
                End If
            Next

            prog_rev1(i, 0) = view_progetto(i).Item("tbx_Progetto").ToString
            prog_rev1(i, 1) = view_progetto(i).Item("check_RevCliente").ToString
            prog_rev1(i, 2) = view_progetto(i).Item("tbx_data").ToString
            prog_rev1(i, 3) = view_progetto(i).Item("cbx_Revisione").ToString

            If esiste_progetto = 0 Then
                numeroPJ = numeroPJ + 1

                prog_rev(numeroPJ - 1, 0) = view_progetto(i).Item("tbx_Progetto").ToString
                prog_rev(numeroPJ - 1, 2) = view_progetto(i).Item("cbx_Stato").ToString
                prog_rev(numeroPJ - 1, 3) = view_progetto(i).Item("ID").ToString


                'per il caso di creazione nuovo progetto
                If mod_new_progetto = 1 Then

                    If prog_rev(numeroPJ - 1, 0) = nome_progetto Then

                        posizione_progetto = numeroPJ - 1

                    End If

                End If


                For k = 0 To view_progetto.Count - 1
                    If prog_rev(numeroPJ - 1, 0) = view_progetto(k).Item("tbx_Progetto").ToString Then
                        prog_rev(numeroPJ - 1, 1) = prog_rev(numeroPJ - 1, 1) + 1
                    End If
                Next
                prog_rev(numeroPJ - 1, 1) = prog_rev(numeroPJ - 1, 1) - 1
            End If

        Next


        '------------------------------------------------ricerco i vettori da utilizzare nel servizio mailing-------------------------------------------------------------

        Dim PJ_target As String
        Dim PJ_presente As Integer

        numero_PJ_mail = 0

        For i = 0 To Numero_righeDBtot - 1
            For j = 0 To 8
                vettore_controllo_mail(i, j) = ""
            Next
        Next




        For i As Integer = 0 To view_progetto.Count - 1

            PJ_target = view_progetto(i).Item("tbx_Progetto").ToString

            If i = 0 Then

                vettore_controllo_mail(numero_PJ_mail, 0) = PJ_target
                numero_PJ_mail = numero_PJ_mail + 1
            Else


                PJ_presente = 0

                For j = 0 To numero_PJ_mail - 1 'controllo che la PJ non sia gia presente

                    If PJ_target = vettore_controllo_mail(j, 0) Then

                        PJ_presente = 1

                    End If

                Next

                If PJ_presente = 0 Then
                    vettore_controllo_mail(numero_PJ_mail, 0) = PJ_target
                    numero_PJ_mail = numero_PJ_mail + 1
                End If

            End If


        Next




        Dim DB_position As Integer = 0

        'ora cerco i dati inerenti alle ultime revisioni
        For i As Integer = 0 To numero_PJ_mail - 1

            PJ_target = vettore_controllo_mail(i, 0)

            Dim ID_PJ As Integer = 0



            For j As Integer = 0 To view_progetto.Count - 1

                Dim ID_star As Integer = view_progetto(j).Item("ID").ToString

                If PJ_target = view_progetto(j).Item("tbx_Progetto").ToString And ID_star > ID_PJ Then
                    ID_PJ = view_progetto(j).Item("ID").ToString
                    DB_position = j
                End If


            Next


            vettore_controllo_mail(i, 1) = view_progetto(DB_position).Item("tbx_data").ToString
            vettore_controllo_mail(i, 2) = view_progetto(DB_position).Item("cbx_Revisione").ToString
            vettore_controllo_mail(i, 3) = view_progetto(DB_position).Item("cbx_Owner").ToString
            vettore_controllo_mail(i, 4) = view_progetto(DB_position).Item("cbx_Stato").ToString
            vettore_controllo_mail(i, 5) = view_progetto(DB_position).Item("Data_mail").ToString
            vettore_controllo_mail(i, 6) = view_progetto(DB_position).Item("tbx_Cliente").ToString


            Dim date1 As Date = Convert.ToDateTime(vettore_controllo_mail(i, 1))
            Dim date2 As Date = Convert.ToDateTime(Today.Date)
            Dim delta_date As Integer = Math.Abs(DateDiff(DateInterval.Day, date1, date2))


            vettore_controllo_mail(i, 7) = delta_date
            vettore_controllo_mail(i, 8) = view_progetto(DB_position).Item("tbx_OrdineRicevuto").ToString


        Next

        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------



    End Sub






End Module
