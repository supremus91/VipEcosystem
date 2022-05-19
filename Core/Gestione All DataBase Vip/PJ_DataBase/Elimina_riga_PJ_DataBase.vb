Module Elimina_riga_PJ_DataBase



    Public Sub Elimina_riga_PJ_DataBase1(Configurazione, Ambiente)

        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()

            Dim cmd As New OleDb.OleDbCommand

            If DB_pasticci_on = 0 Then
                cmd.CommandText = "DELETE FROM PJ_DataBase WHERE cbx_PJ_configurazioni = " & Configurazione & " AND cbx_PJ_ambiente = " & Ambiente & ";"
            Else
                cmd.CommandText = "DELETE FROM PJ_DataBasePasticci WHERE cbx_PJ_configurazioni = " & Configurazione & " AND cbx_PJ_ambiente = " & Ambiente & ";"
            End If

            cmd.CommandType = CommandType.Text
            cmd.Connection = cn

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()


        End Using


    End Sub



End Module
