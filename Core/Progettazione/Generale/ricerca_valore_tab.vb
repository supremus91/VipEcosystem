Module ricerca_valore_tab


    Public Sub ricerca_valore_tab1(N_tab)



        'ricerca nella matrice delle tabelle di appoggio a database
        For t = 0 To 299

            If all_tables(0, t) = N_tab Then
                pos_vect1 = t
                LL1 = all_tables(1, t)

                t = 300

            End If

        Next



    End Sub






End Module
