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



Public Class Datasheet


    Private Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal hProcess As IntPtr, ByVal dwMinimumWorkingSetSize As Int32, ByVal dwMaximumWorkingSetSize As Int32) As Int32



    Private Sub Datasheet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ParGen.Size = New System.Drawing.Size(1625, 39)

        ERP_check.Checked = True

        If tbx_user_modifiche.Text = "" Then
            tbx_user_modifiche.Text = nome_macchina
        End If

        'Guna2GroupBox3.Visible = False

        Chart18.ChartAreas(0).BackColor = Color.WhiteSmoke
        Chart18.ChartAreas(0).BackGradientStyle = GradientStyle.LeftRight
        Chart18.ChartAreas(0).BorderColor = Color.Transparent
        Chart18.ChartAreas(0).BorderWidth = 0

        Chart18.ChartAreas("ChartArea1").AxisX.MajorGrid.LineColor = Color.LightGray
        Chart18.ChartAreas("ChartArea1").AxisY.MajorGrid.LineColor = Color.LightGray


        Guna2GroupBox4.Visible = False
        ParGen.Text = ""


        Load_cbx_catalogo()

        If Datasheet_New_exc = -1 Or Datasheet_New_exc = 1 Then

            If Datasheet_print_mode = 0 Then

                load_Datasheet()

                If mod_archivio = 0 Or mod_archivio = 2 Then

                    If Tmin.Text = "" Then
                        Tmin.Text = "-40"
                    End If

                End If

                If IP.Text = "" Then
                    IP.Text = 55
                End If

                If Ins_Class.Text = "" Then
                    Ins_Class.Text = "F"
                End If

                If rad_Vip.Checked = False And rad_Uni.Checked = False And rad_Atex.Checked = False Then
                    rad_Vip.PerformClick()
                End If


                load_end = 1


                'ATTIVA IL LOADING
                Me.Guna2Panel1.Visible = False  '------> ESEGUIRE IN PARALLELO
                Me.Guna2Panel1.SendToBack()  '------> ESEGUIRE IN PARALLELO
                Application.DoEvents()


                funzione_cbx1(cbx_PJ_ambiente, "cbx_PJ_ambiente")


                If cbx_PJ_ambiente.SelectedIndex = -1 Then
                    cbx_PJ_ambiente.SelectedIndex = 0
                End If


            Else  'Procedura di rigenerazione tutti i Datasheets

                ciclo_Ristampa_Archivio()

            End If


        Else

            compilazione_New()



        End If






    End Sub



    Public Sub ciclo_Ristampa_Archivio()

        'ATTIVA IL LOADING
        Me.Guna2Panel1.Visible = True  '------> ESEGUIRE IN PARALLELO
        Me.Guna2Panel1.BringToFront()  '------> ESEGUIRE IN PARALLELO
        Application.DoEvents()
        Label58.Location = New System.Drawing.Point(500, 490)


        Count_Mail_Error = 0
        For i As Integer = 0 To numero_DS - 1
            Try
                lista_DS_sel_name = DS_lista(i, 1)

                form_Archivio.Label22.Text = "Regenerating " & DS_lista(i, 1) & ". Process at " & Math.Round(i / (numero_DS - 1) * 100, 0) & " %"
                Label58.Text = "Updating " & DS_lista(i, 1) & ". Process at " & Math.Round(i / (numero_DS - 1) * 100, 0) & " %"
                form_Archivio.Label22.Location = New System.Drawing.Point(450, 490)
                Application.DoEvents()

                Load_Archivio()

                Ricerca_imm_sel()

                update_all_pdf(i)

                memory_clean()

            Catch ex As Exception

                'Compongo la stringa contenente tutti gli errori
                Error_log_ristampa(Count_Mail_Error) = DS_lista(i, 1) & " con la seguente eccezione: " & ex.ToString()
                Count_Mail_Error = Count_Mail_Error + 1

            End Try

        Next

        'Se la stringa degli errori è vuota allora invio la mail a fine processo
        If Count_Mail_Error > 0 Then
            MandaMailErrore1()
        End If

        form_DatasheetMenu.Close()

    End Sub





    Public Sub memory_clean()

        For Each item As Control In Guna2GroupBox4.Controls

            If item.GetType Is GetType(System.Windows.Forms.GroupBox) Then

                Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                For Each item1 As Control In gb.Controls

                    If item1.GetType Is GetType(System.Windows.Forms.CheckBox) Then

                        Dim chb As System.Windows.Forms.CheckBox = DirectCast(item1, System.Windows.Forms.CheckBox)

                        Dim imm_name As String = chb.Name.Substring(chb.Name.IndexOf("_") + 1, chb.Name.Length - chb.Name.IndexOf("_") - 1)

                        chb.BackgroundImage.Dispose()

                    End If

                Next

            End If
        Next


    End Sub




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


    Public Sub load_Datasheet()


        'ATTIVA IL LOADING
        Me.Guna2Panel1.Visible = True  '------> ESEGUIRE IN PARALLELO
        Me.Guna2Panel1.BringToFront()  '------> ESEGUIRE IN PARALLELO
        Application.DoEvents()


        directory_exc1 = ""
        directory_exc2 = ""

        For i = 0 To 2
            coeff_bassa(i) = 0
            coeff_alta(i) = 0
        Next


        If load_var = 0 Then

            load_var = 1

            If mod_archivio = 0 Then
                Load_file() 'carica il file excel selezionato
            ElseIf mod_archivio = 1 Then
                Load_Archivio() 'carica i dati dal database
            ElseIf mod_archivio = 2 Then
                Load_correzione_archivio()
            End If

        Else

            If mod_archivio = 0 Then
                Load_file() 'carica il file excel selezionato
            End If

            load_var = 0

            If mod_archivio <> 0 Then
                DatasheetMenu.btnDataSheet.PerformClick()
            End If


        End If

    End Sub



    Private Sub Guna2RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles rad_Vip.CheckedChanged

        grado_eff.SelectedIndex = 0
        grado_eff.Enabled = False

        Label59.Visible = False
        Label60.Visible = False
        Label61.Visible = False
        Label62.Visible = False

        cbx_AtexProtezione.Visible = False
        cbx_AtexCustodia.Visible = False
        cbx_AtexCategoria.Visible = False
        cbx_AtexClasseTemperatura.Visible = False


        Guna2GroupBox4.Visible = True
        warning_mot.Visible = False
        Label45.Visible = False

        mod_fan = "V"
        tipo_motore_PJ = 1

        If mod_fan = "V" Then

            'Ciclo di ricerca e sostituzione immagini atex
            For Each item As Control In Guna2GroupBox4.Controls


                If item.GetType Is GetType(System.Windows.Forms.GroupBox) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                    Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                    For Each item1 As Control In gb.Controls

                        If item1.GetType Is GetType(System.Windows.Forms.CheckBox) Then 'cerco le checkbox dentro la groupbox identificata

                            Dim chb As System.Windows.Forms.CheckBox = DirectCast(item1, System.Windows.Forms.CheckBox)

                            Dim imm_name As String = chb.Name.Substring(chb.Name.IndexOf("_") + 1, chb.Name.Length - chb.Name.IndexOf("_") - 1)


                            If imm_name(0) = "9" Or imm_name(0) = "5" Or imm_name(0) = "0" Then
                                chb.BackgroundImage = My.Resources.ResourceManager.GetObject("_" & imm_name)
                            Else
                                chb.BackgroundImage = My.Resources.ResourceManager.GetObject(imm_name)
                            End If


                            '----------------------------------------codice di inserimento immagine mancante "MANCA FILE ICONA"-----------------------------------------------
                            Dim txt_imm As String

                            Try
                                If imm_name(0) = "9" Or imm_name(0) = "5" Or imm_name(0) = "0" Then
                                    txt_imm = My.Resources.ResourceManager.GetObject("_" & imm_name)
                                Else
                                    txt_imm = My.Resources.ResourceManager.GetObject(imm_name)
                                End If


                                If txt_imm = Nothing Then

                                    chb.BackgroundImage = My.Resources.ResourceManager.GetObject("VUOTA")

                                End If

                            Catch ex As Exception

                            End Try
                            '---------------------------------------------------------------------------------------------------------------------------------------------------



                        End If

                    Next

                End If
            Next

        End If



    End Sub




    Public Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Apri_EXC.Click


        DatasheetMenu.btnDataSheet.PerformClick()


    End Sub





    Private Sub ERP_sel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ERP_sel.SelectedIndexChanged



        If S_D_var = 1 Then

            If ERP_sel.SelectedIndex = 0 Then

                'Inserimento dati ERP
                rendimento_M.Text = Math.Round(Max_eff_ERP2015_bassa, 2)
                ERP_Target_M.Text = Math.Round(target_eff_ERP2015_bassa, 2)
                potenza_M.Text = Math.Round(pow_ERP2015_bassa * 1000, 0)
                portata_M.Text = Math.Round(Q_ERP2015_bassa, 0)
                pressione_M.Text = Math.Round(P_ERP2015_bassa, 0)
                RPM_M.Text = Math.Round(RPM_ERP2015_bassa, 0)
                categoria_eff.SelectedItem = cat_eff_ERP2015_bassa
                categoria_prova.SelectedItem = Cat_prova_ERP2015_bassa


            Else

                'Inserimento dati ERP
                rendimento_M.Text = Math.Round(Max_eff_ERP2013_bassa, 2)
                ERP_Target_M.Text = Math.Round(target_eff_ERP2013_bassa, 2)
                potenza_M.Text = Math.Round(pow_ERP2013_bassa * 1000, 0)
                portata_M.Text = Math.Round(Q_ERP2013_bassa, 0)
                pressione_M.Text = Math.Round(P_ERP2013_bassa, 0)
                RPM_M.Text = Math.Round(RPM_ERP2013_bassa, 0)
                categoria_eff.SelectedItem = cat_eff_ERP2013_bassa
                categoria_prova.SelectedItem = Cat_prova_ERP2013_bassa


            End If




        Else


            If ERP_sel.SelectedIndex = 0 Then

                'Inserimento dati ERP
                rendimento_M.Text = Math.Round(Max_eff_ERP2015_alta, 2)
                ERP_Target_M.Text = Math.Round(target_eff_ERP2015_alta, 2)
                potenza_M.Text = Math.Round(pow_ERP2015_alta * 1000, 0)
                portata_M.Text = Math.Round(Q_ERP2015_alta, 0)
                pressione_M.Text = Math.Round(P_ERP2015_alta, 0)
                RPM_M.Text = Math.Round(RPM_ERP2015_alta, 0)
                categoria_eff.SelectedItem = cat_eff_ERP2015_alta
                categoria_prova.SelectedItem = Cat_prova_ERP2015_alta


            Else

                'Inserimento dati ERP
                rendimento_M.Text = Math.Round(Max_eff_ERP2013_alta, 2)
                ERP_Target_M.Text = Math.Round(target_eff_ERP2013_alta, 2)
                potenza_M.Text = Math.Round(pow_ERP2013_alta * 1000, 0)
                portata_M.Text = Math.Round(Q_ERP2013_alta, 0)
                pressione_M.Text = Math.Round(P_ERP2013_alta, 0)
                RPM_M.Text = Math.Round(RPM_ERP2013_alta, 0)
                categoria_eff.SelectedItem = cat_eff_ERP2013_alta
                categoria_prova.SelectedItem = Cat_prova_ERP2013_alta


            End If




        End If

        If Max_eff_ERP2015_alta <> 0 Then
            tbx_true_eff.Text = Math.Round(Max_eff_ERP2015_alta, 2)
        Else
            tbx_true_eff.Text = Math.Round(Max_eff_ERP2015_bassa, 2)
        End If

    End Sub







    'gestione colorazione textbox e combobox


    Private Sub tbx_Cliente_TextChanged(sender As Object, e As EventArgs) Handles Descrizione.TextChanged

        If Descrizione.Text = "" Then
            Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
        End If


        If print_mode = 0 Then
            descrizione_fan = Descrizione.Text
        End If


        Try

            Salva_DB.Text = "Salva"

            If (descrizione_fan.Length <> 22) Or (descrizione_fan(3) <> " " Or (descrizione_fan(14) <> "_" Or descrizione_fan(15) <> "_") Or descrizione_fan(7) <> "-" Or descrizione_fan(10) <> "-" Or descrizione_fan(13) <> "-" Or descrizione_fan(16) <> "-") Then

                Label44.Text = "Es: TR_ 063-32-ST-__-3X070"
                Label47.Text = "DESCRIZIONE ERRATA"
                Label47.Visible = True
                Salva_DB.Enabled = False
                Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
                Descrizione.ForeColor = Color.Red
                warning_des.Visible = True


            Else



                Label47.Visible = False
                Salva_DB.Enabled = True
                Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
                Descrizione.ForeColor = Color.FromArgb(125, 137, 149)
                Label44.Text = ""
                warning_des.Visible = False


                'se la sintasi del nome e' corretto allora eseguo una ricerca nel database
                Importa_Archivio_DS1()



                If blocca_add = 0 Then
                    Label47.Visible = False
                    Label47.Text = "DESCRIZIONE ERRATA"
                    Salva_DB.Enabled = True
                    Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
                    Descrizione.ForeColor = Color.FromArgb(125, 137, 149)
                Else

                    Salva_DB.Text = "Aggiorna DataBase"
                    Label47.Visible = True
                    Label47.Text = ""
                    Salva_DB.Enabled = True
                    'Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
                    'Descrizione.ForeColor = Color.Red
                End If



                If num_conf_sel = 0 Then

                    Label47.Text = "SELEZIONARE UNA CONFIGURAZIONE"
                    Label47.Visible = True
                    Salva_DB.Enabled = False
                    Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
                    Descrizione.ForeColor = Color.Red
                    warning_des.Visible = True
                End If


            End If



        Catch ex As Exception

            Label44.Text = "Es: TR_ 063-32-ST-__-3X070"
            Salva_DB.Enabled = False
            Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Descrizione.ForeColor = Color.Red
            warning_des.Visible = True

        End Try



    End Sub



    Private Sub pow_installata_TextChanged(sender As Object, e As EventArgs) Handles pow_installata.TextChanged

        If pow_installata.Text = "" Then
            pow_installata.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            pow_installata.BorderColor = Color.FromArgb(74, 231, 148)
        End If



        If print_mode = 0 Then
            potenza_installata = pow_installata.Text
        End If




    End Sub

    Private Sub Tmin_TextChanged(sender As Object, e As EventArgs) Handles Tmin.TextChanged

        If Tmin.Text = "" Then
            Tmin.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            Tmin.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            Tmin_DS = Tmin.Text
        End If

    End Sub

    Private Sub Tmax_TextChanged(sender As Object, e As EventArgs) Handles Tmax.TextChanged

        If Tmax.Text = "" Then
            Tmax.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            Tmax.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            Tmax_DS = Tmax.Text
        End If

    End Sub

    Private Sub Test_numero_TextChanged(sender As Object, e As EventArgs) Handles Test_numero.TextChanged

        If Test_numero.Text = "" Then
            Test_numero.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            Test_numero.BorderColor = Color.FromArgb(74, 231, 148)
        End If


        If print_mode = 0 Then
            test_numeroDS = Test_numero.Text
        End If

    End Sub

    Private Sub alt_Zihel_TextChanged(sender As Object, e As EventArgs) Handles alt_Zihel.TextChanged

        If alt_Zihel.Text = "" Then
            alt_Zihel.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            alt_Zihel.BorderColor = Color.FromArgb(74, 231, 148)
        End If

    End Sub

    Private Sub alt_EBM_TextChanged(sender As Object, e As EventArgs) Handles alt_EBM.TextChanged

        If alt_EBM.Text = "" Then
            alt_EBM.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            alt_EBM.BorderColor = Color.FromArgb(74, 231, 148)
        End If

    End Sub

    Private Sub rendimento_M_TextChanged(sender As Object, e As EventArgs) Handles rendimento_M.TextChanged

        If rendimento_M.Text = "" Then
            rendimento_M.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            rendimento_M.BorderColor = Color.FromArgb(74, 231, 148)
        End If


        If ERP_Target_M.Text > rendimento_M.Text Then

            rendimento_M.Text = Math.Round(ERP_Target_M.Text * 1.01)

            Label40.Visible = True
            warning_ERP.Visible = True
            rendimento_M.ForeColor = Color.Red

        Else
            Label40.Visible = False
            warning_ERP.Visible = False
            rendimento_M.ForeColor = Color.Black
        End If


        If print_mode = 0 Then
            Efficiency_fan = rendimento_M.Text
        End If

    End Sub

    Private Sub potenza_M_TextChanged(sender As Object, e As EventArgs) Handles potenza_M.TextChanged

        If potenza_M.Text = "" Then
            potenza_M.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            potenza_M.BorderColor = Color.FromArgb(74, 231, 148)
        End If


        If print_mode = 0 Then
            ERP_pow = potenza_M.Text
        End If

    End Sub

    Private Sub portata_M_TextChanged(sender As Object, e As EventArgs) Handles portata_M.TextChanged

        If portata_M.Text = "" Then
            portata_M.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            portata_M.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            ERP_Q = portata_M.Text
        End If

    End Sub

    Private Sub pressione_M_TextChanged(sender As Object, e As EventArgs) Handles pressione_M.TextChanged

        If pressione_M.Text = "" Then
            pressione_M.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            pressione_M.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            ERP_P = pressione_M.Text
        End If

    End Sub

    Private Sub RPM_M_TextChanged(sender As Object, e As EventArgs) Handles RPM_M.TextChanged

        If RPM_M.Text = "" Then
            RPM_M.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            RPM_M.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            ERP_RPM = RPM_M.Text
        End If


    End Sub

    Private Sub I_bassa_TextChanged(sender As Object, e As EventArgs) Handles I_bassa.TextChanged

        If I_bassa.Text = "" Then
            I_bassa.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            I_bassa.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            I_bassa_DS = I_bassa.Text
        End If


    End Sub

    Private Sub RPM_bassa_TextChanged(sender As Object, e As EventArgs) Handles RPM_bassa.TextChanged

        If RPM_bassa.Text = "" Then
            RPM_bassa.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            RPM_bassa.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            RPM_bassa_DS = RPM_bassa.Text
        End If

    End Sub

    Private Sub pow_bassa_TextChanged(sender As Object, e As EventArgs) Handles pow_bassa.TextChanged

        If pow_bassa.Text = "" Then
            pow_bassa.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            pow_bassa.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            pow_bassa_DS = pow_bassa.Text
        End If

    End Sub



    Private Sub I_alta_TextChanged(sender As Object, e As EventArgs) Handles I_alta.TextChanged

        If I_alta.Text = "" Then
            I_alta.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            I_alta.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            I_alta_DS = I_alta.Text
        End If

    End Sub

    Private Sub RPM_alta_TextChanged(sender As Object, e As EventArgs) Handles RPM_alta.TextChanged

        If RPM_alta.Text = "" Then
            RPM_alta.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            RPM_alta.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            RPM_alta_DS = RPM_alta.Text
        End If

    End Sub

    Private Sub pow_alta_TextChanged(sender As Object, e As EventArgs) Handles pow_alta.TextChanged

        If pow_alta.Text = "" Then
            pow_alta.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            pow_alta.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            pow_alta_DS = pow_alta.Text
        End If

    End Sub



    Private Sub RPM1_TextChanged(sender As Object, e As EventArgs) Handles RPM1.TextChanged

        If RPM1.Text = "" Then
            RPM1.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            RPM1.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            RPM1_DS = RPM1.Text
        End If

    End Sub

    Private Sub POW1_TextChanged(sender As Object, e As EventArgs) Handles POW1.TextChanged

        If POW1.Text = "" Then
            POW1.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            POW1.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            POW1_DS = POW1.Text
        End If

    End Sub

    Private Sub CURR1_TextChanged(sender As Object, e As EventArgs) Handles CURR1.TextChanged

        If CURR1.Text = "" Then
            CURR1.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            CURR1.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            CURR1_DS = CURR1.Text
        End If

    End Sub

    Private Sub LWA1_TextChanged(sender As Object, e As EventArgs) Handles LWA1.TextChanged

        If LWA1.Text = "" Then
            LWA1.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            LWA1.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            LWA1_DS = LWA1.Text
        End If

    End Sub

    Private Sub RPM2_TextChanged(sender As Object, e As EventArgs) Handles RPM2.TextChanged

        If RPM2.Text = "" Then
            RPM2.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            RPM2.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            RPM2_DS = RPM2.Text
        End If

    End Sub

    Private Sub POW2_TextChanged(sender As Object, e As EventArgs) Handles POW2.TextChanged

        If POW2.Text = "" Then
            POW2.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            POW2.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            POW2_DS = POW2.Text
        End If

    End Sub

    Private Sub CURR2_TextChanged(sender As Object, e As EventArgs) Handles CURR2.TextChanged

        If CURR2.Text = "" Then
            CURR2.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            CURR2.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            CURR2_DS = CURR2.Text
        End If

    End Sub

    Private Sub LWA2_TextChanged(sender As Object, e As EventArgs) Handles LWA2.TextChanged

        If LWA2.Text = "" Then
            LWA2.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            LWA2.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            LWA2_DS = LWA2.Text
        End If

    End Sub

    Private Sub RPM3_TextChanged(sender As Object, e As EventArgs) Handles RPM3.TextChanged

        If RPM3.Text = "" Then
            RPM3.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            RPM3.BorderColor = Color.FromArgb(74, 231, 148)
        End If


        If print_mode = 0 Then
            RPM3_DS = RPM3.Text
        End If



    End Sub

    Private Sub POW3_TextChanged(sender As Object, e As EventArgs) Handles POW3.TextChanged

        If POW3.Text = "" Then
            POW3.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            POW3.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            POW3_DS = POW3.Text
        End If

    End Sub

    Private Sub CURR3_TextChanged(sender As Object, e As EventArgs) Handles CURR3.TextChanged

        If CURR3.Text = "" Then
            CURR3.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            CURR3.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            CURR3_DS = CURR3.Text
        End If

    End Sub

    Private Sub LWA3_TextChanged(sender As Object, e As EventArgs) Handles LWA3.TextChanged

        If LWA3.Text = "" Then
            LWA3.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            LWA3.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            LWA3_DS = LWA3.Text
        End If

    End Sub

    Private Sub RPM4_TextChanged(sender As Object, e As EventArgs) Handles RPM4.TextChanged

        If RPM4.Text = "" Then
            RPM4.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            RPM4.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            RPM4_DS = RPM4.Text
        End If

    End Sub

    Private Sub POW4_TextChanged(sender As Object, e As EventArgs) Handles POW4.TextChanged

        If POW4.Text = "" Then
            POW4.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            POW4.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            POW4_DS = POW4.Text
        End If

    End Sub

    Private Sub CURR4_TextChanged(sender As Object, e As EventArgs) Handles CURR4.TextChanged

        If CURR4.Text = "" Then
            CURR4.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            CURR4.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            CURR4_DS = CURR4.Text
        End If

    End Sub

    Private Sub LWA4_TextChanged(sender As Object, e As EventArgs) Handles LWA4.TextChanged

        If LWA4.Text = "" Then
            LWA4.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            LWA4.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            LWA4_DS = LWA4.Text
        End If

    End Sub

    Private Sub RPM5_TextChanged(sender As Object, e As EventArgs) Handles RPM5.TextChanged

        If RPM5.Text = "" Then
            RPM5.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            RPM5.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            RPM5_DS = RPM5.Text
        End If

    End Sub

    Private Sub POW5_TextChanged(sender As Object, e As EventArgs) Handles POW5.TextChanged

        If POW5.Text = "" Then
            POW5.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            POW5.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            POW5_DS = POW5.Text
        End If

    End Sub

    Private Sub CURR5_TextChanged(sender As Object, e As EventArgs) Handles CURR5.TextChanged

        If CURR5.Text = "" Then
            CURR5.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            CURR5.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            CURR5_DS = CURR5.Text
        End If

    End Sub

    Private Sub LWA5_TextChanged(sender As Object, e As EventArgs) Handles LWA5.TextChanged

        If LWA5.Text = "" Then
            LWA5.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            LWA5.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            LWA5_DS = LWA5.Text
        End If

    End Sub

    Private Sub RPM6_TextChanged(sender As Object, e As EventArgs) Handles RPM6.TextChanged

        If RPM6.Text = "" Then
            RPM6.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            RPM6.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            RPM6_DS = RPM6.Text
        End If

    End Sub

    Private Sub POW6_TextChanged(sender As Object, e As EventArgs) Handles POW6.TextChanged

        If POW6.Text = "" Then
            POW6.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            POW6.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            POW6_DS = POW6.Text
        End If

    End Sub

    Private Sub CURR6_TextChanged(sender As Object, e As EventArgs) Handles CURR6.TextChanged

        If CURR6.Text = "" Then
            CURR6.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            CURR6.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            CURR6_DS = CURR6.Text
        End If

    End Sub

    Private Sub LWA6_TextChanged(sender As Object, e As EventArgs) Handles LWA6.TextChanged

        If LWA6.Text = "" Then
            LWA6.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            LWA6.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            LWA6_DS = LWA6.Text
        End If

    End Sub


    Private Sub ERP_TARGET_M_TextChanged(sender As Object, e As EventArgs) Handles ERP_Target_M.TextChanged

        If ERP_Target_M.Text = "" Then
            ERP_Target_M.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            ERP_Target_M.BorderColor = Color.FromArgb(74, 231, 148)
        End If


        If ERP_Target_M.Text > rendimento_M.Text Then

            rendimento_M.Text = Math.Round(ERP_Target_M.Text * 1.01)

            Label40.Visible = True
            warning_ERP.Visible = True
            rendimento_M.ForeColor = Color.Red

        Else
            Label40.Visible = False
            warning_ERP.Visible = False
            rendimento_M.ForeColor = Color.Black
        End If


        If print_mode = 0 Then
            Efficiency_target = ERP_Target_M.Text
        End If

    End Sub


    Private Sub Tipo_prova_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Tipo_prova.SelectedIndexChanged

        If Tipo_prova.SelectedIndex = -1 Then
            Tipo_prova.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            Tipo_prova.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        tipo_testDS = Tipo_prova.SelectedItem(0)

    End Sub


    Private Sub grado_eff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grado_eff.SelectedIndexChanged

        If grado_eff.SelectedIndex = -1 Then
            grado_eff.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            grado_eff.BorderColor = Color.FromArgb(74, 231, 148)
        End If

    End Sub


    Private Sub categoria_prova_SelectedIndexChanged(sender As Object, e As EventArgs) Handles categoria_prova.SelectedIndexChanged

        If categoria_prova.SelectedIndex = -1 Then
            categoria_prova.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            categoria_prova.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        installation_cat = categoria_prova.SelectedItem


    End Sub


    Private Sub categoria_eff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles categoria_eff.SelectedIndexChanged

        If categoria_eff.SelectedIndex = -1 Then
            categoria_eff.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            categoria_eff.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If categoria_eff.SelectedIndex = 0 Then
            Efficiency_category = "Static"
        ElseIf categoria_eff.SelectedIndex = 1 Then
            Efficiency_category = "Total"
        End If

    End Sub


    Private Sub cbx_TipoMotore_SelectedIndexChanged(sender As Object, e As EventArgs) Handles frame_motore_sel.SelectedIndexChanged

        If frame_motore_sel.SelectedIndex = -1 Then
            frame_motore_sel.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            frame_motore_sel.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        frame_sel = frame_motore_sel.SelectedItem

    End Sub


    Private Sub Tensione_bassa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Tensione_bassa.SelectedIndexChanged

        If Tensione_bassa.SelectedIndex = -1 Then
            Tensione_bassa.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            Tensione_bassa.BorderColor = Color.FromArgb(74, 231, 148)
        End If


        'Try
        tensione_bassa_N = Tensione_bassa.SelectedItem(2) & Tensione_bassa.SelectedItem(3) & Tensione_bassa.SelectedItem(4)
        tensione_bassa_T = Tensione_bassa.SelectedItem

        conn_bassa = Tensione_bassa.SelectedItem
            conn_bassa = conn_bassa(conn_bassa.Length - 1)


            numero_fasi_bassa = Tensione_bassa.SelectedItem(0) & Tensione_bassa.SelectedItem(1)

        'Catch ex As Exception

        '    tensione = "VFD"
        '    conn_bassa = "VFD"
        '    numero_fasi_bassa = ""

        'End Try





    End Sub

    Private Sub Freq_bassa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Freq_bassa.SelectedIndexChanged

        If Freq_bassa.SelectedIndex = -1 Then
            Freq_bassa.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            Freq_bassa.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        freq_bassa_N = Freq_bassa.SelectedItem

    End Sub

    Private Sub Tensione_alta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Tensione_alta.SelectedIndexChanged

        If Tensione_alta.SelectedIndex = -1 Then
            Tensione_alta.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            Tensione_alta.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        tensione_alta_N = Tensione_alta.SelectedItem(2) & Tensione_alta.SelectedItem(3) & Tensione_alta.SelectedItem(4)
        tensione_alta_T = Tensione_alta.SelectedItem

        conn_alta = Tensione_alta.SelectedItem
        conn_alta = conn_alta(conn_alta.Length - 1)

        numero_fasi_alta = Tensione_alta.SelectedItem(0) & Tensione_alta.SelectedItem(1)
    End Sub

    Private Sub Frequenza_alta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Freq_alta.SelectedIndexChanged

        If Freq_alta.SelectedIndex = -1 Then
            Freq_alta.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            Freq_alta.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        freq_alta_N = Freq_alta.SelectedItem

    End Sub

    Private Sub ERP_sel_SelectedIndexChanged1(sender As Object, e As EventArgs) Handles ERP_sel.SelectedIndexChanged

        If ERP_sel.SelectedIndex = -1 Then
            ERP_sel.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            ERP_sel.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        ERP_selezionato = ERP_sel.SelectedItem

    End Sub

    Private Sub Guna2RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles rad_Atex.CheckedChanged


        grado_eff.Enabled = True

        Label59.Visible = True
        Label60.Visible = True
        Label61.Visible = True
        Label62.Visible = True

        cbx_AtexProtezione.Visible = True
        cbx_AtexCustodia.Visible = True
        cbx_AtexCategoria.Visible = True
        cbx_AtexClasseTemperatura.Visible = True

        funzione_cbx1(cbx_AtexClasseTemperatura, "cbx_AtexClasseTemperatura")
        funzione_cbx1(cbx_AtexCategoria, "cbx_AtexCategoria")
        funzione_cbx1(cbx_AtexCustodia, "cbx_AtexCustodia")
        funzione_cbx1(cbx_AtexProtezione, "cbx_AtexProtezione")


        Guna2GroupBox4.Visible = True
        warning_mot.Visible = False
        Label45.Visible = False


        mod_fan = "A"
        tipo_motore_PJ = 3

        If mod_fan = "A" Then

            'Ciclo di ricerca e sostituzione immagini atex

            For Each item As Control In Guna2GroupBox4.Controls
                'Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                If item.GetType Is GetType(System.Windows.Forms.GroupBox) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                    Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                    For Each item1 As Control In gb.Controls

                        If item1.GetType Is GetType(System.Windows.Forms.CheckBox) Then 'cerco le checkbox dentro la groupbox identificata

                            Dim chb As System.Windows.Forms.CheckBox = DirectCast(item1, System.Windows.Forms.CheckBox)

                            Dim imm_name As String = chb.Name.Substring(chb.Name.IndexOf("_") + 1, chb.Name.Length - chb.Name.IndexOf("_") - 1)


                            If imm_name(0) = "9" Or imm_name(0) = "5" Or imm_name(0) = "0" Then
                                chb.BackgroundImage = My.Resources.ResourceManager.GetObject("_" & imm_name & "_atex")
                            Else
                                chb.BackgroundImage = My.Resources.ResourceManager.GetObject(imm_name & "_atex")
                            End If


                            '----------------------------------------codice di inserimento immagine mancante "MANCA FILE ICONA"-----------------------------------------------
                            Dim txt_imm As String

                            Try
                                If imm_name(0) = "9" Or imm_name(0) = "5" Or imm_name(0) = "0" Then
                                    txt_imm = My.Resources.ResourceManager.GetObject("_" & imm_name & "_atex")
                                Else
                                    txt_imm = My.Resources.ResourceManager.GetObject(imm_name & "_atex")
                                End If


                                If txt_imm = Nothing Then

                                    chb.BackgroundImage = My.Resources.ResourceManager.GetObject("VUOTA")

                                End If

                            Catch ex As Exception

                            End Try
                            '---------------------------------------------------------------------------------------------------------------------------------------------------



                        End If

                    Next

                End If
            Next

        End If




    End Sub

    Private Sub Guna2RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles rad_Uni.CheckedChanged

        grado_eff.Enabled = True

        Label59.Visible = False
        Label60.Visible = False
        Label61.Visible = False
        Label62.Visible = False

        cbx_AtexProtezione.Visible = False
        cbx_AtexCustodia.Visible = False
        cbx_AtexCategoria.Visible = False
        cbx_AtexClasseTemperatura.Visible = False


        Guna2GroupBox4.Visible = True
        warning_mot.Visible = False
        Label45.Visible = False

        mod_fan = "U"
        tipo_motore_PJ = 2

        If mod_fan = "U" Then

            'Ciclo di ricerca e sostituzione immagini atex

            For Each item As Control In Guna2GroupBox4.Controls
                'Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                If item.GetType Is GetType(System.Windows.Forms.GroupBox) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                    Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                    For Each item1 As Control In gb.Controls

                        If item1.GetType Is GetType(System.Windows.Forms.CheckBox) Then 'cerco le checkbox dentro la groupbox identificata

                            Dim chb As System.Windows.Forms.CheckBox = DirectCast(item1, System.Windows.Forms.CheckBox)

                            Dim imm_name As String = chb.Name.Substring(chb.Name.IndexOf("_") + 1, chb.Name.Length - chb.Name.IndexOf("_") - 1)


                            If imm_name(0) = "9" Or imm_name(0) = "5" Or imm_name(0) = "0" Then
                                chb.BackgroundImage = My.Resources.ResourceManager.GetObject("_" & imm_name & "_un")
                            Else
                                chb.BackgroundImage = My.Resources.ResourceManager.GetObject(imm_name & "_un")
                            End If


                            '----------------------------------------codice di inserimento immagine mancante "MANCA FILE ICONA"-----------------------------------------------
                            Dim txt_imm As String

                            Try
                                If imm_name(0) = "9" Or imm_name(0) = "5" Or imm_name(0) = "0" Then
                                    txt_imm = My.Resources.ResourceManager.GetObject("_" & imm_name & "_un")
                                Else
                                    txt_imm = My.Resources.ResourceManager.GetObject(imm_name & "_un")
                                End If


                                If txt_imm = Nothing Then

                                    chb.BackgroundImage = My.Resources.ResourceManager.GetObject("VUOTA")

                                End If

                            Catch ex As Exception

                            End Try
                            '---------------------------------------------------------------------------------------------------------------------------------------------------



                        End If

                    Next

                End If
            Next

        End If



    End Sub

    Private Sub Pic_F_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_F_A.CheckedChanged
        If Pic_F_A.Checked = True Then
            tbx_F_A.Visible = True
        Else
            tbx_F_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)

    End Sub


    Private Sub Pic_EAA_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_EAA.CheckedChanged

        If Pic_EAA.Checked = True Then
            tbx_EAA.Visible = True
        Else
            tbx_EAA.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)

    End Sub


    Private Sub Pic_EAP_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_EAP.CheckedChanged

        If Pic_EAP.Checked = True Then
            tbx_EAP.Visible = True
        Else
            tbx_EAP.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)

    End Sub

    Private Sub Pic_M_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_M_A.CheckedChanged
        If Pic_M_A.Checked = True Then
            tbx_M_A.Visible = True
        Else
            tbx_M_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_X_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_X_P.CheckedChanged
        If Pic_X_P.Checked = True Then
            tbx_X_P.Visible = True
        Else
            tbx_X_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_CP_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_C_P.CheckedChanged
        If Pic_C_P.Checked = True Then
            tbx_C_P.Visible = True
        Else
            tbx_C_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_CA_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_C_A.CheckedChanged
        If Pic_C_A.Checked = True Then
            tbx_C_A.Visible = True
        Else
            tbx_C_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_Q_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_Q_A.CheckedChanged
        If Pic_Q_A.Checked = True Then
            tbx_Q_A.Visible = True
        Else
            tbx_Q_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_G_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_G_A.CheckedChanged
        If Pic_G_A.Checked = True Then
            tbx_G_A.Visible = True
        Else
            tbx_G_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_K_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_K_P.CheckedChanged
        If Pic_K_P.Checked = True Then
            tbx_K_P.Visible = True
        Else
            tbx_K_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_PP_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_P_P.CheckedChanged
        If Pic_P_P.Checked = True Then
            tbx_P_P.Visible = True
        Else
            tbx_P_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_PA_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_P_A.CheckedChanged
        If Pic_P_A.Checked = True Then
            tbx_P_A.Visible = True
        Else
            tbx_P_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_5A_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_5_A.CheckedChanged
        If Pic_5_A.Checked = True Then
            tbx_5_A.Visible = True
        Else
            tbx_5_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub


    Private Sub Pic_5P_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_5_P.CheckedChanged
        If Pic_5_P.Checked = True Then
            tbx_5_P.Visible = True
        Else
            tbx_5_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub


    Private Sub Pic_B_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_B_A.CheckedChanged
        If Pic_B_A.Checked = True Then
            tbx_B_A.Visible = True
        Else
            tbx_B_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_W_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_W_A.CheckedChanged
        If Pic_W_A.Checked = True Then
            tbx_WA.Visible = True
        Else
            tbx_WA.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_V_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_V_P.CheckedChanged
        If Pic_V_P.Checked = True Then
            tbx_V_P.Visible = True
        Else
            tbx_V_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_TA_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_T_A.CheckedChanged
        If Pic_T_A.Checked = True Then
            tbx_T_A.Visible = True
        Else
            tbx_T_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_TP_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_T_P.CheckedChanged
        If Pic_T_P.Checked = True Then
            tbx_T_P.Visible = True
        Else
            tbx_T_P.Visible = False
        End If
    End Sub

    Private Sub Pic_H_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_H_P.CheckedChanged
        If Pic_H_P.Checked = True Then
            tbx_H_P.Visible = True
        Else
            tbx_H_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_L_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_L_P.CheckedChanged
        If Pic_L_P.Checked = True Then
            tbx_L_P.Visible = True
        Else
            tbx_L_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_JP_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_J_P.CheckedChanged
        If Pic_J_P.Checked = True Then
            tbx_J_P.Visible = True
        Else
            tbx_J_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_9P_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_9_P.CheckedChanged
        If Pic_9_P.Checked = True Then
            tbx_9_P.Visible = True
        Else
            tbx_9_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_IP_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_I_P.CheckedChanged
        If Pic_I_P.Checked = True Then
            tbx_I_P.Visible = True
        Else
            tbx_I_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    'Private Sub Pic_R_CheckedChanged(sender As Object, e As EventArgs)
    '    If Pic_R.Checked = True Then
    '        tbx_R.Visible = True
    '    Else
    '        tbx_R.Visible = False
    '    End If
    'End Sub

    Private Sub Pic_N_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_N_P.CheckedChanged
        If Pic_N_P.Checked = True Then
            tbx_N_P.Visible = True
        Else
            tbx_N_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_JA_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_J_A.CheckedChanged
        If Pic_J_A.Checked = True Then
            tbx_J_A.Visible = True
        Else
            tbx_J_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_9A_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_9_A.CheckedChanged
        If Pic_9_A.Checked = True Then
            tbx_9_A.Visible = True
        Else
            tbx_9_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_IA_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_I_A.CheckedChanged
        If Pic_I_A.Checked = True Then
            tbx_I_A.Visible = True
        Else
            tbx_I_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_DA_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_D_A.CheckedChanged
        If Pic_D_A.Checked = True Then
            tbx_D_A.Visible = True
        Else
            tbx_D_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_DP_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_D_P.CheckedChanged
        If Pic_D_P.Checked = True Then
            tbx_D_P.Visible = True
        Else
            tbx_D_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_AA_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_A_A.CheckedChanged
        If Pic_A_A.Checked = True Then
            tbx_A_A.Visible = True
        Else
            tbx_A_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_AP_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_A_P.CheckedChanged
        If Pic_A_P.Checked = True Then
            tbx_A_P.Visible = True
        Else
            tbx_A_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_YA_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_Y_A.CheckedChanged
        If Pic_Y_A.Checked = True Then
            tbx_Y_A.Visible = True
        Else
            tbx_Y_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_YP_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_Y_P.CheckedChanged
        If Pic_Y_P.Checked = True Then
            tbx_Y_P.Visible = True
        Else
            tbx_Y_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_OA_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_O_A.CheckedChanged
        If Pic_O_A.Checked = True Then
            tbx_O_A.Visible = True
        Else
            tbx_O_A.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_OP_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_O_P.CheckedChanged
        If Pic_O_P.Checked = True Then
            tbx_O_P.Visible = True
        Else
            tbx_O_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_0_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_0_P.CheckedChanged
        If Pic_0_P.Checked = True Then
            tbx_0_P.Visible = True
        Else
            tbx_0_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_1_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_1_P.CheckedChanged
        If Pic_1_P.Checked = True Then
            tbx_1_P.Visible = True
        Else
            tbx_1_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_E_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_E_P.CheckedChanged
        If Pic_E_P.Checked = True Then
            tbx_E_P.Visible = True
        Else
            tbx_E_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Pic_Z_CheckedChanged(sender As Object, e As EventArgs) Handles Pic_Z_P.CheckedChanged
        If Pic_Z_P.Checked = True Then
            tbx_Z_P.Visible = True
        Else
            tbx_Z_P.Visible = False
        End If

        conta_elementi()
        tbx_Cliente_TextChanged(sender, e)
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Stampa_DS.Click

        stampa_code(0)

    End Sub



    Public Sub stampa_code(n)



        If Q4.Text = "" Then
            S_D_var = 1 'singola velocità
        Else
            S_D_var = 2 'doppia velocità
        End If

        print_mode = 1

        '------------------------------------------------CICLO DI RICERCA DELLE CHECKBOX SELEZIONATE----------------------------------------------------------
        num_conf_sel = 0
        For Each item As Control In Guna2GroupBox4.Controls
            'Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

            If item.GetType Is GetType(System.Windows.Forms.GroupBox) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)




                For Each item1 As Control In gb.Controls



                    If item1.GetType Is GetType(System.Windows.Forms.CheckBox) Then 'cerco le checkbox dentro la groupbox identificata

                        Dim chb As System.Windows.Forms.CheckBox = DirectCast(item1, System.Windows.Forms.CheckBox)

                        If chb.Checked = True Then

                            Try

                                conf_sel(num_conf_sel) = chb.Name
                                num_conf_sel = num_conf_sel + 1

                            Catch ex As Exception



                            End Try


                            For Each item2 As Control In gb.Controls
                                If item2.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then 'cerco le checkbox dentro la groupbox identificata
                                    Dim tbx As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item2, Guna.UI2.WinForms.Guna2TextBox)

                                    part_sel(num_conf_sel - 1) = tbx.Text

                                End If
                            Next


                        End If


                    End If



                Next

            End If
            '--------------------------------------------------------------------------------------------------------------------------------------------------





        Next


        printTool.ClosePreview()


        Report.PrintingSystem.ClearContent()
        Report.CreateDocument(True)


        Try
            PJ_config_star = conf_sel(0).Substring(conf_sel(0).IndexOf("_") + 1, conf_sel(0).Length - conf_sel(0).IndexOf("_") - 1)
        Catch ex As Exception

        End Try



        Header1()
        PaginaA1()


        Dim PJ_print As Integer = 1

        Try


            Leggi_riga_PJ1(PJ_config_star, cbx_PJ_ambiente.SelectedItem)
            PJ1()

        Catch ex As Exception

            PJ_print = 0

            Report.XrTable5.Visible = False
            Report.XrTable6.Visible = False
            Report.XrTable7.Visible = False
            Report.XrTable8.Visible = False
            Report.XrTable9.Visible = False
            Report.XrTable10.Visible = False


            Report.XrPictureBox6.Visible = False
            Report.XrPictureBox7.Visible = False
            Report.XrPictureBox8.Visible = False
            Report.XrPictureBox3.Visible = False
            Report.XrPictureBox5.Visible = False
            Report.XrPictureBox43.Visible = False

        End Try


        If errorePJ > 30 Then 'controllo il numero di errori nel prendere i dati dal database

            Report.XrTable5.Visible = False
            Report.XrTable6.Visible = False
            Report.XrTable7.Visible = False
            Report.XrTable8.Visible = False
            Report.XrTable9.Visible = False
            Report.XrTable10.Visible = False


            Report.XrPictureBox6.Visible = False
            Report.XrPictureBox7.Visible = False
            Report.XrPictureBox8.Visible = False
            Report.XrPictureBox3.Visible = False
            Report.XrPictureBox5.Visible = False
            Report.XrPictureBox43.Visible = False


        End If


        Footer1()


        Report.CreateDocument()



        If eccezione_stampa = 1 Then

            'SALVO IL FILE NELLA DIRECORY IN AUTOMATICO
            Try
                Report.ExportOptions.Pdf.ConvertImagesToJpeg = False
                Report.ExportToPdf(folders_directoryDS & "\" & descrizione_fan & "\" & descrizione_fan & ".pdf")
            Catch ex As Exception

            End Try



            Dim printingSystem1 As PrintingSystemBase = Report.PrintingSystem
            Dim options As ExportOptions = printingSystem1.ExportOptions

            If mod_PJ_DS = 1 Then
                Dim folder_search As String = folders_directory & "\" & prog_rev(posizione_progetto, 0) & "\Rev" & num_rev_generale
                options.PrintPreview.DefaultDirectory = folder_search
            Else
                Try
                    options.PrintPreview.DefaultDirectory = "P:\"
                Catch ex As Exception

                End Try
            End If



            'Riformulo il nome nel caso faccio 1 o piu selezioni
            Dim nom_fan_file As String = ""

            If num_conf_sel = 1 Then

                Nome_fan_corretto1(descrizione_fan(2), conf_sel(0)(4), conf_sel(0)(5), conf_sel(0)(6))
                'Dim direzione_flusso As String = conf_sel(0)(5)
                'Dim nome_config As String = conf_sel(0)(4)
                nom_fan_file = traduzione_ventilatore

            Else

                nom_fan_file = descrizione_fan

            End If


            Dim day_DS As String = Today.Day
            Dim month_DS As String = Today.Month
            Dim year_DS As String = Today.Year



            If PJ_print = 1 And num_conf_sel = 1 Then
                options.PrintPreview.DefaultFileName = day_DS & "-" & month_DS & "-" & year_DS & "-" & " DataBook " & nom_fan_file 'se stampa anche il modulo di progettazione
            Else
                options.PrintPreview.DefaultFileName = day_DS & "-" & month_DS & "-" & year_DS & "-" & " DataSheet " & nom_fan_file 'se NON stampa anche il modulo di progettazione
            End If


            printTool.ShowPreviewDialog()


        Else 'CASO DI CICLO DI STAMPA

            If num_conf_sel = 1 Then

                Nome_fan_corretto1(descrizione_fan(2), conf_sel(0)(4), conf_sel(0)(5), conf_sel(0)(6))

                Dim nome_fan_conf As String = traduzione_ventilatore


                'SALVO IL FILE NELLA DIRECORY IN AUTOMATICO
                Report.ExportOptions.Pdf.ConvertImagesToJpeg = False



                Try
                    If ciclo_stampa_catalogo = 0 Then
                        Report.ExportToPdf(folders_directoryDS & "\" & descrizione_fan & "\" & conf_sel(0)(4) & conf_sel(0)(5) & conf_sel(0)(6) & "\" & traduzione_ventilatore & ".pdf")
                    Else
                        Report.ExportToPdf(folders_directoryDS & "\" & descrizione_fan & "\" & nome_catalogo & "\" & descrizione_fan & ".pdf")
                    End If
                Catch ex As Exception

                End Try

                '*************************************************SQL SERVER AGGIUNGO******************************************************************************
                ERP1(n)
                If DS_totale_dati(n, 87) = True And Datasheet_print_mode = 1 And (((DS_totale_dati(n, 4) = "T-Testato") Or descrizione_fan(0) = "U") And ERP_ok = 1) Or (descrizione_fan(0) = "T" And descrizione_fan(0) = "X" And descrizione_fan(0) = "H") Then 'solo se c'è la spunta nell'archivio carico sul sito 
                    If ciclo_stampa_catalogo = 0 Then
                        SQLSERVER_context1(nome_fan_conf, n, "Catalogo")
                    Else
                        SQLSERVER_context1(nome_fan_conf, n, nome_catalogo)
                    End If
                End If
                '**************************************************************************************************************************************************

            Else 'ultima iterazione del ciclo stampo il ds totale


                Nome_fan_corretto1(descrizione_fan(2), conf_sel(0)(4), conf_sel(0)(5), conf_sel(0)(6))
                Dim nome_fan_conf As String = traduzione_ventilatore
                'SALVO IL FILE NELLA DIRECORY IN AUTOMATICO
                Report.ExportOptions.Pdf.ConvertImagesToJpeg = False
                Try

                    If ciclo_stampa_catalogo = 0 Then
                        Report.ExportToPdf(folders_directoryDS & "\" & descrizione_fan & "\" & descrizione_fan & ".pdf")
                    Else
                        Report.ExportToPdf(folders_directoryDS & "\" & descrizione_fan & "\" & nome_catalogo & "\" & descrizione_fan & ".pdf")
                    End If

                Catch ex As Exception

                End Try

                '*************************************************SQL SERVER AGGIUNGO******************************************************************************
                ERP1(n)
                If DS_totale_dati(n, 87) = True And Datasheet_print_mode = 1 And (((DS_totale_dati(n, 4) = "T-Testato") Or descrizione_fan(0) = "U") And ERP_ok = 1) Or (descrizione_fan(0) = "T" And descrizione_fan(0) = "X" And descrizione_fan(0) = "H") Then 'solo se c'è la spunta nell'archivio carico sul sito 
                    If ciclo_stampa_catalogo = 0 Then
                        SQLSERVER_context1(nome_fan_conf, n, "Catalogo")
                    Else
                        SQLSERVER_context1(nome_fan_conf, n, nome_catalogo)
                    End If
                End If
                '**************************************************************************************************************************************************
            End If

        End If


        print_mode = 0


    End Sub



    'Private Sub Q1_TextChanged(sender As Object, e As EventArgs) Handles Q1.TextChanged

    '    Dim oo As String = (CType(sender, Control)).Name

    '    Q1_change()

    'End Sub

    Public Sub Q1_change()



        If Q1.Text = "" Then
            Q1.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            Q1.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            Q1_DS = Q1.Text
        End If


        'CALCOLO LA PARABOLA 
        Try
            x_bassa_grafico(0) = Q1.Text
        Catch ex As Exception

        End Try

        aggiorna_chart18(x_bassa_grafico, y_bassa_grafico, Q3.Text, Q1.Text, 1)



        'cambio punto
        Try

            'Chart18.Series(0).Points(0).XValue() = Q1.Text
            Chart18.Series(2).Points(0).XValue() = Q1.Text
        Catch ex As Exception

        End Try

    End Sub



    Public Sub Qx_Px()

        If tb_DS.Text = "" Then
            tb_DS.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            tb_DS.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            Select Case tb_DS.Name
                Case "Q1"
                    Q1_DS = tb_DS.Text
                Case "Q2"
                    Q2_DS = tb_DS.Text
                Case "Q3"
                    Q3_DS = tb_DS.Text
                Case "Q4"
                    Q4_DS = tb_DS.Text
                Case "Q5"
                    Q5_DS = tb_DS.Text
                Case "Q6"
                    Q6_DS = tb_DS.Text
                Case "P1"
                    P1_DS = tb_DS.Text
                Case "P2"
                    P2_DS = tb_DS.Text
                Case "P3"
                    P3_DS = tb_DS.Text
                Case "P4"
                    P4_DS = tb_DS.Text
                Case "P5"
                    P5_DS = tb_DS.Text
                Case "P6"
                    P6_DS = tb_DS.Text
            End Select
        End If

        'CALCOLO LA PARABOLA 
        Try

            Dim pto_num As Integer = CInt(tb_DS.Name(1).ToString)

            If tb_DS.Name(0) = "Q" Then

                If pto_num <= 3 Then 'BASSA VELOCITà
                    x_bassa_grafico(pto_num - 1) = tb_DS.Text
                    aggiorna_chart18(x_bassa_grafico, y_bassa_grafico, Q3.Text, Q1.Text, 1)
                Else
                    x_alta_grafico(pto_num - 4) = CInt(tb_DS.Text)
                    aggiorna_chart18(x_alta_grafico, y_alta_grafico, Q6.Text, Q4.Text, 2)
                End If


            ElseIf tb_DS.Name(0) = "P" Then

                If pto_num <= 3 Then 'BASSA VELOCITà
                    y_bassa_grafico(pto_num - 1) = tb_DS.Text
                    aggiorna_chart18(x_bassa_grafico, y_bassa_grafico, Q3.Text, Q1.Text, 1)
                Else
                    y_alta_grafico(pto_num - 4) = tb_DS.Text
                    aggiorna_chart18(x_alta_grafico, y_alta_grafico, Q6.Text, Q4.Text, 2)
                End If

            End If




        Catch ex As Exception

        End Try

        'cambio punto
        Try

            Dim pto_num As Integer = CInt(tb_DS.Name(1).ToString)


            'Chart18.Series(0).Points(1).YValues(0) = CInt(P2.Text)
            Chart18.Series(1 + pto_num).Points(0).YValues(0) = CInt(ParGen.Controls("P" & pto_num).Text)

            'Chart18.Series(0).Points(1).XValue() = Q2.Text
            Chart18.Series(1 + pto_num).Points(0).XValue() = ParGen.Controls("Q" & pto_num).Text
        Catch ex As Exception

        End Try



        ricalcolo_coefficienti()


    End Sub

    Private Sub Qx_Px_TextChanged(sender As Object, e As EventArgs) Handles Q1.TextChanged, Q2.TextChanged, Q3.TextChanged, Q4.TextChanged, Q5.TextChanged, Q6.TextChanged,
            P1.TextChanged, P2.TextChanged, P3.TextChanged, P4.TextChanged, P5.TextChanged, P6.TextChanged

        Dim P_Q_name As String = (CType(sender, Control)).Name

        tb_DS = ParGen.Controls(P_Q_name)

        Qx_Px()

    End Sub


    Private Sub Pic_FA_MouseHover(sender As Object, e As EventArgs) Handles Pic_F_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Predisposizione ad attacco Alto profilo Sbordato Aspirante"
    End Sub

    Private Sub Pic_FA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_F_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_MA_MouseHover(sender As Object, e As EventArgs) Handles Pic_M_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Predisposizione ad attacco Alto profilo Flangiato Aspirante"
    End Sub

    Private Sub Pic_MA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_M_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_XP_MouseHover(sender As Object, e As EventArgs) Handles Pic_X_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Predisposizione ad attacco Alto profilo Premente Flangiato o Sbordato"
    End Sub

    Private Sub Pic_XP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_X_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_CP_MouseHover(sender As Object, e As EventArgs) Handles Pic_C_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Predisposizione ad attacco Basso profilo Premente"
    End Sub

    Private Sub Pic_CP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_C_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_CA_MouseHover(sender As Object, e As EventArgs) Handles Pic_C_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Predisposizione ad attacco Basso profilo Aspirante"
    End Sub

    Private Sub Pic_CA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_C_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_HP_MouseHover(sender As Object, e As EventArgs) Handles Pic_H_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Pannello Alto profilo Sbordato Premente"
    End Sub

    Private Sub Pic_HP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_H_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_LP_MouseHover(sender As Object, e As EventArgs) Handles Pic_L_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Boccaglio Alto profilo Flangiato Premente"
    End Sub

    Private Sub Pic_LP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_L_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_QA_MouseHover(sender As Object, e As EventArgs) Handles Pic_Q_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Pannello Alto profilo Sbordato Aspirante"
    End Sub

    Private Sub Pic_QA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_Q_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_GA_MouseHover(sender As Object, e As EventArgs) Handles Pic_G_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Pannello Alto profilo Flangiato Aspirante"
    End Sub

    Private Sub Pic_GA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_G_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_KP_MouseHover(sender As Object, e As EventArgs) Handles Pic_K_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Pannello Alto profilo Flangiato Premente"
    End Sub

    Private Sub Pic_KP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_K_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_PP_MouseHover(sender As Object, e As EventArgs) Handles Pic_P_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Pannello Basso profilo Premente"
    End Sub

    Private Sub Pic_PP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_P_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_PA_MouseHover(sender As Object, e As EventArgs) Handles Pic_P_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Pannello Basso profilo Aspirante"
    End Sub

    Private Sub Pic_PA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_P_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_5A_MouseHover(sender As Object, e As EventArgs) Handles Pic_5_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Pannello Alto profilo raggio ridotto aspirante"
    End Sub

    Private Sub Pic_5A_MouseLeave(sender As Object, e As EventArgs) Handles Pic_5_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_5P_MouseHover(sender As Object, e As EventArgs) Handles Pic_5_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Pannello Alto profilo raggio ridotto premente"
    End Sub

    Private Sub Pic_5P_MouseLeave(sender As Object, e As EventArgs) Handles Pic_5_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_BA_MouseHover(sender As Object, e As EventArgs) Handles Pic_B_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Pannello Alto profilo raggio ridotto"
    End Sub

    Private Sub Pic_BA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_B_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_WA_MouseHover(sender As Object, e As EventArgs) Handles Pic_W_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Boccaglio Alto profilo Flangiato Aspirante"
    End Sub

    Private Sub Pic_WA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_W_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_VP_MouseHover(sender As Object, e As EventArgs) Handles Pic_V_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Boccaglio Alto profilo Sbordato Premente"
    End Sub

    Private Sub Pic_VP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_V_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_TA_MouseHover(sender As Object, e As EventArgs) Handles Pic_T_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Boccaglio Basso profilo Aspirante"
    End Sub

    Private Sub Pic_TA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_T_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_TP_MouseHover(sender As Object, e As EventArgs) Handles Pic_T_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Boccaglio Basso profilo Premente"
    End Sub

    Private Sub Pic_TP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_T_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_JP_MouseHover(sender As Object, e As EventArgs) Handles Pic_J_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Intubato Cassa Lunga coon sedia Imbullonata Premente"
    End Sub

    Private Sub Pic_JP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_J_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_9P_MouseHover(sender As Object, e As EventArgs) Handles Pic_9_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Intubato Cassa Media Premente"
    End Sub

    Private Sub Pic_9P_MouseLeave(sender As Object, e As EventArgs) Handles Pic_9_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_IP_MouseHover(sender As Object, e As EventArgs) Handles Pic_I_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Intubato Cassa Corta coon sedia Imbullonata Premente"
    End Sub

    Private Sub Pic_IP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_I_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_NP_MouseHover(sender As Object, e As EventArgs) Handles Pic_N_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Anello con supporto motore con singolo raggio Premente"
    End Sub

    Private Sub Pic_NP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_N_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_JA_MouseHover(sender As Object, e As EventArgs) Handles Pic_J_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Intubato Cassa Lunga coon sedia Imbullonata Aspirante"
    End Sub

    Private Sub Pic_JA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_J_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_9A_MouseHover(sender As Object, e As EventArgs) Handles Pic_9_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Intubato Cassa Media Aspirante"
    End Sub

    Private Sub Pic_9A_MouseLeave(sender As Object, e As EventArgs) Handles Pic_9_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_IA_MouseHover(sender As Object, e As EventArgs) Handles Pic_I_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Intubato Cassa Corta coon sedia Imbullonata Aspirante"
    End Sub

    Private Sub Pic_IA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_I_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_DA_MouseHover(sender As Object, e As EventArgs) Handles Pic_D_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Anello con sedia saldata con doppio raggio Aspirante"
    End Sub

    Private Sub Pic_DA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_D_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_DP_MouseHover(sender As Object, e As EventArgs) Handles Pic_D_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Anello con sedia saldata con doppio raggio Premente"
    End Sub

    Private Sub Pic_DP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_D_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_AA_MouseHover(sender As Object, e As EventArgs) Handles Pic_A_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Anello doppia Flangia con supporto motore Aspirante"
    End Sub

    Private Sub Pic_AA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_A_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_AP_MouseHover(sender As Object, e As EventArgs) Handles Pic_A_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Anello doppia Flangia con supporto motore Premente"
    End Sub

    Private Sub Pic_AP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_A_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_YA_MouseHover(sender As Object, e As EventArgs) Handles Pic_Y_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Intubato Controrotante Aspirante"
    End Sub

    Private Sub Pic_YA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_Y_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_YP_MouseHover(sender As Object, e As EventArgs) Handles Pic_Y_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Intubato Controrotante Premente"
    End Sub

    Private Sub Pic_YP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_Y_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_OA_MouseHover(sender As Object, e As EventArgs) Handles Pic_O_A.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Anello con supporto motore con doppio raggio Aspiarnte"
    End Sub

    Private Sub Pic_OA_MouseLeave(sender As Object, e As EventArgs) Handles Pic_O_A.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_OP_MouseHover(sender As Object, e As EventArgs) Handles Pic_O_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Anello con supporto motore con doppio raggio Premente"
    End Sub

    Private Sub Pic_OP_MouseLeave(sender As Object, e As EventArgs) Handles Pic_O_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_0_MouseHover(sender As Object, e As EventArgs) Handles Pic_0_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Servoventilazione NS"
    End Sub

    Private Sub Pic_0_MouseLeave(sender As Object, e As EventArgs) Handles Pic_0_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_1_MouseHover(sender As Object, e As EventArgs) Handles Pic_1_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Servoventilazione SE"
    End Sub

    Private Sub Pic_1_MouseLeave(sender As Object, e As EventArgs) Handles Pic_1_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_E_MouseHover(sender As Object, e As EventArgs) Handles Pic_E_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Predisposizione ad attacco Alto profilo Sbordato Aspirante"
    End Sub

    Private Sub Pic_E_MouseLeave(sender As Object, e As EventArgs) Handles Pic_E_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub

    Private Sub Pic_Z_MouseHover(sender As Object, e As EventArgs) Handles Pic_Z_P.MouseHover
        Guna2GroupBox4.Text = "Configurazione: " & "Torrino Assiale"
    End Sub

    Private Sub Pic_Z_MouseLeave(sender As Object, e As EventArgs) Handles Pic_Z_P.MouseLeave
        Guna2GroupBox4.Text = "Configurazioni"
    End Sub



    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Salva_DB.Click

        Importa_Archivio_DS1()

        If blocca_add = 1 Then ' nel caso il datasheet esistesse gia'

            Yes_No_Warning = 0
            Warning.Label1.Text = "Datasheet already exists. Do you want to overwrite?"
            If Warning.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
                'attendo la risposta della box
            End If

            If Yes_No_Warning = 1 Then ' in caso di risposta affermaiva allora aggiorno


                ' Loop over the subdirectories and remove them with their contents
                For Each d In Directory.GetDirectories(folders_directoryDS & "\" & descrizione_fan)
                    Directory.Delete(d, True)
                Next

                Try
                    Cancella_rigaDS1(descrizione_fan) ' cancello la riga gia esistente nel database
                Catch ex As Exception

                End Try


                Ricerca_imm_sel()
                acquisisci_cbx_tbx()
                Aggiungi_DS1()
                Modifica_rigaDS1()


                'Creazioni directories dei ventilatori
                For i = 0 To num_max_config - 1

                    If conf_sel(i) <> "" Then

                        Dim nome_folder As String = conf_sel(i).Substring(conf_sel(i).IndexOf("_") + 1, conf_sel(i).Length - conf_sel(i).IndexOf("_") - 1)

                        System.IO.Directory.CreateDirectory(folders_directoryDS & "\" & descrizione_fan & "\" & nome_folder)

                    End If

                Next


                crea_dir_serie()



            End If



        Else 'se non esiste



            Ricerca_imm_sel()
            acquisisci_cbx_tbx()
            Aggiungi_DS1()
            Modifica_rigaDS1()



            For i = 0 To num_max_config - 1

                If sel_config(i) <> "" Then

                    Dim nome_folder As String = sel_config(i).Substring(sel_config(i).IndexOf("_") + 1, sel_config(i).Length - sel_config(i).IndexOf("_") - 1)

                    System.IO.Directory.CreateDirectory(folders_directoryDS & "\" & descrizione_fan & "\" & nome_folder) 'creo la directory del progetto

                End If

            Next

            crea_dir_serie()


            'copio il file 1 della prova1
            Try
                IO.File.Copy(directory_exc1_completa, folders_directoryDS & "\" & descrizione_fan & "\" & directory_exc1 & ".xlsx", True) 'copy(dalla cartella, alla cartella con nome del file)
            Catch ex As Exception

            End Try


            'copio il file 1 della prova2
            Try
                IO.File.Copy(directory_exc2_completa, folders_directoryDS & "\" & descrizione_fan & "\" & directory_exc2 & ".xlsx", True) 'copy(dalla cartella, alla cartella con nome del file)
            Catch ex As Exception

            End Try

            'copia del modulo di progettazione
            Try
                Dim file_name_MP As String = folders_directoryDS & "\" & descrizione_fan & "\" & "Modulo di Progettazione Ventilatori Assiali.xlsm"

                If System.IO.File.Exists(file_name_MP) = False Then
                    IO.File.Copy(modulo_PJ, file_name_MP, True) 'copy(dalla cartella, alla cartella con nome del file)
                End If

            Catch ex As Exception

            End Try


        End If


        If Yes_No_Warning = 1 Then
            update_all_pdf(0)
        End If


    End Sub




    Public Sub acquisisci_cbx_tbx()

        aggiunta_val_DS = 0

        'Acquisisco le combobox
        For Each item As Control In ParGen.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                vettore_aggiornamentoDS(aggiunta_val_DS) = cb.SelectedItem
                vettore_nomi_aggiornamentoDS(aggiunta_val_DS) = cb.Name

                aggiunta_val_DS = aggiunta_val_DS + 1

            End If
        Next

        For Each item As Control In ParGen.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)


                vettore_aggiornamentoDS(aggiunta_val_DS) = tb.Text
                vettore_nomi_aggiornamentoDS(aggiunta_val_DS) = tb.Name

                aggiunta_val_DS = aggiunta_val_DS + 1


            End If
        Next

    End Sub



    Private Sub IP_TextChanged(sender As Object, e As EventArgs) Handles IP.TextChanged
        If IP.Text = "" Then
            IP.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            IP.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            IP_DS = IP.Text
        End If
    End Sub



    Private Sub Ins_Class_TextChanged(sender As Object, e As EventArgs) Handles Ins_Class.TextChanged
        If Ins_Class.Text = "" Then
            Ins_Class.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            Ins_Class.BorderColor = Color.FromArgb(74, 231, 148)
        End If

        If print_mode = 0 Then
            Ins_DS = Ins_Class.Text
        End If
    End Sub



    Public Sub Ricerca_imm_sel()

        'RICERCO IL NUMERO DI CONFIGURAZIONI SELEZIONATE




        num_conf_sel = 0

        For i = 0 To 50
            conf_sel(i) = Nothing
        Next


        For Each item As Control In Guna2GroupBox4.Controls
            'Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

            If item.GetType Is GetType(System.Windows.Forms.GroupBox) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)


                For Each item1 As Control In gb.Controls



                    If item1.GetType Is GetType(System.Windows.Forms.CheckBox) Then 'cerco le checkbox dentro la groupbox identificata

                        Dim chb As System.Windows.Forms.CheckBox = DirectCast(item1, System.Windows.Forms.CheckBox)

                        If chb.Checked = True Then

                            Try

                                conf_sel(num_conf_sel) = chb.Name
                                num_conf_sel = num_conf_sel + 1

                            Catch ex As Exception


                            End Try


                            For Each item2 As Control In gb.Controls
                                If item2.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then 'cerco le checkbox dentro la groupbox identificata
                                    Dim tbx As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item2, Guna.UI2.WinForms.Guna2TextBox)

                                    part_sel(num_conf_sel - 1) = tbx.Text

                                End If
                            Next


                        End If


                    End If



                Next

            End If
            '--------------------------------------------------------------------------------------------------------------------------------------------------

        Next



        Dim imm As DevExpress.XtraReports.UI.XRPictureBox
        Dim part As DevExpress.XtraReports.UI.XRLabel
        Dim des As DevExpress.XtraReports.UI.XRLabel

        Dim name_imm As String


        '---------------------------------------CICLO DI INSERIMENTO INMMAGINI--------------------------------------------------
        For i = 0 To 4
            imm = Report.Detail.Controls("Pic" & i + 1)
            part = Report.Detail.Controls("part" & i + 1)
            des = Report.Detail.Controls("des" & i + 1)
            imm.ImageSource = Nothing
            part.Text = ""
            des.Text = ""
        Next

        For i = 0 To 4
            imm = Report.Detail.Controls("Pic" & i + 1)
            part = Report.Detail.Controls("part" & i + 1)
            des = Report.Detail.Controls("des" & i + 1)

            If imm.Name = "Pic" & i + 1 Then

                If conf_sel(i) <> Nothing Then

                    name_imm = conf_sel(i).Substring(conf_sel(i).IndexOf("_") + 1, conf_sel(i).Length - conf_sel(i).IndexOf("_") - 1)

                    sel_config(i) = "Pic_" & name_imm

                Else

                    sel_config(i) = ""

                End If


            End If

        Next


    End Sub





    Private Sub RichTextBox2_TextChanged(sender As Object, e As EventArgs) Handles tbx_NOTE.TextChanged

        testRTF1 = tbx_NOTE.Rtf

        Dim rtf1 As New System.Windows.Forms.RichTextBox

        testNote1 = "NOTE: "
        testNote1 = testNote1 & RTFToPlainText(tbx_NOTE)

    End Sub



    Private Function RTFToPlainText(ByVal rtfbox As RichTextBox) As String
        Dim str As String = String.Empty
        For Each line As String In rtfbox.Lines
            str += line
        Next
        Return str
    End Function




    Public Sub Load_file()


        '--------------------------------------APERTURA DELLA BASSA VELOCITA'------------------------------------------

        Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog()
        openFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx*"
        openFileDialog1.Multiselect = True
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.DefaultExt = ".xlsx"
        openFileDialog1.AddExtension = True



        Dim i As Integer = 0
        Dim files(2) As String
        Dim name_check As Integer
        Dim name_check1 As Integer
        Dim numero_file As Integer



        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            If openFileDialog1.FileName IsNot Nothing Then

                files = openFileDialog1.FileNames

                'Caso di 1 sola selezione
                Try
                    Dim prova As String = files(1)
                    numero_file = 2
                Catch ex As Exception '---> se nontrovo il secondo file significa  che ho selezionato un solo file
                    numero_file = 1
                End Try


                If numero_file = 1 Then

                    directory_exc1 = Path.GetFileNameWithoutExtension(files(0))
                    directory_exc1_completa = files(0)

                    name_file_BASSA = files(0)
                    errore_selezione_file = 0
                    S_D_var = 1



                Else
                    'Caso di 2 selezioni
                    S_D_var = 2




                    directory_exc1 = Path.GetFileNameWithoutExtension(files(0))
                    directory_exc2 = Path.GetFileNameWithoutExtension(files(1))
                    directory_exc1_completa = files(0)
                    directory_exc2_completa = files(1)

                    name_check = files(0).IndexOf("BASSA")

                    If name_check > 0 Then
                        name_file_BASSA = files(0)
                    Else
                        name_check = files(0).IndexOf("ALTA")

                        If name_check > 0 Then
                            name_file_ALTA = files(0)
                        Else
                            errore_selezione_file = 1
                        End If

                    End If


                    name_check = files(1).IndexOf("BASSA")

                    If name_check > 0 Then
                        name_file_BASSA = files(1)
                    Else
                        name_check = files(1).IndexOf("ALTA")

                        If name_check > 0 Then
                            name_file_ALTA = files(1)
                        Else
                            errore_selezione_file = 1
                        End If

                    End If


                End If



            End If


        End If

        '----------------------------------------------------------------------------------------------------------------


        If errore_selezione_file = 0 Then

            Ricerca_tabelle1()

            If Tmax_exc = "" Or Tmax_exc = "0" Then
                Tmax.Text = "+" & 60
            Else
                Tmax.Text = "+" & Tmax_exc
            End If

            Dim minQ As Integer = portata_Excel_bassa(0) * 0.95
            Dim maxQ As Integer

            If S_D_var = 1 Then 'BASSA


                If bassa_find = 1 Then


                    minQ = pto3_bassa(0) * 0.95
                    maxQ = pto1_bassa(0) * 1.05



                    For i = 0 To 9
                        Chart18.Series(0).Points.AddXY(0, 0)
                    Next



                    'Chart18.Series(0).Points.AddXY(CInt(pto1_bassa(0)), pto1_bassa(2))
                    Chart18.Series(2).Points.AddXY(CInt(pto1_bassa(0)), pto1_bassa(2))
                    P1.Text = CInt(pto1_bassa(2))
                    Q1.Text = CInt(pto1_bassa(0))
                    x_bassa(0) = CInt(pto1_bassa(0))
                    y_bassa(0) = CInt(pto1_bassa(2))
                    Chart18.Series(2).Color = Color.Black
                    Chart18.Series(2).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(2).MarkerSize = 6
                    RPM1.Text = Math.Round(pto1_bassa(3), 0)
                    POW1.Text = Math.Round(pto1_bassa(4), 0)
                    CURR1.Text = Math.Round(pto1_bassa(5), 2)
                    LWA1.Text = Math.Round(pto1_bassa(6), 1)


                    'Chart18.Series(0).Points.AddXY(CInt(pto2_bassa(0)), pto2_bassa(2))
                    Chart18.Series(3).Points.AddXY(CInt(pto2_bassa(0)), pto2_bassa(2))
                    P2.Text = CInt(pto2_bassa(2))
                    Q2.Text = CInt(pto2_bassa(0))
                    x_bassa(1) = CInt(pto2_bassa(0))
                    y_bassa(1) = CInt(pto2_bassa(2))
                    Chart18.Series(3).Color = Color.Black
                    Chart18.Series(3).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(3).MarkerSize = 6
                    RPM2.Text = Math.Round(pto2_bassa(3), 0)
                    POW2.Text = Math.Round(pto2_bassa(4), 0)
                    CURR2.Text = Math.Round(pto2_bassa(5), 2)
                    LWA2.Text = Math.Round(pto2_bassa(6), 1)


                    'Chart18.Series(0).Points.AddXY(CInt(pto3_bassa(0)), pto3_bassa(2))
                    Chart18.Series(4).Points.AddXY(CInt(pto3_bassa(0)), pto3_bassa(2))
                    P3.Text = CInt(pto3_bassa(2))
                    Q3.Text = CInt(pto3_bassa(0))
                    x_bassa(2) = CInt(pto3_bassa(0))
                    y_bassa(2) = CInt(pto3_bassa(2))
                    Chart18.Series(4).Color = Color.Black
                    Chart18.Series(4).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(4).MarkerSize = 6
                    RPM3.Text = Math.Round(pto3_bassa(3), 0)
                    POW3.Text = Math.Round(pto3_bassa(4), 0)
                    CURR3.Text = Math.Round(pto3_bassa(5), 2)
                    LWA3.Text = Math.Round(pto3_bassa(6), 1)




                    '-------------------------------------DISEGNO GRAFICO--------------------------------------------
                    x_bassa_grafico(0) = pto1_bassa(0)
                    x_bassa_grafico(1) = pto2_bassa(0)
                    x_bassa_grafico(2) = pto3_bassa(0)

                    y_bassa_grafico(0) = pto1_bassa(2)
                    y_bassa_grafico(1) = pto2_bassa(2)
                    y_bassa_grafico(2) = pto3_bassa(2)

                    'CALCOLO LA PARABOLA 
                    aggiorna_chart18(x_bassa_grafico, y_bassa_grafico, pto1_bassa(0), pto3_bassa(0), 1)

                    '-------------------------------------------------------------------------------------------------




                    'calcolo i coefficienti della parabola
                    calcolo_coeff_parabola1(x_bassa, y_bassa)

                    coeff_bassa(0) = A_parabola
                    coeff_bassa(1) = B_parabola
                    coeff_bassa(2) = C_parabola


                    'Dati di targa
                    I_bassa.Text = Math.Round(pto_lavoro_bassa(1), 2)
                    RPM_bassa.Text = CInt(pto_lavoro_bassa(4))
                    pow_bassa.Text = Math.Round(pto_lavoro_bassa(2), 0) '/ 1000, 2)



                    'Numero prova e Descrizione
                    Descrizione.Text = descrizione_prova
                    Test_numero.Text = Test_numero_bassa


                    'Inserimento dati ERP
                    ERP_sel.SelectedIndex = 0
                    'grado_eff.SelectedIndex = 2
                    Tipo_prova.SelectedIndex = 0

                End If


                Label53.Visible = False
                Label54.Visible = False
                Label49.Visible = False
                Label57.Visible = False
                Label56.Visible = False
                Label55.Visible = False
                Label19.Visible = False
                Label20.Visible = False
                Label21.Visible = False
                RPM4.Visible = False
                POW4.Visible = False
                CURR4.Visible = False
                RPM4.Visible = False
                LWA4.Visible = False
                P4.Visible = False
                Q4.Visible = False
                RPM5.Visible = False
                POW5.Visible = False
                CURR5.Visible = False
                LWA5.Visible = False
                P5.Visible = False
                Q5.Visible = False
                RPM6.Visible = False
                POW6.Visible = False
                CURR6.Visible = False
                LWA6.Visible = False
                P6.Visible = False
                Q6.Visible = False







                Tensione_alta.Visible = False
                Label41.Visible = False
                Freq_alta.Visible = False
                I_alta.Visible = False
                RPM_alta.Visible = False
                pow_alta.Visible = False



                Tensione_bassa.SelectedIndex = 4
                Freq_bassa.SelectedIndex = 0


            Else  'DOPPIA VELOCITA'


                Label53.Visible = True
                Label54.Visible = True
                Label49.Visible = True
                Label57.Visible = True
                Label56.Visible = True
                Label55.Visible = True
                Label19.Visible = True
                Label20.Visible = True
                Label21.Visible = True
                RPM4.Visible = True
                POW4.Visible = True
                CURR4.Visible = True
                RPM4.Visible = True
                LWA4.Visible = True
                P4.Visible = True
                Q4.Visible = True
                RPM5.Visible = True
                POW5.Visible = True
                CURR5.Visible = True
                LWA5.Visible = True
                P5.Visible = True
                Q5.Visible = True
                RPM6.Visible = True
                POW6.Visible = True
                CURR6.Visible = True
                LWA6.Visible = True
                P6.Visible = True
                Q6.Visible = True


                Tensione_alta.Visible = True
                Label41.Visible = True
                Freq_alta.Visible = True
                I_alta.Visible = True
                RPM_alta.Visible = True
                pow_alta.Visible = True


                Tensione_bassa.SelectedIndex = 4
                Tensione_alta.SelectedIndex = 17
                Freq_bassa.SelectedIndex = 0
                Freq_alta.SelectedIndex = 0

                'riporto i valori a grfio

                minQ = pto3_bassa(0) * 0.95
                maxQ = pto1_alta(0) * 1.05

                If bassa_find = 1 Then
                    '    For i = 0 To len_bassa
                    '        If portata_Excel_bassa(i) <> 0 Then
                    '            'Chart18.Series(0).Points.AddXY(portata_Excel_bassa(i), Pstat_Excel_bassa(i))

                    '            'Cerco la portata minima per il grafico
                    '            If portata_Excel_bassa(i) <= minQ Then
                    '                minQ = portata_Excel_bassa(i)
                    '            End If

                    '        End If

                    '    Next


                    For i = 0 To 9
                        Chart18.Series(0).Points.AddXY(0, 0)
                    Next


                    'Chart18.Series(0).Points.AddXY(CInt(pto1_bassa(0)), pto1_bassa(2))
                    Chart18.Series(2).Points.AddXY(CInt(pto1_bassa(0)), pto1_bassa(2))
                    P1.Text = CInt(pto1_bassa(2))
                    Q1.Text = CInt(pto1_bassa(0))
                    x_bassa(0) = CInt(pto1_bassa(0))
                    y_bassa(0) = CInt(pto1_bassa(2))
                    Chart18.Series(2).Color = Color.Black
                    Chart18.Series(2).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(2).MarkerSize = 6
                    RPM1.Text = Math.Round(pto1_bassa(3), 0)
                    POW1.Text = Math.Round(pto1_bassa(4), 0)
                    CURR1.Text = Math.Round(pto1_bassa(5), 2)
                    LWA1.Text = Math.Round(pto1_bassa(6), 1)

                    'Chart18.Series(0).Points.AddXY(CInt(pto2_bassa(0)), pto2_bassa(2))
                    Chart18.Series(3).Points.AddXY(CInt(pto2_bassa(0)), pto2_bassa(2))
                    P2.Text = CInt(pto2_bassa(2))
                    Q2.Text = CInt(pto2_bassa(0))
                    x_bassa(1) = CInt(pto2_bassa(0))
                    y_bassa(1) = CInt(pto2_bassa(2))
                    Chart18.Series(3).Color = Color.Black
                    Chart18.Series(3).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(3).MarkerSize = 6
                    RPM2.Text = Math.Round(pto2_bassa(3), 0)
                    POW2.Text = Math.Round(pto2_bassa(4), 0)
                    CURR2.Text = Math.Round(pto2_bassa(5), 2)
                    LWA2.Text = Math.Round(pto2_bassa(6), 1)


                    'Chart18.Series(0).Points.AddXY(CInt(pto3_bassa(0)), pto3_bassa(2))
                    Chart18.Series(4).Points.AddXY(CInt(pto3_bassa(0)), pto3_bassa(2))
                    P3.Text = CInt(pto3_bassa(2))
                    Q3.Text = CInt(pto3_bassa(0))
                    x_bassa(2) = CInt(pto3_bassa(0))
                    y_bassa(2) = CInt(pto3_bassa(2))
                    Chart18.Series(4).Color = Color.Black
                    Chart18.Series(4).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(4).MarkerSize = 6
                    RPM3.Text = Math.Round(pto3_bassa(3), 0)
                    POW3.Text = Math.Round(pto3_bassa(4), 0)
                    CURR3.Text = Math.Round(pto3_bassa(5), 2)
                    LWA3.Text = Math.Round(pto3_bassa(6), 1)



                    '-------------------------------------DISEGNO GRAFICO--------------------------------------------
                    x_bassa_grafico(0) = pto1_bassa(0)
                    x_bassa_grafico(1) = pto2_bassa(0)
                    x_bassa_grafico(2) = pto3_bassa(0)

                    y_bassa_grafico(0) = pto1_bassa(2)
                    y_bassa_grafico(1) = pto2_bassa(2)
                    y_bassa_grafico(2) = pto3_bassa(2)

                    'CALCOLO LA PARABOLA 
                    aggiorna_chart18(x_bassa_grafico, y_bassa_grafico, pto1_bassa(0), pto3_bassa(0), 1)

                    '-------------------------------------------------------------------------------------------------



                    'calcolo i coefficienti della parabola
                    calcolo_coeff_parabola1(x_bassa, y_bassa)
                    coeff_bassa(0) = A_parabola
                    coeff_bassa(1) = B_parabola
                    coeff_bassa(2) = C_parabola



                    'Dati di targa
                    I_bassa.Text = Math.Round(pto_lavoro_bassa(1), 2)
                    RPM_bassa.Text = CInt(pto_lavoro_bassa(4))
                    pow_bassa.Text = Math.Round(pto_lavoro_bassa(2), 0) ' / 1000, 2)

                End If


                If alta_find = 1 Then

                    '    For i = 0 To len_alta
                    '        If portata_Excel_alta(i) <> 0 Then
                    '            Chart18.Series(1).Points.AddXY(portata_Excel_alta(i), Pstat_Excel_alta(i))
                    '        End If
                    '    Next

                    For i = 0 To 9
                        Chart18.Series(1).Points.AddXY(0, 0)
                    Next


                    'Chart18.Series(1).Points.AddXY(CInt(pto1_alta(0)), pto1_alta(2))
                    Chart18.Series(5).Points.AddXY(CInt(pto1_alta(0)), pto1_alta(2))
                    P4.Text = CInt(pto1_alta(2))
                    Q4.Text = CInt(pto1_alta(0))
                    x_alta(0) = CInt(pto1_alta(0))
                    y_alta(0) = CInt(pto1_alta(2))
                    Chart18.Series(5).Color = Color.Black
                    Chart18.Series(5).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(5).MarkerSize = 6
                    RPM4.Text = Math.Round(pto1_alta(3), 0)
                    POW4.Text = Math.Round(pto1_alta(4), 0)
                    CURR4.Text = Math.Round(pto1_alta(5), 2)
                    LWA4.Text = Math.Round(pto1_alta(6), 1)

                    'Chart18.Series(1).Points.AddXY(CInt(pto2_alta(0)), pto2_alta(2))
                    Chart18.Series(6).Points.AddXY(CInt(pto2_alta(0)), pto2_alta(2))
                    P5.Text = CInt(pto2_alta(2))
                    Q5.Text = CInt(pto2_alta(0))
                    x_alta(1) = CInt(pto2_alta(0))
                    y_alta(1) = CInt(pto2_alta(2))
                    Chart18.Series(6).Color = Color.Black
                    Chart18.Series(6).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(6).MarkerSize = 6
                    RPM5.Text = Math.Round(pto2_alta(3), 0)
                    POW5.Text = Math.Round(pto2_alta(4), 0)
                    CURR5.Text = Math.Round(pto2_alta(5), 2)
                    LWA5.Text = Math.Round(pto2_alta(6), 1)


                    'Chart18.Series(1).Points.AddXY(CInt(pto3_alta(0)), pto3_alta(2))
                    Chart18.Series(7).Points.AddXY(CInt(pto3_alta(0)), pto3_alta(2))
                    P6.Text = CInt(pto3_alta(2))
                    Q6.Text = CInt(pto3_alta(0))
                    x_alta(2) = CInt(pto3_alta(0))
                    y_alta(2) = CInt(pto3_alta(2))
                    Chart18.Series(7).Color = Color.Black
                    Chart18.Series(7).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(7).MarkerSize = 6
                    RPM6.Text = Math.Round(pto3_alta(3), 0)
                    POW6.Text = Math.Round(pto3_alta(4), 0)
                    CURR6.Text = Math.Round(pto3_alta(5), 2)
                    LWA6.Text = Math.Round(pto3_alta(6), 1)


                    '-------------------------------------DISEGNO GRAFICO--------------------------------------------
                    x_alta_grafico(0) = pto1_alta(0)
                    x_alta_grafico(1) = pto2_alta(0)
                    x_alta_grafico(2) = pto3_alta(0)

                    y_alta_grafico(0) = pto1_alta(2)
                    y_alta_grafico(1) = pto2_alta(2)
                    y_alta_grafico(2) = pto3_alta(2)

                    'CALCOLO LA PARABOLA 
                    aggiorna_chart18(x_alta_grafico, y_alta_grafico, pto1_alta(0), pto3_alta(0), 2)

                    '-------------------------------------------------------------------------------------------------



                    'calcolo i coefficienti della parabola
                    calcolo_coeff_parabola1(x_alta, y_alta)
                    coeff_alta(0) = A_parabola
                    coeff_alta(1) = B_parabola
                    coeff_alta(2) = C_parabola

                    'Dati di targa
                    I_alta.Text = Math.Round(pto_lavoro_alta(1), 2)
                    RPM_alta.Text = CInt(pto_lavoro_alta(4))
                    pow_alta.Text = Math.Round(pto_lavoro_alta(2), 0) '/ 1000, 2)


                    'Numero prova e Descrizione
                    Descrizione.Text = descrizione_prova
                    Test_numero.Text = Test_numero_bassa & ";" & Test_numero_alta


                    'Inserimento dati ERP
                    ERP_sel.SelectedIndex = 0
                    'grado_eff.SelectedIndex = 2
                    Tipo_prova.SelectedIndex = 0


                End If



                Label19.Visible = True
                Label20.Visible = True
                Label21.Visible = True
                RPM4.Visible = True
                POW4.Visible = True
                CURR4.Visible = True
                RPM4.Visible = True
                LWA4.Visible = True
                RPM5.Visible = True
                POW5.Visible = True
                CURR5.Visible = True
                LWA5.Visible = True
                RPM6.Visible = True
                POW6.Visible = True
                CURR6.Visible = True
                LWA6.Visible = True



                Tensione_alta.Visible = True
                Freq_alta.Visible = True
                I_alta.Visible = True
                RPM_alta.Visible = True
                pow_alta.Visible = True



            End If



            Chart18.Series(0).Color = Color.FromArgb(0, 192, 0)
            Chart18.Series(1).Color = Color.Blue
            Chart18.ChartAreas(0).AxisX.Minimum = Math.Floor(minQ / 1000) * 1000
            Chart18.ChartAreas(0).AxisX.Maximum = Math.Ceiling(maxQ / 1000) * 1000

            If maxQ < 10000 Then
                Chart18.ChartAreas(0).AxisX.Interval = 1000
            ElseIf maxQ > 10000 And maxQ < 30000 Then
                Chart18.ChartAreas(0).AxisX.Interval = 2000
            ElseIf maxQ > 10000 And maxQ < 30000 Then
                Chart18.ChartAreas(0).AxisX.Interval = 3000
            End If

            Chart18.ChartAreas(0).AxisX.LabelStyle.Font = New System.Drawing.Font("Segoe UI", 7.0F)
            Chart18.ChartAreas(0).AxisY.LabelStyle.Font = New System.Drawing.Font("Segoe UI", 7.0F)
            'Chart18.ChartAreas(0).AxisY.LabelStyle.ForeColor = System.Drawing.Color.Red


            'Attivo la visibilita' di tutto il form
            Stampa_DS.Visible = True
            Salva_DB.Visible = True

            'ParGen.Size = New System.Drawing.Size(1625, 252)
            'Guna2Button2.Location = New System.Drawing.Point(111, 2)
            'Guna2GroupBox3.Visible = True


            Label1.Visible = False
        Else


            Label1.Visible = True


        End If



        '-----------------------------------------Faccio scattare l'aggiornamento del grafico------------------------------------------------------------------------
        If S_D_var = 1 Then
            Try
                Qx_Px()
            Catch ex As Exception

            End Try
        End If

        If S_D_var = 2 Then
            Try
                tb_DS = ParGen.Controls("Q1")
                Qx_Px()
                tb_DS = ParGen.Controls("Q4")
                Qx_Px()
            Catch ex As Exception

            End Try
        End If
        '------------------------------------------------------------------------------------------------------------------------------------------------------------

    End Sub




    Public Sub Load_Archivio()

        'Attivo la visibilita' di tutto il form
        Stampa_DS.Visible = True
        Salva_DB.Visible = True
        Label1.Visible = False

        'Riporto ala riapertura normale quando clicko "Apri prova"
        mod_archivio = 2
        'load_var = 0

        'ricerco la posizione del file sezionato nella gridview
        For i = 0 To numero_DS - 1

            If DS_totale_dati(i, 1) = lista_DS_sel_name Then
                lista_DS_sel = i
            End If

        Next


        If DS_totale_dati(lista_DS_sel, 5) = "V" Then
            rad_Vip.Checked = True
        ElseIf DS_totale_dati(lista_DS_sel, 5) = "A" Then
            rad_Atex.Checked = True
        ElseIf DS_totale_dati(lista_DS_sel, 5) = "U" Then
            rad_Uni.Checked = True
            frame_motore_sel.SelectedItem = DS_totale_dati(lista_DS_sel, 8)
        End If


        Descrizione.Text = DS_totale_dati(lista_DS_sel, 1)
        Test_numero.Text = DS_totale_dati(lista_DS_sel, 3)
        Tipo_prova.SelectedItem = DS_totale_dati(lista_DS_sel, 4)

        Try
            tbx_NOTE.Rtf = DS_totale_dati(lista_DS_sel, 86)
        Catch ex As Exception
            tbx_NOTE.Text = DS_totale_dati(lista_DS_sel, 86)
        End Try



        alt_Zihel.Text = DS_totale_dati(lista_DS_sel, 6)
        alt_EBM.Text = DS_totale_dati(lista_DS_sel, 7)
        frame_motore_sel.SelectedItem = DS_totale_dati(lista_DS_sel, 7)
        ERP_sel.SelectedItem = DS_totale_dati(lista_DS_sel, 9)
        rendimento_M.Text = DS_totale_dati(lista_DS_sel, 10)
        ERP_Target_M.Text = DS_totale_dati(lista_DS_sel, 11)
        grado_eff.SelectedItem = DS_totale_dati(lista_DS_sel, 12)
        categoria_prova.SelectedItem = DS_totale_dati(lista_DS_sel, 13)
        categoria_eff.SelectedItem = DS_totale_dati(lista_DS_sel, 14)
        potenza_M.Text = DS_totale_dati(lista_DS_sel, 15)
        portata_M.Text = DS_totale_dati(lista_DS_sel, 16)
        pressione_M.Text = DS_totale_dati(lista_DS_sel, 17)
        RPM_M.Text = DS_totale_dati(lista_DS_sel, 18)
        pow_installata.Text = DS_totale_dati(lista_DS_sel, 19)
        Tmin.Text = DS_totale_dati(lista_DS_sel, 20)
        Tmax.Text = DS_totale_dati(lista_DS_sel, 21)
        Tensione_bassa.SelectedItem = DS_totale_dati(lista_DS_sel, 22)
        Freq_bassa.SelectedItem = DS_totale_dati(lista_DS_sel, 23)
        I_bassa.Text = DS_totale_dati(lista_DS_sel, 24)
        RPM_bassa.Text = DS_totale_dati(lista_DS_sel, 25)
        pow_bassa.Text = DS_totale_dati(lista_DS_sel, 26)
        Tensione_alta.SelectedItem = DS_totale_dati(lista_DS_sel, 27)
        Freq_alta.SelectedItem = DS_totale_dati(lista_DS_sel, 28)
        I_alta.Text = DS_totale_dati(lista_DS_sel, 29)
        RPM_alta.Text = DS_totale_dati(lista_DS_sel, 30)
        pow_alta.Text = DS_totale_dati(lista_DS_sel, 31)
        Q1.Text = DS_totale_dati(lista_DS_sel, 32)
        P1.Text = DS_totale_dati(lista_DS_sel, 33)
        RPM1.Text = DS_totale_dati(lista_DS_sel, 34)
        POW1.Text = DS_totale_dati(lista_DS_sel, 35)
        CURR1.Text = DS_totale_dati(lista_DS_sel, 36)
        LWA1.Text = DS_totale_dati(lista_DS_sel, 37)
        Q2.Text = DS_totale_dati(lista_DS_sel, 38)
        P2.Text = DS_totale_dati(lista_DS_sel, 39)
        RPM2.Text = DS_totale_dati(lista_DS_sel, 40)
        POW2.Text = DS_totale_dati(lista_DS_sel, 41)
        CURR2.Text = DS_totale_dati(lista_DS_sel, 42)
        LWA2.Text = DS_totale_dati(lista_DS_sel, 43)
        Q3.Text = DS_totale_dati(lista_DS_sel, 44)
        P3.Text = DS_totale_dati(lista_DS_sel, 45)
        RPM3.Text = DS_totale_dati(lista_DS_sel, 46)
        POW3.Text = DS_totale_dati(lista_DS_sel, 47)
        CURR3.Text = DS_totale_dati(lista_DS_sel, 48)
        LWA3.Text = DS_totale_dati(lista_DS_sel, 49)
        Q4.Text = DS_totale_dati(lista_DS_sel, 50)
        P4.Text = DS_totale_dati(lista_DS_sel, 51)
        RPM4.Text = DS_totale_dati(lista_DS_sel, 52)
        POW4.Text = DS_totale_dati(lista_DS_sel, 53)
        CURR4.Text = DS_totale_dati(lista_DS_sel, 54)
        LWA4.Text = DS_totale_dati(lista_DS_sel, 55)
        Q5.Text = DS_totale_dati(lista_DS_sel, 56)
        P5.Text = DS_totale_dati(lista_DS_sel, 57)
        RPM5.Text = DS_totale_dati(lista_DS_sel, 58)
        POW5.Text = DS_totale_dati(lista_DS_sel, 59)
        CURR5.Text = DS_totale_dati(lista_DS_sel, 60)
        LWA5.Text = DS_totale_dati(lista_DS_sel, 61)
        Q6.Text = DS_totale_dati(lista_DS_sel, 62)
        P6.Text = DS_totale_dati(lista_DS_sel, 63)
        RPM6.Text = DS_totale_dati(lista_DS_sel, 64)
        POW6.Text = DS_totale_dati(lista_DS_sel, 65)
        CURR6.Text = DS_totale_dati(lista_DS_sel, 66)
        LWA6.Text = DS_totale_dati(lista_DS_sel, 67)
        cbx_cat1.Text = DS_totale_dati(lista_DS_sel, 88)
        cbx_cat2.Text = DS_totale_dati(lista_DS_sel, 89)
        cbx_cat3.Text = DS_totale_dati(lista_DS_sel, 90)
        cbx_cat4.Text = DS_totale_dati(lista_DS_sel, 91)
        cbx_cat5.Text = DS_totale_dati(lista_DS_sel, 92)


        cbx_AtexProtezione.SelectedItem = DS_totale_dati(lista_DS_sel, 93)
        cbx_AtexCustodia.SelectedItem = DS_totale_dati(lista_DS_sel, 94)
        cbx_AtexCategoria.SelectedItem = DS_totale_dati(lista_DS_sel, 95)
        cbx_AtexClasseTemperatura.SelectedItem = DS_totale_dati(lista_DS_sel, 96)

        ricalcolo_coefficienti()

        'coeff_bassa(0) = DS_totale_dati(lista_DS_sel, 68)
        'coeff_bassa(1) = DS_totale_dati(lista_DS_sel, 69)
        'coeff_bassa(2) = DS_totale_dati(lista_DS_sel, 70)

        'coeff_alta(0) = DS_totale_dati(lista_DS_sel, 71)
        'coeff_alta(1) = DS_totale_dati(lista_DS_sel, 72)
        'coeff_alta(2) = DS_totale_dati(lista_DS_sel, 73)

        directory_exc1 = DS_totale_dati(lista_DS_sel, 74)
        directory_exc2 = DS_totale_dati(lista_DS_sel, 75)



        RichTextBox1.Select()

        Try
            RichTextBox1.Rtf = DS_totale_dati(lista_DS_sel, 76)
        Catch ex As Exception
            RichTextBox1.Text = DS_totale_dati(lista_DS_sel, 76)
        End Try

        check_taglio.SelectedItem = DS_totale_dati(lista_DS_sel, 82)

        cbx_PJ_ambiente.SelectedItem = DS_totale_dati(lista_DS_sel, 83)
        tbx_true_eff.Text = DS_totale_dati(lista_DS_sel, 84)

        If DS_totale_dati(lista_DS_sel, 85) <> "" Then
            tbx_user_modifiche.Text = DS_totale_dati(lista_DS_sel, 85) & ";" & nome_macchina
        Else
            tbx_user_modifiche.Text = nome_macchina
        End If

        '********************************************COMPILAZIONE CHECKBOXES CONFIGURAZIONI SELEZIONATE******************************************


        '---------------------------------Identificazione delle configurazione dalla string del DB ----------------------------------------------
        Dim vettore_immagini(num_max_config) As String 'vettore che accoglie le configurazioni da database ---> dalla cella conf1
        Dim configDB As String = DS_totale_dati(lista_DS_sel, 77)
        Dim count_config As Integer = 0

        Do While configDB.Length > 1
            vettore_immagini(count_config) = configDB.Substring(0, configDB.IndexOf(";"))
            configDB = configDB.Substring(configDB.IndexOf(";") + 1, configDB.Length - configDB.IndexOf(";") - 1)


            count_config = count_config + 1
        Loop
        '----------------------------------------------------------------------------------------------------------------------------------------



        '---------------------------------Identificazione delle configurazione dalla string del DB ----------------------------------------------
        Dim vettore_part(num_max_config) As String 'vettore che accoglie le configurazioni da database ---> dalla cella conf1
        Dim partDB As String = DS_totale_dati(lista_DS_sel, 81)
        Dim count_part As Integer = 0

        Do While partDB.Length > 1
            vettore_part(count_part) = partDB.Substring(0, partDB.IndexOf(";"))
            partDB = partDB.Substring(partDB.IndexOf(";") + 1, partDB.Length - partDB.IndexOf(";") - 1)


            count_part = count_part + 1
        Loop
        '----------------------------------------------------------------------------------------------------------------------------------------



        Try
            'Ciclo di ricerca e sostituzione immagini atex

            For j = 0 To num_max_config - 1

                For Each item As Control In Guna2GroupBox4.Controls
                    'Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                    If item.GetType Is GetType(System.Windows.Forms.GroupBox) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                        Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                        For Each item1 As Control In gb.Controls

                            If item1.GetType Is GetType(System.Windows.Forms.CheckBox) Then 'cerco le checkbox dentro la groupbox identificata

                                Dim chb As System.Windows.Forms.CheckBox = DirectCast(item1, System.Windows.Forms.CheckBox)
                                'Dim tbx As System.Windows.Forms.TextBox = DirectCast(item1, System.Windows.Forms.TextBox)

                                If chb.Name = vettore_immagini(j) Then

                                    chb.Checked = True


                                    Dim tbx As Guna.UI2.WinForms.Guna2TextBox
                                    Dim tbx_name As String = "tbx_" & vettore_immagini(j).Substring(vettore_immagini(j).IndexOf("_") + 1, vettore_immagini(j).Length - vettore_immagini(j).IndexOf("_") - 1)

                                    tbx = gb.Controls(tbx_name)
                                    tbx.Text = vettore_part(j)

                                End If

                            End If





                        Next

                    End If
                Next

            Next

        Catch ex As Exception

        End Try


        '***************************************************************************************************************************************




        IP.Text = DS_totale_dati(lista_DS_sel, 79)
        Ins_Class.Text = DS_totale_dati(lista_DS_sel, 80)







        'Try
        '    'Ciclo di ricerca e sostituzione immagini atex

        '    For j = 0 To num_max_config - 1

        '        For Each item As Control In Guna2GroupBox4.Controls
        '            'Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

        '            If item.GetType Is GetType(System.Windows.Forms.GroupBox) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

        '                Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

        '                For Each item1 As Control In gb.Controls

        '                    If item1.GetType Is GetType(System.Windows.Forms.TextBox) Then 'cerco le checkbox dentro la groupbox identificata

        '                        Dim tbx As System.Windows.Forms.TextBox = DirectCast(item1, System.Windows.Forms.TextBox)

        '                        If tbx.Name = vettore_part(j) Then

        '                            chb.Checked = True

        '                        End If

        '                    End If

        '                Next

        '            End If
        '        Next

        '    Next

        'Catch ex As Exception

        'End Try


        '***************************************************************************************************************************************



        Dim doppia_singola As Integer = 0
        Dim rpm_star_1 As String = DS_totale_dati(lista_DS_sel, 34)
        Dim rpm_star_2 As String = DS_totale_dati(lista_DS_sel, 52)

        If rpm_star_1 <> "" And rpm_star_2 <> "" Then 'doppia velocità
            doppia_singola = 2
        Else
            doppia_singola = 1
        End If





        If doppia_singola = 2 Then 'DOPPIA VELOCITà

            Label53.Visible = True
            Label54.Visible = True
            Label49.Visible = True
            Label57.Visible = True
            Label56.Visible = True
            Label55.Visible = True
            Label19.Visible = True
            Label20.Visible = True
            Label21.Visible = True
            RPM4.Visible = True
            POW4.Visible = True
            CURR4.Visible = True
            RPM4.Visible = True
            LWA4.Visible = True
            P4.Visible = True
            Q4.Visible = True
            RPM5.Visible = True
            POW5.Visible = True
            CURR5.Visible = True
            LWA5.Visible = True
            P5.Visible = True
            Q5.Visible = True
            RPM6.Visible = True
            POW6.Visible = True
            CURR6.Visible = True
            LWA6.Visible = True
            P6.Visible = True
            Q6.Visible = True
            Tensione_alta.Visible = True
            Label41.Visible = True
            Freq_alta.Visible = True
            I_alta.Visible = True
            RPM_alta.Visible = True
            pow_alta.Visible = True


            Dim minQ As Double
            Dim maxQ As Double

            minQ = CInt(Q3.Text) * 0.95
            maxQ = CInt(Q4.Text) * 1.05

            For i = 0 To 9
                Chart18.Series(0).Points.AddXY(0, 0)
            Next

            'Chart18.Series(0).Points.AddXY(CInt(Q1.Text), CInt(P1.Text))
            Chart18.Series(2).Points.AddXY(CInt(Q1.Text), CInt(P1.Text))
            Chart18.Series(2).Color = Color.Black
            Chart18.Series(2).MarkerStyle = MarkerStyle.Circle
            Chart18.Series(2).MarkerSize = 6


            ''Chart18.Series(0).Points.AddXY(CInt(Q2.Text), CInt(P2.Text))
            Chart18.Series(3).Points.AddXY(CInt(Q2.Text), CInt(P2.Text))
            Chart18.Series(3).Color = Color.Black
            Chart18.Series(3).MarkerStyle = MarkerStyle.Circle
            Chart18.Series(3).MarkerSize = 6


            'Chart18.Series(0).Points.AddXY(CInt(Q3.Text), CInt(P3.Text))
            Chart18.Series(4).Points.AddXY(CInt(Q3.Text), CInt(P3.Text))
            Chart18.Series(4).Color = Color.Black
            Chart18.Series(4).MarkerStyle = MarkerStyle.Circle
            Chart18.Series(4).MarkerSize = 6


            '-------------------------------------DISEGNO GRAFICO--------------------------------------------
            x_bassa_grafico(0) = Q1.Text
            x_bassa_grafico(1) = Q2.Text
            x_bassa_grafico(2) = Q3.Text

            y_bassa_grafico(0) = P1.Text
            y_bassa_grafico(1) = P2.Text
            y_bassa_grafico(2) = P3.Text

            'CALCOLO LA PARABOLA 
            aggiorna_chart18(x_bassa_grafico, y_bassa_grafico, Q3.Text, Q1.Text, 1)
            '-------------------------------------------------------------------------------------------------




            For i = 0 To 9
                Chart18.Series(1).Points.AddXY(0, 0)
            Next

            'Chart18.Series(1).Points.AddXY(CInt(Q4.Text), CInt(P4.Text))
            Chart18.Series(5).Points.AddXY(CInt(Q4.Text), CInt(P4.Text))
            Chart18.Series(5).Color = Color.Black
            Chart18.Series(5).MarkerStyle = MarkerStyle.Circle
            Chart18.Series(5).MarkerSize = 6


            'Chart18.Series(1).Points.AddXY(CInt(Q5.Text), CInt(P5.Text))
            Chart18.Series(6).Points.AddXY(CInt(Q5.Text), CInt(P5.Text))
            Chart18.Series(6).Color = Color.Black
            Chart18.Series(6).MarkerStyle = MarkerStyle.Circle
            Chart18.Series(6).MarkerSize = 6



            'Chart18.Series(1).Points.AddXY(CInt(Q6.Text), CInt(P6.Text))
            Chart18.Series(7).Points.AddXY(CInt(Q6.Text), CInt(P6.Text))
            Chart18.Series(7).Color = Color.Black
            Chart18.Series(7).MarkerStyle = MarkerStyle.Circle
            Chart18.Series(7).MarkerSize = 6


            '-------------------------------------DISEGNO GRAFICO--------------------------------------------
            x_alta_grafico(0) = Q4.Text
            x_alta_grafico(1) = Q5.Text
            x_alta_grafico(2) = Q6.Text

            y_alta_grafico(0) = P4.Text
            y_alta_grafico(1) = P5.Text
            y_alta_grafico(2) = P6.Text

            'CALCOLO LA PARABOLA 
            aggiorna_chart18(x_alta_grafico, y_alta_grafico, Q6.Text, Q4.Text, 2)
            '-------------------------------------------------------------------------------------------------


            Chart18.ChartAreas(0).AxisX.Minimum = Math.Floor(minQ / 1000) * 1000
            Chart18.ChartAreas(0).AxisX.Maximum = Math.Ceiling(maxQ / 1000) * 1000

            If maxQ < 10000 Then
                Chart18.ChartAreas(0).AxisX.Interval = 1000
            ElseIf maxQ > 10000 And maxQ < 30000 Then
                Chart18.ChartAreas(0).AxisX.Interval = 2000
            ElseIf maxQ > 10000 And maxQ < 30000 Then
                Chart18.ChartAreas(0).AxisX.Interval = 3000
            End If


        Else 'SINGOLA VELOCITà

            Label53.Visible = False
            Label54.Visible = False
            Label49.Visible = False
            Label57.Visible = False
            Label56.Visible = False
            Label55.Visible = False
            Label19.Visible = False
            Label20.Visible = False
            Label21.Visible = False
            RPM4.Visible = False
            POW4.Visible = False
            CURR4.Visible = False
            RPM4.Visible = False
            LWA4.Visible = False
            P4.Visible = False
            Q4.Visible = False
            RPM5.Visible = False
            POW5.Visible = False
            CURR5.Visible = False
            LWA5.Visible = False
            P5.Visible = False
            Q5.Visible = False
            RPM6.Visible = False
            POW6.Visible = False
            CURR6.Visible = False
            LWA6.Visible = False
            P6.Visible = False
            Q6.Visible = False
            Tensione_alta.Visible = False
            Label41.Visible = False
            Freq_alta.Visible = False
            I_alta.Visible = False
            RPM_alta.Visible = False
            pow_alta.Visible = False






            Dim minQ As Double
            Dim maxQ As Double

            minQ = CInt(Q3.Text) * 0.95
            maxQ = CInt(Q1.Text) * 1.05


            For i = 0 To 9
                Chart18.Series(0).Points.AddXY(0, 0)
            Next


            'Chart18.Series(0).Points.AddXY(CInt(Q1.Text), CInt(P1.Text))
            Chart18.Series(2).Points.AddXY(CInt(Q1.Text), CInt(P1.Text))
            Chart18.Series(2).Color = Color.Black
            Chart18.Series(2).MarkerStyle = MarkerStyle.Circle
            Chart18.Series(2).MarkerSize = 6


            'Chart18.Series(0).Points.AddXY(CInt(Q2.Text), CInt(P2.Text))
            Chart18.Series(3).Points.AddXY(CInt(Q2.Text), CInt(P2.Text))
            Chart18.Series(3).Color = Color.Black
            Chart18.Series(3).MarkerStyle = MarkerStyle.Circle
            Chart18.Series(3).MarkerSize = 6


            'Chart18.Series(0).Points.AddXY(CInt(Q3.Text), CInt(P3.Text))
            Chart18.Series(4).Points.AddXY(CInt(Q3.Text), CInt(P3.Text))
            Chart18.Series(4).Color = Color.Black
            Chart18.Series(4).MarkerStyle = MarkerStyle.Circle
            Chart18.Series(4).MarkerSize = 6

            '-------------------------------------DISEGNO GRAFICO--------------------------------------------
            x_bassa_grafico(0) = Q1.Text
            x_bassa_grafico(1) = Q2.Text
            x_bassa_grafico(2) = Q3.Text

            y_bassa_grafico(0) = P1.Text
            y_bassa_grafico(1) = P2.Text
            y_bassa_grafico(2) = P3.Text


            'CALCOLO LA PARABOLA 
            aggiorna_chart18(x_bassa_grafico, y_bassa_grafico, Q3.Text, Q1.Text, 1)
            '-------------------------------------------------------------------------------------------------


            Chart18.ChartAreas(0).AxisX.Minimum = Math.Floor(minQ / 1000) * 1000
            Chart18.ChartAreas(0).AxisX.Maximum = Math.Ceiling(maxQ / 1000) * 1000

            If maxQ < 10000 Then
                Chart18.ChartAreas(0).AxisX.Interval = 1000
            ElseIf maxQ > 10000 And maxQ < 30000 Then
                Chart18.ChartAreas(0).AxisX.Interval = 2000
            ElseIf maxQ > 10000 And maxQ < 30000 Then
                Chart18.ChartAreas(0).AxisX.Interval = 3000
            End If




        End If










    End Sub





    Public Sub Load_correzione_archivio()



        '--------------------------------------APERTURA DELLA BASSA VELOCITA'------------------------------------------

        Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog()
        openFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx*"
        openFileDialog1.InitialDirectory = folders_directoryDS & "\" & DS_totale_dati(lista_DS_sel, 1)
        openFileDialog1.Multiselect = True
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.DefaultExt = ".xlsx"
        openFileDialog1.AddExtension = True



        Dim i As Integer = 0
        Dim files(2) As String
        Dim name_check As Integer
        Dim name_check1 As Integer
        Dim numero_file As Integer



        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            If openFileDialog1.FileName IsNot Nothing Then

                files = openFileDialog1.FileNames

                'Caso di 1 sola selezione
                Try
                    Dim prova As String = files(1)
                    numero_file = 2
                Catch ex As Exception '---> se nontrovo il secondo file significa  che ho selezionato un solo file
                    numero_file = 1
                End Try


                If numero_file = 1 Then


                    directory_exc1 = Path.GetFileNameWithoutExtension(files(0))
                    directory_exc1_completa = files(0)

                    name_file_BASSA = files(0)
                    errore_selezione_file = 0
                    S_D_var = 1

                    'name_check = name_file_BASSA.IndexOf("BASSA") 'controllo che il nome del file indichi "BASSA"

                    'If name_check > 0 Then
                    '    S_D_var = 1 'indica che ho 1 solo file
                    '    errore_selezione_file = 0
                    'Else
                    '    errore_selezione_file = 1
                    'End If

                Else
                    'Caso di 2 selezioni
                    S_D_var = 2




                    directory_exc1 = Path.GetFileNameWithoutExtension(files(0))
                    directory_exc2 = Path.GetFileNameWithoutExtension(files(1))
                    directory_exc1_completa = files(0)
                    directory_exc2_completa = files(1)

                    name_check = files(0).IndexOf("BASSA")

                    If name_check > 0 Then
                        name_file_BASSA = files(0)
                    Else
                        name_check = files(0).IndexOf("ALTA")

                        If name_check > 0 Then
                            name_file_ALTA = files(0)
                        Else
                            errore_selezione_file = 1
                        End If

                    End If


                    name_check = files(1).IndexOf("BASSA")

                    If name_check > 0 Then
                        name_file_BASSA = files(1)
                    Else
                        name_check = files(1).IndexOf("ALTA")

                        If name_check > 0 Then
                            name_file_ALTA = files(1)
                        Else
                            errore_selezione_file = 1
                        End If

                    End If


                End If



            End If


        End If

        '----------------------------------------------------------------------------------------------------------------


        If errore_selezione_file = 0 Then

            Ricerca_tabelle1()


            If Tmax_exc = "" Or Tmax_exc = "0" Then
                Tmax.Text = "+" & 60
            Else
                Tmax.Text = "+" & Tmax_exc
            End If



            Dim minQ As Integer = portata_Excel_bassa(0) * 0.95
            Dim maxQ As Integer

            If S_D_var = 1 Then 'BASSA


                'riporto i valori a grfio

                If bassa_find = 1 Then
                    'For i = 0 To len_bassa
                    '    If portata_Excel_bassa(i) <> 0 Then
                    '        'Chart18.Series(0).Points.AddXY(portata_Excel_bassa(i), Pstat_Excel_bassa(i))

                    '        'Cerco la portata minima per il grafico
                    '        If portata_Excel_bassa(i) <= minQ Then
                    '            minQ = portata_Excel_bassa(i)
                    '        End If

                    '    End If

                    'Next

                    minQ = pto3_bassa(0) * 0.95
                    maxQ = pto1_bassa(0) * 1.05


                    For i = 0 To 9
                        Chart18.Series(0).Points.AddXY(0, 0)
                    Next


                    'Chart18.Series(0).Points.AddXY(CInt(pto1_bassa(0)), pto1_bassa(2))
                    Chart18.Series(2).Points.AddXY(CInt(pto1_bassa(0)), pto1_bassa(2))
                    P1.Text = CInt(pto1_bassa(2))
                    Q1.Text = CInt(pto1_bassa(0))
                    x_bassa(0) = CInt(pto1_bassa(0))
                    y_bassa(0) = CInt(pto1_bassa(2))
                    Chart18.Series(2).Color = Color.Black
                    Chart18.Series(2).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(2).MarkerSize = 6
                    RPM1.Text = Math.Round(pto1_bassa(3), 0)
                    POW1.Text = Math.Round(pto1_bassa(4), 0)
                    CURR1.Text = Math.Round(pto1_bassa(5), 2)
                    LWA1.Text = Math.Round(pto1_bassa(6), 1)


                    'Chart18.Series(0).Points.AddXY(CInt(pto2_bassa(0)), pto2_bassa(2))
                    Chart18.Series(3).Points.AddXY(CInt(pto2_bassa(0)), pto2_bassa(2))
                    P2.Text = CInt(pto2_bassa(2))
                    Q2.Text = CInt(pto2_bassa(0))
                    x_bassa(1) = CInt(pto2_bassa(0))
                    y_bassa(1) = CInt(pto2_bassa(2))
                    Chart18.Series(3).Color = Color.Black
                    Chart18.Series(3).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(3).MarkerSize = 6
                    RPM2.Text = Math.Round(pto2_bassa(3), 0)
                    POW2.Text = Math.Round(pto2_bassa(4), 0)
                    CURR2.Text = Math.Round(pto2_bassa(5), 2)
                    LWA2.Text = Math.Round(pto2_bassa(6), 1)


                    'Chart18.Series(0).Points.AddXY(CInt(pto3_bassa(0)), pto3_bassa(2))
                    Chart18.Series(4).Points.AddXY(CInt(pto3_bassa(0)), pto3_bassa(2))
                    P3.Text = CInt(pto3_bassa(2))
                    Q3.Text = CInt(pto3_bassa(0))
                    x_bassa(2) = CInt(pto3_bassa(0))
                    y_bassa(2) = CInt(pto3_bassa(2))
                    Chart18.Series(4).Color = Color.Black
                    Chart18.Series(4).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(4).MarkerSize = 6
                    RPM3.Text = Math.Round(pto3_bassa(3), 0)
                    POW3.Text = Math.Round(pto3_bassa(4), 0)
                    CURR3.Text = Math.Round(pto3_bassa(5), 2)
                    LWA3.Text = Math.Round(pto3_bassa(6), 1)


                    '-------------------------------------DISEGNO GRAFICO--------------------------------------------
                    x_bassa_grafico(0) = Q1.Text
                    x_bassa_grafico(1) = Q2.Text
                    x_bassa_grafico(2) = Q3.Text

                    y_bassa_grafico(0) = P1.Text
                    y_bassa_grafico(1) = P2.Text
                    y_bassa_grafico(2) = P3.Text

                    'CALCOLO LA PARABOLA 
                    aggiorna_chart18(x_bassa_grafico, y_bassa_grafico, Q3.Text, Q1.Text, 1)
                    '-------------------------------------------------------------------------------------------------






                    'calcolo i coefficienti della parabola
                    calcolo_coeff_parabola1(x_bassa, y_bassa)

                    coeff_bassa(0) = A_parabola
                    coeff_bassa(1) = B_parabola
                    coeff_bassa(2) = C_parabola


                    'Dati di targa
                    I_bassa.Text = Math.Round(pto_lavoro_bassa(1), 2)
                    RPM_bassa.Text = CInt(pto_lavoro_bassa(4))
                    pow_bassa.Text = Math.Round(pto_lavoro_bassa(2), 0) '/ 1000, 2)



                    'Numero prova e Descrizione
                    Descrizione.Text = descrizione_prova
                    Test_numero.Text = Test_numero_bassa


                    'Inserimento dati ERP
                    ERP_sel.SelectedIndex = 0
                    'grado_eff.SelectedIndex = 2
                    Tipo_prova.SelectedIndex = 0

                End If


                Label53.Visible = False
                Label54.Visible = False
                Label49.Visible = False
                Label57.Visible = False
                Label56.Visible = False
                Label55.Visible = False
                Label19.Visible = False
                Label20.Visible = False
                Label21.Visible = False
                RPM4.Visible = False
                POW4.Visible = False
                CURR4.Visible = False
                RPM4.Visible = False
                LWA4.Visible = False
                P4.Visible = False
                Q4.Visible = False
                RPM5.Visible = False
                POW5.Visible = False
                CURR5.Visible = False
                LWA5.Visible = False
                P5.Visible = False
                Q5.Visible = False
                RPM6.Visible = False
                POW6.Visible = False
                CURR6.Visible = False
                LWA6.Visible = False
                P6.Visible = False
                Q6.Visible = False







                Tensione_alta.Visible = False
                Label41.Visible = False
                Freq_alta.Visible = False
                I_alta.Visible = False
                RPM_alta.Visible = False
                pow_alta.Visible = False



                Tensione_bassa.SelectedIndex = 4
                Freq_bassa.SelectedIndex = 0


            Else  'DOPPIA VELOCITA'


                Label53.Visible = True
                Label54.Visible = True
                Label49.Visible = True
                Label57.Visible = True
                Label56.Visible = True
                Label55.Visible = True
                Label19.Visible = True
                Label20.Visible = True
                Label21.Visible = True
                RPM4.Visible = True
                POW4.Visible = True
                CURR4.Visible = True
                RPM4.Visible = True
                LWA4.Visible = True
                P4.Visible = True
                Q4.Visible = True
                RPM5.Visible = True
                POW5.Visible = True
                CURR5.Visible = True
                LWA5.Visible = True
                P5.Visible = True
                Q5.Visible = True
                RPM6.Visible = True
                POW6.Visible = True
                CURR6.Visible = True
                LWA6.Visible = True
                P6.Visible = True
                Q6.Visible = True


                Tensione_alta.Visible = True
                Label41.Visible = True
                Freq_alta.Visible = True
                I_alta.Visible = True
                RPM_alta.Visible = True
                pow_alta.Visible = True


                Tensione_bassa.SelectedIndex = 4
                Tensione_alta.SelectedIndex = 17
                Freq_bassa.SelectedIndex = 0
                Freq_alta.SelectedIndex = 0

                'riporto i valori a grfio

                minQ = pto3_bassa(0) * 0.95
                maxQ = pto1_alta(0) * 1.05

                If bassa_find = 1 Then
                    '    For i = 0 To len_bassa
                    '        If portata_Excel_bassa(i) <> 0 Then
                    '            'Chart18.Series(0).Points.AddXY(portata_Excel_bassa(i), Pstat_Excel_bassa(i))

                    '            'Cerco la portata minima per il grafico
                    '            If portata_Excel_bassa(i) <= minQ Then
                    '                minQ = portata_Excel_bassa(i)
                    '            End If

                    '        End If

                    '    Next

                    For i = 0 To 9
                        Chart18.Series(0).Points.AddXY(0, 0)
                    Next

                    'Chart18.Series(0).Points.AddXY(CInt(pto1_bassa(0)), pto1_bassa(2))
                    Chart18.Series(2).Points.AddXY(CInt(pto1_bassa(0)), pto1_bassa(2))
                    P1.Text = CInt(pto1_bassa(2))
                    Q1.Text = CInt(pto1_bassa(0))
                    x_bassa(0) = CInt(pto1_bassa(0))
                    y_bassa(0) = CInt(pto1_bassa(2))
                    Chart18.Series(2).Color = Color.Black
                    Chart18.Series(2).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(2).MarkerSize = 6
                    RPM1.Text = Math.Round(pto1_bassa(3), 0)
                    POW1.Text = Math.Round(pto1_bassa(4), 0)
                    CURR1.Text = Math.Round(pto1_bassa(5), 2)
                    LWA1.Text = Math.Round(pto1_bassa(6), 1)

                    'Chart18.Series(0).Points.AddXY(CInt(pto2_bassa(0)), pto2_bassa(2))
                    Chart18.Series(3).Points.AddXY(CInt(pto2_bassa(0)), pto2_bassa(2))
                    P2.Text = CInt(pto2_bassa(2))
                    Q2.Text = CInt(pto2_bassa(0))
                    x_bassa(1) = CInt(pto2_bassa(0))
                    y_bassa(1) = CInt(pto2_bassa(2))
                    Chart18.Series(3).Color = Color.Black
                    Chart18.Series(3).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(3).MarkerSize = 6
                    RPM2.Text = Math.Round(pto2_bassa(3), 0)
                    POW2.Text = Math.Round(pto2_bassa(4), 0)
                    CURR2.Text = Math.Round(pto2_bassa(5), 2)
                    LWA2.Text = Math.Round(pto2_bassa(6), 1)


                    'Chart18.Series(0).Points.AddXY(CInt(pto3_bassa(0)), pto3_bassa(2))
                    Chart18.Series(4).Points.AddXY(CInt(pto3_bassa(0)), pto3_bassa(2))
                    P3.Text = CInt(pto3_bassa(2))
                    Q3.Text = CInt(pto3_bassa(0))
                    x_bassa(2) = CInt(pto3_bassa(0))
                    y_bassa(2) = CInt(pto3_bassa(2))
                    Chart18.Series(4).Color = Color.Black
                    Chart18.Series(4).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(4).MarkerSize = 6
                    RPM3.Text = Math.Round(pto3_bassa(3), 0)
                    POW3.Text = Math.Round(pto3_bassa(4), 0)
                    CURR3.Text = Math.Round(pto3_bassa(5), 2)
                    LWA3.Text = Math.Round(pto3_bassa(6), 1)


                    '-------------------------------------DISEGNO GRAFICO--------------------------------------------
                    x_bassa_grafico(0) = Q1.Text
                    x_bassa_grafico(1) = Q2.Text
                    x_bassa_grafico(2) = Q3.Text

                    y_bassa_grafico(0) = P1.Text
                    y_bassa_grafico(1) = P2.Text
                    y_bassa_grafico(2) = P3.Text


                    'CALCOLO LA PARABOLA 
                    aggiorna_chart18(x_bassa_grafico, y_bassa_grafico, Q3.Text, Q1.Text, 1)
                    '-------------------------------------------------------------------------------------------------


                    'calcolo i coefficienti della parabola
                    calcolo_coeff_parabola1(x_bassa, y_bassa)
                    coeff_bassa(0) = A_parabola
                    coeff_bassa(1) = B_parabola
                    coeff_bassa(2) = C_parabola



                    'Dati di targa
                    I_bassa.Text = Math.Round(pto_lavoro_bassa(1), 2)
                    RPM_bassa.Text = CInt(pto_lavoro_bassa(4))
                    pow_bassa.Text = Math.Round(pto_lavoro_bassa(2), 0) ' / 1000, 2)

                End If


                If alta_find = 1 Then

                    '    For i = 0 To len_alta
                    '        If portata_Excel_alta(i) <> 0 Then
                    '            Chart18.Series(1).Points.AddXY(portata_Excel_alta(i), Pstat_Excel_alta(i))
                    '        End If
                    '    Next


                    For i = 0 To 9
                        Chart18.Series(1).Points.AddXY(0, 0)
                    Next


                    'Chart18.Series(1).Points.AddXY(CInt(pto1_alta(0)), pto1_alta(2))
                    Chart18.Series(5).Points.AddXY(CInt(pto1_alta(0)), pto1_alta(2))
                    P4.Text = CInt(pto1_alta(2))
                    Q4.Text = CInt(pto1_alta(0))
                    x_alta(0) = CInt(pto1_alta(0))
                    y_alta(0) = CInt(pto1_alta(2))
                    Chart18.Series(5).Color = Color.Black
                    Chart18.Series(5).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(5).MarkerSize = 6
                    RPM4.Text = Math.Round(pto1_alta(3), 0)
                    POW4.Text = Math.Round(pto1_alta(4), 0)
                    CURR4.Text = Math.Round(pto1_alta(5), 2)
                    LWA4.Text = Math.Round(pto1_alta(6), 1)

                    'Chart18.Series(1).Points.AddXY(CInt(pto2_alta(0)), pto2_alta(2))
                    Chart18.Series(6).Points.AddXY(CInt(pto2_alta(0)), pto2_alta(2))
                    P5.Text = CInt(pto2_alta(2))
                    Q5.Text = CInt(pto2_alta(0))
                    x_alta(1) = CInt(pto2_alta(0))
                    y_alta(1) = CInt(pto2_alta(2))
                    Chart18.Series(6).Color = Color.Black
                    Chart18.Series(6).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(6).MarkerSize = 6
                    RPM5.Text = Math.Round(pto2_alta(3), 0)
                    POW5.Text = Math.Round(pto2_alta(4), 0)
                    CURR5.Text = Math.Round(pto2_alta(5), 2)
                    LWA5.Text = Math.Round(pto2_alta(6), 1)


                    'Chart18.Series(1).Points.AddXY(CInt(pto3_alta(0)), pto3_alta(2))
                    Chart18.Series(7).Points.AddXY(CInt(pto3_alta(0)), pto3_alta(2))
                    P6.Text = CInt(pto3_alta(2))
                    Q6.Text = CInt(pto3_alta(0))
                    x_alta(2) = CInt(pto3_alta(0))
                    y_alta(2) = CInt(pto3_alta(2))
                    Chart18.Series(7).Color = Color.Black
                    Chart18.Series(7).MarkerStyle = MarkerStyle.Circle
                    Chart18.Series(7).MarkerSize = 6
                    RPM6.Text = Math.Round(pto3_alta(3), 0)
                    POW6.Text = Math.Round(pto3_alta(4), 0)
                    CURR6.Text = Math.Round(pto3_alta(5), 2)
                    LWA6.Text = Math.Round(pto3_alta(6), 1)

                    '-------------------------------------DISEGNO GRAFICO--------------------------------------------
                    x_alta_grafico(0) = Q4.Text
                    x_alta_grafico(1) = Q5.Text
                    x_alta_grafico(2) = Q6.Text

                    y_alta_grafico(0) = P4.Text
                    y_alta_grafico(1) = P5.Text
                    y_alta_grafico(2) = P6.Text


                    'CALCOLO LA PARABOLA 
                    aggiorna_chart18(x_alta_grafico, y_alta_grafico, Q6.Text, Q4.Text, 2)
                    '-------------------------------------------------------------------------------------------------


                    'calcolo i coefficienti della parabola
                    calcolo_coeff_parabola1(x_alta, y_alta)
                    coeff_alta(0) = A_parabola
                    coeff_alta(1) = B_parabola
                    coeff_alta(2) = C_parabola

                    'Dati di targa
                    I_alta.Text = Math.Round(pto_lavoro_alta(1), 2)
                    RPM_alta.Text = CInt(pto_lavoro_alta(4))
                    pow_alta.Text = Math.Round(pto_lavoro_alta(2), 0) '/ 1000, 2)


                    'Numero prova e Descrizione
                    Descrizione.Text = descrizione_prova
                    Test_numero.Text = Test_numero_bassa & ";" & Test_numero_alta


                    'Inserimento dati ERP
                    ERP_sel.SelectedIndex = 0
                    'grado_eff.SelectedIndex = 2
                    Tipo_prova.SelectedIndex = 0


                End If



                Label19.Visible = True
                Label20.Visible = True
                Label21.Visible = True
                RPM4.Visible = True
                POW4.Visible = True
                CURR4.Visible = True
                RPM4.Visible = True
                LWA4.Visible = True
                RPM5.Visible = True
                POW5.Visible = True
                CURR5.Visible = True
                LWA5.Visible = True
                RPM6.Visible = True
                POW6.Visible = True
                CURR6.Visible = True
                LWA6.Visible = True



                Tensione_alta.Visible = True
                Freq_alta.Visible = True
                I_alta.Visible = True
                RPM_alta.Visible = True
                pow_alta.Visible = True



            End If



            Chart18.Series(0).Color = Color.FromArgb(0, 192, 0)
            Chart18.Series(1).Color = Color.Blue
            Chart18.ChartAreas(0).AxisX.Minimum = Math.Floor(minQ / 1000) * 1000
            Chart18.ChartAreas(0).AxisX.Maximum = Math.Ceiling(maxQ / 1000) * 1000

            If maxQ < 10000 Then
                Chart18.ChartAreas(0).AxisX.Interval = 1000
            ElseIf maxQ > 10000 And maxQ < 30000 Then
                Chart18.ChartAreas(0).AxisX.Interval = 2000
            ElseIf maxQ > 10000 And maxQ < 30000 Then
                Chart18.ChartAreas(0).AxisX.Interval = 3000
            End If

            Chart18.ChartAreas(0).AxisX.LabelStyle.Font = New System.Drawing.Font("Segoe UI", 7.0F)
            Chart18.ChartAreas(0).AxisY.LabelStyle.Font = New System.Drawing.Font("Segoe UI", 7.0F)
            'Chart18.ChartAreas(0).AxisY.LabelStyle.ForeColor = System.Drawing.Color.Red


            'Attivo la visibilita' di tutto il form
            Stampa_DS.Visible = True
            Salva_DB.Visible = True

            'ParGen.Size = New System.Drawing.Size(1625, 252)
            'Guna2Button2.Location = New System.Drawing.Point(111, 2)
            'Guna2GroupBox3.Visible = True


            Label1.Visible = False
        Else


            Label1.Visible = True


        End If




    End Sub



    Private Sub cbx_AtexProtezione_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_AtexProtezione.SelectedIndexChanged
        cbx_AtexProtezioneDS = cbx_AtexProtezione.SelectedItem
    End Sub

    Private Sub cbx_AtexCustodia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_AtexCustodia.SelectedIndexChanged
        cbx_AtexCustodiaDS = cbx_AtexCustodia.SelectedItem
    End Sub

    Private Sub cbx_AtexCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_AtexCategoria.SelectedIndexChanged
        cbx_AtexCategoriaDS = cbx_AtexCategoria.SelectedItem
    End Sub

    Private Sub cbx_AtexClasseTemperatura_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_AtexClasseTemperatura.SelectedIndexChanged
        cbx_AtexClasseTemperaturaDS = cbx_AtexClasseTemperatura.SelectedItem
    End Sub



    Public Sub conta_elementi()




        '------------------------------------------------CICLO DI RICERCA DELLE CHECKBOX SELEZIONATE----------------------------------------------------------
        num_conf_sel = 0
        For Each item As Control In Guna2GroupBox4.Controls
            'Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

            If item.GetType Is GetType(System.Windows.Forms.GroupBox) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                For Each item1 As Control In gb.Controls



                    If item1.GetType Is GetType(System.Windows.Forms.CheckBox) Then 'cerco le checkbox dentro la groupbox identificata

                        Dim chb As System.Windows.Forms.CheckBox = DirectCast(item1, System.Windows.Forms.CheckBox)

                        If chb.Checked = True Then

                            num_conf_sel = num_conf_sel + 1


                        End If


                    End If


                Next

            End If
            '--------------------------------------------------------------------------------------------------------------------------------------------------


        Next


    End Sub




    Private Sub Chart18_MouseMove(sender As Object, e As MouseEventArgs) Handles Chart18.MouseMove

        x_base = 63
        x_base_max = 1100


        Label35.Text = "[" & Math.Round(Chart18.ChartAreas(0).AxisX.Minimum + (e.X - x_base) / (x_base_max - x_base) * Chart18.ChartAreas(0).AxisX.Maximum, 0) & " m³/h]" '* 1.05
        Label36.Text = "[" & Math.Round((e.Y - y_base) / (y_base_max - y_base) * Chart18.ChartAreas(0).AxisY.Maximum, 0) & " Pa]"


    End Sub


    Private Sub Chart18_MouseHover(sender As Object, e As EventArgs) Handles Chart18.MouseHover
        Label35.Visible = True
        Label36.Visible = True
    End Sub


    Private Sub Chart18_MouseLeave(sender As Object, e As EventArgs) Handles Chart18.MouseLeave
        Label35.Visible = False
        Label36.Visible = False
    End Sub


    Private Sub check_taglio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles check_taglio.SelectedIndexChanged

        If check_taglio.SelectedIndex = -1 Then
            check_taglio.BorderColor = Color.FromArgb(250, 117, 158)
        Else
            check_taglio.BorderColor = Color.FromArgb(74, 231, 148)
        End If

    End Sub




    Public Sub update_all_pdf(n)


        'ATTIVA IL LOADING
        Me.Guna2Panel1.Visible = True  '------> ESEGUIRE IN PARALLELO
        Me.Guna2Panel1.BringToFront()  '------> ESEGUIRE IN PARALLELO
        Application.DoEvents()


        eccezione_stampa = 0

        'Copio il vettore di tutte le selezioni che ho effettuato ---> mi serve perche' durante il ciclo di stampa i vettori originali cmabiano
        Dim conf_sel_star(num_max_config) As String
        Dim num_conf_sel_star As Integer


        For i = 0 To num_conf_sel
            conf_sel_star(i) = conf_sel(i)
        Next



        ' Loop over the subdirectories and remove them with their contents
        For Each d In Directory.GetDirectories(folders_directoryDS & "\" & descrizione_fan)
            Directory.Delete(d, True)
        Next

        ' Finish removing also the files in the root folder
        'For Each f In Directory.GetFiles(folders_directoryDS & "\" & descrizione_fan)
        '    File.Delete(f)
        'Next



        'DESELEZIONO TUTTE LE CHECKBOX --- > POI LE RISELEZIONO TUTTE AD UNA AD UNA
        For Each item As Control In Guna2GroupBox4.Controls
            'Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

            If item.GetType Is GetType(System.Windows.Forms.GroupBox) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                For Each item1 As Control In gb.Controls


                    If item1.GetType Is GetType(System.Windows.Forms.CheckBox) Then 'cerco le checkbox dentro la groupbox identificata

                        Dim chb As System.Windows.Forms.CheckBox = DirectCast(item1, System.Windows.Forms.CheckBox)

                        chb.Checked = False

                    End If

                Next

            End If
        Next


        Dim check1 As CheckBox
        Dim gb1 As GroupBox
        'SELEZIONI LE CHECKBOX AD UNA AD UNA E STAMPO IL DATASHEET NELLA DIRECTORY CORRISPONDENTE
        For i = 0 To num_max_config - 1

            If conf_sel_star(i) <> "" Then

                Dim nome_folder As String = conf_sel_star(i).Substring(conf_sel_star(i).IndexOf("_") + 1, conf_sel_star(i).Length - conf_sel_star(i).IndexOf("_") - 1)

                gb1 = Guna2GroupBox4.Controls("Check_" & nome_folder)
                check1 = gb1.Controls("Pic_" & nome_folder)

                check1.Checked = True



                Try
                    'crea la directory della configurazione specifica
                    Dim dir_name As String = folders_directoryDS & "\" & descrizione_fan & "\" & nome_folder
                    System.IO.Directory.CreateDirectory(dir_name) 'creo la directory del progetto
                Catch ex As Exception

                End Try

                stampa_code(n)
                check1.Checked = False

            End If

        Next


        crea_dir_serie()
        stampa_catalogo(n)


        'SELEZIONI LE CHECKBOX AD UNA AD UNA E STAMPO IL DATASHEET NELLA DIRECTORY CORRISPONDENTE
        For i = 0 To num_max_config - 1
            If conf_sel_star(i) <> "" Then
                Dim nome_folder As String = conf_sel_star(i).Substring(conf_sel_star(i).IndexOf("_") + 1, conf_sel_star(i).Length - conf_sel_star(i).IndexOf("_") - 1)

                gb1 = Guna2GroupBox4.Controls("Check_" & nome_folder)
                check1 = gb1.Controls("Pic_" & nome_folder)

                check1.Checked = True

                Try
                    'crea la directory della configurazione specifica
                    Dim dir_name As String = folders_directoryDS & "\" & descrizione_fan & "\" & nome_folder
                    System.IO.Directory.CreateDirectory(dir_name) 'creo la directory del progetto
                Catch ex As Exception

                End Try

                stampa_code(n)
                check1.Checked = False
            End If
        Next



        'RISELEZIONO TUTTE LE CHECKBOX --- > POI LE RISELEZIONO TUTTE AD UNA AD UNA
        For Each item As Control In Guna2GroupBox4.Controls
            'Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

            If item.GetType Is GetType(System.Windows.Forms.GroupBox) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                For Each item1 As Control In gb.Controls


                    If item1.GetType Is GetType(System.Windows.Forms.CheckBox) Then 'cerco le checkbox dentro la groupbox identificata

                        Dim chb As System.Windows.Forms.CheckBox = DirectCast(item1, System.Windows.Forms.CheckBox)



                        For i = 0 To num_conf_sel
                            If chb.Name = conf_sel_star(i) Then
                                chb.Checked = True
                            End If
                        Next

                    End If

                Next

            End If
        Next



        stampa_code(n)


        eccezione_stampa = 1

        If Datasheet_print_mode = 1 Then
            'DESELEZIONO TUTTE LE CHECKBOX --- > POI LE RISELEZIONO TUTTE AD UNA AD UNA
            For Each item As Control In Guna2GroupBox4.Controls
                'Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                If item.GetType Is GetType(System.Windows.Forms.GroupBox) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                    Dim gb As System.Windows.Forms.GroupBox = DirectCast(item, System.Windows.Forms.GroupBox)

                    For Each item1 As Control In gb.Controls


                        If item1.GetType Is GetType(System.Windows.Forms.CheckBox) Then 'cerco le checkbox dentro la groupbox identificata

                            Dim chb As System.Windows.Forms.CheckBox = DirectCast(item1, System.Windows.Forms.CheckBox)

                            chb.Checked = False

                        End If

                    Next

                End If
            Next
        End If

        'ATTIVA IL LOADING
        Me.Guna2Panel1.Visible = False  '------> ESEGUIRE IN PARALLELO
        Me.Guna2Panel1.SendToBack()  '------> ESEGUIRE IN PARALLELO
        Application.DoEvents()
    End Sub







    Public Sub aggiorna_chart18(X_vect, Y_vect, Qmin, Qmax, alta_bassa)

        Try
            calcolo_coeff_parabola1(X_vect, Y_vect)
            Calcolo_pti_parabola1(A_parabola, B_parabola, C_parabola, Qmin, Qmax, alta_bassa) 'bassa = 1; alta = 2
        Catch ex As Exception

        End Try


        'cambio punto
        Try
            For i = 0 To 9
                Try

                    If alta_bassa = 1 Then
                        Chart18.Series(alta_bassa - 1).Points(i).XValue() = vect_Bassa_x_chart(i)
                        Chart18.Series(alta_bassa - 1).Points(i).YValues(0) = vect_Bassa_y_chart(i)
                    ElseIf alta_bassa = 2 Then
                        Chart18.Series(alta_bassa - 1).Points(i).XValue() = vect_Alta_x_chart(i)
                        Chart18.Series(alta_bassa - 1).Points(i).YValues(0) = vect_Alta_y_chart(i)
                    End If

                Catch ex As Exception

                End Try
            Next

        Catch ex As Exception

        End Try


    End Sub



    Private Sub tbx_true_eff_TextChanged(sender As Object, e As EventArgs) Handles tbx_true_eff.TextChanged

        Try
            true_eff = tbx_true_eff.Text
        Catch ex As Exception

        End Try


    End Sub

    Private Sub btnMP_Click(sender As Object, e As EventArgs) Handles btnMP.Click
        Try
            Warning_DBPJ.Close()
        Catch ex As Exception

        End Try
        Warning_DBPJ.Show()
    End Sub



    Public Sub compilazione_new()

        Tmin.Text = "-40"
        IP.Text = 55
        Ins_Class.Text = "F"
        rad_Vip.PerformClick()

        Salva_DB.Visible = True
        Stampa_DS.Visible = True
        Tipo_prova.SelectedIndex = 2


        If S_D_var = 1 Then
            Label53.Visible = False
            Label54.Visible = False
            Label49.Visible = False
            Label57.Visible = False
            Label56.Visible = False
            Label55.Visible = False
            Label19.Visible = False
            Label20.Visible = False
            Label21.Visible = False
            RPM4.Visible = False
            POW4.Visible = False
            CURR4.Visible = False
            RPM4.Visible = False
            LWA4.Visible = False
            P4.Visible = False
            Q4.Visible = False
            RPM5.Visible = False
            POW5.Visible = False
            CURR5.Visible = False
            LWA5.Visible = False
            P5.Visible = False
            Q5.Visible = False
            RPM6.Visible = False
            POW6.Visible = False
            CURR6.Visible = False
            LWA6.Visible = False
            P6.Visible = False
            Q6.Visible = False

            Tensione_alta.Visible = False
            Label41.Visible = False
            Freq_alta.Visible = False
            I_alta.Visible = False
            RPM_alta.Visible = False
            pow_alta.Visible = False

            Tensione_bassa.SelectedIndex = 4
            Freq_bassa.SelectedIndex = 0

        Else

            Label53.Visible = True
            Label54.Visible = True
            Label49.Visible = True
            Label57.Visible = True
            Label56.Visible = True
            Label55.Visible = True
            Label19.Visible = True
            Label20.Visible = True
            Label21.Visible = True
            RPM4.Visible = True
            POW4.Visible = True
            CURR4.Visible = True
            RPM4.Visible = True
            LWA4.Visible = True
            P4.Visible = True
            Q4.Visible = True
            RPM5.Visible = True
            POW5.Visible = True
            CURR5.Visible = True
            LWA5.Visible = True
            P5.Visible = True
            Q5.Visible = True
            RPM6.Visible = True
            POW6.Visible = True
            CURR6.Visible = True
            LWA6.Visible = True
            P6.Visible = True
            Q6.Visible = True


            Tensione_alta.Visible = True
            Label41.Visible = True
            Freq_alta.Visible = True
            I_alta.Visible = True
            RPM_alta.Visible = True
            pow_alta.Visible = True


            Tensione_bassa.SelectedIndex = 4
            Tensione_alta.SelectedIndex = 17
            Freq_bassa.SelectedIndex = 0
            Freq_alta.SelectedIndex = 0



        End If


    End Sub



    Public Sub ricalcolo_coefficienti()

        'RICALCOLO I COEFFICIENTI DAI PUNTI SUL GRAFICO

        'ricalcolo i coefficienti della parabola
        Try
            x_bassa(0) = CInt(Q1_DS)
            x_bassa(1) = CInt(Q2_DS)
            x_bassa(2) = CInt(Q3_DS)

            y_bassa(0) = CInt(P1_DS)
            y_bassa(1) = CInt(P2_DS)
            y_bassa(2) = CInt(P3_DS)


            'calcolo i coefficienti della parabola
            calcolo_coeff_parabola1(x_bassa, y_bassa)

            coeff_bassa(0) = A_parabola
            coeff_bassa(1) = B_parabola
            coeff_bassa(2) = C_parabola
        Catch ex As Exception

        End Try


        'ricalcolo i coefficienti della parabola
        Try
            x_alta(0) = CInt(Q4_DS)
            x_alta(1) = CInt(Q5_DS)
            x_alta(2) = CInt(Q6_DS)

            y_alta(0) = CInt(P4_DS)
            y_alta(1) = CInt(P5_DS)
            y_alta(2) = CInt(P6_DS)


            'calcolo i coefficienti della parabola
            calcolo_coeff_parabola1(x_alta, y_alta)

            coeff_alta(0) = A_parabola
            coeff_alta(1) = B_parabola
            coeff_alta(2) = C_parabola
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Catalog_button_MouseHover(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.Hand
        Me.Refresh()
    End Sub

    Private Sub Catalog_button_MouseLeave(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.Arrow
        Me.Refresh()
    End Sub

    Public Sub Load_cbx_catalogo()
        Dim cbx As ComboBox
        For j = 1 To 5
            cbx = ParGen.Controls("cbx_cat" & j)
            cbx.Items.Clear()

            cbx.Items.Add("")
            For i = 0 To numero_Catalogo - 1
                cbx.Items.Add(DataBase_catalogo(i, 11))
            Next
        Next
    End Sub

    Public Sub crea_dir_serie()
        Dim cbx As ComboBox
        'Creazioni directories dei cataloghi
        For i = 1 To 5
            cbx = ParGen.Controls("cbx_cat" & i)
            If cbx.SelectedIndex <> -1 Then
                System.IO.Directory.CreateDirectory(folders_directoryDS & "\" & descrizione_fan & "\" & cbx.SelectedItem)
            End If
        Next
    End Sub


    'Stampa il catalogo della serie 
    Public Sub stampa_catalogo(n)

        Dim check1 As CheckBox
        Dim gb1 As GroupBox

        ciclo_stampa_catalogo = 1
        For i = 1 To 5
            Dim cbx As ComboBox = ParGen.Controls("cbx_cat" & i)
            Dim nome_serie_cat As String = cbx.SelectedItem
            For j = 0 To numero_Catalogo
                If nome_serie_cat = DataBase_catalogo(j, 11) Then
                    'seleziono e stampo le configurazioni del catalogo
                    For k = 0 To 9
                        If DataBase_catalogo(j, k) <> "" Then
                            gb1 = Guna2GroupBox4.Controls("Check_" & DataBase_catalogo(j, k))
                            check1 = gb1.Controls("Pic_" & DataBase_catalogo(j, k))
                            check1.Checked = True
                        End If
                    Next

                    nome_catalogo = nome_serie_cat
                    If nome_serie_cat <> "" Then
                        stampa_code(n)
                    End If

                    'deseleziono tutte le configurazioni del catalogo
                    For k = 0 To 9
                        If DataBase_catalogo(j, k) <> "" Then
                            gb1 = Guna2GroupBox4.Controls("Check_" & DataBase_catalogo(j, k))
                            check1 = gb1.Controls("Pic_" & DataBase_catalogo(j, k))
                            check1.Checked = False
                        End If
                    Next

                End If
            Next
        Next
        ciclo_stampa_catalogo = 0
    End Sub

    Private Sub ERP_check_CheckedChanged(sender As Object, e As EventArgs) Handles ERP_check.CheckedChanged

        If ERP_check.Checked = True Then
            ERP_check_var = True
        Else
            ERP_check_var = False
        End If

    End Sub

End Class