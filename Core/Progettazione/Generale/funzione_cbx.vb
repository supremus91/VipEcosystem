Module funzione_cbx


    Public Sub funzione_cbx1(cbx, cellaDB)


        Dim cb As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(cbx, Guna.UI2.WinForms.Guna2ComboBox)



        'Dim numero_righe_tab As Integer '= getcount(cellaDB)
        Dim LL As Integer
        Dim pos_vect As Integer

        'ricerca nella matrice delle tabelle di appoggio a database
        For t = 0 To 299

            If all_tables(0, t) = cellaDB Then
                pos_vect = t
                LL = all_tables(1, t)

                t = 300

            End If

        Next


        If mod_rev0 = 0 And cb.Name <> "cbx_DispMotore" And cb.Name <> "cbx_Volt" Then 'Nella fase di imposizione della scelta revisione 0 non serve andare a mettere i valori nelle cbx
            cb.Items.Clear()
            For j = 0 To LL
                cb.Items.Add(all_tables(j + 2, pos_vect))
            Next
        End If


        If new_project = 0 Then ' se sto creando una nuova progettazione non compilo le varie textbo ecc, il progetto e' vuoto

            'inserisco il valore del database selezionato da tabella alla revisione selezionata
            For j = 0 To Numero_colonneDB - 1
                If Nome_colonne(j + 1) = cellaDB Then


                    If Nome_colonne(j + 1) = "cbx_AtexProtezione" Then

                        Try
                            Dim controllo As String = Valore_CellaRiga(j) - 1
                            If controllo <> "-1" Then
                                rad_true = 1
                            End If
                        Catch ex As Exception

                        End Try
                    End If




                    If Nome_colonne(j + 1) = "cbx_DispMotore" Then

                        'cb.SelectedIndex = Valore_CellaRiga(j) - 1

                        Select Case Valore_CellaRiga(j)
                            Case 1
                                cb.SelectedItem = "Non definito"
                            Case 2
                                cb.SelectedItem = "A   Premente asse orizzontale"
                            Case 3
                                cb.SelectedItem = "AD Premente con albero verso il basso"
                            Case 4
                                cb.SelectedItem = "AU Premente con albero in alto"
                            Case 5
                                cb.SelectedItem = "B   Aspirante asse orizzontale"
                            Case 6
                                cb.SelectedItem = "BD Aspirante con albero in alto"
                            Case 7
                                cb.SelectedItem = "BU Aspirante con Albero in Basso"
                            Case 8
                                cb.SelectedItem = "H   Radiale asse orizzontale"
                            Case 9
                                cb.SelectedItem = "D Radiale con albero verso il basso"
                            Case 10
                                cb.SelectedItem = "U Radiale con albero in alto"
                        End Select


                    ElseIf Nome_colonne(j + 1) = "cbx_Volt" Then

                        Select Case Valore_CellaRiga(j)

                            Case 1
                                cb.SelectedItem = "220"
                            Case 2
                                cb.SelectedItem = "230"
                            Case 3
                                cb.SelectedItem = "380"
                            Case 4
                                cb.SelectedItem = "400"
                            Case 5
                                cb.SelectedItem = "415"
                            Case 6
                                cb.SelectedItem = "440"
                            Case 7
                                cb.SelectedItem = "460"
                            Case 8
                                cb.SelectedItem = "480"
                            Case 9
                                cb.SelectedItem = "500"
                            Case 10
                                cb.SelectedItem = "575"
                            Case 11
                                cb.SelectedItem = "600"
                            Case 12
                                cb.SelectedItem = "660"
                            Case 13
                                cb.SelectedItem = "690"

                        End Select




                    Else


                        Try


                            If mod_rev0 = 1 Then 'Si vanno ad imporre le scelte fatte nella revisione 0

                                If Valore_CellaRiga(j) - 1 <> -1 Then
                                    cb.SelectedIndex = Valore_CellaRiga(j) - 1

                                    If jump_tipo_richiesta0 <> 1 Then
                                        'cb.Enabled = False
                                    End If

                                End If

                            Else

                                cb.SelectedIndex = Valore_CellaRiga(j) - 1
                                'cb.Enabled = True
                            End If


                        Catch ex As Exception


                            cb.SelectedItem = Valore_CellaRiga(j)


                        End Try




                    End If








                End If
            Next j

        End If


    End Sub



End Module
