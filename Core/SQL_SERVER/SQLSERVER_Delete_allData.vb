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
Imports System.Data.SqlClient
Imports System.Configuration

Module SQLSERVER_Delete_allData




    Public Sub SQLSERVER_Delete_allData1()


        Dim cmd As New SqlCommand
        Try


            connSQL_archivio.Open()
            cmd.Connection = connSQL_archivio
            cmd.CommandText = "Delete From Archivio"

            cmd.ExecuteNonQuery()

        Catch ex As Exception


        Finally

            connSQL_archivio.Close()
        End Try




    End Sub









End Module
