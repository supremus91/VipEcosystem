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
Imports DevExpress.XtraPrinting.Control
Imports DevExpress.XtraPrinting.Localization
Imports DevExpress.XtraPrinting.Native.ExportOptionsControllers
Imports DevExpress.XtraPrinting.Native
Imports Microsoft.VisualBasic
Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Core
Imports VipDesignerUM.SwOpenFile

Public Class SW_interface

    'Public Serie_ventola As String
    'Public Serie_motore As String
    'Public Config_ventilatore As String
    'Public Flusso As String



    Private Sub SW_interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet12.TipoPala'. È possibile spostarla o rimuoverla se necessario.
        Me.TipoPalaTableAdapter.Fill(Me.DataBaseSWDataSet12.TipoPala)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet1.Angolo'. È possibile spostarla o rimuoverla se necessario.
        Me.AngoloTableAdapter.Fill(Me.DataBaseSWDataSet1.Angolo)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet1.Diametri'. È possibile spostarla o rimuoverla se necessario.
        Me.DiametriTableAdapter.Fill(Me.DataBaseSWDataSet1.Diametri)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet.Ventilatori'. È possibile spostarla o rimuoverla se necessario.
        Me.VentilatoriTableAdapter.Fill(Me.DataBaseSWDataSet.Ventilatori)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet.Motore'. È possibile spostarla o rimuoverla se necessario.
        Me.MotoreTableAdapter.Fill(Me.DataBaseSWDataSet.Motore)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet.SerieVentola'. È possibile spostarla o rimuoverla se necessario.
        Me.SerieVentolaTableAdapter.Fill(Me.DataBaseSWDataSet.SerieVentola)

        cbx_ventola.SelectedIndex = -1
        cbx_motore.SelectedIndex = -1
        cbx_ventilatore.SelectedIndex = -1
        cbx_diametro.SelectedIndex = -1
        cbx_angolo.SelectedIndex = -1

        Serie_ventola = ""
        Serie_motore = ""
        Config_ventilatore = ""
        Flusso = ""
        Npale_ventola = ""



    End Sub



    Private Sub Serie_ventola_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_ventola.SelectedIndexChanged

        Try
            Dim nome_ventolaDR As DataRowView = cbx_ventola.SelectedItem


            If nome_ventolaDR(1)(0) <> "W" Then 'caso ventole vip
                Serie_ventola = nome_ventolaDR(1)(1)
                Npale_ventola = nome_ventolaDR(1)(2)

                Try
                    Profilo_ventola = nome_ventolaDR(1)(6)
                Catch ex As Exception
                    Profilo_ventola = 0
                End Try

            ElseIf nome_ventolaDR(1).Length = 2 Then 'caso ventole MW
                Serie_ventola = nome_ventolaDR(1)(1)
                Npale_ventola = nome_ventolaDR(1)(0)

                Try
                    Profilo_ventola = nome_ventolaDR(1)(5)
                Catch ex As Exception
                    Profilo_ventola = 0
                End Try

            End If
        Catch ex As Exception

        End Try


        sblocca_diam_ang()


    End Sub


    Private Sub cbx_pala_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_pala.SelectedIndexChanged

        Dim Tipo_ventolaDR As DataRowView = cbx_pala.SelectedItem

        TipoPala = Tipo_ventolaDR(1)(0)

    End Sub

    Private Sub Serie_motore_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_motore.SelectedIndexChanged

        Try
            Dim Serie_motoreDR As DataRowView = cbx_motore.SelectedItem
            Serie_motore = Serie_motoreDR(1)
        Catch ex As Exception

        End Try

        sblocca_diam_ang()

    End Sub


    Private Sub cbx_ventilatore_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_ventilatore.SelectedIndexChanged

        Try
            Dim Config_ventilatoreDR As DataRowView = cbx_ventilatore.SelectedItem
            Config_ventilatore = Config_ventilatoreDR(1)
        Catch ex As Exception

        End Try

        sblocca_diam_ang()


    End Sub


    Private Sub cbx_flusso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_flusso.SelectedIndexChanged

        Flusso = cbx_flusso.SelectedItem
        sblocca_diam_ang()


    End Sub


    Public Sub sblocca_diam_ang()

        If Serie_ventola <> "" And Serie_motore <> "" And Config_ventilatore <> "" And Flusso <> "" And Npale_ventola <> "" Then
            Pan2.Visible = True
            'lb_NomeFile.Visible = True

            'caso standard
            file_exc_name = Serie_motore & Serie_ventola & " _._._ -_._-_._-" & Config_ventilatore & "_-_._." & Profilo_ventola & Npale_ventola & "_-" & Flusso

            Lettura_RefExcel1()

            accessories_display()

        Else
            Pan2.Visible = False
            Pan3.Visible = False
        End If

    End Sub






    Public Sub accessories_display()

        'Rendo tutte le check invisibili

        For Each item As Control In Pan3.Controls

            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                item.Visible = False
            End If

        Next




        For i = 0 To Numero_colonne_SW - 1

            Try

                Dim check As Guna.UI2.WinForms.Guna2CheckBox
                check = Pan3.Controls("Check" & i + 1)

                check.Text = Nome_colonne_SW(i + 1)
                check.Visible = True

            Catch ex As Exception

            End Try

        Next



    End Sub


    Private Sub Diametro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_diametro.SelectedIndexChanged

        Try
            Dim Diam_DR As DataRowView = cbx_diametro.SelectedItem
            Diam_SW = Diam_DR(1)
        Catch ex As Exception

        End Try



        If cbx_diametro.SelectedIndex <> -1 And cbx_angolo.SelectedIndex <> -1 Then
            Pan3.Visible = True
            'btn_creaSW.Visible = True
        Else
            Pan3.Visible = False
            'btn_creaSW.Visible = False
        End If

    End Sub


    Private Sub Angolo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_angolo.SelectedIndexChanged


        Try
            Dim Angolo_DR As DataRowView = cbx_angolo.SelectedItem
            Angolo_SW = Angolo_DR(1)
        Catch ex As Exception

        End Try

        If cbx_diametro.SelectedIndex <> -1 And cbx_angolo.SelectedIndex <> -1 Then
            Pan3.Visible = True
            'btn_creaSW.Visible = True
        Else
            Pan3.Visible = False
            'btn_creaSW.Visible = False
        End If

    End Sub


    Public Sub generate_excel()


        Dim saveFileDialog1 As SaveFileDialog = New SaveFileDialog()
        saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True
        saveFileDialog1.DefaultExt = ".xlsx"
        saveFileDialog1.AddExtension = True
        saveFileDialog1.FileName = file_exc_name



        Dim files(1) As String
        Dim save_dir As String = ""
        Dim nome_file As String


        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            If saveFileDialog1.FileName IsNot Nothing Then
                save_dir = saveFileDialog1.FileName
                nome_file = Path.GetFileNameWithoutExtension(save_dir)
            End If
        End If


        'copia l'excel nella directory
        Try
            Dim file_name_MP As String = save_dir

            If System.IO.File.Exists(file_name_MP) = False Then
                IO.File.Copy(File_SW, file_name_MP, True) 'copy(dalla cartella, alla cartella con nome del file)
            End If

        Catch ex As Exception

        End Try



        'Apro il file excel e cerco la posizione della tabella prestabilita (LA CELLA RICERCATA E' "PTI")
        objExcel_SW = CreateObject("Excel.Application")
        objExcel_SW.workbooks.open(save_dir)
        objExcel_SW.visible = False




        'objExcel_SW.Worksheets("Foglio1").Cells(1, 1).value = file_exc_name


        objExcel_SW.Worksheets("Foglio1").Cells(1, 1).value = ""
        objExcel_SW.Worksheets("Foglio1").Cells(2, 1).value = "default"


        objExcel_SW.Worksheets("Foglio1").Cells(1, 2).value = "$DESCRIZIONE"
        objExcel_SW.Worksheets("Foglio1").Cells(2, 2).value = "Bulk"

        objExcel_SW.Worksheets("Foglio1").Cells(1, 3).value = "$STATOVISUALIZZAZIONE"
        objExcel_SW.Worksheets("Foglio1").Cells(2, 3).value = "Stato di visualizzazione-1"

        objExcel_SW.Worksheets("Foglio1").Cells(1, 4).value = "$NUMEROPARTE"
        objExcel_SW.Worksheets("Foglio1").Cells(2, 4).value = "$D"


        objExcel_SW.Worksheets("Foglio1").Cells(1, 5).value = "$PADRE"
        objExcel_SW.Worksheets("Foglio1").Cells(2, 5).value = ""

        objExcel_SW.Worksheets("Foglio1").Cells(1, 6).value = "$CONFIGURAZIONE@PALE PER " & Serie_ventola & Npale_ventola & " D400<1>"
        objExcel_SW.Worksheets("Foglio1").Cells(2, 6).value = Diam_SW & " C" & Angolo_SW

        objExcel_SW.Worksheets("Foglio1").Cells(1, 7).value = "$STATO@" & Serie_motore & Serie_ventola & " _._._ -_._-_._-" & Config_ventilatore & "-_._._._._-_<1>"
        objExcel_SW.Worksheets("Foglio1").Cells(2, 7).value = "R"

        objExcel_SW.Worksheets("Foglio1").Cells(1, 8).value = "$CONFIGURAZIONE@" & Serie_motore & Serie_ventola & " _._._ -_._-_._-" & Config_ventilatore & "-_._._._._-_<1>"
        objExcel_SW.Worksheets("Foglio1").Cells(2, 8).value = Diam_SW

        For i = 0 To Numero_colonne_SW - 1
            objExcel_SW.Worksheets("Foglio1").Cells(1, 9 + i).value = Valore_CellaRiga_SW(i)
        Next


        compila_StatiExc()

        'chiudo il file
        objExcel_SW.DisplayAlerts = False
        objExcel_SW.ActiveWorkbook.saveas(save_dir, 51)

        Try
            objExcel_SW.ActiveWorkbook.close()
        Catch ex As Exception

        End Try


        'CHIUDO EXCEL
        objExcel_SW.quit()


    End Sub




    Private Sub btn_creaSW_Click(sender As Object, e As EventArgs)
        generate_excel()
    End Sub


    Public Sub compila_StatiExc()


        For Each item As Control In Pan3.Controls

            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then

                Dim CheckX As Guna.UI2.WinForms.Guna2CheckBox


                For i = 0 To Numero_colonne_SW - 1

                    If item.Text = Nome_colonne_SW(i + 1) Then

                        CheckX = Pan3.Controls("Check" & i + 1)

                        If CheckX.Checked = True Then
                            objExcel_SW.Worksheets("Foglio1").Cells(2, 9 + i).value = "R"
                        Else
                            objExcel_SW.Worksheets("Foglio1").Cells(2, 9 + i).value = "S"
                        End If


                    End If


                Next

            End If

        Next

    End Sub




    Public Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click

        Dim SwOpen As SwOpenFile = New SwOpenFile()

        Try
            SwOpen.Main(Serie_motore, Serie_ventola, Diam_SW, Angolo_SW, Config_ventilatore, TipoPala, Profilo_ventola, Npale_ventola, Flusso, Pan3)
        Catch ex As Exception

        End Try


    End Sub


End Class