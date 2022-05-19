Module Conta_statiPJ




    Public Sub Conta_statiPJ1()


        num_chiuse = 0
        num_lav = 0
        num_cod = 0
        num_attesa = 0
        num_codificata = 0

        Dim view As New DataView(tables1(0))
        Dim add_grid As Integer = 0

        Dim val_comp As String = ""


        For i As Integer = 0 To view.Count - 1


            '---------------------------------------------------------------------------------------------------------------------------------------------
            'This part is used in order to identify the last row realted to a specific PJ --> It is referred at the the last review of a specific project
            Dim sur As Integer = i
            Dim PJ_name As String = view(i).Item("tbx_Progetto").ToString

            Try
                Do While view(sur).Item("tbx_Progetto").ToString = PJ_name
                    sur = sur + 1
                Loop

            Catch ex As Exception

            End Try

            sur = sur - i - 1
            '--------------------------------------------------------------------------------------------------------------------------------------------



            Dim stato_check As String
            Try
                ricerca_valore_tab1("cbx_Stato")
                stato_check = all_tables(view(i + sur).Item("cbx_Stato").ToString + 1, pos_vect1)
            Catch ex As Exception

            End Try



            Dim prog_rew_adapt As String
            For k = 0 To numeroPJ - 1
                If prog_rev(k, 0) = PJ_name Then
                    prog_rew_adapt = prog_rev(k, 1)
                End If
            Next


            If view(i).Item("cbx_Revisione").ToString = prog_rew_adapt Then

                If stato_check = "Chiusa" Then
                    num_chiuse = num_chiuse + 1
                ElseIf stato_check = "In lavorazione" Then
                    num_lav = num_lav + 1
                ElseIf stato_check = "In codifica" Then
                    num_cod = num_cod + 1
                ElseIf stato_check = "In attesa risposta cliente" Then
                    num_attesa = num_attesa + 1
                ElseIf stato_check = "Codificata" Then
                    num_codificata = num_codificata + 1


                End If

            End If


        Next

    End Sub




End Module
