Imports ActUtlTypeLib
Imports Gui_Tset.My
Imports Gui_Tset.RecepieOperation
Imports Microsoft.Office.Interop.Excel
Imports MvCamCtrl.NET
Imports System.Configuration
Imports System.IO
Imports System.Net.IPAddress
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Xml
Imports System.Math
Imports System.Drawing.Drawing2D
Imports Emgu.CV.ML
Imports System.Text
Imports System.IO.Pipes
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Windows.Media.Media3D

Public Class Operations
    Dim SIDE_TYPE(1) As String
    Dim TOP_XPOSITION(5000) As Single
    Dim TOP_YPOSITION(5000) As Single
    Dim BOTTOM_XPOSITION(5000) As Single
    Dim BOTTOM_YPOSITION(5000) As Single
    Dim CNT1 As Integer
    Dim CNT2 As Integer
    Dim TOP_MARK_CODE(5000) As String
    Dim BOTTOM_MARK_CODE(5000) As String
    Dim TOP_CODE_TYPE(5000) As String
    Dim BOTTOM_CODE_TYPE(5000) As String
    Dim TOP_Length As Integer
    Dim BOTTOM_Length As Integer
    Dim TOP_PROGRAM_ID(5000) As String
    Dim TOP_LASER_POWER(5000) As String
    Dim TOP_SCAN_SPEED(5000) As String
    Dim TOP_CODE_NO(5000) As String
    Dim IMAGEPATH1 As String
    Dim BOTTOM_PROGRAM_ID(5000) As String
    Dim BOTTOM_LASER_POWER(5000) As String
    Dim BOTTOM_SCAN_SPEED(5000) As String
    Dim BOTTOM_CODE_NO(5000) As String
    Dim IMAGE_PATH As String
    Dim BOTTOM_SELEC As Boolean
    Dim CODE_BOTTOM(5000) As String
    Dim CODE_NO(5000) As String
    Dim CHECK1 As Int32
    Dim au_start As Integer
    Dim emg As Integer
    Dim stop1 As Integer
    Dim NXT As Integer
    Dim CH As Integer
    Dim bit As Boolean
    Dim score As String
    'Dim MyCamera As CCamera = New CCamera
    'Dim nRet As Int32 = CCamera.MV_OK
    Dim m_bIsException As Boolean
    Dim m_nBufSizeForDriver As UInt32 = 1000 * 1000 * 3
    Dim m_pBufForDriver(m_nBufSizeForDriver) As Byte
    Dim m_stDeviceInfoList As CCamera.MV_CC_DEVICE_INFO_LIST = New CCamera.MV_CC_DEVICE_INFO_LIST
    Dim m_nDeviceIndex As UInt32
    Dim m_bIsGrabbing As Boolean = False
    Dim m_hGrabHandle As System.Threading.Thread
    Dim m_stFrameInfoEx As CCamera.MV_FRAME_OUT_INFO_EX = New CCamera.MV_FRAME_OUT_INFO_EX()
    Dim m_ReadWriteLock As System.Threading.ReaderWriterLock
    Dim start_m As Integer
    'Dim m_nBufSizeForDriver As UInt32 = 1000 * 1000 * 3
    'Dim m_pBufForDriver(m_nBufSizeForDriver) As Byte
    Dim m_nBufSizeForDriver1 As UInt32 = 1000 * 1000 * 3
    Dim m_pBufForDriver1(m_nBufSizeForDriver1) As Byte
    Dim AUTO As Integer
    ''' RECIPE DATA' ''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' RECIPE DATA' <summary>
    ''' RECIPE DATA' ''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    Dim PCB_WIDTH As Single
    Dim MARK_ID As Double
    Dim P_XVALUE(10000) As Single
    Dim P_YVALUE(10000) As Single
    Dim P_SEL(10000) As Integer
    Dim TOP_F_TYPE(10000) As String
    Dim TOP_F_SHAP(10000) As String
    Dim TOP_F_X1(10000) As Integer
    Dim TOP_F_Y1(10000) As Integer
    Dim TOP_F_RX2(10000) As Integer
    Dim TOP_F_RY2(10000) As Integer
    Dim TOP_F_CPX(10000) As Integer
    Dim TOP_F_CPY(10000) As Integer
    Dim TOP_F_XOFF(10000) As Integer
    Dim TOP_F_YOFF(10000) As Integer
    Dim TOP_F_POSX(10000) As Single
    Dim TOP_F_POSY(10000) As Single
    Dim TOP_F_THRE(10000) As Integer
    Dim TOP_F_TOL(10000) As Integer
    Dim TOP_F_BRIG(10000) As Integer
    Dim TOP_F_SCORE(10000) As Integer
    Dim TOP_F_SEL(10000) As String

    Dim BOTTOM_F_TYPE(10000) As String
    Dim BOTTOM_F_SHAP(10000) As String
    Dim BOTTOM_F_X1(10000) As Integer
    Dim BOTTOM_F_Y1(10000) As Integer
    Dim BOTTOM_F_RX2(10000) As Integer
    Dim BOTTOM_F_RY2(10000) As Integer
    Dim BOTTOM_F_CPX(10000) As Integer
    Dim BOTTOM_F_CPY(10000) As Integer
    Dim BOTTOM_F_XOFF(10000) As Integer
    Dim BOTTOM_F_YOFF(10000) As Integer
    Dim BOTTOM_F_POSX(10000) As Single
    Dim BOTTOM_F_POSY(10000) As Single
    Dim BOTTOM_F_THRE(10000) As Integer
    Dim BOTTOM_F_TOL(10000) As Integer
    Dim BOTTOM_F_BRIG(10000) As Integer
    Dim BOTTOM_F_SCORE(10000) As Integer
    Dim BOTTOM_F_SEL(10000) As String





    Dim fid_OFFX1_T(1000) As Integer
    Dim fid_OFFY1_T(1000) As Integer
    Dim fid_OFFX1_C(1000) As Integer
    Dim fid_OFFY1_C(1000) As Integer
    Dim FID_X_MA(2) As Integer
    Dim FID_Y_MA(2) As Integer
    Dim FID_X_OLD(2) As Integer
    Dim FID_Y_OLD(2) As Integer

    Dim offsetx As Single
    Dim offsety As Single
    Dim fid_X As Integer
    Dim fid_Y As Integer
    Dim POS_LENGTH As Integer
    Dim FID_LENGTH As Integer
    Dim FID_LENGTH1 As Integer
    Private stopCycle As Boolean = False
    Private pauseCycle As Boolean = False
    Private cycle As Process
    Private pythonProcess As Process



    Private Sub SetCtrlWhenClose()
        'ComboBoxDeviceList.Enabled = True

    End Sub

    Dim plc As New ActUtlType
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub btStart_Click(sender As Object, e As EventArgs)
        Timer2.Start()
        Timer3.Start()



    End Sub








    Private Sub btStart_MouseDown(sender As Object, e As MouseEventArgs) Handles btStart.MouseDown
        plc.SetDevice("M202", 1)

    End Sub
    Private Sub btStart_MouseUp(sender As Object, e As MouseEventArgs) Handles btStart.MouseUp
        plc.SetDevice("M202", 0)
    End Sub
    Private Sub btStop_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M203", 0)
    End Sub

    Private Sub btStop_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M203", 1)
    End Sub

    Private Sub btPass_MouseUp(sender As Object, e As MouseEventArgs) Handles btPass.MouseUp
        plc.SetDevice("M204", 0)
    End Sub

    Private Sub btPass_MouseDown(sender As Object, e As MouseEventArgs) Handles btPass.MouseDown
        plc.SetDevice("M204", 1)
    End Sub





    Private Sub Operations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        plc.ActLogicalStationNumber = 1
        plc.Open()
        Timer1.Start()
        FID_CHECK_BOX.Checked = MySettings.Default.fiducial
        If FID_CHECK_BOX.Checked Then
            plc.SetDevice("M243", 1)

        Else
            plc.SetDevice("M243", 0)
        End If

        MES_CHECK_BOX.Checked = MySettings.Default.MES1




        B_SCA_CHECK_BOX.Checked = MySettings.Default.BARCODE_SC
        If B_SCA_CHECK_BOX.Checked Then

            plc.SetDevice("M242", 1)
        Else

            plc.SetDevice("M242", 0)
        End If


        MARK_CHECK_BOX.Checked = MySettings.Default.L_MARK
        If MARK_CHECK_BOX.Checked Then

            plc.SetDevice("M241", 1)
        Else

            plc.SetDevice("M241", 0)
        End If


        SMEMA_CHECK_BOX.Checked = MySettings.Default.SMEMA_S
        If SMEMA_CHECK_BOX.Checked Then

            plc.SetDevice("M255", 1)
        Else

            plc.SetDevice("M255", 0)
        End If


        CAM_SCAN_CHECK_BOX.Checked = MySettings.Default.CAM_SCA



        FLIP_CHECK_BOX.Checked = MySettings.Default.FLIP
        If FLIP_CHECK_BOX.Checked Then

            plc.SetDevice("M256", 1)
        Else

            plc.SetDevice("M256", 0)
        End If


        RichTextBox5.Text = MySettings.Default.Good_Count
        RichTextBox7.Text = MySettings.Default.NG_Count

        AUTO_CHECK_BOX.Checked = MySettings.Default.AUTO_M
        IDLE_CHECK_BOX.Checked = MySettings.Default.IDLE_M
        PASS_CHECK_BOX.Checked = MySettings.Default.PASS_M

        If (MySettings.Default.COUNT_0 Is Nothing) Or (MySettings.Default.COUNT_0 = "") Then
            RichTextBox12.Text = "0"
            MySettings.Default.COUNT_0 = "0"
            MySettings.Default.Save()

        End If
        If (MySettings.Default.COUNT_1 Is Nothing) Or (MySettings.Default.COUNT_1 = "") Then
            RichTextBox13.Text = "0"
            MySettings.Default.COUNT_1 = "0"
            MySettings.Default.Save()

        End If
        MySettings.Default.Save()
        RichTextBox12.Text = MySettings.Default.COUNT_0
        RichTextBox13.Text = MySettings.Default.COUNT_1


        RichTextBox3.Text = MySettings.Default.TARGET_CNT
        RichTextBox6.Text = MySettings.Default.BOARD_CNT
        RichTextBox8.Text = MySettings.Default.PANEL_CNT
        RichTextBox11.Text = MySettings.Default.CYCLE_TIM












        'ComboBoxDeviceList.Items.Clear()
        'ComboBoxDeviceList.SelectedIndex = -1
        RichTextBox1.Text = My.Settings.ProgramName
        loadRecipe()
    End Sub
    Private Sub loadRecipe()

        Dim progname As String
        progname = "Defualt"
        If progname = "" Then
            Return
        ElseIf (progname IsNot "") Then

            Dim isValid1 As Boolean = True
            Dim isValid As Boolean = True
            Dim fname As String = "" & ConfigurationManager.AppSettings("DefaultPath").ToString().Trim()

            Dim path As String = fname
            Dim Logdir As String = "" & fname

            Dim ReceipeFileName As String = String.Empty

            If Not Directory.Exists(Logdir) Then
                Directory.CreateDirectory(Logdir)
            End If
            ReceipeFileName = progname
            Dim generatedFile As String = Logdir & ReceipeFileName & ".xml"
            Dim file1 As String = ReceipeFileName & ".xml"


            'Dim files As String = Directory.GetFiles(Logdir, file1, System.IO.SearchOption.AllDirectories)
            Dim check As Boolean = File.Exists(generatedFile)

            If check = False Then
                MySettings.Default.ProgramName = ""
                MsgBox("Program is not Loaded. Please Load Program")
                MySettings.Default.Save()
                Return
            End If
            RichTextBox1.Text = MySettings.Default.ProgramName







            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''''''''''''''''''''''     BOARD DATA   ''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''






            Dim xmDocument As XmlDocument = New XmlDocument()
            xmDocument.Load(generatedFile)
            Dim Boardnodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/BOARD")


            For Each Feeder As XmlNode In Boardnodes
                'Dim BOARDNAME As String = Feeder.ChildNodes(0).InnerText
                'Dim XOFFSET As String = Feeder.ChildNodes(1).InnerText
                'Dim YOFFSET As String = Feeder.ChildNodes(2).InnerText
                'Dim XCOUNT As String = Feeder.ChildNodes(3).InnerText
                'Dim YCOUNT As String = Feeder.ChildNodes(4).InnerText
                'Dim XPITCH As String = Feeder.ChildNodes(5).InnerText
                'Dim YPITCH As String = Feeder.ChildNodes(6).InnerText
                'Dim PAN_LENGTH As String = Feeder.ChildNodes(7).InnerText
                Dim PAN_WIDTH As String = Feeder.ChildNodes(8).InnerText
                'Dim PANEL_THICKNESS As String = Feeder.ChildNodes(9).InnerText
                MARK_ID = Convert.ToDouble(PAN_WIDTH)

                RichTextBox15.Text = MARK_ID


                plc.SetDevice("D374", MARK_ID)

            Next


            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''''''''''''''''''''''     FIDUCIAL DATA   ''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim FIDUCIAL1 As XmlNode = xmDocument.SelectSingleNode("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL")
            'Dim FNodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL/F")




            Dim BBT As String = FIDUCIAL1.ChildNodes(0).InnerText
            If BBT = "1" Then
                BOTTOM_SELEC = True
            Else
                BOTTOM_SELEC = False


            End If




            Dim X1 As Integer = 0
            Dim FIDUCIAL As XmlNode = xmDocument.SelectSingleNode("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL/TOP")
            Dim FNodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL/F")

            For Each Node As XmlNode In FNodes
                Dim VALUE(20) As String
                VALUE(0) = Node.Attributes("SIDE")?.InnerText
                VALUE(1) = Node("SHAPES")?.InnerText
                VALUE(2) = Node("F_X1")?.InnerText
                VALUE(3) = Node("F_Y1")?.InnerText
                VALUE(4) = Node("F_RX2")?.InnerText
                VALUE(5) = Node("F_RY2")?.InnerText
                VALUE(6) = Node("F_CP")?.InnerText
                VALUE(7) = Node("X_OFF")?.InnerText
                VALUE(8) = Node("Y_OFF")?.InnerText
                VALUE(9) = Node("F_PX")?.InnerText
                VALUE(10) = Node("F_PY")?.InnerText
                VALUE(11) = Node("F_SCORE")?.InnerText
                VALUE(12) = Node("FID_TYPE")?.InnerText


                If VALUE(0) = "TOP" Then



                    TOP_F_TYPE(d) = VALUE(0)





                    TOP_F_SHAP(d) = VALUE(1)
                    TOP_F_X1(d) = Convert.ToInt16(VALUE(2))
                    TOP_F_Y1(d) = Convert.ToInt16(VALUE(3))
                    TOP_F_RX2(d) = Convert.ToInt16(VALUE(4))
                    TOP_F_RY2(d) = Convert.ToInt16(VALUE(5))
                    Dim parts() As String = VALUE(6).Trim("()").Split(","c)


                    Dim trimmedPart() As String = parts(1).Split(")"c)


                    TOP_F_CPX(d) = Convert.ToInt16(parts(0))
                    TOP_F_CPY(d) = Convert.ToInt16(trimmedPart(0))
                    If VALUE(7) IsNot "" Then
                        TOP_F_XOFF(d) = Convert.ToInt16(VALUE(7))
                    End If
                    If VALUE(8) IsNot "" Then
                        TOP_F_YOFF(d) = Convert.ToInt16(VALUE(8))
                    End If
                    If VALUE(9) IsNot "" Then
                        TOP_F_POSX(d) = Convert.ToSingle(VALUE(9))
                    End If
                    If VALUE(10) IsNot "" Then
                        TOP_F_POSY(d) = Convert.ToSingle(VALUE(10))
                    End If

                    If VALUE(11) IsNot "" Then
                        TOP_F_SCORE(d) = Convert.ToInt16(VALUE(11))
                    End If




                    TOP_F_SEL(d) = VALUE(12)


                ElseIf VALUE(0) = "BOTTOM" Then

                    BOTTOM_F_TYPE(X1) = VALUE(0)
                    BOTTOM_F_SHAP(X1) = VALUE(1)
                    BOTTOM_F_X1(X1) = Convert.ToInt16(VALUE(2))
                    BOTTOM_F_Y1(X1) = Convert.ToInt16(VALUE(3))
                    BOTTOM_F_RX2(X1) = Convert.ToInt16(VALUE(4))
                    BOTTOM_F_RY2(X1) = Convert.ToInt16(VALUE(5))
                    Dim parts() As String = VALUE(6).Trim("()").Split(","c)


                    Dim trimmedPart() As String = parts(1).Split(")"c)


                    BOTTOM_F_CPX(X1) = Convert.ToInt16(parts(0))
                    BOTTOM_F_CPY(X1) = Convert.ToInt16(trimmedPart(0))
                    If VALUE(7) IsNot "" Then
                        BOTTOM_F_XOFF(X1) = Convert.ToInt16(VALUE(7))
                    End If
                    If VALUE(8) IsNot "" Then
                        BOTTOM_F_YOFF(X1) = Convert.ToInt16(VALUE(8))
                    End If
                    If VALUE(9) IsNot "" Then
                        BOTTOM_F_POSX(X1) = Convert.ToSingle(VALUE(9))
                    End If
                    If VALUE(10) IsNot "" Then
                        BOTTOM_F_POSY(X1) = Convert.ToSingle(VALUE(10))
                    End If

                    If VALUE(11) IsNot "" Then
                        BOTTOM_F_SCORE(X1) = Convert.ToInt16(VALUE(11))
                    End If



                    BOTTOM_F_SEL(X1) = VALUE(12)
                    X1 += 1
                End If
                d += 1
            Next






            ''''''''''''''''''''''''''''''''''''''''
            '''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''
            '''''''''''     POSITION DATA    '''''''''''

            '''''''''''''''''''''''''''''''''''''''''''''
            '''''''''''''''''''''''''''''''''''''''''''''




            Dim rootElement As XmlElement = xmDocument.DocumentElement
            Dim i1 As Integer
            ' Traverse each SIDE element
            For Each sideElement As XmlElement In rootElement.GetElementsByTagName("SIDE")
                ' Create a new TreeNode for SIDE and set the text (e.g., SIDE [FRONT])
                Dim sideName As String = sideElement.GetAttribute("name")
                Dim sideNode As New TreeNode($"SIDE [{sideName}]")
                SIDE_TYPE(i1) = sideName





                Dim I As Integer = 0
                Dim I3 As Integer = 0
                ' Traverse each POSI element within SIDE
                For Each posiElement As XmlElement In sideElement.GetElementsByTagName("POSI")
                    ' Extract POSI attributes (number, X, Y)
                    Dim posiNumber As String = posiElement.GetAttribute("number")
                    Dim posX As String = posiElement.GetAttribute("X")
                    Dim posY As String = posiElement.GetAttribute("Y")




                    ' Create a new TreeNode for POSI (e.g., POSI [1] [X:234][Y:130])
                    'Dim posiNode As New TreeNode($"POSI [{posiNumber}] [X:{posX}][Y:{posY}]")
                    Dim x As Single = CSng(posX)
                    Dim y As Single = CSng(posY)

                    If SIDE_TYPE(i1).StartsWith("TOP") Then
                        TOP_XPOSITION(I) = x
                        TOP_YPOSITION(I) = y
                        TOP_Length = I
                    ElseIf SIDE_TYPE(i1).StartsWith("BOTTOM") Then
                        BOTTOM_XPOSITION(I3) = x
                        BOTTOM_YPOSITION(I3) = y
                        BOTTOM_Length = I3
                    End If
                    ' Traverse each CODE element within POSI
                    Dim ii As Integer

                    For Each codeElement As XmlElement In posiElement.GetElementsByTagName("CODE")
                        ' Create a new TreeNode for CODE and set the text (e.g., (CODE)<YEAR:2:0:0>...)
                        Dim codeText As String = codeElement.InnerText
                        'Dim codeNode As New TreeNode($"(CODE){codeText}")
                        'MARK_CODE(ii) = codeText
                        If SIDE_TYPE(i1).StartsWith("TOP") Then
                            TOP_MARK_CODE(ii) = codeText
                        ElseIf SIDE_TYPE(i1).StartsWith("BOTTOM") Then
                            BOTTOM_MARK_CODE(I3) = codeText
                        End If

                        Dim codeT As String
                        Dim codeD As String
                        ' Add CODE node under POSI node
                        'posiNode.Nodes.Add(codeNode)

                        For Each codeTElement As XmlElement In posiElement.GetElementsByTagName("CODETYPE")

                            codeT = codeTElement.InnerText
                            If SIDE_TYPE(i1).StartsWith("TOP") Then
                                TOP_CODE_TYPE(ii) = codeT
                            ElseIf SIDE_TYPE(i1).StartsWith("BOTTOM") Then
                                BOTTOM_CODE_TYPE(I3) = codeT
                            End If


                        Next

                        For Each codeDElement As XmlElement In posiElement.GetElementsByTagName("CODEDATA")

                            codeD = codeDElement.InnerText
                        Next



                        ' codeNode.Tag = New With {
                        ' .CODE_TYPE = codeT,
                        ' .CODE_DATA = codeD
                        ' }

                        ii = ii + 1
                    Next
                    Dim PROGID As String
                    Dim LSRPWR As String
                    Dim SCANSPD As String

                    For Each codePElement As XmlElement In posiElement.GetElementsByTagName("PROGRAMID")

                        PROGID = codePElement.InnerText

                    Next

                    For Each codeLElement As XmlElement In posiElement.GetElementsByTagName("LASERPOWER")

                        LSRPWR = codeLElement.InnerText
                    Next

                    For Each codeSElement As XmlElement In posiElement.GetElementsByTagName("SCANSPEED")

                        SCANSPD = codeSElement.InnerText
                    Next
                    For Each codeSElement As XmlElement In posiElement.GetElementsByTagName("CODENO")

                        CODEN = codeSElement.InnerText
                    Next




                    If SIDE_TYPE(i1).StartsWith("TOP") Then
                        TOP_PROGRAM_ID(I) = PROGID
                        TOP_LASER_POWER(I) = LSRPWR
                        TOP_SCAN_SPEED(I) = SCANSPD
                        TOP_CODE_NO(I) = CODEN

                    ElseIf SIDE_TYPE(i1).StartsWith("BOTTOM") Then
                        BOTTOM_PROGRAM_ID(I3) = PROGID
                        BOTTOM_LASER_POWER(I3) = LSRPWR
                        BOTTOM_SCAN_SPEED(I3) = SCANSPD
                        BOTTOM_CODE_NO(I3) = CODEN
                    End If


                    ' posiNode.Tag = New With {
                    ' .Program_Id = PROGID,
                    ' .Laser_Power = LSRPWR,
                    ' .Scan_Speed = SCANSPD
                    ' }

                    ' ' Add POSI node under SIDE node
                    ' sideNode.Nodes.Add(posiNode)

                    I = I + 1
                    If SIDE_TYPE(i1).StartsWith("BOTTOM") Then
                        I3 = I3 + 1
                    End If

                Next

                i1 = i1 + 1

                ' Add SIDE node to TreeView
                ' TreeView1.Nodes.Add(sideNode)
            Next

            ' Expand the tree to show all nodes

        End If

    End Sub
    Dim length As Integer
    Public Sub RunPythonScript()
        'Dim pythonPath As String = "C:/Users/HP/AppData/Local/Programs/Python/Python310/python.exe"
        'Dim scriptPath As String = "C:\Users\HP\Downloads\test4.py"
        'Dim imagePath As String = String.Empty ' Update if needed

        'Dim startInfo As New ProcessStartInfo(pythonPath)
        'startInfo.Arguments = """" & scriptPath & """ " & imagePath
        'startInfo.UseShellExecute = False
        'startInfo.RedirectStandardOutput = True
        'startInfo.RedirectStandardError = True

        'Dim process As New Process()
        'process.StartInfo = startInfo
        'process.Start()

        'Dim output As String = process.StandardOutput.ReadToEnd()
        'Dim [error] As String = process.StandardError.ReadToEnd()

        'process.WaitForExit()
        'Thread.Sleep(200)
        '' Refined regex pattern to handle optional negative signs
        'Dim regex As New Regex("Offset: \((?<x>-?\d+), (?<y>-?\d+)\)")
        'Dim match As Match = regex.Match(output)

        'If Match.Success Then
        '    ' Extract coordinates from regex groups
        '    Dim x As String = Match.Groups("x").Value
        '    Dim y As String = Match.Groups("y").Value

        '    ' Update UI with coordinates
        '    'TextBox2.Text = $"Offset: ({x}, {y})"

        '    fid_X = Convert.ToInt16(x)
        '    fid_Y = Convert.ToInt16(y)


        'Else
        '    MsgBox("Offset is not found")
        '    plc.SetDevice("M304", 1)
        '    stopCycle = True
        '    bit = False
        '    Return

        'End If

        '' Display the output and error in the console for debugging
        'Console.WriteLine("Output: " & output)
        'Console.WriteLine("Error: " & [Error])

        ' Optionally handle the disposed image if needed
        ' Dispose of the previous image if it exists
    End Sub


    Private Async Function SAVE_PIC() As Task
        plc.SetDevice("M247", 1)






        For I = 0 To 2


            'Await Task.Delay(700)
            Dim nRet As Int32 = Home_Page.FidCam1.SetEnumValue("TriggerSource", CCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE)
            nRet = Home_Page.FidCam1.SetEnumValue("TriggerMode", CCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON)
            nRet = Home_Page.FidCam1.StartGrabbing()
            'Await Task.Delay(300)
            Dim stFrameOut As CCamera.MV_FRAME_OUT = New CCamera.MV_FRAME_OUT()
            Dim stDisplayInfo As CCamera.MV_DISPLAY_FRAME_INFO = New CCamera.MV_DISPLAY_FRAME_INFO()
            ''Dim nRet1 = Home_Page.FidCam1.GetImageBuffer(stFrameOut, 100)
            nRet = Home_Page.FidCam1.SetGrabStrategy(CCamera.MV_GRAB_STRATEGY.MV_GrabStrategy_LatestImages)
            nRet = Home_Page.FidCam1.SetOutputQueueSize(2)
            nRet = Home_Page.FidCam1.SetCommandValue("TriggerSoftware")

            Dim nRet1 = Home_Page.FidCam1.GetImageBuffer(stFrameOut, 5000)
            ' ch:设置输出缓存个数 | en:Set Output Queue Size


            Task.Delay(300)

            If CCamera.MV_OK = nRet Then

                Dim PRNO As String = RichTextBox1.Text


                Dim OPERATION_IMAGE As String = "" & ConfigurationManager.AppSettings("OPERATION_IMG").ToString().Trim()

                Dim PAT As String = Path.Combine(OPERATION_IMAGE & PRNO)
                If Not Directory.Exists(PAT) Then
                    Directory.CreateDirectory(PAT)
                End If
                Dim currentDateTime As DateTime = DateTime.Now

                Dim year As String = currentDateTime.ToString("yy")
                Dim month As String = Convert.ToString(currentDateTime.Month)
                Dim day As String = Convert.ToString(currentDateTime.Day)
                Dim hour As String = Convert.ToString(currentDateTime.Hour)
                Dim minute As String = Convert.ToString(currentDateTime.Minute)
                IMAGEPATH1 = "" & ConfigurationManager.AppSettings("FiducialImage").ToString().Trim()
                Dim PATHR As String = (hour & "_" & minute & "_" & day & "_" & month & "_" & year)

                IMAGE_PATH = Path.Combine(PAT & PATHR & ".png")



                Dim filePath As String = IMAGEPATH1 & "123" & ".Png"

                ' Check if the file exists
                If File.Exists(filePath) Then
                    Try
                        ' Delete the file
                        File.Delete(filePath)

                    Catch ex As Exception
                        Console.WriteLine("Error deleting file: " & ex.Message)
                    End Try

                End If



                If stFrameOut.stFrameInfo.nFrameLen > m_nBufSizeForDriver1 Then
                    m_nBufSizeForDriver1 = stFrameOut.stFrameInfo.nFrameLen
                    ReDim m_pBufForDriver1(m_nBufSizeForDriver1)
                End If

                m_stFrameInfoEx = stFrameOut.stFrameInfo
                Marshal.Copy(stFrameOut.pBufAddr, m_pBufForDriver1, 0, stFrameOut.stFrameInfo.nFrameLen)

                Dim stSaveImageParam As CCamera.MV_SAVE_IMG_TO_FILE_PARAM = New CCamera.MV_SAVE_IMG_TO_FILE_PARAM()
                Dim pData As IntPtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver1, 0)
                stSaveImageParam.pData = pData
                stSaveImageParam.nDataLen = m_stFrameInfoEx.nFrameLen
                stSaveImageParam.enPixelType = m_stFrameInfoEx.enPixelType
                stSaveImageParam.nWidth = m_stFrameInfoEx.nWidth
                stSaveImageParam.nHeight = m_stFrameInfoEx.nHeight
                stSaveImageParam.enImageType = CCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Png
                stSaveImageParam.iMethodValue = 1
                stSaveImageParam.nQuality = 90
                stSaveImageParam.pImagePath = IMAGEPATH1 & "123" & ".Png"

                ''Thread.Sleep(100)





                ' Create the directory if it doesn't exist







                'File.Delete("D:\Logs\fidimage")
                nRet1 = Home_Page.FidCam1.SaveImageToFile(stSaveImageParam)



                Home_Page.FidCam1.DisplayOneFrame(stDisplayInfo)

                Home_Page.FidCam1.FreeImageBuffer(stFrameOut)
                Home_Page.FidCam1.StopGrabbing()
                ''nRet = Home_Page.FidCam1.SetEnumValue("TriggerSource", CCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE)
                ''' nRet = Home_Page.FidCam1.SetEnumValue("TriggerMode", CCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON)
            Else
                'plc.SetDevice("M247", 0)  ''''''''''' RED LIGHT
                Thread.Sleep(100)
            End If
            I += I
        Next
        plc.SetDevice("M247", 0)
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Try
            Dim value As Integer
            plc.GetDevice("M224", value)

            If (value = 1) AndAlso Not bit Then
                ' Disable buttons
                bt_Gateopenl.Enabled = False
                bt_Gateopenr.Enabled = False
                bt_Pcbstopper.Enabled = False
                Button11.Enabled = False
                bt_Pcbload.Enabled = False
                bt_Pcbunload.Enabled = False
                Button13.Enabled = False
                Button3.Enabled = False
                bt_Pcbclamp.Enabled = False
                bt_Pcbunclamp.Enabled = False
                btStart.BackColor = Color.Green

                ' Send data
                Sendin_DATA()

            ElseIf (value = 0) AndAlso bit Then
                ' Reset PLC devices
                plc.SetDevice("M300", 0)
                plc.SetDevice("M247", 0)  ' RED LIGHT
                ''Await Task.Delay(100)

                ' Re-enable buttons
                bt_Gateopenl.Enabled = True
                bt_Gateopenr.Enabled = True
                bt_Pcbstopper.Enabled = True
                Button11.Enabled = True
                bt_Pcbload.Enabled = True
                bt_Pcbunload.Enabled = True
                Button13.Enabled = True
                Button3.Enabled = True
                bt_Pcbclamp.Enabled = True
                bt_Pcbunclamp.Enabled = True
                btStart.BackColor = Color.White
            End If

        Catch ex As Exception
            MessageBox.Show($"Error in Timer1_Tick: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Private Async Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

    'End Sub
    Public Function ConvertFloatToWord(ByVal value As Single) As Integer()
        Dim floatBytes As Byte() = BitConverter.GetBytes(value)
        Dim lowWord As Integer = BitConverter.ToInt16(floatBytes, 0)
        Dim highWord As Integer = BitConverter.ToInt16(floatBytes, 2)
        Return {lowWord, highWord}
    End Function


    ' Convert word to float
    Public Function ConvertWordToFloat(ByVal register As Integer()) As Single
        Dim bytes(3) As Byte
        Dim lowWordBytes() As Byte = BitConverter.GetBytes(register(0))
        Dim highWordBytes() As Byte = BitConverter.GetBytes(register(1))

        Array.Copy(lowWordBytes, 0, bytes, 0, 2)
        Array.Copy(highWordBytes, 0, bytes, 2, 2)

        Return BitConverter.ToSingle(bytes, 0)
    End Function



    Private Sub Operations_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Home_Page.FidCam1.CloseDevice()
        Home_Page.FidCam1.DestroyDevice()
        Home_Page.LiveCamera1.CloseDevice()
        Home_Page.LiveCamera1.DestroyDevice()
        plc.SetDevice("M247", 0)
        Close_Exe.Main()

        Timer2.Stop()
        Timer3.Stop()
        Dim nRet As Int32 = Home_Page.FidCam1.SetEnumValue("TriggerSource", CCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE)
        nRet = Home_Page.FidCam1.SetEnumValue("TriggerMode", CCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON)
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick


    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick











    End Sub

    'Private Sub CheckBox2_Click(sender As Object, e As EventArgs)
    'Timer1.Start()
    'End Sub


    Private Sub bt_Pcbstopper_MouseDown(sender As Object, e As MouseEventArgs) Handles bt_Pcbstopper.MouseDown
        plc.SetDevice("M219", 1)
    End Sub

    Private Sub bt_Pcbstopper_MouseUp(sender As Object, e As MouseEventArgs) Handles bt_Pcbstopper.MouseUp
        plc.SetDevice("M219", 0)
    End Sub

    Private Sub bt_Gateopenl_MouseDown(sender As Object, e As MouseEventArgs) Handles bt_Gateopenl.MouseDown
        plc.SetDevice("M235", 1)
    End Sub

    Private Sub bt_Gateopenl_MouseUp(sender As Object, e As MouseEventArgs) Handles bt_Gateopenl.MouseUp
        plc.SetDevice("M235", 0)
    End Sub

    Private Sub bt_Gateopenr_MouseDown(sender As Object, e As MouseEventArgs) Handles bt_Gateopenr.MouseDown
        plc.SetDevice("M236", 1)
    End Sub

    Private Sub bt_Gateopenr_MouseUp(sender As Object, e As MouseEventArgs) Handles bt_Gateopenr.MouseUp
        plc.SetDevice("M236", 0)

    End Sub

    Private Sub btLoadpos_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M233", 1)
    End Sub

    Private Sub btLoadpos_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M233", 0)
    End Sub

    Private Sub btUnloadpos_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M234", 1)
    End Sub

    Private Sub btUnloadpos_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M234", 0)
    End Sub

    Private Sub bt_Pcbload_MouseDown(sender As Object, e As MouseEventArgs) Handles bt_Pcbload.MouseDown
        plc.SetDevice("M233", 1)
    End Sub

    Private Sub bt_Pcbload_MouseUp(sender As Object, e As MouseEventArgs) Handles bt_Pcbload.MouseUp
        plc.SetDevice("M233", 0)
    End Sub

    Private Sub bt_Pcbunload_MouseDown(sender As Object, e As MouseEventArgs) Handles bt_Pcbunload.MouseDown
        plc.SetDevice("M234", 1)
    End Sub

    Private Sub bt_Pcbunload_MouseUp(sender As Object, e As MouseEventArgs) Handles bt_Pcbunload.MouseUp
        plc.SetDevice("M234", 0)
    End Sub

    Private Sub Button9_KeyDown(sender As Object, e As KeyEventArgs)
        plc.SetDevice("M239", 1)
    End Sub

    Private Sub bt_Pcbclamp_KeyDown(sender As Object, e As KeyEventArgs) Handles bt_Pcbclamp.KeyDown
        plc.SetDevice("M239", 1)
    End Sub

    Private Sub bt_Pcbclamp_MouseDown(sender As Object, e As MouseEventArgs) Handles bt_Pcbclamp.MouseDown
        plc.SetDevice("M248", 1)
    End Sub

    Private Sub Button3_MouseDown(sender As Object, e As MouseEventArgs) Handles Button3.MouseDown
        plc.SetDevice("M239", 1)
    End Sub

    Private Sub Button3_MouseUp(sender As Object, e As MouseEventArgs) Handles Button3.MouseUp
        plc.SetDevice("M239", 0)
    End Sub

    Private Sub TableLayoutPanel3_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Button13_MouseDown(sender As Object, e As MouseEventArgs) Handles Button13.MouseDown
        plc.SetDevice("M237", 1)
    End Sub

    Private Sub Button13_MouseUp(sender As Object, e As MouseEventArgs) Handles Button13.MouseUp
        plc.SetDevice("M237", 0)
    End Sub

    Private Sub Button11_MouseDown(sender As Object, e As MouseEventArgs) Handles Button11.MouseDown
        plc.SetDevice("M252", 1)
    End Sub

    Private Sub Button11_MouseUp(sender As Object, e As MouseEventArgs) Handles Button11.MouseUp
        plc.SetDevice("M252", 0)
    End Sub

    Private Sub btStart_Click_1(sender As Object, e As EventArgs) Handles btStart.Click
        Timer2.Start()
        Timer3.Start()



    End Sub
    Private Sub live_camera()
        Dim nRet1 As Int32 = Home_Page.LiveCamera1.SetEnumValue("TriggerSource", CCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0)
        nRet1 = Home_Page.LiveCamera1.SetEnumValue("TriggerMode", CCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF)

        Dim stFrameOut As CCamera.MV_FRAME_OUT = New CCamera.MV_FRAME_OUT()
        Dim stDisplayInfo As CCamera.MV_DISPLAY_FRAME_INFO = New CCamera.MV_DISPLAY_FRAME_INFO()
        Dim nRet = Home_Page.LiveCamera1.GetImageBuffer(stFrameOut, 1000)

        If CCamera.MV_OK = nRet Then

            If stFrameOut.stFrameInfo.nFrameLen > m_nBufSizeForDriver Then
                m_nBufSizeForDriver = stFrameOut.stFrameInfo.nFrameLen
                ReDim m_pBufForDriver(m_nBufSizeForDriver)
            End If
            m_stFrameInfoEx = stFrameOut.stFrameInfo
            Marshal.Copy(stFrameOut.pBufAddr, m_pBufForDriver, 0, stFrameOut.stFrameInfo.nFrameLen)
            'stDisplayInfo.hWnd = PictureBox1.Handle
            stDisplayInfo.pData = stFrameOut.pBufAddr
            stDisplayInfo.nDataLen = stFrameOut.stFrameInfo.nFrameLen
            stDisplayInfo.nWidth = stFrameOut.stFrameInfo.nWidth
            stDisplayInfo.nHeight = stFrameOut.stFrameInfo.nHeight
            stDisplayInfo.enPixelType = stFrameOut.stFrameInfo.enPixelType
            Home_Page.LiveCamera1.DisplayOneFrame(stDisplayInfo)
            Home_Page.LiveCamera1.FreeImageBuffer(stFrameOut)

        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs)
        If FID_CHECK_BOX.Checked Then
            MySettings.Default.fiducial = True
            MySettings.Default.Save()
            plc.SetDevice("M243", 1)
        Else
            MySettings.Default.fiducial = False
            MySettings.Default.Save()
            plc.SetDevice("M243", 0)
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles MES_CHECK_BOX.CheckedChanged
        If MES_CHECK_BOX.Checked Then
            MySettings.Default.MES1 = True
            MySettings.Default.Save()
        Else
            MySettings.Default.MES1 = False
            MySettings.Default.Save()
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged_1(sender As Object, e As EventArgs) Handles B_SCA_CHECK_BOX.CheckedChanged
        If B_SCA_CHECK_BOX.Checked Then
            MySettings.Default.BARCODE_SC = True
            MySettings.Default.Save()
            plc.SetDevice("M242", 1)
        Else
            MySettings.Default.BARCODE_SC = False
            MySettings.Default.Save()
            plc.SetDevice("M242", 0)
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles MARK_CHECK_BOX.CheckedChanged
        If MARK_CHECK_BOX.Checked Then
            MySettings.Default.L_MARK = True
            MySettings.Default.Save()
            plc.SetDevice("M241", 1)
        Else
            MySettings.Default.L_MARK = False
            MySettings.Default.Save()
            plc.SetDevice("M241", 0)
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles SMEMA_CHECK_BOX.CheckedChanged
        If SMEMA_CHECK_BOX.Checked Then
            MySettings.Default.SMEMA_S = True
            MySettings.Default.Save()
            plc.SetDevice("M255", 1)
        Else
            MySettings.Default.SMEMA_S = False
            MySettings.Default.Save()
            plc.SetDevice("M255", 0)
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CAM_SCAN_CHECK_BOX.CheckedChanged
        If CAM_SCAN_CHECK_BOX.Checked Then
            MySettings.Default.CAM_SCA = True
            MySettings.Default.Save()

        Else
            MySettings.Default.CAM_SCA = False
            MySettings.Default.Save()

        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles FLIP_CHECK_BOX.CheckedChanged
        If FLIP_CHECK_BOX.Checked Then
            MySettings.Default.FLIP = True
            MySettings.Default.Save()
            plc.SetDevice("M256", 1)
        Else
            MySettings.Default.FLIP = False
            MySettings.Default.Save()
            plc.SetDevice("M256", 0)
        End If
    End Sub
    Private Async Sub Sendin_DATA()



        bit = True
        stopCycle = False
        Try
            plc.GetDevice("M900", AUTO)
            Dim ddd As Integer

            If stopCycle Then Exit Sub
            Await Task.Run(Sub() PauseCheck())

            plc.SetDevice("M202", 1)
            Await Task.Delay(100) ' Replaces Thread.Sleep
            'Await Task.Delay(200) ' Replaces Thread.Sleep
            Dim L As Int16 = SIDE_TYPE.Length
            For D = 0 To L - 1






                If stopCycle Then Return

                If SIDE_TYPE(D) = "TOP" Then
                    If FID_CHECK_BOX.Checked Then
                        Await ProcessCheckBox5OperationsAsync(D)

                        Dim X_OLD As Integer = FID_X_OLD(1) - FID_X_OLD(0)
                        Dim Y_OLD As Integer = FID_Y_OLD(1) - FID_Y_OLD(0)


                        Dim X_NEW As Integer = FID_X_MA(1) - FID_X_MA(0)
                        Dim Y_NEW As Integer = FID_Y_MA(1) - FID_Y_MA(0)







                        Dim X_FID_OFF As Integer = X_NEW - X_OLD
                        Dim Y_FID_OFF As Integer = Y_NEW - Y_OLD
                        plc.SetDevice("D376", X_FID_OFF)
                        plc.SetDevice("D378", Y_FID_OFF)

                        Await Task.Delay(200)
                    End If

                ElseIf SIDE_TYPE(D) = "BOTTOM" And BOTTOM_SELEC = True Then
                    If D = 1 Then
                        Dim FDD As Integer
                        FDD = 1
                        plc.SetDevice("D375", FDD)
                        Dim SD As Integer
                        Task.Delay(100)
                        Do
                            If stopCycle Then Return

                            plc.GetDevice("M261", SD)
                        Loop Until SD = 1


                    End If
                    If FID_CHECK_BOX.Checked Then
                        Await ProcessCheckBox5OperationsAsync(D)

                        Dim X_OLD As Integer = FID_X_OLD(1) - FID_X_OLD(0)
                        Dim Y_OLD As Integer = FID_Y_OLD(1) - FID_Y_OLD(0)


                        Dim X_NEW As Integer = FID_X_MA(1) - FID_X_MA(0)
                        Dim Y_NEW As Integer = FID_Y_MA(1) - FID_Y_MA(0)







                        Dim X_FID_OFF As Integer = X_NEW - X_OLD
                        Dim Y_FID_OFF As Integer = Y_NEW - Y_OLD
                        plc.SetDevice("D376", X_FID_OFF)
                        plc.SetDevice("D378", Y_FID_OFF)

                        Await Task.Delay(200)
                    End If
                End If



                plc.SetDevice("M247", 0)
                plc.SetDevice("M303", 1)
                Await ProcessLoopOperationsAsync(D)







            Next









            plc.SetDevice("M301", 1)
            plc.GetDevice("M900", AUTO)
            If (RichTextBox6.Text Is Nothing) Or (RichTextBox6.Text = "") Then
                RichTextBox6.Text = "1"
                MySettings.Default.BOARD_CNT = RichTextBox6.Text
                MySettings.Default.Save()
            Else




                Dim as1 As Int16 = Convert.ToInt16(RichTextBox6.Text)
                as1 = as1 + 1
                RichTextBox6.Text = as1
                MySettings.Default.BOARD_CNT = RichTextBox6.Text
                MySettings.Default.Save()
            End If

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try
        bit = False
    End Sub

    Private Async Function ProcessCheckBox5OperationsAsync(ByVal ddd As Integer) As Task
        Dim L1 As Integer = 0






        For J = 0 To 1

            'Do Until (F_SEL(L1) = 1)

            If stopCycle Then Return
            Await Task.Run(Sub() PauseCheck())
            plc.SetDevice("M247", 1)

            Dim wordsX() As Integer
            Dim wordsY() As Integer
            If SIDE_TYPE(ddd) = "TOP" Then
                wordsX = ConvertFloatToWord(TOP_F_POSX(J))
                wordsY = ConvertFloatToWord(TOP_F_POSY(J))
            ElseIf SIDE_TYPE(ddd) = "BOTTOM" And BOTTOM_SELEC = True Then
                wordsX = ConvertFloatToWord(BOTTOM_F_POSX(J))
                wordsY = ConvertFloatToWord(BOTTOM_F_POSY(J))


            End If

            plc.SetDevice("D370", wordsX(0))
            plc.SetDevice("D371", wordsX(1))


            plc.SetDevice("D372", wordsY(0))
            plc.SetDevice("D373", wordsY(1))
            plc.SetDevice("M302", 1)
            Await WaitForPLCAsync(CHECK1)

            ' Red light and capture image

            Task.Delay(200)
            If stopCycle Then Return
            Dim nRet1 As Int32 = Home_Page.FidCam1.SetCommandValue("TriggerSoftware")
            SAVE_PIC()
            If stopCycle Then Return
            ' Image processing

            Await ProcessImageAsync()

            ' Write to file
            'Dim centx As Integer = Convert.ToInt16(F_CPX(L1)) * 4
            'Dim centy As Integer = Convert.ToInt16(F_CPY(L1)) * 4
            'Dim valu As String = $"({centx},{centy})"


            ' Run Python script
            'Await RunPythonScriptAsync()
            Await FIDU(J, ddd)
            FID_X_MA(J) = fid_X
            FID_Y_MA(J) = fid_Y


            If SIDE_TYPE(ddd) = "TOP" Then
                FID_X_OLD(J) = Convert.ToInt16(TOP_F_CPX(J))
                FID_Y_OLD(J) = Convert.ToInt16(TOP_F_CPY(J))
            ElseIf SIDE_TYPE(ddd) = "BOTTOM" Then
                FID_X_OLD(J) = Convert.ToInt16(BOTTOM_F_CPX(J))
                FID_Y_OLD(J) = Convert.ToInt16(BOTTOM_F_CPY(J))


            End If




        Next


    End Function
    Private Async Function FIDU(ByVal ID As Int16, ID1 As Int16) As Task
        Dim foldername As String = RichTextBox1.Text

        'Get the selected row
        Dim fidTempImage1 As String = "" & ConfigurationManager.AppSettings("FiducialTemplate").ToString().Trim()

        'Dim selectedRow As DataGridViewRow = datagrdFid.SelectedRows(0)
        Dim selectedShapeID As String
        Dim FIDNO As String

        If SIDE_TYPE(ID1) = "TOP" Then
            selectedShapeID = TOP_F_TYPE(ID)
            FIDNO = TOP_F_SEL(ID)

        ElseIf SIDE_TYPE(ID1) = "BOTTOM" Then
            selectedShapeID = BOTTOM_F_TYPE(ID)
            FIDNO = BOTTOM_F_SEL(ID)

        End If

        If stopCycle Then Return

        Dim tempdirec As String = Path.Combine(fidTempImage1, foldername, selectedShapeID, FIDNO & ".png")


        Using pipeServer As New NamedPipeServerStream("TestPipe", PipeDirection.InOut)
            If stopCycle Then Return
            Console.WriteLine("Waiting for connection...")
            pipeServer.WaitForConnection()

            Dim message As String = tempdirec & "," & IMAGE_PATH
            Dim buffer As Byte() = Encoding.UTF8.GetBytes(message)

            pipeServer.Write(buffer, 0, buffer.Length)
            Console.WriteLine("Message sent to client.")
            Dim buffer1 As Byte() = New Byte(255) {}
            pipeServer.Read(buffer1, 0, buffer1.Length)

            Dim message1 As String = Encoding.UTF8.GetString(buffer1).TrimEnd(ChrW(0))
            Console.WriteLine("Message received from server: " & message1)
            Dim DATA() As String = message1.Split(",")




            fid_X = Convert.ToInt16(DATA(0))
            fid_Y = Convert.ToInt16(DATA(1))
            score = DATA(2)
            RichTextBox14.Text = DATA(0)
            RichTextBox16.Text = DATA(1)


            pipeServer.Disconnect()
        End Using












    End Function
    Private Async Function WaitForPLCAsync(ByVal CHECK2 As Int32) As Task
        Do
            If stopCycle Then Return
            Await Task.Run(Sub() PauseCheck())
            plc.SetDevice("M302", 1)
            Await Task.Delay(200)
            plc.GetDevice("M352", CHECK2)
        Loop Until CHECK2 = 1
    End Function

    Private Async Function ProcessLoopOperationsAsync(ByVal d As Int16) As Task
        Dim i As Int32
        Dim ST As Integer




        If SIDE_TYPE(d) = "TOP" Then
            POS_LENGTH = TOP_Length
        Else
            POS_LENGTH = BOTTOM_Length
        End If


        For i = 0 To (POS_LENGTH)
            If stopCycle Then Return
            plc.SetDevice("M303", 0)
            Await Task.Run(Sub() PauseCheck())

            Do
                If stopCycle Then Return

                Await Task.Run(Sub() PauseCheck())
                plc.GetDevice("M224", au_start)
                plc.GetDevice("M300", stop1)
                If i > 0 Then
                    plc.GetDevice("M353", NXT)
                Else
                    NXT = 1
                End If
                plc.GetDevice("M303", ST)
                plc.GetDevice("X13", emg)


                If (emg = 1) Then
                    Return
                End If

            Loop Until (NXT = 1 And au_start = 1 And stop1 = 0 And ST = 0)
            ''Await WaitForPLCAsync1(ST)

            ''plc.SetDevice("M353", 0)
            Await Task.Delay(300)

            ' Set device with coordinates
            SetPLCCoordinates(i, d)

            plc.SetDevice("M300", 1)
            Await Task.Delay(100)
            RichTextBox12.Text = Convert.ToString(CNT1)
            RichTextBox13.Text = Convert.ToString(CNT2)
            MySettings.Default.COUNT_0 = RichTextBox12.Text

            MySettings.Default.COUNT_1 = RichTextBox13.Text
            MySettings.Default.Save()
            If B_SCA_CHECK_BOX.Checked = False Then
                WaitForPLCAsync1(ST)
            End If


            If B_SCA_CHECK_BOX.Checked Then
                If stopCycle Then Return
                Await ProcessBarcodeCheckAsync(i)

                If RichTextBox4.Text = RichTextBox2.Text Then
                    MySettings.Default.Good_Count += 1
                    MySettings.Default.Save()
                    RichTextBox5.Text = MySettings.Default.Good_Count
                Else
                    MySettings.Default.NG_Count += 1
                    MySettings.Default.Save()
                    RichTextBox7.Text = MySettings.Default.NG_Count
                End If

            End If

        Next
    End Function
    Private Async Function WaitForPLCAsync1(ByVal st1 As Int32) As Task


        Do
            If stopCycle Then Return

            Await Task.Run(Sub() PauseCheck())
            plc.GetDevice("M224", au_start)
            plc.GetDevice("M300", stop1)

            plc.GetDevice("M353", NXT)
            plc.GetDevice("M303", st)
            plc.GetDevice("X13", emg)


            If (emg = 1) Then
                Return
            End If

        Loop Until (NXT = 1 And au_start = 1 And stop1 = 0 And st = 0)

    End Function
    Private Sub SetPLCCoordinates(ByVal index As Integer, INT As Integer)



        Dim CODE_DATA As String
        Dim PATT As String = "<[^>]+>|[a-zA-Z]+"
        Dim floatValueX As Single
        If SIDE_TYPE(INT) = "TOP" Then

            If stopCycle Then Return
            If Single.TryParse(TOP_XPOSITION(index), floatValueX) Then
                Dim wordsX() As Integer = ConvertFloatToWord(floatValueX)
                plc.SetDevice("D370", wordsX(0))
                plc.SetDevice("D371", wordsX(1))
            End If

            Dim floatValueY As Single
            If Single.TryParse(TOP_YPOSITION(index).ToString(), floatValueY) Then
                Dim wordsY() As Integer = ConvertFloatToWord(floatValueY)
                plc.SetDevice("D372", wordsY(0))
                plc.SetDevice("D373", wordsY(1))
            End If

            Dim RED As String = TOP_MARK_CODE(index)

            Dim matches As MatchCollection = Regex.Matches(RED, PATT)

            For Each match As Match In matches

                If stopCycle Then Return
                Dim dd As String = match.Value
                If (dd.StartsWith("<")) Then

                    If (dd.EndsWith(">")) Then

                        Dim trimmedInput As String = dd.Trim("<"c, ">"c)

                        ' Split the remaining string by the colon ":" delimiter
                        Dim splitData() As String = trimmedInput.Split(":"c)

                        ' Access individual parts of the split data
                        Dim part1 As String = splitData(0) ' "MONTH"
                        Dim part2 As String = splitData(1) ' "5"

                        Dim part3 As String = splitData(2) ' "0"
                        Dim part4 As String = splitData(3) ' "0"
                        Dim part21 As Integer = Convert.ToInt16(part2)
                        Dim part31 As Integer = Convert.ToInt16(part3)
                        Dim part41 As Integer = Convert.ToInt16(part4)

                        Dim dat As String
                        Dim data1 As String
                        Dim DDD1 As Integer
                        ' Display the result in a TextBox or use in any logic
                        If part1 = "YEAR" Then

                            data1 = DateTime.Now.Year

                            ' Display the current year in a TextBox or use it in any logic
                            If part21 < 4 Then
                                DDD1 = 4 - part21
                                dat = data1.ToString().Substring(DDD1)
                            Else
                                dat = data1.ToString()
                            End If




                        ElseIf part1 = "MONTH" Then
                            data1 = DateTime.Now.Month

                            dat = data1.ToString()
                        ElseIf part1 = "DAY" Then
                            data1 = DateTime.Now.Day

                            dat = data1.ToString()

                        ElseIf part1 = "HOUR" Then
                            data1 = DateTime.Now.Hour

                            dat = data1.ToString()
                        ElseIf part1 = "MINUTE" Then
                            data1 = DateTime.Now.Minute

                            dat = data1.ToString()
                        ElseIf part1 = "SEC" Then
                            data1 = DateTime.Now.Second

                            dat = data1.ToString()
                        ElseIf part1 = "COUNTER1" Then

                            CNT1 = Convert.ToInt16(RichTextBox12.Text)
                            CNT1 += 1
                            dat = Convert.ToString(CNT1)

                        ElseIf part1 = "COUNTER2" Then

                            CNT2 = Convert.ToInt16(RichTextBox13.Text)
                            CNT2 += 1
                            dat = Convert.ToString(CNT2)


                        End If



                        DigitCode = dat.PadLeft(part21, "0"c)
                    Else
                        DigitCode = match.Value
                    End If
                Else
                    DigitCode = match.Value
                End If

                CODE_DATA &= DigitCode



            Next
            CODE_BOTTOM(index) = CODE_DATA
            CODE_NO(index) = TOP_CODE_NO(index)
            Dim LENGTH As Integer = CODE_DATA.Length
            Dim PRN As Integer = Convert.ToInt16(TOP_PROGRAM_ID(index))
            Dim LEN As Integer = LENGTH
            plc.SetDevice("D374", PRN)
            plc.SetDevice("D548", LEN)






        ElseIf SIDE_TYPE(INT) = "BOTTOM" Then

            If Single.TryParse(BOTTOM_XPOSITION(index), floatValueX) Then
                Dim wordsX() As Integer = ConvertFloatToWord(floatValueX)
                plc.SetDevice("D370", wordsX(0))
                plc.SetDevice("D371", wordsX(1))
            End If

            Dim floatValueY As Single
            If Single.TryParse(BOTTOM_YPOSITION(index).ToString(), floatValueY) Then
                Dim wordsY() As Integer = ConvertFloatToWord(floatValueY)
                plc.SetDevice("D372", wordsY(0))
                plc.SetDevice("D373", wordsY(1))
            End If

            If SIDE_TYPE(0) = "TOP" Then

                Dim myArray As Integer() = {1, 2, 3, 4, 5}
                Dim valueToFind As Integer = 3
                Dim index1 As Integer = Array.IndexOf(CODE_NO, BOTTOM_CODE_NO(index))

                If index1 <> -1 Then
                    CODE_DATA = CODE_BOTTOM(index1)
                End If
                Dim LENGTH As Integer = CODE_DATA.Length
                Dim PRN As Integer = Convert.ToInt16(BOTTOM_PROGRAM_ID(index))
                Dim LEN As Integer = LENGTH
                plc.SetDevice("D374", PRN)
                plc.SetDevice("D548", LEN)


                If stopCycle Then Return
            Else


                Dim RED As String = BOTTOM_MARK_CODE(index)

                    Dim matches As MatchCollection = Regex.Matches(RED, PATT)

                For Each match As Match In matches
                    If stopCycle Then Return
                    Dim dd As String = match.Value
                    If (dd.StartsWith("<")) Then

                        If (dd.EndsWith(">")) Then

                            Dim trimmedInput As String = dd.Trim("<"c, ">"c)

                            ' Split the remaining string by the colon ":" delimiter
                            Dim splitData() As String = trimmedInput.Split(":"c)

                            ' Access individual parts of the split data
                            Dim part1 As String = splitData(0) ' "MONTH"
                            Dim part2 As String = splitData(1) ' "5"

                            Dim part3 As String = splitData(2) ' "0"
                            Dim part4 As String = splitData(3) ' "0"
                            Dim part21 As Integer = Convert.ToInt16(part2)
                            Dim part31 As Integer = Convert.ToInt16(part3)
                            Dim part41 As Integer = Convert.ToInt16(part4)

                            Dim dat As String
                            Dim data1 As String
                            Dim DDD1 As Integer
                            ' Display the result in a TextBox or use in any logic
                            If part1 = "YEAR" Then

                                data1 = DateTime.Now.Year

                                ' Display the current year in a TextBox or use it in any logic
                                If part21 < 4 Then
                                    DDD1 = 4 - part21
                                    dat = data1.ToString().Substring(DDD1)
                                Else
                                    dat = data1.ToString()
                                End If




                            ElseIf part1 = "MONTH" Then
                                data1 = DateTime.Now.Month

                                dat = data1.ToString()
                            ElseIf part1 = "DAY" Then
                                data1 = DateTime.Now.Day

                                dat = data1.ToString()

                            ElseIf part1 = "HOUR" Then
                                data1 = DateTime.Now.Hour

                                dat = data1.ToString()
                            ElseIf part1 = "MINUTE" Then
                                data1 = DateTime.Now.Minute

                                dat = data1.ToString()
                            ElseIf part1 = "SEC" Then
                                data1 = DateTime.Now.Second

                                dat = data1.ToString()
                            ElseIf part1 = "COUNTER1" Then

                                CNT1 = Convert.ToInt16(RichTextBox12.Text)
                                CNT1 += 1
                                dat = Convert.ToString(CNT1)

                            ElseIf part1 = "COUNTER2" Then

                                CNT2 = Convert.ToInt16(RichTextBox13.Text)
                                CNT2 += 1
                                dat = Convert.ToString(CNT2)


                            End If



                            DigitCode = dat.PadLeft(part21, "0"c)
                        Else
                            DigitCode = match.Value
                        End If
                    Else
                        DigitCode = match.Value
                    End If

                    CODE_DATA &= DigitCode



                Next
                'CODE_BOTTOM(index) = CODE_DATA
                'CODE_NO(index) = TOP_CODE_NO(index)
                Dim LENGTH As Integer = CODE_DATA.Length
                Dim PRN As Integer = Convert.ToInt16(TOP_PROGRAM_ID(index))
                Dim LEN As Integer = LENGTH
                plc.SetDevice("D374", PRN)
                plc.SetDevice("D548", LEN)

            End If

        End If



        RichTextBox2.Text = CODE_DATA


        Dim arrdata As Integer() = ConvertStringToWord(CODE_DATA)


        ''//plc.WriteDeviceBlock2("D2000", 2, ref arrdata[]);
        Dim baseAddress As Integer = 550
        For i As Integer = 0 To 49
            plc.SetDevice("D" & (baseAddress + i).ToString(), arrdata(i))
        Next

        'TOP_CODE_TYPE(index)



        'TOP_LASER_POWER(index)
        'TOP_SCAN_SPEED(index)
        'TOP_CODE_NO(index)
        'Dim CODE_BOTTOM(5000) As String
        'Dim CODE_NO(5000) As String


    End Sub


    Public Function ConvertStringToWord(input As String) As Integer()


        Dim stringD1(99) As Byte
        Dim stringD As Byte() = Encoding.ASCII.GetBytes(input)
        Array.Copy(stringD, stringD1, Math.Min(stringD.Length, stringD1.Length))

        ' Prepare integer array for each 2-byte segment
        Dim returnValue(49) As Integer
        For i As Integer = 0 To 49
            returnValue(i) = BitConverter.ToInt16(stringD1, i * 2)
        Next

        Return returnValue
    End Function

    Private Async Function ProcessBarcodeCheckAsync(ByVal index As Integer) As Task
        Do
            If stopCycle Then Return
            Await Task.Run(Sub() PauseCheck())
            plc.GetDevice("M224", au_start)
            plc.GetDevice("M300", stop1)
            plc.GetDevice("M353", NXT)

            plc.GetDevice("X13", emg)
            Await barcode_scannerAsync()
            ''Await laser_barcodeAsync()

        Loop Until (NXT = 1 And au_start = 1 And stop1 = 0) ' Define the appropriate exit condition
    End Function
    Private Async Function barcode_scannerAsync() As Task

        Dim startRegister As Integer = 280
        Dim endRegister As Integer = 299
        Dim numRegisters As Integer = endRegister - startRegister + 1


        Dim words(numRegisters - 1) As Integer
        If stopCycle Then Return

        For i As Integer = 0 To numRegisters - 1
            If stopCycle Then Return
            plc.GetDevice("D" & (startRegister + i).ToString(), words(i))
        Next

        ' Create a byte array to hold the combined byte values
        Dim bytes(numRegisters * 2 - 1) As Byte

        ' Convert the 16-bit integers to a byte array
        For i As Integer = 0 To words.Length - 1
            If stopCycle Then Return
            Dim wordBytes() As Byte = BitConverter.GetBytes(words(i))
            bytes(i * 2) = wordBytes(0)
            bytes(i * 2 + 1) = wordBytes(1)
        Next

        ' Convert the byte array to a string
        Dim strValue As String = System.Text.Encoding.ASCII.GetString(bytes)

        ' Display the string in the RichTextBox
        RichTextBox4.Text = strValue.TrimEnd(Chr(0)) ' Remove any trailing null characters

    End Function
    Private Async Function laser_barcodeAsync() As Task
        Dim startRegister As Integer = 401
        Dim endRegister As Integer = 421
        Dim numRegisters As Integer = endRegister - startRegister + 1


        Dim words(numRegisters - 1) As Integer


        For i As Integer = 0 To numRegisters - 1
            If stopCycle Then Return
            plc.GetDevice("D" & (startRegister + i).ToString(), words(i))
        Next

        ' Create a byte array to hold the combined byte values
        Dim bytes(numRegisters * 2 - 1) As Byte

        ' Convert the 16-bit integers to a byte array
        For i As Integer = 0 To words.Length - 1
            If stopCycle Then Return
            Dim wordBytes() As Byte = BitConverter.GetBytes(words(i))
            bytes(i * 2) = wordBytes(0)
            bytes(i * 2 + 1) = wordBytes(1)
        Next

        ' Convert the byte array to a string
        Dim strValue As String = System.Text.Encoding.ASCII.GetString(bytes)

        ' Display the string in the RichTextBox
        RichTextBox2.Text = strValue.TrimEnd(Chr(0)) ' Remove any trailing null characters

    End Function
    Private Async Function ProcessImageAsync() As Task
        IMAGEPATH1 = "" & ConfigurationManager.AppSettings("FiducialImage").ToString().Trim()

        Dim impth As String = IMAGEPATH1 & "123" & ".Png"
        If File.Exists(impth) Then
            Using img As New Bitmap(impth)
                Dim newImg As New Bitmap(PictureBox3.Width, PictureBox3.Height)

                Using g As Graphics = Graphics.FromImage(newImg)
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic
                    g.DrawImage(img, 0, 0, PictureBox3.Width, PictureBox3.Height)
                End Using

                newImg.Save(IMAGE_PATH)
                Await Task.Delay(100)

                PictureBox3.Invoke(Sub() PictureBox3.LoadAsync(IMAGE_PATH))

                Await Task.Delay(50)
            End Using
        End If
    End Function

    Private Sub PauseCheck()
        Do While pauseCycle
            If stopCycle Then Exit Do
            Thread.Sleep(100)
        Loop
    End Sub



    Private Sub WriteToFile(ByVal filePath As String, ByVal content As String)
        Try
            System.IO.File.WriteAllText(filePath, content)
        Catch ex As Exception
            MessageBox.Show("Error writing to file: " & ex.Message)
        End Try
    End Sub

    Private Async Function RunPythonScriptAsync() As Task
        Try
            RunPythonScript()
        Catch ex As Exception
            ' Handle exception
        End Try

        Try
            If pythonProcess IsNot Nothing AndAlso Not pythonProcess.HasExited Then
                pythonProcess.Kill()
                pythonProcess.Dispose()
                pythonProcess = Nothing
            End If
        Catch ex As Exception
            ' Handle exception
        End Try
    End Function


    Private Sub btStop_Click_1(sender As Object, e As EventArgs) Handles btStop.Click
        stopCycle = True
    End Sub
    Private Sub btPass_Click(sender As Object, e As EventArgs) Handles btPass.Click
        pauseCycle = Not pauseCycle ' Toggle pause state
    End Sub

    Private Sub bt_Pcbclamp_Click(sender As Object, e As EventArgs) Handles bt_Pcbclamp.Click

    End Sub

    Private Sub bt_Pcbclamp_MouseUp(sender As Object, e As MouseEventArgs) Handles bt_Pcbclamp.MouseUp
        plc.SetDevice("M248", 0)
    End Sub
    Public Mode_work As Integer
    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub

    'Private Sub CheckBox11_CheckedChanged(sender As Object, e As EventArgs)
    '    Mode_work = Mode_work + 2
    '    If Mode_work = 2 Then
    '        plc.SetDevice("M244", 1)

    '        PASS_CHECK_BOX.BackColor = Color.Green
    '    Else
    '        Mode_work = 0
    '        plc.SetDevice("M244", 0)
    '        PASS_CHECK_BOX.BackColor = Color.White
    '    End If
    'End Sub

    Private Sub CheckBox9_CheckedChanged(sender As Object, e As EventArgs) Handles AUTO_CHECK_BOX.CheckedChanged
        If AUTO_CHECK_BOX.Checked = True Then
            IDLE_CHECK_BOX.Checked = False
            PASS_CHECK_BOX.Checked = False
            AUTO_CHECK_BOX.Enabled = False
            MySettings.Default.AUTO_M = True
            MySettings.Default.IDLE_M = False
            MySettings.Default.PASS_M = False

            MySettings.Default.Save()

        End If
    End Sub

    Private Sub CheckBox10_CheckedChanged(sender As Object, e As EventArgs) Handles IDLE_CHECK_BOX.CheckedChanged
        If IDLE_CHECK_BOX.Checked = True Then
            IDLE_CHECK_BOX.Enabled = False
            PASS_CHECK_BOX.Checked = False
            AUTO_CHECK_BOX.Checked = False
            MySettings.Default.AUTO_M = False
            MySettings.Default.IDLE_M = True
            MySettings.Default.PASS_M = False
            MySettings.Default.Save()
        End If


    End Sub

    Private Sub CheckBox11_CheckedChanged_1(sender As Object, e As EventArgs) Handles PASS_CHECK_BOX.CheckedChanged
        If PASS_CHECK_BOX.Checked = True Then
            IDLE_CHECK_BOX.Checked = False
            PASS_CHECK_BOX.Enabled = False
            AUTO_CHECK_BOX.Checked = False

            MySettings.Default.AUTO_M = False
            MySettings.Default.IDLE_M = False
            MySettings.Default.PASS_M = True
            MySettings.Default.Save()
        End If


    End Sub

    Private Sub RichTextBox3_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox3.TextChanged
        MySettings.Default.TARGET_CNT = RichTextBox3.Text
        MySettings.Default.Save()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MySettings.Default.TARGET_CNT = "0"
        RichTextBox3.Text = "0"
        MySettings.Default.Save()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MySettings.Default.BARCODE_SC = "0"
        RichTextBox6.Text = "0"
        MySettings.Default.Save()
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        MySettings.Default.PANEL_CNT = "0"
        RichTextBox8.Text = "0"
        MySettings.Default.Save()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MySettings.Default.COUNT_0 = "0"
        RichTextBox12.Text = "0"
        MySettings.Default.Save()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        MySettings.Default.COUNT_1 = "0"
        RichTextBox13.Text = "0"
        MySettings.Default.Save()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        MySettings.Default.Good_Count = "0"
        RichTextBox5.Text = "0"
        MySettings.Default.Save()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        MySettings.Default.NG_Count = "0"
        RichTextBox7.Text = "0"
        MySettings.Default.Save()
    End Sub

    Private Sub FID_CHECK_BOX_CheckedChanged(sender As Object, e As EventArgs) Handles FID_CHECK_BOX.CheckedChanged
        If FID_CHECK_BOX.Checked = True Then
            MySettings.Default.fiducial = True
            MySettings.Default.Save()
        Else
            MySettings.Default.fiducial = False
            MySettings.Default.Save()
        End If
    End Sub

    Private Sub Operations_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Home_Page.FidCam1.CloseDevice()
        Home_Page.FidCam1.DestroyDevice()
        Home_Page.LiveCamera1.CloseDevice()
        Home_Page.LiveCamera1.DestroyDevice()
        plc.SetDevice("M247", 0)

        Close_Exe.Main()
    End Sub


    'Private Sub marking()
    '    If RichTextBox21.Text IsNot "" Then
    '        Dim PATT As String = "<[^>]+>|[a-zA-Z]+"
    '        Dim RED As String = RichTextBox21.Text
    '        Dim CODE_DATA As String
    '        Dim matches As MatchCollection = Regex.Matches(RED, PATT)

    '        For Each match As Match In matches
    '            Dim dd As String = match.Value
    '            If (dd.StartsWith("<")) Then

    '                If (dd.EndsWith(">")) Then

    '                    Dim trimmedInput As String = dd.Trim("<"c, ">"c)

    '                    ' Split the remaining string by the colon ":" delimiter
    '                    Dim splitData() As String = trimmedInput.Split(":"c)

    '                    ' Access individual parts of the split data
    '                    Dim part1 As String = splitData(0) ' "MONTH"
    '                    Dim part2 As String = splitData(1) ' "5"

    '                    Dim part3 As String = splitData(2) ' "0"
    '                    Dim part4 As String = splitData(3) ' "0"
    '                    Dim part21 As Integer = Convert.ToInt16(part2)
    '                    Dim part31 As Integer = Convert.ToInt16(part3)
    '                    Dim part41 As Integer = Convert.ToInt16(part4)

    '                    Dim dat As String
    '                    Dim data1 As String
    '                    Dim DDD1 As Integer
    '                    ' Display the result in a TextBox or use in any logic
    '                    If part1 = "YEAR" Then

    '                        data1 = DateTime.Now.Year

    '                        ' Display the current year in a TextBox or use it in any logic
    '                        If part21 < 4 Then
    '                            DDD1 = 4 - part21
    '                            dat = data1.ToString().Substring(DDD1)
    '                        Else
    '                            dat = data1.ToString()
    '                        End If




    '                    ElseIf part1 = "MONTH" Then
    '                        data1 = DateTime.Now.Month

    '                        dat = data1.ToString()
    '                    ElseIf part1 = "DAY" Then
    '                        data1 = DateTime.Now.Day

    '                        dat = data1.ToString()

    '                    ElseIf part1 = "HOUR" Then
    '                        data1 = DateTime.Now.Hour

    '                        dat = data1.ToString()
    '                    ElseIf part1 = "MINUTE" Then
    '                        data1 = DateTime.Now.Minute

    '                        dat = data1.ToString()
    '                    ElseIf part1 = "SEC" Then
    '                        data1 = DateTime.Now.Second

    '                        dat = data1.ToString()
    '                    ElseIf part1 = "COUNTER1" Then
    '                        dat = Convert.ToString(Start_Count.Text)

    '                    ElseIf part1 = "COUNTER2" Then
    '                        dat = Convert.ToString(Start_Count.Text)

    '                    End If



    '                    DigitCode = dat.PadLeft(part21, "0"c)
    '                Else
    '                    DigitCode = match.Value
    '                End If
    '            Else
    '                DigitCode = match.Value
    '            End If

    '            CODE_DATA &= DigitCode
    '        Next

    '        Dim LENGTH As Integer = CODE_DATA.Length






    '        'Dim COD_DATA As String = "uhkuhuunjojn"

    '        '' //float value = float.Parse(ARRAY);
    '        Dim arrdata As Integer() = ConvertStringToWord(CODE_DATA)
    '        Dim SS As Integer

    '        ''//plc.WriteDeviceBlock2("D2000", 2, ref arrdata[]);
    '        Dim baseAddress As Integer = 550

    '        For i As Integer = 0 To 49
    '            plc.SetDevice("D" & (baseAddress + i).ToString(), arrdata(i))
    '        Next

    '        Dim PRN As Integer = Convert.ToInt16(RichTextBox18.Text)
    '        Dim LEN As Integer = Convert.ToInt16(RichTextBox4.Text)
    '        plc.SetDevice("D374", PRN)
    '        plc.SetDevice("D548", LEN)
    '        plc.SetDevice("M207", 1)
    '        Dim DATA As Integer

    '        plc.GetDevice("M207", DATA)

    '        Do Until DATA = 1
    '            plc.SetDevice("M207", 1)
    '            plc.GetDevice("M207", DATA)
    '        Loop


    '    End If

    'End Sub
    'Public Function ConvertStringToWord(input As String) As Integer()


    '    Dim stringD1(99) As Byte
    '    Dim stringD As Byte() = Encoding.ASCII.GetBytes(input)
    '    Array.Copy(stringD, stringD1, Math.Min(stringD.Length, stringD1.Length))

    '    ' Prepare integer array for each 2-byte segment
    '    Dim returnValue(49) As Integer
    '    For i As Integer = 0 To 49
    '        returnValue(i) = BitConverter.ToInt16(stringD1, i * 2)
    '    Next

    '    Return returnValue
    'End Function





End Class