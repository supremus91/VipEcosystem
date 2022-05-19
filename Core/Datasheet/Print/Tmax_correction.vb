Module Tmax_correction

    Public Sub Tmax_correction1(Tmax)

        Dim T_int As Integer = Math.Truncate(Tmax / 10)
        Dim T_dec As Integer = (Tmax / 10 - T_int) * 10
        Dim Tmax_New As Integer = 0


        If T_dec >= 0 And T_dec <= 2 Then
            Tmax_New = 0
        ElseIf T_dec >= 3 And T_dec <= 7 Then
            Tmax_New = 5
        ElseIf T_dec >= 8 And T_dec <= 10 Then
            Tmax_New = 10
        End If

        Tmax_starA = T_int * 10 + Tmax_New
    End Sub


End Module
