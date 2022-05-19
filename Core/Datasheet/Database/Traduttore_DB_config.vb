Module Traduttore_DB_config


    Public Sub Traduttore_DB_config1(Codice)

        For i = 0 To numero_DS_conf
            If Codice = DS_config_lista(i, 1) Then
                ID_conf = DS_config_lista(i, 0)
            End If
        Next

    End Sub

End Module
