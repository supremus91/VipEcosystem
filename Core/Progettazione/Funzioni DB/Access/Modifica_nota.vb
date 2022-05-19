Module Modifica_nota


    Public Sub Modifica_nota1()

        Try

            Using cn As New OleDb.OleDbConnection(constring)
                'provider to be used when working with access database
                cn.Open()

                'Dim stringa_aggiorna As String = "ProgettoPJ = '" & prog_rev(posizione_progetto, 0) & "', RevisionePJ = '" & N_rev_note & "', DataPJ = '" & Date.Today.Day & "/" & Date.Today.Month & "/" & Date.Today.Year & "', UtentePJ = '" & nome_macchina & "', NotaPJ = '" & testRTF & "'"

                Dim stringa_aggiorna As String = "NotaPJ = '" & testRTF & "'"

                Dim cmd As New OleDb.OleDbCommand


                'cmd.CommandText = "UPDATE NoteProgetto SET " & stringa_aggiorna & " WHERE ProgettoPJ = '" & prog_rev(posizione_progetto, 0) & "'" & " AND RevisionePJ = '" & FormParametri.cbx_Revisione.SelectedIndex & "';" '"'" & " AND ID = '" & ID_note_vect(posizione_grid_note) & "';"

                cmd.CommandText = "UPDATE NoteProgetto SET " & stringa_aggiorna & " WHERE ID = " & ID_note_vect(posizione_grid_note) & ";"

                cmd.CommandType = CommandType.Text
                cmd.Connection = cn

                cmd.ExecuteNonQuery()
                cmd.Dispose()
                cn.Close()




            End Using

        Catch ex As Exception

        End Try





    End Sub



End Module
