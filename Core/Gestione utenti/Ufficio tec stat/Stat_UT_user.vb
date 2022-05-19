Module Stat_UT_user



    Public Sub Stat_UT_user1()


        Dim nome As String = All_client_bk(Num_ID).Substring(All_client_bk(Num_ID).IndexOf("_") + 1, All_client_bk(Num_ID).IndexOf("_Cognome") - 5)
        Dim cognome As String = All_client_bk(Num_ID).Substring(All_client_bk(Num_ID).IndexOf("_Cognome") + 9)
        cognome = cognome.Substring(0, cognome.IndexOf("_Mail"))



        If nome = "Fausto" And cognome = "Fasolini" Then

            Uff_tec_count1()

        ElseIf nome = "Paolo" And cognome = "Caimi" Then

            Uff_tec_count1()

        ElseIf nome = "Alberto" And cognome = "Vergani" Then

            Uff_tec_count1()


        ElseIf nome = "Stefano" And cognome = "Rossini" Then

            Uff_tec_count1()

        ElseIf nome = "Lorenzo" And cognome = "Peretti" Then

            Uff_tec_count1()

        ElseIf nome = "Roberto" And cognome = "Romanò" Then

            Uff_tec_count1()

        End If






    End Sub



End Module
