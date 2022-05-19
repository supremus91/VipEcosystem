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


Public Class CertificateMenu

    Private Sub Guna2CircleQ1_Click(sender As Object, e As EventArgs) Handles Guna2CircleQ1.Click
        Dim webAddress As String = "https://static.weg.net/medias/downloadcenter/h68/h3c/WEG-global-meps-guide-for-low-voltage-motors-50060049-brochure-english-web.pdf"
        Process.Start(webAddress)
    End Sub

    Private Sub Guna2CircleQ1_MouseHover(sender As Object, e As EventArgs) Handles Guna2CircleQ1.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub Guna2CircleQ1_MouseLeave(sender As Object, e As EventArgs) Handles Guna2CircleQ1.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub





    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged, CheckedListBox3.SelectedIndexChanged,
            CheckedListBox2.SelectedIndexChanged, CheckedListBox4.SelectedIndexChanged



        'aggiornamento di variabili di saving
        If CheckedListBox1.GetItemCheckState(6) = 1 Then
            check_ULCSA = 1
            check_NEMA = 1
        Else
            check_ULCSA = 0
            check_NEMA = 0
        End If


        If CheckedListBox3.GetItemCheckState(0) = 1 Then
            check_CUTR = 1
        End If

        If CheckedListBox3.GetItemCheckState(1) = 1 Then
            check_IECEX = 1
        End If

        If CheckedListBox4.GetItemCheckState(1) = 1 Then
            check_EAC = 1
        End If




        'solo se non sono in caso di loading (il cambiamento della check deve essere dettata dall'utente)
        If accessories_load = 1 Then

            Dim b As CheckedListBox = DirectCast(sender, CheckedListBox)




            Dim stringa_aggiorna As String = ""
            Dim ListBox_sel As Integer

            If b.Name = "CheckedListBox1" Then
                ListBox_sel = 1
            ElseIf b.Name = "CheckedListBox2" Then
                ListBox_sel = 2
            ElseIf b.Name = "CheckedListBox3" Then
                ListBox_sel = 3
            ElseIf b.Name = "CheckedListBox4" Then
                ListBox_sel = 4
            End If



            'Compongo la stringa per aggiornare in un colpo solo tutte le check dentro la lista
            For i = 0 To b.Items.Count - 1


                    If i < b.Items.Count - 1 Then
                    stringa_aggiorna = stringa_aggiorna & "Cert_check" & ListBox_sel & "_" & i + 1 & " = '" & b.GetItemCheckState(i) & "',"
                Else
                    stringa_aggiorna = stringa_aggiorna & "Cert_check" & ListBox_sel & "_" & i + 1 & " = '" & b.GetItemCheckState(i) & "'"
                End If


                Next




                Using cn As New OleDb.OleDbConnection(constring)


                    cn.Open()
                    Dim cmd As New OleDb.OleDbCommand
                    cmd.CommandText = "UPDATE Progetto SET " & stringa_aggiorna & " WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & " AND cbx_Revisione = '" & num_rev_generale & "'" & ";"

                    cmd.CommandType = CommandType.Text
                    cmd.Connection = cn

                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                    cn.Close()


                End Using

            End If

    End Sub



    Private Sub CertificateMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lettura_riga1(num_rev_generale)

        Dim chb As CheckedListBox
        Dim gb As Guna.UI2.WinForms.Guna2GroupBox

        accessories_load = 0

        For t = 0 To 3


            gb = Controls("Guna2GroupBox" & t + 1)
            chb = gb.Controls("CheckedListBox" & t + 1)


            For i = 0 To chb.Items.Count - 1

                For j = 0 To Numero_colonneDB - 1
                    If Nome_colonne(j + 1) = "Cert_check" & t + 1 & "_" & i + 1 Then
                        chb.SetItemChecked(i, Valore_CellaRiga(j))
                    End If
                Next


            Next


        Next


        accessories_load = 1

    End Sub


End Class