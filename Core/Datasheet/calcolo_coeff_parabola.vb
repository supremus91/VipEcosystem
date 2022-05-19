Module calcolo_coeff_parabola



    Public Sub calcolo_coeff_parabola1(x_col, y_col)


        den_parabola = (x_col(0) - x_col(1)) * (x_col(0) - x_col(2)) * (x_col(1) - x_col(2))
        A_parabola = (x_col(2) * (y_col(1) - y_col(0)) + x_col(1) * (y_col(0) - y_col(2)) + x_col(0) * (y_col(2) - y_col(1))) / den_parabola
        B_parabola = (x_col(0) ^ 2 * (y_col(1) - y_col(2)) + x_col(2) ^ 2 * (y_col(0) - y_col(1)) + x_col(1) ^ 2 * (y_col(2) - y_col(0))) / den_parabola
        C_parabola = (x_col(1) ^ 2 * (x_col(2) * y_col(0) - x_col(0) * y_col(2)) + x_col(1) * (x_col(0) ^ 2 * y_col(2) - x_col(2) ^ 2 * y_col(0)) + x_col(0) * x_col(2) * (x_col(2) - x_col(0)) * y_col(1)) / den_parabola



    End Sub










End Module
