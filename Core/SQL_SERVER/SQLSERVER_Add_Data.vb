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


Module SQLSERVER_Add_Data



    Public Sub SQLSERVER_Add_Data1(Directory, Descrizione, Configurazione, Flusso, Diametro, Poli, Fasi, Tensione, Freq, coeff1, coeff2, coeff3, Tmin, Tmax, Qmin, Qmax, pow, Q, P, RPM, I, Numero_vel, Cat)

        Dim Datasheet_pdf() As Byte = {}
        Try
            'Salvo il Datasheet dentro il record
            Dim FStream_Datasheet As New FileStream(Directory & "\" & Descrizione & ".pdf", FileMode.Open)
            Dim BReader_Datasheet As New BinaryReader(FStream_Datasheet)
            Datasheet_pdf = BReader_Datasheet.ReadBytes(FStream_Datasheet.Length)
            FStream_Datasheet.Close()
            BReader_Datasheet.Close()
        Catch ex As Exception

        End Try

        Dim Disegno_pdf() As Byte = {}
        Try
            'Salvo il Disegno dentro il record
            Dim FStream_Disegno As New FileStream(Directory & "\" & "Drawing_" & Descrizione & ".pdf", FileMode.Open)
            Dim BReader_Disegno As New BinaryReader(FStream_Disegno)
            Disegno_pdf = BReader_Disegno.ReadBytes(FStream_Disegno.Length)
            FStream_Disegno.Close()
            BReader_Disegno.Close()
        Catch ex As Exception

        End Try

        'E' necessario per il filtro sul sito
        Select Case Poli
            Case "S"
                Poli = 6
            Case "A"
                Poli = 8
            Case "B"
                Poli = 6
            Case "V"
                Poli = 4
        End Select


        Dim cmd As New SqlCommand("insert into MCSS.dbo.Archivio(Descrizione, Configurazione, Flusso, Diametro, Poli, Fasi, Tensione, Freq, coeff1, coeff2,coeff3,Tmin,Tmax,Qmin,Qmax, Datasheet, Disegno,Pow,Q,P,RPM,I,Numero_vel,Motore,Ventola,Catalogo) values(@descrizione, @configurazione,@flusso,@diametro,@poli,@fasi,@tensione,@freq,@coeff1,@coeff2,@coeff3,@Tmin,@Tmax,@Qmin,@Qmax,@datasheet,@disegno,@Pownom,@Qnom,@Pnom,@RPMnom,@Inom,@Nvel,@Mot,@Vent,@Cat)", connSQL_archivio)


        cmd.Parameters.AddWithValue("@descrizione", Descrizione)
        cmd.Parameters.AddWithValue("@configurazione", Configurazione)
        cmd.Parameters.AddWithValue("@flusso", Flusso)
        cmd.Parameters.AddWithValue("@diametro", Diametro)
        cmd.Parameters.AddWithValue("@poli", Poli)
        cmd.Parameters.AddWithValue("@fasi", Fasi)

        If Tensione = "VFD" Then
            cmd.Parameters.AddWithValue("@tensione", 0)
            cmd.Parameters.AddWithValue("@freq", 0)
        Else
            cmd.Parameters.AddWithValue("@tensione", Tensione)
            cmd.Parameters.AddWithValue("@freq", Freq)
        End If

        cmd.Parameters.AddWithValue("@coeff1", coeff1)
        cmd.Parameters.AddWithValue("@coeff2", coeff2)
        cmd.Parameters.AddWithValue("@coeff3", coeff3)
        cmd.Parameters.AddWithValue("@Tmin", Tmin)
        Tmax_correction1(Tmax)
        cmd.Parameters.AddWithValue("@Tmax", Tmax_starA)
        cmd.Parameters.AddWithValue("@Qmin", Qmin)
        cmd.Parameters.AddWithValue("@Qmax", Qmax)
        cmd.Parameters.AddWithValue("@datasheet", Datasheet_pdf)
        cmd.Parameters.AddWithValue("@disegno", Disegno_pdf)
        cmd.Parameters.AddWithValue("@Pownom", pow)
        cmd.Parameters.AddWithValue("@Qnom", Q)
        cmd.Parameters.AddWithValue("@Pnom", P)
        cmd.Parameters.AddWithValue("@RPMnom", RPM)
        cmd.Parameters.AddWithValue("@Inom", I)
        cmd.Parameters.AddWithValue("@Nvel", Numero_vel)
        cmd.Parameters.AddWithValue("@Mot", Descrizione(0))
        cmd.Parameters.AddWithValue("@Vent", Descrizione(1))
        cmd.Parameters.AddWithValue("@Cat", Cat)


        connSQL_archivio.Open()
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        connSQL_archivio.Close()


    End Sub








End Module
