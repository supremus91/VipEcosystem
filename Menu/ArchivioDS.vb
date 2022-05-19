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
Imports System.Data.SqlClient
Imports System.Configuration


Public Class ArchivioDS

    Private Sub ArchivioDS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: questa riga di codice carica i dati nella tabella 'Codifica_DescrizioneDataSet1.Totale'. È possibile spostarla o rimuoverla se necessario.
        Me.TotaleTableAdapter.Fill(Me.Codifica_DescrizioneDataSet1.Totale)

        Guna2DataGridView1.Select()

        'Guna2DataGridView1.ScrollBars = Nothing

        If nome_macchina = "Lorenzo" Then

            btn_Rigenera_Archivio.Visible = True
            btn_rigenera_disegni.Visible = True

        End If

        Try
            Guna2DataGridView1.FirstDisplayedScrollingRowIndex = Guna2DataGridView1.Rows(0).Index
            Guna2DataGridView1.Refresh()
            Guna2DataGridView1.CurrentCell = Guna2DataGridView1.Rows(0).Cells(1)
            Guna2DataGridView1.Rows(0).Selected = True
        Catch ex As Exception

        End Try


        If PJ_ref_star = "" Or PJ_ref_star = Nothing Then
            Directory_btn.Text = "Directory"
            Directory_btn.Image = My.Resources.folder1
        Else
            Directory_btn.Text = "Copy into Project"
            Directory_btn.Image = My.Resources.Copy
        End If



        Importa_Archivio_DS1()



        Guna2ComboBox1.SelectedIndex = 0


        'TODO: questa riga di codice carica i dati nella tabella 'Codifica_DescrizioneDataSet1.Frequenze'. È possibile spostarla o rimuoverla se necessario.
        Me.FrequenzeTableAdapter.Fill(Me.Codifica_DescrizioneDataSet1.Frequenze)
        'TODO: questa riga di codice carica i dati nella tabella 'Codifica_DescrizioneDataSet1.Tensioni'. È possibile spostarla o rimuoverla se necessario.
        Me.TensioniTableAdapter.Fill(Me.Codifica_DescrizioneDataSet1.Tensioni)
        'TODO: questa riga di codice carica i dati nella tabella 'Codifica_DescrizioneDataSet1.Poli'. È possibile spostarla o rimuoverla se necessario.
        Me.PoliTableAdapter.Fill(Me.Codifica_DescrizioneDataSet1.Poli)
        'TODO: questa riga di codice carica i dati nella tabella 'Codifica_DescrizioneDataSet1.Gradazione'. È possibile spostarla o rimuoverla se necessario.
        Me.GradazioneTableAdapter.Fill(Me.Codifica_DescrizioneDataSet1.Gradazione)
        'TODO: questa riga di codice carica i dati nella tabella 'Codifica_DescrizioneDataSet1.Diametri'. È possibile spostarla o rimuoverla se necessario.
        Me.DiametriTableAdapter.Fill(Me.Codifica_DescrizioneDataSet1.Diametri)
        'TODO: questa riga di codice carica i dati nella tabella 'Codifica_DescrizioneDataSet1.Numeropale'. È possibile spostarla o rimuoverla se necessario.
        Me.NumeropaleTableAdapter.Fill(Me.Codifica_DescrizioneDataSet1.Numeropale)
        'TODO: questa riga di codice carica i dati nella tabella 'Codifica_DescrizioneDataSet1.TipoVentola'. È possibile spostarla o rimuoverla se necessario.
        Me.TipoVentolaTableAdapter.Fill(Me.Codifica_DescrizioneDataSet1.TipoVentola)
        'TODO: questa riga di codice carica i dati nella tabella 'Codifica_DescrizioneDataSet1.ProfiliVentola'. È possibile spostarla o rimuoverla se necessario.
        Me.ProfiliVentolaTableAdapter.Fill(Me.Codifica_DescrizioneDataSet1.ProfiliVentola)
        'TODO: questa riga di codice carica i dati nella tabella 'Codifica_DescrizioneDataSet.TipoMotore'. È possibile spostarla o rimuoverla se necessario.
        Me.TipoMotoreTableAdapter.Fill(Me.Codifica_DescrizioneDataSet.TipoMotore)



        Dim add_DS As Integer = 0

        Guna2DataGridView1.Rows.Clear()

        For j = 0 To 4
            vect_DS_state_TOT(j) = 0
        Next

        Ds_completi = 0




        For i As Integer = 0 To numero_DS - 1

            Guna2DataGridView1.Rows.Add()

            Try
                Search_folder(DS_lista(i, 1))
            Catch ex As Exception

            End Try


            For j = 0 To 9
                Guna2DataGridView1.Rows(add_DS).Cells(j).Value = DS_lista(i, j)
            Next

            Guna2DataGridView1.Rows(add_DS).Cells(10).Value = DS_lista(i, 15) 'Frequenza
            Guna2DataGridView1.Rows(add_DS).Cells(11).Value = DS_lista(i, 10) 'Tmax
            Guna2DataGridView1.Rows(add_DS).Cells(12).Value = DS_lista(i, 11) 'Tmin


            '---------------------------------BLOCCO GESTIONE PRESENZA FILE-----------------------------------------
            For y = 0 To 1


                If vect_DS_state(y) = 1 Then
                    Guna2DataGridView1.Rows(i).Cells(13 + y).Value = My.Resources.Resources.Green_L
                Else
                    Guna2DataGridView1.Rows(i).Cells(13 + y).Value = My.Resources.Resources.Red_L
                End If

                Guna2DataGridView1.Columns(13 + y).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

            Next



            '------------------------------------------------controllo che il ventilatore sia erp---------------------------------------------
            ERP1(i)

            If ERP_ok = 1 Then
                Guna2DataGridView1.Rows(add_DS).Cells(15).Value = "SI"
            Else
                Guna2DataGridView1.Rows(add_DS).Cells(15).Value = "NO"
                Guna2DataGridView1.Rows(add_DS).Cells(15).Style.ForeColor = Color.Red
            End If
            '----------------------------------------------------------------------------------------------------------------------------------
            Guna2DataGridView1.Rows(i).Cells(16).Value = My.Resources.Resources.Search_small
            Guna2DataGridView1.Rows(i).Cells(17).Value = DS_totale_dati(i, 87)




            '-----------------------------------------------------------------------------------------------------------

            add_DS = add_DS + 1

        Next






        'CONTATORI DEI DATASHEETS
        Label1.Text = numero_DS


        Label16.Text = numero_DS - vect_DS_state_TOT(0) + 1
        Label3.Text = numero_DS - vect_DS_state_TOT(1)
        Label10.Text = numero_DS - vect_DS_state_TOT(2)
        Label8.Text = numero_DS - vect_DS_state_TOT(3)
        Label18.Text = numero_DS - vect_DS_state_TOT(4)
        Label4.Text = Ds_completi



        If mod_PJ_DS = 1 Then
            Directory_btn.Text = "Copy into Project"
            Directory_btn.Image = My.Resources.Copy

        Else

            Directory_btn.Text = "Directory"
            Directory_btn.Image = My.Resources.folder1

        End If

        'per il form warning_copy
        NomeFolder_selezionata = Guna2DataGridView1.Rows(0).Cells(1).Value

        Filtro_Prova.SelectedIndex = 1


    End Sub




    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click


        Datasheet_New_exc = -1

        If warning_archivio_DS.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
            'attendo la risposta della box
        End If


        PJ_ref = "None"
        mod_archivio = 0

        Try
            DatasheetMenu.Close()
        Catch ex As Exception

        End Try

        DatasheetMenu.Show()



    End Sub




    Private Sub Guna2DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellDoubleClick


        'Accesso alla modifica dei ventilatori
        If nome_macchina = "Lorenzo" Or nome_macchina = "Stefano" Or nome_macchina = "Paolo" Or nome_macchina = "Fausto" Or nome_macchina = "Alberto" Or nome_macchina = "LAPTOP-BB6VDD4D" Then

            Datasheet_New_exc = -1

            DB_pasticci_on = 0

            lista_DS_sel = Guna2DataGridView1.CurrentRow.Index
            lista_DS_sel_name = Guna2DataGridView1.Rows(lista_DS_sel).Cells(1).Value

            load_datasheet(lista_DS_sel_name)

        End If


    End Sub





    Public Sub load_datasheet(Nome_DS)

        lista_DS_sel_name = Nome_DS

        mod_archivio = 1

        load_end = 0

        Timer1.Start()


        'ATTIVA IL LOADING
        Guna2Panel2.Visible = True  '------> ESEGUIRE IN PARALLELO
        Guna2Panel2.BringToFront()  '------> ESEGUIRE IN PARALLELO

        Application.DoEvents()

        Dim task1 As Task = Task.Run(Sub() loadin_view())
        Control.CheckForIllegalCrossThreadCalls = False

        Do While (task1.IsCompleted = False)
            Application.DoEvents()
        Loop

        Try
            DatasheetMenu.Close()
        Catch ex As Exception

        End Try

        load_var = 0

        'DatasheetMenu.Show()
        form_DatasheetMenu = New DatasheetMenu
        form_DatasheetMenu.Show()

    End Sub





    Public Sub loadin_view()


        'Try
        '    DatasheetMenu.Close()
        'Catch ex As Exception

        'End Try

        'load_var = 0

        'DatasheetMenu.Show()


    End Sub



    Public Sub ricerca_DB()



        Dim Tipo_motore As String
        Dim Tipo_ventilatore As String
        Dim diametro As String
        Dim calettamento As String
        Dim tensione As String
        Dim frequenza As String
        Dim Npale As String
        Dim Profilo_pala As String
        Dim poli As String
        Dim Tmin As Integer
        Dim Tmax As Integer
        Dim Tipo_provaDS As String




        'If Q_target.Text <> "" And P_target.Text <> "" Then

        Dim add_DS As Integer = 0

            Guna2DataGridView1.Rows.Clear()



        Dim A_par As Double
            Dim B_par As Double
            Dim C_par As Double
            Dim P_par As Double
        Dim Q_par As Double

        Try
            Q_par = Q_target.Text
        Catch ex As Exception
            Q_par = 0
        End Try


        Dim P_target1 As Double 



        Try
            P_target1 = P_target.Text
        Catch ex As Exception
            P_target1 = 0
        End Try


        For i As Integer = 0 To numero_DS - 1



            'Ottengo la codifica
            Tipo_motore = DS_lista(i, 1)(0)
            Tipo_ventilatore = DS_lista(i, 1)(1)
            diametro = DS_lista(i, 1)(4) & DS_lista(i, 1)(5) & DS_lista(i, 1)(6)
            calettamento = DS_lista(i, 1)(8) & DS_lista(i, 1)(9)
            poli = DS_lista(i, 1)(11)
            tensione = DS_lista(i, 1)(17)
            frequenza = DS_lista(i, 1)(18)
            Profilo_pala = DS_lista(i, 1)(19)
            Npale = DS_lista(i, 1)(20)
            Tipo_provaDS = DS_lista(i, 3)


            'assegno i valori che ricava dal databse 
            Motore_Filtro = Filtro_Motore.SelectedItem
            Serie_Filtro = Filtro_Serie.SelectedItem
            Profilo_Filtro = Filtro_Profilo.SelectedItem
            Pale_Filtro = Filtro_Pale.SelectedItem
            Diametro_Filtro = Filtro_Diametro.SelectedItem
            Calettamento_Filtro = Filtro_Calettamento.SelectedItem
            Poli_Filtro = Filtro_Poli.SelectedItem
            Tensione_Filtro = Filtro_Tensione.SelectedItem
            Frequenza_Filtro = Filtro_Frequenza.SelectedItem
            Prova_Filtro = Filtro_Prova.SelectedItem


            Tmin = DS_lista(i, 11)
            Tmax = DS_lista(i, 10)


            If (Filtro_Motore.SelectedIndex < 1 Or Motore_Filtro(0) = Tipo_motore) And (Filtro_Serie.SelectedIndex < 1 Or Serie_Filtro(0) = Tipo_ventilatore) And (Filtro_Profilo.SelectedIndex < 1 Or Profilo_Filtro(0) = Profilo_pala) And (Filtro_Pale.SelectedIndex < 1 Or Pale_Filtro(0) = Npale) And (Filtro_Diametro.SelectedIndex < 1 Or Diametro_Filtro(0) = diametro) And (Filtro_Calettamento.SelectedIndex < 1 Or Calettamento_Filtro(0) = calettamento) And (Filtro_Tensione.SelectedIndex < 1 Or Tensione_Filtro(0) = tensione) And (Filtro_Frequenza.SelectedIndex < 1 Or Frequenza_Filtro(0) = frequenza) And (Filtro_Poli.SelectedIndex < 1 Or Poli_Filtro(0) = poli) And (Tipo_provaDS = Prova_Filtro Or Filtro_Prova.SelectedIndex = 0) Then


                Dim filtro1_Tmax As Integer = 0
                Dim filtro1_Tmin As Integer = 0


                If Filtro_Tmin.Text <> "" Then 'controllo la temperatura minima
                    If Tmin >= Filtro_Tmin.Text Then
                        filtro1_Tmin = 1
                    End If
                Else
                    filtro1_Tmin = 1
                End If




                If Filtro_Tmax.Text <> "" Then 'controllo la temperatura massima
                    If Tmax <= Filtro_Tmax.Text Then
                        filtro1_Tmax = 1
                    End If
                Else
                    filtro1_Tmax = 1
                End If


                If filtro1_Tmin = 1 And filtro1_Tmax = 1 Then ' controllo delle temperature



                    '---------------------------ricerca pressione portata---------------------------------
                    Try

                        If DS_lista(i, 12) = 0 Then
                            'singola valocità 
                            A_par = DS_totale_dati(i, 68)
                            B_par = DS_totale_dati(i, 69)
                            C_par = DS_totale_dati(i, 70)
                            P_par = A_par * Q_par ^ 2 + B_par * Q_par + C_par

                        Else
                            'doppia velocità
                            A_par = DS_lista(i, 12)
                            B_par = DS_lista(i, 13)
                            C_par = DS_lista(i, 14)
                            P_par = A_par * Q_par ^ 2 + B_par * Q_par + C_par

                        End If


                    Catch ex As Exception

                    End Try


                    '-------------------------------------------------------------------------------------



                    Dim Qmin_fan As Double
                    Dim Qmin_fan1 As Double
                    Dim Qmin_fan2 As Double

                    Dim Qmax_fan As Double
                    Dim Qmax_fan1 As Double
                    Dim Qmax_fan2 As Double

                    If DS_totale_dati(i, 62) <> "" Then
                        'doppia velocità

                        Qmin_fan1 = DS_totale_dati(i, 62)
                        Qmin_fan2 = DS_totale_dati(i, 44)

                        If Qmin_fan1 > Qmin_fan2 Then
                            Qmin_fan = Qmin_fan1
                        Else
                            Qmin_fan = Qmin_fan2
                        End If

                        Qmax_fan1 = DS_totale_dati(i, 32)
                        Qmax_fan2 = DS_totale_dati(i, 50)


                        If Qmax_fan1 > Qmax_fan2 Then
                            Qmax_fan = Qmax_fan1
                        Else
                            Qmax_fan = Qmax_fan2
                        End If



                    Else 'singola velocità

                        Try
                            Qmin_fan = DS_totale_dati(i, 44)
                            Qmax_fan = DS_totale_dati(i, 32)
                        Catch ex As Exception

                        End Try



                    End If



                    If ((P_target1 * 0.8 < P_par And P_target1 * 1.2 > P_par) And (Q_par > Qmin_fan And Q_par < Qmax_fan)) Or (Q_target.Text = "" Or P_target.Text = "") Then

                        Guna2DataGridView1.Rows.Add()

                        For j = 0 To 9
                            Guna2DataGridView1.Rows(add_DS).Cells(j).Value = DS_lista(i, j)
                        Next

                        Guna2DataGridView1.Rows(add_DS).Cells(10).Value = DS_lista(i, 15) 'Frequenza
                        Guna2DataGridView1.Rows(add_DS).Cells(11).Value = DS_lista(i, 10) 'Tmax
                        Guna2DataGridView1.Rows(add_DS).Cells(12).Value = DS_lista(i, 11) 'Tmin

                        Try
                            Search_folder(DS_lista(i, 1))
                        Catch ex As Exception

                        End Try



                        '---------------------------------BLOCCO GESTIONE PRESENZA FILE-----------------------------------------
                        For y = 0 To 1


                            If vect_DS_state(y) = 1 Then
                                Guna2DataGridView1.Rows(add_DS).Cells(13 + y).Value = My.Resources.Resources.Green_L
                            Else
                                Guna2DataGridView1.Rows(add_DS).Cells(13 + y).Value = My.Resources.Resources.Red_L
                            End If

                            Guna2DataGridView1.Columns(13 + y).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                        Next

                        '------------------------------------------------controllo che il ventilatore sia erp---------------------------------------------
                        ERP1(i)

                        If ERP_ok = 1 Then
                            Guna2DataGridView1.Rows(add_DS).Cells(15).Value = "SI"
                        Else
                            Guna2DataGridView1.Rows(add_DS).Cells(15).Value = "NO"
                            Guna2DataGridView1.Rows(add_DS).Cells(15).Style.ForeColor = Color.Red
                        End If
                        '----------------------------------------------------------------------------------------------------------------------------------

                        Guna2DataGridView1.Rows(add_DS).Cells(16).Value = My.Resources.Resources.Search_small

                        Guna2DataGridView1.Rows(add_DS).Cells(17).Value = DS_totale_dati(i, 87)
                        '-----------------------------------------------------------------------------------------------------------




                        add_DS = add_DS + 1

                    End If

                End If

            End If
        Next

        'End If

    End Sub



    Private Sub Q_target_TextChanged(sender As Object, e As EventArgs) Handles Q_target.TextChanged

        Try
            ricerca_DB()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub P_target_TextChanged(sender As Object, e As EventArgs) Handles P_target.TextChanged

        Try
            ricerca_DB()
        Catch ex As Exception

        End Try


    End Sub




    Public Sub Search_folder(nome_file_arc)


        Dim files() As String = IO.Directory.GetFiles(folders_directoryDS & "\" & nome_file_arc)



        'azzero il vettore di controllo
        For j = 0 To 4
            vect_DS_state(j) = 0
        Next

        For Each file As String In files

            Dim file_name As String = Path.GetFileName(file)
            Dim file_estensione As String = Path.GetExtension(file)
            'file_name.Substring(file_name.IndexOf(".") + 1, file_name.Length - 1 - file_name.IndexOf("."))


            If file_estensione = ".pdf" And (file_name.Substring(0, 6) <> "Drawing") Then
                vect_DS_state(0) = 1
                'vect_DS_state_TOT(0) = vect_DS_state_TOT(0) + 1
            End If

            If file_estensione = ".xlsx" Then
                vect_DS_state(1) = 1
                'vect_DS_state_TOT(1) = vect_DS_state_TOT(1) + 1
            End If


            If file_estensione = ".vip" Then
                vect_DS_state(2) = 1
                'vect_DS_state_TOT(1) = vect_DS_state_TOT(1) + 1
            End If


            'If file_estensione = ".STEP" Then
            '    vect_DS_state(2) = 1
            '    vect_DS_state_TOT(2) = vect_DS_state_TOT(2) + 1
            'End If


            'If file_estensione = ".DWG" Then
            '    vect_DS_state(3) = 1
            '    vect_DS_state_TOT(3) = vect_DS_state_TOT(3) + 1
            'End If


            'If (file_name.Substring(0, 7) = "Drawing") Then
            '    vect_DS_state(4) = 1
            '    vect_DS_state_TOT(4) = vect_DS_state_TOT(4) + 1
            'End If

        Next


    End Sub



    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Directory_btn.Click

        If mod_PJ_DS = 0 Then
            Process.Start("explorer.exe", folders_directoryDS & "\" & Guna2DataGridView1.Rows(Guna2DataGridView1.CurrentRow.Index).Cells(1).Value)
        Else

            Yes_No_Warning = 0

            If Warning_copy_folder.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
                'attendo la risposta della box
            End If


            If Yes_No_Warning = 1 Then
                'For Each foundFile As String In My.Computer.FileSystem.GetFiles(folders_directoryDS & "\" & Guna2DataGridView1.Rows(Guna2DataGridView1.CurrentRow.Index).Cells(1).Value, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.rtf")

                Dim folder_search As String = folders_directoryDS & "\" & Guna2DataGridView1.Rows(Guna2DataGridView1.CurrentRow.Index).Cells(1).Value & "\" & Warning_folder_name
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(folder_search)

                    Dim file_name As String = Path.GetFileName(foundFile)

                    'IO.File.Copy(foundFile, folders_directory & "\" & PJ_ref_star & "\" & file_name, True)
                    Dim directory_to_send As String = folders_directory & "\" & PJ_ref_star & "\Rev" & num_rev_generale
                    IO.File.Copy(foundFile, directory_to_send & "\" & file_name, True)
                Next

            End If

        End If


    End Sub



    Private Sub Guna2DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellClick


        NomeFolder_selezionata = Guna2DataGridView1.Rows(Guna2DataGridView1.CurrentRow.Index).Cells(1).Value


        Dim colonna_sel As Integer = e.ColumnIndex


        If e.ColumnIndex = 16 Then

            FolderViewer.Show()

        End If


        If e.ColumnIndex = 17 Then
            Modifica_check_sito1(Guna2DataGridView1.Rows(Guna2DataGridView1.CurrentRow.Index).Cells(17).Value, Guna2DataGridView1.Rows(Guna2DataGridView1.CurrentRow.Index).Cells(1).Value)
            Importa_Archivio_DS1()
        End If

    End Sub



    Private Sub Guna2ComboBox11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Filtro_Motore.SelectedIndexChanged
        Try
            ricerca_DB()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Guna2ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Filtro_Serie.SelectedIndexChanged
        Try
            ricerca_DB()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Guna2ComboBox10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Filtro_Profilo.SelectedIndexChanged
        Try
            ricerca_DB()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Guna2ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Filtro_Pale.SelectedIndexChanged
        Try
            ricerca_DB()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Guna2ComboBox9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Filtro_Diametro.SelectedIndexChanged
        Try
            ricerca_DB()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Guna2ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Filtro_Calettamento.SelectedIndexChanged
        Try
            ricerca_DB()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Guna2ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Filtro_Poli.SelectedIndexChanged
        Try
            ricerca_DB()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Guna2ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Filtro_Tensione.SelectedIndexChanged
        Try
            ricerca_DB()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Guna2ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Filtro_Frequenza.SelectedIndexChanged
        Try
            ricerca_DB()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Guna2TextBox2_TextChanged(sender As Object, e As EventArgs) Handles Filtro_Tmin.TextChanged
        Try
            ricerca_DB()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Filtro_Tmax_TextChanged(sender As Object, e As EventArgs) Handles Filtro_Tmax.TextChanged
        Try
            ricerca_DB()
        Catch ex As Exception

        End Try
    End Sub





    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox1.SelectedIndexChanged

        '---------------------------------------------------Completamento dell textbox di ricerca-----------------------------------------------------------

        Dim vect1 As New AutoCompleteStringCollection
        'Dim view As New DataView(tables1(0))


        'Inserisco nella textbox i valore che deve ricercare
        Select Case Guna2ComboBox1.SelectedIndex
            Case 0 'Descrizione
                For i As Integer = 0 To numero_DS - 1
                    vect1.Add(DS_lista(i, 1))
                Next

            Case 1 'Test numero
                For i As Integer = 0 To numero_DS - 1
                    vect1.Add(DS_lista(i, 2))
                Next
        End Select


        Guna2TextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource
        Guna2TextBox1.AutoCompleteCustomSource = vect1
        Guna2TextBox1.AutoCompleteMode = AutoCompleteMode.Suggest


        '---------------------------------------------------------------------------------------------------------------------------------------------------

    End Sub




    Public Sub ricerca_DB_upper()


        Dim add_DS As Integer = 0


        Guna2DataGridView1.Rows.Clear()


        For i As Integer = 0 To numero_DS - 1

            Dim Nome_sel As String


            'effettuo la ricerca anche a nome parziale
            If Guna2ComboBox1.SelectedIndex = 0 Then
                Nome_sel = DS_lista(i, 1).Substring(0, lunghezza_testo) 'il nome selezionato e' la descrizione
            Else
                Nome_sel = DS_lista(i, 2).Substring(0, lunghezza_testo) 'il nome selezionato e' il numero della prova
            End If




            If Nome_sel.ToLower = testo_ricerca.ToLower Then


                Guna2DataGridView1.Rows.Add()

                For j = 0 To 9
                    Guna2DataGridView1.Rows(add_DS).Cells(j).Value = DS_lista(i, j)
                Next

                Guna2DataGridView1.Rows(add_DS).Cells(10).Value = DS_lista(i, 15) 'Frequenza
                Guna2DataGridView1.Rows(add_DS).Cells(11).Value = DS_lista(i, 10) 'Tmax
                Guna2DataGridView1.Rows(add_DS).Cells(12).Value = DS_lista(i, 11) 'Tmin

                Try
                    Search_folder(DS_lista(i, 1))
                Catch ex As Exception

                End Try



                '---------------------------------BLOCCO GESTIONE PRESENZA FILE-----------------------------------------
                For y = 0 To 2


                    If vect_DS_state(y) = 1 Then
                        Guna2DataGridView1.Rows(add_DS).Cells(13 + y).Value = My.Resources.Resources.Green_L
                    Else
                        Guna2DataGridView1.Rows(add_DS).Cells(13 + y).Value = My.Resources.Resources.Red_L
                    End If

                    Guna2DataGridView1.Columns(13 + y).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                Next

                '------------------------------------------------controllo che il ventilatore sia erp---------------------------------------------
                ERP1(i)

                If ERP_ok = 1 Then
                    Guna2DataGridView1.Rows(add_DS).Cells(15).Value = "SI"
                Else
                    Guna2DataGridView1.Rows(add_DS).Cells(15).Value = "NO"
                    Guna2DataGridView1.Rows(add_DS).Cells(15).Style.ForeColor = Color.Red
                End If

                '----------------------------------------------------------------------------------------------------------------------------------


                Guna2DataGridView1.Rows(add_DS).Cells(16).Value = My.Resources.Resources.Search_small

                Guna2DataGridView1.Rows(add_DS).Cells(17).Value = DS_totale_dati(i, 87)

                add_DS = add_DS + 1

            End If


        Next



    End Sub




    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged



        testo_ricerca = Guna2TextBox1.Text
        lunghezza_testo = testo_ricerca.Length

        Try
            ricerca_DB_upper()
        Catch ex As Exception

        End Try



    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If load_end = 1 Then

            'ATTIVA IL LOADING
            Guna2Panel2.Visible = False  '------> ESEGUIRE IN PARALLELO
            Guna2Panel2.SendToBack()  '------> ESEGUIRE IN PARALLELO

            Timer1.Stop()
        End If


    End Sub


    Private Sub Guna2CircleButton1_MouseHover(sender As Object, e As EventArgs) Handles btn_Rigenera_Archivio.MouseHover
        Me.Cursor = Cursors.Hand
        Me.Refresh()
    End Sub

    Private Sub Guna2CircleButton1_MouseLeave(sender As Object, e As EventArgs) Handles btn_Rigenera_Archivio.MouseLeave
        Me.Cursor = Cursors.Arrow
        Me.Refresh()
    End Sub



    Private Sub btn_Rigenera_Archivio_Click(sender As Object, e As EventArgs) Handles btn_Rigenera_Archivio.Click

        DB_pasticci_on = 0
        Datasheet_New_exc = -1

        Yes_No_Warning = 0
        Warning.Label1.Text = "    Do you want to regenerate all Datasheets?"
        If Warning.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
            'attendo la risposta della box
        End If


        If Yes_No_Warning = 1 Then ' in caso di risposta affermaiva allora aggiorno

            'killo tutti i dati dentro la tabella Archivio del SQL SERVER
            SQLSERVER_Delete_allData1()

            Procedura_rigenera_DS()
        End If

    End Sub



    Public Sub Procedura_rigenera_DS()

        'ATTIVA IL LOADING
        Guna2Panel2.Visible = True  '------> ESEGUIRE IN PARALLELO
        Guna2Panel2.BringToFront()  '------> ESEGUIRE IN PARALLELO
        Label22.Text = ""
        Label22.Location = New System.Drawing.Point(500, 490)
        Guna2PictureBox1.Image = My.Resources.Heal
        Application.DoEvents()




        Datasheet_print_mode = 1
        load_datasheet(0)





        'ATTIVA IL LOADING
        Guna2Panel2.Visible = False  '------> ESEGUIRE IN PARALLELO
        Guna2Panel2.SendToBack()  '------> ESEGUIRE IN PARALLELO
        Label22.Text = "LOADING..."
        Label22.Location = New System.Drawing.Point(738, 490)
        Guna2PictureBox1.Image = My.Resources.loading1

        Application.DoEvents()

        Datasheet_print_mode = 0



    End Sub

    Private Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal hProcess As IntPtr, ByVal dwMinimumWorkingSetSize As Int32, ByVal dwMaximumWorkingSetSize As Int32) As Int32



    Friend Sub ReleaseMemory()
        Try
            GC.Collect()
            GC.WaitForPendingFinalizers()
            If Environment.OSVersion.Platform = PlatformID.Win32NT Then
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1)
            End If
        Catch ex As Exception

        End Try
    End Sub




    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked

        Video_Name = Cartella_Generale_Tutorial & "\Tutorial1.mp4"

        Try
            VideoTutorialMenu.Close()
        Catch ex As Exception

        End Try


        VideoTutorialMenu.Show()



    End Sub



    Private Sub btn_rigenera_disegni_Click(sender As Object, e As EventArgs) Handles btn_rigenera_disegni.Click

        Yes_No_Warning = 0
        Warning.Label1.Text = "    Do you want to regenerate all Drawings?"
        If Warning.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
            'attendo la risposta della box
        End If


        'ATTIVA IL LOADING
        Guna2Panel2.Visible = True  '------> ESEGUIRE IN PARALLELO
        Guna2Panel2.BringToFront()  '------> ESEGUIRE IN PARALLELO
        Label22.Text = ""
        Label22.Location = New System.Drawing.Point(500, 490)
        Guna2PictureBox1.Image = My.Resources.sw_icon
        Application.DoEvents()




        If Yes_No_Warning = 1 Then
            Copia_Disegni1()
        End If





        'ATTIVA IL LOADING
        Guna2Panel2.Visible = False  '------> ESEGUIRE IN PARALLELO
        Guna2Panel2.SendToBack()  '------> ESEGUIRE IN PARALLELO
        Label22.Text = "LOADING..."
        Label22.Location = New System.Drawing.Point(738, 490)
        Guna2PictureBox1.Image = My.Resources.loading1

        Application.DoEvents()





    End Sub

    Private Sub Filtro_Prova_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Filtro_Prova.SelectedIndexChanged
        Try
            ricerca_DB()
        Catch ex As Exception

        End Try
    End Sub
End Class