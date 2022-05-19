Imports System.Runtime.InteropServices
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports System.Threading
Imports System
Imports System.IO
Imports System.Text
Imports System.Net
Imports System.Security.AccessControl
Imports System.Net.Mail
Imports System.IO.Ports
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Net.Sockets
Imports Microsoft.Office.Interop
Imports System.Globalization
Imports System.Windows.Forms.DataVisualization.Charting
Imports DevExpress.XtraReports.UI
Imports System.Collections
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Drawing
Imports MaterialSkin.Animations
Imports MaterialSkin.Controls
Imports MaterialSkin


Public Class Panel_Note


    Public tmp_form1 As FormParametri

    Private Sub Panel_Note_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        RichTextBox1.Select()

        'Controllo che non ci siano dei caratteri che riescono ad essere portati nei database

        testRTF2 = ""

        Try

            For i = 0 To testRTF.Length - 1

                If testRTF(i) = "ò" Then
                    testRTF2 = testRTF2.Substring(0, i - 1) & "o"
                ElseIf testRTF(i) = "à" Then
                    testRTF2 = testRTF2.Substring(0, i - 1) & "a"
                ElseIf testRTF(i) = "€" Then
                    testRTF2 = testRTF2.Substring(0, i - 1) & "e"
                ElseIf testRTF(i) = "È" Then
                    testRTF2 = testRTF2.Substring(0, i - 1) & "E"
                ElseIf testRTF(i) = "ù" Then
                    testRTF2 = testRTF2.Substring(0, i - 1) & "u"
                ElseIf testRTF(i) = "'" Then

                    If i > 0 Then
                        If testRTF(i - 1) <> "\" Then
                            testRTF2 = testRTF2.Substring(0, testRTF2.Length - 1) & " "
                        End If
                    End If

                Else
                    testRTF2 = testRTF2 & testRTF(i)
                End If

            Next

            testRTF = testRTF2

        Catch ex As Exception

        End Try


        Try
            RichTextBox1.Rtf = testRTF
        Catch ex As Exception
            RichTextBox1.Text = testRTF
        End Try




    End Sub



    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

        Try

            'tmp_form1.Guna2DataGridView1.Rows(tmp_form1.Guna2DataGridView1.CurrentRow.Index).Cells(2).Value = RichTextBox1.Text

            If tmp_form1.Guna2DataGridView1.CurrentRow.Index = tmp_form1.Guna2DataGridView1.Rows.Count - 1 And Enter_call = 0 Then

                Aggiungi_Nota1()

                tmp_form1.Guna2DataGridView1.Rows.Add()
                tmp_form1.Guna2DataGridView1.Rows(tmp_form1.Guna2DataGridView1.Rows.Count - 1).Cells(0).Value = Date.Today.Day & "/" & Date.Today.Month & "/" & Date.Today.Year
                tmp_form1.Guna2DataGridView1.Rows(tmp_form1.Guna2DataGridView1.Rows.Count - 1).Cells(1).Value = nome_macchina

            Else



                Modifica_nota1()

            End If


            tmp_form1.Guna2DataGridView1.Rows.Clear()
            lettura_note1()
            tmp_form1.aggiorna_note()




            FormParametri.Guna2DataGridView1.Select()

        Catch ex As Exception

        End Try

        Enter_call = 0

        Me.Close()



    End Sub



    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged


        testRTF2 = ""

        testRTF = RichTextBox1.Rtf


        Try

            For i = 0 To testRTF.Length - 1

                If testRTF(i) = "ò" Then
                    testRTF2 = testRTF2.Substring(0, i - 1) & "o"
                ElseIf testRTF(i) = "à" Then
                    testRTF2 = testRTF2.Substring(0, i - 1) & "a"
                ElseIf testRTF(i) = "€" Then
                    testRTF2 = testRTF2.Substring(0, i - 1) & "e"
                ElseIf testRTF(i) = "È" Then
                    testRTF2 = testRTF2.Substring(0, i - 1) & "E"
                ElseIf testRTF(i) = "ù" Then
                    testRTF2 = testRTF2.Substring(0, i - 1) & "u"
                ElseIf testRTF(i) = "'" Then

                    If i > 0 Then
                        If testRTF(i - 1) <> "\" Then
                            testRTF2 = testRTF2.Substring(0, testRTF2.Length - 1) & " "
                        End If
                    End If

                Else
                    testRTF2 = testRTF2 & testRTF(i)
                End If

            Next

            testRTF = testRTF2

        Catch ex As Exception

        End Try


    End Sub

    'DRAG FORM'
    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture()
    End Sub
    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As Integer)
    End Sub
    Private Sub panelTitleBar_MouseDown(sender As Object, e As MouseEventArgs) Handles panelTitleBar.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub



    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean


        If keyData = Keys.Escape Then

            Try

                'tmp_form1.Guna2DataGridView1.Rows(tmp_form1.Guna2DataGridView1.CurrentRow.Index).Cells(2).Value = RichTextBox1.Text

                If tmp_form1.Guna2DataGridView1.CurrentRow.Index = tmp_form1.Guna2DataGridView1.Rows.Count - 1 And Enter_call = 0 Then

                    Aggiungi_Nota1()

                    tmp_form1.Guna2DataGridView1.Rows.Add()
                    tmp_form1.Guna2DataGridView1.Rows(tmp_form1.Guna2DataGridView1.Rows.Count - 1).Cells(0).Value = Date.Today.Day & "/" & Date.Today.Month & "/" & Date.Today.Year
                    tmp_form1.Guna2DataGridView1.Rows(tmp_form1.Guna2DataGridView1.Rows.Count - 1).Cells(1).Value = nome_macchina

                Else



                    Modifica_nota1()

                End If


                tmp_form1.Guna2DataGridView1.Rows.Clear()
                lettura_note1()
                tmp_form1.aggiorna_note()




                FormParametri.Guna2DataGridView1.Select()

            Catch ex As Exception

            End Try

            Enter_call = 0

            Me.Close()

        End If


    End Function

End Class