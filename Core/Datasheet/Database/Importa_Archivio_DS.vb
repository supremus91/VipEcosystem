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


Module Importa_Archivio_DS

    Public Sub Importa_Archivio_DS1()


        Dim cn As New OleDb.OleDbConnection(constring)
        Dim ds As DataSet
        Dim da As OleDbDataAdapter
        'Dim tables As DataTableCollection
        Dim source1 As New BindingSource



        ds = New DataSet
        tables3 = ds.Tables
        cn.Open()
        da = New OleDbDataAdapter("SELECT * FROM " & "Datasheets", cn)
        da.Fill(ds, "Dati")
        cn.Close()
        Dim view_Datasheets As New DataView(tables3(0))
        Dim esiste_DS As Integer = 0


        numero_DS = view_Datasheets.Count
        'dati per la tab iniziale
        For i As Integer = 0 To view_Datasheets.Count
            For j = 0 To 14
                DS_lista(i, j) = Nothing
            Next
        Next


        'dat totali
        For i As Integer = 0 To view_Datasheets.Count
            For j = 0 To Numero_colonneDBtot - 1
                DS_totale_dati(i, j) = Nothing
            Next
        Next


        blocca_add = 0

        'identifico il numero di revisione per ogni progetto
        For i As Integer = 0 To view_Datasheets.Count - 1

            DS_lista(i, 0) = view_Datasheets(i).Item("Data").ToString
            DS_lista(i, 1) = view_Datasheets(i).Item("Descrizione").ToString

            'controllo se il nome che voglio aggiungere e' gia presente nell'archivio
            If descrizione_fan = DS_lista(i, 1) Then
                blocca_add = 1
            End If


            DS_lista(i, 2) = view_Datasheets(i).Item("Test_numero").ToString
            DS_lista(i, 3) = view_Datasheets(i).Item("Tipo_prova").ToString
            DS_lista(i, 4) = view_Datasheets(i).Item("portata_M").ToString
            DS_lista(i, 5) = view_Datasheets(i).Item("pressione_M").ToString
            DS_lista(i, 6) = view_Datasheets(i).Item("RPM_alta").ToString
            DS_lista(i, 7) = view_Datasheets(i).Item("pow_alta").ToString
            DS_lista(i, 8) = view_Datasheets(i).Item("I_alta").ToString
            DS_lista(i, 9) = view_Datasheets(i).Item("Tensione_alta").ToString
            DS_lista(i, 15) = view_Datasheets(i).Item("Freq_alta").ToString



            If DS_lista(i, 6) = "" Then
                DS_lista(i, 6) = view_Datasheets(i).Item("RPM_bassa").ToString
                DS_lista(i, 7) = view_Datasheets(i).Item("pow_bassa").ToString
                DS_lista(i, 8) = view_Datasheets(i).Item("I_bassa").ToString
                DS_lista(i, 9) = view_Datasheets(i).Item("Tensione_bassa").ToString
                DS_lista(i, 15) = view_Datasheets(i).Item("Freq_bassa").ToString
            End If


            DS_lista(i, 10) = view_Datasheets(i).Item("Tmax").ToString
            DS_lista(i, 11) = view_Datasheets(i).Item("Tmin").ToString


            If view_Datasheets(i).Item("coeff4").ToString <> "" Then
                DS_lista(i, 12) = view_Datasheets(i).Item("coeff4").ToString
                DS_lista(i, 13) = view_Datasheets(i).Item("coeff5").ToString
                DS_lista(i, 14) = view_Datasheets(i).Item("coeff6").ToString
            Else
                DS_lista(i, 12) = view_Datasheets(i).Item("coeff1").ToString
                DS_lista(i, 13) = view_Datasheets(i).Item("coeff2").ToString
                DS_lista(i, 14) = view_Datasheets(i).Item("coeff3").ToString
            End If


            'compilo il vettore globale
            DS_totale_dati(i, 0) = view_Datasheets(i).Item("ID").ToString
            DS_totale_dati(i, 1) = view_Datasheets(i).Item("Descrizione").ToString
            DS_totale_dati(i, 2) = view_Datasheets(i).Item("Progettazione").ToString
            DS_totale_dati(i, 3) = view_Datasheets(i).Item("Test_numero").ToString
            DS_totale_dati(i, 4) = view_Datasheets(i).Item("Tipo_prova").ToString
            DS_totale_dati(i, 5) = view_Datasheets(i).Item("Tipo_motore").ToString
            DS_totale_dati(i, 6) = view_Datasheets(i).Item("alt_Zihel").ToString
            DS_totale_dati(i, 7) = view_Datasheets(i).Item("alt_EBM").ToString
            DS_totale_dati(i, 8) = view_Datasheets(i).Item("frame_motore_sel").ToString
            DS_totale_dati(i, 9) = view_Datasheets(i).Item("ERP_sel").ToString
            DS_totale_dati(i, 10) = view_Datasheets(i).Item("rendimento_M").ToString
            DS_totale_dati(i, 11) = view_Datasheets(i).Item("ERP_Target_M").ToString
            DS_totale_dati(i, 12) = view_Datasheets(i).Item("grado_eff").ToString
            DS_totale_dati(i, 13) = view_Datasheets(i).Item("categoria_prova").ToString
            DS_totale_dati(i, 14) = view_Datasheets(i).Item("categoria_eff").ToString
            DS_totale_dati(i, 15) = view_Datasheets(i).Item("potenza_M").ToString
            DS_totale_dati(i, 16) = view_Datasheets(i).Item("portata_M").ToString
            DS_totale_dati(i, 17) = view_Datasheets(i).Item("pressione_M").ToString
            DS_totale_dati(i, 18) = view_Datasheets(i).Item("RPM_M").ToString
            DS_totale_dati(i, 19) = view_Datasheets(i).Item("pow_installata").ToString
            DS_totale_dati(i, 20) = view_Datasheets(i).Item("Tmin").ToString
            DS_totale_dati(i, 21) = view_Datasheets(i).Item("Tmax").ToString
            DS_totale_dati(i, 22) = view_Datasheets(i).Item("Tensione_bassa").ToString
            DS_totale_dati(i, 23) = view_Datasheets(i).Item("Freq_bassa").ToString
            DS_totale_dati(i, 24) = view_Datasheets(i).Item("I_bassa").ToString
            DS_totale_dati(i, 25) = view_Datasheets(i).Item("RPM_bassa").ToString
            DS_totale_dati(i, 26) = view_Datasheets(i).Item("pow_bassa").ToString
            DS_totale_dati(i, 27) = view_Datasheets(i).Item("Tensione_alta").ToString
            DS_totale_dati(i, 28) = view_Datasheets(i).Item("Freq_alta").ToString
            DS_totale_dati(i, 29) = view_Datasheets(i).Item("I_alta").ToString
            DS_totale_dati(i, 30) = view_Datasheets(i).Item("RPM_alta").ToString
            DS_totale_dati(i, 31) = view_Datasheets(i).Item("pow_alta").ToString
            DS_totale_dati(i, 32) = view_Datasheets(i).Item("Q1").ToString
            DS_totale_dati(i, 33) = view_Datasheets(i).Item("P1").ToString
            DS_totale_dati(i, 34) = view_Datasheets(i).Item("RPM1").ToString
            DS_totale_dati(i, 35) = view_Datasheets(i).Item("POW1").ToString
            DS_totale_dati(i, 36) = view_Datasheets(i).Item("CURR1").ToString
            DS_totale_dati(i, 37) = view_Datasheets(i).Item("LWA1").ToString
            DS_totale_dati(i, 38) = view_Datasheets(i).Item("Q2").ToString
            DS_totale_dati(i, 39) = view_Datasheets(i).Item("P2").ToString
            DS_totale_dati(i, 40) = view_Datasheets(i).Item("RPM2").ToString
            DS_totale_dati(i, 41) = view_Datasheets(i).Item("POW2").ToString
            DS_totale_dati(i, 42) = view_Datasheets(i).Item("CURR2").ToString
            DS_totale_dati(i, 43) = view_Datasheets(i).Item("LWA2").ToString
            DS_totale_dati(i, 44) = view_Datasheets(i).Item("Q3").ToString
            DS_totale_dati(i, 45) = view_Datasheets(i).Item("P3").ToString
            DS_totale_dati(i, 46) = view_Datasheets(i).Item("RPM3").ToString
            DS_totale_dati(i, 47) = view_Datasheets(i).Item("POW3").ToString
            DS_totale_dati(i, 48) = view_Datasheets(i).Item("CURR3").ToString
            DS_totale_dati(i, 49) = view_Datasheets(i).Item("LWA3").ToString
            DS_totale_dati(i, 50) = view_Datasheets(i).Item("Q4").ToString
            DS_totale_dati(i, 51) = view_Datasheets(i).Item("P4").ToString
            DS_totale_dati(i, 52) = view_Datasheets(i).Item("RPM4").ToString
            DS_totale_dati(i, 53) = view_Datasheets(i).Item("POW4").ToString
            DS_totale_dati(i, 54) = view_Datasheets(i).Item("CURR4").ToString
            DS_totale_dati(i, 55) = view_Datasheets(i).Item("LWA4").ToString
            DS_totale_dati(i, 56) = view_Datasheets(i).Item("Q5").ToString
            DS_totale_dati(i, 57) = view_Datasheets(i).Item("P5").ToString
            DS_totale_dati(i, 58) = view_Datasheets(i).Item("RPM5").ToString
            DS_totale_dati(i, 59) = view_Datasheets(i).Item("POW5").ToString
            DS_totale_dati(i, 60) = view_Datasheets(i).Item("CURR5").ToString
            DS_totale_dati(i, 61) = view_Datasheets(i).Item("LWA5").ToString
            DS_totale_dati(i, 62) = view_Datasheets(i).Item("Q6").ToString
            DS_totale_dati(i, 63) = view_Datasheets(i).Item("P6").ToString
            DS_totale_dati(i, 64) = view_Datasheets(i).Item("RPM6").ToString
            DS_totale_dati(i, 65) = view_Datasheets(i).Item("POW6").ToString
            DS_totale_dati(i, 66) = view_Datasheets(i).Item("CURR6").ToString
            DS_totale_dati(i, 67) = view_Datasheets(i).Item("LWA6").ToString
            DS_totale_dati(i, 68) = view_Datasheets(i).Item("coeff1").ToString
            DS_totale_dati(i, 69) = view_Datasheets(i).Item("coeff2").ToString
            DS_totale_dati(i, 70) = view_Datasheets(i).Item("coeff3").ToString
            DS_totale_dati(i, 71) = view_Datasheets(i).Item("coeff4").ToString
            DS_totale_dati(i, 72) = view_Datasheets(i).Item("coeff5").ToString
            DS_totale_dati(i, 73) = view_Datasheets(i).Item("coeff6").ToString
            DS_totale_dati(i, 74) = view_Datasheets(i).Item("file1").ToString
            DS_totale_dati(i, 75) = view_Datasheets(i).Item("file2").ToString
            DS_totale_dati(i, 76) = view_Datasheets(i).Item("Annotazione").ToString
            DS_totale_dati(i, 77) = view_Datasheets(i).Item("conf1").ToString
            DS_totale_dati(i, 78) = view_Datasheets(i).Item("Data").ToString
            DS_totale_dati(i, 79) = view_Datasheets(i).Item("IP").ToString
            DS_totale_dati(i, 80) = view_Datasheets(i).Item("Ins_Class").ToString
            DS_totale_dati(i, 81) = view_Datasheets(i).Item("part1").ToString 'contiene tutti i part number
            DS_totale_dati(i, 82) = view_Datasheets(i).Item("check_taglio").ToString
            DS_totale_dati(i, 83) = view_Datasheets(i).Item("cbx_PJ_ambiente").ToString
            DS_totale_dati(i, 84) = view_Datasheets(i).Item("tbx_true_eff").ToString
            DS_totale_dati(i, 85) = view_Datasheets(i).Item("tbx_user_modifiche").ToString
            DS_totale_dati(i, 86) = view_Datasheets(i).Item("tbx_NOTE").ToString
            DS_totale_dati(i, 87) = view_Datasheets(i).Item("sito").ToString
            DS_totale_dati(i, 88) = view_Datasheets(i).Item("cbx_cat1").ToString
            DS_totale_dati(i, 89) = view_Datasheets(i).Item("cbx_cat2").ToString
            DS_totale_dati(i, 90) = view_Datasheets(i).Item("cbx_cat3").ToString
            DS_totale_dati(i, 91) = view_Datasheets(i).Item("cbx_cat4").ToString
            DS_totale_dati(i, 92) = view_Datasheets(i).Item("cbx_cat5").ToString
            DS_totale_dati(i, 93) = view_Datasheets(i).Item("cbx_AtexProtezione").ToString
            DS_totale_dati(i, 94) = view_Datasheets(i).Item("cbx_AtexCustodia").ToString
            DS_totale_dati(i, 95) = view_Datasheets(i).Item("cbx_AtexCategoria").ToString
            DS_totale_dati(i, 96) = view_Datasheets(i).Item("cbx_AtexClasseTemperatura").ToString

        Next


        cn.Dispose()
        ds.Dispose()
        da.Dispose()
        source1.Dispose()
        tables3.Clear()
        view_Datasheets.Dispose()



    End Sub


End Module


