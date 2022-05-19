Module Elimina_riga_sviluppo

    Public Sub Elimina_riga_sviluppo1()

        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()

            Dim cmd As New OleDb.OleDbCommand


            cmd.CommandText = "DELETE FROM Sviluppo WHERE ID = " & ID_SV & ";"

            cmd.CommandType = CommandType.Text
            cmd.Connection = cn

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()


        End Using


    End Sub



End Module
