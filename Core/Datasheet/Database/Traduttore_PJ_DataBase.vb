Module Traduttore_PJ_DataBase


    Public Sub Traduttore_PJ_DataBase1(ID, Nome_colonna)

        'Dim numero_righe_tab As Integer '= getcount(cellaDB)
        Dim LL As Integer
        Dim pos_vect As Integer

        'ricerca nella matrice delle tabelle di appoggio a database
        For t = 0 To 299

            If all_tables(0, t) = Nome_colonna Then
                pos_vect = t
                LL = all_tables(1, t)

                t = 300

            End If

        Next


        valore_DS_star = all_tables(ID + 1, pos_vect)






    End Sub



End Module
