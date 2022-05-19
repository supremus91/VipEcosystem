Imports DevExpress.XtraCharts
Imports DevExpress.XtraReports.UI
Imports System.Collections
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Drawing


Module PaginaA


    Public Sub PaginaA1()



        Report.XrChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.[False]
        Report.lb_eff_class.Text = ERP_selezionato & " - Maximum efficiency point"
        Report.XrLabel40.Text = "Data related, according to AMCA 210-07, with air density 1,205 kg/m³ at 20°C"

        Report.Lb_frame.Text = "Frame"
        Report.lb_pow.Text = "Pass        [W]"
        Report.lb_rpm.Text = "RPM"
        Report.Lb_Volt.Text = "Poles"
        Report.Lb_freq.Text = "ηₙ               [%]"
        Report.Pnom.Text = "P,n [kW]"
        Report.Curr.Text = "Iₙ              [A]"
        Report.lb_ambT.Text = "Tₐ  [°C]"
        Report.Lb_motor.Text = "Connection"
        Report.lb_ins.Text = "Ins."
        Report.lb_IP.Text = "IP"

        Report.Lb_Volt.Text = "U [V]"
        Report.Lb_freq.Text = "f [Hz]"
        Report.Curr.Text = "Iₙ [A]"
        Report.lb_pow.Text = "Pass [W]"
        Report.Lb_motor.Text = "Conn."


        If testNote1 = Nothing Then
            Report.XrLabel7.Visible = False
        Else
            Report.XrLabel7.Visible = True
            Report.XrLabel7.Text = testNote1
        End If



        If mod_fan = "U" Then

            Report.Lb_frame.WidthF = 70
            Report.lb_frame1.WidthF = 70
            Report.lb_frame2.WidthF = 70

            Report.lb_frame1.Text = frame_sel
            Report.lb_frame2.Text = frame_sel

        Else

            Report.Lb_frame.WidthF = 0
            Report.lb_frame1.WidthF = 0
            Report.lb_frame2.WidthF = 0

        End If



        If potenza_installata <> "" Then

            Report.Pnom1.WidthF = 66.8
            Report.Pnom2.WidthF = 66.8
            Report.Pnom1.Text = potenza_installata
            Report.Pnom2.Text = potenza_installata

        Else

            Report.Pnom.WidthF = 0
            Report.Pnom1.WidthF = 0
            Report.Pnom2.WidthF = 0

        End If



        Report.lb_cert.WidthF = 0
        Report.lb_cert1.WidthF = 0
        Report.lb_cert2.WidthF = 0


        '----------------------------------------CANCELLA CURVE---------------------------------------------
        'curva 1
        For i = 0 To 9 '42

            Try
                Report.XrChart1.Series(0).Points.RemoveAt(0)
            Catch ex As Exception

            End Try
        Next
        'curva di carico
        For i = 0 To 9 ' 43
            Try
                Report.XrChart1.Series(1).Points.RemoveAt(0)
            Catch ex As Exception

            End Try

        Next

        For i = 0 To 2 'i6 44

            Try
                Report.XrChart1.Series(2).Points.RemoveAt(0)
            Catch ex As Exception

            End Try

        Next i

        For i = 0 To 2 'i6 44
            Try
                Report.XrChart1.Series(3).Points.RemoveAt(0)
            Catch ex As Exception

            End Try
        Next i
        '--------------------------------------------------------------------------------------------

        For i = 0 To 9
            Report.XrChart1.Series(0).Points.AddPoint((vect_Bassa_x_chart(i)), (vect_Bassa_y_chart(i)))
        Next

        Try

            If S_D_var = 2 Then

                For i = 0 To 9
                    Report.XrChart1.Series(1).Points.AddPoint((vect_Alta_x_chart(i)), (vect_Alta_y_chart(i)))
                Next

            End If

        Catch ex As Exception

        End Try



        'visualizzazione punti su grafico 1
        Report.XrChart1.Series(2).Visible = True
        Report.XrChart1.Series(3).Visible = True
        Report.XrChart1.Series(4).Visible = True
        Report.XrChart1.Series(5).Visible = True
        Report.XrChart1.Series(6).Visible = True
        Report.XrChart1.Series(7).Visible = True



        For i = 0 To 10 'fino a 10 per essere sicuro di rimuovere tutti i punti

            Try
                Report.XrChart1.Series(2).Points.RemoveAt(i)
            Catch ex As Exception

            End Try

            Try
                Report.XrChart1.Series(3).Points.RemoveAt(i)
            Catch ex As Exception

            End Try

            Try
                Report.XrChart1.Series(4).Points.RemoveAt(i)
            Catch ex As Exception

            End Try

            Try
                Report.XrChart1.Series(5).Points.RemoveAt(i)
            Catch ex As Exception

            End Try

            Try
                Report.XrChart1.Series(6).Points.RemoveAt(i)
            Catch ex As Exception

            End Try

            Try
                Report.XrChart1.Series(7).Points.RemoveAt(i)
            Catch ex As Exception

            End Try



        Next




        '-------------------------------------------------TABELLA PUNTI DI LAVORO ALTA/BASSA--------------------------------------------------------------

        If S_D_var = 1 Then 'BASSA

            Dim shift_DS1 As Integer = 50

            Report.XrTable18.LocationFloat = New DevExpress.Utils.PointFloat(49.94!, 315.18! + shift_DS1 + 10)
            Report.XrTable17.LocationFloat = New DevExpress.Utils.PointFloat(49.94!, 350.34! + shift_DS1 + 10)
            Report.XrTable12.LocationFloat = New DevExpress.Utils.PointFloat(49.94!, 420.0! + shift_DS1)
            Report.XrTable13.LocationFloat = New DevExpress.Utils.PointFloat(49.94!, 500.0! + shift_DS1)


            Dim shift_DS As Integer = 30
            Dim shift_DS2 As Integer = 20


            'immagini delle configurazioni
            Report.Pic1.LocationFloat = New DevExpress.Utils.PointFloat(95.22!, 655.0! - shift_DS)
            Report.Pic2.LocationFloat = New DevExpress.Utils.PointFloat(222.15!, 655.0! - shift_DS)
            Report.Pic3.LocationFloat = New DevExpress.Utils.PointFloat(348.53!, 655.0! - shift_DS)
            Report.Pic4.LocationFloat = New DevExpress.Utils.PointFloat(477.24!, 655.0! - shift_DS)
            Report.Pic5.LocationFloat = New DevExpress.Utils.PointFloat(613.87!, 655.0! - shift_DS)

            Report.Pic6.LocationFloat = New DevExpress.Utils.PointFloat(95.22!, 768.92! - shift_DS + shift_DS2)
            Report.Pic7.LocationFloat = New DevExpress.Utils.PointFloat(222.15!, 768.92! - shift_DS + shift_DS2)
            Report.Pic8.LocationFloat = New DevExpress.Utils.PointFloat(348.53!, 768.92! - shift_DS + shift_DS2)
            Report.Pic9.LocationFloat = New DevExpress.Utils.PointFloat(477.24!, 768.92! - shift_DS + shift_DS2)
            Report.Pic10.LocationFloat = New DevExpress.Utils.PointFloat(613.87!, 768.92 - shift_DS + shift_DS2)

            'warning motore
            Report.XrLabel12.LocationF = New DevExpress.Utils.PointFloat(50.0!, 559.0! - shift_DS2)

            'striscia blu
            Report.XrPictureBox13.LocationFloat = New DevExpress.Utils.PointFloat(0!, 755.88! - shift_DS)
            Report.XrPictureBox15.LocationFloat = New DevExpress.Utils.PointFloat(0!, 869.79! - shift_DS + shift_DS2)

            'description e part number
            Report.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(0!, 757.04! - shift_DS)
            Report.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(0!, 772.83! - shift_DS)
            Report.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(0!, 870.96! - shift_DS + shift_DS2)
            Report.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(0!, 886.75! - shift_DS + shift_DS2)

            'des1 e part1
            Report.des1.LocationFloat = New DevExpress.Utils.PointFloat(95.22!, 757.04! - shift_DS)
            Report.part1.LocationFloat = New DevExpress.Utils.PointFloat(95.22!, 772.83! - shift_DS)


            'des2 e part2
            Report.des2.LocationFloat = New DevExpress.Utils.PointFloat(222.15!, 757.04! - shift_DS)
            Report.part2.LocationFloat = New DevExpress.Utils.PointFloat(222.15!, 772.83! - shift_DS)

            'des3 e part3
            Report.des3.LocationFloat = New DevExpress.Utils.PointFloat(348.53!, 757.04! - shift_DS)
            Report.part3.LocationFloat = New DevExpress.Utils.PointFloat(348.53!, 772.83! - shift_DS)

            'des4 e part4
            Report.des4.LocationFloat = New DevExpress.Utils.PointFloat(477.24!, 757.04! - shift_DS)
            Report.part4.LocationFloat = New DevExpress.Utils.PointFloat(477.24!, 772.83! - shift_DS)

            'des5 e part5
            Report.des5.LocationFloat = New DevExpress.Utils.PointFloat(613.87!, 757.04! - shift_DS)
            Report.part5.LocationFloat = New DevExpress.Utils.PointFloat(613.87!, 772.83! - shift_DS)



            'des6 e part6
            Report.des6.LocationFloat = New DevExpress.Utils.PointFloat(95.22!, 870.96! - shift_DS + shift_DS2)
            Report.part6.LocationFloat = New DevExpress.Utils.PointFloat(95.22!, 886.75! - shift_DS + shift_DS2)

            'des7 e part7
            Report.des7.LocationFloat = New DevExpress.Utils.PointFloat(222.15!, 870.96! - shift_DS + shift_DS2)
            Report.part7.LocationFloat = New DevExpress.Utils.PointFloat(222.15!, 886.75! - shift_DS + shift_DS2)


            'des8 e part8
            Report.des8.LocationFloat = New DevExpress.Utils.PointFloat(348.53!, 870.96! - shift_DS + shift_DS2)
            Report.part8.LocationFloat = New DevExpress.Utils.PointFloat(348.53!, 886.75! - shift_DS + shift_DS2)

            'des9 e part9
            Report.des9.LocationFloat = New DevExpress.Utils.PointFloat(477.24!, 870.96! - shift_DS + shift_DS2)
            Report.part9.LocationFloat = New DevExpress.Utils.PointFloat(477.24!, 886.75! - shift_DS + shift_DS2)

            'des10 e part10
            Report.des10.LocationFloat = New DevExpress.Utils.PointFloat(613.87!, 870.96! - shift_DS + shift_DS2)
            Report.part10.LocationFloat = New DevExpress.Utils.PointFloat(613.87!, 886.75! - shift_DS + shift_DS2)



            'nascondo la tabella sotto
            Report.lb_motor2.Visible = False
            Report.lb_frame2.Visible = False
            Report.lb_freq2.Visible = False
            Report.lb_rpm2.Visible = False
            Report.lb_pow2.Visible = False
            Report.Pnom2.Visible = False
            Report.Curr2.Visible = False
            Report.lb_ins2.Visible = False
            Report.lb_IP2.Visible = False
            Report.lb_cert2.Visible = False
            Report.lb_amb2.Visible = False
            Report.lb_Volt2.Visible = False


            Try

                Report.XrChart1.Series(2).Points.AddPoint(CInt(Q1_DS), CInt(P1_DS))
                Report.XrChart1.Series(3).Points.AddPoint(CInt(Q2_DS), CInt(P2_DS))
                Report.XrChart1.Series(4).Points.AddPoint(CInt(Q3_DS), CInt(P3_DS))

            Catch ex As Exception

            End Try



            Report.XrTableCell302.Text = 1
            Report.XrTableCell306.Text = 2
            Report.XrTableCell310.Text = 3



        Else

            Dim shift_DS As Integer = 20
            Dim shift_DS1 As Integer = 20
            Dim shift2L As Integer = 25

            Report.XrTable18.LocationFloat = New DevExpress.Utils.PointFloat(49.94!, 315.18! + shift_DS + shift_DS1)
            Report.XrTable17.LocationFloat = New DevExpress.Utils.PointFloat(49.94!, 403.07! + shift_DS + shift_DS1)
            Report.XrTable12.LocationFloat = New DevExpress.Utils.PointFloat(49.94!, 473.0! + shift_DS + shift_DS1)
            Report.XrTable13.LocationFloat = New DevExpress.Utils.PointFloat(49.94!, 575.0! + shift_DS + shift_DS1)


            'warning motore
            Report.XrLabel12.LocationF = New DevExpress.Utils.PointFloat(50.0!, 559.0! + 45)

            'immagini delle configurazioni
            Report.Pic1.LocationFloat = New DevExpress.Utils.PointFloat(95.22!, 655.0! + shift_DS)
            Report.Pic2.LocationFloat = New DevExpress.Utils.PointFloat(222.15!, 655.0! + shift_DS)
            Report.Pic3.LocationFloat = New DevExpress.Utils.PointFloat(348.53!, 655.0! + shift_DS)
            Report.Pic4.LocationFloat = New DevExpress.Utils.PointFloat(477.24!, 655.0! + shift_DS)
            Report.Pic5.LocationFloat = New DevExpress.Utils.PointFloat(613.87!, 655.0! + shift_DS)

            Report.Pic6.LocationFloat = New DevExpress.Utils.PointFloat(95.22!, 768.92! + shift_DS + shift2L)
            Report.Pic7.LocationFloat = New DevExpress.Utils.PointFloat(222.15!, 768.92! + shift_DS + shift2L)
            Report.Pic8.LocationFloat = New DevExpress.Utils.PointFloat(348.53!, 768.92! + shift_DS + shift2L)
            Report.Pic9.LocationFloat = New DevExpress.Utils.PointFloat(477.24!, 768.92! + shift_DS + shift2L)
            Report.Pic10.LocationFloat = New DevExpress.Utils.PointFloat(613.87!, 768.92 + shift_DS + shift2L)

            'striscia blu
            Report.XrPictureBox13.LocationFloat = New DevExpress.Utils.PointFloat(0!, 755.88! + shift_DS)
            Report.XrPictureBox15.LocationFloat = New DevExpress.Utils.PointFloat(0!, 869.79! + shift_DS + shift2L)


            'description e part number
            Report.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(0!, 757.04! + shift_DS)
            Report.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(0!, 772.83! + shift_DS)
            Report.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(0!, 870.96! + shift_DS + shift2L)
            Report.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(0!, 886.75! + shift_DS + shift2L)



            'des1 e part1
            Report.des1.LocationFloat = New DevExpress.Utils.PointFloat(95.22!, 757.04! + shift_DS)
            Report.part1.LocationFloat = New DevExpress.Utils.PointFloat(95.22!, 772.83! + shift_DS)


            'des2 e part2
            Report.des2.LocationFloat = New DevExpress.Utils.PointFloat(222.15!, 757.04! + shift_DS)
            Report.part2.LocationFloat = New DevExpress.Utils.PointFloat(222.15!, 772.83! + shift_DS)

            'des3 e part3
            Report.des3.LocationFloat = New DevExpress.Utils.PointFloat(348.53!, 757.04! + shift_DS)
            Report.part3.LocationFloat = New DevExpress.Utils.PointFloat(348.53!, 772.83! + shift_DS)

            'des4 e part4
            Report.des4.LocationFloat = New DevExpress.Utils.PointFloat(477.24!, 757.04! + shift_DS)
            Report.part4.LocationFloat = New DevExpress.Utils.PointFloat(477.24!, 772.83! + shift_DS)

            'des5 e part5
            Report.des5.LocationFloat = New DevExpress.Utils.PointFloat(613.87!, 757.04! + shift_DS)
            Report.part5.LocationFloat = New DevExpress.Utils.PointFloat(613.87!, 772.83! + shift_DS)

            'des6 e part6
            Report.des6.LocationFloat = New DevExpress.Utils.PointFloat(95.22!, 870.96! + shift_DS + shift2L)
            Report.part6.LocationFloat = New DevExpress.Utils.PointFloat(95.22!, 886.75! + shift_DS + shift2L)

            'des7 e part7
            Report.des7.LocationFloat = New DevExpress.Utils.PointFloat(222.15!, 870.96! + shift_DS + shift2L)
            Report.part7.LocationFloat = New DevExpress.Utils.PointFloat(222.15!, 886.75! + shift_DS + shift2L)


            'des8 e part8
            Report.des8.LocationFloat = New DevExpress.Utils.PointFloat(348.53!, 870.96! + shift_DS + shift2L)
            Report.part8.LocationFloat = New DevExpress.Utils.PointFloat(348.53!, 886.75! + shift_DS + shift2L)

            'des9 e part9
            Report.des9.LocationFloat = New DevExpress.Utils.PointFloat(477.24!, 870.96! + shift_DS + shift2L)
            Report.part9.LocationFloat = New DevExpress.Utils.PointFloat(477.24!, 886.75! + shift_DS + shift2L)

            'des10 e part10
            Report.des10.LocationFloat = New DevExpress.Utils.PointFloat(613.87!, 870.96! + shift_DS + shift2L)
            Report.part10.LocationFloat = New DevExpress.Utils.PointFloat(613.87!, 886.75! + shift_DS + shift2L)

            'nascondo la tabella sotto
            Report.lb_motor2.Visible = True
            Report.lb_frame2.Visible = True
            Report.lb_freq2.Visible = True
            Report.lb_rpm2.Visible = True
            Report.lb_pow2.Visible = True
            Report.Pnom2.Visible = True
            Report.Curr2.Visible = True
            Report.lb_cert2.Visible = True
            Report.lb_amb2.Visible = True
            Report.lb_Volt2.Visible = True
            Report.lb_ins2.Visible = True
            Report.lb_IP2.Visible = True

            Report.XrChart1.Series(5).Points.AddPoint(CInt(Q1_DS), CInt(P1_DS))
            Report.XrChart1.Series(6).Points.AddPoint(CInt(Q2_DS), CInt(P2_DS))
            Report.XrChart1.Series(7).Points.AddPoint(CInt(Q3_DS), CInt(P3_DS))

            Try

                Report.XrChart1.Series(2).Points.AddPoint(CInt(Q4_DS), CInt(P4_DS))
                Report.XrChart1.Series(3).Points.AddPoint(CInt(Q5_DS), CInt(P5_DS))
                Report.XrChart1.Series(4).Points.AddPoint(CInt(Q6_DS), CInt(P6_DS))

            Catch ex As Exception

            End Try

            Report.XrTableCell302.Text = 4
            Report.XrTableCell306.Text = 5
            Report.XrTableCell310.Text = 6

        End If






        'Flow rate
        Report.pto3_Q_STAR.Text = Q3_DS 'Math.Round(pto3_bassa(0), 0)
        Report.pto2_Q_STAR.Text = Q2_DS  'Math.Round(pto2_bassa(0), 0)
        Report.pto1_Q_STAR.Text = Q1_DS   'Math.Round(pto1_bassa(0), 0)

        'Flow rate
        Report.pto3_Q_DELTA.Text = Q6_DS ' Math.Round(pto3_alta(0), 0)
        Report.pto2_Q_DELTA.Text = Q5_DS ' Math.Round(pto2_alta(0), 0)
        Report.pto1_Q_DELTA.Text = Q4_DS 'Math.Round(pto1_alta(0), 0)

        'pressure
        Report.pto3_P_STAR.Text = P3_DS 'Math.Round(pto3_bassa(2), 0)
        Report.pto2_P_STAR.Text = P2_DS 'Math.Round(pto2_bassa(2), 0)
        Report.pto1_P_STAR.Text = P1_DS 'Math.Round(pto1_bassa(2), 0)

        'pressure
        Report.pto3_P_DELTA.Text = P6_DS 'Math.Round(pto3_alta(2), 0)
        Report.pto2_P_DELTA.Text = P5_DS 'Math.Round(pto2_alta(2), 0)
        Report.pto1_P_DELTA.Text = P4_DS 'Math.Round(pto1_alta(2), 0)



        If tensione_alta_T = "3~230 Δ/400 Y" Then
            Report.pto1_V_DELTA.Text = numero_fasi_alta & "230Δ / 400Y"
            Report.pto2_V_DELTA.Text = numero_fasi_alta & "230Δ / 400Y"
            Report.pto3_V_DELTA.Text = numero_fasi_alta & "230Δ / 400Y"
        ElseIf tensione_alta_T = "3~400 Δ/690 Y" Then
            Report.pto1_V_DELTA.Text = numero_fasi_alta & "400Δ / 690Y"
            Report.pto2_V_DELTA.Text = numero_fasi_alta & "400Δ / 690Y"
            Report.pto3_V_DELTA.Text = numero_fasi_alta & "400Δ / 690Y"
        Else
            Report.pto1_V_DELTA.Text = numero_fasi_alta & tensione_alta_N
            Report.pto2_V_DELTA.Text = numero_fasi_alta & tensione_alta_N
            Report.pto3_V_DELTA.Text = numero_fasi_alta & tensione_alta_N
        End If


        If tensione_bassa_T = "3~230 Δ/400 Y" Then
            Report.pto1_V_STAR.Text = numero_fasi_bassa & "230Δ / 400Y"
            Report.pto2_V_STAR.Text = numero_fasi_bassa & "230Δ / 400Y"
            Report.pto3_V_STAR.Text = numero_fasi_bassa & "230Δ / 400Y"
        ElseIf tensione_bassa_T = "3~400 Δ/690 Y" Then
            Report.pto1_V_STAR.Text = numero_fasi_bassa & "400Δ / 690Y"
            Report.pto2_V_STAR.Text = numero_fasi_bassa & "400Δ / 690Y"
            Report.pto3_V_STAR.Text = numero_fasi_bassa & "400Δ / 690Y"
        Else
            Report.pto1_V_STAR.Text = numero_fasi_bassa & tensione_bassa_N
            Report.pto2_V_STAR.Text = numero_fasi_bassa & tensione_bassa_N
            Report.pto3_V_STAR.Text = numero_fasi_bassa & tensione_bassa_N
        End If


        'tensioni per tabella 
        Report.pto1_F_DELTA.Text = freq_alta_N
        Report.pto2_F_DELTA.Text = freq_alta_N
        Report.pto3_F_DELTA.Text = freq_alta_N

        Report.pto1_F_STAR.Text = freq_bassa_N
        Report.pto2_F_STAR.Text = freq_bassa_N
        Report.pto3_F_STAR.Text = freq_bassa_N


        'RPM
        Report.pto3_RPM_STAR.Text = RPM3_DS 'Math.Round(pto3_bassa(3), 0)
        'Report.pto2_RPM_STAR.Text = Math.Round(RPM_DeltaStar_shadow, 0)
        Report.pto2_RPM_STAR.Text = RPM2_DS ' Math.Round(pto2_bassa(3), 0)
        Report.pto1_RPM_STAR.Text = RPM1_DS 'Math.Round(pto1_bassa(3), 0)


        Report.pto3_RPM_DELTA.Text = RPM6_DS 'Math.Round(pto3_alta(3), 0)
        'Report.pto2_RPM_delta.Text = Math.Round(RPM_Deltadelta_shadow, 0)
        Report.pto2_RPM_DELTA.Text = RPM5_DS 'Math.Round(pto2_alta(3), 0)
        Report.pto1_RPM_DELTA.Text = RPM4_DS ' Math.Round(pto1_alta(3), 0)



        'POW
        Report.pto3_POW_STAR.Text = POW3_DS '/ 1000 'Math.Round(pto3_bassa(4) / 1000, 2)
        'Report.pto2_POW_STAR.Text = Math.Round(((PowAbs_orange(max_pos))), 0)
        Report.pto2_POW_STAR.Text = POW2_DS '/ 1000 'Math.Round(pto2_bassa(4) / 1000, 2)
        Report.pto1_POW_STAR.Text = POW1_DS '/ 1000 'Math.Round(pto1_bassa(4) / 1000, 2)

        Try
            'POW
            Report.pto3_POW_DELTA.Text = POW6_DS '/ 1000 ' Math.Round(pto3_alta(4) / 1000, 2)
            'Report.pto2_POW_delta.Text = Math.Round(((PowAbs_orange(max_pos))), 0)
            Report.pto2_POW_DELTA.Text = POW5_DS '/ 1000 ' Math.Round(pto2_alta(4) / 1000, 2)
            Report.pto1_POW_DELTA.Text = POW4_DS '/ 1000 ' Math.Round(pto1_alta(4) / 1000, 2)

        Catch ex As Exception

        End Try



        Try
            If tensione_bassa_T = "3~230 Δ/400 Y" Or tensione_bassa_T = "3~400 Δ/690 Y" Then
                Report.pto3_I_STAR.Text = Math.Round(CURR3_DS * Math.Sqrt(3), 2) & " / " & CURR3_DS
            Else
                Report.pto3_I_STAR.Text = CURR3_DS
            End If
        Catch ex As Exception

        End Try

        Try
            If tensione_bassa_T = "3~230 Δ/400 Y" Or tensione_bassa_T = "3~400 Δ/690 Y" Then
                Report.pto2_I_STAR.Text = Math.Round(CURR2_DS * Math.Sqrt(3), 2) & " / " & CURR2_DS
            Else
                Report.pto2_I_STAR.Text = CURR2_DS
            End If
        Catch ex As Exception

        End Try

        Try
            If tensione_bassa_T = "3~230 Δ/400 Y" Or tensione_bassa_T = "3~400 Δ/690 Y" Then
                Report.pto1_I_STAR.Text = Math.Round(CURR1_DS * Math.Sqrt(3), 2) & " / " & CURR1_DS
            Else
                Report.pto1_I_STAR.Text = CURR1_DS
            End If
        Catch ex As Exception

        End Try


        Try
            If tensione_alta_T = "3~230 Δ/400 Y" Or tensione_alta_T = "3~400 Δ/690 Y" Then
                Report.pto3_I_DELTA.Text = Math.Round(CURR6_DS * Math.Sqrt(3), 2) & " / " & CURR6_DS
            Else
                Report.pto3_I_DELTA.Text = CURR6_DS
            End If
        Catch ex As Exception

        End Try

        Try
            If tensione_alta_T = "3~230 Δ/400 Y" Or tensione_alta_T = "3~400 Δ/690 Y" Then
                Report.pto2_I_DELTA.Text = Math.Round(CURR5_DS * Math.Sqrt(3), 2) & " / " & CURR5_DS
            Else
                Report.pto2_I_DELTA.Text = CURR5_DS
            End If
        Catch ex As Exception

        End Try

        Try
            If tensione_alta_T = "3~230 Δ/400 Y" Or tensione_alta_T = "3~400 Δ/690 Y" Then
                Report.pto1_I_DELTA.Text = Math.Round(CURR4_DS * Math.Sqrt(3), 2) & " / " & CURR4_DS
            Else
                Report.pto1_I_DELTA.Text = CURR4_DS
            End If
        Catch ex As Exception

        End Try

        Try
            Report.pto3_LwAtot_STAR.Text = LWA3_DS 'Math.Round(pto3_bassa(6), 1)
        Catch ex As Exception

        End Try

        Try
            Report.pto3_LwAps_STAR.Text = LWA3_DS - 3 'Math.Round(pto3_bassa(6), 1) - 3
        Catch ex As Exception

        End Try

        Try
            Report.pto3_LwAag2_STAR.Text = LWA3_DS - 9 'SMath.Round(pto3_bassa(6), 1) - 9
        Catch ex As Exception

        End Try


        Try
            Report.pto2_LwAtot_STAR.Text = LWA2_DS 'Math.Round(pto2_bassa(6), 1)
        Catch ex As Exception

        End Try

        Try
            Report.pto2_LwAps_STAR.Text = LWA2_DS - 3 'Math.Round(pto2_bassa(6), 1) - 3
        Catch ex As Exception

        End Try

        Try
            Report.pto2_LwAag2_STAR.Text = LWA2_DS - 9 'Math.Round(pto2_bassa(6), 1) - 9
        Catch ex As Exception

        End Try

        Try
            Report.pto1_LwAtot_STAR.Text = LWA1_DS 'Math.Round(pto1_bassa(6), 1)
        Catch ex As Exception

        End Try

        Try
            Report.pto1_LwAps_STAR.Text = LWA1_DS - 3 'Math.Round(pto1_bassa(6), 1) - 3
        Catch ex As Exception

        End Try

        Try
            Report.pto1_LwAag2_STAR.Text = LWA1_DS - 9 'Math.Round(pto1_bassa(6), 1) - 9
        Catch ex As Exception

        End Try


        Try
            Report.pto3_LwAtot_DELTA.Text = LWA6_DS 'Math.Round(pto3_alta(6), 1)
        Catch ex As Exception

        End Try

        Try
            Report.pto3_LwAps_DELTA.Text = LWA6_DS - 3 'Math.Round(pto3_alta(6), 1) - 3
        Catch ex As Exception

        End Try


        Try
            Report.pto3_LwAag2_DELTA.Text = LWA6_DS - 9 'Math.Round(pto3_alta(6), 1) - 9
        Catch ex As Exception

        End Try


        Try
            Report.pto2_LwAtot_DELTA.Text = LWA5_DS 'Math.Round(pto2_alta(6), 1)
        Catch ex As Exception

        End Try


        Try
            Report.pto2_LwAps_DELTA.Text = LWA5_DS - 3 'Math.Round(pto2_alta(6), 1) - 3
        Catch ex As Exception

        End Try


        Try
            Report.pto2_LwAag2_DELTA.Text = LWA5_DS - 9 'Math.Round(pto2_alta(6), 1) - 9
        Catch ex As Exception

        End Try


        Try
            Report.pto1_LwAtot_DELTA.Text = LWA4_DS 'Math.Round(pto1_alta(6), 1)
        Catch ex As Exception

        End Try


        Try
            Report.pto1_LwAps_DELTA.Text = LWA4_DS - 3 'Math.Round(pto1_alta(6), 1) - 3
        Catch ex As Exception

        End Try

        Try
            Report.pto1_LwAag2_DELTA.Text = LWA4_DS - 9 'Math.Round(pto1_alta(6), 1) - 9
        Catch ex As Exception

        End Try




        '----------------------------------------------------------------------------------------------------------------------





        '-----------------------------------------TABELLA PUNTO DI LAVORO-------------------------------------------------------

        If conn_bassa = "Y" Then

            Dim newImage_tri_star1 As Image = My.Resources.ResourceManager.GetObject("star_symbol_rep")
            Report.pto1_conn_STAR.ImageSource = New ImageSource(newImage_tri_star1)

            Dim newImage_tri_star2 As Image = My.Resources.ResourceManager.GetObject("star_symbol_rep_W")
            Report.pto2_conn_STAR.ImageSource = New ImageSource(newImage_tri_star2)

            Dim newImage_tri_star3 As Image = My.Resources.ResourceManager.GetObject("star_symbol_rep")
            Report.pto3_conn_STAR.ImageSource = New ImageSource(newImage_tri_star3)

            Dim newImage_tri_star4 As Image = My.Resources.ResourceManager.GetObject("star_symbol_rep_W")
            Report.pto_bassa_conn.ImageSource = New ImageSource(newImage_tri_star4)

        Else

            Dim newImage_tri_star1 As Image = My.Resources.ResourceManager.GetObject("delta_symbol_rep")
            Report.pto1_conn_STAR.ImageSource = New ImageSource(newImage_tri_star1)

            Dim newImage_tri_star2 As Image = My.Resources.ResourceManager.GetObject("delta_symbol_rep_W")
            Report.pto2_conn_STAR.ImageSource = New ImageSource(newImage_tri_star2)

            Dim newImage_tri_star3 As Image = My.Resources.ResourceManager.GetObject("delta_symbol_rep")
            Report.pto3_conn_STAR.ImageSource = New ImageSource(newImage_tri_star3)

            Dim newImage_tri_star4 As Image = My.Resources.ResourceManager.GetObject("delta_symbol_rep_W")
            Report.pto_bassa_conn.ImageSource = New ImageSource(newImage_tri_star4)

        End If





        If conn_alta = "Y" Then

            Dim newImage_tri_star1 As Image = My.Resources.ResourceManager.GetObject("star_symbol_rep_W")
            Report.pto1_conn_DELTA.ImageSource = New ImageSource(newImage_tri_star1)

            Dim newImage_tri_star2 As Image = My.Resources.ResourceManager.GetObject("star_symbol_rep")
            Report.pto2_conn_DELTA.ImageSource = New ImageSource(newImage_tri_star2)

            Dim newImage_tri_star3 As Image = My.Resources.ResourceManager.GetObject("star_symbol_rep_W")
            Report.pto3_conn_DELTA.ImageSource = New ImageSource(newImage_tri_star3)

            Dim newImage_tri_star4 As Image = My.Resources.ResourceManager.GetObject("star_symbol_rep")
            Report.pto_alta_conn.ImageSource = New ImageSource(newImage_tri_star4)

        Else

            Dim newImage_tri_star1 As Image = My.Resources.ResourceManager.GetObject("delta_symbol_rep_W")
            Report.pto1_conn_DELTA.ImageSource = New ImageSource(newImage_tri_star1)

            Dim newImage_tri_star2 As Image = My.Resources.ResourceManager.GetObject("delta_symbol_rep")
            Report.pto2_conn_DELTA.ImageSource = New ImageSource(newImage_tri_star2)

            Dim newImage_tri_star3 As Image = My.Resources.ResourceManager.GetObject("delta_symbol_rep_W")
            Report.pto3_conn_DELTA.ImageSource = New ImageSource(newImage_tri_star3)

            Dim newImage_tri_star4 As Image = My.Resources.ResourceManager.GetObject("delta_symbol_rep")
            Report.pto_alta_conn.ImageSource = New ImageSource(newImage_tri_star4)

        End If


        'Bassa Velocità
        If tensione_bassa_T = "3~230 Δ/400 Y" Then
            Report.lb_Volt1.Text = "3~230 Δ / 400 Y"
        ElseIf tensione_bassa_T = "3~400 Δ/690 Y" Then
            Report.lb_Volt1.Text = "3~400 Δ / 690 Y"
        Else
            Report.lb_Volt1.Text = numero_fasi_bassa & tensione_bassa_N
        End If


        'Alta Velocità
        If tensione_alta_T = "3~230 Δ/400 Y" Then
            Report.lb_Volt2.Text = "3~230 Δ / 400 Y"
        ElseIf tensione_alta_T = "3~400 Δ/690 Y" Then
            Report.lb_Volt2.Text = "3~400 Δ / 690 Y"
        Else
            Report.lb_Volt2.Text = numero_fasi_alta & tensione_alta_N
        End If


        Report.lb_freq1.Text = freq_bassa_N
        Report.lb_freq2.Text = freq_alta_N

        Report.lb_rpm1.Text = RPM_bassa_DS
        Report.lb_rpm2.Text = RPM_alta_DS

        Report.lb_pow1.Text = pow_bassa_DS
        Report.lb_pow2.Text = pow_alta_DS


        'Bassa Velocità
        If tensione_bassa_T = "3~230 Δ/400 Y" Or tensione_bassa_T = "3~400 Δ/690 Y" Then
            Try
                Report.Curr1.Text = Math.Round(I_bassa_DS * Math.Sqrt(3), 2) & " / " & I_bassa_DS
            Catch ex As Exception

            End Try
        Else
            Report.Curr1.Text = I_bassa_DS
        End If

        'Alta Velocità
        If tensione_alta_T = "3~230 Δ/400 Y" Or tensione_alta_T = "3~400 Δ/690 Y" Then
            Try
                Report.Curr2.Text = Math.Round(I_alta_DS * Math.Sqrt(3), 2) & " / " & I_alta_DS
            Catch ex As Exception

            End Try
        Else
            Report.Curr2.Text = I_alta_DS
        End If



        Report.lb_ins1.Text = Ins_DS
        Report.lb_ins2.Text = Ins_DS

        Report.lb_IP1.Text = IP_DS
        Report.lb_IP2.Text = IP_DS

        'modifica temperatura massima (orrotondata alla cinquina più vicina)
        Tmax_correction1(Tmax_DS)

        Report.lb_amb1.Text = Tmin_DS & "/+" & Tmax_starA
        Report.lb_amb2.Text = Tmin_DS & "/+" & Tmax_starA

        'Condizione di temperatura massima cuscinetti
        If Tmax_starA > 70 Then
            Report.lb_ambT.Text = "Tₐ  [°C]**"
            Report.XrLabel12.Visible = True
            Report.XrLabel12.Text = "**For temperature higher than 70 °C, class H insulation and high temperature kit is required."
        Else
            Report.XrLabel12.Visible = False
        End If

        '-----------------------------------------------------------------------------------------------------------------------

        Dim Efficiency_target1 As Double
        Dim Efficiency_fan1 As Double

        Try
            Efficiency_target1 = Efficiency_target
        Catch ex As Exception

        End Try

        Try
            Efficiency_fan1 = Efficiency_fan
        Catch ex As Exception

        End Try


        '-----------------------------------------TABELLA PUNTO DI LAVORO-------------------------------------------------------
        Report.eff_target.Text = Math.Round(Efficiency_target1, 1)
        Report.fan_eff.Text = Math.Round(Efficiency_fan1, 1)
        Report.Q_ERP.Text = ERP_Q
        Report.P_ERP.Text = ERP_P
        Report.RPM_ERP.Text = ERP_RPM
        Report.pow_ERP.Text = ERP_pow
        '-----------------------------------------------------------------------------------------------------------------------

        '----------------------------------------CONTROLLO ERP------------------------------------------------------------------
        Dim Eff_new As Double = true_eff * 1.1

        Try

            If true_eff < Efficiency_target Then

                If Eff_new > Efficiency_target Then
                    Report.fan_eff.Text = Math.Round(Efficiency_target * 1.01, 1)
                    Report.XrTable13.Visible = True
                Else
                    Report.XrTable13.Visible = False
                End If

            End If

            If ERP_check_var = True And CInt(ERP_pow) >= 125 Then
                Report.XrTable13.Visible = True
            Else
                Report.XrTable13.Visible = False
            End If



        Catch ex As Exception
            Report.XrTable13.Visible = False
        End Try


        '-----------------------------------------------------------------------------------------------------------------------




        Dim imm As DevExpress.XtraReports.UI.XRPictureBox
        Dim part As DevExpress.XtraReports.UI.XRLabel
        Dim des As DevExpress.XtraReports.UI.XRLabel

        Dim name_imm As String


        Dim num_prints As Integer

        'massimo 5 immagini per datasheet
        If num_conf_sel > 10 Then
            num_prints = 9
        Else
            num_prints = num_conf_sel - 1
        End If




        If num_conf_sel < 5 Then

            Report.des6.Visible = False
            Report.des7.Visible = False
            Report.des8.Visible = False
            Report.des9.Visible = False
            Report.des10.Visible = False

            Report.part6.Visible = False
            Report.part7.Visible = False
            Report.part8.Visible = False
            Report.part9.Visible = False
            Report.part10.Visible = False

            Report.Pic6.Visible = False
            Report.Pic7.Visible = False
            Report.Pic8.Visible = False
            Report.Pic9.Visible = False
            Report.Pic10.Visible = False

            Report.XrPictureBox15.Visible = False
            Report.XrLabel5.Visible = False
            Report.XrLabel6.Visible = False

        Else

            Report.des6.Visible = True
            Report.des7.Visible = True
            Report.des8.Visible = True
            Report.des9.Visible = True
            Report.des10.Visible = True

            Report.part6.Visible = True
            Report.part7.Visible = True
            Report.part8.Visible = True
            Report.part9.Visible = True
            Report.part10.Visible = True

            Report.Pic6.Visible = True
            Report.Pic7.Visible = True
            Report.Pic8.Visible = True
            Report.Pic9.Visible = True
            Report.Pic10.Visible = True

            Report.XrPictureBox15.Visible = True
            Report.XrLabel5.Visible = True
            Report.XrLabel6.Visible = True

        End If





        '---------------------------------------CICLO DI INSERIMENTO INMMAGINI--------------------------------------------------
        For i = 0 To 9
            imm = Report.Detail.Controls("Pic" & i + 1)
            part = Report.Detail.Controls("part" & i + 1)
            des = Report.Detail.Controls("des" & i + 1)
            imm.ImageSource = Nothing
            part.Text = ""
            des.Text = ""
        Next




        For i = 0 To num_prints

            imm = Report.Detail.Controls("Pic" & i + 1)
            part = Report.Detail.Controls("part" & i + 1)
            des = Report.Detail.Controls("des" & i + 1)

            If imm.Name = "Pic" & i + 1 Then



                Try

                    If mod_fan = "V" Then
                        name_imm = conf_sel(i).Substring(conf_sel(i).IndexOf("_") + 1, conf_sel(i).Length - conf_sel(i).IndexOf("_") - 1)
                    ElseIf mod_fan = "U" Then
                        name_imm = conf_sel(i).Substring(conf_sel(i).IndexOf("_") + 1, conf_sel(i).Length - conf_sel(i).IndexOf("_") - 1) & "_un"
                    ElseIf mod_fan = "A" Then
                        name_imm = conf_sel(i).Substring(conf_sel(i).IndexOf("_") + 1, conf_sel(i).Length - conf_sel(i).IndexOf("_") - 1) & "_atex"
                    End If


                    Dim image_fan_config As Image
                    If name_imm(0) = "9" Or name_imm(0) = "5" Or name_imm(0) = "0" Then
                        image_fan_config = My.Resources.ResourceManager.GetObject("_" & name_imm)
                    Else
                        image_fan_config = My.Resources.ResourceManager.GetObject(name_imm)
                    End If



                    imm.ImageSource = New ImageSource(image_fan_config)

                Catch ex As Exception

                    Dim image_VUOTA As Image = My.Resources.ResourceManager.GetObject("transparent1")
                    imm.ImageSource = New ImageSource(image_VUOTA)

                End Try




                '----------------------------------------codice di inserimento immagine mancante "MANCA FILE ICONA"-----------------------------------------------
                Dim txt_imm As String

                Try

                    If name_imm(0) = "9" Or name_imm(0) = "5" Or name_imm(0) = "0" Then
                        txt_imm = My.Resources.ResourceManager.GetObject("_" & name_imm)
                    Else
                        txt_imm = My.Resources.ResourceManager.GetObject(name_imm)
                    End If


                    If txt_imm = Nothing Then

                        Dim image_VUOTA As Image = My.Resources.ResourceManager.GetObject("transparent1")
                        imm.ImageSource = New ImageSource(image_VUOTA)

                    End If

                Catch ex As Exception

                End Try
                '---------------------------------------------------------------------------------------------------------------------------------------------------




                part.Text = part_sel(i)



                Try

                    Nome_fan_corretto1(descrizione_fan(2), name_imm(0), name_imm(1), name_imm(2))
                    des.Text = traduzione_ventilatore

                Catch ex As Exception
                    'Codice usato per l'identificazione del codice
                    des.Text = "Errore"
                End Try



            End If


        Next

        '-----------------------------------------------------------------------------------------------------------------------


    End Sub


End Module
