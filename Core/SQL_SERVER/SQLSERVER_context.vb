Module SQLSERVER_context

    Public Sub SQLSERVER_context1(nome_fan_conf, n, Cat)

        '*************************************************SQL SERVER**************************************************************************************
        Dim SQL_dir As String = folders_directoryDS & "\" & descrizione_fan & "\" & conf_sel(0)(4) & conf_sel(0)(5) & conf_sel(0)(6)
        Dim SQL_dir_gen As String = folders_directoryDS & "\" & descrizione_fan
        Dim SQL_conf As String
        Dim SQL_diam As String = nome_fan_conf(4) & nome_fan_conf(5) & nome_fan_conf(3)
        Dim SQL_Coeff1 As Double
        Dim SQL_Coeff2 As Double
        Dim SQL_Coeff3 As Double
        Dim SQL_Qmin As Double
        Dim SQL_Qmax As Double
        Dim SQL_pow As Double = DS_totale_dati(n, 15)
        Dim SQL_Q As Double
        Dim SQL_P As Double
        Dim SQL_RPM As Double = DS_totale_dati(n, 18)
        Dim SQL_I As Double


        If DS_totale_dati(n, 29) <> "" Then
            SQL_I = DS_totale_dati(n, 29)
        Else
            SQL_I = DS_totale_dati(n, 24)
        End If


        Dim diam_check As Integer = nome_fan_conf(3) & nome_fan_conf(4) & nome_fan_conf(5)

        If diam_check >= 100 Then
            SQL_diam = nome_fan_conf(3) & nome_fan_conf(4) & nome_fan_conf(5) & "0"
        End If


        'distinguo tra configurazione ad un e 2 lettere
        If conf_sel(0)(5) <> "_" Then
            SQL_conf = conf_sel(0)(4) & conf_sel(0)(5)
        Else
            SQL_conf = conf_sel(0)(4)
        End If
        coeff_bassa(0) = A_parabola
        coeff_bassa(1) = B_parabola
        coeff_bassa(2) = C_parabola


        Dim SQL_N_vel As String

        If S_D_var = 1 Then 'singola velocità
            SQL_Coeff1 = coeff_bassa(0)
            SQL_Coeff2 = coeff_bassa(1)
            SQL_Coeff3 = coeff_bassa(2)
            SQL_Qmin = DS_totale_dati(n, 44)
            SQL_Qmax = DS_totale_dati(n, 32)
            SQL_N_vel = "Single"
            SQL_P = DS_totale_dati(n, 45)
            SQL_Q = DS_totale_dati(n, 32)
        Else 'doppia velocità

            SQL_Coeff1 = coeff_alta(0)
            SQL_Coeff2 = coeff_alta(1)
            SQL_Coeff3 = coeff_alta(2)
            SQL_Qmin = DS_totale_dati(n, 62)
            SQL_Qmax = DS_totale_dati(n, 50)
            SQL_N_vel = "Double"
            SQL_P = DS_totale_dati(n, 63)
            SQL_Q = DS_totale_dati(n, 50)
        End If



        If num_conf_sel = 1 Then
            SQLSERVER_Add_Data1(SQL_dir, nome_fan_conf, SQL_conf, conf_sel(0)(6), SQL_diam, nome_fan_conf(10), numero_fasi_bassa(0), tensione_bassa_N, freq_bassa_N, SQL_Coeff1, SQL_Coeff2, SQL_Coeff3, Tmin_DS, Tmax_DS, SQL_Qmin, SQL_Qmax, SQL_pow, SQL_Q, SQL_P, SQL_RPM, SQL_I, SQL_N_vel, Cat)
        Else
            SQLSERVER_Add_Data1(SQL_dir_gen, descrizione_fan, "-", "-", SQL_diam, nome_fan_conf(10), numero_fasi_bassa(0), tensione_bassa_N, freq_bassa_N, SQL_Coeff1, SQL_Coeff2, SQL_Coeff3, Tmin_DS, Tmax_DS, SQL_Qmin, SQL_Qmax, SQL_pow, SQL_Q, SQL_P, SQL_RPM, SQL_I, SQL_N_vel, Cat)
        End If



    End Sub





End Module
