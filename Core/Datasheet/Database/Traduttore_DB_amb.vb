Module Traduttore_DB_amb



    Public Sub Traduttore_DB_amb1(Codice)

        For i = 0 To numero_DS_amb
            If Codice = DS_ambiente_lista(i, 1) Then
                ID_amb = DS_ambiente_lista(i, 0)
            End If
        Next


    End Sub



End Module
