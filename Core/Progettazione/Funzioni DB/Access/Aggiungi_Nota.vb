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




Module Aggiungi_Nota


    Public Sub Aggiungi_Nota1()




        Using myconnection As New OleDbConnection(constring)
            myconnection.Open()
            Dim sqlQry As String = "INSERT INTO [NoteProgetto] ([ProgettoPJ], [RevisionePJ], [DataPJ], [UtentePJ], [NotaPJ]) VALUES (@ProgettoPJ, @RevisionePJ, @DataPJ, @UtentePJ, @NotaPJ)"

            Using cmd As New OleDbCommand(sqlQry, myconnection)

                cmd.Parameters.AddWithValue("@ProgettoPJ", prog_rev(posizione_progetto, 0))
                cmd.Parameters.AddWithValue("@RevisionePJ", N_rev_note)
                cmd.Parameters.AddWithValue("@DataPJ", Date.Today.Day & "/" & Date.Today.Month & "/" & Date.Today.Year)
                cmd.Parameters.AddWithValue("@UtentePJ", nome_macchina)
                cmd.Parameters.AddWithValue("@NotaPJ", testRTF)


                cmd.ExecuteNonQuery()

            End Using


        End Using






    End Sub





End Module
