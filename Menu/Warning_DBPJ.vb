Public Class Warning_DBPJ



    Private Sub Guna2RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2RadioButton2.MouseClick

        Importa_PJ_Database1()

        DB_pasticci_on = 1

        Try
            DataBaseMenu.Close()
        Catch ex As Exception

        End Try

        load_var = 0

        DataBaseMenu.Show()

        Me.Close()

    End Sub

    Private Sub Guna2RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2RadioButton1.MouseClick


        Importa_PJ_Database1()

        DB_pasticci_on = 0

        Me.Close()

    End Sub




    Private Sub Guna2RadioButton1_MouseHover(sender As Object, e As EventArgs) Handles Guna2RadioButton1.MouseHover
        Me.Cursor = Cursors.Hand
        Me.Refresh()
    End Sub

    Private Sub Guna2RadioButton1_MouseLeave(sender As Object, e As EventArgs) Handles Guna2RadioButton1.MouseLeave
        Me.Cursor = Cursors.Arrow
        Me.Refresh()
    End Sub

    Private Sub Guna2RadioButton2_MouseHover(sender As Object, e As EventArgs) Handles Guna2RadioButton2.MouseHover
        Me.Cursor = Cursors.Hand
        Me.Refresh()
    End Sub

    Private Sub Guna2RadioButton2_MouseLeave(sender As Object, e As EventArgs) Handles Guna2RadioButton2.MouseLeave
        Me.Cursor = Cursors.Arrow
        Me.Refresh()
    End Sub



    Private Sub Guna2RadioButton1_Click(sender As Object, e As EventArgs) Handles Guna2RadioButton1.Click
        If DB_pasticci_on = 0 Then
            Guna2RadioButton1.Checked = True
            Guna2RadioButton2.Checked = False
        Else
            Guna2RadioButton1.Checked = False
            Guna2RadioButton2.Checked = True
        End If
    End Sub

    Private Sub Guna2RadioButton2_Click(sender As Object, e As EventArgs) Handles Guna2RadioButton2.Click
        If DB_pasticci_on = 0 Then
            Guna2RadioButton2.Checked = True
            Guna2RadioButton1.Checked = True
        Else
            Guna2RadioButton2.Checked = False
            Guna2RadioButton1.Checked = True
        End If
    End Sub


End Class