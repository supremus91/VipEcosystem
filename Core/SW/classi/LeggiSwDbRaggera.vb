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


Public Class LeggiSwDbRaggera




    Public Sub Main(Ventola, Npale, Motore)

        Try

            Using cn As New OleDb.OleDbConnection(constring_RefExcel)
                'provider to be used when working with access database
                cn.Open()
                Dim cmd As New OleDb.OleDbCommand("SELECT [Dnom], [CodiceSW] FROM Raggere WHERE SerieVentola = '" & Ventola & "' AND Npale = '" & Npale & "' AND Motore = '" & Motore & "'", cn)

                Dim myreader As OleDbDataReader

                myreader = cmd.ExecuteReader
                myreader.Read()


                SW_Code = myreader("CodiceSW")
                SW_Raggera = myreader("Dnom")

                cn.Close()

            End Using




        Catch ex As Exception



        End Try




    End Sub






End Class
