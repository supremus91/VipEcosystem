Module Modifica_sviluppo

    Public Sub Modifica_sviluppo1()


        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()

            Dim stringa_aggiorna As String = ""

            For i = 0 To N_colonne_sviluppo - 2


                If i < N_colonne_sviluppo - 2 Then
                    stringa_aggiorna = stringa_aggiorna & vettore_nomiSV(i) & " = '" & vettore_aggiornamentoSV(i) & "',"
                Else
                    stringa_aggiorna = stringa_aggiorna & vettore_nomiSV(i) & " = '" & vettore_aggiornamentoSV(i) & "'"
                End If


            Next



            Dim cmd As New OleDb.OleDbCommand
            'stringa_aggiorna = "cbx_urgenzaSV = 'Alta'"

            cmd.CommandText = "UPDATE Sviluppo SET " & stringa_aggiorna & " WHERE ID = " & ID_SV & ";"

            cmd.CommandType = CommandType.Text
            cmd.Connection = cn

            cmd.ExecuteNonQuery()
            cmd.Dispose()



            cn.Close()

        End Using

    End Sub


End Module
