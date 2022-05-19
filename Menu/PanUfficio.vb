Public Class PanUfficio



    Private Sub PanUfficio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        'Lorenzo
        BtnLorenzo.Text = tot_Uff_tec(0)

        sx = tot_Uff_tec(0) / max_Uff_tec * 150

        If sx < 60 Then
            BtnLorenzo.Size = New System.Drawing.Size(60, 60)
        Else
            BtnLorenzo.Size = New System.Drawing.Size(sx, sx)
        End If


        'Stefano
        BtnStefano.Text = tot_Uff_tec(1)

        sx = tot_Uff_tec(1) / max_Uff_tec * 150

        If sx < 60 Then
            BtnStefano.Size = New System.Drawing.Size(60, 60)
        Else
            BtnStefano.Size = New System.Drawing.Size(sx, sx)
        End If

        'Paolo
        BtnPaolo.Text = tot_Uff_tec(2)

        sx = tot_Uff_tec(2) / max_Uff_tec * 150

        If sx < 60 Then
            BtnPaolo.Size = New System.Drawing.Size(60, 60)
        Else
            BtnPaolo.Size = New System.Drawing.Size(sx, sx)
        End If


        'Alberto
        BtnAlberto.Text = tot_Uff_tec(3)

        sx = tot_Uff_tec(3) / max_Uff_tec * 150

        If sx < 60 Then
            BtnAlberto.Size = New System.Drawing.Size(60, 60)
        Else
            BtnAlberto.Size = New System.Drawing.Size(sx, sx)
        End If


        'Fasuto
        BtnFausto.Text = tot_Uff_tec(4)

        sx = tot_Uff_tec(4) / max_Uff_tec * 150

        If sx < 60 Then
            BtnFausto.Size = New System.Drawing.Size(60, 60)
        Else
            BtnFausto.Size = New System.Drawing.Size(sx, sx)
        End If



        BtnVD.Text = tot_projects_VD_Uff_tec


        BtnDF.Text = tot_projects_DF_VD_Uff_tec
        BtnPF.Text = tot_projects_PF_VD_Uff_tec
        BtnPN.Text = tot_projects_PN_VD_Uff_tec


        BtnAtex.Text = tot_projects_ATX_Uff_tec
        BtnSafe.Text = tot_projects_SF_VD_Uff_tec

        BtnInd.Text = tot_projects_IND_Uff_tec
        BtnOff.Text = tot_projects_OFF_Uff_tec
        BtnSea.Text = tot_projects_SEA_Uff_tec



    End Sub



    Private Sub BtnVD_MouseHover(sender As Object, e As EventArgs) Handles BtnVD.MouseHover

        Me.Cursor = Cursors.Hand

        BtnDF.Visible = True
        BtnPF.Visible = True
        BtnPN.Visible = True

        BtnAtex.Visible = True
        BtnSafe.Visible = True

        BtnInd.Visible = True
        BtnOff.Visible = True
        BtnSea.Visible = True

    End Sub



    Private Sub BtnVD_MouseLeave(sender As Object, e As EventArgs) Handles BtnVD.MouseLeave

        Me.Cursor = Cursors.Arrow

        BtnDF.Visible = False
        BtnPF.Visible = False
        BtnPN.Visible = False

        BtnAtex.Visible = False
        BtnSafe.Visible = False

        BtnInd.Visible = False
        BtnOff.Visible = False
        BtnSea.Visible = False

    End Sub



    Private Sub BtnFausto_MouseHover(sender As Object, e As EventArgs) Handles Guna2CircleButton6.MouseHover, Guna2CircleButton3.MouseHover, BtnStefano.MouseHover, BtnPaolo.MouseHover, BtnLorenzo.MouseHover, BtnFausto.MouseHover, BtnAlberto.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub BtnFausto_MouseLeave(sender As Object, e As EventArgs) Handles Guna2CircleButton6.MouseLeave, Guna2CircleButton3.MouseLeave, BtnStefano.MouseLeave, BtnPaolo.MouseLeave, BtnLorenzo.MouseLeave, BtnFausto.MouseLeave, BtnAlberto.MouseLeave
        Me.Cursor = Cursors.Arrow
    End Sub






    Private Sub BtnFausto_MouseHover1(sender As Object, e As EventArgs) Handles BtnFausto.MouseHover
        BtnVD.Text = "FAUSTO"
    End Sub


    Private Sub Btnlorenzo_MouseHover1(sender As Object, e As EventArgs) Handles BtnLorenzo.MouseHover
        BtnVD.Text = "LORENZO"
    End Sub

    Private Sub BtnStefano_MouseHover1(sender As Object, e As EventArgs) Handles BtnStefano.MouseHover
        BtnVD.Text = "STEFANO"
    End Sub

    Private Sub Btnpaolo_MouseHover1(sender As Object, e As EventArgs) Handles BtnPaolo.MouseHover
        BtnVD.Text = "PAOLO"
    End Sub


    Private Sub Btnalberto_MouseHover1(sender As Object, e As EventArgs) Handles BtnAlberto.MouseHover
        BtnVD.Text = "ALBERTO"
    End Sub





    Private Sub BtnFausto_MouseLeave1(sender As Object, e As EventArgs) Handles BtnFausto.MouseLeave
        BtnVD.Text = tot_projects_VD_Uff_tec
    End Sub

    Private Sub Btnlorenzo_MouseLeave1(sender As Object, e As EventArgs) Handles BtnLorenzo.MouseLeave
        BtnVD.Text = tot_projects_VD_Uff_tec
    End Sub

    Private Sub Btnstefano_MouseLeave1(sender As Object, e As EventArgs) Handles BtnStefano.MouseLeave
        BtnVD.Text = tot_projects_VD_Uff_tec
    End Sub

    Private Sub Btnpaolo_MouseLeave1(sender As Object, e As EventArgs) Handles BtnPaolo.MouseLeave
        BtnVD.Text = tot_projects_VD_Uff_tec
    End Sub

    Private Sub Btnalberto_MouseLeave1(sender As Object, e As EventArgs) Handles BtnAlberto.MouseLeave
        BtnVD.Text = tot_projects_VD_Uff_tec
    End Sub



End Class