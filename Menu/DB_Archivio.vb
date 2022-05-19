Public Class DB_Archivio


    Private Sub DB_Archivio_Load(sender As Object, e As EventArgs) Handles MyBase.Load





        riempi_combobox()
        Importa_PJ_Database1()


        'funzione_cbx1(cbx_VitiFan, "cbx_VitiFan")


        Dim add_DS As Integer = 0

        Guna2DataGridView1.Rows.Clear()


        For i As Integer = 0 To numero_PJ_DataBase - 1

            Guna2DataGridView1.Rows.Add()



            Guna2DataGridView1.Rows(add_DS).Cells(0).Value = PJ_DataBase_lista(i, config_pos)
            Guna2DataGridView1.Rows(add_DS).Cells(1).Value = PJ_DataBase_lista(i, numero_colonne_PJ_DataBase - 1)
            Guna2DataGridView1.Rows(add_DS).Cells(2).Value = PJ_DataBase_lista(i, mot_pos)
            Guna2DataGridView1.Rows(add_DS).Cells(3).Value = PJ_DataBase_lista(i, amb_pos)

            add_DS = add_DS + 1

        Next


        '--------------------------------------Carico la prima riga----------------------------------------------
        puntatore_datagrid = 0
        cbx_values()
        check_values()
        eccezione_motore_vip()
        '--------------------------------------------------------------------------------------------------------

        Try
            Guna2DataGridView1.FirstDisplayedScrollingRowIndex = Guna2DataGridView1.Rows(0).Index
            Guna2DataGridView1.Refresh()
            Guna2DataGridView1.CurrentCell = Guna2DataGridView1.Rows(0).Cells(1)
            Guna2DataGridView1.Rows(0).Selected = True
        Catch ex As Exception

        End Try


        Label1.Text = numero_PJ_DataBase


        Ricerca_configurazioni.Items.Clear()
        Ricerca_configurazioni.Items.Add("-")
        For i As Integer = 0 To numero_DS_conf - 1
            Ricerca_configurazioni.Items.Add(DS_config_lista(i, 1) & " - " & DS_config_lista(i, 2))
        Next
        Ricerca_configurazioni.SelectedIndex = 0

        Ricerca_Ambiente.Items.Clear()
        Ricerca_Ambiente.Items.Add("-")
        For i As Integer = 0 To numero_DS_amb - 1
            Ricerca_Ambiente.Items.Add(DS_ambiente_lista(i, 1))
        Next
        Ricerca_Ambiente.SelectedIndex = 0


        Guna2DataGridView1.Select()



    End Sub






    Public Sub riempi_combobox()


        cbx_PJ_configurazioni.Items.Clear()
        For i As Integer = 0 To numero_DS_conf - 1
            cbx_PJ_configurazioni.Items.Add(DS_config_lista(i, 1) & " - " & DS_config_lista(i, 2))
        Next

        cbx_PJ_ambiente.Items.Clear()
        For i As Integer = 0 To numero_DS_amb - 1
            cbx_PJ_ambiente.Items.Add(DS_ambiente_lista(i, 1))
        Next



        '------------------------Motore Elettrico------------------------------ 
        funzione_cbx1(cbx_tipo_motore_conf, "cbx_tipo_motore_conf")
        funzione_cbx1(cbx_materiale, "cbx_materiale")
        funzione_cbx1(cbx_cooling, "cbx_cooling")
        funzione_cbx1(cbx_IEX, "cbx_IEX")
        funzione_cbx1(cbx_costruzione, "cbx_costruzione")
        funzione_cbx1(cbx_colore, "cbx_colore")
        funzione_cbx1(cbx_corrosione, "cbx_corrosione")
        funzione_cbx1(cbx_MaterialeScudi, "cbx_MaterialeScudi")
        '-------------------------------------------------------------------
        '------------------------Ventilatore------------------------------

        funzione_cbx1(cbx_VitiFan, "cbx_VitiFan")

        '------------------------Ventola------------------------------
        funzione_cbx1(cbx_MaterialeVentola, "cbx_MaterialeVentola")
        funzione_cbx1(cbx_TrattamentoSupVentola, "cbx_TrattamentoSupVentola")
        funzione_cbx1(cbx_ClasseCorrVentola, "cbx_ClasseCorrVentola")
        funzione_cbx1(cbx_MaterialeVitiVentola, "cbx_MaterialeVitiVentola")
        funzione_cbx1(cbx_MaterialeMozzo, "cbx_MaterialeMozzo")
        funzione_cbx1(cbx_TrattamentoMozzo, "cbx_TrattamentoMozzo")
        funzione_cbx1(cbx_ColoreMozzo, "cbx_ColoreMozzo")
        funzione_cbx1(cbx_ClasseCorrMozzo, "cbx_ClasseCorrMozzo")
        funzione_cbx1(cbx_MaterialeRaggera, "cbx_MaterialeRaggera")
        funzione_cbx1(cbx_TrattamentoRaggera, "cbx_TrattamentoRaggera")
        funzione_cbx1(cbx_ColoreRaggera, "cbx_ColoreRaggera")
        funzione_cbx1(cbx_ClasseCorrRaggera, "cbx_ClasseCorrRaggera")
        funzione_cbx1(cbx_MotorTreat, "cbx_MotorTreat")
        '-------------------------------------------------------------------

        '------------------------Convogliatore------------------------------

        funzione_cbx1(cbx_PJ_conv_type, "cbx_PJ_conv_type")
        funzione_cbx1(cbx_MaterialeConvogliatore, "cbx_MaterialeConvogliatore")
        funzione_cbx1(cbx_TrattamentoConvogliatore, "cbx_TrattamentoConvogliatore")
        funzione_cbx1(cbx_ColoreConvogliatore, "cbx_ColoreConvogliatore")
        funzione_cbx1(cbx_ClasseConvogliatore, "cbx_ClasseConvogliatore")
        '-------------------------------------------------------------------
        '------------------------Supporto------------------------------
        funzione_cbx1(cbx_PJ_supp_type, "cbx_PJ_supp_type")
        funzione_cbx1(cbx_MaterialeSupporto, "cbx_MaterialeSupporto")
        funzione_cbx1(cbx_TrattamentoSupporto, "cbx_TrattamentoSupporto")
        funzione_cbx1(cbx_ColoreSupporto, "cbx_ColoreSupporto")
        funzione_cbx1(cbx_ClasseSupporto, "cbx_ClasseSupporto")

        '-------------------------------------------------------------------






    End Sub




    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellClick

        Try
            puntatore_datagrid = Guna2DataGridView1.CurrentRow.Index
            grid_config_sel = Guna2DataGridView1.Rows(puntatore_datagrid).Cells(0).Value
            grid_ambient_sel = Guna2DataGridView1.Rows(puntatore_datagrid).Cells(2).Value
            puntatore_manager() 'identifico l'effettiva posizione nelladatagrid
        Catch ex As Exception

        End Try


        cbx_values()
        check_values()
        eccezione_motore_vip()


    End Sub




    Private Sub Guna2DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles Guna2DataGridView1.KeyDown

        Try
            puntatore_datagrid = Guna2DataGridView1.CurrentRow.Index
            grid_config_sel = Guna2DataGridView1.Rows(puntatore_datagrid).Cells(0).Value
            grid_ambient_sel = Guna2DataGridView1.Rows(puntatore_datagrid).Cells(2).Value
            puntatore_manager() 'identifico l'effettiva posizione nelladatagrid
        Catch ex As Exception

        End Try


        cbx_values()
        check_values()
        eccezione_motore_vip()

    End Sub

    Private Sub Guna2DataGridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles Guna2DataGridView1.KeyUp

        Try
            puntatore_datagrid = Guna2DataGridView1.CurrentRow.Index
            grid_config_sel = Guna2DataGridView1.Rows(puntatore_datagrid).Cells(0).Value
            grid_ambient_sel = Guna2DataGridView1.Rows(puntatore_datagrid).Cells(2).Value
            puntatore_manager() 'identifico l'effettiva posizione nelladatagrid
        Catch ex As Exception

        End Try


        cbx_values()
        check_values()
        eccezione_motore_vip()

    End Sub


    'impongo regole speciali per il motore vip
    Public Sub eccezione_motore_vip()


        If cbx_tipo_motore_conf.SelectedIndex = 0 Then

            Try

                cbx_materiale.SelectedIndex = 0
                cbx_MaterialeScudi.SelectedIndex = 0
                cbx_cooling.SelectedIndex = 1
                cbx_IEX.SelectedIndex = 0
                cbx_costruzione.SelectedIndex = 1


                '------------------per correggere bug grafico----------------------------
                cbx_materiale.Enabled = True
                cbx_MaterialeScudi.Enabled = True
                cbx_cooling.Enabled = True
                cbx_IEX.Enabled = True
                cbx_costruzione.Enabled = True
                '------------------------------------------------------------------------


                cbx_materiale.Enabled = False
                cbx_MaterialeScudi.Enabled = False
                cbx_cooling.Enabled = False
                cbx_IEX.Enabled = False
                cbx_costruzione.Enabled = False

            Catch ex As Exception

            End Try


        Else

            Try

                '------------------per correggere bug grafico----------------------------
                cbx_materiale.Enabled = False
                cbx_MaterialeScudi.Enabled = False
                cbx_cooling.Enabled = False
                cbx_IEX.Enabled = False
                cbx_costruzione.Enabled = False
                '------------------------------------------------------------------------

                cbx_materiale.Enabled = True
                cbx_MaterialeScudi.Enabled = True
                cbx_cooling.Enabled = True
                cbx_IEX.Enabled = True
                cbx_costruzione.Enabled = True

            Catch ex As Exception

            End Try


        End If



    End Sub



    Public Sub cbx_values()



        For Each item As Control In gb_ventilatore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cbx As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                For i = 0 To numero_colonne_PJ_DataBase - 2

                    If cbx.Name = Nome_colonneDS(i + 1) Then

                        If Nome_colonneDS(i + 1) = "cbx_PJ_configurazioni" Then

                            For k As Integer = 0 To numero_DS_conf - 1

                                If DS_config_lista(k, 1) = PJ_DataBase_lista(puntatore_datagrid, i) Then
                                    cbx.StartIndex = k
                                End If

                            Next

                        Else
                                cbx.SelectedItem = PJ_DataBase_lista(puntatore_datagrid, i)
                        End If


                    End If

                Next
            End If
        Next



        For Each item As Control In gb_ventola.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cbx As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                For i = 0 To numero_colonne_PJ_DataBase - 2

                    If cbx.Name = Nome_colonneDS(i + 1) Then

                        cbx.SelectedItem = PJ_DataBase_lista(puntatore_datagrid, i)

                    End If

                Next
            End If
        Next




        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cbx As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                For i = 0 To numero_colonne_PJ_DataBase - 2

                    If cbx.Name = Nome_colonneDS(i + 1) Then

                        cbx.SelectedItem = PJ_DataBase_lista(puntatore_datagrid, i)

                    End If

                Next
            End If
        Next





        For Each item As Control In gb_convogliatore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cbx As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                For i = 0 To numero_colonne_PJ_DataBase - 2

                    If cbx.Name = Nome_colonneDS(i + 1) Then

                        cbx.SelectedItem = PJ_DataBase_lista(puntatore_datagrid, i)

                    End If

                Next
            End If
        Next




        For Each item As Control In gb_supporto.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cbx As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                For i = 0 To numero_colonne_PJ_DataBase - 2

                    If cbx.Name = Nome_colonneDS(i + 1) Then

                        cbx.SelectedItem = PJ_DataBase_lista(puntatore_datagrid, i)

                    End If

                Next
            End If
        Next








    End Sub


    Public Sub check_values()


        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                Dim chb As Guna.UI2.WinForms.Guna2CheckBox = DirectCast(item, Guna.UI2.WinForms.Guna2CheckBox)

                For i = 0 To numero_colonne_PJ_DataBase - 2

                    If chb.Name = Nome_colonneDS(i + 1) Then

                        Try
                            chb.Checked = PJ_DataBase_lista(puntatore_datagrid, i)
                            'chb.Enabled = True
                        Catch ex As Exception
                            'chb.Checked = False
                        End Try

                    End If

                Next

            End If
        Next



    End Sub







    Public Sub Acquisisci_cbx_tbx_check()

        aggiunta_val = 0


        'CBX

        For Each item As Control In gb_ventilatore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)


                If vettore_nomi_aggiornamentoPJ(aggiunta_val) = "cbx_PJ_configurazioni" Then
                    vettore_aggiornamentoPJ(aggiunta_val) = DS_config_lista(cb.SelectedIndex, 0)
                    conf_ID_PJ = DS_config_lista(cb.SelectedIndex, 0)
                Else
                    vettore_aggiornamentoPJ(aggiunta_val) = cb.SelectedIndex + 1
                End If


                vettore_nomi_aggiornamentoPJ(aggiunta_val) = cb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next


        For Each item As Control In gb_ventola.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                vettore_aggiornamentoPJ(aggiunta_val) = cb.SelectedIndex + 1
                vettore_nomi_aggiornamentoPJ(aggiunta_val) = cb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next




        For Each item As Control In gb_motore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                vettore_aggiornamentoPJ(aggiunta_val) = cb.SelectedIndex + 1
                vettore_nomi_aggiornamentoPJ(aggiunta_val) = cb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next




        For Each item As Control In gb_convogliatore.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                vettore_aggiornamentoPJ(aggiunta_val) = cb.SelectedIndex + 1
                vettore_nomi_aggiornamentoPJ(aggiunta_val) = cb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next



        For Each item As Control In gb_supporto.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                vettore_aggiornamentoPJ(aggiunta_val) = cb.SelectedIndex + 1
                vettore_nomi_aggiornamentoPJ(aggiunta_val) = cb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next




        For Each item As Control In gb_motore.Controls

            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                Dim chb As Guna.UI2.WinForms.Guna2CheckBox = DirectCast(item, Guna.UI2.WinForms.Guna2CheckBox)

                vettore_aggiornamentoPJ(aggiunta_val) = chb.CheckState
                vettore_nomi_aggiornamentoPJ(aggiunta_val) = chb.Name

                aggiunta_val = aggiunta_val + 1

            End If
        Next


    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Crea_btn.Click

        Acquisisci_cbx_tbx_check()
        Acquisisci_cbx_tbx_check()

        Esistenza_confgurazione = 0
        'controllo che la configurazione non esista gia'
        For i As Integer = 0 To numero_PJ_DataBase - 1
            If PJ_DataBase_lista(i, config_pos) = DS_config_lista(cbx_PJ_configurazioni.SelectedIndex, 1) And PJ_DataBase_lista(i, amb_pos) = DS_ambiente_lista(cbx_PJ_ambiente.SelectedIndex, 1) Then

                Esistenza_confgurazione = 1

            End If
        Next



        If Esistenza_confgurazione = 0 Then 'se non esiste creo la nuova riga nel database
            Aggiungi_PJ_DataBase1(conf_ID_PJ, cbx_PJ_ambiente.SelectedIndex + 1)
            Modifica_riga_PJ_DataBase1(conf_ID_PJ, cbx_PJ_ambiente.SelectedIndex + 1)
            DB_Archivio_Load(sender, e)

        Else
            Yes_No_Warning = 0

            Warning.Label1.Text = "Configuration already exists. Do you want to overwrite?"
            If Warning.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
                'attendo la risposta della box
            End If

            If Yes_No_Warning = 1 Then 'in caso di risposta affermativa agiorno
                Modifica_riga_PJ_DataBase1(conf_ID_PJ, cbx_PJ_ambiente.SelectedIndex + 1)
                DB_Archivio_Load(sender, e)
            End If

        End If



    End Sub





    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Elimina_btn.Click

        Acquisisci_cbx_tbx_check()
        Acquisisci_cbx_tbx_check()


        Warning.Label1.Text = "                     Do you want to Eliminate?"
        Yes_No_Warning = 0
        If Warning.ShowDialog() = DialogResult.OK Then 'apro la warningper chiedere se si e' intezionati a sovrascrivere
            'attendo la risposta della box
        End If

        If Yes_No_Warning = 1 Then
            Elimina_riga_PJ_DataBase1(conf_ID_PJ, cbx_PJ_ambiente.SelectedIndex + 1)
            DB_Archivio_Load(sender, e)
        End If


    End Sub

    Private Sub Ricerca_configurazioni_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Ricerca_configurazioni.SelectedIndexChanged

        ricerca_configurazioni_text = Ricerca_configurazioni.SelectedItem

        If ricerca_configurazioni_text <> "-" Then

            If ricerca_configurazioni_text(1) <> " " Then
                ricerca_configurazioni_text = ricerca_configurazioni_text(0) & ricerca_configurazioni_text(1)
            Else
                ricerca_configurazioni_text = ricerca_configurazioni_text(0)
            End If


        End If

            modalità_ricerca()

    End Sub

    Private Sub Ricerca_Ambiente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Ricerca_Ambiente.SelectedIndexChanged

        ricerca_ambiente_text = Ricerca_Ambiente.SelectedItem


        modalità_ricerca()

    End Sub



    Public Sub modalità_ricerca()



        Guna2DataGridView1.Rows.Clear()

        Dim add_DS As Integer = 0

        For i As Integer = 0 To numero_PJ_DataBase - 1

            If (PJ_DataBase_lista(i, config_pos) = ricerca_configurazioni_text Or ricerca_configurazioni_text = "-") And (PJ_DataBase_lista(i, amb_pos) = ricerca_ambiente_text Or ricerca_ambiente_text = "-") Then

                Guna2DataGridView1.Rows.Add()

                Guna2DataGridView1.Rows(add_DS).Cells(0).Value = PJ_DataBase_lista(i, config_pos)
                Guna2DataGridView1.Rows(add_DS).Cells(1).Value = PJ_DataBase_lista(i, numero_colonne_PJ_DataBase - 1)
                Guna2DataGridView1.Rows(add_DS).Cells(2).Value = PJ_DataBase_lista(i, mot_pos)
                Guna2DataGridView1.Rows(add_DS).Cells(3).Value = PJ_DataBase_lista(i, amb_pos)

                add_DS = add_DS + 1

            End If


        Next

        Try

            Guna2DataGridView1.FirstDisplayedScrollingRowIndex = Guna2DataGridView1.Rows(0).Index
            Guna2DataGridView1.Refresh()
            Guna2DataGridView1.CurrentCell = Guna2DataGridView1.Rows(0).Cells(1)
            Guna2DataGridView1.Rows(0).Selected = True

        Catch ex As Exception

        End Try


        '--------------------------------------Carico la prima riga----------------------------------------------
        Try
            puntatore_datagrid = 0
            grid_config_sel = Guna2DataGridView1.Rows(puntatore_datagrid).Cells(0).Value
            grid_ambient_sel = Guna2DataGridView1.Rows(puntatore_datagrid).Cells(2).Value
            puntatore_manager() 'identifico l'effettiva posizione nelladatagrid
        Catch ex As Exception

        End Try



        cbx_values()
        check_values()
        eccezione_motore_vip()
        '--------------------------------------------------------------------------------------------------------

        Guna2DataGridView1.Select()

    End Sub



    Public Sub puntatore_manager()

        For i As Integer = 0 To numero_PJ_DataBase - 1

            If PJ_DataBase_lista(i, config_pos) = grid_config_sel And PJ_DataBase_lista(i, amb_pos) = grid_ambient_sel Then

                puntatore_datagrid = i

            End If

        Next


    End Sub


    Private Sub cbx_tipo_motore_conf_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbx_tipo_motore_conf.SelectedIndexChanged
        eccezione_motore_vip()
    End Sub


End Class