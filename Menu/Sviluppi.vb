Public Class Sviluppi





    Private Sub Sviluppi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: questa riga di codice carica i dati nella tabella 'NewPJDataSet.cbx_Owner'. È possibile spostarla o rimuoverla se necessario.
        Me.Cbx_OwnerTableAdapter.Fill(Me.NewPJDataSet.cbx_Owner)


        Numero_colonne_Sviluppo1()
        Importa_Sviluppo1()


        filtro.SelectedItem = "Urgenza"
        Riempi_datagrid(filtro.SelectedItem)


        'filtro.SelectedIndex = 0'

        Label1.Text = Sviluppi_totali
        Label4.Text = Sviluppi_chiusi
        Label3.Text = Sviluppi_waiting
        Label10.Text = Sviluppi_attesa

        Diff_SV.SelectedIndex = 0



        If nome_macchina <> "Lorenzo" Then

            filtro.SelectedItem = "User"
            filtro_star.SelectedItem = nome_macchina


            User_request.Visible = False
            Diff_SV.Visible = False
            tbx_notaSV.Visible = False

            Label11.Visible = False
            Label12.Visible = False
            Label14.Visible = False



            filtro.Visible = False
            filtro_star.Visible = False
            btn_elimina_sviluppo.Visible = False
            btn_convalida_sviluppo.Size = New System.Drawing.Size(469, 36)

        Else


            User_request.Visible = True
            Diff_SV.Visible = True
            tbx_notaSV.Visible = True

            Label11.Visible = True
            Label12.Visible = True
            Label14.Visible = True


            Guna2Panel1.Visible = True
            Label11.Visible = True

        End If


        If nome_macchina = "Lorenzo" Then
            User_request.Text = "Paolo"
        Else
            User_request.Text = nome_macchina
        End If


    End Sub

    Public Sub ricarica_sviluppo()

        Numero_colonne_Sviluppo1()
        Importa_Sviluppo1()



        Riempi_datagrid(filtro.SelectedItem)


        Label1.Text = Sviluppi_totali
        Label4.Text = Sviluppi_chiusi
        Label3.Text = Sviluppi_waiting
        Label10.Text = Sviluppi_attesa
    End Sub


    Public Sub Riempi_datagrid(filtroA)


        Dim add_DS As Integer = 0
        Dim filtro1 As String

        Dim filtro2 As String
        Dim filtro_nome As String

        Guna2DataGridView1.Rows.Clear()




        For i As Integer = 0 To numero_Sviluppo - 1


            'filtro stato
            If filtro.SelectedItem = "Stato" Then
                For j = 0 To N_colonne_sviluppo
                    If Nome_colonne_sviluppo(j) = "cbx_statoSV" Then
                        filtro1 = DataBase_sviluppi(i, j - 1)
                    End If
                Next
            End If

            'filtro nome
            If filtro.SelectedItem = "User" Then
                For j = 0 To N_colonne_sviluppo
                    If Nome_colonne_sviluppo(j) = "tbx_nomeSV" Then
                        filtro1 = DataBase_sviluppi(i, j - 1)
                    End If
                Next
            End If

            'filtro software
            If filtro.SelectedItem = "Software" Then
                For j = 0 To N_colonne_sviluppo
                    If Nome_colonne_sviluppo(j) = "cbx_swSV" Then
                        filtro1 = DataBase_sviluppi(i, j - 1)
                    End If
                Next
            End If

            'filtro urgenza
            If filtro.SelectedItem = "Urgenza" Then
                For j = 0 To N_colonne_sviluppo
                    If Nome_colonne_sviluppo(j) = "cbx_urgenzaSV" Then
                        filtro1 = DataBase_sviluppi(i, j - 1)
                    End If
                Next
            End If


            'filtro difficoltà
            If filtro.SelectedItem = "Difficoltà" Then
                For j = 0 To N_colonne_sviluppo
                    If Nome_colonne_sviluppo(j) = "Diff_SV" Then
                        filtro1 = DataBase_sviluppi(i, j - 1)
                    End If
                Next
            End If


            For j = 0 To N_colonne_sviluppo
                If Nome_colonne_sviluppo(j) = "cbx_statoSV" Then
                    filtro2 = DataBase_sviluppi(i, j - 1)
                End If
            Next

            For j = 0 To N_colonne_sviluppo
                If Nome_colonne_sviluppo(j) = "tbx_nomeSV" Then
                    filtro_nome = DataBase_sviluppi(i, j - 1)
                End If
            Next



            If (filtro_star.SelectedItem = filtro1 And (filtro2 <> "Chiuso" Or (filtro.SelectedItem = "Stato" And filtro_star.SelectedItem = "Chiuso")) And (nome_macchina = filtro_nome Or nome_macchina = "Lorenzo")) Or filtroA = "All" Then

                Guna2DataGridView1.Rows.Add()

                'ID
                For j = 0 To N_colonne_sviluppo
                    If Nome_colonne_sviluppo(j) = "ID" Then
                        Guna2DataGridView1.Rows(add_DS).Cells(0).Value = DataBase_sviluppi(i, j - 1)
                    End If
                Next

                'Software
                For j = 0 To N_colonne_sviluppo
                    If Nome_colonne_sviluppo(j) = "cbx_swSV" Then
                        Guna2DataGridView1.Rows(add_DS).Cells(1).Value = DataBase_sviluppi(i, j - 1)
                    End If
                Next


                'Ambito
                For j = 0 To N_colonne_sviluppo
                    If Nome_colonne_sviluppo(j) = "cbx_ambitoSV" Then
                        Guna2DataGridView1.Rows(add_DS).Cells(2).Value = DataBase_sviluppi(i, j - 1)
                    End If
                Next

                'Ambito
                For j = 0 To N_colonne_sviluppo
                    If Nome_colonne_sviluppo(j) = "tbx_DescrizioneSV" Then
                        Guna2DataGridView1.Rows(add_DS).Cells(3).Value = DataBase_sviluppi(i, j - 1)
                    End If
                Next

                'Stato
                For j = 0 To N_colonne_sviluppo
                    If Nome_colonne_sviluppo(j) = "cbx_statoSV" Then
                        Guna2DataGridView1.Rows(add_DS).Cells(4).Value = DataBase_sviluppi(i, j - 1)

                        Dim strikethrough_style As New DataGridViewCellStyle
                        strikethrough_style.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)

                        If DataBase_sviluppi(i, j - 1) = "Attesa valutazione" Then
                            Guna2DataGridView1.Rows(add_DS).Cells(4).Style = strikethrough_style
                            Guna2DataGridView1.Rows(add_DS).Cells(4).Style.ForeColor = Color.Green
                        End If


                    End If
                Next

                'Creazione
                For j = 0 To N_colonne_sviluppo
                    If Nome_colonne_sviluppo(j) = "tbx_CreazioneSV" Then
                        Guna2DataGridView1.Rows(add_DS).Cells(5).Value = DataBase_sviluppi(i, j - 1)
                    End If
                Next


                'Attesa
                For j = 0 To N_colonne_sviluppo
                    If Nome_colonne_sviluppo(j) = "tbx_giorniSV" Then

                        If Guna2DataGridView1.Rows(add_DS).Cells(4).Value = "Chiusa" Then
                            Guna2DataGridView1.Rows(add_DS).Cells(6).Value = DataBase_sviluppi(i, j - 1)
                        Else

                            Dim date1 As Date = Convert.ToDateTime(Guna2DataGridView1.Rows(add_DS).Cells(5).Value)
                            Dim date2 As Date = Convert.ToDateTime(Today)
                            Dim delta_date As Integer = DateDiff(DateInterval.Day, date1, date2)

                            Guna2DataGridView1.Rows(add_DS).Cells(6).Value = delta_date

                        End If


                    End If
                Next

                'Voto
                For j = 0 To N_colonne_sviluppo
                    If Nome_colonne_sviluppo(j) = "tbx_valutazioneSV" Then

                        Select Case DataBase_sviluppi(i, j - 1)
                            Case "0"
                                Guna2DataGridView1.Rows(add_DS).Cells(7).Value = My.Resources.Resources.transparent1
                            Case "0,5"
                                Guna2DataGridView1.Rows(add_DS).Cells(7).Value = My.Resources.Resources.Star_0_5_tr
                            Case "1"
                                Guna2DataGridView1.Rows(add_DS).Cells(7).Value = My.Resources.Resources.Star_1_tr
                            Case "1,5"
                                Guna2DataGridView1.Rows(add_DS).Cells(7).Value = My.Resources.Resources.Star_1_5_tr
                            Case "2"
                                Guna2DataGridView1.Rows(add_DS).Cells(7).Value = My.Resources.Resources.Star_2_tr
                            Case "2,5"
                                Guna2DataGridView1.Rows(add_DS).Cells(7).Value = My.Resources.Resources.Star_2_5_tr
                            Case "3"
                                Guna2DataGridView1.Rows(add_DS).Cells(7).Value = My.Resources.Resources.Star_3_tr
                            Case "3,5"
                                Guna2DataGridView1.Rows(add_DS).Cells(7).Value = My.Resources.Resources.Star_3_5_tr
                            Case "4"
                                Guna2DataGridView1.Rows(add_DS).Cells(7).Value = My.Resources.Resources.Star_4_tr
                            Case "4,5"
                                Guna2DataGridView1.Rows(add_DS).Cells(7).Value = My.Resources.Resources.Star_4_5_tr
                            Case "5"
                                Guna2DataGridView1.Rows(add_DS).Cells(7).Value = My.Resources.Resources.Star_5_tr
                        End Select


                    End If
                Next

                'Voto
                For j = 0 To N_colonne_sviluppo

                    If Nome_colonne_sviluppo(j) = "tbx_immSV" Then

                        If DataBase_sviluppi(i, j - 1) <> "" Then
                            Guna2DataGridView1.Rows(add_DS).Cells(8).Value = My.Resources.Resources.clip
                        Else
                            Guna2DataGridView1.Rows(add_DS).Cells(8).Value = My.Resources.Resources.add_photo
                        End If


                    End If

                Next



                add_DS = add_DS + 1



            End If



        Next


        Dim indice_grid As Integer

        For i = 0 To Guna2DataGridView1.Rows.Count - 1

            For j = 0 To N_colonne_sviluppo

                indice_grid = 0

                For o = 0 To numero_Sviluppo - 1

                    If DataBase_sviluppi(o, 5) = Guna2DataGridView1.Rows(i).Cells(0).Value Then
                        indice_grid = o
                    End If

                Next



                DataBase_sviluppi_star(i, j) = DataBase_sviluppi(indice_grid, j)
            Next


        Next


        '--------------------------------------Carico la prima riga----------------------------------------------

        cbx_tbx_values()
        cbx_ambitoSV.SelectedItem = DataBase_sviluppi(0, 0)
        'check_values()
        'eccezione_motore_vip()
        '--------------------------------------------------------------------------------------------------------




        Puntatore_sviluppo = 0

        Try
            Guna2DataGridView1.FirstDisplayedScrollingRowIndex = Guna2DataGridView1.Rows(0).Index
            Guna2DataGridView1.Refresh()
            Guna2DataGridView1.CurrentCell = Guna2DataGridView1.Rows(0).Cells(1)
            Guna2DataGridView1.Rows(0).Selected = True
        Catch ex As Exception

        End Try

        Guna2DataGridView1.Select()



    End Sub






    Private Sub cbx_swSV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_swSV.SelectedIndexChanged
        cbx_ambitoSV.Items.Clear()

        If cbx_swSV.SelectedItem = "VipEcosystem" Then

            cbx_ambitoSV.Items.Add("PJ")
            cbx_ambitoSV.Items.Add("Archivio")
            cbx_ambitoSV.Items.Add("DataBase")
            cbx_ambitoSV.Items.Add("Sviluppo")
            cbx_ambitoSV.Items.Add("Sito")
            cbx_ambitoSV.Items.Add("Altro")

        ElseIf cbx_swSV.SelectedItem = "VipDesigner" Then

            cbx_ambitoSV.Items.Add("Ambiente")
            cbx_ambitoSV.Items.Add("Ventola")
            cbx_ambitoSV.Items.Add("Motore")
            cbx_ambitoSV.Items.Add("Configurazione")
            cbx_ambitoSV.Items.Add("Datasheet")
            cbx_ambitoSV.Items.Add("Altro")

        ElseIf cbx_swSV.SelectedItem = "VipAnalyzer" Then

            cbx_ambitoSV.Items.Add("Display")
            cbx_ambitoSV.Items.Add("Tools")
            cbx_ambitoSV.Items.Add("Settings")
            cbx_ambitoSV.Items.Add("Altro")


        ElseIf cbx_swSV.SelectedItem = "ADAMS" Then

            cbx_ambitoSV.Items.Add("Altro")

        ElseIf cbx_swSV.SelectedItem = "SitoVip" Then

            cbx_ambitoSV.Items.Add("Altro")
            cbx_ambitoSV.Items.Add("Navbar")
            cbx_ambitoSV.Items.Add("Footer")
            cbx_ambitoSV.Items.Add("Chi siamo")
            cbx_ambitoSV.Items.Add("Ricerca e sviluppo")
            cbx_ambitoSV.Items.Add("Software - ADAMS")
            cbx_ambitoSV.Items.Add("Software - VipAnalyzer")
            cbx_ambitoSV.Items.Add("Software - VipDesigner")
            cbx_ambitoSV.Items.Add("Prodotti")
            cbx_ambitoSV.Items.Add("Prodotti - Ventilatori Assiali")
            cbx_ambitoSV.Items.Add("Prodotti - Ventilatori Radiali")
            cbx_ambitoSV.Items.Add("Prodotti - Giranti Assiali")
            cbx_ambitoSV.Items.Add("Prodotti - Giranti Radiali")
            cbx_ambitoSV.Items.Add("Prodotti - Dispositivi Elettronici")
            cbx_ambitoSV.Items.Add("Prodotti - Impiantistica")
            cbx_ambitoSV.Items.Add("Prodotti - Convogliatori")
            cbx_ambitoSV.Items.Add("Prodotti - Accessori")
            cbx_ambitoSV.Items.Add("Magazzino")
            cbx_ambitoSV.Items.Add("Fiere")
            cbx_ambitoSV.Items.Add("Contatti")

        ElseIf cbx_swSV.SelectedItem = "VipSelector" Then

            cbx_ambitoSV.Items.Add("Compatti")

        End If

        swSV = cbx_swSV.SelectedItem

    End Sub







    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles btn_aggiungi_sviluppo.Click


        Try

            Acquisisci_cbx_tbx_check()

            Aggiungi_sviluppo1()

            ricarica_sviluppo()




            tbx_DescrizioneSV.Text = ""
            cbx_swSV.SelectedIndex = -1
            cbx_ambitoSV.SelectedIndex = -1
            cbx_urgenzaSV.SelectedIndex = 0
            Diff_SV.SelectedIndex = 0



        Catch ex As Exception



            Yes_No_Warning = 0
            Warning_generaleOK.Label1.Text = "Inserire i dati dello sviluppo da assegnare"
            If Warning_generaleOK.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
                'attendo la risposta della box
            End If


        End Try





    End Sub


    Public Sub Acquisisci_cbx_tbx_check()

        aggiunta_val = 0


        'CBX

        For Each item As Control In Guna2Panel1.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                If cb.Name <> "filtro" And cb.Name <> "User_request" Then

                    vettore_aggiornamentoSV(aggiunta_val) = cb.SelectedItem
                    vettore_nomiSV(aggiunta_val) = cb.Name
                    aggiunta_val = aggiunta_val + 1

                End If
            End If
        Next


        'TBX
        For Each item As Control In Guna2Panel1.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tbx As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                If tbx.Name <> "ricerca" And tbx.Name <> "ID_sviluppo" Then

                    vettore_aggiornamentoSV(aggiunta_val) = tbx.Text
                    vettore_nomiSV(aggiunta_val) = tbx.Name
                    aggiunta_val = aggiunta_val + 1

                End If

            End If
        Next





    End Sub

    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellClick

        Puntatore_sviluppo = Guna2DataGridView1.CurrentRow.Index


        cbx_tbx_values()
    End Sub




    Private Sub tbx_DescrizioneSV_TextChanged(sender As Object, e As EventArgs) Handles tbx_DescrizioneSV.TextChanged
        DescrizioneSV = tbx_DescrizioneSV.Text
    End Sub

    Private Sub tbx_CreazioneSV_TextChanged(sender As Object, e As EventArgs) Handles tbx_CreazioneSV.TextChanged
        CreazioneSV = tbx_CreazioneSV.Text
    End Sub

    Private Sub tbx_chiusuraSV_TextChanged(sender As Object, e As EventArgs) Handles tbx_chiusuraSV.TextChanged
        chiusuraSV = tbx_chiusuraSV.Text
    End Sub

    Private Sub tbx_giorniSV_TextChanged(sender As Object, e As EventArgs) Handles tbx_giorniSV.TextChanged
        giorniSV = tbx_giorniSV.Text
    End Sub

    Private Sub tbx_valutazioneSV_TextChanged(sender As Object, e As EventArgs) Handles tbx_valutazioneSV.TextChanged
        valutazioneSV = tbx_valutazioneSV.Text
    End Sub

    Private Sub cbx_ambitoSV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_ambitoSV.SelectedIndexChanged
        ambitoSV = cbx_ambitoSV.SelectedItem
    End Sub

    Private Sub tbx_nomeSV_TextChanged(sender As Object, e As EventArgs) Handles tbx_nomeSV.TextChanged
        nomeSV = tbx_nomeSV.Text
    End Sub

    Private Sub cbx_statoSV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_statoSV.SelectedIndexChanged
        statoSV = cbx_statoSV.SelectedItem
    End Sub

    Private Sub cbx_urgenzaSV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_urgenzaSV.SelectedIndexChanged
        urgenzaSV = cbx_urgenzaSV.SelectedItem
    End Sub

    Private Sub ID_sviluppo_TextChanged(sender As Object, e As EventArgs) Handles ID_sviluppo.TextChanged
        'ID_SV = ID_sviluppo.Text
    End Sub




    Public Sub cbx_tbx_values()

        For Each item As Control In Guna2Panel1.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cbx As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                For i = 0 To N_colonne_sviluppo

                    If cbx.Name = Nome_colonne_sviluppo(i + 1) Then

                        'cbx.SelectedItem = DataBase_sviluppi_star(Puntatore_sviluppo, i)

                    End If

                Next
            End If
        Next


        For Each item As Control In Guna2Panel1.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tbx As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                For i = 0 To N_colonne_sviluppo

                    If tbx.Name = Nome_colonne_sviluppo(i + 1) Then

                        'tbx.Text = DataBase_sviluppi_star(Puntatore_sviluppo, i)

                    End If


                    If tbx.Name = "ID_sviluppo" Then

                        ID_SV = DataBase_sviluppi_star(Puntatore_sviluppo, 5)

                    End If


                Next
            End If
        Next



    End Sub

    Private Sub btn_elimina_sviluppo_Click(sender As Object, e As EventArgs) Handles btn_elimina_sviluppo.Click


        Acquisisci_cbx_tbx_check()
        Acquisisci_cbx_tbx_check()


        Warning.Label1.Text = "                     Eliminare lo sviluppo?"
        Yes_No_Warning = 0
        If Warning.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
            'attendo la risposta della box
        End If

        If Yes_No_Warning = 1 Then
            Elimina_riga_sviluppo1()
            ricarica_sviluppo()
        End If
    End Sub




    Private Sub btn_convalida_sviluppo_Click(sender As Object, e As EventArgs) Handles btn_convalida_sviluppo.Click


        Try

            If Guna2DataGridView1.Rows(Puntatore_sviluppo).Cells(4).Value = "Attesa valutazione" Or Guna2DataGridView1.Rows(Puntatore_sviluppo).Cells(4).Value = "In sviluppo" Then

                If nome_macchina = "Lorenzo" Then ' la convalida viene data dallo sviluppatore


                    convalida_sviluppatore1()
                    durata_sviluppo = Guna2DataGridView1.Rows(Puntatore_sviluppo).Cells(6).Value
                    convalida_sviluppo_user1()
                    ricarica_sviluppo()


                Else 'convalida datadall'utilizzatore


                    durata_sviluppo = Guna2DataGridView1.Rows(Puntatore_sviluppo).Cells(6).Value

                    Yes_No_Warning = 0
                    If Warning_valutazione.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
                        'attendo la risposta della box
                    End If

                    If Yes_No_Warning = 1 Then
                        convalida_sviluppo_user1()
                        ricarica_sviluppo()
                    End If



                End If

            End If


        Catch ex As Exception

        End Try





    End Sub

    Private Sub btn_clip_Click(sender As Object, e As EventArgs)

        mod_load_imm = 0
        pannello_immagine.Show()
        ricarica_sviluppo()
    End Sub


    Private Sub Guna2DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellClick


        NomeFolder_selezionata = Guna2DataGridView1.Rows(Guna2DataGridView1.CurrentRow.Index).Cells(1).Value


        Dim colonna_sel As Integer = e.ColumnIndex

        If colonna_sel = 8 Then
            mod_load_imm = 1
            pannello_immagine.Show()
        End If





    End Sub






    Private Sub filtro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles filtro.SelectedIndexChanged


        Select Case filtro.SelectedItem

            Case "All"
                filtro_star.Items.Clear()
                filtro_star.Items.Add("All")
                filtro_star.SelectedIndex = 0
                Riempi_datagrid(filtro_star.SelectedItem)

            Case "Stato"

                filtro_star.Items.Clear()
                filtro_star.Items.Add("In sviluppo")
                filtro_star.Items.Add("Chiuso")
                filtro_star.Items.Add("Attesa valutazione")
                filtro_star.SelectedIndex = 0

                Riempi_datagrid(filtro_star.SelectedItem)

            Case "User"
                filtro_star.Items.Clear()
                filtro_star.Items.Add("Andrea")
                filtro_star.Items.Add("Stefano")
                filtro_star.Items.Add("Paolo")
                filtro_star.Items.Add("Roberto")
                filtro_star.Items.Add("Fausto")
                filtro_star.Items.Add("Lorenzo")
                filtro_star.Items.Add("Alberto")
                filtro_star.Items.Add("Alessandro")
                filtro_star.Items.Add("Riccardo")
                filtro_star.SelectedIndex = 0
                Riempi_datagrid(filtro_star.SelectedItem)
            Case "Software"
                filtro_star.Items.Clear()
                filtro_star.Items.Add("VipEcosystem")
                filtro_star.Items.Add("VipDesigner")
                filtro_star.Items.Add("VipAnalyzer")
                filtro_star.Items.Add("ADAMS")
                filtro_star.Items.Add("SitoVip")
                filtro_star.Items.Add("VipSelector")
                filtro_star.SelectedIndex = 0
                Riempi_datagrid(filtro_star.SelectedItem)

            Case "Urgenza"
                filtro_star.Items.Clear()
                filtro_star.Items.Add("Bassa")
                filtro_star.Items.Add("Media")
                filtro_star.Items.Add("Alta")
                filtro_star.SelectedIndex = 2
                Riempi_datagrid(filtro_star.SelectedItem)

            Case "Difficoltà"

                filtro_star.Items.Clear()
                filtro_star.Items.Add("Bassa")
                filtro_star.Items.Add("Media")
                filtro_star.Items.Add("Alta")
                filtro_star.SelectedIndex = 1
                Riempi_datagrid(filtro_star.SelectedItem)

        End Select


    End Sub

    Private Sub filtro_star_SelectedIndexChanged(sender As Object, e As EventArgs) Handles filtro_star.SelectedIndexChanged
        Riempi_datagrid(filtro.SelectedItem)
    End Sub



    Private Sub Guna2DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles Guna2DataGridView1.KeyDown

        Try
            Puntatore_sviluppo = Guna2DataGridView1.CurrentRow.Index

            'puntatore_manager() 'identifico l'effettiva posizione nelladatagrid
        Catch ex As Exception

        End Try


        cbx_tbx_values()

    End Sub

    Private Sub Guna2DataGridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles Guna2DataGridView1.KeyUp

        Try
            Puntatore_sviluppo = Guna2DataGridView1.CurrentRow.Index

            'puntatore_manager() 'identifico l'effettiva posizione nelladatagrid
        Catch ex As Exception

        End Try


        cbx_tbx_values()

    End Sub



    Private Sub User_request_SelectedIndexChanged(sender As Object, e As EventArgs) Handles User_request.SelectedIndexChanged
        tbx_nomeSV.Text = User_request.Text
        nome_richiedente = User_request.Text
    End Sub



    Private Sub Diff_SV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Diff_SV.SelectedIndexChanged

        Try
            difficolta_SV = Diff_SV.SelectedItem
        Catch ex As Exception

        End Try


    End Sub


    Private Sub tbx_notaSV_TextChanged(sender As Object, e As EventArgs) Handles tbx_notaSV.TextChanged

        Try
            NOTE_SV = tbx_notaSV.Text
        Catch ex As Exception

        End Try


    End Sub


End Class