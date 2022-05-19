Imports DevExpress.XtraCharts
Imports DevExpress.XtraReports.UI
Imports System.Collections
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Drawing

Module PJ



    Public Sub PJ1()


        If num_conf_sel = 1 Then


            Report.XrTable5.Visible = True
            Report.XrTable6.Visible = True
            Report.XrTable7.Visible = True
            Report.XrTable8.Visible = True
            Report.XrTable9.Visible = True
            Report.XrTable10.Visible = True

            Report.XrPictureBox6.Visible = True
            Report.XrPictureBox7.Visible = True
            Report.XrPictureBox8.Visible = True
            Report.XrPictureBox3.Visible = True
            Report.XrPictureBox5.Visible = True
            Report.XrPictureBox43.Visible = True

            If mod_fan = "A" Then
                Report.XrTable10.Visible = True
            Else
                Report.XrTable10.Visible = False
            End If

            Dim singlo_doppia As Integer = 0


            Try
                If pow_alta_DS > 0 Then
                    singlo_doppia = 2
                End If
            Catch ex As Exception
                singlo_doppia = 1
            End Try



            'Diametro
            Dim diametro = descrizione_fan(4) & descrizione_fan(5) & descrizione_fan(6)

            Select Case diametro
                Case "020"
                    Report.XrDiameter.Text = "200 mm"
                Case "025"
                    Report.XrDiameter.Text = "250 mm"
                Case "031"
                    Report.XrDiameter.Text = "310 mm"
                Case "035"
                    Report.XrDiameter.Text = "350 mm"
                Case "040"
                    Report.XrDiameter.Text = "400 mm"
                Case "045"
                    Report.XrDiameter.Text = "450 mm"
                Case "050"
                    Report.XrDiameter.Text = "500 mm"
                Case "056"
                    Report.XrDiameter.Text = "560 mm"
                Case "063"
                    Report.XrDiameter.Text = "630 mm"
                Case "071"
                    Report.XrDiameter.Text = "710 mm"
                Case "080"
                    Report.XrDiameter.Text = "800 mm"
                Case "091"
                    Report.XrDiameter.Text = "910 mm"
                Case "100"
                    Report.XrDiameter.Text = "1000 mm"
                Case "125"
                    Report.XrDiameter.Text = "1250 mm"
                Case "134"
                    Report.XrDiameter.Text = "1340 mm"
                Case "156"
                    Report.XrDiameter.Text = "1560 mm"
            End Select


            'Configurazione
            For i As Integer = 0 To numero_DS_conf - 1
                If DS_config_lista(i, 1) = PJ_config_star Then
                    Report.XrConfiguration.Text = DS_config_lista(i, 3)
                End If
            Next

            'AirStream
            If PJ_config_star.Length > 1 Then

                If PJ_config_star(1) = "P" Then
                    Report.XrStream.Text = "Blowing"
                Else
                    Report.XrStream.Text = "Sucking"
                End If

            Else
                Report.XrStream.Text = "Sucking"
            End If


            'Temperature
            Report.XrTemperature.Text = Tmin_DS & "/" & Tmax_DS & " °C"


            'Materiale viti
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_VitiFan" Then
                    Report.XrBolts.Text = Valore_CellaRigaDS_star(i)
                End If
            Next

            'Materiale viti
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_VitiFan" Then
                    Report.XrBolts.Text = Valore_CellaRigaDS_star(i)
                End If
            Next



            'Caratteristiche ventola
            Report.Xr_Imp_gen.Text = descrizione_fan(1) & "-" & descrizione_fan(19) & descrizione_fan(20) & " / " & descrizione_fan(8) & descrizione_fan(9) & "°" & " / " & descrizione_fan(20)



            '*********************************************************************************************************************
            'Materiale pale
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_MaterialeVentola" Then
                    Report.Xr_Imp_Mat_Treat_Corr.Text = Valore_CellaRigaDS_star(i)
                End If
            Next

            'Trattamento pale
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_TrattamentoSupVentola" Then
                    Report.Xr_Imp_Mat_Treat_Corr.Text = Report.Xr_Imp_Mat_Treat_Corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next


            'Corrosione pale
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_ClasseCorrVentola" Then
                    Report.Xr_Imp_Mat_Treat_Corr.Text = Report.Xr_Imp_Mat_Treat_Corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next
            '*********************************************************************************************************************


            'Materiale viti
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_MaterialeVitiVentola" Then
                    Report.Xr_Imp_Bolts.Text = Valore_CellaRigaDS_star(i)
                End If
            Next



            '*********************************************************************************************************************
            'Hub material
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_MaterialeMozzo" Then
                    Report.Xr_Imp_hub_Mat_Treat_Corr.Text = Valore_CellaRigaDS_star(i)
                End If
            Next


            'Hub trattamento
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_TrattamentoMozzo" Then
                    Report.Xr_Imp_hub_Mat_Treat_Corr.Text = Report.Xr_Imp_hub_Mat_Treat_Corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next



            'Hub corrosione
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_ClasseCorrMozzo" Then
                    Report.Xr_Imp_hub_Mat_Treat_Corr.Text = Report.Xr_Imp_hub_Mat_Treat_Corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next

            'Hub colore
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_ColoreMozzo" Then
                    Report.Xr_Imp_hub_Mat_Treat_Corr.Text = Report.Xr_Imp_hub_Mat_Treat_Corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next
            '*********************************************************************************************************************





            '*********************************************************************************************************************
            'Boss material
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_MaterialeRaggera" Then
                    Report.Xr_Imp_boss_Mat_Treat_Corr.Text = Valore_CellaRigaDS_star(i)
                End If
            Next


            'Boss trattamento
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_TrattamentoRaggera" Then
                    Report.Xr_Imp_boss_Mat_Treat_Corr.Text = Report.Xr_Imp_boss_Mat_Treat_Corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next



            'Boss corrosione
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_ClasseCorrRaggera" Then
                    Report.Xr_Imp_boss_Mat_Treat_Corr.Text = Report.Xr_Imp_boss_Mat_Treat_Corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next

            'Boss colore
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_ColoreRaggera" Then
                    Report.Xr_Imp_boss_Mat_Treat_Corr.Text = Report.Xr_Imp_boss_Mat_Treat_Corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next

            '*********************************************************************************************************************



            'Certificato IEC
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_IEX" Then
                    Report.Xr_mot_Eff_cert.Text = Valore_CellaRigaDS_star(i)
                End If
            Next



            'identifico il numero dei poli
            Dim poli_codice As String = descrizione_fan(11)
            Dim poli As String = ""

            Select Case poli_codice

                Case "2"
                    poli = 2
                Case "4"
                    poli = 4
                Case "6"
                    poli = 6
                Case "8"
                    poli = 8
                Case "A"
                    poli = 8
                Case "B"
                    poli = "4/6 Double Velocity"
                Case "C"
                    poli = 12
                Case "D"
                    poli = "10 Double Velocity"
                Case "E"
                    poli = "Electronic"
                Case "F"
                    poli = "6/8 Double Velocity"
                Case "G"
                    poli = "2/4 Double Velocity"
                Case "M"
                    poli = "10 Double Velocity"
                Case "N"
                    poli = "12 Double Velocity"
                Case "P"
                    poli = "2 Double Velocity"
                Case "S"
                    poli = "6 Double Velocity"
                Case "V"
                    poli = "4 Double Velocity"
                Case "Z"
                    poli = "4/8 Double Velocity"
            End Select




            If singlo_doppia = 2 Then

                'caratteristiche motore
                If mod_fan = "V" Then
                    Report.Xr_mot_frame_pow_poles.Text = Math.Round(pow_alta_DS / 1000, 1) & " kW" & " / " & poli
                Else
                    Report.Xr_mot_frame_pow_poles.Text = frame_sel & " / " & Math.Round(pow_alta_DS / 1000, 1) & " kW" & " / " & poli
                End If

            Else

                'caratteristiche motore
                If mod_fan = "V" Then
                    Report.Xr_mot_frame_pow_poles.Text = Math.Round(pow_bassa_DS / 1000, 1) & " kW" & " / " & poli
                Else
                    Report.Xr_mot_frame_pow_poles.Text = frame_sel & " / " & Math.Round(pow_bassa_DS / 1000, 1) & " kW" & " / " & poli
                End If

            End If



            'Certificato IEC
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_costruzione" Then
                    Report.Xr_mot_construction.Text = Valore_CellaRigaDS_star(i)
                End If
            Next

            'collegamento

            If singlo_doppia = 2 Then 'caso di doppia velocita'
                Report.Xr_mot_conn.Text = conn_alta
            Else
                Report.Xr_mot_conn.Text = conn_bassa
            End If


            'classe IP
            Report.Xr_mot_IP.Text = IP_DS

            'Insulation class
            Report.Xr_mot_insulation.Text = Ins_DS


            'Certificato cooling
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_cooling" Then
                    Report.Xr_mot_cooling.Text = Valore_CellaRigaDS_star(i)
                End If
            Next


            'Tensione
            If singlo_doppia = 2 Then
                Report.Xr_mot_Voltage.Text = tensione_alta_N & " V"
            Else
                Report.Xr_mot_Voltage.Text = tensione_alta_N & " V"
            End If

            'Frequenza
            If singlo_doppia = 2 Then
                Report.Xr_mot_freq.Text = freq_alta_N & " Hz"
            Else
                Report.Xr_mot_freq.Text = freq_alta_N & " Hz"
            End If


            'Numero fasi
            If singlo_doppia = 2 Then
                Report.Xr_mot_supply.Text = numero_fasi_alta
            Else
                Report.Xr_mot_supply.Text = numero_fasi_bassa
            End If



            '*********************************************************************************************************************
            'Body material
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_materiale" Then
                    Report.Xr_body_shield.Text = Valore_CellaRigaDS_star(i)
                End If
            Next

            'Body material scudi
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_MaterialeScudi" Then
                    Report.Xr_body_shield.Text = Report.Xr_body_shield.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next
            '*********************************************************************************************************************



            '*********************************************************************************************************************
            'Surface treat
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_MotorTreat" Then
                    Report.Xr_mot_treat_color.Text = Valore_CellaRigaDS_star(i)
                End If
            Next

            'Surface color
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_colore" Then
                    Report.Xr_mot_treat_color.Text = Report.Xr_mot_treat_color.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next
            '*********************************************************************************************************************

            'mot corr
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_corrosione" Then
                    Report.Xr_mot_corr.Text = Valore_CellaRigaDS_star(i)
                End If
            Next


            For i = 0 To Numero_colonneDS - 1
                    If Nome_colonneDS(i + 1) = "check_InternalPainting" Then

                        If Valore_CellaRigaDS_star(i) = True Then
                            Report.Xr_mot_internal.Text = "yes"
                        Else
                            Report.Xr_mot_internal.Text = "no"
                        End If

                    End If
                Next

                'mot corr



                'trattamento windigs
                For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "check_WindingTreatment" Then

                    If Valore_CellaRigaDS_star(i) = True Then
                        Report.Xr_mot_windings.Text = "yes"
                    Else
                        Report.Xr_mot_windings.Text = "no"
                    End If

                End If
            Next



            '*********************************************************************************************************************
            'PTC
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "check_PTC" Then

                    If Valore_CellaRigaDS_star(i) = True Then
                        Report.Xr_mot_PTC_PTO.Text = "yes"
                    Else
                        Report.Xr_mot_PTC_PTO.Text = "no"
                    End If

                End If
            Next


            'PTC
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "check_PTO" Then

                    If Valore_CellaRigaDS_star(i) = True Then
                        Report.Xr_mot_PTC_PTO.Text = Report.Xr_mot_PTC_PTO.Text & " / " & "yes"
                    Else
                        Report.Xr_mot_PTC_PTO.Text = Report.Xr_mot_PTC_PTO.Text & " / " & "no"
                    End If

                End If
            Next


            '*********************************************************************************************************************


            'Scaldiglie
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "check_Scaldiglie" Then

                    If Valore_CellaRigaDS_star(i) = True Then
                        Report.Xr_mot_heaters.Text = "yes"
                    Else
                        Report.Xr_mot_heaters.Text = "no"
                    End If

                End If
            Next



            'Rain cap
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "check_CappelloParapioggia" Then

                    If Valore_CellaRigaDS_star(i) = True Then
                        Report.Xr_mot_rain.Text = "yes"
                    Else
                        Report.Xr_mot_rain.Text = "no"
                    End If

                End If
            Next



            'Water disk
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "check_DiscoParapioggia" Then

                    If Valore_CellaRigaDS_star(i) = True Then
                        Report.Xr_mot_Disk.Text = "yes"
                    Else
                        Report.Xr_mot_Disk.Text = "no"
                    End If

                End If
            Next


            'Fori laterali
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "check_Forilaterali" Then

                    If Valore_CellaRigaDS_star(i) = True Then
                        Report.Xr_mot_drain.Text = "yes"
                    Else
                        Report.Xr_mot_drain.Text = "no"
                    End If

                End If
            Next


            'Inverter
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "check_Inverter" Then

                    If Valore_CellaRigaDS_star(i) = True Then
                        Report.Xr_mot_inverter.Text = "yes"
                    Else
                        Report.Xr_mot_inverter.Text = "no"
                    End If

                End If
            Next


            'Taglio di fase
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "check_taglio" Then

                    If Valore_CellaRigaDS_star(i) = True Then
                        Report.Xr_mot_inverter.Text = Report.Xr_mot_inverter.Text & " / " & "yes"
                    Else
                        Report.Xr_mot_inverter.Text = Report.Xr_mot_inverter.Text & " / " & "no"
                    End If

                End If
            Next


            'Tropicalization
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "check_TropicalizzazioneRot" Then

                    If Valore_CellaRigaDS_star(i) = True Then
                        Report.Xr_mot_rotor.Text = "yes"
                    Else
                        Report.Xr_mot_rotor.Text = "no"
                    End If

                End If
            Next

            'Tropicalization
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "check_TropicalizzazioneStat" Then

                    If Valore_CellaRigaDS_star(i) = True Then
                        Report.Xr_mot_stator.Text = "yes"
                    Else
                        Report.Xr_mot_stator.Text = "no"
                    End If

                End If
            Next


            'Conv type
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_PJ_conv_type" Then
                    Report.Xr_conv_type.Text = Valore_CellaRigaDS_star(i)
                End If
            Next



            '*********************************************************************************************************************
            'Conv material
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_MaterialeConvogliatore" Then
                    Report.Xr_conv_mat_treat_col_corr.Text = Valore_CellaRigaDS_star(i)
                End If
            Next


            'Conv trattamento
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_TrattamentoConvogliatore" Then
                    Report.Xr_conv_mat_treat_col_corr.Text = Report.Xr_conv_mat_treat_col_corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next


            'Conv trattamento
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_ColoreConvogliatore" Then
                    Report.Xr_conv_mat_treat_col_corr.Text = Report.Xr_conv_mat_treat_col_corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next



            'Conv corrosione
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_ClasseConvogliatore" Then
                    Report.Xr_conv_mat_treat_col_corr.Text = Report.Xr_conv_mat_treat_col_corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next
            '*********************************************************************************************************************



            'Conv type
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_PJ_supp_type" Then
                    Report.Xr_supp_type.Text = Valore_CellaRigaDS_star(i)
                End If
            Next




            '*********************************************************************************************************************
            'Supp material
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_MaterialeSupporto" Then
                    Report.Xr_supp_mat_treat_col_corr.Text = Valore_CellaRigaDS_star(i)
                End If
            Next


            'Supp trattamento
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_TrattamentoSupporto" Then
                    Report.Xr_supp_mat_treat_col_corr.Text = Report.Xr_supp_mat_treat_col_corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next

            'Supp colore
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_ColoreSupporto" Then
                    Report.Xr_supp_mat_treat_col_corr.Text = Report.Xr_supp_mat_treat_col_corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next


            'Supp corrosione
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "cbx_ClasseSupporto" Then
                    Report.Xr_supp_mat_treat_col_corr.Text = Report.Xr_supp_mat_treat_col_corr.Text & " / " & Valore_CellaRigaDS_star(i)
                End If
            Next
            '*********************************************************************************************************************

            Try
                Report.Xr_atex_group.Text = cbx_AtexProtezioneDS
                Report.Xr_atex_zone.Text = cbx_AtexCategoriaDS
                Report.Xr_atex_prot.Text = cbx_AtexCustodiaDS
                Report.Xr_atex_expl.Text = cbx_AtexClasseTemperaturaDS
                Report.Xr_atex_cat.Text = cbx_AtexCategoriaDS(0)
            Catch ex As Exception

            End Try



            regole_speciali()


        Else


            Report.XrTable5.Visible = False
            Report.XrTable6.Visible = False
            Report.XrTable7.Visible = False
            Report.XrTable8.Visible = False
            Report.XrTable9.Visible = False
            Report.XrTable10.Visible = False


            Report.XrPictureBox6.Visible = False
            Report.XrPictureBox7.Visible = False
            Report.XrPictureBox8.Visible = False
            Report.XrPictureBox3.Visible = False
            Report.XrPictureBox5.Visible = False
            Report.XrPictureBox43.Visible = False




        End If





    End Sub

    Public Sub regole_speciali()


        Dim dimensione_motore As String = descrizione_fan(0)
        Dim configurazione As String


        If num_conf_sel = 1 Then
            configurazione = conf_sel(0)(4)
        Else
            configurazione = "_"
        End If


        '-----------------------------------------------------------------------------PTO = yes--------------------------------------------------------------------------------------------------------
        'motore Vip dal 125 al 200
        If dimensione_motore = "C" Or dimensione_motore = "T" Or dimensione_motore = "E" Or dimensione_motore = "F" Or dimensione_motore = "V" Or dimensione_motore = "R" Or dimensione_motore = "S" Then

            'PTC
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "check_PTC" Then

                    If Valore_CellaRigaDS_star(i) = True Then
                        Report.Xr_mot_PTC_PTO.Text = "yes"
                    Else
                        Report.Xr_mot_PTC_PTO.Text = "no"
                    End If

                End If
            Next

            Report.Xr_mot_PTC_PTO.Text = Report.Xr_mot_PTC_PTO.Text & "/ yes" 'hanno la PTC

        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        '-----------------------------------------------------------------------------Inverter = yes-----------------------------------------------------------------------------------------------------------
        If dimensione_motore = "E" Or dimensione_motore = "V" Or dimensione_motore = "F" Then
            'Inverter
            Report.Xr_mot_inverter.Text = "yes"

            'Taglio di fase
            For i = 0 To Numero_colonneDS - 1
                If Nome_colonneDS(i + 1) = "check_taglio" Then

                    If Valore_CellaRigaDS_star(i) = True Then
                        Report.Xr_mot_inverter.Text = Report.Xr_mot_inverter.Text & " / " & "yes"
                    Else
                        Report.Xr_mot_inverter.Text = Report.Xr_mot_inverter.Text & " / " & "no"
                    End If

                End If
            Next

        End If
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        '-----------------------------------------------------------------------Classe di efficienza------------------------------------------------------------------------------------------------------
        If Report.Xr_mot_Eff_cert.Text = "Non soggetto" Then
            Report.XrTableCell191.Visible = False
            Report.Xr_mot_Eff_cert.Visible = False
        Else
            Report.XrTableCell191.Visible = True
            Report.Xr_mot_Eff_cert.Visible = True
        End If
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        '-------------------------------------------------------------------------------Convogliatore-------------------------------------------------------------------------------------------------------
        If configurazione = "F" Or configurazione = "M" Or configurazione = "X" Or configurazione = "C" Then

            Report.XrTable8.Visible = False
            Report.XrPictureBox3.Visible = False

        End If
        '---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



    End Sub



End Module
