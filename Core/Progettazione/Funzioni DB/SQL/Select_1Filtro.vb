Imports System.Data.SqlClient


Module Select_1Filtro


    Public Sub Select_1Filtro1(Colonna_Selezione, Tabella, Colonna_Filtro1, Filtro)

        Dim cmd As New SqlCommand("SELECT " & Colonna_Selezione & " FROM " & Tabella & " WHERE " & Colonna_Filtro1 & " = '" & Filtro & "'", connSQL)

        Dim da As New SqlDataAdapter(cmd)
        da.Fill(ds, "list") '// list can be any name u want



        'creo una tabella contenente la chiamata al database
        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1

            col.Add(ds.Tables(0).Rows(i)(Colonna_Selezione).ToString())  '//columnname same As In query

        Next




    End Sub


End Module
