Module Nome_fan_corretto


    'Questa funzione serve a ricreare il nome del ventilatore giusto senza trattini
    'des2 --> descrizione_fan(2)
    'configurazione_A --> M_A valore (1)
    'configurazione_B --> M_A valore(0)
    'Dir_flusso --> lettera della direzione del flusso


    Public Sub Nome_fan_corretto1(des2, configurazione_A, configurazione_B, Dir_flusso)


        Dim caso As Integer = -1
        Dim direzione_flusso As String = Dir_flusso
        Dim nome_config As String



        'Identifico dei casi per riscrivere il nome del ventilatore
        If des2 = "_" And configurazione_B = "_" Then
            caso = 0
        End If

        If des2 = "_" And configurazione_B <> "_" Then
            caso = 1
        End If

        If des2 <> "_" And configurazione_B = "_" Then
            caso = 2
        End If

        If des2 <> "_" And configurazione_B <> "_" Then
            caso = 3
        End If


        Select Case caso

            Case 0 'esempio caso : ER_ 071-32-4D-__-3X07M


                nome_config = configurazione_A
                traduzione_ventilatore = descrizione_fan(0) & descrizione_fan(1) & " " & descrizione_fan.Substring(4, descrizione_fan.IndexOf("__") - 4) & nome_config & "-" & descrizione_fan.Substring(descrizione_fan.IndexOf("__") + 3, descrizione_fan.Length - descrizione_fan.IndexOf("__") - 3) & "-" & direzione_flusso


            Case 1 'esempio caso : ER_ 071-32-4D-EA-3X07M

                nome_config = configurazione_A & configurazione_B
                traduzione_ventilatore = descrizione_fan(0) & descrizione_fan(1) & " " & descrizione_fan.Substring(4, descrizione_fan.IndexOf("__") - 4) & nome_config & "-" & descrizione_fan.Substring(descrizione_fan.IndexOf("__") + 3, descrizione_fan.Length - descrizione_fan.IndexOf("__") - 3) & "-" & direzione_flusso


            Case 2 'esempio caso : ERY 071-32-4D-F_-3X07M

                nome_config = configurazione_A
                traduzione_ventilatore = descrizione_fan(0) & descrizione_fan(1) & descrizione_fan(2) & " " & descrizione_fan.Substring(4, descrizione_fan.IndexOf("__") - 4) & nome_config & "-" & descrizione_fan.Substring(descrizione_fan.IndexOf("__") + 3, descrizione_fan.Length - descrizione_fan.IndexOf("__") - 3) & "-" & direzione_flusso

            Case 3 'esempio caso : ERY 071-32-4D-EA-3X07M

                nome_config = configurazione_A & configurazione_B
                traduzione_ventilatore = descrizione_fan(0) & descrizione_fan(1) & descrizione_fan(2) & " " & descrizione_fan.Substring(4, descrizione_fan.IndexOf("__") - 4) & nome_config & "-" & descrizione_fan.Substring(descrizione_fan.IndexOf("__") + 3, descrizione_fan.Length - descrizione_fan.IndexOf("__") - 3) & "-" & direzione_flusso



        End Select







    End Sub





End Module
