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

Module Aggiungi_Catalogo

    Public Sub Aggiungi_Catalogo1()

        Using myconnection As New OleDbConnection(constring)
            myconnection.Open()
            Dim sqlQry As String = "INSERT INTO [Catalogo] ([NomeSerie], [cf1], [cf2], [cf3], [cf4], [cf5], [cf6], [cf7], [cf8], [cf9], [cf10]) VALUES (@serie, @conf1, @conf2, @conf3, @conf4, @conf5, @conf6, @conf7, @conf8, @conf9, @conf10)"

            Using cmd As New OleDbCommand(sqlQry, myconnection)

                cmd.Parameters.AddWithValue("@serie", vettore_aggiornamentoCAT(0))
                cmd.Parameters.AddWithValue("@conf1", vettore_aggiornamentoCAT(1))
                cmd.Parameters.AddWithValue("@conf2", vettore_aggiornamentoCAT(2))
                cmd.Parameters.AddWithValue("@conf3", vettore_aggiornamentoCAT(3))
                cmd.Parameters.AddWithValue("@conf4", vettore_aggiornamentoCAT(4))
                cmd.Parameters.AddWithValue("@conf5", vettore_aggiornamentoCAT(5))
                cmd.Parameters.AddWithValue("@conf6", vettore_aggiornamentoCAT(6))
                cmd.Parameters.AddWithValue("@conf7", vettore_aggiornamentoCAT(7))
                cmd.Parameters.AddWithValue("@conf8", vettore_aggiornamentoCAT(8))
                cmd.Parameters.AddWithValue("@conf9", vettore_aggiornamentoCAT(9))
                cmd.Parameters.AddWithValue("@conf10", vettore_aggiornamentoCAT(10))


                cmd.ExecuteNonQuery()

            End Using


        End Using


    End Sub




End Module
