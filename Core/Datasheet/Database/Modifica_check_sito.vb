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


Module Modifica_check_sito

    Public Sub Modifica_check_sito1(stato, descrizione)


        Dim sito_state As String = stato
        Dim descrizione_sel As String = descrizione
        Using cn As New OleDb.OleDbConnection(constring)
            'provider to be used when working with access database
            cn.Open()

            Dim cmd As New OleDb.OleDbCommand

            If sito_state = False Then
                cmd.CommandText = "UPDATE Datasheets SET sito = 1 WHERE Descrizione = '" & descrizione_sel & "'" & ";"
            Else
                cmd.CommandText = "UPDATE Datasheets SET sito = 0 WHERE Descrizione = '" & descrizione_sel & "'" & ";"
            End If

            cmd.CommandType = CommandType.Text
            cmd.Connection = cn
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cn.Close()

        End Using



    End Sub








End Module
