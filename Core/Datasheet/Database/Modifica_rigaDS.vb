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


Module Modifica_rigaDS




    Public Sub Modifica_rigaDS1()



        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()

            Dim stringa_aggiorna As String

            For i = 0 To aggiunta_val_DS - 1

                stringa_aggiorna = stringa_aggiorna & vettore_nomi_aggiornamentoDS(i) & " = '" & vettore_aggiornamentoDS(i) & "',"

            Next

            '---------------------------------------configurazioni selezionate--------------------------------------------
            Dim stringa_configurazioni As String = ""

            'compongo la stringa delle configurazioni
            For i = 0 To num_conf_sel - 1
                stringa_configurazioni = stringa_configurazioni & conf_sel(i) & ";"
            Next

            stringa_aggiorna = stringa_aggiorna & "conf1" & " = '" & stringa_configurazioni & "',"
            '-------------------------------------------------------------------------------------------------------------


            '------------------------------------------part numbers-------------------------------------------------------
            Dim stringa_part As String = ""

            'compongo la stringa delle configurazioni
            For i = 0 To num_conf_sel - 1
                stringa_part = stringa_part & part_sel(i) & ";"
            Next

            stringa_aggiorna = stringa_aggiorna & "part1" & " = '" & stringa_part & "',"
            '-------------------------------------------------------------------------------------------------------------


            'data odierna
            stringa_aggiorna = stringa_aggiorna & "Data" & " = '" & data & "',"

            'modifica NOTE
            stringa_aggiorna = stringa_aggiorna & "Annotazione" & " = '" & testRTF1 & "',"



            'configurazioni selezionate
            For i = 0 To 2
                stringa_aggiorna = stringa_aggiorna & "coeff" & i + 1 & " = '" & coeff_bassa(i) & "',"
            Next
            'configurazioni selezionate
            For i = 3 To 5
                stringa_aggiorna = stringa_aggiorna & "coeff" & i + 1 & " = '" & coeff_alta(i - 3) & "',"
            Next



            'nome file 1
            stringa_aggiorna = stringa_aggiorna & "file1" & " = '" & directory_exc1 & "',"

            'nome file 2
            stringa_aggiorna = stringa_aggiorna & "file2" & " = '" & directory_exc2 & "',"

            'atex, vip o unificato
            stringa_aggiorna = stringa_aggiorna & "Tipo_motore" & " = '" & mod_fan & "',"

            'NOTE
            stringa_aggiorna = stringa_aggiorna & "tbx_NOTE" & " = '" & testRTF1 & "',"

            'Sito
            stringa_aggiorna = stringa_aggiorna & "sito" & " = '" & 1 & "',"

            'Numero progettazione
            stringa_aggiorna = stringa_aggiorna & "Progettazione" & " = '" & PJ_ref & "'" 'ATTENZIONE ULTIMA RIGA DI string_aggiorna DEVE TERMINARE CON --> "'"


            Dim cmd As New OleDb.OleDbCommand


            cmd.CommandText = "UPDATE Datasheets SET " & stringa_aggiorna & " WHERE Descrizione = '" & descrizione_fan & "'" & ";"

            cmd.CommandType = CommandType.Text
            cmd.Connection = cn

            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception

            End Try

            cmd.Dispose()



            cn.Close()

        End Using




    End Sub






End Module
