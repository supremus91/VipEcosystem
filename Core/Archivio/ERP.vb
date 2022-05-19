Module ERP


    Public Sub ERP1(u)


        Try
            Dim eff_target As Double = DS_totale_dati(u, 11)
            Dim eff_true As Double = DS_totale_dati(u, 84)
            Dim delta_eff As Double = Math.Abs(eff_target - eff_true) / eff_target * 100

            If eff_true < eff_target Then
                If delta_eff < 10 Then
                    ERP_ok = 1
                Else
                    ERP_ok = 0
                End If
            Else
                ERP_ok = 1
            End If
        Catch ex As Exception
            ERP_ok = 0
        End Try



    End Sub







End Module
