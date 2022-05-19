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

Module Lettura_RefExcel

    Public Sub Lettura_RefExcel1()




        Dim tableName = "RefExcel"
        Dim filterValues = {Nothing, Nothing, tableName, Nothing}
        Dim i As Integer = 0

        Using conn = New OleDbConnection(constring_RefExcel)

            conn.Open()
            Dim columns = conn.GetSchema("Columns", filterValues)
            For Each row As DataRow In columns.Rows
                i = i + 1

                If row("column_name") <> "ID" Then
                    Nome_colonne_SW(i) = row("column_name")
                Else
                    i = i - 1
                End If
            Next

        End Using

        Numero_colonne_SW = i

        Using cn As New OleDb.OleDbConnection(constring_RefExcel)

            cn.Open()
            Dim cmd As New OleDb.OleDbCommand("SELECT * FROM RefExcel WHERE ID = 1", cn)
            Dim myreader As OleDbDataReader

            myreader = cmd.ExecuteReader
            myreader.Read()

            Try

                For i = 0 To Numero_colonne_SW - 1

                    Valore_CellaRiga_SW(i) = myreader(Nome_colonne_SW(i + 1))

                Next

            Catch ex As Exception

            End Try





        End Using



    End Sub



End Module
