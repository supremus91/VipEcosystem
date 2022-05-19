Public Class warning_archivio_DS

    Private Sub warning_archivio_DS_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Guna2RadioButton1.Visible = False
        Guna2RadioButton2.Visible = False

    End Sub


    Private Sub Guna2ImageButton1_MouseHover(sender As Object, e As EventArgs) Handles Guna2ImageButton1.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub Guna2ImageButton1_MouseLeave(sender As Object, e As EventArgs) Handles Guna2ImageButton1.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub Guna2ImageButton2_MouseHover(sender As Object, e As EventArgs) Handles Guna2ImageButton2.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub Guna2ImageButton2_MouseLeave(sender As Object, e As EventArgs) Handles Guna2ImageButton2.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub


    Private Sub Guna2ImageButton1_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton1.Click


        Datasheet_New_exc = 0
        Guna2RadioButton1.Visible = True
        Guna2RadioButton2.Visible = True


    End Sub


    Private Sub Guna2ImageButton2_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton2.Click
        Datasheet_New_exc = 1
        Me.Close()
    End Sub



    Private Sub Guna2RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2RadioButton1.CheckedChanged
        S_D_var = 1
        Me.Close()
    End Sub

    Private Sub Guna2RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2RadioButton2.CheckedChanged
        S_D_var = 2
        Me.Close()
    End Sub


End Class