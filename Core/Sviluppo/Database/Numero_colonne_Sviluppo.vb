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

Module Numero_colonne_Sviluppo

    Public Sub Numero_colonne_Sviluppo1()

        Dim tableName = "Sviluppo"
        Dim filterValues = {Nothing, Nothing, tableName, Nothing}
        Dim i As Integer = 0

        Using conn = New OleDbConnection(constring)
            conn.Open()
            Dim columns = conn.GetSchema("Columns", filterValues)
            For Each row As DataRow In columns.Rows
                i = i + 1
                Nome_colonne_sviluppo(i) = row("column_name")

                Try
                    Descrizione_colonne_sviluppo(i) = row("Description")
                Catch ex As Exception
                    Descrizione_colonne_sviluppo(i) = ""
                End Try

                N_colonne_sviluppo = i
            Next
        End Using



    End Sub

End Module
