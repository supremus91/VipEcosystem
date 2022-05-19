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


Public Class Progettazioni


    Private Sub FormProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2DataGridView1.Select()
        LoadTheme()

        hide_var = 0
        Guna2CirclePictureBox1_Click(sender, e)

    End Sub
    Private Sub LoadTheme()

        spostamento_effettuato = 1

        filterAfterLoad = 0
        For Each btns As Control In Me.Controls
            If btns.[GetType]() = GetType(Button) Then
                Dim btn As Button = CType(btns, Button)
                btn.BackColor = ThemeColor.PrimaryColor
                btn.ForeColor = Color.White
                btn.FlatAppearance.BorderColor = ThemeColor.PrimaryColor
            End If
        Next

        'come selezione iniziale si cerca a chi appartiene la progettazione
        Guna2TextBox1.Text = nome_macchina
        cbx_stati_PJ.SelectedIndex = 0
        filtroTab1 = "cbx_Owner"
        mod_filtro = 1


        If primo_avvio = 1 Then
            main_load()

        Else 'lo salto nella prima fasse perche rallenta
            Form1.Guna2Panel1.Visible = True  '------> ESEGUIRE IN PARALLELO
            Form1.Guna2Panel1.BringToFront()  '------> ESEGUIRE IN PARALLELO

            Application.DoEvents()

            Dim task1 As Task = Task.Run(Sub() main_load())
            Control.CheckForIllegalCrossThreadCalls = False


            Do While (task1.IsCompleted = False)
                Application.DoEvents()
            Loop

            Form1.Guna2Panel1.Visible = False '------> ESEGUIRE IN PARALLELO

            primo_avvio = 0
        End If



        filterAfterLoad = 1
        on_load = 1



        warning_super_late()


        'SPOSTAMENTO ICONA FAUSO DA EFFETTUARE SOLO 1 VOLTA

    End Sub



    Public Sub main_load()

        'ListView1.BackColor = ThemeColor.PrimaryColor
        'ListView1.ForeColor = ThemeColor.SecondaryColor

        'Label3.ForeColor = ThemeColor.PrimaryColor
        'Label4.ForeColor = ThemeColor.PrimaryColor




        numero_progetti = Guna2DataGridView1.Rows.Count


        If on_load = 0 Then
            Numero_colonne1()
            Numero_colonne_DS1()
            Numero_colonne_Sviluppo1()
            Importa_Ambiente1()
            Importa_Configurazioni1()
            SchemaDatabase1()
        End If


        lettura_progetto1()


        Try
            compilazione_grid()
        Catch ex As Exception

        End Try



        'If sblocco_take_tabs = 1 Then
        '    Crea_DB_tabelle1()
        '    sblocco_take_tabs = 0
        'End If


        Label1.Text = numeroPJ

        Try
            Guna2DataGridView1.Rows(posizione_progetto).Selected = True
        Catch ex As Exception

        End Try


        Label4.Text = num_chiuse
        Label3.Text = num_cod
        Label10.Text = num_lav
        Label7.Text = num_codificata
        Label11.Text = num_attesa

        Try
            complete_textbox()
        Catch ex As Exception

        End Try


    End Sub


    Public Sub complete_textbox()


        Dim vect1 As New AutoCompleteStringCollection
        Dim view As New DataView(tables1(0))


        'Inserisco nella textbox i valore che deve ricercare
        Select Case cbx_stati_PJ.SelectedIndex
            Case 0 'Owner
                For i As Integer = 0 To view.Count - 1
                    'filtroTab1 = "cbx_Owner"
                    ricerca_valore_tab1(filtroTab1)
                    vect1.Add(all_tables(view(i).Item(filtroTab1).ToString + 1, pos_vect1))
                Next
            Case 1
                For i As Integer = 0 To view.Count - 1
                    vect1.Add(view(i).Item(filtroTab1).ToString)
                Next

            Case 2
                For i As Integer = 0 To view.Count - 1
                    vect1.Add(view(i).Item(filtroTab1).ToString)
                Next
            Case 3
                For i As Integer = 0 To view.Count - 1
                    vect1.Add(view(i).Item(filtroTab1).ToString)
                Next
            Case 4
                For i As Integer = 0 To view.Count - 1
                    ricerca_valore_tab1(filtroTab1)
                    vect1.Add(all_tables(view(i).Item(filtroTab1).ToString + 1, pos_vect1))
                Next
            Case 5
                For i As Integer = 0 To view.Count - 1
                    vect1.Add(view(i).Item(filtroTab1).ToString)
                Next
        End Select




        Guna2TextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource
        Guna2TextBox1.AutoCompleteCustomSource = vect1
        Guna2TextBox1.AutoCompleteMode = AutoCompleteMode.Suggest


        Load_base_rating()
        azzera_user()
        Importa_Archivio_DS1()
        Datasheet_stat()
        Lettura_stat_Users()
        Importa_Sviluppo1()
        Numero_colonne_catalogo1()
        Importa_Catalogo1()
        calcola_livello_user()
        totale_PJ_vipDesigner()
        update_players()
        listaPJ_utenti()

        attesa_convalida()






    End Sub




    Public Sub compilazione_grid()


        If ggg = 0 Then
            Lettura_TabDB1()
            ggg = 1
        End If

        Conta_statiPJ1()

        Guna2DataGridView1.Rows.Clear()

        Dim view As New DataView(tables1(0))
        Dim add_grid As Integer = 0

        Dim numero_righe_tab As Integer
        Dim val_comp As String = ""




        Dim nome0 As String


        For i As Integer = 0 To view.Count - 1


            '---------------------------------------------------------------------------------------------------------------------------------------------
            'This part is used in order to identify the last row realted to a specific PJ --> It is referred at the the last review of a specific project
            Dim sur As Integer = i
            Dim PJ_name As String = view(i).Item("tbx_Progetto").ToString

            Try
                Do While view(sur).Item("tbx_Progetto").ToString = PJ_name
                    sur = sur + 1
                Loop

            Catch ex As Exception

            End Try

            sur = sur - i - 1
            '--------------------------------------------------------------------------------------------------------------------------------------------

            'Per alcuni valori delle tabelle il database restituisce un numero e quindi va ricercato il corrispondente valore
            Select Case cbx_stati_PJ.SelectedIndex
                Case 0
                    'filtroTab1 = "cbx_Owner"
                    ricerca_valore_tab1(filtroTab1)
                    val_comp = all_tables(view(i + sur).Item(filtroTab1).ToString + 1, pos_vect1)
                Case 1
                    'filtroTab1 = "tbx_Data"
                    val_comp = view(i + sur).Item(filtroTab1).ToString
                Case 2
                    'filtroTab1 = "tbx_Riferimento"
                    val_comp = view(i + sur).Item(filtroTab1).ToString
                Case 3
                    'filtroTab1 = "tbx_Cliente"
                    val_comp = view(i + sur).Item(filtroTab1).ToString
                Case 4
                    'filtroTab1 = "cbx_Stato"
                    ricerca_valore_tab1(filtroTab1)
                    val_comp = all_tables(view(i + sur).Item(filtroTab1).ToString + 1, pos_vect1)
                Case 5
                    'filtroTab1 = "tbx_Cliente"
                    val_comp = view(i + sur).Item(filtroTab1).ToString
            End Select





            Dim stato_check As String
            Try
                ricerca_valore_tab1("cbx_Stato")
                stato_check = all_tables(view(i + sur).Item("cbx_Stato").ToString + 1, pos_vect1)
            Catch ex As Exception

            End Try



            Dim prog_rew_adapt As String
            For k = 0 To numeroPJ - 1


                If prog_rev(k, 0) = PJ_name Then

                    prog_rew_adapt = prog_rev(k, 1)

                End If
            Next


            Dim Nome_sel As String
            Dim comparison_string As String



            Try 'EFFETTUO UNA RICERCA INDIPENDENTE DALLA DIMENSIONE DEL TESTO
                Nome_sel = val_comp.Substring(0, lunghezza_testo).ToLower
                comparison_string = Guna2TextBox1.Text.ToLower
            Catch ex As Exception
                Nome_sel = 0
                comparison_string = 1
            End Try





            If view(i).Item("cbx_Revisione").ToString = prog_rew_adapt And ((Nome_sel = comparison_string And (stato_check <> "Chiusa" Or cbx_stati_PJ.SelectedItem <> "Assegnata")) Or mod_filtro = 0) Then


                'cerco il creatore della PJ
                For u As Integer = 0 To view.Count - 1

                    If view(u).Item("tbx_Progetto").ToString = view(i).Item("tbx_Progetto").ToString And view(u).Item("cbx_Revisione").ToString = "0" Then

                        nome0 = view(u).Item("cbx_Owner").ToString

                        Select Case nome0
                            Case 1
                                nome0 = "Andrea"
                            Case 2
                                nome0 = "Stefano"
                            Case 3
                                nome0 = "Paolo"
                            Case 4
                                nome0 = "Roberto"
                            Case 5
                                nome0 = "Fausto"
                            Case 6
                                nome0 = "Lorenzo"
                            Case 7
                                nome0 = "Alberto"
                            Case 8
                                nome0 = "Alessandro"
                            Case 9
                                nome0 = "Riccardo"
                        End Select

                    End If

                Next



                Guna2DataGridView1.Rows.Add()
                Guna2DataGridView1.Rows(add_grid).Cells(0).Value = view(i).Item("tbx_Progetto").ToString



                For t As Integer = 0 To view.Count - 1
                    If view(t).Item("cbx_Revisione").ToString = "0" And Guna2DataGridView1.Rows(add_grid).Cells(0).Value = view(t).Item("tbx_Progetto").ToString Then

                        Guna2DataGridView1.Rows(add_grid).Cells(1).Value = view(t).Item("tbx_Data").ToString

                    End If
                Next


                Guna2DataGridView1.Rows(add_grid).Cells(2).Value = view(i + sur).Item("tbx_Cliente").ToString
                Guna2DataGridView1.Rows(add_grid).Cells(3).Value = view(i + sur).Item("tbx_OffertaNumero").ToString


                'combobox tipo richiesta
                'numero_righe_tab = getcount("cbx_TipoRichiesta")
                'Lettura_cella_singolaDB1("cbx_TipoRichiesta", "Descrizione")

                'Try
                '    Guna2DataGridView1.Rows(add_grid).Cells(5).Value = vettore_elemento_cbx(view(i).Item("cbx_TipoRichiesta") - 1)
                'Catch ex As Exception
                '    Guna2DataGridView1.Rows(add_grid).Cells(5).Value = ""
                'End Try

                Try
                    ricerca_valore_tab1("cbx_TipoRichiesta")
                    Guna2DataGridView1.Rows(add_grid).Cells(4).Value = all_tables(view(i + sur).Item("cbx_TipoRichiesta").ToString + 1, pos_vect1)
                Catch ex As Exception

                End Try

                Guna2DataGridView1.Rows(add_grid).Cells(5).Value = view(i + sur).Item("tbx_Riferimento").ToString

                'combobox owner
                'numero_righe_tab = getcount("cbx_Owner")
                'Lettura_cella_singolaDB1("cbx_Owner", "Descrizione")

                'Try
                '    Guna2DataGridView1.Rows(add_grid).Cells(7).Value = vettore_elemento_cbx(view(i).Item("cbx_Owner") - 1)
                'Catch ex As Exception
                '    Guna2DataGridView1.Rows(add_grid).Cells(7).Value = ""
                'End Try


                Try
                    ricerca_valore_tab1("cbx_Owner")
                    Guna2DataGridView1.Rows(add_grid).Cells(6).Value = all_tables(view(i + sur).Item("cbx_Owner").ToString + 1, pos_vect1)
                Catch ex As Exception

                End Try


                'combobox owner
                'numero_righe_tab = getcount("cbx_Stato")
                'Lettura_cella_singolaDB1("cbx_Stato", "Descrizione")

                'Try
                '    Guna2DataGridView1.Rows(add_grid).Cells(8).Value = vettore_elemento_cbx(view(i).Item("cbx_Stato") - 1)
                'Catch ex As Exception
                '    Guna2DataGridView1.Rows(add_grid).Cells(8).Value = ""
                'End Try


                Guna2DataGridView1.Rows(add_grid).Cells(7).Value = nome0


                Try
                    ricerca_valore_tab1("cbx_Stato")
                    Guna2DataGridView1.Rows(add_grid).Cells(8).Value = all_tables(view(i + sur).Item("cbx_Stato").ToString + 1, pos_vect1)
                Catch ex As Exception

                End Try

                'view(i).Item("tbx_Cliente").ToString


                Guna2DataGridView1.Rows(add_grid).Cells(9).Value = prog_rew_adapt

                'If (i + sur) = view.Count - 1 Then ' serve per identificare il corretto numero di revisione per l'ultimo elemento del DB
                '    Guna2DataGridView1.Rows(add_grid).Cells(8).Value = view(i + sur - 1).Item("cbx_Revisione").ToString
                'Else
                '    Guna2DataGridView1.Rows(add_grid).Cells(8).Value = view(i + sur).Item("cbx_Revisione").ToString
                'End If



                'Guna2DataGridView1.Rows(add_grid).Cells(8).Value = prog_rev(add_grid, 1)
                Guna2DataGridView1.Rows(add_grid).Cells(10).Value = view(i + sur).Item("tbx_Descrizione").ToString

                If view(i + sur).Item("tbx_OrdineRicevuto").ToString = 0 Then
                    Guna2DataGridView1.Rows(add_grid).Cells(11).Value = "NO"
                Else
                    Guna2DataGridView1.Rows(add_grid).Cells(11).Value = "SI"
                End If



                'Inserimento prezzo
                Dim prezzo_round As Integer
                Try
                    prezzo_round = view(i + sur).Item("tbx_prezzo_SCONTO").ToString
                    Guna2DataGridView1.Rows(add_grid).Cells(12).Value = prezzo_round
                Catch ex As Exception
                    Guna2DataGridView1.Rows(add_grid).Cells(12).Value = ""
                End Try




                add_grid = add_grid + 1

            End If


        Next


    End Sub




    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click

        new_PJVar = 1

        nome_progetto = "PJ" & Date.Today.Year & Date.Today.Month & Date.Today.Day

        'lettura_progetto1()


        mod_new_progetto = 1 'apro la sessione nuova progettazione

        Guna2TextBox1.Text = ""

        new_project = 1

        Numero_colonne1()
        numero_righeDB = getcount("Progetto") 'numero delle righe database


        'controllo che nel database non esista gia' una progettazione con quel nome e nel caso aggiungo cambio il codice

        For i = 0 To 9999

            If prog_rev(i, 0) = nome_progetto Then

                Dim num_add As Integer = nome_progetto.Substring(6, nome_progetto.Length - 6) + 1

                nome_progetto = "PJ" & Date.Today.Year & (num_add)

                i = -1
            End If

        Next

        prog_rev(numeroPJ, 0) = nome_progetto
        prog_rev(numeroPJ, 1) = 0

        System.IO.Directory.CreateDirectory(folders_directory & "\" & nome_progetto) 'creo la directory del progetto

        Guna2DataGridView1.Rows.Add()
        Guna2DataGridView1.Rows(numeroPJ).Cells(0).Value = nome_progetto

        Guna2DataGridView1.Rows(numeroPJ).Selected = True
        posizione_progetto = numeroPJ

        SchemaDatabase1() ' ottengo i nomi delle tabelle database e il numero totale

        aggiungiPJ1()

        mod_new_progetto = 1

        lettura_progetto1()


        Form1.btnParametri.Visible = True

        If data_progetto = Nothing Then
            data_progetto = Date.Today.Day & "/" & Date.Today.Month & "/" & Date.Today.Year
        End If




        jump_rev = 1
        Form1.btnParametri.PerformClick()

        FormParametri.ParGen.Text = nome_progetto & " del " & data_progetto

        numeroPJ = numeroPJ + 1
        Label1.Text = numeroPJ

        lettura_progetto1()



        'mod_new_progetto = 0 'chiudo la sessione nuova progettazione

        'on_load = 0 'al loading deve riaggiornare tutti i vettori dei progetti
        'primo_avvio = 1 'al loading deve riaggiornare tutti i vettori dei progetti
    End Sub




    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged


        testo_ricerca = Guna2TextBox1.Text
        lunghezza_testo = testo_ricerca.Length


        If filterAfterLoad = 1 Then

            If Guna2TextBox1.Text = "" Then

                mod_filtro = 0


                If mod_new_progetto = 1 Then
                    main_load()
                    mod_new_progetto = 0
                Else
                    Try
                        compilazione_grid()
                    Catch ex As Exception

                    End Try

                End If

            Else

                mod_filtro = 1


                If mod_new_progetto = 1 Then
                    main_load()
                    mod_new_progetto = 0
                Else
                    Try
                        compilazione_grid()
                    Catch ex As Exception

                    End Try

                End If



            End If


        End If


    End Sub



    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_stati_PJ.SelectedIndexChanged

        If filterAfterLoad = 1 Then

            Select Case cbx_stati_PJ.SelectedIndex
                Case 0
                    filtroTab1 = "cbx_Owner"
                    Guna2TextBox1.Enabled = True
                    Guna2TextBox1.Text = nome_macchina
                Case 1
                    filtroTab1 = "tbx_Data"
                    Guna2TextBox1.Enabled = True
                    Guna2TextBox1.Text = ""
                Case 2
                    filtroTab1 = "tbx_Riferimento"
                    Guna2TextBox1.Enabled = True
                    Guna2TextBox1.Text = ""
                Case 3
                    filtroTab1 = "tbx_Cliente"
                    Guna2TextBox1.Enabled = True
                    Guna2TextBox1.Text = ""
                Case 4
                    filtroTab1 = "cbx_Stato"
                    Guna2TextBox1.Enabled = True
                    Guna2TextBox1.Text = ""
                Case 5
                    filtroTab1 = "tbx_Progetto"
                    Guna2TextBox1.Enabled = True
                    Guna2TextBox1.Text = ""
            End Select

        End If


        Try
            complete_textbox()
        Catch ex As Exception

        End Try


    End Sub



    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        'Try

        '    If keyData = Keys.Enter Then

        '        Form1.btnParametri.Visible = True
        '        posizione_progetto = Guna2DataGridView1.CurrentRow.Index
        '        data_progetto = Guna2DataGridView1.Rows(posizione_progetto).Cells(1).Value.ToString


        '        'Il vettore prog_rev(i,0) che contiene i nomei dei progetti non è allineato con la datagrid quindi devo ricercare la posizione
        '        Dim find_PJ As String = Guna2DataGridView1.Rows(posizione_progetto).Cells(0).Value.ToString
        '        For i = 0 To 9999
        '            If prog_rev(i, 0) = find_PJ Then
        '                posizione_progetto = i
        '                i = 10000
        '            End If
        '        Next


        '        Lettura_riga1(prog_rev(posizione_progetto, 1))

        '        Form1.btnParametri.PerformClick()

        '    End If




        'Catch ex As Exception

        'End Try


    End Function



    Private Sub Guna2DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellDoubleClick

        mod_PJ_DS = 1

        posizione_progetto = Guna2DataGridView1.CurrentRow.Index

        numero_progetti = Guna2DataGridView1.Rows(posizione_progetto).Cells(9).Value


        If Guna2DataGridView1.Rows(posizione_progetto).Cells(11).Value = "SI" Then
            OrdineRic = 1
        ElseIf Guna2DataGridView1.Rows(posizione_progetto).Cells(11).Value = "NO" Then
            OrdineRic = 0
        End If


        prezzoPJ = Guna2DataGridView1.Rows(posizione_progetto).Cells(12).Value


        Form1.btnParametri.Visible = True

        data_progetto = Guna2DataGridView1.Rows(posizione_progetto).Cells(1).Value.ToString


        'Il vettore prog_rev(i,0) che contiene i nomi dei progetti non è allineato con la datagrid quindi devo ricercare la posizione
        Dim find_PJ As String = Guna2DataGridView1.Rows(posizione_progetto).Cells(0).Value.ToString
        For i = 0 To 9999
            If prog_rev(i, 0) = find_PJ Then
                posizione_progetto = i
                i = 10000
            End If
        Next




        Lettura_riga1(prog_rev(posizione_progetto, 1))

        Form1.btnParametri.PerformClick()

    End Sub


    Public Sub update_players()




        'Ciclo di ricerca utenti ufficio tecnico

        For Each item As Control In Guna2Panel1.Controls

            If item.GetType Is GetType(System.Windows.Forms.Panel) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                Dim pan As System.Windows.Forms.Panel = DirectCast(item, System.Windows.Forms.Panel)




                For i = 0 To 9



                    If item.Name = Vettore_stat_uffico_tecnico(i, 0) And item.Name <> "Lorenzo" Then

                        For Each item1 As Control In pan.Controls

                            If item1.Name = item.Name & "_PJ_aperte" Then
                                item1.Text = Vettore_stat_uffico_tecnico(i, 1)
                            ElseIf item1.Name = item.Name & "_PJ_chiuse" Then
                                item1.Text = Vettore_stat_uffico_tecnico(i, 2)
                            ElseIf item1.Name = item.Name & "_VipDesigner" Then
                                item1.Text = Vettore_stat_uffico_tecnico(i, 3)
                            ElseIf item1.Name = item.Name & "_contributi" Then
                                item1.Text = Vettore_stat_uffico_tecnico(i, 4)
                            ElseIf item1.Name = item.Name & "_Databook" Then
                                item1.Text = Vettore_stat_uffico_tecnico(i, 5)
                            End If


                            calcola_exp(i, item.Name)


                            Dim load_bar As Guna.UI2.WinForms.Guna2CircleProgressBar
                            load_bar = pan.Controls(item.Name & "_Progress")
                            Dim exp_bar As Guna.UI2.WinForms.Guna2CircleProgressBar
                            exp_bar = load_bar.Controls(item.Name & "_level")
                            Dim label_lv As Label
                            label_lv = pan.Controls(item.Name & "_lv")

                            load_bar.Maximum = 30

                            If item1.Name = item.Name & "_Progress" Then
                                'regolo colore barra PJ aperte
                                load_bar.Value = Vettore_stat_uffico_tecnico(i, 1)


                                If load_bar.Value <= 10 Then
                                    load_bar.ProgressColor = Color.DarkGreen
                                    load_bar.ProgressColor2 = Color.DarkGreen
                                ElseIf load_bar.Value > 10 And load_bar.Value <= 15 Then
                                    load_bar.ProgressColor = Color.Gold
                                    load_bar.ProgressColor2 = Color.Gold
                                ElseIf load_bar.Value > 15 And load_bar.Value <= 25 Then
                                    load_bar.ProgressColor = Color.Orange
                                    load_bar.ProgressColor2 = Color.Orange
                                ElseIf load_bar.Value > 25 Then
                                    load_bar.ProgressColor = Color.Red
                                    load_bar.ProgressColor2 = Color.Red
                                End If

                            End If



                            exp_bar.Value = exp_val
                            label_lv.Text = "lv." & level_val


                            If item1.Name = item.Name & "_spec" Then
                                item1.Text = Math.Round(exp_tot, 0) & "/" & estremo_exp
                            End If

                            '----------------------------------------------------Gestione gemme APERTE--------------------------------------------------------------

                            If item1.Name = item.Name & "_X_1" Then
                                If lv_aperte = 0 Then
                                    item1.Visible = False
                                ElseIf lv_aperte = 1 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then
                                        item1.Visible = True
                                    End If
                                End If
                            End If

                            If item1.Name = item.Name & "_X_2" Then
                                If lv_aperte = 0 Then
                                    item1.Visible = False
                                ElseIf lv_aperte = 1 Then
                                    item1.Visible = False
                                ElseIf lv_aperte = 2 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then

                                        Dim pic As Guna.UI2.WinForms.Guna2CirclePictureBox
                                        Dim panel As Panel = Guna2Panel1.Controls(item.Name)
                                        pic = pan.Controls(item.Name & "_X_1")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_X_2")
                                        pic.Visible = True

                                    End If
                                End If
                            End If

                            If item1.Name = item.Name & "_X_3" Then
                                If lv_aperte = 0 Then
                                    item1.Visible = False
                                ElseIf lv_aperte = 1 Then
                                    item1.Visible = False
                                ElseIf lv_aperte = 2 Then
                                    item1.Visible = False
                                ElseIf lv_aperte = 3 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then

                                        Dim pic As Guna.UI2.WinForms.Guna2CirclePictureBox
                                        Dim panel As Panel = Guna2Panel1.Controls(item.Name)
                                        pic = pan.Controls(item.Name & "_X_1")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_X_2")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_X_3")
                                        pic.Visible = True


                                    End If
                                End If
                            End If


                            '---------------------------------------------------------------------------------------------------------------------------------------


                            '----------------------------------------------------Gestione gemme CHIUSE--------------------------------------------------------------

                            If item1.Name = item.Name & "_PJ_1" Then
                                If lv_chiuse = 0 Then
                                    item1.Visible = False
                                ElseIf lv_chiuse = 1 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then
                                        item1.Visible = True
                                    End If
                                End If
                            End If

                            If item1.Name = item.Name & "_PJ_2" Then
                                If lv_chiuse = 0 Then
                                    item1.Visible = False
                                ElseIf lv_chiuse = 1 Then
                                    item1.Visible = False
                                ElseIf lv_chiuse = 2 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then

                                        Dim pic As Guna.UI2.WinForms.Guna2CirclePictureBox
                                        Dim panel As Panel = Guna2Panel1.Controls(item.Name)
                                        pic = pan.Controls(item.Name & "_PJ_1")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_PJ_2")
                                        pic.Visible = True



                                    End If
                                End If
                            End If

                            If item1.Name = item.Name & "_PJ_3" Then
                                If lv_chiuse = 0 Then
                                    item1.Visible = False
                                ElseIf lv_chiuse = 1 Then
                                    item1.Visible = False
                                ElseIf lv_chiuse = 2 Then
                                    item1.Visible = False
                                ElseIf lv_chiuse = 3 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then

                                        Dim pic As Guna.UI2.WinForms.Guna2CirclePictureBox
                                        Dim panel As Panel = Guna2Panel1.Controls(item.Name)
                                        pic = pan.Controls(item.Name & "_PJ_1")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_PJ_2")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_PJ_3")
                                        pic.Visible = True


                                    End If
                                End If
                            End If


                            '---------------------------------------------------------------------------------------------------------------------------------------


                            '----------------------------------------------------Gestione gemme VIP--------------------------------------------------------------

                            If item1.Name = item.Name & "_SW_1" Then
                                If lv_VIP = 0 Then
                                    item1.Visible = False
                                ElseIf lv_VIP = 1 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then
                                        item1.Visible = True
                                    End If
                                End If
                            End If

                            If item1.Name = item.Name & "_SW_2" Then
                                If lv_VIP = 0 Then
                                    item1.Visible = False
                                ElseIf lv_VIP = 1 Then
                                    item1.Visible = False
                                ElseIf lv_VIP = 2 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then

                                        Dim pic As Guna.UI2.WinForms.Guna2CirclePictureBox
                                        Dim panel As Panel = Guna2Panel1.Controls(item.Name)
                                        pic = pan.Controls(item.Name & "_SW_1")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_SW_2")
                                        pic.Visible = True


                                    End If
                                End If
                            End If

                            If item1.Name = item.Name & "_SW_3" Then
                                If lv_VIP = 0 Then
                                    item1.Visible = False
                                ElseIf lv_VIP = 1 Then
                                    item1.Visible = False
                                ElseIf lv_VIP = 2 Then
                                    item1.Visible = False
                                ElseIf lv_VIP = 3 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then

                                        Dim pic As Guna.UI2.WinForms.Guna2CirclePictureBox
                                        Dim panel As Panel = Guna2Panel1.Controls(item.Name)
                                        pic = pan.Controls(item.Name & "_SW_1")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_SW_2")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_SW_3")
                                        pic.Visible = True

                                    End If
                                End If
                            End If


                            '---------------------------------------------------------------------------------------------------------------------------------------


                            '----------------------------------------------------Gestione gemme SVILUPPO--------------------------------------------------------------

                            If item1.Name = item.Name & "_CC_1" Then
                                If lv_con = 0 Then
                                    item1.Visible = False
                                ElseIf lv_con = 1 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then
                                        item1.Visible = True
                                    End If
                                End If
                            End If

                            If item1.Name = item.Name & "_CC_2" Then
                                If lv_con = 0 Then
                                    item1.Visible = False
                                ElseIf lv_con = 1 Then
                                    item1.Visible = False
                                ElseIf lv_con = 2 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then

                                        Dim pic As Guna.UI2.WinForms.Guna2CirclePictureBox
                                        Dim panel As Panel = Guna2Panel1.Controls(item.Name)
                                        pic = pan.Controls(item.Name & "_CC_1")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_CC_2")
                                        pic.Visible = True


                                    End If
                                End If
                            End If

                            If item1.Name = item.Name & "_CC_3" Then
                                If lv_con = 0 Then
                                    item1.Visible = False
                                ElseIf lv_con = 1 Then
                                    item1.Visible = False
                                ElseIf lv_con = 2 Then
                                    item1.Visible = False
                                ElseIf lv_con = 3 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then

                                        Dim pic As Guna.UI2.WinForms.Guna2CirclePictureBox
                                        Dim panel As Panel = Guna2Panel1.Controls(item.Name)
                                        pic = pan.Controls(item.Name & "_CC_1")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_CC_2")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_CC_3")
                                        pic.Visible = True

                                    End If
                                End If
                            End If



                            '---------------------------------------------------------------------------------------------------------------------------------------


                            '----------------------------------------------------Gestione gemme SVILUPPO--------------------------------------------------------------

                            If item1.Name = item.Name & "_AR_1" Then
                                If lv_DS = 0 Then
                                    item1.Visible = False
                                ElseIf lv_DS = 1 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then
                                        item1.Visible = True
                                    End If
                                End If
                            End If

                            If item1.Name = item.Name & "_AR_2" Then
                                If lv_DS = 0 Then
                                    item1.Visible = False
                                ElseIf lv_DS = 1 Then
                                    item1.Visible = False
                                ElseIf lv_DS = 2 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then

                                        Dim pic As Guna.UI2.WinForms.Guna2CirclePictureBox
                                        Dim panel As Panel = Guna2Panel1.Controls(item.Name)
                                        pic = pan.Controls(item.Name & "_AR_1")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_AR_2")
                                        pic.Visible = True


                                    End If
                                End If
                            End If

                            If item1.Name = item.Name & "_AR_3" Then
                                If lv_DS = 0 Then
                                    item1.Visible = False
                                ElseIf lv_DS = 1 Then
                                    item1.Visible = False
                                ElseIf lv_DS = 2 Then
                                    item1.Visible = False
                                ElseIf lv_DS = 3 Then
                                    If nome_macchina = item.Name Or livello_user >= 10 Then

                                        Dim pic As Guna.UI2.WinForms.Guna2CirclePictureBox
                                        Dim panel As Panel = Guna2Panel1.Controls(item.Name)
                                        pic = pan.Controls(item.Name & "_AR_1")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_AR_2")
                                        pic.Visible = True
                                        pic = pan.Controls(item.Name & "_AR_3")
                                        pic.Visible = True

                                    End If
                                End If
                            End If


                            '---------------------------------------------------------------------------------------------------------------------------------------
                        Next


                    ElseIf item.Name = Vettore_stat_uffico_tecnico(i, 0) And item.Name = "Lorenzo" Then



                        If stelle_medie < 0.5 Then
                            Lorenzo_rating.Image = My.Resources.transparent1
                        ElseIf stelle_medie >= 0.5 And stelle_medie < 1 Then
                            Lorenzo_rating.Image = My.Resources.Star_0_5_tr
                        ElseIf stelle_medie >= 1 And stelle_medie < 1.5 Then
                            Lorenzo_rating.Image = My.Resources.Star_1_tr
                        ElseIf stelle_medie >= 1.5 And stelle_medie < 2 Then
                            Lorenzo_rating.Image = My.Resources.Star_1_5_tr
                        ElseIf stelle_medie >= 2 And stelle_medie < 2.5 Then
                            Lorenzo_rating.Image = My.Resources.Star_2_tr
                        ElseIf stelle_medie >= 2.5 And stelle_medie < 3 Then
                            Lorenzo_rating.Image = My.Resources.Star_2_5_tr
                        ElseIf stelle_medie >= 3 And stelle_medie < 3.5 Then
                            Lorenzo_rating.Image = My.Resources.Star_3_tr
                        ElseIf stelle_medie >= 3.5 And stelle_medie < 4 Then
                            Lorenzo_rating.Image = My.Resources.Star_3_5_tr
                        ElseIf stelle_medie >= 4 And stelle_medie < 4.5 Then
                            Lorenzo_rating.Image = My.Resources.Star_4_tr
                        ElseIf stelle_medie >= 4.5 And stelle_medie < 4.8 Then
                            Lorenzo_rating.Image = My.Resources.Star_4_5_tr
                        ElseIf stelle_medie >= 4.8 Then
                            Lorenzo_rating.Image = My.Resources.Star_5_tr
                        End If


                        Lorenzo_pending.Text = Sviluppi_waiting


                        calcola_exp(i, item.Name)



                        Lorenzo_level.Value = exp_val
                        Lorenzo_lv.Text = "lv." & level_val
                        Lorenzo_spec.Text = Math.Round(exp_tot, 0) & "/" & estremo_exp

                        Lorenzo_contributi.Text = Sviluppi_chiusi
                        Lorenzo_Databook.Text = Vettore_stat_uffico_tecnico(i, 5)

                        Lorenzo_Progress.Maximum = 60


                        'regolo colore barra PJ aperte
                        Lorenzo_Progress.Value = Sviluppi_waiting


                        If Lorenzo_Progress.Value <= 15 Then
                            Lorenzo_Progress.ProgressColor = Color.DarkGreen
                            Lorenzo_Progress.ProgressColor2 = Color.DarkGreen
                        ElseIf Lorenzo_Progress.Value > 15 And Lorenzo_Progress.Value <= 30 Then
                            Lorenzo_Progress.ProgressColor = Color.Gold
                            Lorenzo_Progress.ProgressColor2 = Color.Gold
                        ElseIf Lorenzo_Progress.Value > 30 And Lorenzo_Progress.Value <= 40 Then
                            Lorenzo_Progress.ProgressColor = Color.Orange
                            Lorenzo_Progress.ProgressColor2 = Color.Orange
                        ElseIf Lorenzo_Progress.Value > 40 Then
                            Lorenzo_Progress.ProgressColor = Color.Red
                            Lorenzo_Progress.ProgressColor = Color.Red
                        End If



                        '----------------------------------------------------Gestione gemme CURSE--------------------------------------------------------------

                        If nome_macchina = "Lorenzo" Or livello_user >= 10 Then

                            If lv_curse = 0 Then
                                Lorenzo_P_1.Visible = False
                            ElseIf lv_curse = 1 Then
                                If nome_macchina = "Lorenzo" Or livello_user >= 10 Then
                                    Lorenzo_P_1.Visible = True
                                End If
                            End If



                            If lv_curse = 0 Then
                                Lorenzo_P_2.Visible = False
                            ElseIf lv_curse = 1 Then
                                Lorenzo_P_2.Visible = False
                            ElseIf lv_curse = 2 Then
                                If nome_macchina = "Lorenzo" Or livello_user >= 10 Then
                                    Lorenzo_P_1.Visible = True
                                    Lorenzo_P_2.Visible = True
                                End If
                            End If



                            If lv_curse = 0 Then
                                Lorenzo_P_3.Visible = False
                            ElseIf lv_curse = 1 Then
                                Lorenzo_P_3.Visible = False
                            ElseIf lv_curse = 2 Then
                                Lorenzo_P_3.Visible = False
                            ElseIf lv_curse = 3 Then
                                If nome_macchina = "Lorenzo" Or livello_user >= 10 Then
                                    Lorenzo_P_1.Visible = True
                                    Lorenzo_P_2.Visible = True
                                    Lorenzo_P_3.Visible = True
                                End If
                            End If

                        End If


                        '---------------------------------------------------------------------------------------------------------------------------------------

                        '----------------------------------------------------Gestione gemme SVILUPPO--------------------------------------------------------------


                        If lv_con = 0 Then
                            Lorenzo_CC_1.Visible = False
                        ElseIf lv_con = 1 Then
                            If nome_macchina = "Lorenzo" Or livello_user >= 10 Then
                                Lorenzo_CC_1.Visible = True
                            End If
                        End If



                        If lv_con = 0 Then
                            Lorenzo_CC_2.Visible = False
                        ElseIf lv_con = 1 Then
                            Lorenzo_CC_2.Visible = False
                        ElseIf lv_con = 2 Then
                            If nome_macchina = "Lorenzo" Or livello_user >= 10 Then
                                Lorenzo_CC_1.Visible = True
                                Lorenzo_CC_2.Visible = True
                            End If
                        End If



                        If lv_con = 0 Then
                            Lorenzo_CC_3.Visible = False
                        ElseIf lv_con = 1 Then
                            Lorenzo_CC_3.Visible = False
                        ElseIf lv_con = 2 Then
                            Lorenzo_CC_3.Visible = False
                        ElseIf lv_con = 3 Then
                            If nome_macchina = "Lorenzo" Or livello_user >= 10 Then
                                Lorenzo_CC_1.Visible = True
                                Lorenzo_CC_2.Visible = True
                                Lorenzo_CC_3.Visible = True
                            End If
                        End If



                        '---------------------------------------------------------------------------------------------------------------------------------------


                        '----------------------------------------------------Gestione gemme SVILUPPO--------------------------------------------------------------


                        If lv_DS = 0 Then
                            Lorenzo_AR_1.Visible = False
                        ElseIf lv_DS = 1 Then
                            If nome_macchina = "Lorenzo" Or livello_user >= 10 Then
                                Lorenzo_AR_1.Visible = True
                            End If
                        End If



                        If lv_DS = 0 Then
                            Lorenzo_AR_2.Visible = False
                        ElseIf lv_DS = 1 Then
                            Lorenzo_AR_2.Visible = False
                        ElseIf lv_DS = 2 Then
                            If nome_macchina = "Lorenzo" Or livello_user >= 10 Then
                                Lorenzo_AR_1.Visible = True
                                Lorenzo_AR_2.Visible = True
                            End If
                        End If



                        If lv_DS = 0 Then
                            Lorenzo_AR_3.Visible = False
                        ElseIf lv_DS = 1 Then
                            Lorenzo_AR_3.Visible = False
                        ElseIf lv_DS = 2 Then
                            Lorenzo_AR_3.Visible = False
                        ElseIf lv_DS = 3 Then
                            If nome_macchina = "Lorenzo" Or livello_user >= 10 Then
                                Lorenzo_AR_1.Visible = True
                                Lorenzo_AR_2.Visible = True
                                Lorenzo_AR_3.Visible = True
                            End If
                        End If


                        '---------------------------------------------------------------------------------------------------------------------------------------



                    End If


                Next

                If hide_var = 1 Then
                    pan.Visible = False
                End If


            End If

        Next

    End Sub


    Public Sub Lettura_stat_Users()


        For i As Integer = 0 To numero_PJ_mail - 1

            Dim user_rating As String = ""
            Dim stato_rating As String = ""

            For u = 0 To 9

                'identifico il nome dello user
                ricerca_valore_tab1("cbx_Owner")
                user_rating = all_tables(vettore_controllo_mail(i, 3) + 1, pos_vect1)

                If user_rating = Vettore_stat_uffico_tecnico(u, 0) Then

                    'identifico il nome dello stato
                    ricerca_valore_tab1("cbx_Stato")
                    stato_rating = all_tables(vettore_controllo_mail(i, 4) + 1, pos_vect1)

                    If stato_rating = "In lavorazione" Or stato_rating = "In codifica" Or stato_rating = "In attesa risposta cliente" Then
                        Vettore_stat_uffico_tecnico(u, 1) = Vettore_stat_uffico_tecnico(u, 1) + 1
                    Else
                        Vettore_stat_uffico_tecnico(u, 2) = Vettore_stat_uffico_tecnico(u, 2) + 1
                    End If


                End If

            Next



        Next


    End Sub





    Public Sub totale_PJ_vipDesigner()



        attivi = 0

        For i = 0 To Num_ID - 1

            Dim User_ID As Integer = i
            Dim text_DWN As String
            Dim safe_area As Integer = 0
            Dim atex_area As Integer = 0
            Dim azienda_star As String

            text_DWN = All_client_bk(i)

            Dim nome As String = text_DWN.Substring(text_DWN.IndexOf("_") + 1, text_DWN.IndexOf("_Cognome") - 5)
            Dim cognome As String = text_DWN.Substring(text_DWN.IndexOf("_Cognome") + 9)
            cognome = cognome.Substring(0, cognome.IndexOf("_Mail"))
            Dim Azienda As String = text_DWN.Substring(text_DWN.IndexOf("_Azienda") + 9)
            Azienda = Azienda.Substring(0, Azienda.IndexOf("_Stato"))
            Dim Nazione As String = text_DWN.Substring(text_DWN.IndexOf("_Stato") + 7)
            Nazione = Nazione.Substring(0, Nazione.IndexOf("_Password"))
            Dim Email As String = text_DWN.Substring(text_DWN.IndexOf("_Mail") + 6)
            Email = Email.Substring(0, Email.IndexOf("_Azienda"))
            Dim Target As String = text_DWN.Substring(text_DWN.IndexOf("_Password") + 10)
            Target = Target.Substring(0, Target.IndexOf("_Autorizzazione"))
            client_target(i) = Target


            client_ATX(i) = text_DWN.Substring(text_DWN.IndexOf("_ATX") + 4, text_DWN.IndexOf("_SF") - (text_DWN.IndexOf("_ATX") + 4))
            client_SF(i) = text_DWN.Substring(text_DWN.IndexOf("_SF") + 3, text_DWN.IndexOf("_IND") - (text_DWN.IndexOf("_SF") + 3))




            For o = 0 To 9

                Try
                    azienda_star = Azienda(0) & Azienda(1) & Azienda(2)
                Catch ex As Exception
                    azienda_star = ""
                End Try


                If nome = Vettore_stat_uffico_tecnico(o, 0) And azienda_star = "Vip" Then
                    safe_area = client_SF(i)
                    atex_area = client_ATX(i)
                    Vettore_stat_uffico_tecnico(o, 3) = (atex_area + safe_area).ToString
                End If

            Next


        Next i







    End Sub


    Public Sub azzera_user()

        '-----------------------------------------azzero i contatori degli utenti------------------------------------------------
        Vettore_stat_uffico_tecnico(0, 0) = "Fausto"
        Vettore_stat_uffico_tecnico(1, 0) = "Paolo"
        Vettore_stat_uffico_tecnico(2, 0) = "Stefano"
        Vettore_stat_uffico_tecnico(3, 0) = "Roberto"
        Vettore_stat_uffico_tecnico(4, 0) = "Alberto"
        Vettore_stat_uffico_tecnico(5, 0) = "Lorenzo"
        Vettore_stat_uffico_tecnico(6, 0) = "Riccardo"
        Vettore_stat_uffico_tecnico(7, 0) = "???"
        Vettore_stat_uffico_tecnico(8, 0) = "???"
        Vettore_stat_uffico_tecnico(9, 0) = "???"

        For i = 0 To 9
            For j = 1 To 6
                Vettore_stat_uffico_tecnico(i, j) = "0"
            Next
        Next
        '------------------------------------------------------------------------------------------------------------------------
    End Sub




    Public Sub Datasheet_stat()

        Dim numero_modifiche As Integer
        Dim stringa_modifiche As String
        Dim appoggio_modifiche As String

        For i = 0 To numero_DS - 1

            numero_modifiche = 0
            For j = 0 To DS_totale_dati(i, 85).Length - 1
                If DS_totale_dati(i, 85)(j) = ";" Then
                    numero_modifiche = numero_modifiche + 1
                End If
            Next

            stringa_modifiche = DS_totale_dati(i, 85)

            If numero_modifiche = 0 Then

                For o = 0 To 9
                    If stringa_modifiche = Vettore_stat_uffico_tecnico(o, 0) Then
                        Vettore_stat_uffico_tecnico(o, 5) = Vettore_stat_uffico_tecnico(o, 5) + 1
                    End If
                Next


            Else

                For j = 0 To numero_modifiche


                    If j = numero_modifiche Then

                        For o = 0 To 9
                            If stringa_modifiche = Vettore_stat_uffico_tecnico(o, 0) Then
                                Vettore_stat_uffico_tecnico(o, 5) = Vettore_stat_uffico_tecnico(o, 5) + 1
                            End If
                        Next

                    Else

                        appoggio_modifiche = stringa_modifiche.Substring(0, stringa_modifiche.IndexOf(";"))

                        For o = 0 To 9
                            If appoggio_modifiche = Vettore_stat_uffico_tecnico(o, 0) Then
                                Vettore_stat_uffico_tecnico(o, 5) = Vettore_stat_uffico_tecnico(o, 5) + 1
                            End If
                        Next


                        stringa_modifiche = stringa_modifiche.Substring(stringa_modifiche.IndexOf(";") + 1, stringa_modifiche.Length - stringa_modifiche.IndexOf(";") - 1)

                    End If


                Next

            End If





        Next





    End Sub



    Public Sub calcola_exp(indice, nome)




        exp_PJ_aperte = Vettore_stat_uffico_tecnico(indice, 1)
        exp_PJ_chiuse = Vettore_stat_uffico_tecnico(indice, 2)
        exp_VipDes = Vettore_stat_uffico_tecnico(indice, 3)
        exp_contributi = Vettore_stat_uffico_tecnico(indice, 4)
        exp_DS = Vettore_stat_uffico_tecnico(indice, 5)


        boost_aperte = 0
        lv_aperte = 0

        boost_chiuse = 0
        lv_chiuse = 0

        boost_VIP = 0
        lv_VIP = 0

        boost_con = 0
        lv_con = 0

        boost_DS = 0
        lv_DS = 0


        curse_dev = 0
        lv_curse = 0

        If nome <> "Lorenzo" Then

            'identifico i livelli dei boost

            'APERTE
            If exp_PJ_aperte >= 10 And exp_PJ_aperte < 15 Then
                boost_aperte = 1
                lv_aperte = 1
            ElseIf exp_PJ_aperte >= 15 And exp_PJ_aperte < 25 Then
                boost_aperte = 3
                lv_aperte = 2
            ElseIf exp_PJ_aperte >= 25 Then
                boost_aperte = 5
                lv_aperte = 3
            End If


            'CHIUSE
            If exp_PJ_chiuse >= 60 And exp_PJ_chiuse < 100 Then
                boost_chiuse = 1
                lv_chiuse = 1
            ElseIf exp_PJ_chiuse >= 100 And exp_PJ_chiuse < 300 Then
                boost_chiuse = 2
                lv_chiuse = 2
            ElseIf exp_PJ_chiuse >= 300 Then
                boost_chiuse = 3
                lv_chiuse = 3
            End If

            'VIP
            If exp_VipDes >= 150 And exp_VipDes < 400 Then
                boost_VIP = 0.2
                lv_VIP = 1
            ElseIf exp_VipDes >= 400 And exp_VipDes < 1500 Then
                boost_VIP = 0.4
                lv_VIP = 2
            ElseIf exp_VipDes >= 1500 Then
                boost_VIP = 0.6
                lv_VIP = 3
            End If


            'con
            If exp_contributi >= 50 And exp_contributi < 150 Then
                boost_con = 0.2
                lv_con = 1
            ElseIf exp_contributi >= 150 And exp_contributi < 400 Then
                boost_con = 0.4
                lv_con = 2
            ElseIf exp_contributi >= 400 Then
                boost_con = 0.6
                lv_con = 3
            End If


            'ds
            If exp_DS >= 50 And exp_DS < 150 Then
                boost_DS = 0.2
                lv_DS = 1
            ElseIf exp_DS >= 150 And exp_DS < 400 Then
                boost_DS = 0.4
                lv_DS = 2
            ElseIf exp_DS >= 400 Then
                boost_DS = 0.6
                lv_DS = 3
            End If


            If exp_PJ_chiuse = 0 Or exp_PJ_aperte = 0 Then
                exp_tot = exp_PJ_chiuse * (5 + boost_chiuse) - (2 + (boost_aperte)) * exp_PJ_aperte + (0.4 + boost_VIP) * exp_VipDes + (0.6 + boost_con) * exp_contributi + (0.5 + boost_DS) * exp_DS

            Else
                exp_tot = exp_PJ_chiuse * (5 + boost_chiuse) - (2 + (boost_aperte * (1 + (exp_PJ_chiuse / exp_PJ_aperte)))) * exp_PJ_aperte + (0.4 + boost_VIP) * exp_VipDes + (0.6 + boost_con) * exp_contributi + (0.5 + boost_DS) * exp_DS

            End If



            definisci_lv(nome)




        Else

            'con
            If Sviluppi_chiusi >= 50 And Sviluppi_chiusi < 150 Then
                boost_con = 0.2
                lv_con = 1
            ElseIf Sviluppi_chiusi >= 150 And Sviluppi_chiusi < 400 Then
                boost_con = 0.4
                lv_con = 2
            ElseIf Sviluppi_chiusi >= 400 Then
                boost_con = 0.6
                lv_con = 3
            End If


            'ds
            If exp_DS >= 50 And exp_DS < 150 Then
                boost_DS = 0.2
                lv_DS = 1
            ElseIf exp_DS >= 150 And exp_DS < 400 Then
                boost_DS = 0.4
                lv_DS = 2
            ElseIf exp_DS >= 400 Then
                boost_DS = 0.6
                lv_DS = 3
            End If

            'curse
            If Sviluppi_waiting >= 20 And Sviluppi_waiting < 40 Then
                curse_dev = 1
                lv_curse = 1
            ElseIf Sviluppi_waiting >= 40 And Sviluppi_waiting < 60 Then
                curse_dev = 3
                lv_curse = 2
            ElseIf Sviluppi_waiting >= 60 Then
                curse_dev = 5
                lv_curse = 3
            End If



            If exp_contributi = 0 Or Sviluppi_waiting = 0 Then
                exp_tot = (1 + boost_con * (0.4 + (stelle_medie / 5))) * Sviluppi_chiusi + (0.5 + boost_DS) * exp_DS - (3.5 + curse_dev) * Sviluppi_waiting
            Else
                exp_tot = (1 + boost_con * (0.4 + (stelle_medie / 5))) * Sviluppi_chiusi + (0.5 + boost_DS) * exp_DS - (3.5 + curse_dev * (1 + (exp_contributi / Sviluppi_waiting))) * Sviluppi_waiting
            End If





            definisci_lv(nome)

        End If

    End Sub


    Public Sub definisci_lv(nome)

        exp_estremi(0, 0) = -10000
        exp_estremi(0, 1) = 5

        exp_estremi(1, 0) = 5
        exp_estremi(1, 1) = 20

        exp_estremi(2, 0) = 20
        exp_estremi(2, 1) = 50

        exp_estremi(3, 0) = 50
        exp_estremi(3, 1) = 100

        exp_estremi(4, 0) = 100
        exp_estremi(4, 1) = 200

        exp_estremi(5, 0) = 200
        exp_estremi(5, 1) = 350

        exp_estremi(6, 0) = 350
        exp_estremi(6, 1) = 600

        exp_estremi(7, 0) = 600
        exp_estremi(7, 1) = 1000

        exp_estremi(8, 0) = 1000
        exp_estremi(8, 1) = 1500


        exp_estremi(9, 0) = 1500
        exp_estremi(9, 1) = 2200

        exp_estremi(10, 0) = 2200
        exp_estremi(10, 1) = 3000

        exp_estremi(11, 0) = 3000
        exp_estremi(11, 1) = 4000

        exp_estremi(12, 0) = 4000
        exp_estremi(12, 1) = 5500

        exp_estremi(13, 0) = 5500
        exp_estremi(13, 1) = 7500

        exp_estremi(14, 0) = 5500
        exp_estremi(14, 1) = 10500

        exp_estremi(15, 0) = 10500
        exp_estremi(15, 1) = 14000


        exp_estremi(16, 0) = 14000
        exp_estremi(16, 1) = 20000

        exp_estremi(17, 0) = 20000
        exp_estremi(17, 1) = 30000


        'trova livello
        For i = 0 To 99

            If exp_tot >= exp_estremi(i, 0) And exp_tot <= exp_estremi(i, 1) Then


                If exp_tot <= 0 Then
                    exp_val = exp_tot
                    level_val = 0
                    estremo_exp = 5
                Else
                    exp_val = (exp_tot - exp_estremi(i, 0)) / (exp_estremi(i, 1) - exp_estremi(i, 0)) * 100
                    estremo_exp = exp_estremi(i, 1)
                    level_val = i + 1
                End If


            End If

        Next

        If nome = nome_macchina And pre_carica_lv = 1 Then

            Skill_lv(level_val)

        End If


        If pre_carica_lv = 0 Then
            livello_user = level_val
        End If

        If livello_user >= 3 Then
            btn_hide.Visible = True
        End If

        'For i = 1 To 11
        '    exp_estremi(i, 1) = 2 ^ i + 5
        '    exp_estremi(i, 0) = exp_estremi(i - 1, 1)
        'Next


    End Sub


    Public Sub Load_base_rating()

        'nascondo tutti gli avatar

        For Each item As Control In Guna2Panel1.Controls

            If item.GetType Is GetType(System.Windows.Forms.Panel) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                Dim pan As System.Windows.Forms.Panel = DirectCast(item, System.Windows.Forms.Panel)

                If pan.Name <> nome_macchina Then

                    pan.Visible = False


                    'invisibili immagini gemme grandi
                    For Each item1 As Control In pan.Controls
                        If item1.Name = item.Name & "_X" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_PJ" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_SW" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_CC" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_AR" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_P" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_rating" Then
                            item1.Visible = False
                        End If
                    Next

                    ''invisibili immagini gemme piccole
                    For Each item1 As Control In pan.Controls
                        If item1.Name = item.Name & "_X_1" Or item1.Name = item.Name & "_X_2" Or item1.Name = item.Name & "_X_3" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_PJ_1" Or item1.Name = item.Name & "_PJ_2" Or item1.Name = item.Name & "_PJ_3" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_SW_1" Or item1.Name = item.Name & "_SW_2" Or item1.Name = item.Name & "_SW_3" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_CC_1" Or item1.Name = item.Name & "_CC_2" Or item1.Name = item.Name & "_CC_3" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_AR_1" Or item1.Name = item.Name & "_AR_2" Or item1.Name = item.Name & "_AR_3" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_P_1" Or item1.Name = item.Name & "_P_2" Or item1.Name = item.Name & "_P_3" Then
                            item1.Visible = False
                        End If
                    Next

                    'invisibili le text
                    For Each item1 As Control In pan.Controls
                        If item1.Name = item.Name & "_PJ_aperte" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_PJ_chiuse" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_VipDesigner" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_contributi" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_Databook" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_pending" Then
                            item1.Visible = False
                        End If
                    Next

                    'nascondo le barre

                    For Each item1 As Control In pan.Controls
                        If item1.Name = item.Name & "_Progress" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_lv" Then
                            item1.Visible = False
                        ElseIf item1.Name = item.Name & "_spec" Then
                            item1.Visible = False
                        End If

                        Dim load_bar As Guna.UI2.WinForms.Guna2CircleProgressBar
                        load_bar = pan.Controls(item.Name & "_Progress")
                        Dim exp_bar As Guna.UI2.WinForms.Guna2CircleProgressBar
                        exp_bar = load_bar.Controls(item.Name & "_level")

                        exp_bar.Visible = False

                    Next




                Else

                    If spostamento_effettuato = 1 Then
                        Dim x_location As Integer = pan.Location.X
                        Dim y_location As Integer = pan.Location.Y



                        pan.Visible = True
                        pan.Location = New System.Drawing.Point(230, 48)

                        Fausto.Location = New System.Drawing.Point(x_location, y_location)

                        spostamento_effettuato = 0
                    End If
                End If


                End If

        Next



    End Sub





    Public Sub Skill_lv(livello)


        'LIVELLO 3
        If livello >= 3 Then

            For Each item As Control In Guna2Panel1.Controls

                If item.GetType Is GetType(System.Windows.Forms.Panel) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                    Dim pan As System.Windows.Forms.Panel = DirectCast(item, System.Windows.Forms.Panel)

                    If hide_var = 0 Then
                        pan.Visible = True
                    End If


                    For Each item1 As Control In pan.Controls

                            If item1.Name = item.Name & "_Progress" Then
                                item1.Visible = True
                            End If

                            Dim load_bar As Guna.UI2.WinForms.Guna2CircleProgressBar
                            load_bar = pan.Controls(item.Name & "_Progress")
                            Dim exp_bar As Guna.UI2.WinForms.Guna2CircleProgressBar
                            exp_bar = load_bar.Controls(item.Name & "_level")


                            exp_bar.Visible = True

                        Next

                    End If

            Next

        End If


        'LIVELLO 4
        If livello >= 4 Then

            For Each item As Control In Guna2Panel1.Controls

                If item.GetType Is GetType(System.Windows.Forms.Panel) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                    Dim pan As System.Windows.Forms.Panel = DirectCast(item, System.Windows.Forms.Panel)

                    If hide_var = 0 Then
                        pan.Visible = True
                    End If

                    For Each item1 As Control In pan.Controls

                        If item1.Name = item.Name & "_lv" Then
                            item1.Visible = True
                        End If


                    Next

                End If

            Next

        End If



        'LIVELLO 5
        If livello >= 5 Then

            For Each item As Control In Guna2Panel1.Controls

                If item.GetType Is GetType(System.Windows.Forms.Panel) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                    Dim pan As System.Windows.Forms.Panel = DirectCast(item, System.Windows.Forms.Panel)

                    If hide_var = 0 Then
                        pan.Visible = True
                    End If

                    For Each item1 As Control In pan.Controls

                        If item1.Name = item.Name & "_spec" Then
                            item1.Visible = True
                        End If


                    Next

                End If

            Next

        End If

        'LIVELLO 6
        If livello >= 6 Then

            For Each item As Control In Guna2Panel1.Controls

                If item.GetType Is GetType(System.Windows.Forms.Panel) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                    Dim pan As System.Windows.Forms.Panel = DirectCast(item, System.Windows.Forms.Panel)

                    If hide_var = 0 Then
                        pan.Visible = True
                    End If

                    For Each item1 As Control In pan.Controls


                        If item1.Name = item.Name & "_PJ_aperte" Then
                            item1.Visible = True
                        End If
                        If item1.Name = item.Name & "_X" Then
                            item1.Visible = True
                        End If


                        If item1.Name = "Lorenzo_pending" Then
                            item1.Visible = True
                        End If
                        If item1.Name = "Lorenzo_P" Then
                            item1.Visible = True
                        End If

                        If item1.Name = "Lorenzo_rating" Then
                            item1.Visible = True
                        End If

                    Next

                End If

            Next

        End If

        'LIVELLO 7
        If livello >= 7 Then

            For Each item As Control In Guna2Panel1.Controls

                If item.GetType Is GetType(System.Windows.Forms.Panel) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                    Dim pan As System.Windows.Forms.Panel = DirectCast(item, System.Windows.Forms.Panel)

                    If hide_var = 0 Then
                        pan.Visible = True
                    End If

                    For Each item1 As Control In pan.Controls


                        If item1.Name = item.Name & "_PJ_chiuse" Then
                            item1.Visible = True
                        End If
                        If item1.Name = item.Name & "_PJ" Then
                            item1.Visible = True
                        End If


                    Next

                End If

            Next

        End If

        'LIVELLO 8
        If livello >= 8 Then

            For Each item As Control In Guna2Panel1.Controls

                If item.GetType Is GetType(System.Windows.Forms.Panel) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                    Dim pan As System.Windows.Forms.Panel = DirectCast(item, System.Windows.Forms.Panel)

                    If hide_var = 0 Then
                        pan.Visible = True
                    End If

                    For Each item1 As Control In pan.Controls


                        If item1.Name = item.Name & "_VipDesigner" Then
                            item1.Visible = True
                        End If
                        If item1.Name = item.Name & "_SW" Then
                            item1.Visible = True
                        End If


                    Next

                End If

            Next

        End If

        'LIVELLO 9
        If livello >= 9 Then

            For Each item As Control In Guna2Panel1.Controls

                If item.GetType Is GetType(System.Windows.Forms.Panel) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                    Dim pan As System.Windows.Forms.Panel = DirectCast(item, System.Windows.Forms.Panel)

                    pan.Visible = True

                    For Each item1 As Control In pan.Controls


                        If item1.Name = item.Name & "_contributi" Then
                            item1.Visible = True
                        End If
                        If item1.Name = item.Name & "_CC" Then
                            item1.Visible = True
                        End If


                    Next

                End If

            Next

        End If

        'LIVELLO 10
        If livello >= 10 Then

            For Each item As Control In Guna2Panel1.Controls

                If item.GetType Is GetType(System.Windows.Forms.Panel) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                    Dim pan As System.Windows.Forms.Panel = DirectCast(item, System.Windows.Forms.Panel)

                    pan.Visible = True

                    For Each item1 As Control In pan.Controls


                        If item1.Name = item.Name & "_Databook" Then
                            item1.Visible = True
                        End If
                        If item1.Name = item.Name & "_AR" Then
                            item1.Visible = True
                        End If


                    Next

                End If

            Next

        End If



    End Sub

    Public Sub calcola_livello_user()


        pre_carica_lv = 0

        For i = 0 To 9
            If Vettore_stat_uffico_tecnico(i, 0) = nome_macchina Then
                calcola_exp(i, nome_macchina)
            End If
        Next


        pre_carica_lv = 1



    End Sub

    Private Sub pic_attesa_conv_Click(sender As Object, e As EventArgs) Handles pic_attesa_conv.Click
        Form1.btnDeveloper.PerformClick()
    End Sub

    Private Sub Label_convalide_Click(sender As Object, e As EventArgs) Handles Label_convalide.Click
        Form1.btnDeveloper.PerformClick()
    End Sub

    Private Sub pic_attesa_conv_MouseHover(sender As Object, e As EventArgs) Handles pic_attesa_conv.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub pic_attesa_conv_MouseLeave(sender As Object, e As EventArgs) Handles pic_attesa_conv.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub Label_convalide_MouseHover(sender As Object, e As EventArgs) Handles Label_convalide.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub Label_convalide_MouseLeave(sender As Object, e As EventArgs) Handles Label_convalide.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub



    Private Sub Guna2CirclePictureBox1_Click(sender As Object, e As EventArgs) Handles btn_hide.Click

        If hide_var = 0 Then
            hide_var = 1
            btn_hide.Image = My.Resources.show

            'nascondo tutti gli avatar

            For Each item As Control In Guna2Panel1.Controls

                If item.GetType Is GetType(System.Windows.Forms.Panel) Then 'cerco tutte le groupbox dentro al Guna2GroupBox4

                    Dim pan As System.Windows.Forms.Panel = DirectCast(item, System.Windows.Forms.Panel)


                    pan.Visible = False

                End If

            Next

        Else

            hide_var = 0
            btn_hide.Image = My.Resources.hidden

            update_players()
        End If
    End Sub

    Private Sub btn_hide_MouseHover(sender As Object, e As EventArgs) Handles btn_hide.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub btn_hide_MouseLeave(sender As Object, e As EventArgs) Handles btn_hide.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub


    Public Sub warning_super_late()


        Dim nome_user As String = ""

        Giorni_PJ_super_late = 0
        PJ_super_late = ""

        For i As Integer = 0 To numero_PJ_mail - 1

            'identifico il nome dello user
            ricerca_valore_tab1("cbx_Owner")
            nome_user = all_tables(vettore_controllo_mail(i, 3) + 1, pos_vect1)

            Dim stato_PJ_ref As String
            ricerca_valore_tab1("cbx_Stato")
            stato_PJ_ref = all_tables(vettore_controllo_mail(i, 4) + 1, pos_vect1)



            If nome_macchina = nome_user Then

                If (vettore_controllo_mail(i, 7) > Giorni_PJ_super_late) And (stato_PJ_ref <> "Chiusa" And stato_PJ_ref <> "Codificata") Then

                    Giorni_PJ_super_late = vettore_controllo_mail(i, 7)
                    PJ_super_late = vettore_controllo_mail(i, 0)
                    Rev_PJ_super_late = vettore_controllo_mail(i, 2)
                    data_PJ_super_late = vettore_controllo_mail(i, 1)
                    ordine_ric = vettore_controllo_mail(i, 8)

                End If

            End If

        Next



        If Giorni_PJ_super_late > 10 Then

            If nome_macchina <> "Lorenzo" Then
                Label_warning.Visible = True
                pic_warning_PJ.Visible = True
                Label_warning.Text = PJ_super_late & " in attesa da " & Giorni_PJ_super_late & " giorni!"
            End If

        Else

            Label_warning.Visible = False
            pic_warning_PJ.Visible = False

        End If


    End Sub


    Public Sub open_PJ_super_late()

        mod_PJ_DS = 1


        numero_progetti = Rev_PJ_super_late


        If ordine_ric = "SI" Then
            OrdineRic = 1
        ElseIf ordine_ric = "NO" Then
            OrdineRic = 0
        End If

        Form1.btnParametri.Visible = True

        data_progetto = data_PJ_super_late


        'Il vettore prog_rev(i,0) che contiene i nomi dei progetti non è allineato con la datagrid quindi devo ricercare la posizione

        For i = 0 To 9999
            If prog_rev(i, 0) = PJ_super_late Then
                posizione_progetto = i
                i = 10000
            End If
        Next


        Lettura_riga1(Rev_PJ_super_late)

        Form1.btnParametri.PerformClick()


    End Sub

    Private Sub Label_warning_Click(sender As Object, e As EventArgs) Handles Label_warning.Click
        open_PJ_super_late()
    End Sub

    Private Sub pic_warning_PJ_Click(sender As Object, e As EventArgs) Handles pic_warning_PJ.Click
        open_PJ_super_late()
    End Sub

    Private Sub Label_warning_MouseHover(sender As Object, e As EventArgs) Handles Label_warning.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub Label_warning_MouseLeave(sender As Object, e As EventArgs) Handles Label_warning.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub pic_warning_PJ_MouseHover(sender As Object, e As EventArgs) Handles pic_warning_PJ.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub pic_warning_PJ_MouseLeave(sender As Object, e As EventArgs) Handles pic_warning_PJ.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub




    Public Sub attesa_convalida()



        If nome_macchina <> "Lorenzo" Then


            If numero_convalide_attese > 0 Then


                pic_attesa_conv.Visible = True
                Label_convalide.Visible = True

                If numero_convalide_attese = 1 Then
                    Label_convalide.Text = numero_convalide_attese & " Sviluppo da convalidare!"
                Else
                    Label_convalide.Text = numero_convalide_attese & " Sviluppi da convalidare!"
                End If


                'posizione warning PJ SUPER LATE
                pic_warning_PJ.Location = New System.Drawing.Point(519, 4)
                Label_warning.Location = New System.Drawing.Point(560, 16)

                'posizione warning sviluppi
                pic_attesa_conv.Location = New System.Drawing.Point(259, 4)
                Label_convalide.Location = New System.Drawing.Point(299, 16)

            Else

                pic_attesa_conv.Visible = False
                Label_convalide.Visible = False

                'posizione warning PJ SUPER LATE
                pic_warning_PJ.Location = New System.Drawing.Point(259, 4)
                Label_warning.Location = New System.Drawing.Point(299, 16)

            End If


        Else


            Label_convalide.Location = New System.Drawing.Point(300, 17)
            pic_attesa_conv.Location = New System.Drawing.Point(259, 5)


            pic_warning_PJ.Visible = False
            Label_warning.Visible = False

            If Sviluppi_waiting > 0 Then

                pic_attesa_conv.Visible = True
                Label_convalide.Visible = True

                Label_convalide.Text = Sviluppi_waiting & " In attesa di sviluppo!"


            End If



        End If


        'If nome_macchina <> "Lorenzo" Then

        '    pic_attesa_conv.Visible = False
        '    Label_convalide.Visible = False


        '    If numero_convalide_attese > 0 Then

        '        Panel_convalida.Visible = True

        '        If numero_convalide_attese = 1 Then
        '            Label_convalida_new.Text = numero_convalide_attese & " sviluppo cha hai richiesto è stato ultimato, dai un'occhiata!"
        '        Else
        '            Label_convalida_new.Text = numero_convalide_attese & " sviluppi cha hai richiesto sono stati ultimati, dai un'occhiata!"
        '        End If

        '        Select Case numero_convalide_attese

        '            Case 1
        '                SV1.Visible = True
        '            Case 2
        '                SV1.Visible = True
        '                SV2.Visible = True
        '            Case 3
        '                SV1.Visible = True
        '                SV2.Visible = True
        '                SV3.Visible = True
        '            Case 4
        '                SV1.Visible = True
        '                SV2.Visible = True
        '                SV3.Visible = True
        '                SV4.Visible = True
        '            Case 5
        '                SV1.Visible = True
        '                SV2.Visible = True
        '                SV3.Visible = True
        '                SV4.Visible = True
        '                SV5.Visible = True
        '            Case 6
        '                SV1.Visible = True
        '                SV2.Visible = True
        '                SV3.Visible = True
        '                SV4.Visible = True
        '                SV5.Visible = True
        '                SV6.Visible = True
        '            Case 7
        '                SV1.Visible = True
        '                SV2.Visible = True
        '                SV3.Visible = True
        '                SV4.Visible = True
        '                SV5.Visible = True
        '                SV6.Visible = True
        '                SV7.Visible = True
        '            Case 8
        '                SV1.Visible = True
        '                SV2.Visible = True
        '                SV3.Visible = True
        '                SV4.Visible = True
        '                SV5.Visible = True
        '                SV6.Visible = True
        '                SV7.Visible = True
        '                SV8.Visible = True
        '            Case 9
        '                SV1.Visible = True
        '                SV2.Visible = True
        '                SV3.Visible = True
        '                SV4.Visible = True
        '                SV5.Visible = True
        '                SV6.Visible = True
        '                SV7.Visible = True
        '                SV8.Visible = True
        '                SV9.Visible = True
        '            Case 10
        '                SV1.Visible = True
        '                SV2.Visible = True
        '                SV3.Visible = True
        '                SV4.Visible = True
        '                SV5.Visible = True
        '                SV6.Visible = True
        '                SV7.Visible = True
        '                SV8.Visible = True
        '                SV9.Visible = True
        '                SV10.Visible = True
        '            Case 11
        '                SV1.Visible = True
        '                SV2.Visible = True
        '                SV3.Visible = True
        '                SV4.Visible = True
        '                SV5.Visible = True
        '                SV6.Visible = True
        '                SV7.Visible = True
        '                SV8.Visible = True
        '                SV9.Visible = True
        '                SV10.Visible = True
        '                SV11.Visible = True
        '            Case 12
        '                SV1.Visible = True
        '                SV2.Visible = True
        '                SV3.Visible = True
        '                SV4.Visible = True
        '                SV5.Visible = True
        '                SV6.Visible = True
        '                SV7.Visible = True
        '                SV8.Visible = True
        '                SV9.Visible = True
        '                SV10.Visible = True
        '                SV11.Visible = True
        '                SV12.Visible = True
        '        End Select


        '    Else

        '        Panel_convalida.Visible = False

        '    End If


        'Else

        '    pic_warning_PJ.Visible = False
        '    Label_warning.Visible = False

        '    pic_attesa_conv.Location = New System.Drawing.Point(259, 5)
        '    Label_convalide.Location = New System.Drawing.Point(300, 17)

        '    If Sviluppi_waiting > 0 Then

        '        pic_attesa_conv.Visible = True
        '        Label_convalide.Visible = True

        '        Label_convalide.Text = Sviluppi_waiting & " In attesa di sviluppo!"

        '    End If

        'End If

    End Sub

    Private Sub pic_attesa_conv_new_Click(sender As Object, e As EventArgs) Handles pic_attesa_conv_new.Click
        Form1.btnDeveloper.PerformClick()
    End Sub

    Private Sub Label_convalida_new_Click(sender As Object, e As EventArgs) Handles Label_convalida_new.Click
        Form1.btnDeveloper.PerformClick()
    End Sub



    Private Sub Label_convalida_new_MouseHover(sender As Object, e As EventArgs) Handles Label_convalida_new.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub pic_attesa_conv_new_MouseHover(sender As Object, e As EventArgs) Handles pic_attesa_conv_new.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub Label_convalida_new_MouseLeave(sender As Object, e As EventArgs) Handles Label_convalida_new.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub pic_attesa_conv_new_MouseLeave(sender As Object, e As EventArgs) Handles pic_attesa_conv_new.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub


End Class

