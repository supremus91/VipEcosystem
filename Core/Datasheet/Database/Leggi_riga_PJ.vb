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


Module Leggi_riga_PJ


    Public Sub Leggi_riga_PJ1(chiave, ambiente)

        'Valori calcolati dal nome effettivo all'ID del database
        Traduttore_DB_amb1(ambiente)
        Traduttore_DB_config1(chiave)


        Try


            Using cn As New OleDb.OleDbConnection(constring)
                'provider to be used when working with access database
                cn.Open()

                Dim databasePJ_name As String

                If DB_pasticci_on = 0 Then
                    databasePJ_name = "PJ_DataBase"
                Else
                    databasePJ_name = "PJ_DataBasePasticci"
                End If

                Dim cmd As New OleDb.OleDbCommand("SELECT * FROM " & databasePJ_name & " WHERE cbx_PJ_configurazioni = " & ID_conf & " AND cbx_PJ_ambiente = " & ID_amb & " AND cbx_tipo_motore_conf = " & tipo_motore_PJ, cn)

                Dim myreader As OleDbDataReader

                myreader = cmd.ExecuteReader
                myreader.Read()

                errorePJ = 0

                For i = 0 To Numero_colonneDS - 1

                    Try


                        Valore_CellaRigaDS(i) = myreader(Nome_colonneDS(i + 1))

                        If Nome_colonneDS(i + 1)(0) = "c" And Nome_colonneDS(i + 1)(1) = "b" And Nome_colonneDS(i + 1)(2) = "x" Then 'combobox seguo una traduzione
                            Traduttore_PJ_DataBase1(Valore_CellaRigaDS(i), Nome_colonneDS(i + 1))
                            Valore_CellaRigaDS_star(i) = valore_DS_star

                        Else
                            Valore_CellaRigaDS_star(i) = Valore_CellaRigaDS(i)
                        End If



                    Catch ex As Exception
                        Valore_CellaRigaDS(i) = ""
                        errorePJ = errorePJ + 1
                    End Try

                Next


                cn.Close()

            End Using



        Catch ex As Exception

        End Try













    End Sub






End Module
