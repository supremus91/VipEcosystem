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
Imports System.Globalization
Imports System.Windows.Forms.DataVisualization.Charting
Imports DevExpress.XtraReports.UI
Imports System.Collections
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Drawing
Imports DevExpress.XtraPrinting.Control
Imports DevExpress.XtraPrinting.Localization
Imports DevExpress.XtraPrinting.Native.ExportOptionsControllers
Imports DevExpress.XtraPrinting.Native
Imports Microsoft.VisualBasic

Module Copia_Disegni

    Public Sub Copia_Disegni1()


        'Ciclo attraverso tutti i ventilatori nel database Archivio
        For i As Integer = 0 To numero_DS - 1

            Dim Fan_name As String = DS_lista(i, 1)
            Dim Fan_Directory As String = "H:\Comune\Applicazioni\Archivio Datasheet\" + Fan_name
            Dim Dir_Disegni As String = "H:\Ufficio Tecnico\Disegni\VV - Ventilatori\.PDF"



            'Ciclo tra tutte le cartelle delle configurazioni
            For Each Dir As String In Directory.GetDirectories(Fan_Directory)


                Dim len_Fan_Dir As Integer = Fan_Directory.Length
                Dim Code_Motore_Ventola As String = Fan_name(0) + Fan_name(1)
                Dim Code_Diam As String = Fan_name(4) + Fan_name(5) + Fan_name(6)
                Dim Code_Conf As String = Dir(len_Fan_Dir + 1)
                Dim Code_Flux As String = Dir(len_Fan_Dir + 3)
                Dim Prof_pale As String = Fan_name(19)
                Dim Num_pale As String = Fan_name(20)

                form_Archivio.Label22.Text = "Regenerating " & DS_lista(i, 1) & " configuratio: " & Code_Conf & ". Process at " & Math.Round(i / (numero_DS - 1) * 100, 0) & " %"
                form_Archivio.Label22.Location = New System.Drawing.Point(370, 490)
                Application.DoEvents()

                'Ciclo tra tutti i disegni nella cartella dei disegni
                For Each Dir_All_disegni As String In Directory.GetDirectories(Dir_Disegni)

                    Dim Disegni_fan_dir As String = New DirectoryInfo(Dir_All_disegni).Name

                    'Identifico la cartella della serie di appartenenza
                    If Disegni_fan_dir = "VENTILATORI SERIE " + Code_Motore_Ventola Then

                        'Ciclo tra tutti i disegni della serie specifica
                        For Each File_Conf_disegno As String In Directory.GetFiles(Dir_Disegni + "\" + Disegni_fan_dir)

                            Dim Name_Disegno As String = New FileInfo(File_Conf_disegno).Name

                            Try

                                Name_Disegno = Name_Disegno.Substring(0, Name_Disegno.IndexOf(".PDF"))


                                'La parentesi tonda identifica la versione NON STANDARD
                                If Name_Disegno(Name_Disegno.Length - 1) <> ")" Then


                                    Dim Code_Motore_Ventola_D As String = Name_Disegno(0) + Name_Disegno(1)
                                    Dim Code_Diam_D As String = Name_Disegno(3) + Name_Disegno(4) + Name_Disegno(5)
                                    Dim Code_Conf_D As String = ""

                                    'Ricerco il nome della configurazione in un range in cui si trova la configurazione
                                    For k = 11 To 17

                                        If Name_Disegno(k) <> "." And Name_Disegno(k) <> "-" And Name_Disegno(k) <> "_" Then
                                            Code_Conf_D = Name_Disegno(k)
                                        End If

                                    Next

                                    Dim Num_pale_D As String = Name_Disegno(Name_Disegno.Length - 4)
                                    Dim Prof_pale_D As String = Name_Disegno(Name_Disegno.Length - 5)
                                    Dim Code_Flux_D As String = Name_Disegno(Name_Disegno.Length - 1)

                                    'Se identifico l'ugualianza dei parametri identificatori del ventilatore allora copio il file nella directory
                                    If Code_Motore_Ventola_D = Code_Motore_Ventola And Code_Diam_D = Code_Diam And Code_Conf_D = Code_Conf And Num_pale_D = Num_pale And (Prof_pale_D = Prof_pale Or Prof_pale_D = "_") And Code_Flux_D = Code_Flux Then

                                        'copio il file 1 della prova2
                                        Try
                                            IO.File.Copy(File_Conf_disegno, Dir & "\Drawing_" & Name_Disegno & ".PDF", True) 'copy(dalla cartella, alla cartella con nome del file)
                                        Catch ex As Exception

                                        End Try

                                    End If

                                End If



                            Catch ex As Exception

                            End Try


                        Next

                    End If



                Next


            Next

        Next


    End Sub

End Module
