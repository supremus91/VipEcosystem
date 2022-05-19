Public Class Warning_valutazione



    Private Sub Warning_valutazione_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2RatingStar1.Value = 2.5
    End Sub

    Private Sub Guna2RatingStar1_ValueChanged(sender As Object, e As EventArgs) Handles Guna2RatingStar1.ValueChanged

        rating_user = Guna2RatingStar1.Value
        Guna2Button1.Visible = True

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click

        Yes_No_Warning = 1
        Me.Close()

    End Sub


End Class