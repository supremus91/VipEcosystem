Module convalida_sviluppo_user


    Public Sub convalida_sviluppo_user1()

        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()

            Dim stringa_aggiorna As String


            If nome_macchina = "Lorenzo" Then
                stringa_aggiorna = "tbx_giorniSV = '" & durata_sviluppo & "'"
            Else
                stringa_aggiorna = "cbx_statoSV = 'Chiuso', tbx_valutazioneSV = '" & rating_user & "'"
                'stringa_aggiorna = "cbx_statoSV = 'Chiuso', tbx_giorniSV = '" & durata_sviluppo & "', tbx_valutazioneSV = '" & rating_user & "'"
            End If

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
