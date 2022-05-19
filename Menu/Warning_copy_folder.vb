Imports System.Runtime.InteropServices
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


Public Class Warning_copy_folder



    Private Sub Warning_copy_folder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        folder_search()




    End Sub

    Public Sub folder_search()


        Dim folders() As String = IO.Directory.GetDirectories(folders_directoryDS & "\" & NomeFolder_selezionata)
        Dim sommaFolder As Integer

        Guna2ComboBox1.Items.Clear()

        For Each folder As String In folders

            Dim folder_name As String = Path.GetFileName(folder)

            Guna2ComboBox1.Items.Add(folder_name)


        Next


    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Yes_No_Warning = 1
        Me.Close()
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Yes_No_Warning = 0
        Me.Close()
    End Sub




    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox1.SelectedIndexChanged


        Warning_folder_name = Guna2ComboBox1.SelectedItem

        Label2.Visible = True
        Guna2Button1.Visible = True
        Guna2Button2.Visible = True

    End Sub
End Class