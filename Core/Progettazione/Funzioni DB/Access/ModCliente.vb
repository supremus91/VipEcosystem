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


Module ModCliente

    Public Sub ModCliente1(Nrevisione)


        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()


            Dim cmd As New OleDb.OleDbCommand

            Dim stringa_modifica As String

            If check_richiesta = 1 Or Nrevisione = 0 Then
                stringa_modifica = "check_RevCliente" & " = '" & 1 & "'"
            Else
                stringa_modifica = "check_RevCliente" & " = '" & 0 & "'"
            End If


            cmd.CommandText = "UPDATE Progetto SET " & stringa_modifica & " WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & " AND cbx_Revisione = '" & Nrevisione & "'" & ";"

            cmd.CommandType = CommandType.Text
            cmd.Connection = cn

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()



        End Using

    End Sub


End Module
