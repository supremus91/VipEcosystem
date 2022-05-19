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


Module AggiungiPJ



    Public Sub aggiungiPJ1()

        Using myconnection As New OleDbConnection(constring)
            myconnection.Open()
            Dim sqlQry As String = "INSERT INTO [Progetto] ([tbx_Progetto], [cbx_Revisione], [tbx_data], [cbx_Stato], [tbx_OrdineRicevuto], [cbx_Owner]) VALUES (@tbx_Progetto, @cbx_Revisione, @tbx_data, @cbx_Stato, @tbx_OrdineRicevuto, @cbx_Owner)"

            Using cmd As New OleDbCommand(sqlQry, myconnection)

                cmd.Parameters.AddWithValue("@tbx_Progetto", nome_progetto)
                cmd.Parameters.AddWithValue("@cbx_Revisione", "0")
                cmd.Parameters.AddWithValue("@tbx_data", Date.Today.Day & "/" & Date.Today.Month & "/" & Date.Today.Year)
                cmd.Parameters.AddWithValue("@cbx_Stato", 1)
                cmd.Parameters.AddWithValue("@tbx_OrdineRicevuto", "0")


                ricerca_valore_tab1("@cbx_Owner")
                Dim owner As Integer = 0
                For i = 2 To 8
                    If all_tables(i, pos_vect1) = nome_macchina Then
                        owner = i
                    End If
                Next
                cmd.Parameters.AddWithValue("@cbx_Owner", owner)



                cmd.ExecuteNonQuery()

            End Using


        End Using


        'composizione vettore colonne
        'Dim DB_vettore_colonne As String

        'For i = 1 To Numero_colonneDB

        '    If i < Numero_colonneDB Then
        '        DB_vettore_colonne = DB_vettore_colonne & "[" & Nome_colonne(i) & "], "
        '    Else
        '        DB_vettore_colonne = DB_vettore_colonne & "[" & Nome_colonne(i) & "]"
        '    End If

        'Next


        '        'Dim posizione As Integer = FormNotifications.Guna2DataGridView1.CurrentRow.Index


        '        'creo nome della progettazione
        '        nome_progetto = "PJ" & Date.Today.Year & Date.Today.Month & Date.Today.Day

        '        For i = 1 To 1 'Numero_colonneDB

        '            'cmd.Parameters.AddWithValue("@" & Nome_colonne(i), NameOf("bTest"))
        '        Next


        '        cmd.Parameters.AddWithValue("@ID", 8)
        '        cmd.Parameters.AddWithValue("@Progetto", nome_progetto)
        '        cmd.Parameters.AddWithValue("@Revisione", 0)
        '        cmd.Parameters.AddWithValue("@DataInserimento", Date.Today.Year & "/" & Date.Today.Month & "/" & Date.Today.Day)
        '        cmd.Parameters.AddWithValue("@Owner", "user")
        '        cmd.Parameters.AddWithValue("@Stato", FormParametri.cbx_Stato.SelectedItem)
        '        cmd.Parameters.AddWithValue("@TipoRichiesta", Grid_Pressione(1)) 'Progettazioni.Guna2DataGridView1.Rows(posizione).Cells(3).Value)
        '        cmd.Parameters.AddWithValue("@Cliente", Progettazioni.Guna2DataGridView1.Rows(posizione).Cells(4).Value) '*
        '        cmd.Parameters.AddWithValue("@Descrizione", Progettazioni.Guna2DataGridView1.Rows(posizione).Cells(2).Value) '*
        '        cmd.Parameters.AddWithValue("@RiferimentoCliente", Progettazioni.Guna2DataGridView1.Rows(posizione).Cells(5).Value)
        '        cmd.Parameters.AddWithValue("@Quantita", FormParametri.cbx_quantita.SelectedItem)


        '        If FormParametri.Rad_Atex.Checked = True Then

        '            cmd.Parameters.AddWithValue("@Atex", True)
        '            cmd.Parameters.AddWithValue("@AtexProtezione", FormParametri.cbx_AtexProtezione.SelectedItem)
        '            cmd.Parameters.AddWithValue("@AtexCustodia", FormParametri.cbx_explosion.SelectedItem)
        '            cmd.Parameters.AddWithValue("@AtexCategoria", FormParametri.cbx_AtexCategoria.SelectedItem)
        '            cmd.Parameters.AddWithValue("@AtexClasseTemperatura", FormParametri.cbx_AtexClasseTemperatura.SelectedItem)

        '        Else

        '            cmd.Parameters.AddWithValue("@Atex", False)

        '        End If


        '        cmd.Parameters.AddWithValue("@Diametro", Progettazioni.Guna2DataGridView1.Rows(posizione).Cells(6).Value)
        '        cmd.Parameters.AddWithValue("@Portata", Progettazioni.Guna2DataGridView1.Rows(posizione).Cells(7).Value) '*
        '        cmd.Parameters.AddWithValue("@Pressione", Progettazioni.Guna2DataGridView1.Rows(posizione).Cells(8).Value) '*
        '        'cmd.Parameters.AddWithValue("@Tipopressione", "")
        '        cmd.Parameters.AddWithValue("@Direzione", FormParametri.cbx_Direzione.SelectedItem)
        '        cmd.Parameters.AddWithValue("@TemperaturaMinAmb", FormParametri.cbx_TemperaturaMinAmb.SelectedItem) '*
        '        cmd.Parameters.AddWithValue("@TemperaturaMaxAmb", FormParametri.cbx_TemperaturaMaxAmb.SelectedItem) '*
        '        cmd.Parameters.AddWithValue("@MaxDba", FormParametri.cbx_rumore.SelectedItem)
        '        cmd.Parameters.AddWithValue("@TipoMotore", FormParametri.cbx_TipoMotore.SelectedItem) '*
        '        cmd.Parameters.AddWithValue("@Alimentazione", FormParametri.cbx_Alimentazione.SelectedItem) '*
        '        cmd.Parameters.AddWithValue("@Volt", FormParametri.cbx_Volt.SelectedItem) '*
        '        cmd.Parameters.AddWithValue("@Hz", FormParametri.cbx_Hz.SelectedItem) '*
        '        cmd.Parameters.AddWithValue("@Kw", FormParametri.cbx_kW.SelectedItem)
        '        cmd.Parameters.AddWithValue("@IP", FormParametri.cbx_IP.SelectedItem)
        '        cmd.Parameters.AddWithValue("@ClasseIsolamento", FormParametri.cbx_ClasseIsolamento.SelectedItem) '*
        '        cmd.Parameters.AddWithValue("@Poli", FormParametri.cbx_Poli.SelectedItem)


        '        If FormParametri.cbx_IEX.SelectedIndex = 0 Then
        '            cmd.Parameters.AddWithValue("@IE1", True)
        '            cmd.Parameters.AddWithValue("@IE2", False)
        '            cmd.Parameters.AddWithValue("@IE3", False)
        '        ElseIf FormParametri.cbx_IEX.SelectedIndex = 0 Then
        '            cmd.Parameters.AddWithValue("@IE1", False)
        '            cmd.Parameters.AddWithValue("@IE2", True)
        '            cmd.Parameters.AddWithValue("@IE3", False)
        '        ElseIf FormParametri.cbx_IEX.SelectedIndex = 0 Then
        '            cmd.Parameters.AddWithValue("@IE1", False)
        '            cmd.Parameters.AddWithValue("@IE2", False)
        '            cmd.Parameters.AddWithValue("@IE3", True)
        '        End If

        '        cmd.Parameters.AddWithValue("@UL/CSA", FormParametri.check_ULCSA.CheckedState)
        '        cmd.Parameters.AddWithValue("@IECEX", FormParametri.check_IECEX.CheckedState)
        '        cmd.Parameters.AddWithValue("@CUTR", FormParametri.check_CUTR.CheckedState)
        '        cmd.Parameters.AddWithValue("@NEMAPREMIUM", FormParametri.check_NEMA.CheckedState)


        'If FormNotifications.cbx_inverter.SelectedIndex = 0 Then
        '    cmd.Parameters.AddWithValue("@Inverter", True)
        'Else
        '    cmd.Parameters.AddWithValue("@Inverter", False)
        'End If


        'If FormNotifications.cbx_PTC.SelectedIndex = 0 Then
        '    cmd.Parameters.AddWithValue("@Ptc", True)
        'Else
        '    cmd.Parameters.AddWithValue("@Ptc", False)
        'End If


        'If FormNotifications.cbx_scaldiglie.SelectedIndex = 0 Then
        '    cmd.Parameters.AddWithValue("@Scaldiglie", True)
        'Else
        '    cmd.Parameters.AddWithValue("@Scaldiglie", False)
        'End If


        'If FormNotifications.cbx_fori.SelectedIndex = 0 Then

        '    cmd.Parameters.AddWithValue("@ForiNDE", True)
        '    cmd.Parameters.AddWithValue("@ForiDE", False)

        'Else

        '    cmd.Parameters.AddWithValue("@ForiNDE", False)
        '    cmd.Parameters.AddWithValue("@ForiDE", True)

        'End If

        'cmd.Parameters.AddWithValue("@Forilaterali", FormNotifications.Check_fori.CheckState)



        'If FormNotifications.cbx_cappello.SelectedIndex = 0 Then
        '    cmd.Parameters.AddWithValue("@CappelloParapioggia", True)
        'Else
        '    cmd.Parameters.AddWithValue("@CappelloParapioggia", False)
        'End If

        'If FormNotifications.cbx_spargiacqua.SelectedIndex = 0 Then
        '    cmd.Parameters.AddWithValue("@DiscoParapioggia", True)
        'Else
        '    cmd.Parameters.AddWithValue("@DiscoParapioggia", False)
        'End If

        'If FormNotifications.cbx_tipo_mot.SelectedIndex = 0 Then
        '    cmd.Parameters.AddWithValue("@Tropicalizzazione", True)
        'Else
        '    cmd.Parameters.AddWithValue("@Tropicalizzazione", False)
        'End If

        'If FormNotifications.cbx_viti.SelectedIndex = 0 Then
        '    cmd.Parameters.AddWithValue("@VitiMotoreInox", True)
        'Else
        '    cmd.Parameters.AddWithValue("@VitiMotoreInox", False)
        'End If

        'If FormNotifications.cbx_rete_motore.SelectedIndex = 0 Then
        '    cmd.Parameters.AddWithValue("@ReteLatoMotore", True)
        'Else
        '    cmd.Parameters.AddWithValue("@ReteLatoMotore", False)
        'End If


        'If FormNotifications.cbx_rete_ventola.SelectedIndex = 0 Then
        '    cmd.Parameters.AddWithValue("@ReteLatoVentola", True)
        'Else
        '    cmd.Parameters.AddWithValue("@ReteLatoVentola", False)
        'End If


        'cmd.ExecuteNonQuery()

        '    End Using


        'End Using





    End Sub






End Module
