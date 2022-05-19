Module Cancella_rigaDS


    Public Sub Cancella_rigaDS1(Descrizione)


        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()

            Dim cmd As New OleDb.OleDbCommand

            'cmd.CommandText = "UPDATE NoteProgetto SET " & stringa_aggiorna & " WHERE ProgettoPJ = '" & prog_rev(posizione_progetto, 0) & "'" & " AND RevisionePJ = '" & FormParametri.cbx_Revisione.SelectedIndex & "';" '"'" & " AND ID = '" & ID_note_vect(posizione_grid_note) & "';"

            cmd.CommandText = "DELETE FROM Datasheets WHERE Descrizione = " & "'" & Descrizione & "'" & ";"

            cmd.CommandType = CommandType.Text
            cmd.Connection = cn

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()



        End Using



    End Sub



End Module
