Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports System.Threading
Imports System
Imports System.IO
Imports System.Text
Imports System.Net
Imports System.Security.AccessControl
Imports System.Net.Mail
Imports System.Runtime.InteropServices


Module Modifica_riga


    Public Sub Modifica_riga1(Nrevisione)



        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()

            Dim stringa_aggiorna As String

            For i = 0 To aggiunta_val - 1

                If i < aggiunta_val - 1 Then



                    If vettore_nomi_aggiornamento(i) = "check_RevCliente" Then


                        If check_richiesta = 1 Then
                            stringa_aggiorna = stringa_aggiorna & vettore_nomi_aggiornamento(i) & " = '" & 1 & "',"
                        Else
                            stringa_aggiorna = stringa_aggiorna & vettore_nomi_aggiornamento(i) & " = '" & 0 & "',"
                        End If


                    Else
                        stringa_aggiorna = stringa_aggiorna & vettore_nomi_aggiornamento(i) & " = '" & vettore_aggiornamento(i) & "',"
                    End If

                Else

                    If vettore_nomi_aggiornamento(i) = "check_RevCliente" Then

                        If check_richiesta = 1 Then
                            stringa_aggiorna = stringa_aggiorna & vettore_nomi_aggiornamento(i) & " = '" & 1 & "'"
                        Else
                            stringa_aggiorna = stringa_aggiorna & vettore_nomi_aggiornamento(i) & " = '" & 0 & "'"
                        End If


                    Else
                        stringa_aggiorna = stringa_aggiorna & vettore_nomi_aggiornamento(i) & " = '" & vettore_aggiornamento(i) & "'"
                    End If


                End If

            Next


            Dim cmd As New OleDb.OleDbCommand


            cmd.CommandText = "UPDATE Progetto SET " & stringa_aggiorna & " WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & " AND cbx_Revisione = '" & Nrevisione & "'" & ";"

            cmd.CommandType = CommandType.Text
            cmd.Connection = cn

            cmd.ExecuteNonQuery()
            cmd.Dispose()




            Dim stringa_modifica As String = "tbx_data" & " = '" & Date.Today.Day & "/" & Date.Today.Month & "/" & Date.Today.Year & "'"

            cmd.CommandText = "UPDATE Progetto SET " & stringa_modifica & " WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & " AND cbx_Revisione = '" & Nrevisione & "'" & ";"
            cmd.CommandType = CommandType.Text
            cmd.Connection = cn
            cmd.ExecuteNonQuery()
            cmd.Dispose()




            cn.Close()




        End Using





    End Sub




End Module
