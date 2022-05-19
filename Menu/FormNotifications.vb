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
Imports System.Data.SqlClient

Public Class FormParametri


    Private Sub FormParametri_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        For i = 0 To numero_note_totali - 1
            note_vect(i) = ""
            ID_note_vect(i) = ""
        Next

        code_pale.Text = ""
        code_profilo.Text = ""
        code_tipo.Text = ""

        If new_PJVar = 0 Then
            check_RevCliente.Visible = True
        End If

        Guna2DataGridView1.Select()


        btn_Datasheet.Visible = False
        btn_VipDesigner.Visible = False
        Guna2Button2.Visible = False
        blocca_controllo_vip = 0

        Form1.Guna2Panel1.Visible = True  '------> ESEGUIRE IN PARALLELO
        Form1.Guna2Panel1.BringToFront()  '------> ESEGUIRE IN PARALLELO

        Application.DoEvents()

        'Nella fase di loading impedisce l'esecuzione di alcune parti del codice (eseguite nelle fasi successive)
        fase_load = 1
        Dim task1 As Task = Task.Run(Sub() main_load())
        Control.CheckForIllegalCrossThreadCalls = False

        Do While (task1.IsCompleted = False)
            Application.DoEvents()
        Loop

        Form1.Guna2Panel1.Visible = False '------> ESEGUIRE IN PARALLELO

        'cerco il valore che stabilisce se la configurazone e'atex o meno
        For i = 0 To Numero_tabelle
            Select Case SchemaDB(i)
                Case "cbx_AtexProtezione"
                    funzione_cbx1(cbx_AtexProtezione, SchemaDB(i))
            End Select
        Next


        If rad_true = 1 Then
            Rad_Atex.Checked = True
        Else
            Rad_Atex.Checked = False
        End If

        jump_rev = 0

        lettura_note1()
        aggiorna_note()
        blocca_controllo_vip = 1
        SbloccaVD1()


        new_project = 0


        If cbx_Revisione.SelectedIndex = 0 And fast_PJ = 0 Then

            Guna2Button2.Text = "Salva richiesta cliente"
            ParGen.Text = "" '"INSERIRE I PARAMETRI RICHIESTI DAL CLIENTE"
            ParGen.CustomBorderColor = Color.Red
            Label23.Visible = True
            Label23.Text = "INSERIRE I PARAMETRI RICHIESTI DAL CLIENTE"
            All_Hide()
            cbx_Stato.SelectedIndex = 0
            Rad_Safe.Checked = True

        Else

            Guna2Button2.Text = "Salva Progetto"
            ParGen.CustomBorderColor = Color.FromArgb(213, 218, 223)
            Label23.Visible = False


            'blocca parametri clienti
            'mod_rev0 = 1
            'Lettura_riga1(0)
            'compila_cbx_ClinteRev0()
            'compila_tbx_ClinteRev0()
            'compila_check_ClinteRev0()
            'mod_rev0 = 0

        End If


        If cbx_TipoRichiesta.SelectedIndex = 0 Then

            gb_conv1.Visible = True
            gb_conv1.Location = New System.Drawing.Point(4, 150)


            gb_generale.Visible = False
            gb_motore.Visible = False
            gb_ventilatore.Visible = False
            gb_ventola.Visible = False
            gb_convogliatore.Visible = False
            gb_supporto.Visible = False


            btn_Datasheet.Visible = False
            btn_VipDesigner.Visible = False
            Guna2Button2.Visible = True
            'All_Show()
            btn_VipDesigner.BringToFront()
            btn_Datasheet.BringToFront()

        Else

            gb_conv1.Visible = False
            gb_conv1.Location = New System.Drawing.Point(1283, 150)

            If fast_PJ = 0 Then
                gb_generale.Visible = True
                gb_motore.Visible = True
                gb_ventilatore.Visible = True
                gb_ventola.Visible = True
                gb_convogliatore.Visible = True
                gb_supporto.Visible = True
            End If

        End If


        'update the image V or X considering if the order has been recieved or not
        If OrdineRic = 0 Then
            Guna2PictureBox2.Image = My.Resources.Xxx
        Else
            Guna2PictureBox2.Image = My.Resources.Vvv
        End If

        tbx_OrdineRicevuto.Text = OrdineRic


        'prezzo
        If prezzoPJ <> "" Then
            LabelPrezzo.Visible = True
            LabelPrezzo.Text = prezzoPJ & " €"
        Else
            LabelPrezzo.Visible = False
        End If



        'Se ho appena creato una progettazione
        If new_PJVar = 1 Then
            tbx_Temperatura.Text = 20
            tbx_Altitude.Text = 0
            tbx_Relative.Text = 0
            cbx_AirflowTarget.SelectedIndex = 1
            tbx_fast.Text = "0"
        End If




        If fast_PJ = 0 Then
            Guna2PictureBox3.Image = My.Resources.slow
            Label87.Text = "PJ completa"

            'gb_generale.Visible = False
            'gb_motore.Visible = False
            'gb_ventilatore.Visible = False
            'gb_ventola.Visible = False
            'gb_supporto.Visible = False
            'gb_convogliatore.Visible = False
            'Guna2DataGridView1.Visible = False

        Else
            Guna2PictureBox3.Image = My.Resources.fast
            Label87.Text = "PJ rapida"

            gb_generale.Visible = False
            gb_motore.Visible = False
            gb_ventilatore.Visible = False
            gb_ventola.Visible = False
            gb_supporto.Visible = False
            gb_convogliatore.Visible = False
            gb_conv1.Visible = False
            Guna2DataGridView1.Visible = true
        End If




        If lockPJ = 0 Or lockPJ = userNum Then
            ConnStateUser1(1)
        End If

        Select Case lockPJ
            Case 2
                userNameLock = "Andrea"
            Case 3
                userNameLock = "Stefano"
            Case 4
                userNameLock = "Paolo"
            Case 5
                userNameLock = "Roberto"
            Case 6
                userNameLock = "Fausto"
            Case 7
                userNameLock = "Lorenzo"
            Case 8
                userNameLock = "Alberto"
            Case 9
                userNameLock = "Alessandro"
            Case 10
                userNameLock = "Riccardo"
        End Select



        If lockPJ = 0 Or lockPJ = userNum Then
            Me.Enabled = True
            ParGen.ForeColor = Color.Black
        Else
            Me.Enabled = False
            ParGen.Text = "Attualmente aperta da " & userNameLock
            ParGen.ForeColor = Color.Red
        End If

        'Connessione al database sql Business per autocompilare la textbox tbx_clienti
        Select_1Filtro1("an_descr1", "dbo.anagra", "an_tipo", "C")

        tbx_Cliente.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbx_Cliente.AutoCompleteCustomSource = col
        tbx_Cliente.AutoCompleteMode = AutoCompleteMode.Suggest


        'Eccezione caso senza pannello --> pannello disabilitato
        cbx_ConfigurazioneFan_SelectedIndexChanged(sender, e)





        '--------------------MODALITA' DATASHEET = PROGETTAZIONE----------------------------
        mod_archivio = 0
        'mi serve per salvare nel databse l'eventuale nome della progetta<<zione associata
        PJ_ref_star = prog_rev(posizione_progetto, 0)
        '-----------------------------------------------------------------------------------





        '------------------RENDO INVISIBILE LA GRIGLIA DELLE NOTE EL CASO DI CREAZIONE--------------------------------
        If new_PJVar = 1 Then
            Guna2DataGridView1.Visible = False
            ParGen.Text = prog_rev(posizione_progetto, 0) & " del " & data_progetto
        End If
        '-------------------------------------------------------------------------------------------------------------

        Form1.lblTitle.Text = PJ_ref_star


        If nome_macchina = "Lorenzo" Or nome_macchina = "Andrea" Or nome_macchina = "Paolo" Then

            tbx_codice.ReadOnly = False

        Else

            tbx_codice.ReadOnly = True

        End If

        fase_load = 0


    End Sub



    Public Sub main_load()



        Lettura_riga1(prog_rev(posizione_progetto, 1))

        'INIZIO FUNZIONE
        ParGen.Visible = False
        gb_generale.Visible = False
        gb_motore.Visible = False
        gb_ventilatore.Visible = False
        gb_ventola.Visible = False
        gb_supporto.Visible = False
        gb_convogliatore.Visible = False
        Guna2DataGridView1.Visible = False
        Guna2Button2.Visible = False




        If new_project = 0 Then ' se sto creando una nuova progettazione non compilo le varie textbo ecc, il progetto e' vuoto
            compila_tbx()

            'Gestione caricamento progettazione rapida lentaf
            Try
                fast_PJ = tbx_fast.Text
            Catch ex As Exception
                fast_PJ = 0
            End Try

            compila_check()
            compila_radio()
        End If

        compila_cbx()

        'numero revisioni
        Dim rev As Integer = prog_rev(posizione_progetto, 1)
        cbx_Revisione.Items.Clear()

        Try

            Dim Rev_cl1(20) As Integer
            Dim Data_cl1(20) As String
            Dim RevN_cl1(20) As String
            Dim count_rev As Integer = 0
            Dim count_rev1 As Integer

            'ricostruisco un vettore contente la "tipologia" di revisione, se = 1 allora significa che il tipo di revisione e' REVISIONE CLIENTE altrimenti e' una revisone normale
            For i = 0 To num_righeDB
                If prog_rev1(i, 0) = prog_rev(posizione_progetto, 0) Then

                    If prog_rev1(i, 1) = "" Then
                        Rev_cl1(count_rev) = 0
                    Else
                        Rev_cl1(count_rev) = prog_rev1(i, 1)
                    End If


                    Data_cl1(count_rev) = prog_rev1(i, 2)
                    RevN_cl1(count_rev) = prog_rev1(i, 3)
                    count_rev = count_rev + 1
                End If
            Next


            Dim Rev_cl(20) As Integer
            Dim Data_cl(20) As String
            Dim RevN_cl(20) As String
            Dim uc As Integer = 0



            'riordino il vettore
            For i = 0 To rev


                For j = 0 To rev

                    If RevN_cl1(j) = uc Then
                        Rev_cl(i) = Rev_cl1(j)
                        Data_cl(i) = Data_cl1(j)
                        RevN_cl(i) = j
                        uc = uc + 1
                        j = rev + 1
                    End If


                Next

            Next

            'compila_cbx() 'inserito perche' senza l'atex non viene compilato alla prima apertura

            For i = 0 To rev

                If i = 0 Then
                    cbx_Revisione.Items.Add("Richiesta cl. 0    |     " & Data_cl(i))
                ElseIf Rev_cl(i) = 1 Then
                    count_rev1 = count_rev1 + 1
                    cbx_Revisione.Items.Add("Richiesta cl. " & count_rev1 & "    |     " & Data_cl(i))
                ElseIf Rev_cl(i) = 0 Then
                    cbx_Revisione.Items.Add("Revisione    " & i - count_rev1 & "    |     " & Data_cl(i))
                End If

            Next

        Catch ex As Exception
            'Alla creazione di una nuova PJ impongo il primo elemento pari a "Richiesta cliente"
            cbx_Revisione.Items.Add("Richiesta")
        End Try



        cbx_Revisione.SelectedIndex = rev
        ParGen.Text = prog_rev(posizione_progetto, 0) & " del " & data_progetto




        If cbx_Owner.SelectedIndex = -1 Then

            cbx_Owner.SelectedItem = nome_macchina

        End If


        ParGen.Visible = True

        If fast_PJ = 0 Then
            gb_generale.Visible = True
            gb_motore.Visible = True
            gb_ventilatore.Visible = True
            gb_ventola.Visible = True
            gb_supporto.Visible = True
            gb_convogliatore.Visible = True
            Guna2DataGridView1.Visible = True
        End If

        'Guna2Button2.Visible = True

    End Sub





    'PER MODIFICARE RIGHE GIA' ESISTENTE
    Public Sub Update_database()


        Dim provider As String
        Dim dataFile As String
        Dim connString As String
        Dim myConnection As OleDbConnection = New OleDbConnection


        provider = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
        dataFile = "H:\Comune\Applicazioni\VipProject\NewPJ.accdb"
        connString = provider & dataFile
        myConnection.ConnectionString = connString
        myConnection.Open()
        Dim str As String
        'str = "update Progetto set [Revisione] ='" & Guna2TextBox1.Text & "', [Owner] ='" & 1 & "'  where [ID] = " & 10 & ""
        Dim cmd As OleDbCommand = New OleDbCommand(str, myConnection)
        MsgBox("Update Success")
        Try
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            myConnection.Close()
            'TextBox1.Clear()
            'TextBox2.Clear()
            'TextBox3.Clear()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



    End Sub





    Public Sub compila_cbx()


        rad_true = 0
        Dim eccezione_ConfigurazioneFan As String



        For i = 0 To Numero_tabelle

            Select Case SchemaDB(i)


                '------------------------Tabella Generale------------------------------
                Case "cbx_TipoFan"
                    funzione_cbx1(cbx_TipoFan, SchemaDB(i))
                Case "cbx_Direzione"
                    funzione_cbx1(cbx_Direzione, SchemaDB(i))
                Case "cbx_ConfigurazioneFan"
                    funzione_cbx1(cbx_ConfigurazioneFan, SchemaDB(i))
                Case "cbx_ApplicazioneFan"
                    funzione_cbx1(cbx_ApplicazioneFan, SchemaDB(i))
                Case "cbx_rumore"
                    'funzione_cbx1(cbx_rumore, SchemaDB(i))
                Case "cbx_AtexProtezione"
                    funzione_cbx1(cbx_AtexProtezione, SchemaDB(i))
                Case "cbx_AtexCustodia"
                    funzione_cbx1(cbx_AtexCustodia, SchemaDB(i))
                Case "cbx_AtexCategoria"
                    funzione_cbx1(cbx_AtexCategoria, SchemaDB(i))
                Case "cbx_AtexClasseTemperatura"
                    funzione_cbx1(cbx_AtexClasseTemperatura, SchemaDB(i))
                Case "cbx_AirflowTarget"
                    funzione_cbx1(cbx_AirflowTarget, SchemaDB(i))
                    '-------------------------------------------------------------------

                    '------------------------Motore Elettrico------------------------------
                Case "cbx_TipoMotore"
                    funzione_cbx1(cbx_TipoMotore, SchemaDB(i))
                Case "cbx_Alimentazione"
                    funzione_cbx1(cbx_Alimentazione, SchemaDB(i))
                Case "cbx_Volt"

                    'ATTENZIONE cbx_Volt DEVE essere aggiornata dopo cbx_Alimentazione e cbx_Hz
                    funzione_tensione()
                    funzione_cbx1(cbx_Volt, SchemaDB(i))

                Case "cbx_Hz"
                    funzione_cbx1(cbx_Hz, SchemaDB(i))
                Case "cbx_Poli"
                    funzione_cbx1(cbx_Poli, SchemaDB(i))
                Case "cbx_materiale"
                    funzione_cbx1(cbx_materiale, SchemaDB(i))
                Case "cbx_cooling"
                    funzione_cbx1(cbx_cooling, SchemaDB(i))
                Case "cbx_IEX"
                    funzione_cbx1(cbx_IEX, SchemaDB(i))
                Case "cbx_ClasseIsolamento"
                    funzione_cbx1(cbx_ClasseIsolamento, SchemaDB(i))
                Case "cbx_IP"
                    funzione_cbx1(cbx_IP, SchemaDB(i))
                Case "cbx_costruzione"
                    funzione_cbx1(cbx_costruzione, SchemaDB(i))
                Case "cbx_superficiale"
                    funzione_cbx1(cbx_superficiale, SchemaDB(i))
                Case "cbx_colore"
                    funzione_cbx1(cbx_colore, SchemaDB(i))
                Case "cbx_corrosione"
                    funzione_cbx1(cbx_corrosione, SchemaDB(i))
                Case "cbx_MaterialeScudi"
                    funzione_cbx1(cbx_MaterialeScudi, SchemaDB(i))
                    '-------------------------------------------------------------------
                    '------------------------Ventilatore------------------------------
                Case "cbx_VitiFan"
                    funzione_cbx1(cbx_VitiFan, SchemaDB(i))
                Case "cbx_DispMotore"

                    cbx_DispMotore.Items.Clear()

                    If cbx_Direzione.SelectedIndex = 0 Then
                        cbx_DispMotore.Items.Add("Non definito")
                        cbx_DispMotore.Items.Add("A   Premente asse orizzontale")
                        cbx_DispMotore.Items.Add("AD Premente con albero verso il basso")
                        cbx_DispMotore.Items.Add("AU Premente con albero in alto")
                    ElseIf cbx_Direzione.SelectedIndex = 1 Then
                        cbx_DispMotore.Items.Add("Non definito")
                        cbx_DispMotore.Items.Add("B   Aspirante asse orizzontale")
                        cbx_DispMotore.Items.Add("BD Aspirante con albero in alto")
                        cbx_DispMotore.Items.Add("BU Aspirante con Albero in Basso")
                    ElseIf cbx_Direzione.SelectedIndex = 3 Then
                        cbx_DispMotore.Items.Add("Non definito")
                        cbx_DispMotore.Items.Add("H   Radiale asse orizzontale")
                        cbx_DispMotore.Items.Add("D Radiale con albero verso il basso")
                        cbx_DispMotore.Items.Add("U Radiale con albero in alto")
                    End If

                    funzione_cbx1(cbx_DispMotore, SchemaDB(i))


                    '------------------------Ventola------------------------------
                Case "cbx_MaterialeVentola"
                    funzione_cbx1(cbx_MaterialeVentola, SchemaDB(i))
                Case "cbx_TrattamentoSupVentola"
                    funzione_cbx1(cbx_TrattamentoSupVentola, SchemaDB(i))
                Case "cbx_ColoreVentola"
                    funzione_cbx1(cbx_ColoreVentola, SchemaDB(i))
                Case "cbx_ClasseCorrVentola"
                    funzione_cbx1(cbx_ClasseCorrVentola, SchemaDB(i))
                Case "cbx_MaterialeVitiVentola"
                    funzione_cbx1(cbx_MaterialeVitiVentola, SchemaDB(i))
                Case "cbx_MaterialeMozzo"
                    funzione_cbx1(cbx_MaterialeMozzo, SchemaDB(i))
                Case "cbx_TrattamentoMozzo"
                    funzione_cbx1(cbx_TrattamentoMozzo, SchemaDB(i))
                Case "cbx_ColoreMozzo"
                    funzione_cbx1(cbx_ColoreMozzo, SchemaDB(i))
                Case "cbx_ClasseCorrMozzo"
                    funzione_cbx1(cbx_ClasseCorrMozzo, SchemaDB(i))
                Case "cbx_MaterialeRaggera"
                    funzione_cbx1(cbx_MaterialeRaggera, SchemaDB(i))
                Case "cbx_TrattamentoRaggera"
                    funzione_cbx1(cbx_TrattamentoRaggera, SchemaDB(i))
                Case "cbx_ColoreRaggera"
                    funzione_cbx1(cbx_ColoreRaggera, SchemaDB(i))
                Case "cbx_ClasseCorrRaggera"
                    funzione_cbx1(cbx_ClasseCorrRaggera, SchemaDB(i))
                Case "cbx_tipo_ventola"
                    funzione_cbx1(cbx_tipo_ventola, SchemaDB(i))
                Case "cbx_profilo_ventola"
                    funzione_cbx1(cbx_profilo_ventola, SchemaDB(i))
                Case "cbx_gradi_ventola"
                    funzione_cbx1(cbx_gradi_ventola, SchemaDB(i))
                Case "cbx_diametro_ventola"
                    funzione_cbx1(cbx_diametro_ventola, SchemaDB(i))
                Case "cbx_pale_ventola"
                    funzione_cbx1(cbx_pale_ventola, SchemaDB(i))
                    '-------------------------------------------------------------------

                    '------------------------Convogliatore------------------------------
                Case "cbx_TipoConvogliatore"
                    funzione_cbx1(cbx_TipoConvogliatore, SchemaDB(i))
                Case "cbx_MaterialeConvogliatore"
                    funzione_cbx1(cbx_MaterialeConvogliatore, SchemaDB(i))
                Case "cbx_TrattamentoConvogliatore"
                    funzione_cbx1(cbx_TrattamentoConvogliatore, SchemaDB(i))
                Case "cbx_ColoreConvogliatore"
                    funzione_cbx1(cbx_ColoreConvogliatore, SchemaDB(i))
                Case "cbx_ClasseConvogliatore"
                    funzione_cbx1(cbx_ClasseConvogliatore, SchemaDB(i))

                    '-------------------------------------------------------------------

                 '------------------------Supporto------------------------------
                Case "cbx_TipoSupporto"
                    funzione_cbx1(cbx_TipoSupporto, SchemaDB(i))
                Case "cbx_MaterialeSupporto"
                    funzione_cbx1(cbx_MaterialeSupporto, SchemaDB(i))
                Case "cbx_TrattamentoSupporto"
                    funzione_cbx1(cbx_TrattamentoSupporto, SchemaDB(i))
                Case "cbx_ColoreSupporto"
                    funzione_cbx1(cbx_ColoreSupporto, SchemaDB(i))
                Case "cbx_ClasseSupporto"
                    funzione_cbx1(cbx_ClasseSupporto, SchemaDB(i))

                    '-------------------------------------------------------------------

                      '------------------------Parametri Generali------------------------------

                Case "cbx_Owner"
                    funzione_cbx1(cbx_Owner, SchemaDB(i))
                Case "cbx_Stato"
                    funzione_cbx1(cbx_Stato, SchemaDB(i))
                Case "cbx_TipoRichiesta"
                    jump_tipo_richiesta = 1
                    funzione_cbx1(cbx_TipoRichiesta, SchemaDB(i))
                    jump_tipo_richiesta = 0

                    '-------------------------------------------------------------------

           '------------------------Parametri Convolgiatori singoli------------------------------

                Case "cbx_Tipo_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_Tipo_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0
                Case "cbx_Diam_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_Diam_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0
                Case "cbx_Mat_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_Mat_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0
                Case "cbx_TratSup_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_TratSup_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0
                Case "cbx_Colore_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_Colore_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0
                Case "cbx_ClassCorr_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_ClassCorr_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0
                Case "cbx_TratSup_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_TratSup_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0

                    '-------------------------------------------------------------------

            End Select


        Next


        'jump_tipo_richiesta = 1
        'If cbx_TipoRichiesta.SelectedIndex = 0 Then

        '    gb_conv1.Visible = True
        '    gb_conv1.Location = New System.Drawing.Point(4, 150)
        '    gb_generale.Visible = False
        '    gb_motore.Visible = False
        '    gb_ventilatore.Visible = False
        '    gb_ventola.Visible = False
        '    gb_convogliatore.Visible = False
        '    gb_supporto.Visible = False


        '    btn_VipDesigner.Visible = True
        '    Guna2Button2.Visible = True
        '    'All_Show()
        '    btn_VipDesigner.BringToFront()

        'Else

        '    gb_conv1.Visible = False
        '    gb_conv1.Location = New System.Drawing.Point(1283, 150)
        '    gb_generale.Visible = True
        '    gb_motore.Visible = True
        '    gb_ventilatore.Visible = True
        '    gb_ventola.Visible = True
        '    gb_convogliatore.Visible = True
        '    gb_supporto.Visible = True

        'End If
        'jump_tipo_richiesta = 0


        '    If cb.Name = SchemaDB(i) Then



        '        cb.Items.Clear()
        '        Dim numero_righe_tab As Integer = getcount(SchemaDB(i))

        '        Lettura_cella_singolaDB1(SchemaDB(i), "Descrizione")

        '        For j = 0 To L_vettore
        '            cb.Items.Add(vettore_elemento_cbx(j))
        '        Next


        '        'inserisco il valore del database selezionato da tabella alla revisione selezionata
        '        For j = 0 To Numero_colonneDB - 1




        '            If Nome_colonne(j + 1) = SchemaDB(i) Then

        '                If Nome_colonne(j + 1) = "cbx_AtexProtezione" Then

        '                    Try
        '                        Dim controllo As String = Valore_CellaRiga(j) - 1
        '                        If controllo <> "-1" Then
        '                            rad_true = 1
        '                        End If
        '                    Catch ex As Exception

        '                    End Try


        '                End If

        '                Try

        '                    cb.SelectedIndex = Valore_CellaRiga(j) - 1

        '                Catch ex As Exception

        '                End Try
        '            End If

        '        Next

        '    End If
        'Next

        'End If
        'Next





        'For Each item As Control In gb_generale.Controls
        '    If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
        '        Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

        '        For i = 0 To Numero_tabelle
        '            If cb.Name = SchemaDB(i) Then



        '                cb.Items.Clear()
        '                Dim numero_righe_tab As Integer = getcount(SchemaDB(i))

        '                Lettura_cella_singolaDB1(SchemaDB(i), "Descrizione")

        '                For j = 0 To L_vettore
        '                    cb.Items.Add(vettore_elemento_cbx(j))
        '                Next


        '                'inserisco il valore del database selezionato da tabella alla revisione selezionata
        '                For j = 0 To Numero_colonneDB - 1




        '                    If Nome_colonne(j + 1) = SchemaDB(i) Then

        '                        If Nome_colonne(j + 1) = "cbx_AtexProtezione" Then

        '                            Try
        '                                Dim controllo As String = Valore_CellaRiga(j) - 1
        '                                If controllo <> "-1" Then
        '                                    rad_true = 1
        '                                End If
        '                            Catch ex As Exception

        '                            End Try


        '                        End If

        '                        Try

        '                            cb.SelectedIndex = Valore_CellaRiga(j) - 1

        '                        Catch ex As Exception

        '                        End Try
        '                    End If

        '                Next

        '            End If
        '        Next

        '    End If
        'Next

        'For Each item As Control In gb_motore.Controls
        '    If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
        '        Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

        '        For i = 0 To Numero_tabelle
        '            If cb.Name = SchemaDB(i) Then

        '                cb.Items.Clear()
        '                Dim numero_righe_tab As Integer = getcount(SchemaDB(i))

        '                Lettura_cella_singolaDB1(SchemaDB(i), "Descrizione")

        '                For j = 0 To L_vettore
        '                    cb.Items.Add(vettore_elemento_cbx(j))
        '                Next


        '                'inserisco il valore del database selezionato da tabella alla revisione selezionata
        '                For j = 0 To Numero_colonneDB - 1

        '                    If Nome_colonne(j + 1) = SchemaDB(i) Then
        '                        Try
        '                            cb.SelectedIndex = Valore_CellaRiga(j) - 1
        '                        Catch ex As Exception

        '                        End Try
        '                    End If

        '                Next



        '            End If

        '        Next

        '    End If
        'Next






        'For Each item As Control In ParGen.Controls
        '    If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
        '        Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

        '        For i = 0 To Numero_tabelle
        '            If cb.Name = SchemaDB(i) Then ' And cb.Name <> "cbx_Revisione" Then

        '                cb.Items.Clear()
        '                Dim numero_righe_tab As Integer = getcount(SchemaDB(i))

        '                Lettura_cella_singolaDB1(SchemaDB(i), "Descrizione")

        '                For j = 0 To L_vettore
        '                    cb.Items.Add(vettore_elemento_cbx(j))
        '                Next


        '                'inserisco il valore del database selezionato da tabella alla revisione selezionata
        '                For j = 0 To Numero_colonneDB - 1

        '                    If Nome_colonne(j + 1) = SchemaDB(i) Then
        '                        Try
        '                            cb.SelectedIndex = Valore_CellaRiga(j) - 1
        '                        Catch ex As Exception

        '                        End Try
        '                    End If


        '                Next

        '            End If



        '        Next

        '    End If
        'Next




        'For Each item As Control In gb_ventilatore.Controls
        '    If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
        '        Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

        '        For i = 0 To Numero_tabelle
        '            If cb.Name = SchemaDB(i) Then

        '                cb.Items.Clear()
        '                Dim numero_righe_tab As Integer = getcount(SchemaDB(i))

        '                Lettura_cella_singolaDB1(SchemaDB(i), "Descrizione")

        '                For j = 0 To L_vettore
        '                    cb.Items.Add(vettore_elemento_cbx(j))
        '                Next



        '                'inserisco il valore del database selezionato da tabella alla revisione selezionata
        '                For j = 0 To Numero_colonneDB - 1

        '                    If Nome_colonne(j + 1) = SchemaDB(i) Then
        '                        Try
        '                            cb.SelectedIndex = Valore_CellaRiga(j) - 1
        '                        Catch ex As Exception

        '                        End Try
        '                    End If

        '                Next






        '            End If

        '        Next

        '    End If
        'Next

        'For Each item As Control In gb_ventola.Controls
        '    If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
        '        Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

        '        For i = 0 To Numero_tabelle
        '            If cb.Name = SchemaDB(i) Then

        '                cb.Items.Clear()
        '                Dim numero_righe_tab As Integer = getcount(SchemaDB(i))

        '                Lettura_cella_singolaDB1(SchemaDB(i), "Descrizione")

        '                For j = 0 To L_vettore
        '                    cb.Items.Add(vettore_elemento_cbx(j))
        '                Next


        '                'inserisco il valore del database selezionato da tabella alla revisione selezionata
        '                For j = 0 To Numero_colonneDB - 1

        '                    If Nome_colonne(j + 1) = SchemaDB(i) Then
        '                        Try
        '                            cb.SelectedIndex = Valore_CellaRiga(j) - 1
        '                        Catch ex As Exception

        '                        End Try
        '                    End If

        '                Next



        '            End If

        '        Next

        '    End If
        'Next


        'For Each item As Control In gb_supporto.Controls
        '    If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
        '        Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

        '        For i = 0 To Numero_tabelle
        '            If cb.Name = SchemaDB(i) Then

        '                cb.Items.Clear()
        '                Dim numero_righe_tab As Integer = getcount(SchemaDB(i))

        '                Lettura_cella_singolaDB1(SchemaDB(i), "Descrizione")

        '                For j = 0 To L_vettore
        '                    cb.Items.Add(vettore_elemento_cbx(j))
        '                Next


        '                'inserisco il valore del database selezionato da tabella alla revisione selezionata
        '                For j = 0 To Numero_colonneDB - 1

        '                    If Nome_colonne(j + 1) = SchemaDB(i) Then
        '                        Try
        '                            cb.SelectedIndex = Valore_CellaRiga(j) - 1
        '                        Catch ex As Exception

        '                        End Try
        '                    End If

        '                Next



        '            End If

        '        Next

        '    End If
        'Next


        'For Each item As Control In gb_convogliatore.Controls
        '    If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
        '        Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

        '        For i = 0 To Numero_tabelle
        '            If cb.Name = SchemaDB(i) Then

        '                cb.Items.Clear()
        '                Dim numero_righe_tab As Integer = getcount(SchemaDB(i))

        '                Lettura_cella_singolaDB1(SchemaDB(i), "Descrizione")

        '                For j = 0 To L_vettore
        '                    cb.Items.Add(vettore_elemento_cbx(j))
        '                Next


        '                'inserisco il valore del database selezionato da tabella alla revisione selezionata
        '                For j = 0 To Numero_colonneDB - 1

        '                    If Nome_colonne(j + 1) = SchemaDB(i) Then
        '                        Try
        '                            cb.SelectedIndex = Valore_CellaRiga(j) - 1
        '                        Catch ex As Exception

        '                        End Try
        '                    End If

        '                Next



        '            End If

        '        Next

        '    End If
        'Next





    End Sub





    Public Sub compila_tbx()


        For Each item As Control In gb_generale.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                For i = 0 To Numero_colonneDB - 1

                    If tb.Name = Nome_colonne(i + 1) Then

                        tb.Text = Valore_CellaRiga(i)
                        'tb.Enabled = True
                    End If

                Next

            End If
        Next


        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                For i = 0 To Numero_colonneDB - 1

                    If tb.Name = Nome_colonne(i + 1) Then

                        tb.Text = Valore_CellaRiga(i)
                        'tb.Enabled = True

                    End If

                Next

            End If
        Next



        For Each item As Control In gb_ventola.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                For i = 0 To Numero_colonneDB - 1

                    If tb.Name = Nome_colonne(i + 1) Then

                        tb.Text = Valore_CellaRiga(i)
                        'tb.Enabled = True

                    End If

                Next

            End If
        Next




        For Each item As Control In ParGen.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                For i = 0 To Numero_colonneDB - 1

                    If tb.Name = Nome_colonne(i + 1) Then

                        tb.Text = Valore_CellaRiga(i)
                        'tb.Enabled = True

                    End If

                Next

            End If
        Next


        For Each item As Control In gb_conv1.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                For i = 0 To Numero_colonneDB - 1

                    If tb.Name = Nome_colonne(i + 1) Then

                        tb.Text = Valore_CellaRiga(i)
                        'tb.Enabled = True
                    End If

                Next

            End If
        Next


    End Sub








    Public Sub compila_check()


        For Each item As Control In gb_generale.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                Dim chb As Guna.UI2.WinForms.Guna2CheckBox = DirectCast(item, Guna.UI2.WinForms.Guna2CheckBox)

                For i = 0 To Numero_colonneDB - 1

                    If chb.Name = Nome_colonne(i + 1) Then

                        Try
                            chb.Checked = Valore_CellaRiga(i)
                            'chb.Enabled = True
                        Catch ex As Exception
                            'chb.Checked = False
                        End Try

                    End If

                Next

            End If
        Next



        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                Dim chb As Guna.UI2.WinForms.Guna2CheckBox = DirectCast(item, Guna.UI2.WinForms.Guna2CheckBox)

                For i = 0 To Numero_colonneDB - 1

                    If chb.Name = Nome_colonne(i + 1) Then

                        Try
                            chb.Checked = Valore_CellaRiga(i)
                            'chb.Enabled = True
                        Catch ex As Exception
                            'chb.Checked = False
                        End Try

                    End If

                Next

            End If
        Next



        For Each item As Control In ParGen.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                Dim chb As Guna.UI2.WinForms.Guna2CheckBox = DirectCast(item, Guna.UI2.WinForms.Guna2CheckBox)

                For i = 0 To Numero_colonneDB - 1

                    If chb.Name = Nome_colonne(i + 1) Then

                        Try
                            chb.Checked = Valore_CellaRiga(i)
                            'chb.Enabled = True
                        Catch ex As Exception
                            'chb.Checked = False
                        End Try

                    End If

                Next

            End If
        Next

        If cbx_Revisione.SelectedIndex = 0 Then
            check_RevCliente.Visible = False
        Else
            check_RevCliente.Visible = True
        End If


    End Sub






    Public Sub compila_radio()


        For Each item As Control In gb_generale.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2RadioButton) Then
                Dim rad As Guna.UI2.WinForms.Guna2RadioButton = DirectCast(item, Guna.UI2.WinForms.Guna2RadioButton)

                For i = 0 To Numero_colonneDB - 1

                    If rad.Name = Nome_colonne(i + 1) Then


                        Try
                            If Valore_CellaRiga(i) = True Then
                                rad.Checked = Valore_CellaRiga(i)
                            Else
                                Rad_Safe.Checked = True
                            End If

                        Catch ex As Exception

                        End Try


                    End If

                Next

            End If
        Next

    End Sub




    Private Sub cbx_Revisione_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_Revisione.SelectedIndexChanged


        num_rev_generale = cbx_Revisione.SelectedIndex


        If cbx_Revisione.SelectedIndex = 0 Then
            cbx_Owner.Enabled = False
        Else
            cbx_Owner.Enabled = True
        End If


        If blocco_load_imm = 1 Then

            Form1.Guna2Panel1.Visible = True  '------> ESEGUIRE IN PARALLELO
            Form1.Guna2Panel1.BringToFront()  '------> ESEGUIRE IN PARALLELO
            Application.DoEvents()


            Dim task1 As Task = Task.Run(Sub() update_rev())
            Control.CheckForIllegalCrossThreadCalls = False

            Do While (task1.IsCompleted = False)
                Application.DoEvents()
            Loop


            Form1.Guna2Panel1.Visible = False '------> ESEGUIRE IN PARALLELO

        Else

            update_rev()

        End If


        If cbx_Revisione.SelectedIndex <> 0 Then 'aggiungo il layer delle scelte cliente

            'blocca parametri clienti
            'mod_rev0 = 1
            'Lettura_riga1(0)
            'compila_cbx_ClinteRev0()
            'compila_tbx_ClinteRev0()
            'compila_check_ClinteRev0()
            'mod_rev0 = 0

        End If



        'jump_tipo_richiesta = 1
        'If cbx_TipoRichiesta.SelectedIndex = 0 Then


        '    'gb_conv1.Visible = True
        '    'gb_conv1.Location = New System.Drawing.Point(4, 150)
        '    'gb_generale.Visible = False
        '    'gb_motore.Visible = False
        '    'gb_ventilatore.Visible = False
        '    'gb_ventola.Visible = False
        '    'gb_convogliatore.Visible = False
        '    'gb_supporto.Visible = False


        '    'btn_VipDesigner.Visible = True
        '    'Guna2Button2.Visible = True
        '    'All_Show()
        '    'btn_VipDesigner.BringToFront()

        'Else

        '    'gb_conv1.Visible = False
        '    'gb_conv1.Location = New System.Drawing.Point(1283, 150)
        '    'gb_generale.Visible = True
        '    'gb_motore.Visible = True
        '    'gb_ventilatore.Visible = True
        '    'gb_ventola.Visible = True
        '    'gb_convogliatore.Visible = True
        '    'gb_supporto.Visible = True


        'End If
        'jump_tipo_richiesta = 0






    End Sub




    Public Sub update_rev()

        If jump_rev = 0 Then


            If skip_rev_cbx_change = 0 Then

                Lettura_riga1(cbx_Revisione.SelectedIndex)

                'gb_conv1.Visible = False
                'If cbx_TipoRichiesta.SelectedIndex <> 0 Then
                '    ParGen.Visible = False
                '    gb_generale.Visible = False
                '    gb_motore.Visible = False
                '    gb_ventilatore.Visible = False
                '    gb_ventola.Visible = False
                '    gb_supporto.Visible = False
                '    gb_convogliatore.Visible = False

                '    Guna2DataGridView1.Visible = False
                '    Guna2Button2.Visible = False
                'End If

                compila_cbx()
                compila_tbx()
                compila_check()
                compila_radio()

                'cerco il valore che stabilisce se la configurazone e'atex o meno
                For i = 0 To Numero_tabelle
                    Select Case SchemaDB(i)
                        Case "cbx_AtexProtezione"
                            funzione_cbx1(cbx_AtexProtezione, SchemaDB(i))
                    End Select
                Next


                If rad_true = 1 Then
                    Rad_Atex.Checked = True
                Else
                    Rad_Atex.Checked = False
                End If

                'If cbx_TipoRichiesta.SelectedIndex <> 0 Then
                '    'If cbx_TipoRichiesta.SelectedIndex <> 0 Then
                '    ParGen.Visible = True
                '    gb_generale.Visible = True
                '    gb_motore.Visible = True
                '    Guna2DataGridView1.Visible = True
                '    'Guna2Button2.Visible = True
                '    gb_ventilatore.Visible = True
                '    gb_ventola.Visible = True
                '    gb_supporto.Visible = True
                '    gb_convogliatore.Visible = True
                '    'Else
                '    'ParGen.Visible = True
                '    'Guna2DataGridView1.Visible = True
                '    'gb_conv1.Visible = True
                '    'End If
                'End If

            End If

        End If




    End Sub




    Public Sub Acquisisci_cbx_tbx_check()


        aggiunta_val = 0


        'CBX

        For Each item As Control In gb_generale.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                vettore_aggiornamento(aggiunta_val) = cb.SelectedIndex + 1
                vettore_nomi_aggiornamento(aggiunta_val) = cb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next



        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)


                vettore_aggiornamento(aggiunta_val) = cb.SelectedIndex + 1
                vettore_nomi_aggiornamento(aggiunta_val) = cb.Name


                If cb.Name = "cbx_Volt" Then

                    Select Case cb.SelectedItem

                        Case 220
                            vettore_aggiornamento(aggiunta_val) = 1
                        Case 230
                            vettore_aggiornamento(aggiunta_val) = 2
                        Case 380
                            vettore_aggiornamento(aggiunta_val) = 3
                        Case 400
                            vettore_aggiornamento(aggiunta_val) = 4
                        Case 415
                            vettore_aggiornamento(aggiunta_val) = 5
                        Case 440
                            vettore_aggiornamento(aggiunta_val) = 6
                        Case 460
                            vettore_aggiornamento(aggiunta_val) = 7
                        Case 480
                            vettore_aggiornamento(aggiunta_val) = 8
                        Case 500
                            vettore_aggiornamento(aggiunta_val) = 9
                        Case 575
                            vettore_aggiornamento(aggiunta_val) = 10
                        Case 600
                            vettore_aggiornamento(aggiunta_val) = 11
                        Case 660
                            vettore_aggiornamento(aggiunta_val) = 12
                        Case 690
                            vettore_aggiornamento(aggiunta_val) = 13

                    End Select



                End If



                aggiunta_val = aggiunta_val + 1

            End If
        Next

        For Each item As Control In gb_ventilatore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)


                'Eccezioni

                If cb.Name = "cbx_DispMotore" Then

                    Select Case cb.SelectedItem
                        Case "Non definito"
                            vettore_aggiornamento(aggiunta_val) = 1
                        Case "A   Premente asse orizzontale"
                            vettore_aggiornamento(aggiunta_val) = 2
                        Case "AD Premente con albero verso il basso"
                            vettore_aggiornamento(aggiunta_val) = 3
                        Case "AU Premente con albero in alto"
                            vettore_aggiornamento(aggiunta_val) = 4
                        Case "B   Aspirante asse orizzontale"
                            vettore_aggiornamento(aggiunta_val) = 5
                        Case "BD Aspirante con albero in alto"
                            vettore_aggiornamento(aggiunta_val) = 6
                        Case "BU Aspirante con Albero in Basso"
                            vettore_aggiornamento(aggiunta_val) = 7
                        Case "H   Radiale asse orizzontale"
                            vettore_aggiornamento(aggiunta_val) = 8
                        Case "D Radiale con albero verso il basso"
                            vettore_aggiornamento(aggiunta_val) = 9
                        Case "U Radiale con albero in alto"
                            vettore_aggiornamento(aggiunta_val) = 10
                    End Select
                    vettore_nomi_aggiornamento(aggiunta_val) = cb.Name

                Else 'regola standard

                    vettore_aggiornamento(aggiunta_val) = cb.SelectedIndex + 1
                    vettore_nomi_aggiornamento(aggiunta_val) = cb.Name

                End If



                aggiunta_val = aggiunta_val + 1

            End If
        Next

        For Each item As Control In gb_ventola.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                vettore_aggiornamento(aggiunta_val) = cb.SelectedIndex + 1
                vettore_nomi_aggiornamento(aggiunta_val) = cb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next

        For Each item As Control In gb_supporto.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                vettore_aggiornamento(aggiunta_val) = cb.SelectedIndex + 1
                vettore_nomi_aggiornamento(aggiunta_val) = cb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next

        For Each item As Control In gb_convogliatore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                vettore_aggiornamento(aggiunta_val) = cb.SelectedIndex + 1
                vettore_nomi_aggiornamento(aggiunta_val) = cb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next

        For Each item As Control In ParGen.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                If cb.Name = "cbx_Revisione" Then
                    vettore_aggiornamento(aggiunta_val) = cb.SelectedIndex
                    vettore_nomi_aggiornamento(aggiunta_val) = cb.Name
                Else
                    vettore_aggiornamento(aggiunta_val) = cb.SelectedIndex + 1
                    vettore_nomi_aggiornamento(aggiunta_val) = cb.Name
                End If


                aggiunta_val = aggiunta_val + 1

            End If
        Next

        For Each item As Control In gb_conv1.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                vettore_aggiornamento(aggiunta_val) = cb.SelectedIndex + 1
                vettore_nomi_aggiornamento(aggiunta_val) = cb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next




        'TBX
        For Each item As Control In gb_generale.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)


                vettore_aggiornamento(aggiunta_val) = tb.Text
                vettore_nomi_aggiornamento(aggiunta_val) = tb.Name

                aggiunta_val = aggiunta_val + 1


            End If
        Next


        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                vettore_aggiornamento(aggiunta_val) = tb.Text
                vettore_nomi_aggiornamento(aggiunta_val) = tb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next

        For Each item As Control In gb_ventola.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                vettore_aggiornamento(aggiunta_val) = tb.Text
                vettore_nomi_aggiornamento(aggiunta_val) = tb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next


        For Each item As Control In ParGen.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                vettore_aggiornamento(aggiunta_val) = tb.Text
                vettore_nomi_aggiornamento(aggiunta_val) = tb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next

        For Each item As Control In gb_conv1.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                vettore_aggiornamento(aggiunta_val) = tb.Text
                vettore_nomi_aggiornamento(aggiunta_val) = tb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next



        'CHECK

        For Each item As Control In gb_generale.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                Dim chb As Guna.UI2.WinForms.Guna2CheckBox = DirectCast(item, Guna.UI2.WinForms.Guna2CheckBox)


                vettore_aggiornamento(aggiunta_val) = chb.CheckState
                vettore_nomi_aggiornamento(aggiunta_val) = chb.Name

                aggiunta_val = aggiunta_val + 1


            End If
        Next



        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                Dim chb As Guna.UI2.WinForms.Guna2CheckBox = DirectCast(item, Guna.UI2.WinForms.Guna2CheckBox)

                vettore_aggiornamento(aggiunta_val) = chb.CheckState
                vettore_nomi_aggiornamento(aggiunta_val) = chb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next



        For Each item As Control In ParGen.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                Dim chb As Guna.UI2.WinForms.Guna2CheckBox = DirectCast(item, Guna.UI2.WinForms.Guna2CheckBox)

                vettore_aggiornamento(aggiunta_val) = chb.CheckState
                vettore_nomi_aggiornamento(aggiunta_val) = chb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next

        If cbx_Revisione.SelectedIndex = 0 Then
            check_RevCliente.Visible = False
        Else
            check_RevCliente.Visible = True
        End If

    End Sub


    Private Sub btn_VipDesigner_Click(sender As Object, e As EventArgs) Handles btn_VipDesigner.Click

        Yes_No_Warning = 0

        Warning.Label1.Text = "     Vuoi progettare con Vip Designer?"

        If Warning.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
            'attendo la risposta della box
        End If


        If Yes_No_Warning = 1 Then
            Guna2Button2_Click(sender, e)
            salva_open_progettoVD1()

            'Procedura di blocco form fino a che ho aperto il vipdesigner
            ConnStateVD1(cbx_Revisione.SelectedIndex) 'metto la cella stato connessione a Db a 1 --> blocco la modifica del VipProject
            Me.Enabled = False
            Timer2.Start()
        End If


    End Sub


    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click ', Guna2Button1.Click

        save_PJ()

    End Sub

    Public Sub save_PJ()

        new_PJVar = 0

        If fast_PJ = 0 Then
            Guna2DataGridView1.Visible = True
        Else
            Guna2DataGridView1.Visible = False
        End If

        Guna2Button1.Visible = True
            Guna2Button3.Visible = True
            cbx_Revisione.Visible = True

        If cbx_TipoRichiesta.SelectedIndex = 0 Or fast_PJ = 1 Then
            btn_VipDesigner.Visible = False
        Else
            btn_VipDesigner.Visible = True
            End If


            btn_Datasheet.Visible = True

            blocco_load_imm = 0 'impedisco la visualizzazione della gif load quando aggiungo una revisone

            Timer1.Start()


            Guna2PictureBox1.Visible = True


            gb_convogliatore.Visible = False
        gb_supporto.Visible = False
        gb_ventilatore.Visible = False


        Guna2PictureBox1.Image = My.Resources.smile_gif

        Acquisisci_cbx_tbx_check()


        no_modificaCLrev = 1
        Modifica_riga1(cbx_Revisione.SelectedIndex)
        no_modificaCLrev = 0


        num_rev = cbx_Revisione.SelectedIndex

        If jump_rev0 = 1 Then
            num_rev = num_rev + 1
        End If


        If New_rev = 1 And jump_rev0 = 0 Then
            System.IO.Directory.CreateDirectory(folders_directory & "\" & prog_rev(posizione_progetto, 0) & "\Rev" & num_rev + 1) 'creo la directory del progetto

            Try

                If cbx_TipoRichiesta.SelectedItem <> "Convogliatori" Then

                    Dim dir_dest As String = folders_directory & "\" & prog_rev(posizione_progetto, 0) & "\Rev" & num_rev & "\" & "Modulo di Progettazione Ventilatori Assiali " & "Rev" & num_rev & ".xlsm"

                    If System.IO.File.Exists(dir_dest) = False Then
                        IO.File.Copy(modulo_PJ, dir_dest, True) 'copy(dalla cartella, alla cartella con nome del file)
                    End If

                End If


            Catch ex As Exception

            End Try


        Else

            System.IO.Directory.CreateDirectory(folders_directory & "\" & prog_rev(posizione_progetto, 0) & "\Rev" & num_rev) 'creo la directory del progetto
            System.IO.Directory.CreateDirectory(folders_directory & "\" & prog_rev(posizione_progetto, 0) & "\Rev" & num_rev + 1)

            Try

                If cbx_TipoRichiesta.SelectedItem <> "Convogliatori" Then

                    Dim dir_dest As String = folders_directory & "\" & prog_rev(posizione_progetto, 0) & "\Rev" & num_rev & "\" & "Modulo di Progettazione Ventilatori Assiali " & "Rev" & num_rev & ".xlsm"

                    If System.IO.File.Exists(dir_dest) = False Then

                        IO.File.Copy(modulo_PJ, dir_dest, True) 'copy(dalla cartella, alla cartella con nome del file)

                    End If

                End If


            Catch ex As Exception

            End Try

        End If


        If cbx_Revisione.SelectedIndex = 0 And jump_rev0 = 0 And cbx_Revisione.Items.Count = 1 Then

            Guna2Button2.Text = "Salva Progetto"
            ParGen.CustomBorderColor = Color.FromArgb(213, 218, 223)
            Label23.Visible = False


            jump_rev0 = 1
            Guna2Button1.PerformClick()
            jump_rev0 = 0

            'blocca parametri clienti
            'mod_rev0 = 1
            'Lettura_riga1(0)
            'compila_cbx_ClinteRev0()
            'compila_tbx_ClinteRev0()
            'compila_check_ClinteRev0()
            'mod_rev0 = 0

        End If


        New_rev = 0

    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        time_val = time_val + 1


        If time_val > 46 Then

            Guna2PictureBox1.Visible = False

            If cbx_TipoRichiesta.SelectedItem <> "Convogliatori" And fast_PJ = 0 Then
                gb_convogliatore.Visible = True
                gb_supporto.Visible = True
                gb_ventilatore.Visible = True
            End If


            blocco_load_imm = 1 ' sblocco
            time_val = 0
            Timer1.Stop()
            End If


    End Sub



    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click


        Yes_No_Warning = 0

        Warning.Label1.Text = "     Vuoi creare una nuova revisione?"

        If Warning.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
            'attendo la risposta della box
        End If


        If Yes_No_Warning = 1 Then

            New_rev = 1

            AggiungiRev1()

            Dim RichiestaCL As String = 0
            Dim RevCL As String = 0


            For i = 0 To cbx_Revisione.Items.Count - 1

                Dim Str_Rev As String = cbx_Revisione.Items(i)

                If Str_Rev.Substring(0, 2) = "Ri" Then
                    RichiestaCL = RichiestaCL + 1
                Else
                    RevCL = RevCL + 1
                End If

            Next


            skip_rev_cbx_change = 1
            'cbx_Revisione.Items.Add("Revisione " & prog_rev(posizione_progetto, 1) + 1)
            'cbx_Revisione.SelectedIndex = prog_rev(posizione_progetto, 1) + 1
            If check_RevCliente.Checked = True Then
                cbx_Revisione.Items.Add("Richiesta  " & RichiestaCL)
            Else
                cbx_Revisione.Items.Add("Revisione " & RevCL + 1)
            End If

            cbx_Revisione.SelectedIndex = cbx_Revisione.Items.Count - 1
            skip_rev_cbx_change = 0

            Acquisisci_cbx_tbx_check()
            Modifica_riga1(cbx_Revisione.SelectedIndex)

            prog_rev(posizione_progetto, 1) = prog_rev(posizione_progetto, 1) + 1
            'lettura_progetto1()

            ModCliente1(cbx_Revisione.SelectedIndex)

            'Escludo il ciclo di creazione del progetto
            If cbx_Revisione.Items.Count = 2 Then
                cbx_Revisione.SelectedIndex = cbx_Revisione.Items.Count - 2
            End If

        End If



    End Sub

    Private Sub Rad_Safe_CheckedChanged(sender As Object, e As EventArgs) Handles Rad_Safe.CheckedChanged

        If Rad_Safe.Checked = True Then


            cbx_AtexCategoria.Visible = False
            cbx_AtexClasseTemperatura.Visible = False
            cbx_AtexCustodia.Visible = False
            cbx_AtexProtezione.Visible = False

            cbx_AtexCategoria.SelectedIndex = -1
            cbx_AtexClasseTemperatura.SelectedIndex = -1
            cbx_AtexCustodia.SelectedIndex = -1
            cbx_AtexProtezione.SelectedIndex = -1

            Label14.Visible = False
            Label22.Visible = False
            Label35.Visible = False
            Label36.Visible = False

        Else



            cbx_AtexCategoria.Visible = True
            cbx_AtexClasseTemperatura.Visible = True
            cbx_AtexCustodia.Visible = True
            cbx_AtexProtezione.Visible = True

            Label14.Visible = True
            Label22.Visible = True
            Label35.Visible = True
            Label36.Visible = True

        End If

    End Sub



    Private Sub Rad_Atex_CheckedChanged(sender As Object, e As EventArgs) Handles Rad_Atex.CheckedChanged


        If Rad_Safe.Checked = True Then


            cbx_AtexCategoria.Visible = False
            cbx_AtexClasseTemperatura.Visible = False
            cbx_AtexCustodia.Visible = False
            cbx_AtexProtezione.Visible = False


            cbx_AtexCategoria.SelectedIndex = -1
            cbx_AtexClasseTemperatura.SelectedIndex = -1
            cbx_AtexCustodia.SelectedIndex = -1
            cbx_AtexProtezione.SelectedIndex = -1

            Label14.Visible = False
            Label22.Visible = False
            Label35.Visible = False
            Label36.Visible = False

        Else

            cbx_AtexCategoria.Visible = True
            cbx_AtexClasseTemperatura.Visible = True
            cbx_AtexCustodia.Visible = True
            cbx_AtexProtezione.Visible = True

            Label14.Visible = True
            Label22.Visible = True
            Label35.Visible = True
            Label36.Visible = True


            For i = 0 To Numero_tabelle

                Select Case SchemaDB(i)
                    Case "cbx_AtexProtezione"
                        funzione_cbx1(cbx_AtexProtezione, SchemaDB(i))
                    Case "cbx_AtexCustodia"
                        funzione_cbx1(cbx_AtexCustodia, SchemaDB(i))
                    Case "cbx_AtexCategoria"
                        funzione_cbx1(cbx_AtexCategoria, SchemaDB(i))
                    Case "cbx_AtexClasseTemperatura"
                        funzione_cbx1(cbx_AtexClasseTemperatura, SchemaDB(i))
                End Select

            Next

        End If


    End Sub

    Private Sub cbx_TipoFan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_TipoFan.SelectedIndexChanged


        'If blocca_controllo_vip = 1 Then

        cbx_ConfigurazioneFan.Items.Clear()

        If cbx_TipoFan.SelectedIndex = 1 Then

            cbx_ConfigurazioneFan.Items.Add("Standard")
            cbx_ConfigurazioneFan.Items.Add("Torrino3")
            cbx_ConfigurazioneFan.Items.Add("Torrino4")

            'Direzione flusso
            'cbx_Direzione.SelectedIndex = 3
            'cbx_Direzione.Enabled = False

        ElseIf cbx_TipoFan.SelectedIndex = 0 Then

            cbx_ConfigurazioneFan.Items.Add("I")
            cbx_ConfigurazioneFan.Items.Add("9")
            cbx_ConfigurazioneFan.Items.Add("J")
            cbx_ConfigurazioneFan.Items.Add("A")

            'Direzione flusso
            'cbx_Direzione.SelectedIndex = 0
            'cbx_Direzione.Enabled = True

        ElseIf cbx_TipoFan.SelectedIndex = 2 Then

            cbx_ConfigurazioneFan.Items.Add("Basso profilo")
            cbx_ConfigurazioneFan.Items.Add("Alto profilo sbordato")
            cbx_ConfigurazioneFan.Items.Add("Alto profilo flangiato")
            cbx_ConfigurazioneFan.Items.Add("Basso profilo - senza pannello")
            cbx_ConfigurazioneFan.Items.Add("Alto profilo sbordato - senza pannello")
            cbx_ConfigurazioneFan.Items.Add("Alto profilo flangiato - senza pannello")

            'Direzione flusso
            'cbx_Direzione.SelectedIndex = 0
            'cbx_Direzione.Enabled = True

        ElseIf cbx_TipoFan.SelectedIndex = 3 Then


            cbx_ConfigurazioneFan.Items.Add("I") '0
            cbx_ConfigurazioneFan.Items.Add("J") '1
            cbx_ConfigurazioneFan.Items.Add("9") '2
            cbx_ConfigurazioneFan.Items.Add("A") '3
            cbx_ConfigurazioneFan.Items.Add("E") '4
            cbx_ConfigurazioneFan.Items.Add("F") '5
            cbx_ConfigurazioneFan.Items.Add("G") '6
            cbx_ConfigurazioneFan.Items.Add("H") '7
            cbx_ConfigurazioneFan.Items.Add("K") '8
            cbx_ConfigurazioneFan.Items.Add("L") '9
            cbx_ConfigurazioneFan.Items.Add("5") '10
            cbx_ConfigurazioneFan.Items.Add("P") '11
            cbx_ConfigurazioneFan.Items.Add("F") '12
            cbx_ConfigurazioneFan.Items.Add("M") '14
            cbx_ConfigurazioneFan.Items.Add("N") '15

            'Direzione flusso
            'cbx_Direzione.SelectedIndex = 0
            'cbx_Direzione.Enabled = True

        End If

        Try

            'Ricorda di aggiornare il valore di Valore_CellaRiga(21) --> se cambia il database puo essere che non sia piu 21
            cbx_ConfigurazioneFan.SelectedIndex = Valore_CellaRiga(21) - 1

        Catch ex As Exception

        End Try


        hide_fastPJ()


    End Sub



    Private Sub cbx_TipoFan_SelectedIndexChanged1(sender As Object, e As EventArgs) Handles cbx_TipoFan.SelectedIndexChanged



        If cbx_TipoFan.Text = "" Then
            Pic_TipoFan.Image = My.Resources.warning1
            cbx_TipoFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoFan = 0
        Else
            Pic_TipoFan.Image = My.Resources.Vstate
            cbx_TipoFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoFan = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If


    End Sub



    Private Sub cbx_ApplicazioneFan_SelectedIndexChanged1(sender As Object, e As EventArgs) Handles cbx_ApplicazioneFan.SelectedIndexChanged



        If cbx_ApplicazioneFan.Text = "" Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If

        hide_fastPJ()
    End Sub






    'cbx_ApplicazioneFan




    'Private Sub cbx_ConfigurazioneFan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_ConfigurazioneFan.SelectedIndexChanged

    '    If cbx_ConfigurazioneFan.Text = "" Then
    '        Pic_Config.Image = My.Resources.warning1
    '        cbx_ConfigurazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
    '        Set_Config = 0
    '    Else
    '        Pic_Config.Image = My.Resources.Vstate
    '        cbx_ConfigurazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
    '        Set_Config = 1
    '    End If

    '    If blocca_controllo_vip = 1 Then
    '        SbloccaVD1()
    '    End If

    'End Sub

    'Private Sub cbx_ApplicazioneFan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_ApplicazioneFan.SelectedIndexChanged

    '    If cbx_ApplicazioneFan.Text = "" Then
    '        Pic_App.Image = My.Resources.warning1
    '        cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
    '        Set_App = 0
    '    Else
    '        Pic_App.Image = My.Resources.Vstate
    '        cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
    '        Set_App = 1
    '    End If

    '    If blocca_controllo_vip = 1 Then
    '        SbloccaVD1()
    '    End If

    'End Sub

    'Private Sub cbx_Poli_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_Poli.SelectedIndexChanged

    '    If cbx_Poli.Text = "" Then
    '        Pic_Poli.Image = My.Resources.warning1
    '        cbx_Poli.BorderColor = Color.FromArgb(250, 117, 158)
    '        Set_Poli = 0
    '    Else
    '        Pic_Poli.Image = My.Resources.Vstate
    '        cbx_Poli.BorderColor = Color.FromArgb(74, 231, 148)
    '        Set_Poli = 1
    '    End If

    '    If blocca_controllo_vip = 1 Then
    '        SbloccaVD1()
    '    End If

    'End Sub


    Private Sub Rad_Safe_CheckedChanged1(sender As Object, e As EventArgs) Handles Rad_Safe.CheckedChanged, Rad_Atex.CheckedChanged


        Set_Atex_Safe = 1
        Pic_save_atex.Image = My.Resources.Vstate

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If


    End Sub


    Private Sub tbx_Cliente_TextChanged(sender As Object, e As EventArgs) Handles tbx_Cliente.TextChanged



        If tbx_Cliente.Text = "" Then
            Pic_client.Image = My.Resources.warning1
            tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
            Set_client = 0
        Else
            Pic_client.Image = My.Resources.Vstate
            tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
            Set_client = 1
        End If

        If tbx_Portata.Text = "" Then
            Pic_Portata.Image = My.Resources.warning1
            tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Portata = 0
        Else
            Pic_Portata.Image = My.Resources.Vstate
            tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Portata = 1
        End If

        If tbx_Pressione.Text = "" Then
            Pic_pressione.Image = My.Resources.warning1
            tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Pressione = 0
        Else
            Pic_pressione.Image = My.Resources.Vstate
            tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Pressione = 1
        End If

        If cbx_Direzione.SelectedIndex = -1 Then
            Pic_direzione.Image = My.Resources.warning1
            cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_DirFlusso = 0
        Else
            Pic_direzione.Image = My.Resources.Vstate
            cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_DirFlusso = 1
        End If



        If cbx_ApplicazioneFan.SelectedIndex = -1 Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If


        If tbx_TemperaturaMinAmb.Text = "" Then
            Pic_min.Image = My.Resources.warning1
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Min = 0
        Else
            Pic_min.Image = My.Resources.Vstate
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Min = 1
        End If

        If tbx_TemperaturaMaxAmb.Text = "" Then
            Pic_max.Image = My.Resources.warning1
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Max = 0
        Else
            Pic_max.Image = My.Resources.Vstate
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Max = 1
        End If

        If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
            Pic_richiesta.Image = My.Resources.warning1
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoRichiesta = 0
        Else
            Pic_richiesta.Image = My.Resources.Vstate
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoRichiesta = 1
        End If

        If tbx_Quantita.Text = "" Then
            Pic_Qtita.Image = My.Resources.warning1
            tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Quantita = 0
        Else
            Pic_Qtita.Image = My.Resources.Vstate
            tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Quantita = 1
        End If


        If tbx_Riferimento.Text = "" Then
            Pic_Rif.Image = My.Resources.warning1
            tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Rif = 0
        Else
            Pic_Rif.Image = My.Resources.Vstate
            tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Rif = 1
        End If


        If tbx_Descrizione.Text = "" Then ''
            Pic_descrizione.Image = My.Resources.warning1
            tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Descrizione = 0
        Else
            Pic_descrizione.Image = My.Resources.Vstate
            tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Descrizione = 1
        End If

        If cbx_TipoMotore.SelectedIndex = -1 Then
            Pic_TipoMot.Image = My.Resources.warning1
            cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoMot = 0
        Else
            Pic_TipoMot.Image = My.Resources.Vstate
            cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoMot = 1
        End If

        If cbx_Volt.SelectedIndex = -1 Then
            Pic_Tensione.Image = My.Resources.warning1
            cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Volt = 0
        Else
            Pic_Tensione.Image = My.Resources.Vstate
            cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Volt = 1
        End If

        If cbx_Hz.SelectedIndex = -1 Then
            Pic_freq.Image = My.Resources.warning1
            cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Freq = 0
        Else
            Pic_freq.Image = My.Resources.Vstate
            cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Freq = 1
        End If

        If cbx_Alimentazione.SelectedIndex = -1 Then
            Pic_alimentazione.Image = My.Resources.warning1
            cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Alim = 0
        Else
            Pic_alimentazione.Image = My.Resources.Vstate
            cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Alim = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If


    End Sub

    Private Sub tbx_Portata_TextChanged(sender As Object, e As EventArgs) Handles tbx_Portata.TextChanged


        If tbx_Cliente.Text = "" Then
            Pic_client.Image = My.Resources.warning1
            tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
            Set_client = 0
        Else
            Pic_client.Image = My.Resources.Vstate
            tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
            Set_client = 1
        End If

        If tbx_Portata.Text = "" Then
            Pic_Portata.Image = My.Resources.warning1
            tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Portata = 0
        Else
            Pic_Portata.Image = My.Resources.Vstate
            tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Portata = 1
        End If

        If tbx_Pressione.Text = "" Then
            Pic_pressione.Image = My.Resources.warning1
            tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Pressione = 0
        Else
            Pic_pressione.Image = My.Resources.Vstate
            tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Pressione = 1
        End If

        If cbx_Direzione.SelectedIndex = -1 Then
            Pic_direzione.Image = My.Resources.warning1
            cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_DirFlusso = 0
        Else
            Pic_direzione.Image = My.Resources.Vstate
            cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_DirFlusso = 1
        End If


        If cbx_ApplicazioneFan.SelectedIndex = -1 Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If

        If tbx_TemperaturaMinAmb.Text = "" Then
            Pic_min.Image = My.Resources.warning1
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Min = 0
        Else
            Pic_min.Image = My.Resources.Vstate
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Min = 1
        End If

        If tbx_TemperaturaMaxAmb.Text = "" Then
            Pic_max.Image = My.Resources.warning1
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Max = 0
        Else
            Pic_max.Image = My.Resources.Vstate
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Max = 1
        End If

        If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
            Pic_richiesta.Image = My.Resources.warning1
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoRichiesta = 0
        Else
            Pic_richiesta.Image = My.Resources.Vstate
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoRichiesta = 1
        End If


        If tbx_Quantita.Text = "" Then
            Pic_Qtita.Image = My.Resources.warning1
            tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Quantita = 0
        Else
            Pic_Qtita.Image = My.Resources.Vstate
            tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Quantita = 1
        End If

        If tbx_Riferimento.Text = "" Then
            Pic_Rif.Image = My.Resources.warning1
            tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Rif = 0
        Else
            Pic_Rif.Image = My.Resources.Vstate
            tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Rif = 1
        End If

        If tbx_Descrizione.Text = "" Then ''
            Pic_descrizione.Image = My.Resources.warning1
            tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Descrizione = 0
        Else
            Pic_descrizione.Image = My.Resources.Vstate
            tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Descrizione = 1
        End If

        If cbx_TipoMotore.SelectedIndex = -1 Then
            Pic_TipoMot.Image = My.Resources.warning1
            cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoMot = 0
        Else
            Pic_TipoMot.Image = My.Resources.Vstate
            cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoMot = 1
        End If

        If cbx_Volt.SelectedIndex = -1 Then
            Pic_Tensione.Image = My.Resources.warning1
            cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Volt = 0
        Else
            Pic_Tensione.Image = My.Resources.Vstate
            cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Volt = 1
        End If

        If cbx_Hz.SelectedIndex = -1 Then
            Pic_freq.Image = My.Resources.warning1
            cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Freq = 0
        Else
            Pic_freq.Image = My.Resources.Vstate
            cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Freq = 1
        End If

        If cbx_Alimentazione.SelectedIndex = -1 Then
            Pic_alimentazione.Image = My.Resources.warning1
            cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Alim = 0
        Else
            Pic_alimentazione.Image = My.Resources.Vstate
            cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Alim = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If

        hide_fastPJ()

    End Sub

    Private Sub tbx_Pressione_TextChanged(sender As Object, e As EventArgs) Handles tbx_Pressione.TextChanged



        If tbx_Cliente.Text = "" Then
            Pic_client.Image = My.Resources.warning1
            tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
            Set_client = 0
        Else
            Pic_client.Image = My.Resources.Vstate
            tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
            Set_client = 1
        End If

        If tbx_Portata.Text = "" Then
            Pic_Portata.Image = My.Resources.warning1
            tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Portata = 0
        Else
            Pic_Portata.Image = My.Resources.Vstate
            tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Portata = 1
        End If

        If tbx_Pressione.Text = "" Then
            Pic_pressione.Image = My.Resources.warning1
            tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Pressione = 0
        Else
            Pic_pressione.Image = My.Resources.Vstate
            tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Pressione = 1
        End If

        If cbx_Direzione.SelectedIndex = -1 Then
            Pic_direzione.Image = My.Resources.warning1
            cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_DirFlusso = 0
        Else
            Pic_direzione.Image = My.Resources.Vstate
            cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_DirFlusso = 1
        End If


        If cbx_ApplicazioneFan.SelectedIndex = -1 Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If

        If tbx_TemperaturaMinAmb.Text = "" Then
            Pic_min.Image = My.Resources.warning1
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Min = 0
        Else
            Pic_min.Image = My.Resources.Vstate
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Min = 1
        End If

        If tbx_TemperaturaMaxAmb.Text = "" Then
            Pic_max.Image = My.Resources.warning1
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Max = 0
        Else
            Pic_max.Image = My.Resources.Vstate
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Max = 1
        End If

        If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
            Pic_richiesta.Image = My.Resources.warning1
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoRichiesta = 0
        Else
            Pic_richiesta.Image = My.Resources.Vstate
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoRichiesta = 1
        End If

        If tbx_Quantita.Text = "" Then
            Pic_Qtita.Image = My.Resources.warning1
            tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Quantita = 0
        Else
            Pic_Qtita.Image = My.Resources.Vstate
            tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Quantita = 1
        End If

        If tbx_Riferimento.Text = "" Then
            Pic_Rif.Image = My.Resources.warning1
            tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Rif = 0
        Else
            Pic_Rif.Image = My.Resources.Vstate
            tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Rif = 1
        End If


        If tbx_Descrizione.Text = "" Then ''
            Pic_descrizione.Image = My.Resources.warning1
            tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Descrizione = 0
        Else
            Pic_descrizione.Image = My.Resources.Vstate
            tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Descrizione = 1
        End If

        If cbx_TipoMotore.SelectedIndex = -1 Then
            Pic_TipoMot.Image = My.Resources.warning1
            cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoMot = 0
        Else
            Pic_TipoMot.Image = My.Resources.Vstate
            cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoMot = 1
        End If

        If cbx_Volt.SelectedIndex = -1 Then
            Pic_Tensione.Image = My.Resources.warning1
            cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Volt = 0
        Else
            Pic_Tensione.Image = My.Resources.Vstate
            cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Volt = 1
        End If

        If cbx_Hz.SelectedIndex = -1 Then
            Pic_freq.Image = My.Resources.warning1
            cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Freq = 0
        Else
            Pic_freq.Image = My.Resources.Vstate
            cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Freq = 1
        End If

        If cbx_Alimentazione.SelectedIndex = -1 Then
            Pic_alimentazione.Image = My.Resources.warning1
            cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Alim = 0
        Else
            Pic_alimentazione.Image = My.Resources.Vstate
            cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Alim = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If

        hide_fastPJ()

    End Sub

    Private Sub cbx_Direzione_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_Direzione.SelectedIndexChanged



        If tbx_Cliente.Text = "" Then
            Pic_client.Image = My.Resources.warning1
            tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
            Set_client = 0
        Else
            Pic_client.Image = My.Resources.Vstate
            tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
            Set_client = 1
        End If

        If tbx_Portata.Text = "" Then
            Pic_Portata.Image = My.Resources.warning1
            tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Portata = 0
        Else
            Pic_Portata.Image = My.Resources.Vstate
            tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Portata = 1
        End If

        If tbx_Pressione.Text = "" Then
            Pic_pressione.Image = My.Resources.warning1
            tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Pressione = 0
        Else
            Pic_pressione.Image = My.Resources.Vstate
            tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Pressione = 1
        End If

        If cbx_Direzione.SelectedIndex = -1 Then
            Pic_direzione.Image = My.Resources.warning1
            cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_DirFlusso = 0
        Else
            Pic_direzione.Image = My.Resources.Vstate
            cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_DirFlusso = 1
        End If


        If cbx_ApplicazioneFan.SelectedIndex = -1 Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If

        If tbx_TemperaturaMinAmb.Text = "" Then
            Pic_min.Image = My.Resources.warning1
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Min = 0
        Else
            Pic_min.Image = My.Resources.Vstate
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Min = 1
        End If

        If tbx_TemperaturaMaxAmb.Text = "" Then
            Pic_max.Image = My.Resources.warning1
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Max = 0
        Else
            Pic_max.Image = My.Resources.Vstate
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Max = 1
        End If

        If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
            Pic_richiesta.Image = My.Resources.warning1
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoRichiesta = 0
        Else
            Pic_richiesta.Image = My.Resources.Vstate
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoRichiesta = 1
        End If

        If tbx_Quantita.Text = "" Then
            Pic_Qtita.Image = My.Resources.warning1
            tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Quantita = 0
        Else
            Pic_Qtita.Image = My.Resources.Vstate
            tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Quantita = 1
        End If

        If tbx_Riferimento.Text = "" Then
            Pic_Rif.Image = My.Resources.warning1
            tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Rif = 0
        Else
            Pic_Rif.Image = My.Resources.Vstate
            tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Rif = 1
        End If


        If tbx_Descrizione.Text = "" Then ''
            Pic_descrizione.Image = My.Resources.warning1
            tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Descrizione = 0
        Else
            Pic_descrizione.Image = My.Resources.Vstate
            tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Descrizione = 1
        End If

        If cbx_TipoMotore.SelectedIndex = -1 Then
            Pic_TipoMot.Image = My.Resources.warning1
            cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoMot = 0
        Else
            Pic_TipoMot.Image = My.Resources.Vstate
            cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoMot = 1
        End If

        If cbx_Volt.SelectedIndex = -1 Then
            Pic_Tensione.Image = My.Resources.warning1
            cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Volt = 0
        Else
            Pic_Tensione.Image = My.Resources.Vstate
            cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Volt = 1
        End If

        If cbx_Hz.SelectedIndex = -1 Then
            Pic_freq.Image = My.Resources.warning1
            cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Freq = 0
        Else
            Pic_freq.Image = My.Resources.Vstate
            cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Freq = 1
        End If

        If cbx_Alimentazione.SelectedIndex = -1 Then
            Pic_alimentazione.Image = My.Resources.warning1
            cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Alim = 0
        Else
            Pic_alimentazione.Image = My.Resources.Vstate
            cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Alim = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If


        cbx_DispMotore.Items.Clear()


        If cbx_Direzione.SelectedIndex = 0 Then
            cbx_DispMotore.Items.Add("Non definito")
            cbx_DispMotore.Items.Add("A   Premente asse orizzontale")
            cbx_DispMotore.Items.Add("AD Premente con albero verso il basso")
            cbx_DispMotore.Items.Add("AU Premente con albero in alto")
        ElseIf cbx_Direzione.SelectedIndex = 1 Then
            cbx_DispMotore.Items.Add("Non definito")
            cbx_DispMotore.Items.Add("B   Aspirante asse orizzontale")
            cbx_DispMotore.Items.Add("BD Aspirante con albero in alto")
            cbx_DispMotore.Items.Add("BU Aspirante con Albero in Basso")
        ElseIf cbx_Direzione.SelectedIndex = 3 Then
            cbx_DispMotore.Items.Add("Non definito")
            cbx_DispMotore.Items.Add("H   Radiale asse orizzontale")
            cbx_DispMotore.Items.Add("D Radiale con albero verso il basso")
            cbx_DispMotore.Items.Add("U Radiale con albero in alto")

        End If


        funzione_cbx1(cbx_DispMotore, "cbx_DispMotore")

        hide_fastPJ()

    End Sub






    Private Sub tbx_TemperaturaMinAmb_TextChanged(sender As Object, e As EventArgs) Handles tbx_TemperaturaMinAmb.TextChanged



        If tbx_Cliente.Text = "" Then
            Pic_client.Image = My.Resources.warning1
            tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
            Set_client = 0
        Else
            Pic_client.Image = My.Resources.Vstate
            tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
            Set_client = 1
        End If

        If tbx_Portata.Text = "" Then
            Pic_Portata.Image = My.Resources.warning1
            tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Portata = 0
        Else
            Pic_Portata.Image = My.Resources.Vstate
            tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Portata = 1
        End If

        If tbx_Pressione.Text = "" Then
            Pic_pressione.Image = My.Resources.warning1
            tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Pressione = 0
        Else
            Pic_pressione.Image = My.Resources.Vstate
            tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Pressione = 1
        End If

        If cbx_Direzione.SelectedIndex = -1 Then
            Pic_direzione.Image = My.Resources.warning1
            cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_DirFlusso = 0
        Else
            Pic_direzione.Image = My.Resources.Vstate
            cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_DirFlusso = 1
        End If


        If cbx_ApplicazioneFan.SelectedIndex = -1 Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If

        If tbx_TemperaturaMinAmb.Text = "" Then
            Pic_min.Image = My.Resources.warning1
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Min = 0
        Else
            Pic_min.Image = My.Resources.Vstate
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Min = 1
        End If

        If tbx_TemperaturaMaxAmb.Text = "" Then
            Pic_max.Image = My.Resources.warning1
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Max = 0
        Else
            Pic_max.Image = My.Resources.Vstate
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Max = 1
        End If

        If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
            Pic_richiesta.Image = My.Resources.warning1
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoRichiesta = 0
        Else
            Pic_richiesta.Image = My.Resources.Vstate
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoRichiesta = 1
        End If

        If tbx_Quantita.Text = "" Then
            Pic_Qtita.Image = My.Resources.warning1
            tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Quantita = 0
        Else
            Pic_Qtita.Image = My.Resources.Vstate
            tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Quantita = 1
        End If

        If tbx_Riferimento.Text = "" Then
            Pic_Rif.Image = My.Resources.warning1
            tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Rif = 0
        Else
            Pic_Rif.Image = My.Resources.Vstate
            tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Rif = 1
        End If


        If tbx_Descrizione.Text = "" Then ''
            Pic_descrizione.Image = My.Resources.warning1
            tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Descrizione = 0
        Else
            Pic_descrizione.Image = My.Resources.Vstate
            tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Descrizione = 1
        End If

        If cbx_TipoMotore.SelectedIndex = -1 Then
            Pic_TipoMot.Image = My.Resources.warning1
            cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoMot = 0
        Else
            Pic_TipoMot.Image = My.Resources.Vstate
            cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoMot = 1
        End If

        If cbx_Volt.SelectedIndex = -1 Then
            Pic_Tensione.Image = My.Resources.warning1
            cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Volt = 0
        Else
            Pic_Tensione.Image = My.Resources.Vstate
            cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Volt = 1
        End If

        If cbx_Hz.SelectedIndex = -1 Then
            Pic_freq.Image = My.Resources.warning1
            cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Freq = 0
        Else
            Pic_freq.Image = My.Resources.Vstate
            cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Freq = 1
        End If

        If cbx_Alimentazione.SelectedIndex = -1 Then
            Pic_alimentazione.Image = My.Resources.warning1
            cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Alim = 0
        Else
            Pic_alimentazione.Image = My.Resources.Vstate
            cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Alim = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If

        hide_fastPJ()

    End Sub



    Private Sub tbx_TemperaturaMaxAmb_TextChanged(sender As Object, e As EventArgs) Handles tbx_TemperaturaMaxAmb.TextChanged


        If tbx_Cliente.Text = "" Then
            Pic_client.Image = My.Resources.warning1
            tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
            Set_client = 0
        Else
            Pic_client.Image = My.Resources.Vstate
            tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
            Set_client = 1
        End If

        If tbx_Portata.Text = "" Then
            Pic_Portata.Image = My.Resources.warning1
            tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Portata = 0
        Else
            Pic_Portata.Image = My.Resources.Vstate
            tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Portata = 1
        End If

        If tbx_Pressione.Text = "" Then
            Pic_pressione.Image = My.Resources.warning1
            tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Pressione = 0
        Else
            Pic_pressione.Image = My.Resources.Vstate
            tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Pressione = 1
        End If

        If cbx_Direzione.SelectedIndex = -1 Then
            Pic_direzione.Image = My.Resources.warning1
            cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_DirFlusso = 0
        Else
            Pic_direzione.Image = My.Resources.Vstate
            cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_DirFlusso = 1
        End If


        If cbx_ApplicazioneFan.SelectedIndex = -1 Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If

        If tbx_TemperaturaMinAmb.Text = "" Then
            Pic_min.Image = My.Resources.warning1
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Min = 0
        Else
            Pic_min.Image = My.Resources.Vstate
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Min = 1
        End If

        If tbx_TemperaturaMaxAmb.Text = "" Then
            Pic_max.Image = My.Resources.warning1
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Max = 0
        Else
            Pic_max.Image = My.Resources.Vstate
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Max = 1
        End If

        If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
            Pic_richiesta.Image = My.Resources.warning1
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoRichiesta = 0
        Else
            Pic_richiesta.Image = My.Resources.Vstate
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoRichiesta = 1
        End If

        If tbx_Quantita.Text = "" Then
            Pic_Qtita.Image = My.Resources.warning1
            tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Quantita = 0
        Else
            Pic_Qtita.Image = My.Resources.Vstate
            tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Quantita = 1
        End If

        If tbx_Riferimento.Text = "" Then
            Pic_Rif.Image = My.Resources.warning1
            tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Rif = 0
        Else
            Pic_Rif.Image = My.Resources.Vstate
            tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Rif = 1
        End If


        If tbx_Descrizione.Text = "" Then ''
            Pic_descrizione.Image = My.Resources.warning1
            tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Descrizione = 0
        Else
            Pic_descrizione.Image = My.Resources.Vstate
            tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Descrizione = 1
        End If

        If cbx_TipoMotore.SelectedIndex = -1 Then
            Pic_TipoMot.Image = My.Resources.warning1
            cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoMot = 0
        Else
            Pic_TipoMot.Image = My.Resources.Vstate
            cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoMot = 1
        End If

        If cbx_Volt.SelectedIndex = -1 Then
            Pic_Tensione.Image = My.Resources.warning1
            cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Volt = 0
        Else
            Pic_Tensione.Image = My.Resources.Vstate
            cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Volt = 1
        End If

        If cbx_Hz.SelectedIndex = -1 Then
            Pic_freq.Image = My.Resources.warning1
            cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Freq = 0
        Else
            Pic_freq.Image = My.Resources.Vstate
            cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Freq = 1
        End If

        If cbx_Alimentazione.SelectedIndex = -1 Then
            Pic_alimentazione.Image = My.Resources.warning1
            cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Alim = 0
        Else
            Pic_alimentazione.Image = My.Resources.Vstate
            cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Alim = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If

        hide_fastPJ()

    End Sub



    Private Sub cbx_TipoRichiesta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_TipoRichiesta.SelectedIndexChanged




        'Blocco combobox tipo progettazione --> Pannello e ventilatore
        For i = 0 To Numero_colonneDB - 1

            'cerco la variabile di connessione al VipDesigner
            If Nome_colonne(i + 1) = "cbx_TipoRichiesta" Then

                If Valore_CellaRiga(i) <> "" Then
                    cbx_TipoRichiesta.Enabled = False
                End If

            End If

        Next



        If jump_tipo_richiesta = 0 Then

            If cbx_TipoRichiesta.SelectedIndex = 0 Then


                If fast_PJ = 0 Then
                    gb_conv1.Visible = True

                End If


                gb_conv1.Location = New System.Drawing.Point(4, 150)
                gb_generale.Visible = False
                gb_motore.Visible = False
                gb_ventilatore.Visible = False
                gb_ventola.Visible = False
                gb_convogliatore.Visible = False
                gb_supporto.Visible = False

                All_Show()

                btn_Datasheet.Visible = True
                btn_VipDesigner.Visible = False
                Guna2Button2.Visible = True

                btn_VipDesigner.BringToFront()
                btn_Datasheet.BringToFront()
            Else

                gb_conv1.Visible = False
                gb_conv1.Location = New System.Drawing.Point(1283, 150)

                If fast_PJ = 0 Then
                    gb_generale.Visible = True
                    gb_motore.Visible = True
                    gb_ventilatore.Visible = True
                    gb_ventola.Visible = True
                    gb_convogliatore.Visible = True
                    gb_supporto.Visible = True
                End If

            End If


            If tbx_Cliente.Text = "" Then
                Pic_client.Image = My.Resources.warning1
                tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
                Set_client = 0
            Else
                Pic_client.Image = My.Resources.Vstate
                tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
                Set_client = 1
            End If

            If tbx_Portata.Text = "" Then
                Pic_Portata.Image = My.Resources.warning1
                tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
                Set_Portata = 0
            Else
                Pic_Portata.Image = My.Resources.Vstate
                tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
                Set_Portata = 1
            End If

            If tbx_Pressione.Text = "" Then
                Pic_pressione.Image = My.Resources.warning1
                tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
                Set_Pressione = 0
            Else
                Pic_pressione.Image = My.Resources.Vstate
                tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
                Set_Pressione = 1
            End If

            If cbx_Direzione.SelectedIndex = -1 Then
                Pic_direzione.Image = My.Resources.warning1
                cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
                Set_DirFlusso = 0
            Else
                Pic_direzione.Image = My.Resources.Vstate
                cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
                Set_DirFlusso = 1
            End If


            If cbx_ApplicazioneFan.SelectedIndex = -1 Then
                Pic_applicazione.Image = My.Resources.warning1
                cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
                Set_applicazione = 0
            Else
                Pic_applicazione.Image = My.Resources.Vstate
                cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
                Set_applicazione = 1
            End If

            If tbx_TemperaturaMinAmb.Text = "" Then
                Pic_min.Image = My.Resources.warning1
                tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
                Set_Min = 0
            Else
                Pic_min.Image = My.Resources.Vstate
                tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
                Set_Min = 1
            End If

            If tbx_TemperaturaMaxAmb.Text = "" Then
                Pic_max.Image = My.Resources.warning1
                tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
                Set_Max = 0
            Else
                Pic_max.Image = My.Resources.Vstate
                tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
                Set_Max = 1
            End If

            If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
                Pic_richiesta.Image = My.Resources.warning1
                cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
                Set_TipoRichiesta = 0
            Else
                Pic_richiesta.Image = My.Resources.Vstate
                cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
                Set_TipoRichiesta = 1
            End If

            If tbx_Quantita.Text = "" Then
                Pic_Qtita.Image = My.Resources.warning1
                tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
                Set_Quantita = 0
            Else
                Pic_Qtita.Image = My.Resources.Vstate
                tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
                Set_Quantita = 1
            End If

            If tbx_Riferimento.Text = "" Then
                Pic_Rif.Image = My.Resources.warning1
                tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
                Set_Rif = 0
            Else
                Pic_Rif.Image = My.Resources.Vstate
                tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
                Set_Rif = 1
            End If

            If tbx_Descrizione.Text = "" Then ''
                Pic_descrizione.Image = My.Resources.warning1
                tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
                Set_Descrizione = 0
            Else
                Pic_descrizione.Image = My.Resources.Vstate
                tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
                Set_Descrizione = 1
            End If

            If cbx_TipoMotore.SelectedIndex = -1 Then
                Pic_TipoMot.Image = My.Resources.warning1
                cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
                Set_TipoMot = 0
            Else
                Pic_TipoMot.Image = My.Resources.Vstate
                cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
                Set_TipoMot = 1
            End If

            If cbx_Volt.SelectedIndex = -1 Then
                Pic_Tensione.Image = My.Resources.warning1
                cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
                Set_Volt = 0
            Else
                Pic_Tensione.Image = My.Resources.Vstate
                cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
                Set_Volt = 1
            End If

            If cbx_Hz.SelectedIndex = -1 Then
                Pic_freq.Image = My.Resources.warning1
                cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
                Set_Freq = 0
            Else
                Pic_freq.Image = My.Resources.Vstate
                cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
                Set_Freq = 1
            End If

            If cbx_Alimentazione.SelectedIndex = -1 Then
                Pic_alimentazione.Image = My.Resources.warning1
                cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
                Set_Alim = 0
            Else
                Pic_alimentazione.Image = My.Resources.Vstate
                cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
                Set_Alim = 1
            End If

            If blocca_controllo_vip = 1 Then
                SbloccaVD1()
            End If

        End If
    End Sub

    Private Sub tbx_Descrizione_TextChanged(sender As Object, e As EventArgs) Handles tbx_Descrizione.TextChanged



        If tbx_Cliente.Text = "" Then
            Pic_client.Image = My.Resources.warning1
            tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
            Set_client = 0
        Else
            Pic_client.Image = My.Resources.Vstate
            tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
            Set_client = 1
        End If

        If tbx_Portata.Text = "" Then
            Pic_Portata.Image = My.Resources.warning1
            tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Portata = 0
        Else
            Pic_Portata.Image = My.Resources.Vstate
            tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Portata = 1
        End If

        If tbx_Pressione.Text = "" Then
            Pic_pressione.Image = My.Resources.warning1
            tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Pressione = 0
        Else
            Pic_pressione.Image = My.Resources.Vstate
            tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Pressione = 1
        End If

        If cbx_Direzione.SelectedIndex = -1 Then
            Pic_direzione.Image = My.Resources.warning1
            cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_DirFlusso = 0
        Else
            Pic_direzione.Image = My.Resources.Vstate
            cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_DirFlusso = 1
        End If


        If cbx_ApplicazioneFan.SelectedIndex = -1 Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If

        If tbx_TemperaturaMinAmb.Text = "" Then
            Pic_min.Image = My.Resources.warning1
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Min = 0
        Else
            Pic_min.Image = My.Resources.Vstate
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Min = 1
        End If

        If tbx_TemperaturaMaxAmb.Text = "" Then
            Pic_max.Image = My.Resources.warning1
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Max = 0
        Else
            Pic_max.Image = My.Resources.Vstate
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Max = 1
        End If

        If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
            Pic_richiesta.Image = My.Resources.warning1
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoRichiesta = 0
        Else
            Pic_richiesta.Image = My.Resources.Vstate
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoRichiesta = 1
        End If

        If tbx_Quantita.Text = "" Then
            Pic_Qtita.Image = My.Resources.warning1
            tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Quantita = 0
        Else
            Pic_Qtita.Image = My.Resources.Vstate
            tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Quantita = 1
        End If

        If tbx_Riferimento.Text = "" Then
            Pic_Rif.Image = My.Resources.warning1
            tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Rif = 0
        Else
            Pic_Rif.Image = My.Resources.Vstate
            tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Rif = 1
        End If


        If tbx_Descrizione.Text = "" Then ''
            Pic_descrizione.Image = My.Resources.warning1
            tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Descrizione = 0
        Else
            Pic_descrizione.Image = My.Resources.Vstate
            tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Descrizione = 1
        End If

        If cbx_TipoMotore.SelectedIndex = -1 Then
            Pic_TipoMot.Image = My.Resources.warning1
            cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoMot = 0
        Else
            Pic_TipoMot.Image = My.Resources.Vstate
            cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoMot = 1
        End If

        If cbx_Volt.SelectedIndex = -1 Then
            Pic_Tensione.Image = My.Resources.warning1
            cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Volt = 0
        Else
            Pic_Tensione.Image = My.Resources.Vstate
            cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Volt = 1
        End If

        If cbx_Hz.SelectedIndex = -1 Then
            Pic_freq.Image = My.Resources.warning1
            cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Freq = 0
        Else
            Pic_freq.Image = My.Resources.Vstate
            cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Freq = 1
        End If

        If cbx_Alimentazione.SelectedIndex = -1 Then
            Pic_alimentazione.Image = My.Resources.warning1
            cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Alim = 0
        Else
            Pic_alimentazione.Image = My.Resources.Vstate
            cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Alim = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If

    End Sub

    Private Sub tbx_Quantita_TextChanged(sender As Object, e As EventArgs) Handles tbx_Quantita.TextChanged



        If tbx_Cliente.Text = "" Then
            Pic_client.Image = My.Resources.warning1
            tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
            Set_client = 0
        Else
            Pic_client.Image = My.Resources.Vstate
            tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
            Set_client = 1
        End If

        If tbx_Portata.Text = "" Then
            Pic_Portata.Image = My.Resources.warning1
            tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Portata = 0
        Else
            Pic_Portata.Image = My.Resources.Vstate
            tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Portata = 1
        End If

        If tbx_Pressione.Text = "" Then
            Pic_pressione.Image = My.Resources.warning1
            tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Pressione = 0
        Else
            Pic_pressione.Image = My.Resources.Vstate
            tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Pressione = 1
        End If

        If cbx_Direzione.SelectedIndex = -1 Then
            Pic_direzione.Image = My.Resources.warning1
            cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_DirFlusso = 0
        Else
            Pic_direzione.Image = My.Resources.Vstate
            cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_DirFlusso = 1
        End If


        If cbx_ApplicazioneFan.SelectedIndex = -1 Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If

        If tbx_TemperaturaMinAmb.Text = "" Then
            Pic_min.Image = My.Resources.warning1
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Min = 0
        Else
            Pic_min.Image = My.Resources.Vstate
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Min = 1
        End If

        If tbx_TemperaturaMaxAmb.Text = "" Then
            Pic_max.Image = My.Resources.warning1
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Max = 0
        Else
            Pic_max.Image = My.Resources.Vstate
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Max = 1
        End If


        If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
            Pic_richiesta.Image = My.Resources.warning1
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoRichiesta = 0
        Else
            Pic_richiesta.Image = My.Resources.Vstate
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoRichiesta = 1
        End If

        If tbx_Quantita.Text = "" Then
            Pic_Qtita.Image = My.Resources.warning1
            tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Quantita = 0
        Else
            Pic_Qtita.Image = My.Resources.Vstate
            tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Quantita = 1
        End If

        If tbx_Riferimento.Text = "" Then
            Pic_Rif.Image = My.Resources.warning1
            tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Rif = 0
        Else
            Pic_Rif.Image = My.Resources.Vstate
            tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Rif = 1
        End If


        If tbx_Descrizione.Text = "" Then ''
            Pic_descrizione.Image = My.Resources.warning1
            tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Descrizione = 0
        Else
            Pic_descrizione.Image = My.Resources.Vstate
            tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Descrizione = 1
        End If

        If cbx_TipoMotore.SelectedIndex = -1 Then
            Pic_TipoMot.Image = My.Resources.warning1
            cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoMot = 0
        Else
            Pic_TipoMot.Image = My.Resources.Vstate
            cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoMot = 1
        End If

        If cbx_Volt.SelectedIndex = -1 Then
            Pic_Tensione.Image = My.Resources.warning1
            cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Volt = 0
        Else
            Pic_Tensione.Image = My.Resources.Vstate
            cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Volt = 1
        End If

        If cbx_Hz.SelectedIndex = -1 Then
            Pic_freq.Image = My.Resources.warning1
            cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Freq = 0
        Else
            Pic_freq.Image = My.Resources.Vstate
            cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Freq = 1
        End If

        If cbx_Alimentazione.SelectedIndex = -1 Then
            Pic_alimentazione.Image = My.Resources.warning1
            cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Alim = 0
        Else
            Pic_alimentazione.Image = My.Resources.Vstate
            cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Alim = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If

    End Sub

    Private Sub tbx_Rif_TextChanged(sender As Object, e As EventArgs) Handles tbx_Riferimento.TextChanged



        If tbx_Cliente.Text = "" Then
            Pic_client.Image = My.Resources.warning1
            tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
            Set_client = 0
        Else
            Pic_client.Image = My.Resources.Vstate
            tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
            Set_client = 1
        End If

        If tbx_Portata.Text = "" Then
            Pic_Portata.Image = My.Resources.warning1
            tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Portata = 0
        Else
            Pic_Portata.Image = My.Resources.Vstate
            tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Portata = 1
        End If

        If tbx_Pressione.Text = "" Then
            Pic_pressione.Image = My.Resources.warning1
            tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Pressione = 0
        Else
            Pic_pressione.Image = My.Resources.Vstate
            tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Pressione = 1
        End If

        If cbx_Direzione.SelectedIndex = -1 Then
            Pic_direzione.Image = My.Resources.warning1
            cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_DirFlusso = 0
        Else
            Pic_direzione.Image = My.Resources.Vstate
            cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_DirFlusso = 1
        End If


        If cbx_ApplicazioneFan.SelectedIndex = -1 Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If

        If tbx_TemperaturaMinAmb.Text = "" Then
            Pic_min.Image = My.Resources.warning1
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Min = 0
        Else
            Pic_min.Image = My.Resources.Vstate
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Min = 1
        End If

        If tbx_TemperaturaMaxAmb.Text = "" Then
            Pic_max.Image = My.Resources.warning1
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Max = 0
        Else
            Pic_max.Image = My.Resources.Vstate
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Max = 1
        End If

        If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
            Pic_richiesta.Image = My.Resources.warning1
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoRichiesta = 0
        Else
            Pic_richiesta.Image = My.Resources.Vstate
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoRichiesta = 1
        End If

        If tbx_Quantita.Text = "" Then
            Pic_Qtita.Image = My.Resources.warning1
            tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Quantita = 0
        Else
            Pic_Qtita.Image = My.Resources.Vstate
            tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Quantita = 1
        End If

        If tbx_Riferimento.Text = "" Then
            Pic_Rif.Image = My.Resources.warning1
            tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Rif = 0
        Else
            Pic_Rif.Image = My.Resources.Vstate
            tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Rif = 1
        End If


        If tbx_Descrizione.Text = "" Then ''
            Pic_descrizione.Image = My.Resources.warning1
            tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Descrizione = 0
        Else
            Pic_descrizione.Image = My.Resources.Vstate
            tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Descrizione = 1
        End If

        If cbx_TipoMotore.SelectedIndex = -1 Then
            Pic_TipoMot.Image = My.Resources.warning1
            cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoMot = 0
        Else
            Pic_TipoMot.Image = My.Resources.Vstate
            cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoMot = 1
        End If

        If cbx_Volt.SelectedIndex = -1 Then
            Pic_Tensione.Image = My.Resources.warning1
            cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Volt = 0
        Else
            Pic_Tensione.Image = My.Resources.Vstate
            cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Volt = 1
        End If

        If cbx_Hz.SelectedIndex = -1 Then
            Pic_freq.Image = My.Resources.warning1
            cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Freq = 0
        Else
            Pic_freq.Image = My.Resources.Vstate
            cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Freq = 1
        End If

        If cbx_Alimentazione.SelectedIndex = -1 Then
            Pic_alimentazione.Image = My.Resources.warning1
            cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Alim = 0
        Else
            Pic_alimentazione.Image = My.Resources.Vstate
            cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Alim = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If

    End Sub

    Private Sub cbx_TipoMotore_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_TipoMotore.SelectedIndexChanged



        If tbx_Cliente.Text = "" Then
            Pic_client.Image = My.Resources.warning1
            tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
            Set_client = 0
        Else
            Pic_client.Image = My.Resources.Vstate
            tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
            Set_client = 1
        End If

        If tbx_Portata.Text = "" Then
            Pic_Portata.Image = My.Resources.warning1
            tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Portata = 0
        Else
            Pic_Portata.Image = My.Resources.Vstate
            tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Portata = 1
        End If

        If tbx_Pressione.Text = "" Then
            Pic_pressione.Image = My.Resources.warning1
            tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Pressione = 0
        Else
            Pic_pressione.Image = My.Resources.Vstate
            tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Pressione = 1
        End If

        If cbx_Direzione.SelectedIndex = -1 Then
            Pic_direzione.Image = My.Resources.warning1
            cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_DirFlusso = 0
        Else
            Pic_direzione.Image = My.Resources.Vstate
            cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_DirFlusso = 1
        End If


        If cbx_ApplicazioneFan.SelectedIndex = -1 Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If

        If tbx_TemperaturaMinAmb.Text = "" Then
            Pic_min.Image = My.Resources.warning1
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Min = 0
        Else
            Pic_min.Image = My.Resources.Vstate
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Min = 1
        End If

        If tbx_TemperaturaMaxAmb.Text = "" Then
            Pic_max.Image = My.Resources.warning1
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Max = 0
        Else
            Pic_max.Image = My.Resources.Vstate
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Max = 1
        End If

        If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
            Pic_richiesta.Image = My.Resources.warning1
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoRichiesta = 0
        Else
            Pic_richiesta.Image = My.Resources.Vstate
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoRichiesta = 1
        End If

        If tbx_Quantita.Text = "" Then
            Pic_Qtita.Image = My.Resources.warning1
            tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Quantita = 0
        Else
            Pic_Qtita.Image = My.Resources.Vstate
            tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Quantita = 1
        End If

        If tbx_Riferimento.Text = "" Then
            Pic_Rif.Image = My.Resources.warning1
            tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Rif = 0
        Else
            Pic_Rif.Image = My.Resources.Vstate
            tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Rif = 1
        End If


        If tbx_Descrizione.Text = "" Then ''
            Pic_descrizione.Image = My.Resources.warning1
            tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Descrizione = 0
        Else
            Pic_descrizione.Image = My.Resources.Vstate
            tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Descrizione = 1
        End If

        If cbx_TipoMotore.SelectedIndex = -1 Then
            Pic_TipoMot.Image = My.Resources.warning1
            cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoMot = 0
        Else
            Pic_TipoMot.Image = My.Resources.Vstate
            cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoMot = 1
        End If

        If cbx_Volt.SelectedIndex = -1 Then
            Pic_Tensione.Image = My.Resources.warning1
            cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Volt = 0
        Else
            Pic_Tensione.Image = My.Resources.Vstate
            cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Volt = 1
        End If

        If cbx_Hz.SelectedIndex = -1 Then
            Pic_freq.Image = My.Resources.warning1
            cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Freq = 0
        Else
            Pic_freq.Image = My.Resources.Vstate
            cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Freq = 1
        End If

        If cbx_Alimentazione.SelectedIndex = -1 Then
            Pic_alimentazione.Image = My.Resources.warning1
            cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Alim = 0
        Else
            Pic_alimentazione.Image = My.Resources.Vstate
            cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Alim = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If


        hide_fastPJ()

    End Sub

    Private Sub cbx_Volt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_Volt.SelectedIndexChanged



        If tbx_Cliente.Text = "" Then
            Pic_client.Image = My.Resources.warning1
            tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
            Set_client = 0
        Else
            Pic_client.Image = My.Resources.Vstate
            tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
            Set_client = 1
        End If

        If tbx_Portata.Text = "" Then
            Pic_Portata.Image = My.Resources.warning1
            tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Portata = 0
        Else
            Pic_Portata.Image = My.Resources.Vstate
            tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Portata = 1
        End If

        If tbx_Pressione.Text = "" Then
            Pic_pressione.Image = My.Resources.warning1
            tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Pressione = 0
        Else
            Pic_pressione.Image = My.Resources.Vstate
            tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Pressione = 1
        End If

        If cbx_Direzione.SelectedIndex = -1 Then
            Pic_direzione.Image = My.Resources.warning1
            cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_DirFlusso = 0
        Else
            Pic_direzione.Image = My.Resources.Vstate
            cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_DirFlusso = 1
        End If


        If cbx_ApplicazioneFan.SelectedIndex = -1 Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If

        If tbx_TemperaturaMinAmb.Text = "" Then
            Pic_min.Image = My.Resources.warning1
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Min = 0
        Else
            Pic_min.Image = My.Resources.Vstate
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Min = 1
        End If

        If tbx_TemperaturaMaxAmb.Text = "" Then
            Pic_max.Image = My.Resources.warning1
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Max = 0
        Else
            Pic_max.Image = My.Resources.Vstate
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Max = 1
        End If

        If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
            Pic_richiesta.Image = My.Resources.warning1
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoRichiesta = 0
        Else
            Pic_richiesta.Image = My.Resources.Vstate
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoRichiesta = 1
        End If

        If tbx_Quantita.Text = "" Then
            Pic_Qtita.Image = My.Resources.warning1
            tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Quantita = 0
        Else
            Pic_Qtita.Image = My.Resources.Vstate
            tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Quantita = 1
        End If

        If tbx_Riferimento.Text = "" Then
            Pic_Rif.Image = My.Resources.warning1
            tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Rif = 0
        Else
            Pic_Rif.Image = My.Resources.Vstate
            tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Rif = 1
        End If


        If tbx_Descrizione.Text = "" Then ''
            Pic_descrizione.Image = My.Resources.warning1
            tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Descrizione = 0
        Else
            Pic_descrizione.Image = My.Resources.Vstate
            tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Descrizione = 1
        End If

        If cbx_TipoMotore.SelectedIndex = -1 Then
            Pic_TipoMot.Image = My.Resources.warning1
            cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoMot = 0
        Else
            Pic_TipoMot.Image = My.Resources.Vstate
            cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoMot = 1
        End If

        If cbx_Volt.SelectedIndex = -1 Then
            Pic_Tensione.Image = My.Resources.warning1
            cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Volt = 0
        Else
            Pic_Tensione.Image = My.Resources.Vstate
            cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Volt = 1
        End If

        If cbx_Hz.SelectedIndex = -1 Then
            Pic_freq.Image = My.Resources.warning1
            cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Freq = 0
        Else
            Pic_freq.Image = My.Resources.Vstate
            cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Freq = 1
        End If

        If cbx_Alimentazione.SelectedIndex = -1 Then
            Pic_alimentazione.Image = My.Resources.warning1
            cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Alim = 0
        Else
            Pic_alimentazione.Image = My.Resources.Vstate
            cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Alim = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If

        hide_fastPJ()

    End Sub

    Private Sub cbx_Hz_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_Hz.SelectedIndexChanged

        funzione_tensione()
        Try
            funzione_cbx1(cbx_Volt, "cbx_Volt")
        Catch ex As Exception

        End Try


        If tbx_Cliente.Text = "" Then
            Pic_client.Image = My.Resources.warning1
            tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
            Set_client = 0
        Else
            Pic_client.Image = My.Resources.Vstate
            tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
            Set_client = 1
        End If

        If tbx_Portata.Text = "" Then
            Pic_Portata.Image = My.Resources.warning1
            tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Portata = 0
        Else
            Pic_Portata.Image = My.Resources.Vstate
            tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Portata = 1
        End If

        If tbx_Pressione.Text = "" Then
            Pic_pressione.Image = My.Resources.warning1
            tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Pressione = 0
        Else
            Pic_pressione.Image = My.Resources.Vstate
            tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Pressione = 1
        End If

        If cbx_Direzione.SelectedIndex = -1 Then
            Pic_direzione.Image = My.Resources.warning1
            cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_DirFlusso = 0
        Else
            Pic_direzione.Image = My.Resources.Vstate
            cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_DirFlusso = 1
        End If


        If cbx_ApplicazioneFan.SelectedIndex = -1 Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If

        If tbx_TemperaturaMinAmb.Text = "" Then
            Pic_min.Image = My.Resources.warning1
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Min = 0
        Else
            Pic_min.Image = My.Resources.Vstate
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Min = 1
        End If

        If tbx_TemperaturaMaxAmb.Text = "" Then
            Pic_max.Image = My.Resources.warning1
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Max = 0
        Else
            Pic_max.Image = My.Resources.Vstate
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Max = 1
        End If

        If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
            Pic_richiesta.Image = My.Resources.warning1
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoRichiesta = 0
        Else
            Pic_richiesta.Image = My.Resources.Vstate
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoRichiesta = 1
        End If

        If tbx_Quantita.Text = "" Then
            Pic_Qtita.Image = My.Resources.warning1
            tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Quantita = 0
        Else
            Pic_Qtita.Image = My.Resources.Vstate
            tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Quantita = 1
        End If

        If tbx_Riferimento.Text = "" Then
            Pic_Rif.Image = My.Resources.warning1
            tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Rif = 0
        Else
            Pic_Rif.Image = My.Resources.Vstate
            tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Rif = 1
        End If

        If tbx_Descrizione.Text = "" Then ''
            Pic_descrizione.Image = My.Resources.warning1
            tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Descrizione = 0
        Else
            Pic_descrizione.Image = My.Resources.Vstate
            tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Descrizione = 1
        End If

        If cbx_TipoMotore.SelectedIndex = -1 Then
            Pic_TipoMot.Image = My.Resources.warning1
            cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoMot = 0
        Else
            Pic_TipoMot.Image = My.Resources.Vstate
            cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoMot = 1
        End If

        If cbx_Volt.SelectedIndex = -1 Then
            Pic_Tensione.Image = My.Resources.warning1
            cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Volt = 0
        Else
            Pic_Tensione.Image = My.Resources.Vstate
            cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Volt = 1
        End If

        If cbx_Hz.SelectedIndex = -1 Then
            Pic_freq.Image = My.Resources.warning1
            cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Freq = 0
        Else
            Pic_freq.Image = My.Resources.Vstate
            cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Freq = 1
        End If

        If cbx_Alimentazione.SelectedIndex = -1 Then
            Pic_alimentazione.Image = My.Resources.warning1
            cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Alim = 0
        Else
            Pic_alimentazione.Image = My.Resources.Vstate
            cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Alim = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If

        hide_fastPJ()

    End Sub

    Private Sub cbx_Alimentazione_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_Alimentazione.SelectedIndexChanged

        funzione_tensione()
        Try
            funzione_cbx1(cbx_Volt, "cbx_Volt")
        Catch ex As Exception

        End Try


        If tbx_Cliente.Text = "" Then
            Pic_client.Image = My.Resources.warning1
            tbx_Cliente.BorderColor = Color.FromArgb(250, 117, 158)
            Set_client = 0
        Else
            Pic_client.Image = My.Resources.Vstate
            tbx_Cliente.BorderColor = Color.FromArgb(74, 231, 148)
            Set_client = 1
        End If

        If tbx_Portata.Text = "" Then
            Pic_Portata.Image = My.Resources.warning1
            tbx_Portata.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Portata = 0
        Else
            Pic_Portata.Image = My.Resources.Vstate
            tbx_Portata.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Portata = 1
        End If

        If tbx_Pressione.Text = "" Then
            Pic_pressione.Image = My.Resources.warning1
            tbx_Pressione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Pressione = 0
        Else
            Pic_pressione.Image = My.Resources.Vstate
            tbx_Pressione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Pressione = 1
        End If

        If cbx_Direzione.SelectedIndex = -1 Then
            Pic_direzione.Image = My.Resources.warning1
            cbx_Direzione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_DirFlusso = 0
        Else
            Pic_direzione.Image = My.Resources.Vstate
            cbx_Direzione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_DirFlusso = 1
        End If


        If cbx_ApplicazioneFan.SelectedIndex = -1 Then
            Pic_applicazione.Image = My.Resources.warning1
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(250, 117, 158)
            Set_applicazione = 0
        Else
            Pic_applicazione.Image = My.Resources.Vstate
            cbx_ApplicazioneFan.BorderColor = Color.FromArgb(74, 231, 148)
            Set_applicazione = 1
        End If

        If tbx_TemperaturaMinAmb.Text = "" Then
            Pic_min.Image = My.Resources.warning1
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Min = 0
        Else
            Pic_min.Image = My.Resources.Vstate
            tbx_TemperaturaMinAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Min = 1
        End If

        If tbx_TemperaturaMaxAmb.Text = "" Then
            Pic_max.Image = My.Resources.warning1
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Max = 0
        Else
            Pic_max.Image = My.Resources.Vstate
            tbx_TemperaturaMaxAmb.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Max = 1
        End If

        If cbx_TipoRichiesta.SelectedIndex = -1 And cbx_TipoRichiesta.SelectedItem <> Nothing Then
            Pic_richiesta.Image = My.Resources.warning1
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoRichiesta = 0
        Else
            Pic_richiesta.Image = My.Resources.Vstate
            cbx_TipoRichiesta.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoRichiesta = 1
        End If

        If tbx_Quantita.Text = "" Then
            Pic_Qtita.Image = My.Resources.warning1
            tbx_Quantita.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Quantita = 0
        Else
            Pic_Qtita.Image = My.Resources.Vstate
            tbx_Quantita.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Quantita = 1
        End If

        If tbx_Riferimento.Text = "" Then
            Pic_Rif.Image = My.Resources.warning1
            tbx_Riferimento.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Rif = 0
        Else
            Pic_Rif.Image = My.Resources.Vstate
            tbx_Riferimento.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Rif = 1
        End If

        If tbx_Descrizione.Text = "" Then ''
            Pic_descrizione.Image = My.Resources.warning1
            tbx_Descrizione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Descrizione = 0
        Else
            Pic_descrizione.Image = My.Resources.Vstate
            tbx_Descrizione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Descrizione = 1
        End If

        If cbx_TipoMotore.SelectedIndex = -1 Then
            Pic_TipoMot.Image = My.Resources.warning1
            cbx_TipoMotore.BorderColor = Color.FromArgb(250, 117, 158)
            Set_TipoMot = 0
        Else
            Pic_TipoMot.Image = My.Resources.Vstate
            cbx_TipoMotore.BorderColor = Color.FromArgb(74, 231, 148)
            Set_TipoMot = 1
        End If

        If cbx_Volt.SelectedIndex = -1 Then
            Pic_Tensione.Image = My.Resources.warning1
            cbx_Volt.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Volt = 0
        Else
            Pic_Tensione.Image = My.Resources.Vstate
            cbx_Volt.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Volt = 1
        End If

        If cbx_Hz.SelectedIndex = -1 Then
            Pic_freq.Image = My.Resources.warning1
            cbx_Hz.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Freq = 0
        Else
            Pic_freq.Image = My.Resources.Vstate
            cbx_Hz.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Freq = 1
        End If

        If cbx_Alimentazione.SelectedIndex = -1 Then
            Pic_alimentazione.Image = My.Resources.warning1
            cbx_Alimentazione.BorderColor = Color.FromArgb(250, 117, 158)
            Set_Alim = 0
        Else
            Pic_alimentazione.Image = My.Resources.Vstate
            cbx_Alimentazione.BorderColor = Color.FromArgb(74, 231, 148)
            Set_Alim = 1
        End If

        If blocca_controllo_vip = 1 Then
            SbloccaVD1()
        End If

        hide_fastPJ()

    End Sub

    Private Sub check_ForiDE_CheckedChanged(sender As Object, e As EventArgs) Handles check_ForiDE.CheckedChanged

        If check_ForiNDE.Checked = True Then

            check_ForiNDE.Checked = False

        End If

    End Sub

    Private Sub check_ForiNDE_CheckedChanged(sender As Object, e As EventArgs) Handles check_ForiNDE.CheckedChanged

        If check_ForiDE.Checked = True Then

            check_ForiDE.Checked = False

        End If

    End Sub




    Public Sub aggiorna_note()


        Dim view_progetto As New DataView(tables2(0))

        Dim add_count As Integer = 0

        'identifico il numero di revisione per ogni progetto
        For i As Integer = 0 To view_progetto.Count - 1

            If view_progetto(i).Item("ProgettoPJ").ToString() = prog_rev(posizione_progetto, 0) Then

                ID_note_vect(add_count) = view_progetto(i).Item("ID").ToString
                Data_note_vect(add_count) = view_progetto(i).Item("DataPJ").ToString
                User_note_vect(add_count) = view_progetto(i).Item("UtentePJ").ToString
                Rev_note_vect(add_count) = view_progetto(i).Item("RevisionePJ").ToString
                note_vect(add_count) = view_progetto(i).Item("NotaPJ").ToString

                add_count = add_count + 1

            End If
        Next



        Dim buffer_Data_note_vect(numero_note_totali) As String
        Dim buffer_ID_note_vect(numero_note_totali) As String
        Dim buffer_User_note_vect(numero_note_totali) As String
        Dim buffer_Rev_note_vect(numero_note_totali) As String
        Dim buffer_note_vect(numero_note_totali) As String

        Dim posizioni_note_vect(numero_note_totali) As String


        Dim ID_min As String = ID_note_vect(0)


        'Codice di riordino note per Data
        For i = 0 To add_count - 1

            If (ID_min) > (ID_note_vect(i)) Then

                ID_min = ID_note_vect(i)

            End If

        Next


        '-----------------------------------------------------Riordino del vettore delle note dall'ID piu piccolo a quello piu grande---------------------------------------------------------------------------------
        Dim trovato1 As Integer

        For j = 0 To add_count - 1

            trovato1 = 0
            'Codice di riordino note per Data
            For i = 0 To add_count - 1

                If (CInt(ID_min) >= CInt(ID_note_vect(i))) And ID_note_vect(i) <> "100000" Then

                    If trovato1 = 1 Then
                        j = j + 1
                    End If

                    buffer_ID_note_vect(j) = ID_min
                    ID_note_vect(i) = "100000"

                    posizioni_note_vect(j) = i

                    trovato1 = 1


                End If

                If i = add_count - 1 Then

                    ID_min = ID_note_vect(0)

                    'Codice di riordino note per Data
                    For k = 0 To add_count - 1

                        If CInt(ID_min) >= CInt(ID_note_vect(k)) And CInt(ID_note_vect(k) <> "100000") Then

                            ID_min = ID_note_vect(k)

                        End If

                    Next

                End If

            Next

        Next
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        'RIORDINO I VETTORI
        For i = 0 To add_count - 1
            buffer_Data_note_vect(i) = Data_note_vect(posizioni_note_vect(i))
            buffer_User_note_vect(i) = User_note_vect(posizioni_note_vect(i))
            buffer_Rev_note_vect(i) = Rev_note_vect(posizioni_note_vect(i))
            buffer_note_vect(i) = note_vect(posizioni_note_vect(i))
        Next



        'INSERISCO LE NOTE NEL DATAGRID
        For i = 0 To add_count - 1

            Guna2DataGridView1.Rows.Add()
            ID_note_vect(i) = buffer_ID_note_vect(i)
            Guna2DataGridView1.Rows(i).Cells(0).Value = buffer_Data_note_vect(i)
            Guna2DataGridView1.Rows(i).Cells(1).Value = buffer_User_note_vect(i)
            Guna2DataGridView1.Rows(i).Cells(2).Value = buffer_Rev_note_vect(i)

            note_vect(i) = buffer_note_vect(i)


            '--------------------blocco traduzione da RTF A TEXT-----------------------------
            Dim bridge_string As String = buffer_note_vect(i)

            Try
                RichTextBox1.Rtf = bridge_string
            Catch ex As Exception
                RichTextBox1.Text = bridge_string
            End Try

            bridge_string = RichTextBox1.Text
            Guna2DataGridView1.Rows(i).Cells(3).Value = bridge_string
            '--------------------------------------------------------------------------------

        Next





        Guna2DataGridView1.Rows.Add()
        Guna2DataGridView1.Rows(add_count).Cells(0).Value = Date.Today.Day & "/" & Date.Today.Month & "/" & Date.Today.Year
        Guna2DataGridView1.Rows(add_count).Cells(1).Value = nome_macchina


    End Sub





    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click


        Process.Start("explorer.exe", folders_directory & "\" & prog_rev(posizione_progetto, 0) & "\Rev" & cbx_Revisione.SelectedIndex)


    End Sub


    Public Sub SbloccaVD1()


        If jump_tipo_richiesta = 0 Then


            If cbx_TipoRichiesta.SelectedIndex = 0 Or fast_PJ = 1 Then


                If Set_client * Set_TipoRichiesta * Set_Descrizione * Set_Quantita Then
                    Try

                        If new_PJVar = 0 Then
                            btn_Datasheet.Visible = True
                        End If



                        If cbx_TipoRichiesta.SelectedIndex = 0 Or fast_PJ = 1 Then
                            btn_VipDesigner.Visible = False
                        Else
                            btn_VipDesigner.Visible = True
                        End If




                        Guna2Button2.Visible = True
                        All_Show()
                        btn_VipDesigner.BringToFront()
                        btn_Datasheet.BringToFront()
                    Catch ex As Exception

                    End Try


                Else

                    btn_Datasheet.Visible = False
                    btn_VipDesigner.Visible = False
                    Guna2Button2.Visible = False

                    'All_Hide()
                End If



            Else

                If Set_client * Set_TipoRichiesta * Set_Descrizione * Set_Quantita * Set_Rif * Set_Portata * Set_Pressione * Set_DirFlusso * Set_applicazione * Set_Min * Set_Max * Set_TipoMot * Set_Alim * Set_Volt * Set_Freq * Set_TipoFan * Set_Atex_Safe = 1 And new_PJVar = 1 Then 'Nuova Progettazione


                    Guna2Button2.Visible = True
                    All_Show()
                    btn_VipDesigner.BringToFront()
                    btn_Datasheet.BringToFront()


                    'SCRITTA IN ALTO QUANDO COMPLETO TUTTI I CAMPI RICHIESTI
                    Label23.Visible = False
                    ParGen.CustomBorderColor = Color.FromArgb(213, 218, 223)
                    ParGen.Text = prog_rev(posizione_progetto, 0) & " del " & data_progetto


                ElseIf (Set_client * Set_TipoRichiesta * Set_Descrizione * Set_Rif * Set_Quantita * Set_Portata * Set_Pressione * Set_DirFlusso * Set_applicazione * Set_Min * Set_Max * Set_TipoMot * Set_Alim * Set_Volt * Set_Freq * Set_TipoFan * Set_Atex_Safe = 1 And new_PJVar = 0)  Then 'Tutto compilato

                    check_RevCliente.Visible = True
                    Guna2Button1.Visible = True
                    Guna2Button2.Visible = True
                    Guna2Button3.Visible = True
                    cbx_Revisione.Visible = True

                    If cbx_TipoRichiesta.SelectedIndex = 0 Then
                        btn_VipDesigner.Visible = False
                    Else
                        btn_VipDesigner.Visible = True
                    End If


                    btn_Datasheet.Visible = True
                    Guna2Button2.Visible = True
                    ButtonCert.Visible = True
                    ButtonConn.Visible = True
                    ButtonAcc.Visible = True


                    All_Show()
                    btn_VipDesigner.BringToFront()
                    btn_Datasheet.BringToFront()


                    'SCRITTA IN ALTO QUANDO COMPLETO TUTTI I CAMPI RICHIESTI
                    Label23.Visible = False
                    ParGen.CustomBorderColor = Color.FromArgb(213, 218, 223)
                    ParGen.Text = prog_rev(posizione_progetto, 0) & " del " & data_progetto

                Else

                    check_RevCliente.Visible = False
                    btn_VipDesigner.Visible = False
                    btn_Datasheet.Visible = False
                    Guna2Button2.Visible = False
                    Guna2Button1.Visible = False
                    Guna2Button3.Visible = False
                    cbx_Revisione.Visible = False
                    ButtonCert.Visible = False
                    ButtonConn.Visible = False
                    ButtonAcc.Visible = False


                    'SCRITTA IN ALTO DI BLOCCO PROGETTAZIONE
                    ParGen.CustomBorderColor = Color.Red
                    Label23.Visible = True
                    Label23.Text = "INSERIRE I PARAMETRI RICHIESTI DAL CLIENTE"

                    'All_Hide()
                End If




            End If
        End If

    End Sub





    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick


        time_val1 = time_val1 + 1


        'controllo lo stato ogni 10 sec
        If time_val1 = 25 Then

            'controllo se lo stato di connessione e' ancora a 1 --> se il valore e' diverso da 1 significa che il VipDesigner e' stato chiuso --> sblocca la progettazione
            Lettura_riga1(cbx_Revisione.SelectedIndex)

            If lockPJ = 0 Then
                Timer2.Stop()
                Me.Enabled = True
                ConnStateUser1(1)
                Form1.btnParametri.PerformClick()
            End If

            time_val1 = 0

        End If


    End Sub



    Public Sub salva_open_progettoVD1()

        save_session(0) = "Progettazione_" & prog_rev(posizione_progetto, 0) & "__" & cbx_Revisione.SelectedIndex

        'Tipo ventilatore
        save_session(1) = cbx_TipoFan.SelectedIndex '1 = PlugFan; 0 = Duct Fan; 2 = Axial Fan with Noozle; 3 = New Fan



        If Rad_Safe.Checked = True Then
            save_session(2) = 0 '0 = SafeArea; 1 = Atex
        Else
            save_session(2) = 1 '0 = SafeArea; 1 = Atex
        End If

        save_session(3) = cbx_ApplicazioneFan.SelectedIndex '0 = Industrial; 1 = OffShore; 2 = SeaShore;


        save_session(4) = tbx_Portata.Text
        save_session(5) = tbx_Pressione.Text
        save_session(6) = tbx_Temperatura.Text
        save_session(7) = tbx_Altitude.Text
        save_session(8) = tbx_Relative.Text



        save_session(9) = cbx_AirflowTarget.SelectedItem
        save_session(10) = tbx_TemperaturaMinAmb.Text
        save_session(11) = tbx_TemperaturaMaxAmb.Text
        save_session(12) = cbx_Hz.Text
        save_session(13) = cbx_Poli.Text
        save_session(14) = tbx_Diametro.Text


        save_session(15) = ""
        save_session(16) = ""
        save_session(17) = cbx_AtexProtezione.Text
        save_session(18) = cbx_AtexCustodia.Text
        save_session(19) = cbx_AtexClasseTemperatura.Text
        save_session(20) = cbx_AtexCategoria.Text



        '--------------------PARTE MOTORE---------------------------

        save_session(21) = cbx_Alimentazione.SelectedIndex
        save_session(22) = cbx_Volt.SelectedItem
        save_session(23) = cbx_materiale.SelectedIndex
        save_session(24) = cbx_cooling.SelectedIndex
        save_session(25) = cbx_IEX.SelectedItem
        save_session(26) = cbx_ClasseIsolamento.SelectedIndex
        save_session(27) = cbx_superficiale.SelectedIndex
        save_session(28) = cbx_colore.SelectedItem
        save_session(29) = cbx_corrosione.SelectedItem
        save_session(70) = cbx_IP.SelectedItem

        If check_PTC.Checked = True Then
            save_session(30) = 0
        Else
            save_session(30) = 1
        End If

        If check_PTO.Checked = True Then
            save_session(31) = 0
        Else
            save_session(31) = 1
        End If

        If check_VitiMotoreInox.Checked = True Then
            save_session(32) = 0
        Else
            save_session(32) = 1
        End If

        If check_CappelloParapioggia.Checked = True Then
            save_session(33) = 0
        Else
            save_session(33) = 1
        End If

        If check_DiscoParapioggia.Checked = True Then
            save_session(34) = 0
        Else
            save_session(34) = 1
        End If

        If check_ForiDE.Checked = True Or check_ForiNDE.Checked = True Or check_Forilaterali.Checked = True Then
            save_session(35) = 0
        Else
            save_session(35) = 1
        End If

        If check_TropicalizzazioneRot.Checked = True Then
            save_session(36) = 0
        Else
            save_session(36) = 1
        End If

        If check_TropicalizzazioneStat.Checked = True Then
            save_session(37) = 0
        Else
            save_session(37) = 1
        End If

        If cbx_MaterialeScudi.SelectedIndex = 0 Then
            save_session(38) = 1
        Else
            save_session(38) = 0
        End If

        If check_verniciatura.Checked = True Then
            save_session(39) = 0
        Else
            save_session(39) = 1
        End If

        If check_ReteLatoMotore = 1 Then
            save_session(40) = 0
        Else
            save_session(40) = 1
        End If

        If check_ReteLatoVentola = 1 Then
            save_session(41) = 0
        Else
            save_session(41) = 1
        End If

        If check_Scaldiglie.Checked = True Then
            save_session(42) = 0
        Else
            save_session(42) = 1
        End If

        If check_ULCSA = 1 Then
            save_session(43) = 0
        Else
            save_session(43) = 1
        End If

        If check_IECEX = 1 Then
            save_session(44) = 0
        Else
            save_session(44) = 1
        End If

        If check_EAC = 1 Then
            save_session(45) = 0
        Else
            save_session(45) = 1
        End If

        If check_CUTR = 1 Then
            save_session(46) = 0
        Else
            save_session(46) = 1
        End If

        If check_NEMA = 1 Then
            save_session(47) = 0
        Else
            save_session(47) = 1
        End If


        '--------------------PARTE VENTOLA---------------------------

        save_session(48) = cbx_MaterialeVentola.SelectedItem
        save_session(49) = cbx_TrattamentoSupVentola.SelectedItem
        save_session(50) = cbx_ColoreVentola.SelectedItem
        save_session(51) = cbx_ClasseCorrVentola.SelectedItem
        save_session(52) = cbx_MaterialeVitiVentola.SelectedItem

        '--------------------MOZZO---------------------------
        save_session(53) = cbx_MaterialeMozzo.SelectedItem
        save_session(54) = cbx_TrattamentoMozzo.SelectedItem
        save_session(55) = cbx_ColoreMozzo.SelectedItem
        save_session(56) = cbx_ClasseCorrMozzo.SelectedItem

        '--------------------PARTE RAGGERA---------------------------

        save_session(72) = cbx_MaterialeRaggera.SelectedItem
        save_session(73) = cbx_TrattamentoRaggera.SelectedItem
        save_session(74) = cbx_ColoreRaggera.SelectedItem
        save_session(75) = cbx_ClasseCorrRaggera.SelectedItem

        '--------------------PARTE SUPPORTO---------------------------

        save_session(57) = cbx_TipoSupporto.SelectedItem
        save_session(58) = cbx_MaterialeSupporto.SelectedItem
        save_session(59) = cbx_TrattamentoSupporto.SelectedItem
        save_session(60) = cbx_ColoreSupporto.SelectedItem
        save_session(61) = cbx_ClasseSupporto.SelectedItem

        '--------------------PARTE CONVOGLIATORE---------------------------

        save_session(62) = cbx_TipoConvogliatore.SelectedItem
        save_session(63) = cbx_MaterialeConvogliatore.SelectedItem
        save_session(64) = cbx_TrattamentoConvogliatore.SelectedItem
        save_session(65) = cbx_ColoreConvogliatore.SelectedItem
        save_session(66) = cbx_ClasseConvogliatore.SelectedItem


        If check_Inverter.Checked = True Then
            save_session(67) = 0
        Else
            save_session(67) = 1
        End If

        If check_taglio.Checked = True Then
            save_session(78) = 0
        Else
            save_session(78) = 1
        End If

        save_session(68) = cbx_ConfigurazioneFan.SelectedIndex

        save_session(69) = cbx_Direzione.SelectedIndex

        save_session(71) = cbx_DispMotore.SelectedIndex

        save_session(76) = cbx_DispMotore.SelectedIndex
        save_session(77) = cbx_DispMotore.SelectedIndex

        'Codice di salvataggio progetto
        Dim str_save As String

        For i = 0 To 78
            str_save = str_save & save_session(i) + Environment.NewLine
        Next


        Dim saveFileDialog1 As SaveFileDialog = New SaveFileDialog()

        saveFileDialog1.Filter = "vip files (*.vip)|*.vip*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True


        Dim ore As String = Now.ToShortTimeString.Substring(0, 2)
        Dim min As String = Now.ToShortTimeString.Substring(3, 2)

        Dim giorno As String = Now.ToShortDateString.Substring(0, 2)
        Dim mese As String = Now.ToShortDateString.Substring(3, 2)
        Dim anno As String = Now.ToShortDateString.Substring(6, 2)


        saveFileDialog1.FileName = "Progetto" & "__" & ore & "_" & min & "___" & giorno & "_" & mese & "_" & anno & "_" & "Q" & tbx_Portata.Text & "DP" & tbx_Pressione.Text & ".vip"

        saveFileDialog1.DefaultExt = "vip"
        saveFileDialog1.AddExtension = True

        Dim nome_file_sv As String = prog_rev(posizione_progetto, 0) & "_Rev" & cbx_Revisione.SelectedIndex & "__" & ore & "_" & min & "___" & giorno & "_" & mese & "_" & anno & "_" & "Q" & tbx_Portata.Text & "DP" & tbx_Pressione.Text & ".vip"
        Dim path_save As String = folders_directory & "\" & prog_rev(posizione_progetto, 0) & "\Rev" & cbx_Revisione.SelectedIndex & "\" & nome_file_sv

        File.WriteAllText(path_save, str_save)



        'Apre il progetto appena salvato
        Dim proc As New System.Diagnostics.Process()
        proc = Process.Start(path_save, "")

    End Sub




    Public Sub compila_cbx_ClinteRev0()


        rad_true = 0
        Dim eccezione_ConfigurazioneFan As String


        For i = 0 To Numero_tabelle

            Select Case SchemaDB(i)


                '------------------------Tabella Generale------------------------------
                Case "cbx_TipoFan"
                    funzione_cbx1(cbx_TipoFan, SchemaDB(i))
                Case "cbx_Direzione"
                    funzione_cbx1(cbx_Direzione, SchemaDB(i))
                Case "cbx_ConfigurazioneFan"
                    'funzione_cbx1(cbx_ConfigurazioneFan, SchemaDB(i))
                Case "cbx_ApplicazioneFan"
                    funzione_cbx1(cbx_ApplicazioneFan, SchemaDB(i))
                Case "cbx_rumore"
                    'funzione_cbx1(cbx_rumore, SchemaDB(i))
                Case "cbx_AtexProtezione"
                    funzione_cbx1(cbx_AtexProtezione, SchemaDB(i))
                Case "cbx_AtexCustodia"
                    funzione_cbx1(cbx_AtexCustodia, SchemaDB(i))
                Case "cbx_AtexCategoria"
                    funzione_cbx1(cbx_AtexCategoria, SchemaDB(i))
                Case "cbx_AtexClasseTemperatura"
                    funzione_cbx1(cbx_AtexClasseTemperatura, SchemaDB(i))
                Case "cbx_AirflowTarget"
                    funzione_cbx1(cbx_AirflowTarget, SchemaDB(i))
                    '-------------------------------------------------------------------

                    '------------------------Motore Elettrico------------------------------
                Case "cbx_TipoMotore"
                    funzione_cbx1(cbx_TipoMotore, SchemaDB(i))
                Case "cbx_Alimentazione"
                    funzione_cbx1(cbx_Alimentazione, SchemaDB(i))
                Case "cbx_Volt"
                    'ATTENZIONE cbx_Volt DEVE essere aggiornata dopo cbx_Alimentazione e cbx_Hz
                    funzione_tensione()
                    funzione_cbx1(cbx_Volt, SchemaDB(i))
                Case "cbx_Hz"
                    funzione_cbx1(cbx_Hz, SchemaDB(i))
                Case "cbx_Poli"
                    funzione_cbx1(cbx_Poli, SchemaDB(i))
                Case "cbx_materiale"
                    funzione_cbx1(cbx_materiale, SchemaDB(i))
                Case "cbx_cooling"
                    funzione_cbx1(cbx_cooling, SchemaDB(i))
                Case "cbx_IEX"
                    funzione_cbx1(cbx_IEX, SchemaDB(i))
                Case "cbx_ClasseIsolamento"
                    funzione_cbx1(cbx_ClasseIsolamento, SchemaDB(i))
                Case "cbx_IP"
                    funzione_cbx1(cbx_IP, SchemaDB(i))
                Case "cbx_costruzione"
                    funzione_cbx1(cbx_costruzione, SchemaDB(i))
                Case "cbx_superficiale"
                    funzione_cbx1(cbx_superficiale, SchemaDB(i))
                Case "cbx_colore"
                    funzione_cbx1(cbx_colore, SchemaDB(i))
                Case "cbx_corrosione"
                    funzione_cbx1(cbx_corrosione, SchemaDB(i))
                Case "cbx_MaterialeScudi"
                    funzione_cbx1(cbx_MaterialeScudi, SchemaDB(i))
                    '-------------------------------------------------------------------
                    '------------------------Ventilatore------------------------------
                Case "cbx_VitiFan"
                    funzione_cbx1(cbx_VitiFan, SchemaDB(i))
                Case "cbx_DispMotore"

                    cbx_DispMotore.Items.Clear()

                    If cbx_Direzione.SelectedIndex = 0 Then
                        cbx_DispMotore.Items.Add("Non definito")
                        cbx_DispMotore.Items.Add("A   Premente asse orizzontale")
                        cbx_DispMotore.Items.Add("AD Premente con albero verso il basso")
                        cbx_DispMotore.Items.Add("AU Premente con albero in alto")
                    ElseIf cbx_Direzione.SelectedIndex = 1 Then
                        cbx_DispMotore.Items.Add("Non definito")
                        cbx_DispMotore.Items.Add("B   Aspirante asse orizzontale")
                        cbx_DispMotore.Items.Add("BD Aspirante con albero in alto")
                        cbx_DispMotore.Items.Add("BU Aspirante con Albero in Basso")
                    ElseIf cbx_Direzione.SelectedIndex = 3 Then
                        cbx_DispMotore.Items.Add("Non definito")
                        cbx_DispMotore.Items.Add("H   Radiale asse orizzontale")
                        cbx_DispMotore.Items.Add("D Radiale con albero verso il basso")
                        cbx_DispMotore.Items.Add("U Radiale con albero in alto")
                    End If


                    funzione_cbx1(cbx_DispMotore, SchemaDB(i))
                    '-------------------------------------------------------------------

                    '------------------------Ventola------------------------------
                Case "cbx_MaterialeVentola"
                    funzione_cbx1(cbx_MaterialeVentola, SchemaDB(i))
                Case "cbx_TrattamentoSupVentola"
                    funzione_cbx1(cbx_TrattamentoSupVentola, SchemaDB(i))
                Case "cbx_ColoreVentola"
                    funzione_cbx1(cbx_ColoreVentola, SchemaDB(i))
                Case "cbx_ClasseCorrVentola"
                    funzione_cbx1(cbx_ClasseCorrVentola, SchemaDB(i))
                Case "cbx_MaterialeVitiVentola"
                    funzione_cbx1(cbx_MaterialeVitiVentola, SchemaDB(i))
                Case "cbx_MaterialeMozzo"
                    funzione_cbx1(cbx_MaterialeMozzo, SchemaDB(i))
                Case "cbx_TrattamentoMozzo"
                    funzione_cbx1(cbx_TrattamentoMozzo, SchemaDB(i))
                Case "cbx_ColoreMozzo"
                    funzione_cbx1(cbx_ColoreMozzo, SchemaDB(i))
                Case "cbx_ClasseCorrMozzo"
                    funzione_cbx1(cbx_ClasseCorrMozzo, SchemaDB(i))
                Case "cbx_MaterialeRaggera"
                    funzione_cbx1(cbx_MaterialeRaggera, SchemaDB(i))
                Case "cbx_TrattamentoRaggera"
                    funzione_cbx1(cbx_TrattamentoRaggera, SchemaDB(i))
                Case "cbx_ColoreRaggera"
                    funzione_cbx1(cbx_ColoreRaggera, SchemaDB(i))
                Case "cbx_ClasseCorrRaggera"
                    funzione_cbx1(cbx_ClasseCorrRaggera, SchemaDB(i))
                Case "cbx_tipo_ventola"
                    funzione_cbx1(cbx_tipo_ventola, SchemaDB(i))
                Case "cbx_profilo_ventola"
                    funzione_cbx1(cbx_profilo_ventola, SchemaDB(i))
                Case "cbx_gradi_ventola"
                    funzione_cbx1(cbx_gradi_ventola, SchemaDB(i))
                Case "cbx_diametro_ventola"
                    funzione_cbx1(cbx_diametro_ventola, SchemaDB(i))
                Case "cbx_pale_ventola"
                    funzione_cbx1(cbx_pale_ventola, SchemaDB(i))

        '-------------------------------------------------------------------

                    '------------------------Convogliatore------------------------------
                Case "cbx_TipoConvogliatore"
                    funzione_cbx1(cbx_TipoConvogliatore, SchemaDB(i))
                Case "cbx_MaterialeConvogliatore"
                    funzione_cbx1(cbx_MaterialeConvogliatore, SchemaDB(i))
                Case "cbx_TrattamentoConvogliatore"
                    funzione_cbx1(cbx_TrattamentoConvogliatore, SchemaDB(i))
                Case "cbx_ColoreConvogliatore"
                    funzione_cbx1(cbx_ColoreConvogliatore, SchemaDB(i))
                Case "cbx_ClasseConvogliatore"
                    funzione_cbx1(cbx_ClasseConvogliatore, SchemaDB(i))

                    '-------------------------------------------------------------------

                 '------------------------Supporto------------------------------
                Case "cbx_TipoSupporto"
                    funzione_cbx1(cbx_TipoSupporto, SchemaDB(i))
                Case "cbx_MaterialeSupporto"
                    funzione_cbx1(cbx_MaterialeSupporto, SchemaDB(i))
                Case "cbx_TrattamentoSupporto"
                    funzione_cbx1(cbx_TrattamentoSupporto, SchemaDB(i))
                Case "cbx_ColoreSupporto"
                    funzione_cbx1(cbx_ColoreSupporto, SchemaDB(i))
                Case "cbx_ClasseSupporto"
                    funzione_cbx1(cbx_ClasseSupporto, SchemaDB(i))

                    '-------------------------------------------------------------------
                    '------------------------Parametri Convolgiatori singoli------------------------------
                Case "cbx_Tipo_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_Tipo_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0
                Case "cbx_Diam_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_Diam_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0
                Case "cbx_Mat_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_Mat_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0
                Case "cbx_TratSup_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_TratSup_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0
                Case "cbx_Colore_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_Colore_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0
                Case "cbx_ClassCorr_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_ClassCorr_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0
                Case "cbx_TratSup_conv"
                    jump_tipo_richiesta0 = 1
                    funzione_cbx1(cbx_TratSup_conv, SchemaDB(i))
                    jump_tipo_richiesta0 = 0
                    '-------------------------------------------------------------------

            End Select

        Next




    End Sub



    Public Sub compila_tbx_ClinteRev0()


        For Each item As Control In gb_generale.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                For i = 0 To Numero_colonneDB - 1

                    If tb.Name = Nome_colonne(i + 1) Then

                        If Valore_CellaRiga(i) <> "" Then
                            'tb.Text = Valore_CellaRiga(i)
                            'tb.Enabled = False
                        End If

                    End If

                Next

            End If
        Next


        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                For i = 0 To Numero_colonneDB - 1

                    If tb.Name = Nome_colonne(i + 1) Then

                        If Valore_CellaRiga(i) <> "" Then
                            'tb.Text = Valore_CellaRiga(i)
                            'tb.Enabled = False
                        End If

                    End If

                Next

            End If
        Next


        For Each item As Control In gb_ventola.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                For i = 0 To Numero_colonneDB - 1

                    If tb.Name = Nome_colonne(i + 1) Then

                        If Valore_CellaRiga(i) <> "" Then
                            'tb.Text = Valore_CellaRiga(i)
                            'tb.Enabled = False
                        End If

                    End If

                Next

            End If
        Next




    End Sub




    Public Sub compila_check_ClinteRev0()


        For Each item As Control In gb_generale.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                Dim chb As Guna.UI2.WinForms.Guna2CheckBox = DirectCast(item, Guna.UI2.WinForms.Guna2CheckBox)

                For i = 0 To Numero_colonneDB - 1

                    If chb.Name = Nome_colonne(i + 1) Then

                        Try

                            If Valore_CellaRiga(i) = True Then
                                'chb.Checked = Valore_CellaRiga(i)
                                'chb.Enabled = False
                            End If

                        Catch ex As Exception
                            chb.Checked = False
                        End Try

                    End If

                Next

            End If
        Next



        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                Dim chb As Guna.UI2.WinForms.Guna2CheckBox = DirectCast(item, Guna.UI2.WinForms.Guna2CheckBox)

                For i = 0 To Numero_colonneDB - 1

                    If chb.Name = Nome_colonne(i + 1) Then

                        Try

                            If Valore_CellaRiga(i) = True Then
                                'chb.Checked = Valore_CellaRiga(i)
                                'chb.Enabled = False
                            End If

                        Catch ex As Exception
                            chb.Checked = False
                        End Try

                    End If

                Next

            End If
        Next

    End Sub


    Public Sub All_Hide()

        Guna2Button1.Visible = False
        Guna2Button2.Visible = False
        Guna2Button3.Visible = False
        cbx_Revisione.Visible = False
        Label4.Visible = False


        For Each item As Control In gb_generale.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cb.Visible = False


            End If
        Next

        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cb.Visible = False


            End If
        Next

        For Each item As Control In gb_ventilatore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cb.Visible = False


            End If
        Next


        For Each item As Control In gb_ventola.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cb.Visible = False


            End If
        Next

        For Each item As Control In gb_convogliatore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cb.Visible = False


            End If
        Next


        For Each item As Control In gb_supporto.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cb.Visible = False


            End If
        Next



        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                Dim chb As Guna.UI2.WinForms.Guna2CheckBox = DirectCast(item, Guna.UI2.WinForms.Guna2CheckBox)


                chb.Visible = False


            End If
        Next


        For Each item As Control In ParGen.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                tb.Visible = False

            End If
        Next


        For Each item As Control In gb_generale.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                tb.Visible = False

            End If
        Next

        tbx_Potenza.Visible = False
        tbx_Cliente.Visible = True
        cbx_TipoRichiesta.Visible = True
        tbx_Descrizione.Visible = True
        cbx_TipoFan.Visible = True
        cbx_Direzione.Visible = True
        cbx_ApplicazioneFan.Visible = True
        tbx_Portata.Visible = True
        tbx_Pressione.Visible = True
        tbx_TemperaturaMinAmb.Visible = True
        tbx_TemperaturaMaxAmb.Visible = True
        cbx_TipoMotore.Visible = True
        cbx_Alimentazione.Visible = True
        cbx_Volt.Visible = True
        cbx_Hz.Visible = True
        tbx_Quantita.Visible = True
        tbx_Riferimento.Visible = True
        Label4.Visible = True

    End Sub




    Public Sub All_Show()

        tbx_Potenza.Visible = True
        'Guna2Button1.Visible = True
        Guna2Button2.Visible = True
        'Guna2Button3.Visible = True
        'cbx_Revisione.Visible = True
        Label4.Visible = True

        ButtonCert.Visible = True
        ButtonConn.Visible = True
        ButtonAcc.Visible = True

        Dim num_rev As Integer = prog_rev(posizione_progetto, 1)

        If num_rev > 0 And cbx_TipoRichiesta.SelectedIndex = 1 Or cbx_TipoRichiesta.SelectedIndex = 2 Then
            Guna2Button1.Visible = True
            Guna2Button3.Visible = True
            cbx_Revisione.Visible = True

            If fast_PJ = 0 Then
                btn_VipDesigner.Visible = True
            End If

            btn_Datasheet.Visible = True


        End If



        For Each item As Control In gb_generale.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cb.Visible = True


                If Rad_Safe.Checked = True Then


                    cbx_AtexCategoria.Visible = False
                    cbx_AtexClasseTemperatura.Visible = False
                    cbx_AtexCustodia.Visible = False
                    cbx_AtexProtezione.Visible = False


                    cbx_AtexCategoria.SelectedIndex = -1
                    cbx_AtexClasseTemperatura.SelectedIndex = -1
                    cbx_AtexCustodia.SelectedIndex = -1
                    cbx_AtexProtezione.SelectedIndex = -1

                    Label14.Visible = False
                    Label22.Visible = False
                    Label35.Visible = False
                    Label36.Visible = False

                Else

                    cbx_AtexCategoria.Visible = True
                    cbx_AtexClasseTemperatura.Visible = True
                    cbx_AtexCustodia.Visible = True
                    cbx_AtexProtezione.Visible = True

                    Label14.Visible = True
                    Label22.Visible = True
                    Label35.Visible = True
                    Label36.Visible = True

                End If



            End If
        Next

        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cb.Visible = True


            End If
        Next

        For Each item As Control In gb_ventilatore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cb.Visible = True


            End If
        Next


        For Each item As Control In gb_ventola.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cb.Visible = True


            End If
        Next

        For Each item As Control In gb_convogliatore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cb.Visible = True


            End If
        Next


        For Each item As Control In gb_supporto.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cb.Visible = True


            End If
        Next



        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                Dim chb As Guna.UI2.WinForms.Guna2CheckBox = DirectCast(item, Guna.UI2.WinForms.Guna2CheckBox)

                If chb.Name <> "check_NEMA" Then
                    chb.Visible = True
                End If

            End If
        Next


        For Each item As Control In ParGen.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                If tb.Name <> "tbx_OrdineRicevuto" And tb.Name <> "tbx_fast" Then
                    tb.Visible = True
                End If

            End If
        Next


        For Each item As Control In gb_generale.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                If tb.Name <> "tbx_OrdineRicevuto" And tb.Name <> "tbx_fast" Then
                    tb.Visible = True
                End If

            End If
        Next


    End Sub

    Private Sub Guna2CircleButton1_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton1.Click
        Form1.btnProgette.PerformClick()
    End Sub




    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        Try

            If keyData = Keys.Enter Then

                posizione_grid_note = Guna2DataGridView1.CurrentRow.Index

                If posizione_grid_note < Guna2DataGridView1.Rows.Count - 1 Then
                    Enter_call = 1
                End If

                testRTF = note_vect(Guna2DataGridView1.CurrentRow.Index)
                Panel_Note.tmp_form1 = Me

                Panel_Note.Show()
            End If


            If keyData = Keys.Escape Then

                Panel_Note.Close()

            End If

            'If keyData = Keys.Back Then

            '    Form1.btnProgette.PerformClick()
            '    Form1.btnParametri.Visible = False

            'End If


            If keyData = Keys.Delete Then

                Yes_No_Warning = 0
                Warning.Label1.Text = "             Do you want delete the note?"
                If Warning.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
                    'attendo la risposta della box
                End If

                If Yes_No_Warning = 1 Then


                    lettura_note1()
                    aggiorna_note()
                    posizione_grid_note = Guna2DataGridView1.CurrentRow.Index
                    Cancella_nota1()
                    Guna2DataGridView1.Rows.Clear()
                    lettura_note1()
                    aggiorna_note()

                    For i = 0 To 99

                        ID_note_vect(i) = ""
                        note_vect(i) = ""

                    Next

                    Dim view_progetto As New DataView(tables2(0))
                    Dim add_count As Integer = 0

                    For i As Integer = 0 To view_progetto.Count - 1

                        If view_progetto(i).Item("ProgettoPJ").ToString() = prog_rev(posizione_progetto, 0) Then


                            ID_note_vect(add_count) = view_progetto(i).Item("ID").ToString
                            note_vect(add_count) = view_progetto(i).Item("NotaPJ").ToString


                            add_count = add_count + 1

                        End If


                    Next

                End If







            End If



        Catch ex As Exception

        End Try




    End Function




    Private Sub gb_motore_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove, gb_motore.MouseMove, gb_generale.MouseMove, gb_ventilatore.MouseMove, gb_ventola.MouseMove, gb_supporto.MouseMove, gb_convogliatore.MouseMove, Guna2Panel2.MouseMove, Guna2DataGridView1.MouseMove

        Try
            Panel_Note.BringToFront()
        Catch ex As Exception

        End Try

    End Sub



    Private Sub Guna2DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellDoubleClick

        posizione_grid_note = Guna2DataGridView1.CurrentRow.Index
        testRTF = note_vect(Guna2DataGridView1.CurrentRow.Index)
        Panel_Note.tmp_form1 = Me

        N_rev_note = cbx_Revisione.SelectedIndex

        Panel_Note.Show()

    End Sub


    Private Sub Guna2PictureBox2_MouseHover(sender As Object, e As EventArgs) Handles Guna2PictureBox2.MouseHover, Guna2PictureBox3.MouseHover
        Me.Cursor = Cursors.Hand
        Me.Refresh()
    End Sub

    Private Sub Guna2PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles Guna2PictureBox2.MouseLeave, Guna2PictureBox3.MouseLeave
        Me.Cursor = Cursors.Arrow
        Me.Refresh()
    End Sub




    Private Sub Guna2PictureBox2_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox2.Click

        If OrdineRic = 0 Then
            Guna2PictureBox2.Image = My.Resources.Vvv
            OrdineRic = 1
            tbx_OrdineRicevuto.Text = OrdineRic 'la textbox serve per il processo di aggiornamento database
        Else
            Guna2PictureBox2.Image = My.Resources.Xxx
            OrdineRic = 0
            tbx_OrdineRicevuto.Text = OrdineRic  'la textbox serve per il processo di aggiornamento database
        End If

    End Sub





    Private Sub tbx_ventola_TextChanged(sender As Object, e As EventArgs) Handles tbx_ventola.TextChanged
        gb_ventola.Text = "Ventola:              " & tbx_ventola.Text
    End Sub


    Private Sub tbx_motore_TextChanged(sender As Object, e As EventArgs) Handles tbx_motore.TextChanged

        Try
            Dim NomeMotoreDB_star As String = tbx_motore.Text
            Dim vettore_passaggio(8) As String

            For i = 0 To 7
                If i < 7 Then
                    vettore_passaggio(i) = NomeMotoreDB_star.Substring(0, NomeMotoreDB_star.IndexOf("_"))
                    NomeMotoreDB_star = NomeMotoreDB_star.Substring(NomeMotoreDB_star.IndexOf("_") + 1, NomeMotoreDB_star.Length - NomeMotoreDB_star.IndexOf("_") - 1)
                Else
                    vettore_passaggio(i) = NomeMotoreDB_star
                End If
            Next

            gb_motore.Text = "Motore elettrico:      " & vettore_passaggio(0) & "            frame " & vettore_passaggio(4)
            tbx_Potenza.Text = vettore_passaggio(1)

        Catch ex As Exception

        End Try




    End Sub

    Private Sub check_RevCliente_CheckedChanged(sender As Object, e As EventArgs) Handles check_RevCliente.CheckedChanged

        If check_RevCliente.Checked = True Then
            check_richiesta = 1
        Else
            check_richiesta = 0
        End If

    End Sub

    Private Sub Guna2CircleButton2_Click(sender As Object, e As EventArgs) Handles ButtonConn.Click

        Try
            Special_Settings.Close()
        Catch ex As Exception

        End Try

        cable_ACT = 1
        cert_ACT = 0
        acc_ACT = 0

        Special_Settings.Show()
    End Sub

    Private Sub Guna2CircleButton3_Click(sender As Object, e As EventArgs) Handles ButtonCert.Click

        Try
            Special_Settings.Close()
        Catch ex As Exception

        End Try

        cable_ACT = 0
        cert_ACT = 1
        acc_ACT = 0

        Special_Settings.Show()
    End Sub

    Private Sub Guna2CircleButton4_Click(sender As Object, e As EventArgs) Handles ButtonAcc.Click

        Try
            Special_Settings.Close()
        Catch ex As Exception

        End Try


        cable_ACT = 0
        cert_ACT = 0
        acc_ACT = 1

        Special_Settings.Show()

    End Sub


    Private Sub Guna2CircleButton2_MouseHover(sender As Object, e As EventArgs) Handles ButtonCert.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub Guna2CircleButton2_MouseLeave(sender As Object, e As EventArgs) Handles ButtonCert.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub Guna2CircleButton3_MouseHover(sender As Object, e As EventArgs) Handles ButtonConn.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub Guna2CircleButton3_MouseLeave(sender As Object, e As EventArgs) Handles ButtonConn.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub Guna2CircleButton4_MouseHover(sender As Object, e As EventArgs) Handles ButtonAcc.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub Guna2CircleButton4_MouseLeave(sender As Object, e As EventArgs) Handles ButtonAcc.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub



    Private Sub btn_Datasheet_Click(sender As Object, e As EventArgs) Handles btn_Datasheet.Click



        Try
            PrezzoMenu.Close()
        Catch ex As Exception

        End Try

        PrezzoMenu.Show()

    End Sub



    Private Sub cbx_ConfigurazioneFan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_ConfigurazioneFan.SelectedIndexChanged



        If cbx_ConfigurazioneFan.SelectedItem = "Basso profilo - senza pannello" Or cbx_ConfigurazioneFan.SelectedItem = "Alto profilo sbordato - senza pannello" Or cbx_ConfigurazioneFan.SelectedItem = "Alto profilo flangiato - senza pannello" Then


            cbx_TipoConvogliatore.SelectedIndex = -1
            cbx_MaterialeConvogliatore.SelectedIndex = -1
            cbx_TrattamentoConvogliatore.SelectedIndex = -1
            cbx_ColoreConvogliatore.SelectedIndex = -1
            cbx_ClasseConvogliatore.SelectedIndex = -1

            gb_convogliatore.Enabled = False

        Else

            gb_convogliatore.Enabled = True

        End If




    End Sub


    Public Sub funzione_tensione()

        cbx_Volt.Items.Clear()


        Dim alimentazione As String = cbx_Alimentazione.SelectedItem
        Dim hz As String = cbx_Hz.SelectedItem

        'determina le tensioni standard e quelle speciali in funzione del n° di fasi e della frequenza
        If cbx_Alimentazione.SelectedItem = "Trifase" And cbx_Hz.SelectedItem = "50" Then
            cbx_Volt.Items.Add("220")
            cbx_Volt.Items.Add("230")
            cbx_Volt.Items.Add("380")
            cbx_Volt.Items.Add("400")
            cbx_Volt.Items.Add("415")
            cbx_Volt.Items.Add("440")
            cbx_Volt.Items.Add("690")


        End If

        If cbx_Alimentazione.SelectedItem = "Trifase" And cbx_Hz.SelectedItem = "60" Then

            cbx_Volt.Items.Add("220")
            cbx_Volt.Items.Add("230")
            cbx_Volt.Items.Add("380")
            cbx_Volt.Items.Add("400")
            cbx_Volt.Items.Add("440")
            cbx_Volt.Items.Add("460")
            cbx_Volt.Items.Add("480")
            cbx_Volt.Items.Add("500")
            cbx_Volt.Items.Add("575")
            cbx_Volt.Items.Add("600")
            cbx_Volt.Items.Add("690")


        End If

        If cbx_Alimentazione.SelectedItem = "Monofase" And cbx_Hz.SelectedItem = "50" Then

            cbx_Volt.Items.Add("220")
            cbx_Volt.Items.Add("230")

        End If

        If cbx_Alimentazione.SelectedItem = "Monofase" And cbx_Hz.SelectedItem = "60" Then

            cbx_Volt.Items.Add("110")
            cbx_Volt.Items.Add("220")
            cbx_Volt.Items.Add("230")


        End If




    End Sub

    Private Sub cbx_tipo_ventola_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_tipo_ventola.SelectedIndexChanged

        'code_tipo.Visible = True

        Dim selezione As Integer = cbx_tipo_ventola.SelectedIndex

        Select Case selezione
            Case 0
                code_tipo.Text = "?"
            Case 1
                code_tipo.Text = "A"
            Case 2
                code_tipo.Text = "C"
            Case 3
                code_tipo.Text = "F"
            Case 4
                code_tipo.Text = "H"
            Case 5
                code_tipo.Text = "K"
            Case 6
                code_tipo.Text = "L"
            Case 7
                code_tipo.Text = "M"
            Case 8
                code_tipo.Text = "N"
            Case 9
                code_tipo.Text = "R"
            Case 10
                code_tipo.Text = "T"
            Case 11
                code_tipo.Text = "V"
            Case 12
                code_tipo.Text = "W"
        End Select


        'composizione codice
        tbx_ventola.Text = code_tipo.Text & cbx_diametro_ventola.SelectedItem & "-" & cbx_gradi_ventola.SelectedItem & "°" & "-__-_-__" & code_profilo.Text & code_pale.Text & "_-_"

    End Sub

    Private Sub cbx_profilo_ventola_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_profilo_ventola.SelectedIndexChanged

        'code_profilo.Visible = True

        Dim selezione As Integer = cbx_profilo_ventola.SelectedIndex

        Select Case selezione
            Case 0
                code_profilo.Text = "?"
            Case 1
                code_profilo.Text = "0"
            Case 2
                code_profilo.Text = "1"
            Case 3
                code_profilo.Text = "2"
            Case 4
                code_profilo.Text = "3"
            Case 5
                code_profilo.Text = "4"
            Case 6
                code_profilo.Text = "5"
            Case 7
                code_profilo.Text = "6"
            Case 8
                code_profilo.Text = "7"
            Case 9
                code_profilo.Text = "8"
            Case 10
                code_profilo.Text = "9"
            Case 11
                code_profilo.Text = "A"
            Case 12
                code_profilo.Text = "B"
            Case 13
                code_profilo.Text = "C"
            Case 14
                code_profilo.Text = "D"
            Case 15
                code_profilo.Text = "E"
            Case 16
                code_profilo.Text = "F"
            Case 17
                code_profilo.Text = "G"
            Case 18
                code_profilo.Text = "H"
            Case 19
                code_profilo.Text = "I"
            Case 20
                code_profilo.Text = "J"
            Case 21
                code_profilo.Text = "K"
            Case 22
                code_profilo.Text = "L"
            Case 23
                code_profilo.Text = "M"
            Case 24
                code_profilo.Text = "N"
            Case 25
                code_profilo.Text = "O"
            Case 26
                code_profilo.Text = "P"
            Case 27
                code_profilo.Text = "Q"
            Case 28
                code_profilo.Text = "R"
            Case 29
                code_profilo.Text = "S"
            Case 30
                code_profilo.Text = "T"
            Case 31
                code_profilo.Text = "U"
            Case 32
                code_profilo.Text = "V"
            Case 33
                code_profilo.Text = "Y"
            Case 34
                code_profilo.Text = "Z"

        End Select

        'composizione codice
        tbx_ventola.Text = code_tipo.Text & cbx_diametro_ventola.SelectedItem & "-" & cbx_gradi_ventola.SelectedItem & "°" & "-__-_-__" & code_profilo.Text & code_pale.Text & "_-_"

    End Sub


    Private Sub cbx_pale_ventola_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_pale_ventola.SelectedIndexChanged

        'code_pale.Visible = True

        Dim selezione As Integer = cbx_pale_ventola.SelectedIndex

        Select Case selezione
            Case 0
                code_pale.Text = "?"
            Case 1
                code_pale.Text = "0"
            Case 2
                code_pale.Text = "1"
            Case 3
                code_pale.Text = "2"
            Case 4
                code_pale.Text = "3"
            Case 5
                code_pale.Text = "4"
            Case 6
                code_pale.Text = "5"
            Case 7
                code_pale.Text = "6"
            Case 8
                code_pale.Text = "7"
            Case 9
                code_pale.Text = "8"
            Case 10
                code_pale.Text = "9"
            Case 11
                code_pale.Text = "A"
            Case 12
                code_pale.Text = "B"
            Case 13
                code_pale.Text = "C"
            Case 14
                code_pale.Text = "D"
            Case 15
                code_pale.Text = "E"
            Case 16
                code_pale.Text = "F"
            Case 17
                code_pale.Text = "G"
            Case 18
                code_pale.Text = "H"
            Case 19
                code_pale.Text = "I"
            Case 20
                code_pale.Text = "L"
            Case 21
                code_pale.Text = "M"
        End Select

        'composizione codice
        tbx_ventola.Text = code_tipo.Text & cbx_diametro_ventola.SelectedItem & "-" & cbx_gradi_ventola.SelectedItem & "°" & "-__-_-__" & code_profilo.Text & code_pale.Text & "_-_"

    End Sub

    Private Sub cbx_gradi_ventola_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_gradi_ventola.SelectedIndexChanged
        'composizione codice
        tbx_ventola.Text = code_tipo.Text & cbx_diametro_ventola.SelectedItem & "-" & cbx_gradi_ventola.SelectedItem & "°" & "-__-_-__" & code_profilo.Text & code_pale.Text & "_-_"
    End Sub

    Private Sub cbx_diametro_ventola_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_diametro_ventola.SelectedIndexChanged
        'composizione codice
        tbx_ventola.Text = code_tipo.Text & cbx_diametro_ventola.SelectedItem & "-" & cbx_gradi_ventola.SelectedItem & "°" & "-__-_-__" & code_profilo.Text & code_pale.Text & "_-_"
    End Sub



    Private Sub Guna2PictureBox3_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox3.Click

        funzione_FastPJ()

    End Sub



    Public Sub funzione_FastPJ()


        If fase_load = 0 Then

            If fast_PJ = 0 Then
                Guna2PictureBox3.Image = My.Resources.fast
                fast_PJ = 1
                tbx_fast.Text = fast_PJ 'la textbox serve per il processo di aggiornamento database
                Label87.Text = "PJ rapida"

                gb_generale.Visible = False
                gb_motore.Visible = False
                gb_ventilatore.Visible = False
                gb_ventola.Visible = False
                gb_supporto.Visible = False
                gb_convogliatore.Visible = False
                gb_conv1.Visible = False



                Guna2DataGridView1.Visible = False

                'btn_Datasheet.Visible = True
                'Guna2Button3.Visible = True
                'Guna2Button2.Visible = True

                btn_VipDesigner.Visible = False

                SbloccaVD1()

            Else
                Guna2PictureBox3.Image = My.Resources.slow
                fast_PJ = 0
                tbx_fast.Text = fast_PJ  'la textbox serve per il processo di aggiornamento database
                Label87.Text = "PJ completa"


                If cbx_TipoRichiesta.SelectedIndex = 0 Then
                    btn_VipDesigner.Visible = False

                    Guna2DataGridView1.Visible = True
                    gb_conv1.Visible = True
                Else
                    btn_VipDesigner.Visible = True

                    gb_generale.Visible = True
                    gb_motore.Visible = True
                    gb_ventilatore.Visible = True
                    gb_ventola.Visible = True
                    gb_supporto.Visible = True
                    gb_convogliatore.Visible = True
                    Guna2DataGridView1.Visible = True
                End If


                If new_PJVar = 1 Then
                    btn_Datasheet.Visible = False
                    Guna2Button3.Visible = False
                    btn_VipDesigner.Visible = False
                    Guna2Button2.Visible = False
                End If

                SbloccaVD1()

            End If

        End If





    End Sub


    Private Sub Guna2PictureBox4_MouseHover(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub Guna2PictureBox4_MouseLeave(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.Arrow
    End Sub




    Public Sub hide_fastPJ()
        'Funzione usata per nascondere il tasto progettazione rapida, momentanenamente disattivata
        'Serve a togliere la possibilità all'utente di cambiare una progettazione da rapida a completa

        Dim somma_stati As Integer = Set_TipoFan + Set_DirFlusso + Set_applicazione + Set_Portata + Set_Pressione + Set_Min + Set_Max + Set_TipoMot + Set_Alim + Set_Volt + Set_Freq


        If cbx_TipoRichiesta.SelectedIndex = 0 Then

                If cbx_Tipo_conv.SelectedIndex <> -1 Or cbx_Mat_conv.SelectedIndex <> -1 Or cbx_Diam_conv.SelectedIndex <> -1 Or cbx_TratSup_conv.SelectedIndex <> -1 Or cbx_Colore_conv.SelectedIndex <> -1 Or cbx_ClassCorr_conv.SelectedIndex <> -1 Or tbx_spessore_conv.Text <> "" Or tbx_Ninserti_conv.Text <> "" Or tbx_DimInserti_conv.Text <> "" Then

                    Guna2PictureBox3.Visible = False
                    Label87.Visible = False

                End If


            Else


                If new_PJVar <> 1 Then

                    If somma_stati > 0 Then

                        If fast_PJ = 1 Then
                        funzione_FastPJ()
                        'Guna2PictureBox3.Visible = False
                        'Label87.Visible = False
                    Else
                        'Guna2PictureBox3.Visible = False
                        'Label87.Visible = False
                    End If

                    End If

                End If

        End If


    End Sub

    Private Sub cbx_Tipo_conv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_Tipo_conv.SelectedIndexChanged
        hide_fastPJ()
    End Sub


    Private Sub cbx_Diam_conv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_Diam_conv.SelectedIndexChanged
        hide_fastPJ()
    End Sub

    Private Sub cbx_Mat_conv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_Mat_conv.SelectedIndexChanged
        hide_fastPJ()
    End Sub

    Private Sub cbx_TratSup_conv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_TratSup_conv.SelectedIndexChanged
        hide_fastPJ()
    End Sub

    Private Sub cbx_Colore_conv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_Colore_conv.SelectedIndexChanged
        hide_fastPJ()
    End Sub

    Private Sub cbx_ClassCorr_conv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_ClassCorr_conv.SelectedIndexChanged
        hide_fastPJ()
    End Sub

    Private Sub tbx_spessore_conv_TextChanged(sender As Object, e As EventArgs) Handles tbx_spessore_conv.TextChanged
        hide_fastPJ()
    End Sub

    Private Sub tbx_Ninserti_conv_TextChanged(sender As Object, e As EventArgs) Handles tbx_Ninserti_conv.TextChanged
        hide_fastPJ()
    End Sub

    Private Sub tbx_DimInserti_conv_TextChanged(sender As Object, e As EventArgs) Handles tbx_DimInserti_conv.TextChanged
        hide_fastPJ()
    End Sub


End Class
