Public Class Catalogo
    Private Sub Catalogo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet11.Ventilatori'. È possibile spostarla o rimuoverla se necessario.
        Me.VentilatoriTableAdapter9.Fill(Me.DataBaseSWDataSet11.Ventilatori)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet10.Ventilatori'. È possibile spostarla o rimuoverla se necessario.
        Me.VentilatoriTableAdapter8.Fill(Me.DataBaseSWDataSet10.Ventilatori)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet9.Ventilatori'. È possibile spostarla o rimuoverla se necessario.
        Me.VentilatoriTableAdapter7.Fill(Me.DataBaseSWDataSet9.Ventilatori)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet8.Ventilatori'. È possibile spostarla o rimuoverla se necessario.
        Me.VentilatoriTableAdapter6.Fill(Me.DataBaseSWDataSet8.Ventilatori)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet7.Ventilatori'. È possibile spostarla o rimuoverla se necessario.
        Me.VentilatoriTableAdapter5.Fill(Me.DataBaseSWDataSet7.Ventilatori)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet6.Ventilatori'. È possibile spostarla o rimuoverla se necessario.
        Me.VentilatoriTableAdapter4.Fill(Me.DataBaseSWDataSet6.Ventilatori)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet5.Ventilatori'. È possibile spostarla o rimuoverla se necessario.
        Me.VentilatoriTableAdapter3.Fill(Me.DataBaseSWDataSet5.Ventilatori)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet4.Ventilatori'. È possibile spostarla o rimuoverla se necessario.
        Me.VentilatoriTableAdapter2.Fill(Me.DataBaseSWDataSet4.Ventilatori)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet3.Ventilatori'. È possibile spostarla o rimuoverla se necessario.
        Me.VentilatoriTableAdapter1.Fill(Me.DataBaseSWDataSet3.Ventilatori)
        'TODO: questa riga di codice carica i dati nella tabella 'DataBaseSWDataSet2.Ventilatori'. È possibile spostarla o rimuoverla se necessario.
        Me.VentilatoriTableAdapter.Fill(Me.DataBaseSWDataSet2.Ventilatori)

        load_catalogo()

    End Sub

    Public Sub load_catalogo()

        reset_all_selection()
        Numero_colonne_catalogo1()
        Importa_Catalogo1()
        Riempi_datagrid()
        ID_SV = DataBase_catalogo(Puntatore_catalogo, 10)

    End Sub


    Public Sub reset_all_selection()

        For Each item As Control In Guna2Panel1.Controls

            If item.GetType Is GetType(Guna.UI2.WinForms.Guna2ComboBox) Then
                Dim cbx As Guna.UI2.WinForms.Guna2ComboBox = DirectCast(item, Guna.UI2.WinForms.Guna2ComboBox)

                cbx.SelectedIndex = -1

            End If


        Next

    End Sub



    Private Sub btn_aggiungi_catalogo_Click(sender As Object, e As EventArgs) Handles btn_aggiungi_catalogo.Click

        If NomeSerie.Text <> "" Then

            Acquisisci_cbx_tbx()

            Aggiungi_Catalogo1()

            ricarica_Catalogo()

        End If

    End Sub




    Public Sub Acquisisci_cbx_tbx()


        'Nome serie
        vettore_aggiornamentoCAT(0) = NomeSerie.Text

        'Config1
        If Conf1.SelectedIndex <> -1 And ap1.SelectedIndex <> -1 Then
            Dim Serie1 As DataRowView = Conf1.SelectedItem
            vettore_aggiornamentoCAT(1) = Serie1(1) & "_" & ap1.SelectedItem
        Else
            vettore_aggiornamentoCAT(1) = ""
        End If


        'Config2
        If Conf2.SelectedIndex <> -1 And ap2.SelectedIndex <> -1 Then
            Dim Serie2 As DataRowView = Conf2.SelectedItem
            vettore_aggiornamentoCAT(2) = Serie2(1) & "_" & ap2.SelectedItem
        Else
            vettore_aggiornamentoCAT(2) = ""
        End If

        'Config3
        If Conf3.SelectedIndex <> -1 And ap3.SelectedIndex <> -1 Then
            Dim Serie3 As DataRowView = Conf3.SelectedItem
            vettore_aggiornamentoCAT(3) = Serie3(1) & "_" & ap3.SelectedItem
        Else
            vettore_aggiornamentoCAT(3) = ""
        End If

        'Config4
        If Conf4.SelectedIndex <> -1 And ap4.SelectedIndex <> -1 Then
            Dim Serie4 As DataRowView = Conf4.SelectedItem
            vettore_aggiornamentoCAT(4) = Serie4(1) & "_" & ap4.SelectedItem
        Else
            vettore_aggiornamentoCAT(4) = ""
        End If

        'Config5
        If Conf5.SelectedIndex <> -1 And ap5.SelectedIndex <> -1 Then
            Dim Serie5 As DataRowView = Conf5.SelectedItem
            vettore_aggiornamentoCAT(5) = Serie5(1) & "_" & ap5.SelectedItem
        Else
            vettore_aggiornamentoCAT(5) = ""
        End If

        'Config6
        If Conf6.SelectedIndex <> -1 And ap6.SelectedIndex <> -1 Then
            Dim Serie6 As DataRowView = Conf6.SelectedItem
            vettore_aggiornamentoCAT(6) = Serie6(1) & "_" & ap6.SelectedItem
        Else
            vettore_aggiornamentoCAT(6) = ""
        End If

        'Config7
        If Conf7.SelectedIndex <> -1 And ap7.SelectedIndex <> -1 Then
            Dim Serie7 As DataRowView = Conf7.SelectedItem
            vettore_aggiornamentoCAT(7) = Serie7(1) & "_" & ap7.SelectedItem
        Else
            vettore_aggiornamentoCAT(7) = ""
        End If

        'Config8
        If Conf8.SelectedIndex <> -1 And ap8.SelectedIndex <> -1 Then
            Dim Serie8 As DataRowView = Conf8.SelectedItem
            vettore_aggiornamentoCAT(8) = Serie8(1) & "_" & ap8.SelectedItem
        Else
            vettore_aggiornamentoCAT(8) = ""
        End If

        'Config9
        If Conf9.SelectedIndex <> -1 And ap9.SelectedIndex <> -1 Then
            Dim Serie9 As DataRowView = Conf9.SelectedItem
            vettore_aggiornamentoCAT(9) = Serie9(1) & "_" & ap9.SelectedItem
        Else
            vettore_aggiornamentoCAT(9) = ""
        End If

        'Config10
        If Conf10.SelectedIndex <> -1 And ap10.SelectedIndex <> -1 Then
            Dim Serie10 As DataRowView = Conf10.SelectedItem
            vettore_aggiornamentoCAT(10) = Serie10(1) & "_" & ap10.SelectedItem
        Else
            vettore_aggiornamentoCAT(10) = ""
        End If

    End Sub


    Public Sub ricarica_Catalogo()

        Numero_colonne_catalogo1()

        Importa_Catalogo1()

        Riempi_datagrid()


    End Sub


    Public Sub Riempi_datagrid()


        Dim add_DS As Integer = 0

        Guna2DataGridView1.Rows.Clear()


        For i As Integer = 0 To numero_Catalogo - 1


            Guna2DataGridView1.Rows.Add()

            'NomeSerie
            For j = 0 To N_colonne_Catalogo
                If Nome_colonne_catalogo(j) = "NomeSerie" Then
                    Guna2DataGridView1.Rows(add_DS).Cells(0).Value = DataBase_catalogo(i, j - 1)
                End If
            Next

            'cf1
            For j = 0 To N_colonne_Catalogo
                If Nome_colonne_catalogo(j) = "cf1" Then
                    Guna2DataGridView1.Rows(add_DS).Cells(1).Value = DataBase_catalogo(i, j - 1)
                End If
            Next

            'cf2
            For j = 0 To N_colonne_Catalogo
                If Nome_colonne_catalogo(j) = "cf2" Then
                    Guna2DataGridView1.Rows(add_DS).Cells(2).Value = DataBase_catalogo(i, j - 1)
                End If
            Next

            'cf3
            For j = 0 To N_colonne_Catalogo
                If Nome_colonne_catalogo(j) = "cf3" Then
                    Guna2DataGridView1.Rows(add_DS).Cells(3).Value = DataBase_catalogo(i, j - 1)
                End If
            Next

            'cf4
            For j = 0 To N_colonne_Catalogo
                If Nome_colonne_catalogo(j) = "cf4" Then
                    Guna2DataGridView1.Rows(add_DS).Cells(4).Value = DataBase_catalogo(i, j - 1)
                End If
            Next

            'cf5
            For j = 0 To N_colonne_Catalogo
                If Nome_colonne_catalogo(j) = "cf5" Then
                    Guna2DataGridView1.Rows(add_DS).Cells(5).Value = DataBase_catalogo(i, j - 1)
                End If
            Next

            'cf6
            For j = 0 To N_colonne_Catalogo
                If Nome_colonne_catalogo(j) = "cf6" Then
                    Guna2DataGridView1.Rows(add_DS).Cells(6).Value = DataBase_catalogo(i, j - 1)
                End If
            Next

            'cf7
            For j = 0 To N_colonne_Catalogo
                If Nome_colonne_catalogo(j) = "cf7" Then
                    Guna2DataGridView1.Rows(add_DS).Cells(7).Value = DataBase_catalogo(i, j - 1)
                End If
            Next

            'cf8
            For j = 0 To N_colonne_Catalogo
                If Nome_colonne_catalogo(j) = "cf8" Then
                    Guna2DataGridView1.Rows(add_DS).Cells(8).Value = DataBase_catalogo(i, j - 1)
                End If
            Next

            'cf9
            For j = 0 To N_colonne_Catalogo
                If Nome_colonne_catalogo(j) = "cf9" Then
                    Guna2DataGridView1.Rows(add_DS).Cells(9).Value = DataBase_catalogo(i, j - 1)
                End If
            Next

            'cf10
            For j = 0 To N_colonne_Catalogo
                If Nome_colonne_catalogo(j) = "cf10" Then
                    Guna2DataGridView1.Rows(add_DS).Cells(10).Value = DataBase_catalogo(i, j - 1)
                End If
            Next

            add_DS = add_DS + 1


        Next


        Puntatore_catalogo = 0

        Try
            Guna2DataGridView1.FirstDisplayedScrollingRowIndex = Guna2DataGridView1.Rows(0).Index
            Guna2DataGridView1.Refresh()
            Guna2DataGridView1.CurrentCell = Guna2DataGridView1.Rows(0).Cells(1)
            Guna2DataGridView1.Rows(0).Selected = True
        Catch ex As Exception

        End Try

        Guna2DataGridView1.Select()



    End Sub


    Private Sub Guna2DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles Guna2DataGridView1.KeyDown

        Try
            Puntatore_catalogo = Guna2DataGridView1.CurrentRow.Index
            ID_SV = DataBase_catalogo(Puntatore_catalogo, 10)
        Catch ex As Exception

        End Try


    End Sub


    Private Sub Guna2DataGridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles Guna2DataGridView1.KeyUp

        Try
            Puntatore_catalogo = Guna2DataGridView1.CurrentRow.Index
            ID_SV = DataBase_catalogo(Puntatore_catalogo, 10)

        Catch ex As Exception

        End Try



    End Sub

    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellClick

        Puntatore_catalogo = Guna2DataGridView1.CurrentRow.Index
        ID_SV = DataBase_catalogo(Puntatore_catalogo, 10)

    End Sub

    Private Sub btn_elimina_catalogo_Click(sender As Object, e As EventArgs) Handles btn_elimina_catalogo.Click
        Elimina_riga_catalogo1()
        load_catalogo()
    End Sub
End Class