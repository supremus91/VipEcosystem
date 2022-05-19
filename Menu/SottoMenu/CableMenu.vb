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


Public Class CableMenu


    Private Sub Guna2CircleButton3_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton3.Click

        Guna2PictureBox1.Image = My.Resources.cb_p3

        Guna2CircleButton3.Image = My.Resources.circle1
        Guna2CircleButton2.Image = My.Resources.circle2
        Guna2CircleButton1.Image = My.Resources.circle2
        Guna2CircleButton4.Image = My.Resources.circle2

        If accessories_load = 1 Then

            Using cn As New OleDb.OleDbConnection(constring)


                cn.Open()
                Dim cmd As New OleDb.OleDbCommand
                cmd.CommandText = "UPDATE Progetto SET " & "Pos_install = 'up'" & " WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & " AND cbx_Revisione = '" & num_rev_generale & "'" & ";"

                cmd.CommandType = CommandType.Text
                cmd.Connection = cn

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                cn.Close()


            End Using

        End If

    End Sub

    Private Sub Guna2CircleButton2_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton2.Click

        Guna2PictureBox1.Image = My.Resources.cb_p2

        Guna2CircleButton3.Image = My.Resources.circle2
        Guna2CircleButton2.Image = My.Resources.circle1
        Guna2CircleButton1.Image = My.Resources.circle2
        Guna2CircleButton4.Image = My.Resources.circle2

        If accessories_load = 1 Then

            Using cn As New OleDb.OleDbConnection(constring)


                cn.Open()
                Dim cmd As New OleDb.OleDbCommand
                cmd.CommandText = "UPDATE Progetto SET " & "Pos_install = 'right'" & " WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & " AND cbx_Revisione = '" & num_rev_generale & "'" & ";"

                cmd.CommandType = CommandType.Text
                cmd.Connection = cn

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                cn.Close()


            End Using

        End If
    End Sub

    Private Sub Guna2CircleButton1_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton1.Click

        Guna2PictureBox1.Image = My.Resources.cb_p1

        Guna2CircleButton3.Image = My.Resources.circle2
        Guna2CircleButton2.Image = My.Resources.circle2
        Guna2CircleButton1.Image = My.Resources.circle1
        Guna2CircleButton4.Image = My.Resources.circle2

        If accessories_load = 1 Then

            Using cn As New OleDb.OleDbConnection(constring)


                cn.Open()
                Dim cmd As New OleDb.OleDbCommand
                cmd.CommandText = "UPDATE Progetto SET " & "Pos_install = 'down'" & " WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & " AND cbx_Revisione = '" & num_rev_generale & "'" & ";"

                cmd.CommandType = CommandType.Text
                cmd.Connection = cn

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                cn.Close()


            End Using


        End If
    End Sub

    Private Sub Guna2CircleButton4_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton4.Click

        Guna2PictureBox1.Image = My.Resources.cb_p4

        Guna2CircleButton3.Image = My.Resources.circle2
        Guna2CircleButton2.Image = My.Resources.circle2
        Guna2CircleButton1.Image = My.Resources.circle2
        Guna2CircleButton4.Image = My.Resources.circle1

        If accessories_load = 1 Then

            Using cn As New OleDb.OleDbConnection(constring)


                cn.Open()
                Dim cmd As New OleDb.OleDbCommand
                cmd.CommandText = "UPDATE Progetto SET " & "Pos_install = 'left'" & " WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & " AND cbx_Revisione = '" & num_rev_generale & "'" & ";"

                cmd.CommandType = CommandType.Text
                cmd.Connection = cn

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                cn.Close()



            End Using

        End If

    End Sub




    Private Sub CableMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lettura_riga1(num_rev_generale)


        accessories_load = 0

        Dim posizione_install As String

        For j = 0 To Numero_colonneDB - 1
            If Nome_colonne(j + 1) = "Pos_install" Then
                posizione_install = Valore_CellaRiga(j)

                Select Case posizione_install

                    Case "up"
                        Guna2CircleButton3.PerformClick()
                    Case "down"
                        Guna2CircleButton1.PerformClick()
                    Case "left"
                        Guna2CircleButton4.PerformClick()
                    Case "right"
                        Guna2CircleButton2.PerformClick()

                End Select



            End If
        Next

        accessories_load = 1



    End Sub


End Class