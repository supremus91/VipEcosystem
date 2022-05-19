Module convalida_sviluppatore


    Public Sub convalida_sviluppatore1()


        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()



            Dim cmd As New OleDb.OleDbCommand
            'stringa_aggiorna = "cbx_urgenzaSV = 'Alta'"

            cmd.CommandText = "UPDATE Sviluppo SET " & "cbx_statoSV = 'Attesa valutazione'" & " WHERE ID = " & ID_SV & ";"

            cmd.CommandType = CommandType.Text
            cmd.Connection = cn

            cmd.ExecuteNonQuery()
            cmd.Dispose()



            cn.Close()

        End Using


    End Sub



End Module
