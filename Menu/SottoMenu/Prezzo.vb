Public Class Prezzo



    Private Sub Prezzo_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        If nome_macchina = "Fausto" Or nome_macchina = "Lorenzo" Or nome_macchina = "Paolo" Or nome_macchina = "Remo" Then

            Guna2Button2.Visible = True

        End If

        Lettura_riga1(prog_rev(posizione_progetto, 1))
        carica_valori()


        tbx_prezzo_Leave(sender, e)
    End Sub


    Private Sub tbx_prezzo_Leave(sender As Object, e As EventArgs) Handles tbx_prezzo_motore.Leave, tbx_prezzo_girante.Leave, tbx_prezzo_supporto.Leave, tbx_prezzo_montaggio.Leave,
             tbx_prezzo_imballo.Leave, tbx_prezzo_imballaggio.Leave, tbx_prezzo_collaudo.Leave, tbx_prezzo_convogliatore.Leave, tbx_prezzo_coeff_listino.Leave,
             tbx_prezzo_coeff_sconto.Leave, tbx_prezzo_TOTALE.Leave, tbx_prezzo_LISTINO.Leave, tbx_prezzo_SCONTO.Leave, tbx_prezzo_SCONTO.Leave, tbx_prezzo_note_motore.Leave,
             tbx_prezzo_note_girante.Leave, tbx_prezzo_note_supporto.Leave, tbx_prezzo_note_montaggio.Leave, tbx_prezzo_note_imballo.Leave, tbx_prezzo_note_imballaggio.Leave,
             tbx_prezzo_note_collaudo.Leave, tbx_prezzo_note_convogliatore.Leave


        If tbx_prezzo_motore.Text = "" Then
            tbx_prezzo_motore.Text = "Motore"
            tbx_prezzo_motore.ForeColor = Color.Gray
        End If


        If tbx_prezzo_girante.Text = "" Then
            tbx_prezzo_girante.Text = "Girante"
            tbx_prezzo_girante.ForeColor = Color.Gray
        End If


        If tbx_prezzo_supporto.Text = "" Then
            tbx_prezzo_supporto.Text = "Supporto"
            tbx_prezzo_supporto.ForeColor = Color.Gray
        End If


        If tbx_prezzo_montaggio.Text = "" Then
            tbx_prezzo_montaggio.Text = "Montaggio"
            tbx_prezzo_montaggio.ForeColor = Color.Gray
        End If


        If tbx_prezzo_imballo.Text = "" Then
            tbx_prezzo_imballo.Text = "Imballo"
            tbx_prezzo_imballo.ForeColor = Color.Gray
        End If

        If tbx_prezzo_imballaggio.Text = "" Then
            tbx_prezzo_imballaggio.Text = "Imballaggio"
            tbx_prezzo_imballaggio.ForeColor = Color.Gray
        End If

        If tbx_prezzo_collaudo.Text = "" Then
            tbx_prezzo_collaudo.Text = "Collaudo"
            tbx_prezzo_collaudo.ForeColor = Color.Gray
        End If

        If tbx_prezzo_convogliatore.Text = "" Then
            tbx_prezzo_convogliatore.Text = "Convogliatore"
            tbx_prezzo_convogliatore.ForeColor = Color.Gray
        End If

        If tbx_prezzo_coeff_listino.Text = "" Then
            tbx_prezzo_coeff_listino.Text = "3,75"
            tbx_prezzo_coeff_listino.ForeColor = Color.Gray
        End If

        If tbx_prezzo_coeff_sconto.Text = "" Then
            tbx_prezzo_coeff_sconto.Text = "50"
            tbx_prezzo_coeff_sconto.ForeColor = Color.Gray
        End If


        If tbx_prezzo_TOTALE.Text = "" Then
            tbx_prezzo_TOTALE.Text = "TOTALE"
            tbx_prezzo_TOTALE.ForeColor = Color.Gray
        End If

        If tbx_prezzo_LISTINO.Text = "" Then
            tbx_prezzo_LISTINO.Text = "LISTINO"
            tbx_prezzo_LISTINO.ForeColor = Color.Gray
        End If

        If tbx_prezzo_SCONTO.Text = "" Then
            tbx_prezzo_SCONTO.Text = "SCONTO"
            tbx_prezzo_SCONTO.ForeColor = Color.Gray
        End If


        If tbx_prezzo_note_motore.Text = "" Then
            tbx_prezzo_note_motore.Text = "Note motore"
            tbx_prezzo_note_motore.ForeColor = Color.Gray
        End If

        If tbx_prezzo_note_girante.Text = "" Then
            tbx_prezzo_note_girante.Text = "Note girante"
            tbx_prezzo_note_girante.ForeColor = Color.Gray
        End If

        If tbx_prezzo_note_supporto.Text = "" Then
            tbx_prezzo_note_supporto.Text = "Note supporto"
            tbx_prezzo_note_supporto.ForeColor = Color.Gray
        End If


        If tbx_prezzo_note_montaggio.Text = "" Then
            tbx_prezzo_note_montaggio.Text = "Note montaggio"
            tbx_prezzo_note_montaggio.ForeColor = Color.Gray
        End If

        If tbx_prezzo_note_imballo.Text = "" Then
            tbx_prezzo_note_imballo.Text = "Note imballo"
            tbx_prezzo_note_imballo.ForeColor = Color.Gray
        End If

        If tbx_prezzo_note_imballaggio.Text = "" Then
            tbx_prezzo_note_imballaggio.Text = "Note imballaggio"
            tbx_prezzo_note_imballaggio.ForeColor = Color.Gray
        End If

        If tbx_prezzo_note_collaudo.Text = "" Then
            tbx_prezzo_note_collaudo.Text = "Note collaudo"
            tbx_prezzo_note_collaudo.ForeColor = Color.Gray
        End If

        If tbx_prezzo_note_convogliatore.Text = "" Then
            tbx_prezzo_note_convogliatore.Text = "Note convogliatore"
            tbx_prezzo_note_convogliatore.ForeColor = Color.Gray
        End If


    End Sub





    Private Sub tbx_prezzo_motore_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_motore.Enter

        If tbx_prezzo_motore.Text = "Motore" Then
            tbx_prezzo_motore.Text = ""
            tbx_prezzo_motore.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_girante_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_girante.Enter

        If tbx_prezzo_girante.Text = "Girante" Then
            tbx_prezzo_girante.Text = ""
            tbx_prezzo_girante.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_supporto_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_supporto.Enter

        If tbx_prezzo_supporto.Text = "Supporto" Then
            tbx_prezzo_supporto.Text = ""
            tbx_prezzo_supporto.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_montaggio_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_montaggio.Enter

        If tbx_prezzo_montaggio.Text = "Montaggio" Then
            tbx_prezzo_montaggio.Text = ""
            tbx_prezzo_montaggio.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_imballo_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_imballo.Enter

        If tbx_prezzo_imballo.Text = "Imballo" Then
            tbx_prezzo_imballo.Text = ""
            tbx_prezzo_imballo.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_imballaggio_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_imballaggio.Enter

        If tbx_prezzo_imballaggio.Text = "Imballaggio" Then
            tbx_prezzo_imballaggio.Text = ""
            tbx_prezzo_imballaggio.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_collaudo_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_collaudo.Enter

        If tbx_prezzo_collaudo.Text = "Collaudo" Then
            tbx_prezzo_collaudo.Text = ""
            tbx_prezzo_collaudo.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_convogliatore_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_convogliatore.Enter


        If tbx_prezzo_convogliatore.Text = "Convogliatore" Then
            tbx_prezzo_convogliatore.Text = ""
            tbx_prezzo_convogliatore.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_coeff_listino_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_coeff_listino.Enter

        If tbx_prezzo_coeff_listino.Text = "3,75" Then
            tbx_prezzo_coeff_listino.Text = ""
            tbx_prezzo_coeff_listino.ForeColor = Color.Black
        End If
    End Sub


    Private Sub tbx_prezzo_coeff_sconto_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_coeff_sconto.Enter

        If tbx_prezzo_coeff_sconto.Text = "50" Then
            tbx_prezzo_coeff_sconto.Text = ""
            tbx_prezzo_coeff_sconto.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_TOTALE_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_TOTALE.Enter

        If tbx_prezzo_TOTALE.Text = "TOTALE" Then
            tbx_prezzo_TOTALE.Text = ""
            tbx_prezzo_TOTALE.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_LISTINO_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_LISTINO.Enter

        If tbx_prezzo_LISTINO.Text = "LISTINO" Then
            tbx_prezzo_LISTINO.Text = ""
            tbx_prezzo_LISTINO.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_SCONTO_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_SCONTO.Enter

        If tbx_prezzo_SCONTO.Text = "SCONTO" Then
            tbx_prezzo_SCONTO.Text = ""
            tbx_prezzo_SCONTO.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_note_motore_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_note_motore.Enter

        If tbx_prezzo_note_motore.Text = "Note motore" Then
            tbx_prezzo_note_motore.Text = ""
            tbx_prezzo_note_motore.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_note_girante_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_note_girante.Enter

        If tbx_prezzo_note_girante.Text = "Note girante" Then
            tbx_prezzo_note_girante.Text = ""
            tbx_prezzo_note_girante.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_note_supporto_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_note_supporto.Enter

        If tbx_prezzo_note_supporto.Text = "Note supporto" Then
            tbx_prezzo_note_supporto.Text = ""
            tbx_prezzo_note_supporto.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_note_montaggio_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_note_montaggio.Enter

        If tbx_prezzo_note_montaggio.Text = "Note montaggio" Then
            tbx_prezzo_note_montaggio.Text = ""
            tbx_prezzo_note_montaggio.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_note_imballo_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_note_imballo.Enter

        If tbx_prezzo_note_imballo.Text = "Note imballo" Then
            tbx_prezzo_note_imballo.Text = ""
            tbx_prezzo_note_imballo.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_note_imballaggio_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_note_imballaggio.Enter

        If tbx_prezzo_note_imballaggio.Text = "Note imballaggio" Then
            tbx_prezzo_note_imballaggio.Text = ""
            tbx_prezzo_note_imballaggio.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_note_collaudo_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_note_collaudo.Enter

        If tbx_prezzo_note_collaudo.Text = "Note collaudo" Then
            tbx_prezzo_note_collaudo.Text = ""
            tbx_prezzo_note_collaudo.ForeColor = Color.Black
        End If

    End Sub

    Private Sub tbx_prezzo_note_convogliatore_Enter(sender As Object, e As EventArgs) Handles tbx_prezzo_note_convogliatore.Enter

        If tbx_prezzo_note_convogliatore.Text = "Note convogliatore" Then
            tbx_prezzo_note_convogliatore.Text = ""
            tbx_prezzo_note_convogliatore.ForeColor = Color.Black
        End If

    End Sub





    Private Sub tbx_prezzo_TextChanged(sender As Object, e As EventArgs) Handles tbx_prezzo_motore.TextChanged, tbx_prezzo_girante.TextChanged, tbx_prezzo_supporto.TextChanged, tbx_prezzo_montaggio.TextChanged,
            tbx_prezzo_montaggio.TextChanged, tbx_prezzo_imballo.TextChanged, tbx_prezzo_imballaggio.TextChanged, tbx_prezzo_collaudo.TextChanged, tbx_prezzo_convogliatore.TextChanged,
            tbx_prezzo_coeff_listino.TextChanged, tbx_prezzo_coeff_sconto.TextChanged

        tbx_prezzo_TOTALE.Text = 0

        Try
            If IsNumeric(tbx_prezzo_motore.Text) Then

                tbx_prezzo_TOTALE.Text = CInt(tbx_prezzo_TOTALE.Text) + tbx_prezzo_motore.Text

                tbx_prezzo_LISTINO.Text = tbx_prezzo_TOTALE.Text * tbx_prezzo_coeff_listino.Text
                tbx_prezzo_SCONTO.Text = tbx_prezzo_LISTINO.Text * (100 - tbx_prezzo_coeff_sconto.Text) / 100

            End If
        Catch ex As Exception

        End Try




        Try

            If IsNumeric(tbx_prezzo_girante.Text) Then

                tbx_prezzo_TOTALE.Text = CInt(tbx_prezzo_TOTALE.Text) + tbx_prezzo_girante.Text


                tbx_prezzo_LISTINO.Text = tbx_prezzo_TOTALE.Text * tbx_prezzo_coeff_listino.Text
                tbx_prezzo_SCONTO.Text = tbx_prezzo_LISTINO.Text * (100 - tbx_prezzo_coeff_sconto.Text) / 100
            End If
        Catch ex As Exception

        End Try




        Try

            If IsNumeric(tbx_prezzo_supporto.Text) Then

                tbx_prezzo_TOTALE.Text = CInt(tbx_prezzo_TOTALE.Text) + tbx_prezzo_supporto.Text


                tbx_prezzo_LISTINO.Text = tbx_prezzo_TOTALE.Text * tbx_prezzo_coeff_listino.Text
                tbx_prezzo_SCONTO.Text = tbx_prezzo_LISTINO.Text * (100 - tbx_prezzo_coeff_sconto.Text) / 100
            End If
        Catch ex As Exception

        End Try



        Try

            If IsNumeric(tbx_prezzo_montaggio.Text) Then

                tbx_prezzo_TOTALE.Text = CInt(tbx_prezzo_TOTALE.Text) + tbx_prezzo_montaggio.Text


                tbx_prezzo_LISTINO.Text = tbx_prezzo_TOTALE.Text * tbx_prezzo_coeff_listino.Text
                tbx_prezzo_SCONTO.Text = tbx_prezzo_LISTINO.Text * (100 - tbx_prezzo_coeff_sconto.Text) / 100
            End If
        Catch ex As Exception

        End Try



        Try

            If IsNumeric(tbx_prezzo_imballo.Text) Then

                tbx_prezzo_TOTALE.Text = CInt(tbx_prezzo_TOTALE.Text) + tbx_prezzo_imballo.Text


                tbx_prezzo_LISTINO.Text = tbx_prezzo_TOTALE.Text * tbx_prezzo_coeff_listino.Text
                tbx_prezzo_SCONTO.Text = tbx_prezzo_LISTINO.Text * (100 - tbx_prezzo_coeff_sconto.Text) / 100
            End If
        Catch ex As Exception

        End Try





        Try

            If IsNumeric(tbx_prezzo_imballaggio.Text) Then

                tbx_prezzo_TOTALE.Text = CInt(tbx_prezzo_TOTALE.Text) + tbx_prezzo_imballaggio.Text


                tbx_prezzo_LISTINO.Text = tbx_prezzo_TOTALE.Text * tbx_prezzo_coeff_listino.Text
                tbx_prezzo_SCONTO.Text = tbx_prezzo_LISTINO.Text * (100 - tbx_prezzo_coeff_sconto.Text) / 100
            End If
        Catch ex As Exception

        End Try


        Try

            If IsNumeric(tbx_prezzo_collaudo.Text) Then

                tbx_prezzo_TOTALE.Text = CInt(tbx_prezzo_TOTALE.Text) + tbx_prezzo_collaudo.Text


                tbx_prezzo_LISTINO.Text = tbx_prezzo_TOTALE.Text * tbx_prezzo_coeff_listino.Text
                tbx_prezzo_SCONTO.Text = tbx_prezzo_LISTINO.Text * (100 - tbx_prezzo_coeff_sconto.Text) / 100
            End If
        Catch ex As Exception

        End Try






        Try

            If IsNumeric(tbx_prezzo_convogliatore.Text) Then

                tbx_prezzo_TOTALE.Text = CInt(tbx_prezzo_TOTALE.Text) + tbx_prezzo_convogliatore.Text


                tbx_prezzo_LISTINO.Text = tbx_prezzo_TOTALE.Text * tbx_prezzo_coeff_listino.Text
                tbx_prezzo_SCONTO.Text = tbx_prezzo_LISTINO.Text * (100 - tbx_prezzo_coeff_sconto.Text) / 100
            End If
        Catch ex As Exception

        End Try


    End Sub




    Public Sub acquisisci_tbx_check_DB()

        aggiunta_prezzo_val = 0

        For Each item As Control In Guna2Panel1.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)


                vettore_aggiornamento_prezzo(aggiunta_prezzo_val) = tb.Text
                vettore_nomi_aggiornamento_prezzo(aggiunta_prezzo_val) = tb.Name

                aggiunta_prezzo_val = aggiunta_prezzo_val + 1


            End If
        Next

        For Each item As Control In Guna2Panel2.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)


                vettore_aggiornamento_prezzo(aggiunta_prezzo_val) = tb.Text
                vettore_nomi_aggiornamento_prezzo(aggiunta_prezzo_val) = tb.Name

                aggiunta_prezzo_val = aggiunta_prezzo_val + 1


            End If
        Next


        For Each item As Control In Guna2Panel3.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)


                vettore_aggiornamento_prezzo(aggiunta_prezzo_val) = tb.Text
                vettore_nomi_aggiornamento_prezzo(aggiunta_prezzo_val) = tb.Name

                aggiunta_prezzo_val = aggiunta_prezzo_val + 1


            End If
        Next

        For Each item As Control In Guna2Panel3.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then
                Dim chx As Guna.UI2.WinForms.Guna2CheckBox = DirectCast(item, Guna.UI2.WinForms.Guna2CheckBox)



                If chx.Checked = True Then
                    vettore_aggiornamento_prezzo(aggiunta_prezzo_val) = 1
                Else
                    vettore_aggiornamento_prezzo(aggiunta_prezzo_val) = 0
                End If


                vettore_nomi_aggiornamento_prezzo(aggiunta_prezzo_val) = chx.Name

                aggiunta_prezzo_val = aggiunta_prezzo_val + 1


            End If
        Next


    End Sub




    Public Sub change_Prezzo_DB()


        Dim stringa_aggiorna As String = ""

        For i = 0 To aggiunta_prezzo_val - 1

            If i < aggiunta_prezzo_val - 1 Then
                stringa_aggiorna = stringa_aggiorna & vettore_nomi_aggiornamento_prezzo(i) & " = '" & vettore_aggiornamento_prezzo(i) & "',"
            Else
                stringa_aggiorna = stringa_aggiorna & vettore_nomi_aggiornamento_prezzo(i) & " = '" & vettore_aggiornamento_prezzo(i) & "'"
            End If

        Next




        'Vado a cambiare il valore della checkbox da database
        Using cn As New OleDb.OleDbConnection(constring)


            cn.Open()
            Dim cmd As New OleDb.OleDbCommand
            cmd.CommandText = "UPDATE Progetto SET " & stringa_aggiorna & " WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & " AND cbx_Revisione = '" & num_rev_generale & "'" & ";"

            cmd.CommandType = CommandType.Text
            cmd.Connection = cn

            cmd.ExecuteNonQuery()
            cmd.Dispose()

            cn.Close()

        End Using



    End Sub



    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        acquisisci_tbx_check_DB()
        change_Prezzo_DB()

        form_parametri.LabelPrezzo.Text = tbx_prezzo_SCONTO.Text

    End Sub


    Public Sub carica_valori()


        For Each item As Control In Guna2Panel1.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                For i = 0 To Numero_colonneDB - 1

                    If tb.Name = Nome_colonne(i + 1) Then

                        tb.Text = Valore_CellaRiga(i)

                    End If

                Next

            End If
        Next

        For Each item As Control In Guna2Panel2.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                For i = 0 To Numero_colonneDB - 1

                    If tb.Name = Nome_colonne(i + 1) Then

                        tb.Text = Valore_CellaRiga(i)

                    End If

                Next

            End If
        Next


        For Each item As Control In Guna2Panel3.Controls
            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2TextBox) Then
                Dim tb As Guna.UI2.WinForms.Guna2TextBox = DirectCast(item, Guna.UI2.WinForms.Guna2TextBox)

                For i = 0 To Numero_colonneDB - 1

                    If tb.Name = Nome_colonne(i + 1) Then

                        tb.Text = Valore_CellaRiga(i)

                    End If

                Next

            End If
        Next


        'checkbox
        For Each item As Control In Guna2Panel3.Controls
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



    End Sub




    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

        testRTF1 = RichTextBox1.Rtf

    End Sub


End Class