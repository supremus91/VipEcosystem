Imports DevExpress.XtraCharts
Imports DevExpress.XtraReports.UI
Imports System.Collections
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Drawing

Module Header


    Public Sub Header1()



        If num_conf_sel = 1 Then

            Dim caso As Integer = -1

            Dim nome_config As String

            Nome_fan_corretto1(descrizione_fan(2), conf_sel(0)(4), conf_sel(0)(5), conf_sel(0)(6))

            Report.XrLabel1.Text = traduzione_ventilatore


        Else

            Try
                If descrizione_fan(2) = "_" Then
                    Report.XrLabel1.Text = descrizione_fan(0) & descrizione_fan(1) & " " & descrizione_fan.Substring(4, descrizione_fan.Length - 4)
                Else
                    Report.XrLabel1.Text = descrizione_fan
                End If
            Catch ex As Exception

            End Try


        End If



    End Sub


End Module
