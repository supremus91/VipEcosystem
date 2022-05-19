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

Module ConnStateUser

    Public Sub ConnStateUser1(comando)


        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()


            Dim cmd As New OleDb.OleDbCommand

            Dim stringa_modifica As String


            If comando = 1 Then ' caso in cui apro la prgetta
                stringa_modifica = "tbx_ConnState" & " = '" & UserNum & "'"
            Else
                stringa_modifica = "tbx_ConnState" & " = '" & 0 & "'"
            End If


            'cmd.CommandText = "UPDATE Progetto SET " & stringa_modifica & " WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & " AND cbx_Revisione = '" & Nrevisione & "'" & ";"
            cmd.CommandText = "UPDATE Progetto SET " & stringa_modifica & " WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & ";"

            cmd.CommandType = CommandType.Text
            cmd.Connection = cn

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()




        End Using







    End Sub


End Module
