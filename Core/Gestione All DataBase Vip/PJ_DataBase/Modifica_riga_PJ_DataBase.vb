Module Modifica_riga_PJ_DataBase



    Public Sub Modifica_riga_PJ_DataBase1(Configurazione, Ambiente)


        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()

            Dim stringa_aggiorna As String = ""

            For i = 0 To numero_colonne_PJ_DataBase - 2


                If i < numero_colonne_PJ_DataBase - 2 Then
                    stringa_aggiorna = stringa_aggiorna & vettore_nomi_aggiornamentoPJ(i) & " = '" & vettore_aggiornamentoPJ(i) & "',"
                Else
                    stringa_aggiorna = stringa_aggiorna & vettore_nomi_aggiornamentoPJ(i) & " = '" & vettore_aggiornamentoPJ(i) & "'"
                End If


            Next



            Dim cmd As New OleDb.OleDbCommand

            If DB_pasticci_on = 0 Then
                cmd.CommandText = "UPDATE PJ_DataBase SET " & stringa_aggiorna & " WHERE cbx_PJ_configurazioni = " & Configurazione & " AND cbx_PJ_ambiente = " & Ambiente & ";"
            Else
                cmd.CommandText = "UPDATE PJ_DataBasePasticci SET " & stringa_aggiorna & " WHERE cbx_PJ_configurazioni = " & Configurazione & " AND cbx_PJ_ambiente = " & Ambiente & ";"
            End If

            cmd.CommandType = CommandType.Text
            cmd.Connection = cn

            cmd.ExecuteNonQuery()
            cmd.Dispose()



            cn.Close()

        End Using




    End Sub





End Module
