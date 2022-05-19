Module Calcolo_pti_parabola




    Public Sub Calcolo_pti_parabola1(A, B, C, Qmin, Qmax, alta_bassa)

        '1 significa bassa
        '2 significa alta


        Dim DQ As Double = Math.Abs((Qmax - Qmin) / 10)
        Dim Qx As Double = Qmin




        If alta_bassa = 1 Then

            For i = 0 To 9

                vect_Bassa_x_chart(i) = Qx
                vect_Bassa_y_chart(i) = A * Qx ^ 2 + B * Qx + C


                If i < 8 Then
                    Qx = Qx + DQ
                Else
                    Qx = Qmax
                End If



            Next

        Else


            For i = 0 To 9
                vect_Alta_x_chart(i) = Qx
                vect_Alta_y_chart(i) = A * Qx ^ 2 + B * Qx + C

                If i < 8 Then
                    Qx = Qx + DQ
                Else
                    Qx = Qmax
                End If

            Next



        End If

    End Sub



End Module
