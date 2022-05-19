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

Public Class AccessoriesMenu



    Private Sub AccessoriesMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Lettura_riga1(num_rev_generale)


        accessories_load = 0
        Dim k As Integer
        Dim j As Integer
        Dim gb As GroupBox
        Dim cbx As ComboBox
        Dim chb As CheckBox


        For k = 0 To 11

            For j = 0 To 4

                gb = GroupBox22.Controls("Check" & k + 1)
                cbx = gb.Controls("ComboBox" & k + 1 & "_" & j)
                chb = gb.Controls("Pic" & k + 1)

                If j = 0 Then

                    For i = 0 To Numero_colonneDB - 1
                        If Nome_colonne(i + 1) = chb.Name Then
                            chb.Checked = Valore_CellaRiga(i)
                        End If
                    Next


                Else

                    For i = 0 To Numero_colonneDB - 1

                        If Nome_colonne(i + 1) = cbx.Name Then
                            Try
                                cbx.Text = Valore_CellaRiga(i)
                            Catch ex As Exception

                            End Try
                        End If

                    Next


                End If


            Next


        Next

        accessories_load = 1



        'tutte le combobox dove le goupbox con checkbox sono su false vengono disabilitate
        For i = 0 To 11

            gb = GroupBox22.Controls("Check" & i + 1)
            chb = gb.Controls("Pic" & i + 1)

            If chb.Checked = False Then

                For j = 0 To 3
                    cbx = gb.Controls("ComboBox" & i + 1 & "_" & j + 1)
                    cbx.SelectedIndex = -1
                    cbx.Enabled = False
                Next

            End If

        Next


    End Sub




    Private Sub ComboBox1_1_textchanged(sender As Object, e As EventArgs) Handles ComboBox1_1.TextChanged, ComboBox1_2.TextChanged, ComboBox1_3.TextChanged, ComboBox1_4.TextChanged,
            ComboBox2_1.TextChanged, ComboBox2_2.TextChanged, ComboBox2_3.TextChanged, ComboBox2_4.TextChanged,
            ComboBox3_1.TextChanged, ComboBox3_2.TextChanged, ComboBox3_3.TextChanged, ComboBox3_4.TextChanged,
            ComboBox4_1.TextChanged, ComboBox4_2.TextChanged, ComboBox4_3.TextChanged, ComboBox4_4.TextChanged,
            ComboBox5_1.TextChanged, ComboBox5_2.TextChanged, ComboBox5_3.TextChanged, ComboBox5_4.TextChanged,
            ComboBox6_1.TextChanged, ComboBox6_2.TextChanged, ComboBox6_3.TextChanged, ComboBox6_4.TextChanged,
            ComboBox7_1.TextChanged, ComboBox7_2.TextChanged, ComboBox7_3.TextChanged, ComboBox7_4.TextChanged,
            ComboBox8_1.TextChanged, ComboBox8_2.TextChanged, ComboBox8_3.TextChanged, ComboBox8_4.TextChanged,
            ComboBox9_1.TextChanged, ComboBox9_2.TextChanged, ComboBox9_3.TextChanged, ComboBox9_4.TextChanged,
            ComboBox10_1.TextChanged, ComboBox10_2.TextChanged, ComboBox10_3.TextChanged, ComboBox10_4.TextChanged,
            ComboBox11_1.TextChanged, ComboBox11_2.TextChanged, ComboBox11_3.TextChanged, ComboBox11_4.TextChanged,
            ComboBox12_1.TextChanged, ComboBox12_2.TextChanged, ComboBox12_3.TextChanged, ComboBox12_4.TextChanged




        If accessories_load = 1 Then


            Dim b As ComboBox = DirectCast(sender, ComboBox)

            Using cn As New OleDb.OleDbConnection(constring)

                Dim cella_val As String = b.Name & " = '" & b.Text & "'"

                cn.Open()
                Dim cmd As New OleDb.OleDbCommand
                cmd.CommandText = "UPDATE Progetto SET " & cella_val & " WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & " AND cbx_Revisione = '" & num_rev_generale & "'" & ";"

                cmd.CommandType = CommandType.Text
                cmd.Connection = cn

                cmd.ExecuteNonQuery()
                cmd.Dispose()

                cn.Close()

            End Using



        End If


    End Sub

    Private Sub Pic1_CheckedChanged(sender As Object, e As EventArgs) Handles Pic1.CheckedChanged, Pic2.CheckedChanged, Pic3.CheckedChanged, Pic4.CheckedChanged, Pic5.CheckedChanged, Pic6.CheckedChanged,
         Pic7.CheckedChanged, Pic8.CheckedChanged, Pic9.CheckedChanged, Pic10.CheckedChanged, Pic11.CheckedChanged, Pic12.CheckedChanged



        If accessories_load = 1 Then

            'Identifico il nome della checkbox che ho clickato
            Dim b As CheckBox = DirectCast(sender, CheckBox)


            Dim num_acc As Integer = b.Name.Substring(3, b.Name.Length - 3)
            Dim gb As GroupBox
            Dim cbx As ComboBox

            'per il save del file
            If b.Name = "Pic6" Then
                If b.Checked = True Then
                    check_ReteLatoMotore = 1
                Else
                    check_ReteLatoMotore = 0
                End If

            End If

            If b.Name = "Pic7" Then
                If b.Checked = True Then
                    check_ReteLatoMotore = 1
                Else
                    check_ReteLatoMotore = 0
                End If
            End If

            If b.Checked = False Then

                    'Se metto la check a False allora deseleziono tutte le combobox corrispondeti e le disabilito
                    For k = 0 To 3

                        gb = GroupBox22.Controls("Check" & num_acc)
                        cbx = gb.Controls("ComboBox" & num_acc & "_" & k + 1)
                        cbx.SelectedIndex = -1
                        cbx.Text = ""
                        cbx.Enabled = False

                    Next


                Else
                    'Sblocco le combobx se la checkbox e' su true
                    For k = 0 To 3

                        gb = GroupBox22.Controls("Check" & num_acc)
                        cbx = gb.Controls("ComboBox" & num_acc & "_" & k + 1)
                        cbx.Enabled = True

                    Next

                End If




                'Vado a cambiare il valore della checkbox da database
                Using cn As New OleDb.OleDbConnection(constring)


                    Dim cella_val As String
                    Dim check_state As Integer


                    If b.Checked = True Then
                        check_state = 1
                    Else
                        check_state = 0
                    End If


                    cella_val = b.Name & " = '" & check_state & "'"

                    cn.Open()
                    Dim cmd As New OleDb.OleDbCommand
                    cmd.CommandText = "UPDATE Progetto SET " & cella_val & " WHERE tbx_Progetto = '" & prog_rev(posizione_progetto, 0) & "'" & " AND cbx_Revisione = '" & num_rev_generale & "'" & ";"

                    cmd.CommandType = CommandType.Text
                    cmd.Connection = cn

                    cmd.ExecuteNonQuery()
                    cmd.Dispose()

                    cn.Close()

                End Using



            End If


    End Sub


End Class