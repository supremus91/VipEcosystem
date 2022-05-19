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

Module Aggiungi_sviluppo

    Public Sub Aggiungi_sviluppo1()

        Using myconnection As New OleDbConnection(constring)
            myconnection.Open()
            Dim sqlQry As String = "INSERT INTO [Sviluppo] ([cbx_swSV], [cbx_ambitoSV], [tbx_DescrizioneSV], [cbx_statoSV], [tbx_CreazioneSV], [tbx_chiusuraSV], [tbx_giorniSV], [tbx_valutazioneSV], [tbx_nomeSV], [cbx_urgenzaSV], [Diff_SV], [tbx_notaSV]) VALUES (@cbx_swSV, @cbx_ambitoSV, @tbx_DescrizioneSV, @cbx_statoSV, @tbx_CreazioneSV, @tbx_chiusuraSV, @tbx_giorniSV, @tbx_valutazioneSV, @tbx_nomeSV, @cbx_urgenzaSV, @Diff_SV, @tbx_notaSV)"

            Using cmd As New OleDbCommand(sqlQry, myconnection)

                cmd.Parameters.AddWithValue("@cbx_swSV", swSV)
                cmd.Parameters.AddWithValue("@cbx_ambitoSV", ambitoSV)
                cmd.Parameters.AddWithValue("@tbx_DescrizioneSV", DescrizioneSV)
                cmd.Parameters.AddWithValue("@cbx_statoSV", "In sviluppo")
                cmd.Parameters.AddWithValue("@tbx_CreazioneSV", Today.ToShortDateString)
                cmd.Parameters.AddWithValue("@tbx_chiusuraSV", "")
                cmd.Parameters.AddWithValue("@tbx_giorniSV", "0")
                cmd.Parameters.AddWithValue("@tbx_valutazioneSV", "0")
                cmd.Parameters.AddWithValue("@tbx_nomeSV", nome_richiedente)
                cmd.Parameters.AddWithValue("@cbx_urgenzaSV", urgenzaSV)
                cmd.Parameters.AddWithValue("@Diff_SV", difficolta_SV)
                cmd.Parameters.AddWithValue("@tbx_notaSV", NOTE_SV)
                cmd.Parameters.AddWithValue("@tbx_notaSV", Versione_ref)

                cmd.ExecuteNonQuery()

            End Using


        End Using


    End Sub


End Module
