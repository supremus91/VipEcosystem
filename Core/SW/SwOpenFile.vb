'---------------------------------------------------------------------------
' Preconditions: 
' 1. Verify that the specified document to open exists.
' 2. Open the Immediate window.
'
' Postconditions:
' 1. Opens the specified document.
' 2. Sets the SOLIDWORKS working directory to the document directory.
' 3. Examine the Immediate window.
'---------------------------------------------------------------------------

Imports SolidWorks.Interop.sldworks

Imports SolidWorks.Interop.swconst

'Imports Microsoft.Office.Interop.Excel


'Imports System.Runtime.InteropServices

'Imports System

'Imports System.Diagnostics


Public Class SwOpenFile

    Dim doc As ModelDoc2


    Dim fileerror As Integer

    Dim filewarning As Integer

    Public swApp As SldWorks = CreateObject("SldWorks.Application")

    Public Sub Main(Motore As String, Ventola As String, Diametro As String, Calettamento As String, Configurazione As String, TipoPala As String, TipoVentola As String,
                    NumeroPale As String, Flusso As String, ByRef pan As Guna.UI2.WinForms.Guna2Panel, ByRef load As Guna.UI2.WinForms.Guna2Panel, ByRef lb As Label)



        Dim percentuale As Integer = 0

        Lettura_RefExcel1()

        Try

            Dim diam_SW_star As String = ""

            If Diam_SW < 1000 Then
                diam_SW_star = "0" & Diam_SW(0) & Diam_SW(1)
            Else
                diam_SW_star = Diam_SW(0) & Diam_SW(1) & Diam_SW(2)
            End If

            swApp.Visible = False

            'Get the current working directory before opening the document

            Debug.Print("Current working directory is " & swApp.GetCurrentWorkingDirectory)

            Dim CodiceVentilatore As String = Motore & Ventola & " _._._-_._-_._-" & Configurazione & "-_._._" & NumeroPale & "_-" & Flusso
            Dim CodiceVentilatore1 As String = Motore & Ventola & " _._._-_._-_._-[" & Configurazione & "]-_._._" & NumeroPale & "_-" & Flusso
            Dim CodiceVentilatore2 As String = Motore & Ventola & " " & diam_SW_star & "-" & Calettamento & "-" & "_._" & "-" & Configurazione & "-" & "_._" & TipoVentola & NumeroPale & "._-" & Flusso

            percentuale = percentuale + 10
            lb.Text = percentuale & "% - Cerco nell'archivio " & CodiceVentilatore
            Application.DoEvents()

            'Apertura della directory contenente l'assemblato
            doc = swApp.OpenDoc6(directory_SW_assemblati & Motore & Ventola & "\MODELLI AUTOMATICI\" & Configurazione & "\" & CodiceVentilatore & ".SLDASM", swDocumentTypes_e.swDocASSEMBLY, swOpenDocOptions_e.swOpenDocOptions_Silent, "", fileerror, filewarning)

            'Opening a document with SldWorks::OpenDoc6 does not set the working directory

            Debug.Print("Current working directory is still " & swApp.GetCurrentWorkingDirectory)


            'Set the working directory to the document directory
            swApp.SetCurrentWorkingDirectory(Left(doc.GetPathName, InStrRev(doc.GetPathName, "\")))
            Debug.Print("Current working directory is now " & swApp.GetCurrentWorkingDirectory)


            percentuale = percentuale + 20
            lb.Text = percentuale & "% - Montaggio motore in " & CodiceVentilatore
            Application.DoEvents()

            'Dim swDesTable As DesignTable
            'Dim nTotRow As Integer
            'Dim nTotCol As Integer
            'Dim sRowStr As String
            'Dim i As Integer
            'Dim j As Integer
            'Dim bRet As Boolean

            'swDesTable = doc.GetDesignTable
            'bRet = swDesTable.Attach

            'nTotRow = swDesTable.GetTotalRowCount
            'nTotCol = swDesTable.GetTotalColumnCount
            'Debug.Print("File = " & doc.GetPathName)
            'Debug.Print("  Title        = " & swDesTable.GetTitle)
            'Debug.Print("  Row          = " & swDesTable.GetRowCount)
            'Debug.Print("  Col          = " & swDesTable.GetColumnCount)
            'Debug.Print("  TotRow       = " & nTotRow)
            'Debug.Print("  TotCol       = " & nTotCol)
            'Debug.Print("  VisRow       = " & swDesTable.GetVisibleRowCount)
            'Debug.Print("  VisCol       = " & swDesTable.GetVisibleColumnCount)
            'Debug.Print("")

            'For i = 0 To nTotRow
            '    sRowStr = "  |"
            '    For j = 0 To nTotCol
            '        sRowStr = sRowStr + swDesTable.GetEntryText(i, j) + "|"
            '    Next j
            '    Debug.Print(sRowStr)
            'Next i
            'swDesTable.Detach()

            'oggetto che identifica la dimensione della raggera
            Dim LeggiRaggera As LeggiSwDbRaggera = New LeggiSwDbRaggera()
            LeggiRaggera.Main(Ventola, NumeroPale, Motore, TipoPala)


            Dim status As Boolean
            Dim docDocExt As ModelDocExtension





            docDocExt = doc.Extension
            'status = docDocExt.SelectByID2("063 M@RR _._._ -_._-_._-[M]-_._._7_-A.SLDASM", "CONFIGURATIONS", 0, 0, 0, False, 0, Nothing, 0)
            status = docDocExt.SelectByID2(diam_SW_star & " M@" & CodiceVentilatore1 & ".SLDASM", "CONFIGURATIONS", 0, 0, 0, False, 0, Nothing, 0)
            doc.ShowConfiguration2(diam_SW_star & " " & Configurazione)


            For i = 0 To 9
                Try
                    doc.Extension.SelectByID2("PALE PER " & TipoPala & " " & Ventola & NumeroPale & " D" & SW_Raggera & "-" & i & "@" & CodiceVentilatore1, "COMPONENT", 0, 0, 0, False, 0, Nothing, 0)
                    doc.EditUnsuppress2()

                    Dim swComp As Component2
                    swComp = doc.SelectionManager.GetSelectedObjectsComponent4(1, -1)
                    swComp.ReferencedConfiguration = diam_SW_star & " C" & Calettamento
                    status = doc.EditRebuild3()
                    doc.ClearSelection2(True)
                Catch ex As Exception

                End Try


                percentuale = percentuale + 5
                lb.Text = percentuale & "% - Montaggio ventola in " & CodiceVentilatore
                Application.DoEvents()

            Next


            '' Select a sketch and hide it
            'status = docDocExt.SelectByID2("PALE PER R7 D440-2@VR _._._-_._-_._-Q-_._._7_-A", "COMPONENT", 0, 0, 0, False, 0, Nothing, 0)
            ''doc.FeatureManager.HideBodies()
            'doc.EditSuppress2()

            'Dim swComp As Component2
            'swComp = doc.SelectionManager.GetSelectedObjectsComponent4(1, -1)
            'swComp.ReferencedConfiguration = diam_SW_star & " C" & Calettamento
            'status = doc.EditRebuild3()
            'doc.ClearSelection2(True)


            Dim chx_count As Integer = 0
            For Each item As Control In pan.Controls

                percentuale = percentuale + 3
                lb.Text = percentuale & "% - Montaggio accessori in " & CodiceVentilatore
                Application.DoEvents()

                If item.GetType Is GetType(Guna.UI2.WinForms.Guna2CheckBox) Then



                    Dim CheckX As Guna.UI2.WinForms.Guna2CheckBox
                    CheckX = pan.Controls("Check" & (chx_count + 1))

                    status = docDocExt.SelectByID2(Valore_CellaRiga_SW(chx_count) & "@" & CodiceVentilatore1, "COMPONENT", 0, 0, 0, False, 0, Nothing, 0)

                    If CheckX.Checked = True Then
                        doc.EditUnsuppress2()
                    Else
                        doc.EditSuppress2()
                    End If

                    doc.ClearSelection2(True)

                    chx_count = chx_count + 1
                End If

            Next

            Dim saveFileDialog1 As SaveFileDialog = New SaveFileDialog()
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = True

            saveFileDialog1.FileName = CodiceVentilatore1


            saveFileDialog1.DefaultExt = "STEP"
            saveFileDialog1.AddExtension = True


            percentuale = 99
            lb.Text = percentuale & "% - Salvataggio " & CodiceVentilatore
            Application.DoEvents()


            If saveFileDialog1.ShowDialog() = DialogResult.OK Then

                If saveFileDialog1.FileName IsNot Nothing Then

                    status = doc.SaveAs3(saveFileDialog1.FileName & ".STEP", 0, 2)

                End If

            End If

            'status = doc.SaveAs3("C:\Users\" & nome_macchina & "\" & "Desktop" & "\" & CodiceVentilatore2 & ".STEP", 0, 2)


            'Dim sketchLines As Object
            'Dim swSketchSegment As SketchSegment
            'Dim swSketchMgr As SketchManager
            'swSketchMgr = doc.SketchManager


            '' Sketch a rectangle
            'status = docDocExt.SelectByID2("Front Plane", "PLANE", 0, 0, 0, False, 0, Nothing, 0)
            'doc.ClearSelection2(True)
            'sketchLines = swSketchMgr.CreateCornerRectangle(-0.0684166678777842, 0.0376953152008355, 0, -0.0273535635019471, 0.00483994917499331, 0)
            'doc.ClearSelection2(True)
            'swSketchMgr.InsertSketch(True)

            '' Sketch a circle
            'status = docDocExt.SelectByID2("Front Plane", "PLANE", 0, 0, 0, False, 0, Nothing, 0)
            'doc.ClearSelection2(True)
            'swSketchSegment = swSketchMgr.CreateCircle(0.044426, 0.079347, 0.0#, 0.057359, 0.06229, 0.0#)
            'doc.ClearSelection2(True)
            'swSketchMgr.InsertSketch(True)


            swApp.ExitApp()


            'il software rimne in esecuzione, ad una riapertura sucessiva mi dà errore se non killo l'app
            For Each prog As Process In Process.GetProcesses
                If prog.ProcessName = "SLDWORKS" Then
                    prog.Kill()
                End If
            Next



        Catch ex As Exception

            swApp.ExitApp()


            'il software rimne in esecuzione, ad una riapertura sucessiva mi dà errore se non killo l'app
            For Each prog As Process In Process.GetProcesses
                If prog.ProcessName = "SLDWORKS" Then
                    prog.Kill()
                End If
            Next


            'ATTIVA IL LOADING
            load.Visible = False  '------> ESEGUIRE IN PARALLELO
            load.SendToBack()  '------> ESEGUIRE IN PARALLELO
            lb.Text = "LOADING..."
            lb.Location = New System.Drawing.Point(738, 490)

            Application.DoEvents()


        End Try

        'ATTIVA IL LOADING
        load.Visible = False  '------> ESEGUIRE IN PARALLELO
        load.SendToBack()  '------> ESEGUIRE IN PARALLELO
        lb.Text = "LOADING..."
        lb.Location = New System.Drawing.Point(738, 490)

        Application.DoEvents()



    End Sub



End Class
