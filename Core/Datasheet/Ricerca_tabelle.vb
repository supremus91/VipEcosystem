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
Imports System.IO.Ports
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Net.Sockets
Imports Microsoft.Office.Interop
Imports System.Globalization
Imports System.Windows.Forms.DataVisualization.Charting
Imports DevExpress.XtraReports.UI
Imports System.Collections
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Drawing
Imports MaterialSkin.Animations
Imports MaterialSkin.Controls
Imports MaterialSkin
Imports SavingUserSettings.Properties


Module Ricerca_tabelle



    Public Sub Ricerca_tabelle1()


        Dim check_empty As String = "a"

        Dim COLUMN As Integer = 0
        Dim RAW As Integer = 0
        Dim find_table As Integer = 0


        Max_eff_ERP2015_bassa = 0
        target_eff_ERP2015_bassa = 0
        pow_ERP2015_bassa = 0
        Q_ERP2015_bassa = 0
        P_ERP2015_bassa = 0
        RPM_ERP2015_bassa = 0
        cat_eff_ERP2015_bassa = 0
        Cat_prova_ERP2015_bassa = 0

        Max_eff_ERP2015_alta = 0
        target_eff_ERP2015_alta = 0
        pow_ERP2015_alta = 0
        Q_ERP2015_alta = 0
        P_ERP2015_alta = 0
        RPM_ERP2015_alta = 0
        cat_eff_ERP2015_alta = 0
        Cat_prova_ERP2015_alta = 0



        If S_D_var = 1 Then '-------> CASO SINGLO VELOCITA'

            'Apro il file excel e cerco la posizione della tabella prestabilita (LA CELLA RICERCATA E' "PTI")
            objExcel_BASSA = CreateObject("Excel.Application")
            objExcel_BASSA.workbooks.open(name_file_BASSA)
            objExcel_BASSA.visible = False


            alta_find = 0
            bassa_find = 0
            find_table = 0

            Tmax_exc = Math.Round(CInt(objExcel_BASSA.Worksheets("Riscaldamento Motore").Cells(27, 6).Value), 0)



            Do While RAW < 50 And find_table <> 1

                check_empty = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(RAW + 1, COLUMN + 1).Value)


                'Ho trovato una delle 2 tabelle
                If check_empty = "PTI" Then
                    find_table = find_table + 1


                    'Importo i vettori delle tabelle
                    Dim raw_tab As Integer = RAW + 2
                    Dim column_tab As Integer = COLUMN + 2
                    Dim elemento_TAB As String = "a"


                    Do While elemento_TAB <> ""

                        elemento_TAB = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value)

                        If check_empty = "PTI" Then


                            portata_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value)
                            Ptot_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value)
                            Pstat_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value)
                            RPM_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value)
                            Power_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value)
                            Curr_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value)
                            bassa_find = 1
                            len_bassa = raw_tab - RAW - 2


                            Dim cella_target As String = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, COLUMN + 1).Value)


                            If cella_target = "1" Then
                                pto1_bassa(0) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
                                pto1_bassa(1) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
                                pto1_bassa(2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
                                pto1_bassa(3) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
                                pto1_bassa(4) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
                                pto1_bassa(5) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR
                                pto1_bassa(6) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 6).Value) 'LWA
                            ElseIf cella_target = "2" Then
                                pto2_bassa(0) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
                                pto2_bassa(1) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
                                pto2_bassa(2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
                                pto2_bassa(3) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
                                pto2_bassa(4) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
                                pto2_bassa(5) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR
                                pto2_bassa(6) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 6).Value) 'LWA
                            ElseIf cella_target = "3" Then
                                pto3_bassa(0) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
                                pto3_bassa(1) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
                                pto3_bassa(2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
                                pto3_bassa(3) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
                                pto3_bassa(4) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
                                pto3_bassa(5) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR
                                pto3_bassa(6) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 6).Value) 'LWA



                            End If


                        End If

                        raw_tab = raw_tab + 1

                    Loop
                End If



                COLUMN = COLUMN + 1


                'Arrivato alla colonna 1000 scendo di una riga
                If COLUMN = 20 Then
                    COLUMN = 0
                    RAW = RAW + 1
                End If


            Loop



            '--------------------------------------------Ricavo numero prova e codice ventilatore----------------------------------------------
            Test_numero_bassa = CStr(objExcel_BASSA.Worksheets("Dati originali").Cells(4, 2).Value)
            descrizione_prova = CStr(objExcel_BASSA.Worksheets("Dati originali").Cells(4, 3).Value)
            Try

                Dim controllo_descrizione_prova_bassa As Integer = descrizione_prova.IndexOf("BASSA")
                Dim controllo_descrizione_prova_alta As Integer = descrizione_prova.IndexOf("ALTA")


                If controllo_descrizione_prova_bassa > -1 Then
                    descrizione_prova = descrizione_prova.Substring(0, controllo_descrizione_prova_bassa - 1)
                End If

                If controllo_descrizione_prova_alta > -1 Then
                    descrizione_prova = descrizione_prova.Substring(0, controllo_descrizione_prova_alta - 1)
                End If


            Catch ex As Exception

            End Try

            '-----------------------------------------------------------------------------------------------------------------------------------






            '--------------------------------------------Ricavo il punto di lavoro -------------------------------------------------------------
            RAW = 1
            check_empty = "u"



            Do While (check_empty <> "x" And RAW < 50)
                check_empty = CStr(objExcel_BASSA.Worksheets("Totale Dati").Cells(RAW + 1, 11).Value)
                RAW = RAW + 1
            Loop

            If RAW < 50 Then

                For i = 0 To 9
                    pto_lavoro_bassa(i) = CStr(objExcel_BASSA.Worksheets("Totale Dati").Cells(RAW, i + 1).Value)
                Next

            End If
            '------------------------------------------------------------------------------------------------------------------------------------




            '--------------------------------------------Acquisizione dati ERP-------------------------------------------------------------------
            Max_eff_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(4, 1).Value)
            target_eff_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(7, 1).Value)
            pow_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(23, 2).Value)
            Q_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(24, 2).Value)
            P_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(25, 2).Value)
            RPM_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(26, 2).Value)
            Cat_prova_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(18, 2).Value)
            cat_eff_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(19, 2).Value)


            Max_eff_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(4, 1).Value)
            target_eff_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(7, 1).Value)
            pow_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(23, 2).Value)
            Q_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(24, 2).Value)
            P_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(25, 2).Value)
            RPM_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(26, 2).Value)
            Cat_prova_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(18, 2).Value)
            cat_eff_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(19, 2).Value)

            '------------------------------------------------------------------------------------------------------------------------------------



            'chiudo il file
            objExcel_BASSA.DisplayAlerts = False

            Try
                objExcel_BASSA.ActiveWorkbook.close()
            Catch ex As Exception

            End Try




            'CHIUDO EXCEL
            objExcel_BASSA.quit()





        ElseIf S_D_var = 2 Then


            'Apro il file excel e cerco la posizione della tabella prestabilita (LA CELLA RICERCATA E' "PTI")
            objExcel_BASSA = CreateObject("Excel.Application")
            objExcel_BASSA.workbooks.open(name_file_BASSA)
            objExcel_BASSA.visible = False


            alta_find = 0
            bassa_find = 0
            find_table = 0




            Do While RAW < 50 And find_table <> 1

                check_empty = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(RAW + 1, COLUMN + 1).Value)


                'Ho trovato una delle 2 tabelle
                If check_empty = "PTI" Then
                    find_table = find_table + 1


                    'Importo i vettori delle tabelle
                    Dim raw_tab As Integer = RAW + 2
                    Dim column_tab As Integer = COLUMN + 2
                    Dim elemento_TAB As String = "a"


                    Do While elemento_TAB <> ""

                        elemento_TAB = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value)

                        If check_empty = "PTI" Then


                            portata_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value)
                            Ptot_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value)
                            Pstat_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value)
                            RPM_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value)
                            Power_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value)
                            Curr_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value)
                            bassa_find = 1
                            len_bassa = raw_tab - RAW - 2


                            Dim cella_target As String = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, COLUMN + 1).Value)


                            If cella_target = "1" Then
                                pto1_bassa(0) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
                                pto1_bassa(1) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
                                pto1_bassa(2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
                                pto1_bassa(3) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
                                pto1_bassa(4) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
                                pto1_bassa(5) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR
                                pto1_bassa(6) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 6).Value) 'LWA
                            ElseIf cella_target = "2" Then
                                pto2_bassa(0) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
                                pto2_bassa(1) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
                                pto2_bassa(2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
                                pto2_bassa(3) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
                                pto2_bassa(4) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
                                pto2_bassa(5) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR
                                pto2_bassa(6) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 6).Value) 'LWA
                            ElseIf cella_target = "3" Then
                                pto3_bassa(0) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
                                pto3_bassa(1) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
                                pto3_bassa(2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
                                pto3_bassa(3) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
                                pto3_bassa(4) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
                                pto3_bassa(5) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR
                                pto3_bassa(6) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 6).Value) 'LWA
                            End If


                        End If

                        raw_tab = raw_tab + 1

                    Loop
                End If



                COLUMN = COLUMN + 1


                'Arrivato alla colonna 1000 scendo di una riga
                If COLUMN = 20 Then
                    COLUMN = 0
                    RAW = RAW + 1
                End If


            Loop



            '--------------------------------------------Ricavo numero prova e codice ventilatore----------------------------------------------
            Test_numero_bassa = CStr(objExcel_BASSA.Worksheets("Dati originali").Cells(4, 2).Value)
            descrizione_prova = CStr(objExcel_BASSA.Worksheets("Dati originali").Cells(4, 3).Value)

            Dim controllo_descrizione_prova_bassa As Integer = descrizione_prova.IndexOf("BASSA")
            Dim controllo_descrizione_prova_alta As Integer = descrizione_prova.IndexOf("ALTA")


            If controllo_descrizione_prova_bassa > -1 Then
                descrizione_prova = descrizione_prova.Substring(0, controllo_descrizione_prova_bassa - 1)
            End If

            If controllo_descrizione_prova_alta > -1 Then
                descrizione_prova = descrizione_prova.Substring(0, controllo_descrizione_prova_alta - 1)
            End If

            '-----------------------------------------------------------------------------------------------------------------------------------


            '--------------------------------------------Ricavo il punto di lavoro -------------------------------------------------------------
            RAW = 1
            check_empty = "u"



            Do While (check_empty <> "x" And RAW < 50)
                check_empty = CStr(objExcel_BASSA.Worksheets("Totale Dati").Cells(RAW + 1, 11).Value)
                RAW = RAW + 1
            Loop

            If RAW < 50 Then

                For i = 0 To 9
                    pto_lavoro_bassa(i) = CStr(objExcel_BASSA.Worksheets("Totale Dati").Cells(RAW, i + 1).Value)
                Next

            End If
            '-------------------------------------------------------------------------------------------------------------------------------------


            '--------------------------------------------Acquisizione dati ERP-------------------------------------------------------------------
            Max_eff_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(4, 1).Value)
            target_eff_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(7, 1).Value)
            pow_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(23, 2).Value)
            Q_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(24, 2).Value)
            P_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(25, 2).Value)
            RPM_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(26, 2).Value)
            Cat_prova_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(18, 2).Value)
            cat_eff_ERP2013_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2013").Cells(19, 2).Value)


            Max_eff_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(4, 1).Value)
            target_eff_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(7, 1).Value)
            pow_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(23, 2).Value)
            Q_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(24, 2).Value)
            P_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(25, 2).Value)
            RPM_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(26, 2).Value)
            Cat_prova_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(18, 2).Value)
            cat_eff_ERP2015_bassa = CStr(objExcel_BASSA.Worksheets("Verifiche ERP 2015").Cells(19, 2).Value)

            '------------------------------------------------------------------------------------------------------------------------------------

            'save the file
            objExcel_BASSA.DisplayAlerts = False
            Try
                objExcel_BASSA.ActiveWorkbook.close()
            Catch ex As Exception

            End Try


            'Apro il file excel e cerco la posizione della tabella prestabilita (LA CELLA RICERCATA E' "PTI")
            objExcel_ALTA = CreateObject("Excel.Application")
            objExcel_ALTA.workbooks.open(name_file_ALTA)
            objExcel_ALTA.visible = False


            alta_find = 0
            alta_find = 0
            find_table = 0
            RAW = 1

            Tmax_exc = Math.Round(CInt(objExcel_ALTA.Worksheets("Riscaldamento Motore").Cells(27, 6).Value), 0)

            Do While RAW < 50 And find_table <> 1

                check_empty = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(RAW + 1, COLUMN + 1).Value)

                'Ho trovato una delle 2 tabelle
                If check_empty = "PTI" Then
                    find_table = find_table + 1


                    'Importo i vettori delle tabelle
                    Dim raw_tab As Integer = RAW + 2
                    Dim column_tab As Integer = COLUMN + 2
                    Dim elemento_TAB As String = "a"


                    Do While elemento_TAB <> ""

                        elemento_TAB = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value)

                        If check_empty = "PTI" Then


                            portata_Excel_alta(raw_tab - RAW - 2) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value)
                            Ptot_Excel_alta(raw_tab - RAW - 2) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value)
                            Pstat_Excel_alta(raw_tab - RAW - 2) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value)
                            RPM_Excel_alta(raw_tab - RAW - 2) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value)
                            Power_Excel_alta(raw_tab - RAW - 2) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value)
                            Curr_Excel_alta(raw_tab - RAW - 2) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value)
                            alta_find = 1
                            len_alta = raw_tab - RAW - 2


                            Dim cella_target As String = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, COLUMN + 1).Value)


                            If cella_target = "1" Then
                                pto1_alta(0) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
                                pto1_alta(1) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
                                pto1_alta(2) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
                                pto1_alta(3) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
                                pto1_alta(4) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
                                pto1_alta(5) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR
                                pto1_alta(6) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 6).Value) 'LWA
                            ElseIf cella_target = "2" Then
                                pto2_alta(0) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
                                pto2_alta(1) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
                                pto2_alta(2) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
                                pto2_alta(3) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
                                pto2_alta(4) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
                                pto2_alta(5) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR
                                pto2_alta(6) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 6).Value) 'LWA
                            ElseIf cella_target = "3" Then
                                pto3_alta(0) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
                                pto3_alta(1) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
                                pto3_alta(2) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
                                pto3_alta(3) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
                                pto3_alta(4) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
                                pto3_alta(5) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR
                                pto3_alta(6) = CStr(objExcel_ALTA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 6).Value) 'LWA
                            End If


                        End If

                        raw_tab = raw_tab + 1

                    Loop
                End If



                COLUMN = COLUMN + 1


                'Arrivato alla colonna 1000 scendo di una riga
                If COLUMN = 20 Then
                    COLUMN = 0
                    RAW = RAW + 1
                End If


            Loop



            '--------------------------------------------Ricavo numero prova e codice ventilatore----------------------------------------------
            Test_numero_alta = CStr(objExcel_ALTA.Worksheets("Dati originali").Cells(4, 2).Value)
            '-----------------------------------------------------------------------------------------------------------------------------------



            '--------------------------------------------Ricavo il punto di lavoro -------------------------------------------------------------
            RAW = 1
            check_empty = "u"



            Do While (check_empty <> "x" And RAW < 50)
                check_empty = CStr(objExcel_ALTA.Worksheets("Totale Dati").Cells(RAW + 1, 11).Value)
                RAW = RAW + 1
            Loop

            If RAW < 50 Then

                For i = 0 To 9
                    pto_lavoro_alta(i) = CStr(objExcel_ALTA.Worksheets("Totale Dati").Cells(RAW, i + 1).Value)
                Next

            End If
            '-------------------------------------------------------------------------------------------------------------------------------------


            '--------------------------------------------Acquisizione dati ERP-------------------------------------------------------------------
            Max_eff_ERP2013_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2013").Cells(4, 1).Value)
            target_eff_ERP2013_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2013").Cells(7, 1).Value)
            pow_ERP2013_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2013").Cells(23, 2).Value)
            Q_ERP2013_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2013").Cells(24, 2).Value)
            P_ERP2013_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2013").Cells(25, 2).Value)
            RPM_ERP2013_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2013").Cells(26, 2).Value)
            Cat_prova_ERP2013_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2013").Cells(18, 2).Value)
            cat_eff_ERP2013_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2013").Cells(19, 2).Value)


            Max_eff_ERP2015_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2015").Cells(4, 1).Value)
            target_eff_ERP2015_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2015").Cells(7, 1).Value)
            pow_ERP2015_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2015").Cells(23, 2).Value)
            Q_ERP2015_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2015").Cells(24, 2).Value)
            P_ERP2015_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2015").Cells(25, 2).Value)
            RPM_ERP2015_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2015").Cells(26, 2).Value)
            Cat_prova_ERP2015_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2015").Cells(18, 2).Value)
            cat_eff_ERP2015_alta = CStr(objExcel_ALTA.Worksheets("Verifiche ERP 2015").Cells(19, 2).Value)

            '------------------------------------------------------------------------------------------------------------------------------------


            'save the file
            objExcel_ALTA.DisplayAlerts = False
            Try
                objExcel_ALTA.ActiveWorkbook.close()
            Catch ex As Exception

            End Try



            'CHIUDO EXCEL
            objExcel_BASSA.quit()
            objExcel_ALTA.quit()

        End If





        ''Apro il file excel e cerco la posizione della tabella prestabilita (LA CELLA RICERCATA E' "PTI")
        'objExcel_BASSA = CreateObject("Excel.Application")
        'objExcel_BASSA.workbooks.open(name_file_BASSA)
        'objExcel_BASSA.visible = False


        ''Dim check_empty As String = "a"

        ''Dim COLUMN As Integer = 0
        ''Dim RAW As Integer = 0
        ''Dim find_table As Integer = 0

        'alta_find = 0
        'bassa_find = 0



        'Do While RAW < 500 And find_table <> 2

        '    check_empty = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(RAW + 1, COLUMN + 1).Value)


        '    'Ho trovato una delle 2 tabelle
        '    If check_empty = "PTI ALTA" Or check_empty = "PTI BASSA" Then
        '        find_table = find_table + 1


        '        'Importo i vettori delle tabelle
        '        Dim raw_tab As Integer = RAW + 2
        '        Dim column_tab As Integer = COLUMN + 2
        '        Dim elemento_TAB As String = "a"


        '        Do While elemento_TAB <> ""

        '            elemento_TAB = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value)

        '            If check_empty = "PTI ALTA" Then

        '                portata_Excel_alta(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value)
        '                Ptot_Excel_alta(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value)
        '                Pstat_Excel_alta(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value)
        '                RPM_Excel_alta(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value)
        '                Power_Excel_alta(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value)
        '                Curr_Excel_alta(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value)
        '                alta_find = 1
        '                len_alta = raw_tab - RAW - 2



        '                Dim cella_target As String = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, COLUMN + 1).Value)

        '                If cella_target = "1" Then
        '                    pto1_alta(0) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
        '                    pto1_alta(1) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
        '                    pto1_alta(2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
        '                    pto1_alta(3) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
        '                    pto1_alta(4) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
        '                    pto1_alta(5) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR
        '                ElseIf cella_target = "2" Then
        '                    pto2_alta(0) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
        '                    pto2_alta(1) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
        '                    pto2_alta(2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
        '                    pto2_alta(3) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
        '                    pto2_alta(4) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
        '                    pto2_alta(5) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR

        '                ElseIf cella_target = "3" Then
        '                    pto3_alta(0) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
        '                    pto3_alta(1) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
        '                    pto3_alta(2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
        '                    pto3_alta(3) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
        '                    pto3_alta(4) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
        '                    pto3_alta(5) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR
        '                End If




        '            ElseIf check_empty = "PTI BASSA" Then



        '                portata_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value)
        '                Ptot_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value)
        '                Pstat_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value)
        '                RPM_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value)
        '                Power_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value)
        '                Curr_Excel_bassa(raw_tab - RAW - 2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value)
        '                bassa_find = 1
        '                len_bassa = raw_tab - RAW - 2





        '                Dim cella_target As String = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, COLUMN + 1).Value)

        '                If cella_target = "1" Then
        '                    pto1_bassa(0) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
        '                    pto1_bassa(1) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
        '                    pto1_bassa(2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
        '                    pto1_bassa(3) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
        '                    pto1_bassa(4) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
        '                    pto1_bassa(5) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR
        '                ElseIf cella_target = "2" Then
        '                    pto2_bassa(0) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
        '                    pto2_bassa(1) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
        '                    pto2_bassa(2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
        '                    pto2_bassa(3) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
        '                    pto2_bassa(4) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
        '                    pto2_bassa(5) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR

        '                ElseIf cella_target = "3" Then
        '                    pto3_bassa(0) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab).Value) 'PORTATA
        '                    pto3_bassa(1) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 1).Value) 'PRESSIONE TOTALE
        '                    pto3_bassa(2) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 2).Value) 'PRESSIONE STATICA
        '                    pto3_bassa(3) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 3).Value) 'RPM
        '                    pto3_bassa(4) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 4).Value) 'POWER
        '                    pto3_bassa(5) = CStr(objExcel_BASSA.Worksheets("Catalogo").Cells(raw_tab, column_tab + 5).Value) 'CURR
        '                End If


        '            End If

        '            raw_tab = raw_tab + 1

        '        Loop
        '    End If



        '    COLUMN = COLUMN + 1


        '    'Arrivato alla colonna 1000 scendo di una riga
        '    If COLUMN = 1000 Then
        '        COLUMN = 0
        '        RAW = RAW + 1
        '    End If


        'Loop



        ''save the file
        'objExcel_BASSA.DisplayAlerts = False
        'objExcel_BASSA.ActiveWorkbook.close()









    End Sub





End Module
