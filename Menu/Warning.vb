Public Class Warning

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Yes_No_Warning = 1
        Me.Close()
    End Sub


    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Yes_No_Warning = 0
        Me.Close()
    End Sub


End Class