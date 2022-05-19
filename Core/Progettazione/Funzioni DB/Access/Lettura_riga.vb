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

Module Lettura_riga



    Public Sub Lettura_riga1(Nrevisione)


        Try


            Using cn As New OleDb.OleDbConnection(constring)
                'provider to be used when working with access database
                cn.Open()
                Dim cmd As New OleDb.OleDbCommand("SELECT * FROM Progetto WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & " AND cbx_Revisione = '" & Nrevisione & "'", cn)
                Dim myreader As OleDbDataReader

                myreader = cmd.ExecuteReader
                myreader.Read()



                For i = 0 To Numero_colonneDB - 1

                    Try



                        Valore_CellaRiga(i) = myreader(Nome_colonne(i + 1))


                        'cerco la variabile di connessione al VipDesigner
                        If Nome_colonne(i + 1) = "tbx_ConnState" Then
                            Try
                                lockPJ = Valore_CellaRiga(i)
                            Catch ex As Exception
                                lockPJ = 0
                            End Try
                        End If

                        'cerco la variabile di connessione al VipDesigner
                        If Nome_colonne(i + 1) = "tbx_fast" Then
                            Try
                                fast_PJ = Valore_CellaRiga(i)
                            Catch ex As Exception
                                fast_PJ = 0
                            End Try
                        End If


                    Catch ex As Exception
                        Valore_CellaRiga(i) = ""
                    End Try

                Next


                cn.Close()

            End Using



        Catch ex As Exception

        End Try





    End Sub




End Module
