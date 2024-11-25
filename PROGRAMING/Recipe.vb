Imports System.Windows.Forms
Imports System.Net.Mime.MediaTypeNames
Imports ActUtlTypeLib
Imports System.Xml
Imports System.IO
Imports System.Xml.Linq
Imports Guna.UI2.WinForms
Imports System.Configuration
Imports Gui_Tset.RecepieOperation
Imports System.ComponentModel
Imports System.Timers
Imports System.Text
Imports System.Security.Policy
Imports Microsoft.SqlServer
Imports System.Windows
Imports System.ComponentModel.Design
Imports System.Drawing.Drawing2D
Imports System.Xml.Schema
Imports Gui_Tset.My
Imports System.Runtime.InteropServices
Imports MvCamCtrl.NET
Imports Microsoft.NET.Sdk
Imports Emgu.CV
Imports System.Drawing.Imaging
Imports System.Text.RegularExpressions
Imports System.Runtime.Remoting.Lifetime
Imports System.Threading
Imports System.Runtime.InteropServices.ComTypes
Imports Emgu.CV.Features2D
Imports Emgu.CV.ML.KNearest
Imports System.Web.UI.WebControls.Expressions
Imports Emgu.CV.Flann
Imports System.IO.Pipes
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Web.UI.WebControls
Imports TreeNode = System.Windows.Forms.TreeNode
Imports Button = System.Windows.Forms.Button
Imports ComboBox = System.Windows.Forms.ComboBox
Imports TextBox = System.Windows.Forms.TextBox
Imports MvCamCtrl.NET.CameraParams
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button







Public Class Recipe
    Dim POINT As Integer
    Dim LED As Integer
    Dim plc As New ActUtlType
    Public Shared GRPATH As String = String.Empty
    ' Track the active button
    Dim fidpic As PictureBox
    Dim livepic As PictureBox
    Dim livepic1 As PictureBox
    Private isF1Active As Boolean = False
    Private isF2Active As Boolean = False
    Private isDrawing As Boolean = False
    Private startPoint As Point
    Private endPoint As Point
    Private shapes As New List(Of Shape)()

    Private currentID As Integer = 1
    Private currentColor As Color = Color.Blue
    Private drawSquares As Boolean = False
    Dim openFileDialog As New OpenFileDialog()
    Dim m_nBufSizeForDriver As UInt32 = 1000 * 1000 * 3
    Dim m_pBufForDriver(m_nBufSizeForDriver) As Byte
    Dim m_nBufSizeForDriver1 As UInt32 = 1000 * 1000 * 3
    Dim m_pBufForDriver1(m_nBufSizeForDriver1) As Byte
    Dim m_stDeviceInfoList As CCamera.MV_CC_DEVICE_INFO_LIST = New CCamera.MV_CC_DEVICE_INFO_LIST
    Dim m_nDeviceIndex As UInt32
    Dim m_stFrameInfoEx As CCamera.MV_FRAME_OUT_INFO_EX = New CCamera.MV_FRAME_OUT_INFO_EX()
    Private xposs As String
    Private yposs As String
    Private pythonProcess As Process
    Dim DATA() As String
    Dim COUNT_1_SELECT As Boolean
    Dim START_CONT_1 As String
    Dim END_CNT_1 As String


    Dim COUNT_2_SELECT As Boolean
    Dim START_CONT_2 As String
    Dim END_CNT_2 As String

    Private Sub ValidateTextBox(tb As TextBox, ep As ErrorProvider)
        Dim input As String = tb.Text
        Dim result As Double

        ' Only validate if there is input
        If input <> String.Empty Then
            ' Check if the input is a valid non-negative number
            If Not Double.TryParse(input, result) OrElse result < 0 Then
                ep.SetError(tb, "Please enter a valid numeric value.")
            Else
                ep.SetError(tb, String.Empty) ' Clear the error
            End If
        Else
            ep.SetError(tb, String.Empty) ' Clear the error if empty
        End If
    End Sub

    ' Generic validation method for ComboBoxes
    Private Sub ValidateComboBox(comboBox As ComboBox, e As CancelEventArgs)
        If comboBox.SelectedIndex = -1 Then
            ErrorProvider1.SetError(comboBox, "Please select a valid option.")
            e.Cancel = True
        Else
            ErrorProvider1.SetError(comboBox, String.Empty) ' Clear the error
        End If
    End Sub



    Dim ListXMLPath As List(Of String) = New List(Of String)()
    Private Function ToggleButtonColor(button As Button) As Boolean
        Static originalBackColor As New Dictionary(Of Button, Color)

        If originalBackColor.ContainsKey(button) Then
            If button.BackColor = Color.Green Then
                button.BackColor = originalBackColor(button)
                Return False
            Else
                button.BackColor = Color.Green
                Return True
            End If
        Else
            originalBackColor.Add(button, button.BackColor)
            button.BackColor = Color.Green
            Return True
        End If
    End Function

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

        System.Array.Copy(lowWordBytes, 0, bytes, 0, 2)
        System.Array.Copy(highWordBytes, 0, bytes, 2, 2)

        Return BitConverter.ToSingle(bytes, 0)
    End Function
    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs)

    End Sub
    Private WithEvents m251Timer As System.Timers.Timer
    Dim isMonitoringM251 As Boolean = False
    Dim previousM251State As Integer = 0

    Public Async Function plccon() As Task
        plc.ActLogicalStationNumber = 1
        plc.Open()
        Timer1.Start()
    End Function
    Private Sub Recipe_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Guna2CircleButton1.BackColor = normal
        plccon()
        'PictureBox1.BackColor = Color.Red
        ' PictureBox2.BackColor = Color.Red


        rtxtcurrentpg.Text = My.Settings.ProgramName

        livepic = PictureBox8
        livepic1 = PictureBox3


        DATAGRID.MultiSelect = False
        DATAGRID.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Timer2.Start()
        Timer3.Start()
        Dim nRet As Int32
        AddHandler rtxtcurrentpg.TextChanged, AddressOf rtxtcurrentpg_TextChanged
        Panel27.Hide()
        Panel26.Hide()
        'FID_CAMERA.Enabled = True
        LIVE_CAM.Checked = False
        ' FID_1.Enabled = False
        ''FID_1.Checked = True
        live_1.Checked = False
        FID_CAMERA.Checked = True
        nRet = Home_Page.FidCam1.SetEnumValue("TriggerSource", CCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE)
        nRet = Home_Page.FidCam1.SetEnumValue("TriggerMode", CCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON)


        'datagrdFid.Rows.Add("TOP", "Circle", "0", "0", "0", "0", "0", "0", "0", "0", "0",
        '                     "0", "FID1")
        'datagrdFid.Rows.Add("TOP", "Circle", "0", "0", "0", "0", "0", "0", "0", "0", "0",
        '                     "0", "FID2")
        'datagrdFid.Rows.Add("BOTTOM", "Circle", "0", "0", "0", "0", "0", "0", "0", "0", "0",
        '                     "0", "FID1")
        'datagrdFid.Rows.Add("BOTTOM", "Circle", "0", "0", "0", "0", "0", "0", "0", "0", "0",
        '                     "0", "FID2")

        LoadReceipe()
        loadData()



    End Sub



    Private Sub btn_Widthadj_Click(sender As Object, e As EventArgs)
        ToggleButtonColor(btn_Widthadj)
        Dim floatValue As Single
        If Single.TryParse(Panel_Width.Text, floatValue) Then
            ' Round the float value to two decimal places
            Dim roundedValue As Single = Math.Round(floatValue, 3)

            ' Convert the rounded float value to two 16-bit integers
            Dim words() As Integer = ConvertFloatToWord(roundedValue)

            ' Example: Write to different data registers
            plc.SetDevice("D320", words(0))
            plc.SetDevice("D321", words(1))
        Else

        End If
    End Sub



    Private Sub btLoadpos_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M252", 0)
    End Sub

    Private Sub btLoadpos_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M252", 1)
    End Sub

    Private Sub btUnloadpos_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M232", 0)

    End Sub

    Private Sub btUnloadpos_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M232", 1)

    End Sub










    Private Sub btPcbstopper_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M237", 0)
    End Sub

    Private Sub btPcbstopper_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M237", 1)
    End Sub

    Private Sub btServoon_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M238", 0)
    End Sub

    Private Sub btServoon_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M238", 1)
    End Sub

    Private Sub btPcbclamp_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M239", 0)
    End Sub

    Private Sub btPcbclamp_MouseDown(sender As Object, e As MouseEventArgs) Handles CLAMP_WEDENING.MouseDown
        plc.SetDevice("M239", 1)
    End Sub

    Private Sub bt_Move_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub bt_Gateopenl_MouseDown(sender As Object, e As MouseEventArgs) Handles LEFT_SHUTTER.MouseDown
        plc.SetDevice("M235", 1)
    End Sub



    Private Sub bt_Gateopenl_MouseUp(sender As Object, e As MouseEventArgs) Handles LEFT_SHUTTER.MouseUp
        plc.SetDevice("M235", 0)
    End Sub



    Private Sub bt_Gateopenr_MouseDown(sender As Object, e As MouseEventArgs) Handles RIGHT_SHUTTER.MouseDown
        plc.SetDevice("M236", 1)


    End Sub

    Private Sub bt_Gateopenr_MouseUp(sender As Object, e As MouseEventArgs) Handles RIGHT_SHUTTER.MouseUp
        plc.SetDevice("M236", 0)

    End Sub

    Private Sub bt_Pcbload_MouseDown(sender As Object, e As MouseEventArgs) Handles PACB_LOAD.MouseDown
        plc.SetDevice("M234", 1)

    End Sub

    Private Sub bt_Pcbload_MouseUp(sender As Object, e As MouseEventArgs) Handles PACB_LOAD.MouseUp
        plc.SetDevice("M234", 0)
    End Sub



    Private Sub bt_Unclamp_MouseDown(sender As Object, e As MouseEventArgs) Handles CLAMP_UNCLAMP.MouseDown
        plc.SetDevice("M248", 1)

    End Sub

    Private Sub bt_Pcbunclamp_MouseUp(sender As Object, e As MouseEventArgs) Handles CLAMP_UNCLAMP.MouseUp
        plc.SetDevice("M248", 0)
    End Sub



    Private Sub bt_Teachpos_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M244", 1)
    End Sub

    Private Sub bt_Teachpos_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M244", 0)
    End Sub

    Private Sub bt_Move_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M243", 1)
    End Sub

    Private Sub bt_Move_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M243", 0)
    End Sub

    Private Sub bt_Learnshape_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M245", 0)
    End Sub

    Private Sub bt_Learnshape_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M245", 1)
    End Sub

    Private Sub bt_Save_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M246", 1)
    End Sub

    Private Sub bt_Save_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M246", 1)
    End Sub

    Private Sub bt_Test_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M247", 1)
    End Sub

    Private Sub bt_Test_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M247", 0)
    End Sub

    Private Sub RadioButton3_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M238", 1)
        ' PictureBox1.BackColor = Color.Green

    End Sub

    Private Sub btservo_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M238", 0)
        ' PictureBox1.BackColor = Color.Red
    End Sub

    Private Sub btn_Widthadj_MouseUp(sender As Object, e As MouseEventArgs) Handles CONV_WIDTH_ADJUST.MouseUp
        plc.SetDevice("M240", 0)
    End Sub

    Private Sub btn_Widthadj_MouseDown(sender As Object, e As MouseEventArgs) Handles CONV_WIDTH_ADJUST.MouseDown
        plc.SetDevice("M240", 1)
    End Sub

    Private Sub btn_Trackmov_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M241", 1)
    End Sub

    Private Sub btn_Trackmov_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M240", 0)
    End Sub

    Private Sub btn_Array_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M242", 1)
    End Sub

    Private Sub btn_Array_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M242", 1)
    End Sub

    Private Sub HOMEPOS_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M231", 1)
        'PictureBox2.BackColor = Color.Green
    End Sub

    Private Sub HOMEPOS_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M231", 1)
        ' PictureBox2.BackColor = Color.Red
    End Sub



    Private Sub txt_p_wed_TextChanged(sender As Object, e As EventArgs)
        Validatetxt_p_wed()
    End Sub





    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        datagrdFid.Rows.Clear()

        Dim progname As String
        progname = InputBox("PLEASE ENTER PROGRAM NAME", MessageBoxButtons.OKCancel)

        Dim currentDirectory As String = Directory.GetCurrentDirectory()

        If progname = "" Then
            Return

        ElseIf (progname IsNot "") Then
            '''''''''recipe path

            Dim RecipePath As String = "" & ConfigurationManager.AppSettings("ReceipeFilepath").ToString().Trim()


            Try
                If Not Directory.Exists(RecipePath) Then
                    Directory.CreateDirectory(RecipePath)
                    'MessageBox.Show("Folder created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    'MessageBox.Show("Folder already exists.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show("An error occurred while creating the folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            '''''''' backup path
            Dim BackupPath As String = "" & ConfigurationManager.AppSettings("BackupReceipeFilepath").ToString().Trim()

            Try
                If Not Directory.Exists(BackupPath) Then
                    Directory.CreateDirectory(BackupPath)
                    'MessageBox.Show("Folder created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    'MessageBox.Show("Folder already exists.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show("An error occurred while creating the folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try



            ''''' fid temp path 
            Dim FidTempPath As String = "" & ConfigurationManager.AppSettings("FiducialTemplate").ToString().Trim()

            Try
                If Not Directory.Exists(FidTempPath) Then
                    Directory.CreateDirectory(FidTempPath)
                    'MessageBox.Show("Folder created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    'MessageBox.Show("Folder already exists.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show("An error occurred while creating the folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try


            '''''' fid image path
            Dim FidImagePath As String = "" & ConfigurationManager.AppSettings("FiducialImage").ToString().Trim()

            Try
                If Not Directory.Exists(FidImagePath) Then
                    Directory.CreateDirectory(FidImagePath)
                    'MessageBox.Show("Folder created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    'MessageBox.Show("Folder already exists.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show("An error occurred while creating the folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try


            ReceipeFileName = progname

            Dim generatedFile As String

            Dim generatedFile1 As String = RecipePath & ReceipeFileName & ".xml"
            Dim BaackupgeneratedFile As String = BackupPath & ReceipeFileName & ".xml"
            Dim nam As String = ReceipeFileName & ".xml"
            If Not File.Exists(generatedFile1) Then
                generatedFile = RecipePath & ReceipeFileName & ".xml"

            Else
                Dim _LaserHeadLocation As String() = Directory.GetFiles(RecipePath, nam, System.IO.SearchOption.AllDirectories)
                Dim count As Integer = _LaserHeadLocation.Count()
                ReceipeFileName = progname & "_" & count + 1
                generatedFile = RecipePath & ReceipeFileName & ".xml"
            End If

            Dim Xoffset As String
            Dim Yoffset As String
            Dim Xcount As String
            Dim Ycount As String
            Dim Xpitch As String
            Dim Ypitch As String
            Dim P_length As String
            Dim P_Width As String
            Dim p_Tick As String
            Dim Start_cont As String
            Dim End_cont As String
            Dim Start_cont1 As String
            Dim End_cont1 As String
            Dim COUNT_SELE_1 As Boolean
            Dim COUNT_SELE_2 As Boolean
            If X_offset.Text = "" Then
                Xoffset = "0.0"
            Else
                Xoffset = X_offset.Text
            End If
            If Y_offset.Text = "" Then
                Yoffset = "0.0"
            Else
                Yoffset = Y_offset.Text
            End If
            If X_Pitch_Count.Text = "" Then
                Xcount = "0.0"
            Else
                Xcount = X_Pitch_Count.Text
            End If
            If Y_Pitch_Count.Text = "" Then
                Ycount = "0"
            Else
                Ycount = Y_Pitch_Count.Text
            End If
            If X_Pitch.Text = "" Then
                Xpitch = "0"
            Else
                Xpitch = X_Pitch.Text
            End If
            If Y_Pitch.Text = "" Then
                Ypitch = "0.0"
            Else
                Ypitch = Y_Pitch.Text
            End If
            If Panel_Length.Text = "" Then
                P_length = "0.0"
            Else
                P_length = Panel_Length.Text
            End If
            If Panel_Width.Text = "" Then
                P_Width = "0.0"
            Else
                P_Width = Panel_Width.Text
            End If
            If Panel_Thick.Text = "" Then
                p_Tick = "0.0"
            Else
                p_Tick = Panel_Thick.Text
            End If


            If Counter2.Checked = True Then
                COUNT_SELE_2 = True
                COUNT_SELE_1 = False
            Else
                COUNT_SELE_1 = True
                COUNT_SELE_2 = False
            End If

            If Counter1.Checked = True Then

                If Start_Count.Text = "" Then
                    Start_cont = "1"
                Else
                    Start_cont = Start_Count.Text

                End If
                If End_Count.Text = "" Then
                    End_cont = "9999"
                Else
                    End_cont = End_Count.Text
                End If
            Else
                Start_cont = "1"
                End_cont = "9999"
            End If
            If Counter2.Checked = True Then
                If Start_Count.Text = "" Then
                    Start_cont1 = "1"
                Else
                    Start_cont1 = Start_Count.Text

                End If
                If End_Count.Text = "" Then
                    End_cont1 = "9999"
                Else
                    End_cont1 = End_Count.Text
                End If
            Else
                Start_cont1 = "1"

                End_cont1 = "9999"

            End If


            Try


                Dim xdoc As XDocument = XDocument.Parse("<JOBList></JOBList>")

                ' Create the <JOB> element with nested structure
                Dim contacts As XElement = New XElement("JOB",
                                        New XElement("TAGTYPE",
                                            New XAttribute("NAME", "RECEIPE"), ' Receipe name attribute
                                            New XElement("RECEIPE", New XAttribute("NO", "0"),
                                                New XElement("BOARD",
                                                    New XElement("BOARDNAME", "xxxxxxxxxx"),
                                                    New XElement("X_OFFSET", Xoffset),
                                                    New XElement("Y_OFFSET", Yoffset),
                                                    New XElement("X_COUNT", Xcount),
                                                    New XElement("Y_COUNT", Ycount),
                                                    New XElement("X_PITCH", Xpitch),
                                                    New XElement("Y_PITCH", Ypitch),
                                                    New XElement("PANEL_LENGTH", P_length),
                                                    New XElement("PANEL_WIDTH", P_Width),
                                                    New XElement("PANEL_THICKNESS", p_Tick)),
                                                New XElement("COUNTER_1",
                                                    New XElement("COUNTER_1", COUNT_SELE_1),
                                                    New XElement("START_COUNT", Start_cont),
                                                    New XElement("END_COUNT", End_cont)),
                                                New XElement("COUNTER_2",
                                                    New XElement("COUNTER_2", COUNT_SELE_2),
                                                    New XElement("START_COUNT", Start_cont1),
                                                    New XElement("END_COUNT", End_cont1)),
                                 New XElement("MARKPOSITION"),
                                 New XElement("FIDUCIAL")),
                New XElement("LASTSAVINGTIME", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")),
                                 New XElement("RECEIPENAME", New XAttribute("NAME", "RECIEPE"),
                                                    New XElement("RNAME", ReceipeFileName),
                                                    New XElement("RPATH", generatedFile))))

                ' Add the created JOB element to the root of the document
                xdoc.Root.Add(contacts)

                ' Save the XML to the specified files
                xdoc.Save(generatedFile)
                xdoc.Save(BaackupgeneratedFile)

                datagrdFid.Rows.Add("TOP", "Circle", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                             "0", "FID1")
                datagrdFid.Rows.Add("TOP", "Circle", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                             "0", "FID2")
                datagrdFid.Rows.Add("BOTTOM", "Circle", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                             "0", "FID1")
                datagrdFid.Rows.Add("BOTTOM", "Circle", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                             "0", "FID2")

                Dim xmDocument As XmlDocument = New XmlDocument()
                xmDocument.Load(generatedFile)
                Dim MARKPOSITION As XmlNode = xmDocument.SelectSingleNode("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL")

                ' Clear existing child nodes
                MARKPOSITION.RemoveAll()

                Dim DATA(20) As String
                Dim BTMFID As XmlElement = xmDocument.CreateElement("BOTTOM_FID")
                If CheckBox1.Checked Then
                    BTMFID.InnerText = "1"
                Else
                    BTMFID.InnerText = "0"
                End If
                MARKPOSITION.AppendChild(BTMFID)

                ' Iterate through DataGridView rows and add elements based on row data
                For Each row As DataGridViewRow In datagrdFid.Rows
                    If Not row.IsNewRow Then
                        ' Create the main position element
                        Dim POSI As XmlElement = xmDocument.CreateElement("F")
                        MARKPOSITION.AppendChild(POSI)

                        ' Add SIDE attribute
                        Dim SIDE As XmlAttribute = xmDocument.CreateAttribute("SIDE")
                        SIDE.InnerText = If(row.Index < 2, "TOP", "BOTTOM")
                        POSI.Attributes.Append(SIDE)



                        ' Fill DATA array with cell values from the current row
                        For i As Integer = 1 To 12
                            DATA(i) = If(row.Cells(i).Value IsNot Nothing, row.Cells(i).Value.ToString(), "")
                        Next

                        ' Create and append XML elements with data from DATA array
                        Dim SHAPE As XmlElement = xmDocument.CreateElement("SHAPES")
                        SHAPE.InnerText = DATA(1)
                        POSI.AppendChild(SHAPE)

                        Dim F_X1 As XmlElement = xmDocument.CreateElement("F_X1")
                        F_X1.InnerText = DATA(2)
                        POSI.AppendChild(F_X1)

                        Dim F_Y1 As XmlElement = xmDocument.CreateElement("F_Y1")
                        F_Y1.InnerText = DATA(3)
                        POSI.AppendChild(F_Y1)

                        Dim F_RX2 As XmlElement = xmDocument.CreateElement("F_RX2")
                        F_RX2.InnerText = DATA(4)
                        POSI.AppendChild(F_RX2)

                        Dim F_RY2 As XmlElement = xmDocument.CreateElement("F_RY2")
                        F_RY2.InnerText = DATA(5)
                        POSI.AppendChild(F_RY2)

                        Dim F_CP As XmlElement = xmDocument.CreateElement("F_CP")
                        F_CP.InnerText = DATA(6)
                        POSI.AppendChild(F_CP)

                        Dim X_OFF As XmlElement = xmDocument.CreateElement("X_OFF")
                        X_OFF.InnerText = DATA(7)
                        POSI.AppendChild(X_OFF)

                        Dim Y_OFF As XmlElement = xmDocument.CreateElement("Y_OFF")
                        Y_OFF.InnerText = DATA(8)
                        POSI.AppendChild(Y_OFF)

                        Dim F_PX As XmlElement = xmDocument.CreateElement("F_PX")
                        F_PX.InnerText = DATA(9)
                        POSI.AppendChild(F_PX)

                        Dim F_PY As XmlElement = xmDocument.CreateElement("F_PY")
                        F_PY.InnerText = DATA(10)
                        POSI.AppendChild(F_PY)

                        Dim SCOR As XmlElement = xmDocument.CreateElement("F_SCORE")
                        SCOR.InnerText = DATA(11)
                        POSI.AppendChild(SCOR)
                        Dim FIDNO As XmlElement = xmDocument.CreateElement("FID_TYPE")
                        FIDNO.InnerText = DATA(12)
                        POSI.AppendChild(FIDNO)


                    End If
                Next

                ' Save the modified XML document
                xmDocument.Save(generatedFile)
                xmDocument.Save(BaackupgeneratedFile)

                LoadReceipe()


            Catch ex As Exception



                Dim Lfname As String = "" & ConfigurationManager.AppSettings("Success_Logs").ToString().Trim()



                Dim Lpath As String = Lfname
                Dim LLogdir As String = "" & Lfname

                Dim LReceipeFileName As String = String.Empty
                If Not Directory.Exists(LLogdir) Then
                    Directory.CreateDirectory(LLogdir)
                End If




                Dim LgeneratedFile1 As String = LLogdir & ReceipeFileName & ".xml"


                Dim xdoc1 = XDocument.Parse("<Error_Logs></Error_Logs>")
                Dim contacts1 As XElement = New XElement("Error",
                                                            New XElement("FileName", generatedFile),
                                                            New XElement("DateTime", DateTime.Now().ToString()),
                                                            New XElement("UserName", ""),
                                                             New XElement("PageName", "Recipe"),
                                                            New XElement("Path", generatedFile),
                                                            New XElement("Message", "Recipe added successfully"))
                xdoc1.Root.Add(contacts1)
                xdoc1.Save(LgeneratedFile1)


                MessageBox.Show("An error occurred while saving the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub LoadReceipe()

        'load xml and read xml and display on grid view
        Dim dtRecipetable As DataTable = New DataTable()
        dtRecipetable.Clear()
        dtRecipetable.Clone()
        dtRecipetable.TableName = "dtRecipetable"
        dtRecipetable.Columns.Add("S_NO", GetType(Int32))
        dtRecipetable.Columns.Add("pname", GetType(String))
        dtRecipetable.Columns.Add("cwidht", GetType(String))
        dtRecipetable.Columns.Add("date_time", GetType(String))
        dtRecipetable.Columns.Add("RPATH", GetType(String))

        Dim countRecipe As Integer = 0

        Dim ReceipeFilepath As String = "" & ConfigurationManager.AppSettings("ReceipeFilepath").ToString().Trim()
        Dim _PrgLogPath As String() = Directory.GetFiles(ReceipeFilepath, "*.xml", System.IO.SearchOption.AllDirectories)
        Dim recipename As String = String.Empty
        Dim RPATH As String = String.Empty
        Dim LASTSAVINGTIME As String = String.Empty
        Dim ITEMCOUNT As Int32 = _PrgLogPath.Count()
        If ITEMCOUNT > 0 Then
            For Each item In _PrgLogPath
                Dim fs As FileStream = New FileStream(item, FileMode.Open)
                Dim sr As StreamReader = New StreamReader(fs)
                Dim s As String = sr.ReadToEnd()
                sr.Close()
                fs.Close()
                Dim xmDocument As XmlDocument = New XmlDocument()
                xmDocument.Load(item)
                Dim selectedoperationnodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/LASTSAVINGTIME")
                Dim RECEIPENAMEnodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPENAME")
                Dim BOARDnodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/BOARD")






                For Each _RECEIPENAMEnodes As XmlNode In RECEIPENAMEnodes
                    recipename = _RECEIPENAMEnodes.ChildNodes(0).InnerText
                    RPATH = _RECEIPENAMEnodes.ChildNodes(1).InnerText
                Next

                countRecipe = countRecipe + 1



                Dim P_Lenghth As String = String.Empty
                Dim P_with As String = String.Empty
                Dim P_thik As String = String.Empty
                For Each BOARD As XmlNode In BOARDnodes

                    P_with = BOARD.ChildNodes(8).InnerText



                Next

                For Each FDATE As XmlNode In selectedoperationnodes
                    LASTSAVINGTIME = FDATE.ChildNodes(0).InnerText

                Next


                dtRecipetable.Rows.Add(countRecipe, recipename, P_with, LASTSAVINGTIME, RPATH)
                DATAGRID.DataSource = dtRecipetable

            Next





        Else
            dtRecipetable.Clear()

            DATAGRID.DataSource = dtRecipetable

        End If

    End Sub






    Private Sub txt_p_len_TextChanged(sender As Object, e As EventArgs)
        ValidateTxt_p_len()
    End Sub
    Public Function GetSideName(ByVal val As String) As String
        Dim name As String = String.Empty

        Select Case val
            Case "1"
                name = "TOP"
                Return name
            Case "2"
                name = "BOT"
                Return name
        End Select

        Return name
    End Function
    Public Function GetShapeName(ByVal val As String) As String
        Dim name As String = String.Empty

        Select Case val
            Case "0"
                name = "Circle"
                Return name
            Case "1"
                name = "Rectangle"
                Return name
            Case "2"
                name = "Genric"
                Return name
        End Select

        Return name
    End Function
    Public Function GetColorName(ByVal val As String) As String
        Dim name As String = String.Empty

        Select Case val
            Case "0"
                name = "RED"
                Return name
            Case "1"
                name = "BLUE"
                Return name
            Case "2"
                name = "GREEN"
                Return name
        End Select

        Return name
    End Function

    'Private Sub Button5_Click_1(sender As Object, e As EventArgs)

    '    Try

    '        If ListXMLPath.Count > 0 Then

    '            If Not String.IsNullOrEmpty(GRPATH) Then
    '                Dim path As String = GRPATH
    '                Dim Panel As New Panel
    '                Panel.Setb = path
    '                Panel.Show()

    '                ' create success or process logs



    '            Else
    '                MessageBox.Show("Please select correct recipe!")
    '            End If
    '        Else
    '            MessageBox.Show("Please select correct recipe!")
    '        End If

    '    Catch ex As Exception


    '        '




    '    End Try



    'End Sub
    Private isCellClicked As Boolean = False
    Private Sub DATAGRID_CellClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DATAGRID.CellClick
        shapes.Clear()
        datagrdFid.Rows.Clear()
        If e.RowIndex >= 0 Then
            isCellClicked = True

            ' Deselect any currently selected rows
            For Each row As DataGridViewRow In DATAGRID.SelectedRows
                row.DefaultCellStyle.Font = DATAGRID.DefaultCellStyle.Font
            Next

            ' Select the new row
            Dim selectedRow As DataGridViewRow = DATAGRID.Rows(e.RowIndex)
            selectedRow.Selected = True
            Dim style As New DataGridViewCellStyle
            style.BackColor = Color.LightGray
            style.Font = New System.Drawing.Font("Tahoma", 8)
            selectedRow.DefaultCellStyle = style
            DATAGRID.MultiSelect = False
            DATAGRID.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            If DATAGRID.Rows.Count > 0 AndAlso DATAGRID.Rows(0) IsNot Nothing Then





                Dim _RecepieOperation As RecepieOperation = New RecepieOperation()
                If e.RowIndex >= 0 Then
                    Dim row As DataGridViewRow = Me.DATAGRID.Rows(e.RowIndex)
                    Dim filename As String = row.Cells("RPATH").Value.ToString()
                    GRPATH = row.Cells("RPATH").Value.ToString()
                    'Dim row As DataGridViewRow = Me.DataGridView1.Rows(e.RowIndex)
                    row.DefaultCellStyle.BackColor = Color.LightGray
                    row.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8)

                    ' Adding array for delete path
                    ListXMLPath.Add(GRPATH)

                    ' Add file name into panel name textbox
                    Dim pname As String = Path.GetFileNameWithoutExtension(GRPATH)
                    txt_Sel_Prog_name.Text = pname
                    'rtxtcurrentpg.Text = pname





                    ' Dim FileName As String = Path.GetFileNameWithoutExtension(item)
                    Dim fs As FileStream = New FileStream(filename, FileMode.Open)
                    Dim sr As StreamReader = New StreamReader(fs)
                    Dim s As String = sr.ReadToEnd()
                    sr.Close()
                    fs.Close()

                    Dim xmDocument1 As XDocument = XDocument.Load(filename)
                    Dim xmDocument As XmlDocument = New XmlDocument()
                    xmDocument.Load(filename)
                    'DataGridView1.Rows.Clear()

                    '' Iterate through the XML elements and populate the DataGridView
                    'For Each rowElement As XmlNode In xmDocument.SelectNodes("//Row")
                    '    Dim newRow As DataGridViewRow = DataGridView1.Rows(DataGridView1.Rows.Add())
                    '    Dim cellIndex As Integer = 0
                    '    For Each cellElement As XmlNode In rowElement.ChildNodes
                    '        newRow.Cells(cellIndex).Value = cellElement.InnerText
                    '        cellIndex += 1
                    '    Next
                    'Next

                    Dim Boardnodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/BOARD")

                    For Each Feeder As XmlNode In Boardnodes
                        Dim BOARDNAME As String = Feeder.ChildNodes(0).InnerText
                        Dim XOFFSET As String = Feeder.ChildNodes(1).InnerText
                        Dim YOFFSET As String = Feeder.ChildNodes(2).InnerText
                        Dim XCOUNT As String = Feeder.ChildNodes(3).InnerText
                        Dim YCOUNT As String = Feeder.ChildNodes(4).InnerText
                        Dim XPITCH As String = Feeder.ChildNodes(5).InnerText
                        Dim YPITCH As String = Feeder.ChildNodes(6).InnerText
                        Dim PAN_LENGTH As String = Feeder.ChildNodes(7).InnerText
                        Dim PAN_WIDTH As String = Feeder.ChildNodes(8).InnerText
                        Dim PANEL_THICKNESS As String = Feeder.ChildNodes(9).InnerText



                        X_offset.Text = XOFFSET
                        Y_offset.Text = YOFFSET
                        X_Pitch_Count.Text = XCOUNT
                        Y_Pitch_Count.Text = YCOUNT
                        X_Pitch.Text = XPITCH
                        Y_Pitch.Text = YPITCH

                        Panel_Length.Text = PAN_LENGTH
                        Panel_Width.Text = PAN_WIDTH
                        Panel_Thick.Text = PANEL_THICKNESS
                    Next

                    LOADPOSISATA(filename)



                    Dim COUNTERs_1 As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/COUNTER_1")

                    For Each _PCNT As XmlNode In COUNTERs_1

                        COUNT_1_SELECT = _PCNT.ChildNodes(0).InnerText
                        START_CONT_1 = _PCNT.ChildNodes(1).InnerText

                        END_CNT_1 = _PCNT.ChildNodes(2).InnerText




                    Next



                    Dim COUNTERs_2 As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/COUNTER_2")
                    For Each _CNT2 As XmlNode In COUNTERs_2



                        COUNT_2_SELECT = _CNT2.ChildNodes(0).InnerText
                        START_CONT_2 = _CNT2.ChildNodes(1).InnerText

                        END_CNT_2 = _CNT2.ChildNodes(2).InnerText

                    Next

                    If COUNT_1_SELECT = True Then
                        Counter1.Checked = True

                        Start_Count.Text = START_CONT_1
                        End_Count.Text = END_CNT_1
                    ElseIf COUNT_2_SELECT = True Then

                        Counter2.Checked = True
                        Start_Count.Text = START_CONT_2
                        End_Count.Text = END_CNT_2
                    End If


                    'Dim BTMFID As XmlElement = xmDocument.CreateElement("BOTTOM_FID")
                    'If CheckBox1.Checked Then
                    '    BTMFID.InnerText = "1"
                    'Else
                    '    BTMFID.InnerText = "0"
                    'End If
                    'MARKPOSITION.AppendChild(BTMFID)
                    Dim RECEIPENAMEnodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/RECEIPENAME")
                    For Each _PLCTAGnodes As XmlNode In RECEIPENAMEnodes
                        Dim RNAME As String = _PLCTAGnodes.ChildNodes(0).InnerText
                        Dim RPATH As String = _PLCTAGnodes.ChildNodes(1).InnerText

                    Next


                    Dim FIDUCIAL As XmlNode = xmDocument.SelectSingleNode("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL")
                    'Dim FNodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL/F")
                    datagrdFid.Rows.Clear()
                    shapes.Clear()

                    Dim BBT As String = FIDUCIAL.ChildNodes(0).InnerText
                    If BBT = "1" Then
                            CheckBox1.Checked = True
                        Else
                            CheckBox1.Checked = False


                        End If


                    Dim MARKPOSITION As XmlNode = xmDocument.SelectSingleNode("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL")

                    Dim fiducials As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL/F")


                    ' Iterate through each <F> node
                    For Each fidNode As XmlNode In fiducials
                        ' Read values from each child element within <F>
                        Dim side As String = fidNode.Attributes("SIDE")?.InnerText
                        Dim shape As String = fidNode("SHAPES")?.InnerText
                        Dim fX1 As String = fidNode("F_X1")?.InnerText
                        Dim fY1 As String = fidNode("F_Y1")?.InnerText
                        Dim fRX2 As String = fidNode("F_RX2")?.InnerText
                        Dim fRY2 As String = fidNode("F_RY2")?.InnerText
                        Dim fCP As String = fidNode("F_CP")?.InnerText
                        Dim xOff As String = fidNode("X_OFF")?.InnerText
                        Dim yOff As String = fidNode("Y_OFF")?.InnerText
                        Dim fPX As String = fidNode("F_PX")?.InnerText
                        Dim fPY As String = fidNode("F_PY")?.InnerText
                        Dim fScore As String = fidNode("F_SCORE")?.InnerText
                        Dim fidNo As String = fidNode("FID_TYPE")?.InnerText

                        ' Add a new row to the DataGridView with the values from XML
                        Dim rowIndex1 As Integer = datagrdFid.Rows.Add()
                        currentID = rowIndex1 + 1

                        If shape = "Square" Then
                            ' Draw square as before
                            Dim size As New Size(Convert.ToInt16(fRX2), Convert.ToInt16(fRY2))
                            Dim topLeft As New Point(Convert.ToInt16(fX1), Convert.ToInt16(fY1))
                            shapes.Add(New Square(topLeft, size, currentID, currentColor))

                        ElseIf ElseIfshape = "Circle" Then
                            ' Draw circle with dynamic size

                            Dim parts() As String = fCP.Trim("()").Split(","c)


                            Dim trimmedPart() As String = parts(1).Split(")"c)
                            Dim centrx As String = parts(0)
                            Dim centey As String = trimmedPart(0)
                            Dim radius As Integer = CInt(Convert.ToInt16(fRX2))
                            Dim centerX As Integer = Convert.ToInt16(centrx)
                            Dim centerY As Integer = Convert.ToInt16(centey)
                            shapes.Add(New Circle(New Point(centerX, centerY), radius / 2, currentID, currentColor))


                        End If


                        datagrdFid.Rows(rowIndex1).Cells(0).Value = side
                        datagrdFid.Rows(rowIndex1).Cells(1).Value = shape
                        datagrdFid.Rows(rowIndex1).Cells(2).Value = fX1
                        datagrdFid.Rows(rowIndex1).Cells(3).Value = fY1
                        datagrdFid.Rows(rowIndex1).Cells(4).Value = fRX2
                        datagrdFid.Rows(rowIndex1).Cells(5).Value = fRY2
                        datagrdFid.Rows(rowIndex1).Cells(6).Value = fCP
                        datagrdFid.Rows(rowIndex1).Cells(7).Value = xOff
                        datagrdFid.Rows(rowIndex1).Cells(8).Value = yOff
                        datagrdFid.Rows(rowIndex1).Cells(9).Value = fPX
                        datagrdFid.Rows(rowIndex1).Cells(10).Value = fPY
                        datagrdFid.Rows(rowIndex1).Cells(11).Value = fScore
                        datagrdFid.Rows(rowIndex1).Cells(12).Value = fidNo


                    Next




                    'For Each Node1 As XmlNode In FNodes

                    '    Dim VALUE(20) As String
                    '    VALUE(0) = Node1.ChildNodes(0).InnerText
                    '    VALUE(1) = Node1.ChildNodes(1).InnerText
                    '    VALUE(2) = Node1.ChildNodes(2).InnerText
                    '    VALUE(3) = Node1.ChildNodes(3).InnerText
                    '    VALUE(4) = Node1.ChildNodes(4).InnerText
                    '    VALUE(5) = Node1.ChildNodes(5).InnerText
                    '    VALUE(6) = Node1.ChildNodes(6).InnerText
                    '    VALUE(7) = Node1.ChildNodes(7).InnerText
                    '    VALUE(8) = Node1.ChildNodes(8).InnerText
                    '    VALUE(9) = Node1.ChildNodes(9).InnerText
                    '    VALUE(10) = Node1.ChildNodes(10).InnerText
                    '    VALUE(11) = Node1.ChildNodes(11).InnerText
                    '    VALUE(12) = Node1.ChildNodes(12).InnerText


                    '    Dim rowIndex1 As Integer = datagrdFid.Rows.Add()



                    '    datagrdFid.Rows(rowIndex1).Cells(0).Value = VALUE(0)
                    '    datagrdFid.Rows(rowIndex1).Cells(1).Value = VALUE(1)
                    '    datagrdFid.Rows(rowIndex1).Cells(2).Value = VALUE(2)
                    '    datagrdFid.Rows(rowIndex1).Cells(3).Value = VALUE(3)
                    '    datagrdFid.Rows(rowIndex1).Cells(4).Value = VALUE(4)
                    '    datagrdFid.Rows(rowIndex1).Cells(5).Value = VALUE(5)
                    '    datagrdFid.Rows(rowIndex1).Cells(6).Value = VALUE(6)
                    '    datagrdFid.Rows(rowIndex1).Cells(7).Value = VALUE(7)
                    '    datagrdFid.Rows(rowIndex1).Cells(8).Value = VALUE(8)
                    '    datagrdFid.Rows(rowIndex1).Cells(9).Value = VALUE(9)
                    '    datagrdFid.Rows(rowIndex1).Cells(10).Value = VALUE(10)
                    '    datagrdFid.Rows(rowIndex1).Cells(11).Value = VALUE(11)
                    '    datagrdFid.Rows(rowIndex1).Cells(12).Value = VALUE(12)


                    '    currentID = rowIndex1 + 1

                    '    If VALUE(1) = "Square" Then
                    '        ' Draw square as before
                    '        Dim size As New Size(Convert.ToInt16(VALUE(4)), Convert.ToInt16(VALUE(5)))
                    '        Dim topLeft As New Point(Convert.ToInt16(VALUE(2)), Convert.ToInt16(VALUE(3)))
                    '        shapes.Add(New Square(topLeft, size, currentID, currentColor))

                    '    ElseIf VALUE(1) = "Circle" Then
                    '        ' Draw circle with dynamic size

                    '        Dim parts() As String = VALUE(6).Trim("()").Split(","c)


                    '        Dim trimmedPart() As String = parts(1).Split(")"c)
                    '        Dim centrx As String = parts(0)
                    '        Dim centey As String = trimmedPart(0)
                    '        Dim radius As Integer = CInt(Convert.ToInt16(VALUE(4)))
                    '        Dim centerX As Integer = Convert.ToInt16(centrx)
                    '        Dim centerY As Integer = Convert.ToInt16(centey)
                    '        shapes.Add(New Circle(New Point(centerX, centerY), radius / 2, currentID, currentColor))


                    '    End If


                    'Next




                    'Next



                End If
            End If
        Else
            MessageBox.Show("Please create recipe !.")
        End If
    End Sub
    Private Sub loadData()

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
            'Dim side As String = cmbpanelside.Text

            'Dim files As String = Directory.GetFiles(Logdir, file1, System.IO.SearchOption.AllDirectories)
            Dim check As Boolean = File.Exists(generatedFile)

            If check = False Then
                MySettings.Default.ProgramName = ""


                Return
            End If
            txt_Sel_Prog_name.Text = MySettings.Default.ProgramName

            LOADPOSISATA(generatedFile)

            Dim xmDocument As XmlDocument = New XmlDocument()
            xmDocument.Load(generatedFile)
            Dim Boardnodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/BOARD")


            For Each Feeder As XmlNode In Boardnodes
                Dim BOARDNAME As String = Feeder.ChildNodes(0).InnerText
                Dim XOFFSET As String = Feeder.ChildNodes(1).InnerText
                Dim YOFFSET As String = Feeder.ChildNodes(2).InnerText
                Dim XCOUNT As String = Feeder.ChildNodes(3).InnerText
                Dim YCOUNT As String = Feeder.ChildNodes(4).InnerText
                Dim XPITCH As String = Feeder.ChildNodes(5).InnerText
                Dim YPITCH As String = Feeder.ChildNodes(6).InnerText
                Dim PAN_LENGTH As String = Feeder.ChildNodes(7).InnerText
                Dim PAN_WIDTH As String = Feeder.ChildNodes(8).InnerText
                Dim PANEL_THICKNESS As String = Feeder.ChildNodes(9).InnerText





                X_offset.Text = XOFFSET
                Y_offset.Text = YOFFSET
                X_Pitch_Count.Text = XCOUNT
                Y_Pitch_Count.Text = YCOUNT
                X_Pitch.Text = XPITCH
                Y_Pitch.Text = YPITCH

                Panel_Length.Text = PAN_LENGTH
                Panel_Width.Text = PAN_WIDTH
                Panel_Thick.Text = PANEL_THICKNESS
            Next







            Dim RECEIPENAMEnodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/RECEIPENAME")
            For Each _PLCTAGnodes As XmlNode In RECEIPENAMEnodes
                Dim RNAME As String = _PLCTAGnodes.ChildNodes(0).InnerText
                Dim RPATH As String = _PLCTAGnodes.ChildNodes(1).InnerText

            Next



            Dim FIDUCIAL As XmlNode = xmDocument.SelectSingleNode("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL")



            Dim BBT As String = FIDUCIAL.ChildNodes(0).InnerText
            If BBT = "1" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False


                End If






            'Next

            Dim fiducials As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL/F")

            ' Iterate through each <F> node
            For Each fidNode As XmlNode In fiducials
                ' Read values from each child element within <F>
                Dim side As String = fidNode.Attributes("SIDE")?.InnerText
                Dim shape As String = fidNode("SHAPES")?.InnerText
                Dim fX1 As String = fidNode("F_X1")?.InnerText
                Dim fY1 As String = fidNode("F_Y1")?.InnerText
                Dim fRX2 As String = fidNode("F_RX2")?.InnerText
                Dim fRY2 As String = fidNode("F_RY2")?.InnerText
                Dim fCP As String = fidNode("F_CP")?.InnerText
                Dim xOff As String = fidNode("X_OFF")?.InnerText
                Dim yOff As String = fidNode("Y_OFF")?.InnerText
                Dim fPX As String = fidNode("F_PX")?.InnerText
                Dim fPY As String = fidNode("F_PY")?.InnerText
                Dim fScore As String = fidNode("F_SCORE")?.InnerText
                Dim fidNo As String = fidNode("FID_TYPE")?.InnerText

                ' Add a new row to the DataGridView with the values from XML
                Dim rowIndex1 As Integer = datagrdFid.Rows.Add()
                currentID = rowIndex1 + 1

                If shape = "Square" Then
                    ' Draw square as before
                    Dim size As New Size(Convert.ToInt16(fRX2), Convert.ToInt16(fRY2))
                    Dim topLeft As New Point(Convert.ToInt16(fX1), Convert.ToInt16(fY1))
                    shapes.Add(New Square(topLeft, size, currentID, currentColor))

                ElseIf ElseIfshape = "Circle" Then
                    ' Draw circle with dynamic size

                    Dim parts() As String = fCP.Trim("()").Split(","c)


                    Dim trimmedPart() As String = parts(1).Split(")"c)
                    Dim centrx As String = parts(0)
                    Dim centey As String = trimmedPart(0)
                    Dim radius As Integer = CInt(Convert.ToInt16(fRX2))
                    Dim centerX As Integer = Convert.ToInt16(centrx)
                    Dim centerY As Integer = Convert.ToInt16(centey)
                    shapes.Add(New Circle(New Point(centerX, centerY), radius / 2, currentID, currentColor))


                End If

                datagrdFid.Rows(rowIndex1).Cells(0).Value = side
                datagrdFid.Rows(rowIndex1).Cells(1).Value = shape
                datagrdFid.Rows(rowIndex1).Cells(2).Value = fX1
                datagrdFid.Rows(rowIndex1).Cells(3).Value = fY1
                datagrdFid.Rows(rowIndex1).Cells(4).Value = fRX2
                datagrdFid.Rows(rowIndex1).Cells(5).Value = fRY2
                datagrdFid.Rows(rowIndex1).Cells(6).Value = fCP
                datagrdFid.Rows(rowIndex1).Cells(7).Value = xOff
                datagrdFid.Rows(rowIndex1).Cells(8).Value = yOff
                datagrdFid.Rows(rowIndex1).Cells(9).Value = fPX
                datagrdFid.Rows(rowIndex1).Cells(10).Value = fPY
                datagrdFid.Rows(rowIndex1).Cells(11).Value = fScore
                datagrdFid.Rows(rowIndex1).Cells(12).Value = fidNo
            Next

            'Dim FIDUCIAL As XmlNode = xmDocument.SelectSingleNode("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL")
            'Dim FNodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL/F")
            'datagrdFid.Rows.Clear()
            'shapes.Clear()
            'For Each Node As XmlNode In FNodes
            '    Dim VALUE(20) As String
            '    VALUE(0) = Node.ChildNodes(0).InnerText
            '    VALUE(1) = Node.ChildNodes(1).InnerText
            '    VALUE(2) = Node.ChildNodes(2).InnerText
            '    VALUE(3) = Node.ChildNodes(3).InnerText
            '    VALUE(4) = Node.ChildNodes(4).InnerText
            '    VALUE(5) = Node.ChildNodes(5).InnerText
            '    VALUE(6) = Node.ChildNodes(6).InnerText
            '    VALUE(7) = Node.ChildNodes(7).InnerText
            '    VALUE(8) = Node.ChildNodes(8).InnerText
            '    VALUE(9) = Node.ChildNodes(9).InnerText
            '    VALUE(10) = Node.ChildNodes(10).InnerText
            '    VALUE(11) = Node.ChildNodes(11).InnerText
            '    VALUE(12) = Node.ChildNodes(12).InnerText






            '    Dim rowIndex As Integer = datagrdFid.Rows.Add()



            '    datagrdFid.Rows(rowIndex).Cells(0).Value = VALUE(0)
            '    datagrdFid.Rows(rowIndex).Cells(1).Value = VALUE(1)
            '    datagrdFid.Rows(rowIndex).Cells(2).Value = VALUE(2)
            '    datagrdFid.Rows(rowIndex).Cells(3).Value = VALUE(3)
            '    datagrdFid.Rows(rowIndex).Cells(4).Value = VALUE(4)
            '    datagrdFid.Rows(rowIndex).Cells(5).Value = VALUE(5)
            '    datagrdFid.Rows(rowIndex).Cells(6).Value = VALUE(6)
            '    datagrdFid.Rows(rowIndex).Cells(7).Value = VALUE(7)
            '    datagrdFid.Rows(rowIndex).Cells(8).Value = VALUE(8)
            '    datagrdFid.Rows(rowIndex).Cells(9).Value = VALUE(9)
            '    datagrdFid.Rows(rowIndex).Cells(10).Value = VALUE(10)
            '    datagrdFid.Rows(rowIndex).Cells(11).Value = VALUE(11)
            '    datagrdFid.Rows(rowIndex).Cells(12).Value = VALUE(12)



            '    currentID = rowIndex + 1


            '    If VALUE(1) = "Square" Then
            '        ' Draw square as before
            '        Dim size As New Size(Convert.ToInt16(VALUE(4)), Convert.ToInt16(VALUE(5)))
            '        Dim topLeft As New Point(Convert.ToInt16(VALUE(2)), Convert.ToInt16(VALUE(3)))
            '        shapes.Add(New Square(topLeft, size, currentID, currentColor))

            '    ElseIf VALUE(1) = "Circle" Then
            '        Dim cnter(2) As String

            '        Dim AX(2) As String

            '        cnter = VALUE(6).Split(","c)
            '        AX(0) = cnter(0).Trim("("c)
            '        AX(1) = cnter(1).Trim(")"c)
            '        ' Draw circle with dynamic size
            '        Dim radius As Integer = CInt(Convert.ToInt16(VALUE(4)))
            '        Dim centerX As Integer = Convert.ToInt16(AX(0))
            '        Dim centerY As Integer = Convert.ToInt16(AX(1))
            '        shapes.Add(New Circle(New Point(centerX, centerY), radius / 2, currentID, currentColor))

            '        'AddShapeToDataGridView(currentID, "Circle", centerX - radius, centerY - radius, radius, 0, centerX, centerY)
            '    End If





            'Next


        End If




    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim progname As String
        progname = txt_Sel_Prog_name.Text
        If progname = "" Then
            Return
        ElseIf (progname IsNot "") Then
            Dim isValid1 As Boolean = True
            Dim isValid As Boolean = True
            Dim fname As String = "" & ConfigurationManager.AppSettings("ReceipeFilepath").ToString().Trim()
            Dim Backupfname As String = "" & ConfigurationManager.AppSettings("BackupReceipeFilepath").ToString().Trim()
            Dim path As String = fname
            Dim Logdir As String = "" & fname
            Dim BackupLogdir As String = "" & Backupfname
            Dim ReceipeFileName As String = String.Empty
            If Not Directory.Exists(BackupLogdir) Then
                Directory.CreateDirectory(BackupLogdir)
            End If
            If Not Directory.Exists(Logdir) Then
                Directory.CreateDirectory(Logdir)
            End If
            ReceipeFileName = progname
            Dim generatedFile As String = Logdir & ReceipeFileName & ".xml"
            Dim BaackupgeneratedFile As String = BackupLogdir & ReceipeFileName & ".xml"
            'Dim side As String = cmbpanelside.Text
            Dim xmDocument As XmlDocument = New XmlDocument()
            xmDocument.Load(generatedFile)
            Dim Boardnodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/BOARD")

            For Each Feeder As XmlNode In Boardnodes
                Dim BOARD As BOARD = New BOARD()





                'Dim BOARDNAME As String = Feeder.ChildNodes(0).InnerText
                Feeder.ChildNodes(1).InnerText = X_offset.Text
                Feeder.ChildNodes(2).InnerText = Y_offset.Text
                Feeder.ChildNodes(3).InnerText = X_Pitch_Count.Text
                Feeder.ChildNodes(4).InnerText = Y_Pitch_Count.Text
                Feeder.ChildNodes(5).InnerText = X_Pitch.Text
                Feeder.ChildNodes(6).InnerText = Y_Pitch.Text
                Feeder.ChildNodes(7).InnerText = Panel_Length.Text
                Feeder.ChildNodes(8).InnerText = Panel_Width.Text
                Feeder.ChildNodes(9).InnerText = Panel_Thick.Text

            Next






            Dim COUNTERs_1 As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/COUNTER_1")

            For Each _PLCTAGnodes As XmlNode In COUNTERs_1

                If Counter1.Checked = True Then
                    _PLCTAGnodes.ChildNodes(0).InnerText = True
                    _PLCTAGnodes.ChildNodes(1).InnerText = Start_Count.Text
                    _PLCTAGnodes.ChildNodes(2).InnerText = End_Count.Text

                Else
                    _PLCTAGnodes.ChildNodes(0).InnerText = False
                    _PLCTAGnodes.ChildNodes(1).InnerText = "1"
                    _PLCTAGnodes.ChildNodes(2).InnerText = "9999"
                End If


            Next



            Dim COUNTERs_2 As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPE/COUNTER_2")
            For Each _PLC1TAGnodes As XmlNode In COUNTERs_2

                If Counter2.Checked = True Then
                    _PLC1TAGnodes.ChildNodes(0).InnerText = True
                    _PLC1TAGnodes.ChildNodes(1).InnerText = Start_Count.Text
                    _PLC1TAGnodes.ChildNodes(2).InnerText = End_Count.Text

                Else
                    _PLC1TAGnodes.ChildNodes(0).InnerText = False
                    _PLC1TAGnodes.ChildNodes(1).InnerText = "1"
                    _PLC1TAGnodes.ChildNodes(2).InnerText = "9999"
                End If


            Next


            Dim LASTSAVING As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/LASTSAVINGTIME")

            For Each _PLCTAGnode As XmlNode In LASTSAVING
                _PLCTAGnode.ChildNodes(0).InnerText = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")


            Next




            Dim RECEIPENAMEnodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPENAME")




            For Each _PLCTAGnodes As XmlNode In RECEIPENAMEnodes
                _PLCTAGnodes.ChildNodes(0).InnerText = progname


            Next







            'Next



            xmDocument.Save(generatedFile)
            xmDocument.Save(BaackupgeneratedFile)


            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''














        End If
    End Sub
    Private Sub btnfiducial_Click(sender As Object, e As EventArgs)
        Try
            If ListXMLPath.Count > 0 Then
                If Not String.IsNullOrEmpty(GRPATH) Then
                    Dim path As String = GRPATH
                    Dim Fiducial As New Fiducial
                    Fiducial.Setb = path
                    Fiducial.Show()
                Else
                    MessageBox.Show("Please select correct recipe!")
                End If
            Else
                MessageBox.Show("Please select correct recipe!")
            End If


        Catch ex As Exception

        End Try

    End Sub



    Private Sub txt_p_name_TextChanged(sender As Object, e As EventArgs)
        ValidateTxt_p_name()
    End Sub

    Private Sub txt_p_name_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        ValidateTxt_p_name()
    End Sub

    Private Sub ValidateTxt_p_name()
        Dim input As String = txt_Sel_Prog_name.Text

        ' Check if the input contains invalid characters (decimal points)
        If input.Contains(".") Then
            ErrorProvider1.SetError(txt_Sel_Prog_name, "Decimal points are not allowed.")
        ElseIf String.IsNullOrWhiteSpace(input) Then
            ErrorProvider1.SetError(txt_Sel_Prog_name, "Please enter a value.")
        Else
            ErrorProvider1.SetError(txt_Sel_Prog_name, String.Empty)
        End If
    End Sub

    Private Sub txt_p_len_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        ValidateTxt_p_len()
    End Sub

    Private Sub ValidateTxt_p_len()
        Dim input As String = Panel_Length.Text
        Dim result As Double

        ' Only validate if there is input
        If input <> String.Empty Then
            ' Check if the input is a valid non-negative number
            If Not Double.TryParse(input, result) OrElse result < 0 Then
                ErrorProvider1.SetError(txt_p_len, "Please enter a valid numeric value.")
            Else
                ErrorProvider1.SetError(txt_p_len, String.Empty) ' Clear the error
            End If
        Else
            ErrorProvider1.SetError(txt_p_len, String.Empty) ' Clear the error if empty
        End If
    End Sub

    Private Sub txt_p_wed_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        Validatetxt_p_wed()
    End Sub

    Private Sub Validatetxt_p_wed()
        Dim input As String = Panel_Width.Text
        Dim result As Double

        ' Only validate if there is input
        If input <> String.Empty Then
            ' Check if the input is a valid non-negative number
            If Not Double.TryParse(input, result) OrElse result < 0 Then
                ErrorProvider1.SetError(txt_p_wed, "Please enter a valid numeric value.")
            Else
                ErrorProvider1.SetError(txt_p_wed, String.Empty) ' Clear the error
            End If
        Else
            ErrorProvider1.SetError(txt_p_wed, String.Empty) ' Clear the error if empty
        End If
    End Sub

    Private Sub thk_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        Validatethk()
    End Sub

    Private Sub Validatethk()
        Dim input As String = Panel_Thick.Text






        Dim result As Double

        ' Only validate if there is input
        If input <> String.Empty Then
            ' Check if the input is a valid non-negative number
            If Not Double.TryParse(input, result) OrElse result < 0 Then
                ErrorProvider1.SetError(thk, "Please enter a valid numeric value.")
            Else
                ErrorProvider1.SetError(thk, String.Empty) ' Clear the error
            End If
        Else
            ErrorProvider1.SetError(thk, String.Empty) ' Clear the error if empty
        End If
    End Sub

    Private Sub txt_p_weight_TextChanged(sender As Object, e As EventArgs)
        Validatetxt_p_weight()
    End Sub

    Private Sub txt_p_weight_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        Validatetxt_p_weight()
    End Sub

    Private Sub Validatetxt_p_weight()
        Dim input As String = X_offset.Text
        Dim result As Double



        ' Only validate if there is input
        If input <> String.Empty Then
            ' Check if the input is a valid non-negative number
            If Not Double.TryParse(input, result) OrElse result < 0 Then
                ErrorProvider1.SetError(txt_p_weight, "Please enter a valid numeric value.")
            Else
                ErrorProvider1.SetError(txt_p_weight, String.Empty) ' Clear the error
            End If
        Else
            ErrorProvider1.SetError(txt_p_weight, String.Empty) ' Clear the error if empty
        End If
    End Sub

    Private Sub UD_R_count_TextChanged(sender As Object, e As EventArgs)
        ValidateUD_R_count()
    End Sub

    Private Sub UD_R_count_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        ValidateUD_R_count()
    End Sub

    Private Sub ValidateUD_R_count()
        Dim input As String = X_Pitch_Count.Text
        Dim result As Double

        ' Only validate if there is input
        If input <> String.Empty Then
            ' Check if the input is a valid non-negative number
            If Not Double.TryParse(input, result) OrElse result < 0 Then
                ErrorProvider1.SetError(UD_R_count, "Please enter a valid numeric value.")
            Else
                ErrorProvider1.SetError(UD_R_count, String.Empty) ' Clear the error
            End If
        Else
            ErrorProvider1.SetError(UD_R_count, String.Empty) ' Clear the error if empty
        End If
    End Sub

    Private Sub UD_R_pitch_TextChanged(sender As Object, e As EventArgs)
        ValidateUD_R_pitch()
    End Sub

    Private Sub UD_R_pitch_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        ValidateUD_R_pitch()
    End Sub

    Private Sub ValidateUD_R_pitch()
        Dim input As String = X_Pitch.Text
        Dim result As Double

        ' Only validate if there is input
        If input <> String.Empty Then
            ' Check if the input is a valid non-negative number
            If Not Double.TryParse(input, result) OrElse result < 0 Then
                ErrorProvider1.SetError(UD_R_pitch, "Please enter a valid numeric value.")
            Else
                ErrorProvider1.SetError(UD_R_pitch, String.Empty) ' Clear the error
            End If
        Else
            ErrorProvider1.SetError(UD_R_pitch, String.Empty) ' Clear the error if empty
        End If
    End Sub

    Private Sub UD_C_count_TextChanged(sender As Object, e As EventArgs)
        ValidateUD_C_count()
    End Sub

    Private Sub UD_C_count_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        ValidateUD_C_count()
    End Sub

    Private Sub ValidateUD_C_count()
        Dim input As String = Y_Pitch_Count.Text
        Dim result As Double

        ' Only validate if there is input


        If input <> String.Empty Then
            ' Check if the input is a valid non-negative number
            If Not Double.TryParse(input, result) OrElse result < 0 Then
                ErrorProvider1.SetError(UD_C_count, "Please enter a valid numeric value.")
            Else
                ErrorProvider1.SetError(UD_C_count, String.Empty) ' Clear the error
            End If
        Else
            ErrorProvider1.SetError(UD_C_count, String.Empty) ' Clear the error if empty
        End If
    End Sub

    Private Sub UD_C_pitch_TextChanged(sender As Object, e As EventArgs)
        ValidateUD_C_pitch()
    End Sub

    Private Sub UD_C_pitch_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        ValidateUD_C_pitch()
    End Sub

    Private Sub ValidateUD_C_pitch()
        Dim input As String = Y_Pitch.Text
        Dim result As Double





        ' Only validate if there is input
        If input <> String.Empty Then
            ' Check if the input is a valid non-negative number
            If Not Double.TryParse(input, result) OrElse result < 0 Then
                ErrorProvider1.SetError(UD_C_pitch, "Please enter a valid numeric value.")
            Else
                ErrorProvider1.SetError(UD_C_pitch, String.Empty) ' Clear the error
            End If
        Else
            ErrorProvider1.SetError(UD_C_pitch, String.Empty) ' Clear the error if empty
        End If
    End Sub



    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try            ' Check if a row is selected in the DataGridView
            If ListXMLPath.Count > 0 Then
                ' Get the selected row
                Dim selectedRow As DataGridViewRow = DATAGRID.SelectedRows(0)
                Dim selectedFilePath As String = selectedRow.Cells("RPATH").Value.ToString()

                ' Prompt the user for confirmation
                Dim result = MessageBox.Show("Are you sure you want to delete this file?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If result = DialogResult.Yes Then
                    ' Delete the selected file
                    File.Delete(selectedFilePath)
                    ListXMLPath.Remove(selectedFilePath)

                    ' Remove the selected row from the DataGridView
                    DATAGRID.Rows.Remove(selectedRow)

                    Dim fname As String = "" & ConfigurationManager.AppSettings("Deleted_Logs").ToString().Trim()

                    '  Dim XMLOutPutPath As String = "D:\LM- Test/"

                    Dim path As String = fname
                    Dim Logdir As String = "" & fname

                    Dim ReceipeFileName As String = String.Empty
                    If Not Directory.Exists(Logdir) Then
                        Directory.CreateDirectory(Logdir)
                    End If

                    Dim _LaserHeadLocation As String() = Directory.GetFiles(Logdir, "*.xml", System.IO.SearchOption.AllDirectories)
                    Dim count As Integer = _LaserHeadLocation.Count()

                    If count = 0 Then
                        ReceipeFileName = txt_Sel_Prog_name.Text
                    Else
                        ReceipeFileName = txt_Sel_Prog_name.Text & "_" & count + 1
                    End If

                    Dim generatedFile As String = Logdir + ReceipeFileName & ".xml"








                    Dim xdoc = XDocument.Parse("<Error_Logs></Error_Logs>")
                    Dim contacts As XElement = New XElement("Error",
                                                        New XElement("FileName", generatedFile),
                                                        New XElement("DateTime", DateTime.Now().ToString()),
                                                        New XElement("UserName", ""),
                                                         New XElement("PageName", "Recipe"),
                                                        New XElement("Path", generatedFile),
                                                        New XElement("ErrorMessage", "Successfully deleted"))
                    xdoc.Root.Add(contacts)
                    xdoc.Save(generatedFile)





                    ' Show a message that the file was deleted successfully
                    MessageBox.Show("File deleted successfully.", "Delete Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                ' If no row is selected, show a message
                MessageBox.Show("Please select a row to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            ' Handle the exception


            Dim fname As String = "" & ConfigurationManager.AppSettings("Error_Logs").ToString().Trim()

            '  Dim XMLOutPutPath As String = "D:\LM- Test/"

            Dim path As String = fname
            Dim Logdir As String = "" & fname

            Dim ReceipeFileName As String = String.Empty
            If Not Directory.Exists(Logdir) Then
                Directory.CreateDirectory(Logdir)
            End If

            Dim _LaserHeadLocation As String() = Directory.GetFiles(Logdir, "*.xml", System.IO.SearchOption.AllDirectories)
            Dim count As Integer = _LaserHeadLocation.Count()

            If count = 0 Then
                ReceipeFileName = txt_Sel_Prog_name.Text
            Else
                ReceipeFileName = txt_Sel_Prog_name.Text & "_" & count + 1
            End If

            Dim generatedFile As String = Logdir & ReceipeFileName & ".xml"








            Dim xdoc = XDocument.Parse("<Error_Logs></Error_Logs>")
            Dim contacts As XElement = New XElement("Error",
                                                        New XElement("FileName", generatedFile),
                                                        New XElement("DateTime", DateTime.Now().ToString()),
                                                        New XElement("UserName", ""),
                                                         New XElement("PageName", "Recipe"),
                                                        New XElement("Path", generatedFile),
                                                        New XElement("ErrorMessage", ex.Message))
            xdoc.Root.Add(contacts)
            xdoc.Save(generatedFile)
            MessageBox.Show("An error occurred while deleting the file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub btnclear_Click(sender As Object, e As EventArgs)
        LoadReceipe()
        txt_Sel_Prog_name.Text = String.Empty
        rtxtcurrentpg.Text = ""
        GRPATH = String.Empty
        ListXMLPath.Clear()




    End Sub


    Private _parameter As String
    Public Shared CurrentPageText As String = String.Empty

    Private Sub rtxtcurrentpg_TextChanged(sender As Object, e As EventArgs)
        Dim searchText As String = rtxtcurrentpg.Text
        For Each row As DataGridViewRow In DATAGRID.Rows
            If row.Cells(1).Value IsNot Nothing AndAlso row.Cells(1).Value.ToString() = searchText Then
                ' LoadDataFromRow(row)
                Exit For
            End If
        Next
        CurrentPageText = rtxtcurrentpg.Text
        Dim homePage As Home_Page = Nothing

        ' Iterate through all open forms to find Home_Page
        For Each frm As Form In System.Windows.Forms.Application.OpenForms
            If TypeOf frm Is Home_Page Then
                homePage = CType(frm, Home_Page)
                Exit For
            End If
        Next

        ' If Home_Page is found, update the label
        If homePage IsNot Nothing Then

        Else
            ' If Home_Page is not open, create a new instance, update the label, and show the form
            homePage = New Home_Page()

            homePage.Show()
        End If
    End Sub
    Public Sub ReadDataFromFiducialData1()
        ' Access the data from FiducialData1.SelectedShapeData1
        Dim serialNo As Integer = FiducialData1.SelectedShapeData1.SerialNo1
        Dim shape As String = FiducialData1.SelectedShapeData1.Shape1
        Dim x1 As Integer = FiducialData1.SelectedShapeData1.X11
        Dim y1 As Integer = FiducialData1.SelectedShapeData1.Y11
        Dim x2 As Integer = FiducialData1.SelectedShapeData1.X21
        Dim y2 As Integer = FiducialData1.SelectedShapeData1.Y21
        Dim center As String = FiducialData1.SelectedShapeData1.Center1

    End Sub
    Public Sub ReadDataFromFiducialData2()
        ' Access the data from FiducialData1.SelectedShapeData1
        Dim serialNo As Integer = FiducialData2.SelectedShapeData2.SerialNo2
        Dim shape As String = FiducialData2.SelectedShapeData2.Shape2
        Dim x1 As Integer = FiducialData2.SelectedShapeData2.X12
        Dim y1 As Integer = FiducialData2.SelectedShapeData2.Y12
        Dim x2 As Integer = FiducialData2.SelectedShapeData2.X22
        Dim y2 As Integer = FiducialData2.SelectedShapeData2.Y22
        Dim center As String = FiducialData2.SelectedShapeData2.Center2

        ' Fill the text box cbShape with the value of shape1

    End Sub

    Dim previousValuex As Single
    Dim previousValueY As Single
    Dim previousValueCW As Single
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        RichTextBox8.Text = txt_Sel_Prog_name.Text
        RichTextBox14.Text = txt_Sel_Prog_name.Text
        RichTextBox15.Text = txt_Sel_Prog_name.Text

        Dim X(1) As Integer
        plc.GetDevice("D342", X(0))
        plc.GetDevice("D343", X(1))
        Dim BIT As Integer
        plc.GetDevice("M260", BIT)

        If BIT = 1 Then
            plc.SetDevice("M260", 0)
            MessageBox.Show("MARKING DONE")
            plc.SetDevice("M207", 0)
        End If

        Dim xnum As Single = ConvertWordToFloat(X)
        Dim roundedNumber As Double = Math.Round(xnum, 2)
        If roundedNumber <> previousValuex Then
            X_Current1.Text = roundedNumber.ToString("F2")
            X_Current2.Text = roundedNumber.ToString("F2")

            X_Current.Text = roundedNumber.ToString("F2")
            previousValuex = roundedNumber
        End If

        Dim Y(1) As Integer
        plc.GetDevice("D344", Y(0))
        plc.GetDevice("D345", Y(1))

        Dim ynum As Single = ConvertWordToFloat(Y)
        Dim roundedNumberY As Double = Math.Round(ynum, 2)
        If roundedNumberY <> previousValueY Then

            Y_Current1.Text = roundedNumberY.ToString("F2")
            Y_Current2.Text = roundedNumberY.ToString("F2")
            Y_Current.Text = roundedNumberY.ToString("F2")
            previousValueY = roundedNumberY
        End If
        Dim CW(1) As Integer
        plc.GetDevice("D312", CW(0))
        plc.GetDevice("D313", CW(1))

        Dim Cnum As Single = ConvertWordToFloat(CW)
        Dim CONV As Double = Math.Round(Cnum, 2)
        If CONV <> previousValueY Then
            CONV_Current1.Text = CONV.ToString("F2")
            CONV_Current2.Text = CONV.ToString("F2")
            CONV_Current.Text = CONV.ToString("F2")
            previousValueCW = CONV
        End If


        If RichTextBox2.Text = "" Then
            RichTextBox2.Text = "1"
        End If
        If RichTextBox3.Text = "" Then
            RichTextBox3.Text = "0"
        End If












    End Sub





    Private Sub btn_Widthadj_Click_1(sender As Object, e As EventArgs)
        Dim floatValue As Single
        If Single.TryParse(Panel_Width.Text, floatValue) Then
            ' Round the float value to two decimal places
            Dim roundedValue As Single = Math.Round(floatValue, 3)

            ' Convert the rounded float value to two 16-bit integers
            Dim words() As Integer = ConvertFloatToWord(roundedValue)

            ' Example: Write to different data registers
            plc.SetDevice("D320", words(0))
            plc.SetDevice("D321", words(1))
        Else
        End If

    End Sub



    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim selectedValue As String = Guna2ComboBox1.SelectedItem.ToString()
        Select Case selectedValue
            Case "HIGH"
                plc.SetDevice("D358", 3)
            Case "MEDIUM"
                plc.SetDevice("D358", 2)
            Case "LOW"
                plc.SetDevice("D358", 1)
        End Select
    End Sub


    Private Sub btn_Widthadj_MouseDown_1(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M240", 1)
    End Sub

    Private Sub btn_Widthadj_MouseUp_1(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M240", 1)
    End Sub



    Private Sub Guna2ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim selectedValue As String = Guna2ComboBox2.SelectedItem.ToString()
        Select Case selectedValue
            Case "0.1MM"
                plc.SetDevice("D356", 3)
            Case "1MM"
                plc.SetDevice("D356", 2)
            Case "10MM"
                plc.SetDevice("D356", 1)
            Case "CONITNUES"
                plc.SetDevice("D356", 0)
        End Select
    End Sub

    Private Sub Guna2Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub



    Private Sub btn_Widthadj_Click_2(sender As Object, e As EventArgs)
        Dim floatValueCW As Single
        If Single.TryParse(Panel_Width.Text, floatValueCW) Then
            ' Convert the float value to two 16-bit integers
            Dim words() As Integer = ConvertFloatToWord(floatValueCW)

            ' Write the integers to the PLC registers
            plc.SetDevice("D320", words(0))
            plc.SetDevice("D321", words(1))
        Else
            ' If parsing fails, you can handle the invalid input here
        End If
    End Sub

    Private Sub btn_Widthadj_MouseDown_2(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M240", 1)
    End Sub

    Private Sub btn_Widthadj_MouseUp_2(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M240", 0)
    End Sub



    Private Sub PictureBox7_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox7.MouseDown

        If shapes.Count < datagrdFid.Rows.Count Then ' Subtract 1 to account for the new row placeholder
            If e.Button = MouseButtons.Left Then
                startPoint = e.Location
                endPoint = e.Location ' Set endPoint initially to start point
                isDrawing = True
            End If
        Else

            MessageBox.Show("You have reached the maximum number of shapes allowed.", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub PictureBox7_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox7.MouseMove
        If isDrawing AndAlso e.Button = MouseButtons.Left Then
            endPoint = e.Location
            PictureBox7.Invalidate()
        End If
    End Sub

    Private Sub AddShapeToDataGridView(id As Integer, shape As String, x1 As Integer, y1 As Integer, width As Integer, Optional height As Single = 0, Optional centerX As Integer = 0, Optional centerY As Integer = 0)
        xposs = X_Current1.Text
        yposs = Y_Current1.Text
        Dim centerPoint As String

        ' Variables for main and sub IDs
        Dim mainid As Integer = 1
        Dim subid As Integer = 1
        Dim TYP As String = ""
        'Dim CELVALUE As String

        ' Determine the main or sub ID


        ' Add row based on shape type
        If shape = "Square" Then
            Dim x2 As Integer = width
            Dim y2 As Integer = height
            Dim centerXInt As Integer = CInt((x1 + width) \ 2) ' Calculate center X as integer
            Dim centerYInt As Integer = CInt((y1 + height) \ 2) ' Calculate center Y as integer
            centerPoint = $"({centerXInt}, {centerYInt})"

            datagrdFid.SelectedRows(0).Cells(1).Value = shape
            datagrdFid.SelectedRows(0).Cells(2).Value = x1.ToString()
            datagrdFid.SelectedRows(0).Cells(3).Value = y1.ToString()
            datagrdFid.SelectedRows(0).Cells(4).Value = x2.ToString()
            datagrdFid.SelectedRows(0).Cells(5).Value = y2.ToString()
            datagrdFid.SelectedRows(0).Cells(6).Value = centerPoint





        ElseIf shape = "Circle" Then
            centerPoint = $"({centerX}, {centerY})"
            Dim row As String() = {TYP, shape, x1.ToString(), y1.ToString(), width.ToString(), height.ToString(), centerPoint, xposs, yposs}
            'datagrdFid.SelectedRows(0).Cells(0).Value = TYP
            datagrdFid.SelectedRows(0).Cells(1).Value = shape
            datagrdFid.SelectedRows(0).Cells(2).Value = x1.ToString()
            datagrdFid.SelectedRows(0).Cells(3).Value = y1.ToString()
            datagrdFid.SelectedRows(0).Cells(4).Value = width.ToString()
            datagrdFid.SelectedRows(0).Cells(5).Value = height.ToString()
            datagrdFid.SelectedRows(0).Cells(6).Value = centerPoint



        Else
            Throw New ArgumentException("Invalid shape type")
        End If
    End Sub
    Private Sub Button13_Click(sender As Object, e As EventArgs)

        Dim mainid As Integer = 1
        Dim subid As Integer = 1
        Dim TYP As String = ""
        Dim CELVALUE As String



        ' Determine the main or sub ID

        If FIDTYPE.Text = "MAIN" Then
            If datagrdFid.Rows.Count > 0 Then
                For Each row1 As DataGridViewRow In datagrdFid.Rows
                    CELVALUE = Convert.ToString(row1.Cells(0).Value)
                    If CELVALUE(0) = "M" Then
                        mainid += 1

                    End If


                Next
            End If
            TYP = "MAIN" & mainid
        ElseIf (FIDTYPE.Text = "SUB") Or (FIDTYPE.Text = "") Then
            If datagrdFid.Rows.Count > 0 Then
                For Each row1 As DataGridViewRow In datagrdFid.Rows
                    CELVALUE = Convert.ToString(row1.Cells(0).Value)
                    If CELVALUE(0) = "S" Then
                        subid += 1
                    End If

                Next
            End If
            TYP = "SUB" & subid
        End If

        Dim SHAP As String
        If isDrawing Then
            ' Check the maximum current ID from the DataGridView
            'Dim maxCurrentID As Integer = GetMaxCurrentID()
            'If maxCurrentID >= currentID Then
            '    currentID = maxCurrentID + 1
            'End If

            If drawSquares Then
                ' Draw square as before
                SHAP = "Square"
            Else
                ' Draw circle with dynamic size


                SHAP = "Circle"
            End If


        End If


        If SCORE.Text = "" Then
            SCORE.Text = "0"
        End If





        datagrdFid.Rows.Add(TYP, SHAP, "0", "0", "0", "0", "0", "0", "0", xposs, yposs,
                             SCORE.Text, False)
        datagrdFid.Rows(0).Selected = False
        Dim int1 As Integer = datagrdFid.Rows.Count
        datagrdFid.Rows(int1 - 1).Selected = True

    End Sub
    Private Class Shape
        Public Property ID As String
        Public Property Color As Color

        Public Sub New(id As String, color As Color)
            Me.ID = id
            Me.Color = color
        End Sub
    End Class

    Private Class Square
        Inherits Shape

        Public Property TopLeft As Point
        Public Property Size As Size

        Public Sub New(topLeft As Point, size As Size, id As Integer, color As Color)
            MyBase.New(id, color)
            Me.TopLeft = topLeft
            Me.Size = size
        End Sub
    End Class

    Private Class Circle
        Inherits Shape

        Public Property Center As Point
        Public Property Radius As Integer

        Public Sub New(center As Point, radius As Integer, id As Integer, color As Color)
            MyBase.New(id, color)
            Me.Center = center
            Me.Radius = radius
        End Sub
    End Class

    Private Sub PictureBox7_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox7.Paint
        Dim g As Graphics = e.Graphics
        Dim font As New Font("Arial", 12)
        Dim brush As New SolidBrush(Color.Black)

        'Draw all stored shapes
        For Each shape In shapes
            Dim pen As New Pen(shape.Color, 2)
            If TypeOf shape Is Square Then
                Dim square As Square = DirectCast(shape, Square)
                g.DrawRectangle(pen, square.TopLeft.X, square.TopLeft.Y, square.Size.Width, square.Size.Height)
                g.DrawString(square.ID.ToString(), font, brush, square.TopLeft.X, square.TopLeft.Y - 20)
            ElseIf TypeOf shape Is Circle Then
                Dim circle As Circle = DirectCast(shape, Circle)
                g.DrawEllipse(pen, circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2)
                g.DrawString(circle.ID.ToString(), font, brush, circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius - 20)
            End If
        Next

        ' Draw the current shape if drawing is in progress
        If isDrawing Then
            Dim pen As New Pen(currentColor, 2)
            If drawSquares Then
                Dim size As New Size(Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y))
                Dim rect As New Rectangle(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y), size.Width, size.Height)
                g.DrawRectangle(pen, rect)
                g.DrawString(currentID.ToString(), font, brush, rect.X, rect.Y - 20)
            Else ' Draw circle
                Dim radius As Integer = CInt(Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2)) / 2)
                Dim centerX As Integer = Math.Min(startPoint.X, endPoint.X) + radius
                Dim centerY As Integer = Math.Min(startPoint.Y, endPoint.Y) + radius
                g.DrawEllipse(pen, centerX - radius, centerY - radius, radius * 2, radius * 2)
                g.DrawString(currentID.ToString(), font, brush, centerX - radius, centerY - radius - 20)
            End If
        End If

    End Sub
    Private normal As Color
    Private Async Function FIDUCIALCHECK() As Task
        Using pipeServer As New NamedPipeServerStream("TestPipe", PipeDirection.InOut)
            Console.WriteLine("Waiting for connection...")
            pipeServer.WaitForConnection()

            Dim message As String = "C:\Users\NMT-Sudarshan\Desktop\EBW8Image8.jpg,C:\Users\NMT-Sudarshan\Desktop\EBW8Image1.jpg"
            Dim buffer As Byte() = Encoding.UTF8.GetBytes(message)

            pipeServer.Write(buffer, 0, buffer.Length)
            Console.WriteLine("Message sent to client.")
            Dim buffer1 As Byte() = New Byte(255) {}
            pipeServer.Read(buffer1, 0, buffer1.Length)

            Dim message1 As String = Encoding.UTF8.GetString(buffer1).TrimEnd(ChrW(0))
            Console.WriteLine("Message received from server: " & message1)
            DATA = message1.Split(",")

            pipeServer.Disconnect()
        End Using
    End Function


    Private Sub Guna2CircleButton1_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton1.Click
        isDrawing = False
        drawSquares = False
        PictureBox7.Invalidate()
        Guna2Button1.BackColor = normal
        Guna2CircleButton1.BackColor = Color.Green
        Panel26.Hide()
        Panel27.Show()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        drawSquares = True ' Set flag to draw squares
        isDrawing = False ' Ensure we're not drawing circles
        Guna2CircleButton1.BackColor = normal
        Guna2Button1.BackColor = Color.Green
        Panel26.Show()
        Panel27.Hide()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button27.Click
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            currentColor = ColorDialog1.Color
        End If
    End Sub
    Private Sub UpdateDataGridViewWithTextBoxValues(rowIndex As Integer)
        ' Update the DataGridView to include values from TextBox1 and TextBox2
        datagrdFid.Rows(rowIndex).Cells(9).Value = X_Current1.Text
        datagrdFid.Rows(rowIndex).Cells(10).Value = Y_Current1.Text
    End Sub
    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        If datagrdFid.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Increase the boundary from the top
                Dim increaseAmount As Integer = 2 ' Amount by which the boundary should be increased
                square.TopLeft = New Point(square.TopLeft.X, square.TopLeft.Y - increaseAmount)
                square.Size = New Size(square.Size.Width, square.Size.Height + increaseAmount)

                ' Update the DataGridView to reflect the changes
                datagrdFid.Rows(rowIndex).Cells(3).Value = square.TopLeft.Y ' Update the Y1 value
                datagrdFid.Rows(rowIndex).Cells(5).Value = square.TopLeft.Y + square.Size.Height ' Update the Y2 value
                datagrdFid.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point
                UpdateDataGridViewWithTextBoxValues(rowIndex)
                ' Redraw PictureBox
                PictureBox7.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a square.")
            End If
        Else
            MessageBox.Show("Please select a square from the DataGridView.")
        End If
    End Sub
    Private Sub ClearAndDisposePictureBox(pictureBox As PictureBox, serialNumber As String)
        ' Check if the PictureBox contains an image with the specified serial number
        If pictureBox.Image IsNot Nothing Then
            ' Check if the image file path corresponds to the serial number
            Dim imagePath As String = Path.Combine("A:\Project", txt_Sel_Prog_name.Text.Trim(), serialNumber & ".png")
            If File.Exists(imagePath) Then
                pictureBox.Image = Nothing
                pictureBox.Invalidate()
            End If
        End If
    End Sub
    Private Sub Button18_Click(sender As Object, e As EventArgs)
        ' Check if a row is selected
        If datagrdFid.SelectedRows.Count > 0 Then
            ' Show a confirmation dialog
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            ' If the user confirms the deletion
            If result = DialogResult.Yes Then
                ' Check if txt_Sel_Prog_name is empty
                Dim folderName As String = txt_Sel_Prog_name.Text.Trim()
                If String.IsNullOrEmpty(folderName) Then
                    MessageBox.Show("Please select a recipe first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                ' Get the selected row index
                Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

                ' Get the S.No from the selected row
                '
                Dim serialNumber As String = datagrdFid.Rows(rowIndex).Cells(0).Value.ToString()
                Dim IMAGEPATH1 As String = "" & ConfigurationManager.AppSettings("FiducialImage").ToString().Trim()
                ' Define the image file path based on S.No
                Dim imagePath As String = Path.Combine(IMAGEPATH1, folderName, serialNumber & ".png")

                ' Try to delete the image file if it exists
                Try
                    If File.Exists(imagePath) Then
                        File.Delete(imagePath)
                    End If
                Catch ex As IOException
                    MessageBox.Show("Error deleting the file: " & ex.Message)
                End Try

                ' Remove the corresponding shape from the shapes list
                Dim selectedShapeID As String = datagrdFid.Rows(rowIndex).Cells(0).Value.ToString()
                Dim shapeToRemove As Shape = shapes.FirstOrDefault(Function(s) s.ID = (rowIndex + 1))
                If shapeToRemove IsNot Nothing Then
                    shapes.Remove(shapeToRemove)
                End If

                ' Remove the row from the DataGridView


                ' Clear and dispose of the PictureBox images
                'ClearAndDisposePictureBox(Guna2PictureBox1, serialNumber)
                ' ClearAndDisposePictureBox(Guna2PictureBox2, serialNumber)

                ' Update the currentID to be one more than the maximum ID in the shapes list
                If shapes.Count > 0 Then
                    currentID = shapes.Max(Function(s) Convert.ToInt32(s.ID)) + 1
                Else
                    currentID = 1
                End If

                ' Redraw PictureBox or perform any other necessary UI update
                PictureBox7.Invalidate()
                'Dim selectedShapeID As String = (datagrdFid.Rows(rowIndex).Cells(0).Value)
                'Dim shapeToRemove As Shape = shapes.FirstOrDefault(Function(s) s.ID = (rowIndex))
                If shapeToRemove IsNot Nothing Then
                    shapes.Remove(shapeToRemove)
                End If

                ' Remove the row from the DataGridView
                datagrdFid.Rows.RemoveAt(rowIndex)

                ' Redraw PictureBox


                Dim progname As String
                progname = txt_Sel_Prog_name.Text
                If progname = "" Then
                    Return
                ElseIf (progname IsNot "") Then
                    Dim isValid1 As Boolean = True
                    Dim isValid As Boolean = True
                    Dim fname As String = "" & ConfigurationManager.AppSettings("ReceipeFilepath").ToString().Trim()
                    Dim Backupfname As String = "" & ConfigurationManager.AppSettings("BackupReceipeFilepath").ToString().Trim()
                    Dim path As String = fname
                    Dim Logdir As String = "" & fname
                    Dim BackupLogdir As String = "" & Backupfname
                    Dim ReceipeFileName As String = String.Empty
                    If Not Directory.Exists(BackupLogdir) Then
                        Directory.CreateDirectory(BackupLogdir)
                    End If
                    If Not Directory.Exists(Logdir) Then
                        Directory.CreateDirectory(Logdir)
                    End If
                    ReceipeFileName = progname
                    Dim generatedFile As String = Logdir & ReceipeFileName & ".xml"
                    Dim BaackupgeneratedFile As String = BackupLogdir & ReceipeFileName & ".xml"

                    Dim xmDocument As XmlDocument = New XmlDocument()
                    xmDocument.Load(generatedFile)
                    Dim MARKPOSITION As XmlNode = xmDocument.SelectSingleNode("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL")
                    'Dim Nodes = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/MARKPOSITION/POSI")
                    'Dim dd As XmlNodeList = MARKPOSITION.SelectNodes("JOBList/JOB/TAGTYPE/MARKPOSITION/POSI")
                    'MARKPOSITION.RemoveChild(dd)
                    MARKPOSITION.RemoveAll()

                    xmDocument.Save(generatedFile)
                    xmDocument.Save(BaackupgeneratedFile)











                    Dim BTMFID As XmlElement = xmDocument.CreateElement("BOTTOM_FID")
                    If CheckBox1.Checked Then
                        BTMFID.InnerText = "1"
                    Else
                        BTMFID.InnerText = "0"
                    End If
                    MARKPOSITION.AppendChild(BTMFID)


                    For Each row As DataGridViewRow In datagrdFid.Rows
                        If row.IsNewRow Then Continue For ' Skip new row placeholder

                        ' Create <F> node for each DataGridView row
                        Dim POSI As XmlElement = xmDocument.CreateElement("F")
                        MARKPOSITION.AppendChild(POSI)

                        ' Set SIDE attribute
                        Dim SIDE As XmlAttribute = xmDocument.CreateAttribute("SIDE")
                        SIDE.InnerText = row.Cells("SIDE").Value?.ToString() ' Replace "TOP" with actual cell data if needed
                        POSI.Attributes.Append(SIDE)

                        ' Create elements for each cell and add to <F> node
                        Dim SHAPE As XmlElement = xmDocument.CreateElement("SHAPES")
                        SHAPE.InnerText = row.Cells("SHAPES").Value?.ToString()
                        POSI.AppendChild(SHAPE)

                        Dim F_X1 As XmlElement = xmDocument.CreateElement("F_X1")
                        F_X1.InnerText = row.Cells("F_X1").Value?.ToString()
                        POSI.AppendChild(F_X1)

                        Dim F_Y1 As XmlElement = xmDocument.CreateElement("F_Y1")
                        F_Y1.InnerText = row.Cells("F_Y1").Value?.ToString()
                        POSI.AppendChild(F_Y1)

                        Dim F_RX2 As XmlElement = xmDocument.CreateElement("F_RX2")
                        F_RX2.InnerText = row.Cells("F_RX2").Value?.ToString()
                        POSI.AppendChild(F_RX2)

                        Dim F_RY2 As XmlElement = xmDocument.CreateElement("F_RY2")
                        F_RY2.InnerText = row.Cells("F_RY2").Value?.ToString()
                        POSI.AppendChild(F_RY2)

                        Dim F_CP As XmlElement = xmDocument.CreateElement("F_CP")
                        F_CP.InnerText = row.Cells("F_CP").Value?.ToString()
                        POSI.AppendChild(F_CP)

                        Dim X_OFF As XmlElement = xmDocument.CreateElement("X_OFF")
                        X_OFF.InnerText = row.Cells("X_OFF").Value?.ToString()
                        POSI.AppendChild(X_OFF)

                        Dim Y_OFF As XmlElement = xmDocument.CreateElement("Y_OFF")
                        Y_OFF.InnerText = row.Cells("Y_OFF").Value?.ToString()
                        POSI.AppendChild(Y_OFF)

                        Dim F_PX As XmlElement = xmDocument.CreateElement("F_PX")
                        F_PX.InnerText = row.Cells("F_PX").Value?.ToString()
                        POSI.AppendChild(F_PX)

                        Dim F_PY As XmlElement = xmDocument.CreateElement("F_PY")
                        F_PY.InnerText = row.Cells("F_PY").Value?.ToString()
                        POSI.AppendChild(F_PY)

                        Dim SCOR As XmlElement = xmDocument.CreateElement("F_SCORE")
                        SCOR.InnerText = row.Cells("F_SCORE").Value?.ToString()
                        POSI.AppendChild(SCOR)

                        Dim FID_NO As XmlElement = xmDocument.CreateElement("FID_NO")
                        FID_NO.InnerText = row.Cells("FID_NO").Value?.ToString()
                        POSI.AppendChild(FID_NO)
                    Next

                    ' Save the updated XML document
                    xmDocument.Save(generatedFile)
                    xmDocument.Save(BaackupgeneratedFile)
                    'Dim i As Integer = 0
                    'Dim DATA(20) As String
                    'For Each row As DataGridViewRow In datagrdFid.Rows
                    '    If Not row.IsNewRow Then
                    '        Dim POSI As XmlElement = xmDocument.CreateElement("F")
                    '        MARKPOSITION.AppendChild(POSI)
                    '        Dim ID As XmlAttribute = xmDocument.CreateAttribute("id")
                    '        ID.Value = (i + 1).ToString
                    '        POSI.Attributes.Append(ID)
                    '        Dim SNO As XmlElement = xmDocument.CreateElement("SNO")
                    '        Dim SHAPE As XmlElement = xmDocument.CreateElement("SHAPES")
                    '        Dim F_X1 As XmlElement = xmDocument.CreateElement("F_X1")
                    '        Dim F_Y1 As XmlElement = xmDocument.CreateElement("F_Y1")
                    '        Dim F_RX2 As XmlElement = xmDocument.CreateElement("F_RX2")
                    '        Dim F_RY2 As XmlElement = xmDocument.CreateElement("F_RY2")
                    '        Dim F_CP As XmlElement = xmDocument.CreateElement("F_CP")
                    '        Dim X_OFF As XmlElement = xmDocument.CreateElement("X_OFF")
                    '        Dim Y_OFF As XmlElement = xmDocument.CreateElement("Y_OFF")
                    '        Dim F_PX As XmlElement = xmDocument.CreateElement("F_PX")
                    '        Dim F_PY As XmlElement = xmDocument.CreateElement("F_PY")
                    '        Dim SCOR As XmlElement = xmDocument.CreateElement("F_SCORE")
                    '        Dim SEL As XmlElement = xmDocument.CreateElement("SEL")







                    '        DATA(0) = datagrdFid.Rows(i).Cells(0).Value.ToString()
                    '        DATA(1) = datagrdFid.Rows(i).Cells(1).Value.ToString()
                    '        DATA(2) = datagrdFid.Rows(i).Cells(2).Value.ToString()
                    '        DATA(3) = datagrdFid.Rows(i).Cells(3).Value.ToString()
                    '        DATA(4) = datagrdFid.Rows(i).Cells(4).Value.ToString()
                    '        DATA(5) = datagrdFid.Rows(i).Cells(5).Value.ToString()
                    '        DATA(6) = datagrdFid.Rows(i).Cells(6).Value.ToString()
                    '        DATA(7) = datagrdFid.Rows(i).Cells(7).Value.ToString()
                    '        DATA(8) = datagrdFid.Rows(i).Cells(8).Value.ToString()
                    '        DATA(9) = datagrdFid.Rows(i).Cells(9).Value.ToString()
                    '        DATA(10) = datagrdFid.Rows(i).Cells(10).Value.ToString()
                    '        DATA(11) = datagrdFid.Rows(i).Cells(11).Value.ToString()
                    '        DATA(12) = datagrdFid.Rows(i).Cells(12).Value.ToString()










                    '        'If datagrdFid.Rows(i).Cells(11).Value = False Then
                    '        'DATA(11) = "False"
                    '        'Else
                    '        'DATA(11) = "True"
                    '        'End If





                    '        SNO.InnerText = DATA(0)
                    '        SHAPE.InnerText = DATA(1)
                    '        F_X1.InnerText = DATA(2)
                    '        F_Y1.InnerText = DATA(3)
                    '        F_RX2.InnerText = DATA(4)
                    '        F_RY2.InnerText = DATA(5)
                    '        F_CP.InnerText = DATA(6)
                    '        X_OFF.InnerText = DATA(7)
                    '        Y_OFF.InnerText = DATA(8)
                    '        F_PX.InnerText = DATA(9)
                    '        F_PY.InnerText = DATA(10)

                    '        SCOR.InnerText = DATA(11)
                    '        SEL.InnerText = DATA(12)


                    '        POSI.AppendChild(SNO)
                    '        POSI.AppendChild(SHAPE)
                    '        POSI.AppendChild(F_X1)
                    '        POSI.AppendChild(F_Y1)
                    '        POSI.AppendChild(F_RX2)
                    '        POSI.AppendChild(F_RY2)
                    '        POSI.AppendChild(F_CP)
                    '        POSI.AppendChild(X_OFF)
                    '        POSI.AppendChild(Y_OFF)
                    '        POSI.AppendChild(F_PX)
                    '        POSI.AppendChild(F_PY)
                    '        POSI.AppendChild(SCOR)
                    '        POSI.AppendChild(SEL)
                    '        i = i + 1
                    '    End If
                    'Next
                    'xmDocument.Save(generatedFile)
                    'xmDocument.Save(BaackupgeneratedFile)

                End If
            End If
            PictureBox7.Invalidate()
        Else
            MessageBox.Show("Please select a row to delete.")
        End If
        'Recipe delete
        ' Check if a row is selected

    End Sub

    Private Sub Guna2Button8_Click(sender As Object, e As EventArgs) Handles Guna2Button8.Click

        If datagrdFid.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Contract the boundary from the top
                Dim contractAmount As Integer = 2 ' Amount by which the boundary should be contracted
                If square.Size.Height > contractAmount Then ' Ensure the height doesn't go negative
                    square.TopLeft = New Point(square.TopLeft.X, square.TopLeft.Y + contractAmount)
                    square.Size = New Size(square.Size.Width, square.Size.Height - contractAmount)

                    ' Update the DataGridView to reflect the changes
                    datagrdFid.Rows(rowIndex).Cells(3).Value = square.TopLeft.Y ' Update the Y1 value
                    datagrdFid.Rows(rowIndex).Cells(5).Value = square.TopLeft.Y + square.Size.Height ' Update the Y2 value
                    datagrdFid.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point
                    UpdateDataGridViewWithTextBoxValues(rowIndex)
                    ' Redraw PictureBox
                    PictureBox7.Invalidate()
                Else
                    MessageBox.Show("Cannot contract square further.")
                End If
            Else
                MessageBox.Show("Selected shape is not a square.")
            End If
        Else
            MessageBox.Show("Please select a square from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button9_Click(sender As Object, e As EventArgs) Handles Guna2Button9.Click
        If datagrdFid.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Contract the boundary from the bottom
                Dim contractAmount As Integer = 2 ' Amount by which the boundary should be contracted
                If square.Size.Height > contractAmount Then ' Ensure the height doesn't go negative
                    square.Size = New Size(square.Size.Width, square.Size.Height - contractAmount)

                    ' Update the DataGridView to reflect the changes
                    datagrdFid.Rows(rowIndex).Cells(5).Value = square.TopLeft.Y + square.Size.Height ' Update the Y2 value
                    datagrdFid.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point
                    UpdateDataGridViewWithTextBoxValues(rowIndex)
                    ' Redraw PictureBox
                    PictureBox7.Invalidate()
                Else
                    MessageBox.Show("Cannot contract square further.")
                End If
            Else
                MessageBox.Show("Selected shape is not a square.")
            End If
        Else
            MessageBox.Show("Please select a square from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        If datagrdFid.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Increase the boundary from the bottom
                Dim increaseAmount As Integer = 2 ' Amount by which the boundary should be increased
                square.Size = New Size(square.Size.Width, square.Size.Height + increaseAmount)

                ' Update the DataGridView to reflect the changes
                datagrdFid.Rows(rowIndex).Cells(5).Value = square.TopLeft.Y + square.Size.Height ' Update the Y2 value
                datagrdFid.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point
                UpdateDataGridViewWithTextBoxValues(rowIndex)
                ' Redraw PictureBox
                PictureBox7.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a square.")
            End If
        Else
            MessageBox.Show("Please select a square from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button7_Click(sender As Object, e As EventArgs) Handles Guna2Button7.Click
        If datagrdFid.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Increase the boundary from the left
                Dim increaseAmount As Integer = 2 ' Amount by which the boundary should be increased
                square.TopLeft = New Point(square.TopLeft.X - increaseAmount, square.TopLeft.Y)
                square.Size = New Size(square.Size.Width + increaseAmount, square.Size.Height)

                ' Update the DataGridView to reflect the changes
                datagrdFid.Rows(rowIndex).Cells(2).Value = square.TopLeft.X ' Update the X1 value
                datagrdFid.Rows(rowIndex).Cells(4).Value = square.TopLeft.X + square.Size.Width ' Update the X2 value
                datagrdFid.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point
                UpdateDataGridViewWithTextBoxValues(rowIndex)
                ' Redraw PictureBox
                PictureBox7.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a square.")
            End If
        Else
            MessageBox.Show("Please select a square from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button11_Click(sender As Object, e As EventArgs) Handles Guna2Button11.Click
        If datagrdFid.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Contract the boundary from the left
                Dim contractAmount As Integer = 2 ' Amount by which the boundary should be contracted
                If square.Size.Width > contractAmount Then ' Ensure the width doesn't go negative
                    square.TopLeft = New Point(square.TopLeft.X + contractAmount, square.TopLeft.Y)
                    square.Size = New Size(square.Size.Width - contractAmount, square.Size.Height)

                    ' Update the DataGridView to reflect the changes
                    datagrdFid.Rows(rowIndex).Cells(2).Value = square.TopLeft.X ' Update the X1 value
                    datagrdFid.Rows(rowIndex).Cells(4).Value = square.TopLeft.X + square.Size.Width ' Update the X2 value
                    datagrdFid.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point
                    UpdateDataGridViewWithTextBoxValues(rowIndex)
                    ' Redraw PictureBox
                    PictureBox7.Invalidate()
                Else
                    MessageBox.Show("Cannot contract square further.")
                End If
            Else
                MessageBox.Show("Selected shape is not a square.")
            End If
        Else
            MessageBox.Show("Please select a square from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button10_Click(sender As Object, e As EventArgs) Handles Guna2Button10.Click
        If datagrdFid.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Contract the boundary from the right
                Dim contractAmount As Integer = 2 ' Amount by which the boundary should be contracted
                If square.Size.Width > contractAmount Then ' Ensure the width doesn't go negative
                    square.Size = New Size(square.Size.Width - contractAmount, square.Size.Height)

                    ' Update the DataGridView to reflect the changes
                    datagrdFid.Rows(rowIndex).Cells(4).Value = square.TopLeft.X + square.Size.Width ' Update the X2 value
                    datagrdFid.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point
                    UpdateDataGridViewWithTextBoxValues(rowIndex)
                    ' Redraw PictureBox
                    PictureBox7.Invalidate()
                Else
                    MessageBox.Show("Cannot contract square further.")
                End If
            Else
                MessageBox.Show("Selected shape is not a square.")
            End If
        Else
            MessageBox.Show("Please select a square from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        If datagrdFid.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Increase the boundary from the right
                Dim increaseAmount As Integer = 2 ' Amount by which the boundary should be increased
                square.Size = New Size(square.Size.Width + increaseAmount, square.Size.Height)

                ' Update the DataGridView to reflect the changes
                datagrdFid.Rows(rowIndex).Cells(4).Value = square.TopLeft.X + square.Size.Width ' Update the X2 value
                datagrdFid.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point
                UpdateDataGridViewWithTextBoxValues(rowIndex)
                ' Redraw PictureBox
                PictureBox7.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a square.")
            End If
        Else
            MessageBox.Show("Please select a square from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button12_Click(sender As Object, e As EventArgs) Handles Guna2Button12.Click
        If datagrdFid.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim circle As Circle = shapes.OfType(Of Circle)().FirstOrDefault(Function(s) s.ID = selectedShapeID)
            If circle IsNot Nothing Then
                Dim moveAmount As Integer = 2 ' Amount by which the circle should be moved
                circle.Center = New Point(circle.Center.X, circle.Center.Y - moveAmount)
                datagrdFid.Rows(rowIndex).Cells(3).Value = circle.Center.Y - circle.Radius ' Update the Y1 value
                datagrdFid.Rows(rowIndex).Cells(6).Value = $"({circle.Center.X}, {circle.Center.Y})" ' Update the center point
                PictureBox7.Invalidate()
                UpdateDataGridViewWithTextBoxValues(rowIndex)
            Else
                MessageBox.Show("Selected shape is not a circle.")
            End If
        Else
            MessageBox.Show("Please select a circle from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button14_Click(sender As Object, e As EventArgs) Handles Guna2Button14.Click
        If datagrdFid.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim circle As Circle = shapes.OfType(Of Circle)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If circle IsNot Nothing Then
                ' Move the circle to the left
                Dim moveAmount As Integer = 2 ' Amount by which the circle should be moved
                circle.Center = New Point(circle.Center.X + moveAmount, circle.Center.Y)

                ' Update the DataGridView to reflect the changes
                datagrdFid.Rows(rowIndex).Cells(2).Value = circle.Center.X - circle.Radius ' Update the X1 value
                datagrdFid.Rows(rowIndex).Cells(6).Value = $"({circle.Center.X}, {circle.Center.Y})" ' Update the center point
                UpdateDataGridViewWithTextBoxValues(rowIndex)
                ' Redraw PictureBox
                PictureBox7.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a circle.")
            End If
        Else
            MessageBox.Show("Please select a circle from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button15_Click(sender As Object, e As EventArgs) Handles Guna2Button15.Click
        If datagrdFid.SelectedRows.Count > 0 Then

            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim circle As Circle = shapes.OfType(Of Circle)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If circle IsNot Nothing Then
                ' Move the circle downwards
                Dim moveAmount As Integer = 2
                circle.Center = New Point(circle.Center.X, circle.Center.Y + moveAmount)

                ' Update the DataGridView to reflect the changes
                datagrdFid.Rows(rowIndex).Cells(3).Value = circle.Center.Y - circle.Radius ' Update the Y1 value
                datagrdFid.Rows(rowIndex).Cells(6).Value = $"({circle.Center.X}, {circle.Center.Y})" ' Update the center point
                UpdateDataGridViewWithTextBoxValues(rowIndex)
                ' Redraw PictureBox
                PictureBox7.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a circle.")
            End If
        Else
            MessageBox.Show("Please select a circle from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button13_Click(sender As Object, e As EventArgs) Handles Guna2Button13.Click
        If datagrdFid.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim circle As Circle = shapes.OfType(Of Circle)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If circle IsNot Nothing Then
                ' Move the circle to the left
                Dim moveAmount As Integer = 2 ' Amount by which the circle should be moved
                circle.Center = New Point(circle.Center.X - moveAmount, circle.Center.Y)

                ' Update the DataGridView to reflect the changes
                datagrdFid.Rows(rowIndex).Cells(2).Value = circle.Center.X - circle.Radius ' Update the X1 value
                datagrdFid.Rows(rowIndex).Cells(6).Value = $"({circle.Center.X}, {circle.Center.Y})" ' Update the center point
                UpdateDataGridViewWithTextBoxValues(rowIndex)
                ' Redraw PictureBox
                PictureBox7.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a circle.")
            End If
        Else
            MessageBox.Show("Please select a circle from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button16_Click(sender As Object, e As EventArgs) Handles Guna2Button16.Click
        If datagrdFid.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim circle As Circle = shapes.OfType(Of Circle)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If circle IsNot Nothing Then
                ' Increase the size of the circle
                Dim increaseAmount As Integer = 2 ' Amount by which the radius should be increased
                circle.Radius += increaseAmount

                ' Update the DataGridView to reflect the changes
                datagrdFid.Rows(rowIndex).Cells(2).Value = circle.Center.X - circle.Radius ' Update the X1 value
                datagrdFid.Rows(rowIndex).Cells(3).Value = circle.Center.Y - circle.Radius ' Update the Y1 value
                datagrdFid.Rows(rowIndex).Cells(4).Value = circle.Radius * 2 ' Update the width (diameter)
                UpdateDataGridViewWithTextBoxValues(rowIndex)
                ' Redraw PictureBox
                PictureBox7.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a circle.")
            End If
        Else
            MessageBox.Show("Please select a circle from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button17_Click(sender As Object, e As EventArgs) Handles Guna2Button17.Click
        If datagrdFid.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = rowIndex + 1

            ' Find the shape with the corresponding ID
            Dim circle As Circle = shapes.OfType(Of Circle)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If circle IsNot Nothing Then
                ' Increase the size of the circle
                Dim increaseAmount As Integer = 2 ' Amount by which the radius should be increased
                circle.Radius -= increaseAmount

                ' Update the DataGridView to reflect the changes
                datagrdFid.Rows(rowIndex).Cells(2).Value = circle.Center.X - circle.Radius ' Update the X1 value
                datagrdFid.Rows(rowIndex).Cells(3).Value = circle.Center.Y - circle.Radius ' Update the Y1 value
                datagrdFid.Rows(rowIndex).Cells(4).Value = circle.Radius * 2 ' Update the width (diameter)
                UpdateDataGridViewWithTextBoxValues(rowIndex)
                ' Redraw PictureBox
                PictureBox7.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a circle.")
            End If
        Else
            MessageBox.Show("Please select a circle from the DataGridView.")
        End If
    End Sub



    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles SAVE_FID.Click
        Dim progname As String
        progname = RichTextBox14.Text
        If progname = "" Then
            Return
        ElseIf (progname IsNot "") Then
            Dim isValid1 As Boolean = True
            Dim isValid As Boolean = True
            Dim fname As String = "" & ConfigurationManager.AppSettings("ReceipeFilepath").ToString().Trim()
            Dim Backupfname As String = "" & ConfigurationManager.AppSettings("BackupReceipeFilepath").ToString().Trim()
            Dim path As String = fname
            Dim Logdir As String = "" & fname
            Dim BackupLogdir As String = "" & Backupfname
            Dim ReceipeFileName As String = String.Empty
            If Not Directory.Exists(BackupLogdir) Then
                Directory.CreateDirectory(BackupLogdir)
            End If
            If Not Directory.Exists(Logdir) Then
                Directory.CreateDirectory(Logdir)
            End If
            ReceipeFileName = progname
            Dim generatedFile As String = Logdir & ReceipeFileName & ".xml"
            Dim BaackupgeneratedFile As String = BackupLogdir & ReceipeFileName & ".xml"



            Dim xmDocument As XmlDocument = New XmlDocument()
            xmDocument.Load(generatedFile)
            Dim MARKPOSITION As XmlNode = xmDocument.SelectSingleNode("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL")


            ' Clear existing child nodes
            MARKPOSITION.RemoveAll()


            Dim BTMFID As XmlElement = xmDocument.CreateElement("BOTTOM_FID")
            If CheckBox1.Checked Then
                BTMFID.InnerText = "1"
            Else
                BTMFID.InnerText = "0"
            End If
            MARKPOSITION.AppendChild(BTMFID)

            Dim DATA(20) As String

            ' Iterate through DataGridView rows and add elements based on row data
            For Each row As DataGridViewRow In datagrdFid.Rows
                If Not row.IsNewRow Then
                    ' Create the main position element
                    Dim POSI As XmlElement = xmDocument.CreateElement("F")
                    MARKPOSITION.AppendChild(POSI)

                    ' Add SIDE attribute
                    Dim SIDE As XmlAttribute = xmDocument.CreateAttribute("SIDE")
                    SIDE.InnerText = If(row.Index < 2, "TOP", "BOTTOM")
                    POSI.Attributes.Append(SIDE)



                    ' Fill DATA array with cell values from the current row
                    For i As Integer = 1 To 12
                        DATA(i) = If(row.Cells(i).Value IsNot Nothing, row.Cells(i).Value.ToString(), "")
                    Next

                    ' Create and append XML elements with data from DATA array
                    Dim SHAPE As XmlElement = xmDocument.CreateElement("SHAPES")
                    SHAPE.InnerText = DATA(1)
                    POSI.AppendChild(SHAPE)

                    Dim F_X1 As XmlElement = xmDocument.CreateElement("F_X1")
                    F_X1.InnerText = DATA(2)
                    POSI.AppendChild(F_X1)

                    Dim F_Y1 As XmlElement = xmDocument.CreateElement("F_Y1")
                    F_Y1.InnerText = DATA(3)
                    POSI.AppendChild(F_Y1)

                    Dim F_RX2 As XmlElement = xmDocument.CreateElement("F_RX2")
                    F_RX2.InnerText = DATA(4)
                    POSI.AppendChild(F_RX2)

                    Dim F_RY2 As XmlElement = xmDocument.CreateElement("F_RY2")
                    F_RY2.InnerText = DATA(5)
                    POSI.AppendChild(F_RY2)

                    Dim F_CP As XmlElement = xmDocument.CreateElement("F_CP")
                    F_CP.InnerText = DATA(6)
                    POSI.AppendChild(F_CP)

                    Dim X_OFF As XmlElement = xmDocument.CreateElement("X_OFF")
                    X_OFF.InnerText = DATA(7)
                    POSI.AppendChild(X_OFF)

                    Dim Y_OFF As XmlElement = xmDocument.CreateElement("Y_OFF")
                    Y_OFF.InnerText = DATA(8)
                    POSI.AppendChild(Y_OFF)

                    Dim F_PX As XmlElement = xmDocument.CreateElement("F_PX")
                    F_PX.InnerText = DATA(9)
                    POSI.AppendChild(F_PX)

                    Dim F_PY As XmlElement = xmDocument.CreateElement("F_PY")
                    F_PY.InnerText = DATA(10)
                    POSI.AppendChild(F_PY)

                    Dim SCOR As XmlElement = xmDocument.CreateElement("F_SCORE")
                    SCOR.InnerText = DATA(11)
                    POSI.AppendChild(SCOR)
                    Dim FIDNO As XmlElement = xmDocument.CreateElement("FID_TYPE")
                    FIDNO.InnerText = DATA(12)
                    POSI.AppendChild(FIDNO)


                End If
            Next




            ' Save the modified XML document
            xmDocument.Save(generatedFile)
            xmDocument.Save(BaackupgeneratedFile)
            'Dim xmDocument As New XmlDocument()
            'xmDocument.Load(generatedFile)
            'Dim MARKPOSITION As XmlNode = xmDocument.SelectSingleNode("JOBList/JOB/TAGTYPE/RECEIPE/FIDUCIAL")
            'MARKPOSITION.RemoveAll() ' Clear existing nodes
            'xmDocument.Save(generatedFile)
            'xmDocument.Save(BaackupgeneratedFile)

            'For Each row As DataGridViewRow In datagrdFid.Rows
            '    If row.IsNewRow Then Continue For ' Skip new row placeholder

            '    ' Create <F> node for each DataGridView row
            '    Dim POSI As XmlElement = xmDocument.CreateElement("F")
            '    MARKPOSITION.AppendChild(POSI)

            '    ' Set SIDE attribute
            '    Dim SIDE As XmlAttribute = xmDocument.CreateAttribute("SIDE")
            '    SIDE.InnerText = row.Cells("SIDE").Value?.ToString() ' Replace "TOP" with actual cell data if needed
            '    POSI.Attributes.Append(SIDE)

            '    ' Create elements for each cell and add to <F> node
            '    Dim SHAPE As XmlElement = xmDocument.CreateElement("SHAPES")
            '    SHAPE.InnerText = row.Cells("SHAPES").Value?.ToString()
            '    POSI.AppendChild(SHAPE)

            '    Dim F_X1 As XmlElement = xmDocument.CreateElement("F_X1")
            '    F_X1.InnerText = row.Cells("F_X1").Value?.ToString()
            '    POSI.AppendChild(F_X1)

            '    Dim F_Y1 As XmlElement = xmDocument.CreateElement("F_Y1")
            '    F_Y1.InnerText = row.Cells("F_Y1").Value?.ToString()
            '    POSI.AppendChild(F_Y1)

            '    Dim F_RX2 As XmlElement = xmDocument.CreateElement("F_RX2")
            '    F_RX2.InnerText = row.Cells("F_RX2").Value?.ToString()
            '    POSI.AppendChild(F_RX2)

            '    Dim F_RY2 As XmlElement = xmDocument.CreateElement("F_RY2")
            '    F_RY2.InnerText = row.Cells("F_RY2").Value?.ToString()
            '    POSI.AppendChild(F_RY2)

            '    Dim F_CP As XmlElement = xmDocument.CreateElement("F_CP")
            '    F_CP.InnerText = row.Cells("F_CP").Value?.ToString()
            '    POSI.AppendChild(F_CP)

            '    Dim X_OFF As XmlElement = xmDocument.CreateElement("X_OFF")
            '    X_OFF.InnerText = row.Cells("X_OFF").Value?.ToString()
            '    POSI.AppendChild(X_OFF)

            '    Dim Y_OFF As XmlElement = xmDocument.CreateElement("Y_OFF")
            '    Y_OFF.InnerText = row.Cells("Y_OFF").Value?.ToString()
            '    POSI.AppendChild(Y_OFF)

            '    Dim F_PX As XmlElement = xmDocument.CreateElement("F_PX")
            '    F_PX.InnerText = row.Cells("F_PX").Value?.ToString()
            '    POSI.AppendChild(F_PX)

            '    Dim F_PY As XmlElement = xmDocument.CreateElement("F_PY")
            '    F_PY.InnerText = row.Cells("F_PY").Value?.ToString()
            '    POSI.AppendChild(F_PY)

            '    Dim SCOR As XmlElement = xmDocument.CreateElement("F_SCORE")
            '    SCOR.InnerText = row.Cells("F_SCORE").Value?.ToString()
            '    POSI.AppendChild(SCOR)

            '    Dim FID_NO As XmlElement = xmDocument.CreateElement("FID_NO")
            '    FID_NO.InnerText = row.Cells("FID_NO").Value?.ToString()
            '    POSI.AppendChild(FID_NO)
            'Next

            '' Save the updated XML document
            'xmDocument.Save(generatedFile)
            'xmDocument.Save(BaackupgeneratedFile)

        End If
    End Sub







    Private Sub ButtonSoftwareOnce_Click(sender As Object, e As EventArgs) Handles TRIGGER.Click
        If FID_CAMERA.Checked = True Then
            Dim nRet As Int32
            nRet = Home_Page.FidCam1.SetCommandValue("TriggerSoftware")
        End If



    End Sub
    Private Sub Recipe_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        Home_Page.LiveCamera1.SetEnumValue("TriggerMode", CCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON)
        Home_Page.FidCam1.SetEnumValue("TriggerMode", CCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON)
        plc.SetDevice("M247", 0)  ''''''''''' RED LIGHT
        Thread.Sleep(100)
    End Sub
    Private Sub SaveImage(shapeToSave As Shape, folderPath As String, imageName As String)

        ' Determine bounding box
        Dim boundingBox As Rectangle
        If TypeOf shapeToSave Is Square Then
            Dim square As Square = CType(shapeToSave, Square)
            ' Calculate the center point

            boundingBox = New Rectangle((square.TopLeft.X) * 4, (square.TopLeft.Y) * 4, (square.Size.Width) * 4, (square.Size.Height) * 4)
        ElseIf TypeOf shapeToSave Is Circle Then
            Dim circle As Circle = CType(shapeToSave, Circle)
            ' Scale the radius by 4
            Dim scaledRadius As Integer = circle.Radius * 4
            Dim centrX As Integer = (circle.Center.X) * 4
            Dim centrY As Integer = (circle.Center.Y) * 4

            boundingBox = New Rectangle((centrX - scaledRadius), (centrY - scaledRadius), (scaledRadius * 2), (scaledRadius * 2))
        Else
            MsgBox("Unknown shape type.")
            Return
        End If

        ' Create a bitmap and draw the image
        Dim bitmap As New Bitmap(boundingBox.Width, boundingBox.Height)
        Dim img As New Bitmap("C:\Manage Files\Load\123.Png")
        Using g As Graphics = Graphics.FromImage(bitmap)
            Dim sourceRect As New Rectangle(boundingBox.Location, boundingBox.Size)
            Dim destRect As New Rectangle(0, 0, boundingBox.Width, boundingBox.Height)
            If PictureBox7.Image IsNot Nothing Then
                g.DrawImage(img, destRect, sourceRect, GraphicsUnit.Pixel)
                img.Dispose()
            Else
                MsgBox("Live camera feed is not available.")
                Return
            End If
        End Using

        ' Ensure the directory exists
        If Not Directory.Exists(folderPath) Then Directory.CreateDirectory(folderPath)

        ' Define the save path
        Dim savePath As String = Path.Combine(folderPath, imageName)

        ' Save the bitmap to the specified path
        Try
            bitmap.Save(savePath, System.Drawing.Imaging.ImageFormat.Png)
            MsgBox("Image saved successfully to " & savePath)
        Catch ex As Exception
            MsgBox("Error saving the image: " & ex.Message)
        End Try
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles SAVE_ROI.Click
        Dim shapeToSave As Shape

        Dim folderName As String = RichTextBox14.Text
        Dim directoryPath As String
        Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index
        Dim FIDNAME1 As String = datagrdFid.Rows(rowIndex).Cells(12).Value.ToString()
        Dim selectedSNo As String = datagrdFid.Rows(rowIndex).Cells(0).Value.ToString()

        Dim fidImage As String = "" & ConfigurationManager.AppSettings("FiducialImage").ToString().Trim()

        Dim fidTempImage1 As String = "" & ConfigurationManager.AppSettings("FiducialTemplate").ToString().Trim()
        Try
            ' Check if any row is selected in the DataGridView
            If datagrdFid.SelectedRows.Count = 0 Then
                MsgBox("Please select a shape from the DataGridView.")
                Return
            End If

            ' Get the selected row index


            ' Get the corresponding S.No from the selected row




            ' Get the corresponding shape ID
            Dim selectedShapeID As String = datagrdFid.Rows(rowIndex).Cells(0).Value.ToString()

            ' Find the shape with the corresponding ID
            shapeToSave = shapes.FirstOrDefault(Function(s) s.ID = (rowIndex + 1))

            If shapeToSave Is Nothing Then
                MsgBox("Selected shape not found.")
                Return
            End If

            ' Determine the bounding box of the shape
            Dim boundingBox As Rectangle
            If TypeOf shapeToSave Is Square Then
                Dim square As Square = CType(shapeToSave, Square)
                boundingBox = New Rectangle((square.TopLeft.X), (square.TopLeft.Y), (square.Size.Width), (square.Size.Height))
            ElseIf TypeOf shapeToSave Is Circle Then
                Dim circle As Circle = CType(shapeToSave, Circle)
                boundingBox = New Rectangle((circle.Center.X - circle.Radius), (circle.Center.Y - circle.Radius), (circle.Radius * 2), (circle.Radius * 2))
            Else
                MsgBox("Unknown shape type.")
                Return
            End If

            ' Create a new bitmap with the size of the bounding box
            Dim bitmap As New Bitmap(boundingBox.Width, boundingBox.Height)

            ' Draw the relevant portion of the live camera PictureBox image onto the new bitmap
            Using g As Graphics = Graphics.FromImage(bitmap)
                ' Adjust the source rectangle to match the shape's bounding box within the PictureBox
                Dim sourceRect As New Rectangle(boundingBox.Location, boundingBox.Size)
                Dim destRect As New Rectangle(0, 0, boundingBox.Width, boundingBox.Height)

                ' Check if the live camera feed image is available
                If PictureBox7.Image IsNot Nothing Then
                    ' Draw the image from Guna2PictureBox1, which contains the live camera feed
                    g.DrawImage(PictureBox7.Image, destRect, sourceRect, GraphicsUnit.Pixel)
                Else
                    MsgBox("Live camera feed is not available.")
                    Return
                End If

            End Using

            ' Construct the directory path based on TextBox3 value


            If String.IsNullOrEmpty(folderName) Then
                ' If TextBox3 is empty, use the default path
                directoryPath = "D:\ff\Wrong"
            Else
                ' Use the folder name from TextBox3
                directoryPath = Path.Combine(fidImage, folderName, selectedSNo)

                ' Create the directory if it doesn't exist
                If Not Directory.Exists(directoryPath) Then
                    Directory.CreateDirectory(directoryPath)
                End If
            End If

            ' Save the new bitmap to the specified path with the S.No as filename
            Dim savePath1 As String = Path.Combine(directoryPath, FIDNAME1 & ".png")
            Try
                bitmap.Save(savePath1, System.Drawing.Imaging.ImageFormat.Png)
                ' MsgBox("Image saved successfully to " & savePath1)
            Catch ex As Exception
                MsgBox("Error saving the image: " & ex.Message)
            End Try
        Catch ex As Exception

        End Try
        'here we startted saving
        Try
            'Dim tempPath As String = "C:\Manage Files\Template_Image"

            tempPath = Path.Combine(fidTempImage1, folderName, selectedSNo)

            ' Create the directory if it doesn't exist
            If Not Directory.Exists(tempPath) Then
                Directory.CreateDirectory(tempPath)
            End If

            Dim selectedFileName As String = datagrdFid.SelectedRows(0).Cells(0).Value.ToString()

            ' Call the SaveImage function
            'SaveImage(shapeToSave, tempPath, selectedFileName & ".Png")
            Dim savePath2 As String = Path.Combine(tempPath, FIDNAME1 & ".png")
            Dim temp As Bitmap = PictureBox7.Image
            temp.Save(savePath2)


            'MsgBox("Image saved successfully to " & savePath2)


        Catch ex As Exception

        End Try

        'Try
        '    ' Check if a row is selected
        '    If datagrdFid.SelectedRows.Count > 0 Then
        '        ' Get the selected row
        '        Dim selectedRow As DataGridViewRow = datagrdFid.SelectedRows(0)

        '        ' Extract the value from column 6
        '        Dim column6Value As String = selectedRow.Cells(4).Value.ToString()
        '        Dim parts() As String = column6Value.Trim("()").Split(","c)
        '        'cnter = VALUE(6).Split("(", ","c)
        '        'Dim part As String = "45)"
        '        'Dim part1() As String = part.Trim("()")

        '        Dim trimmedPart() As String = parts(1).Split(")"c)

        '        Dim centx As Integer = Convert.ToInt16(parts(0))
        '        Dim centy As Integer = Convert.ToInt16(trimmedPart(0))
        '        centx = centx * 4
        '        centy = centy * 4
        '        Dim valu As String = ("(" & centx & "," & centy & ")")
        '        ' Extract the name for the text file from the selected row (e.g., from column 1)
        '        Dim fileName As String = "text" & ".txt"

        '        ' Define the file path
        '        Dim filePath As String = "C:\Manage Files\centre Point\" & fileName

        '        ' Write the value to the text file
        '        Try
        '            System.IO.File.WriteAllText(filePath, valu)
        '            MessageBox.Show("Value stored in " & filePath)
        '        Catch ex As Exception
        '            MessageBox.Show("Error writing to file: " & ex.Message)
        '        End Try
        '    Else
        '        MessageBox.Show("Please select a row first.")
        '    End If
        'Catch ex As Exception

        'End Try
        'Try
        '    ' Define the folder path
        '    Dim folderPath As String = "C:\Manage Files\image_name"

        '    ' Ensure the folder exists, if not, create it
        '    If Not System.IO.Directory.Exists(folderPath) Then
        '        System.IO.Directory.CreateDirectory(folderPath)
        '    End If

        '    ' Define the specific text for the filename
        '    Dim specificFileName As String = "text.txt"

        '    ' Get the selected row's cell (0) value
        '    Dim cellValue As String = datagrdFid.SelectedRows(0).Cells(0).Value.ToString()

        '    ' Construct the full file path
        '    Dim filePath As String = System.IO.Path.Combine(folderPath, specificFileName)

        '    ' Write the value to the text file
        '    System.IO.File.WriteAllText(filePath, cellValue)

        '    ' Inform the user
        '    MessageBox.Show("File saved successfully at " & filePath)
        'Catch ex As Exception

        'End Try
    End Sub

    Private imageLoadCount As Integer = 0
    Private Sub datagrdFid_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles datagrdFid.CellContentDoubleClick
        ' Check if a valid row was clicked
        If e.RowIndex < 0 Then
            Exit Sub
        End If

        ' Check if TextBox3 is empty
        Dim folderName As String = txt_Sel_Prog_name.Text.Trim()
        If String.IsNullOrEmpty(folderName) Then
            MessageBox.Show("Please select a recipe first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Get the S.No from the clicked row
        Dim selectedRow As DataGridViewRow = datagrdFid.Rows(e.RowIndex)
        Dim serialNumber As String = selectedRow.Cells(0).Value.ToString()

        ' Determine the directory path based on TextBox3 value
        Dim directoryPath As String = Path.Combine("C:\Manage Files\Fid_Image", folderName)

        ' Define the image file path based on S.No
        Dim imagePath As String = Path.Combine(directoryPath, serialNumber & ".png")

        ' Load the image into the PictureBox
        If imageLoadCount = 0 Then
            If File.Exists(imagePath) Then
                ' Guna2PictureBox1.Image = New Bitmap(imagePath)
                imageLoadCount += 1
            Else
                MessageBox.Show("Image not found for S.No " & serialNumber, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        ElseIf imageLoadCount = 1 Then
            Dim result As DialogResult = MessageBox.Show("Do you want to load the second image?", "Confirmation", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                If File.Exists(imagePath) Then
                    ' Guna2PictureBox2.Image = New Bitmap(imagePath)
                    imageLoadCount += 1
                Else
                    MessageBox.Show("Image not found for S.No " & serialNumber, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        ElseIf imageLoadCount >= 2 Then
            Dim resetResult As DialogResult = MessageBox.Show("You have loaded two images. Do you want to reset?", "Reset", MessageBoxButtons.YesNo)
            If resetResult = DialogResult.Yes Then
                'Guna2PictureBox1.Image = Nothing
                'Guna2PictureBox2.Image = Nothing
                imageLoadCount = 0 ' Reset the counter
            End If
        End If
    End Sub

    Private Sub Learn_Click(sender As Object, e As EventArgs) Handles Learn.Click


        Dim rowIndex As Integer = datagrdFid.SelectedRows(0).Index
        datagrdFid.Rows(rowIndex).Cells(9).Value = X_Current1.Text
        datagrdFid.Rows(rowIndex).Cells(10).Value = Y_Current1.Text

        ' datagrdFid.Rows(rowIndex).Cells(13).Value = TextBox4.Text
        datagrdFid.Rows(rowIndex).Cells(11).Value = SCORE.Text
    End Sub

    Private Sub PictureBox7_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox7.MouseUp

        If isDrawing Then
            ' Check the maximu current ID from the DataGridView
            'Dim maxCurrentID As Integer = GetMaxCurrentID()
            'If maxCurrentID >= currentID Then
            '    currentID = maxCurrentID + 1
            'End If
            Dim IND As Integer = datagrdFid.SelectedRows(0).Index
            'Dim 
            currentID = IND + 1
            If shapes.Count < datagrdFid.Rows.Count Then ' Subtract 1 to account for the new row placeholder
                If drawSquares Then
                    ' Draw square as before
                    Dim size As New Size(Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y))
                    Dim topLeft As New Point(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y))
                    shapes.Add(New Square(topLeft, size, currentID, currentColor))
                    AddShapeToDataGridView(currentID, "Square", topLeft.X, topLeft.Y, size.Width, size.Height)
                Else
                    ' Draw circle with dynamic size
                    Dim radius As Integer = CInt(Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2)) / 2)
                    Dim centerX As Integer = Math.Min(startPoint.X, endPoint.X) + radius
                    Dim centerY As Integer = Math.Min(startPoint.Y, endPoint.Y) + radius
                    shapes.Add(New Circle(New Point(centerX, centerY), radius, currentID, currentColor))

                    AddShapeToDataGridView(currentID, "Circle", centerX - radius, centerY - radius, radius * 2, 0, centerX, centerY)
                End If

                currentID += 1
                isDrawing = False
                ' Guna2PictureBox1.Invalidate()
            Else

                MessageBox.Show("You cannot draw more shapes than the number of rows in the DataGridView.", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles GET_RESULT.Click
        Try
            ' Check if a row is selected
            If datagrdFid.SelectedRows.Count > 0 Then
                Dim foldername As String = RichTextBox14.Text

                'Get the selected row
                Dim fidTempImage1 As String = "" & ConfigurationManager.AppSettings("FiducialTemplate").ToString().Trim()
                Dim fidImage1 As String = "" & ConfigurationManager.AppSettings("FiducialImage").ToString().Trim()



                ' Create the directory if it doesn't exist






                Dim selectedRow As DataGridViewRow = datagrdFid.SelectedRows(0)

                Dim selectedShapeID As String = selectedRow.Cells(0).Value.ToString()
                Dim FINOD As String = selectedRow.Cells(12).Value.ToString()


                Dim tempdirec As String = Path.Combine(fidTempImage1, foldername, selectedShapeID, FINOD & ".png")
                Dim fiddirec As String = Path.Combine(fidImage1, foldername, selectedShapeID, FINOD & ".png")




                'Extract the value from column 6
                Dim column6Value As String = selectedRow.Cells(6).Value.ToString()
                Dim parts() As String = column6Value.Trim("()").Split(","c)
                'Dim cnter As String = VALUE(6).Split("(", ","c)
                'Dim part As String = "45)"
                'Dim part1() As String = parts(1).Trim("()")

                Dim trimmedPart() As String = parts(1).Split(")"c)

                Dim centx As Integer = Convert.ToInt16(parts(0))
                Dim centy As Integer = Convert.ToInt16(trimmedPart(0))
                centx = centx * 4
                centy = centy * 4
                Dim valu As String = ("(" & centx & "," & centy & ")")



                Using pipeServer As New NamedPipeServerStream("TestPipe", PipeDirection.InOut)
                    Console.WriteLine("Waiting for connection...")
                    pipeServer.WaitForConnection()

                    Dim message As String = tempdirec & "," & fiddirec
                    Dim buffer As Byte() = Encoding.UTF8.GetBytes(message)

                    pipeServer.Write(buffer, 0, buffer.Length)
                    Console.WriteLine("Message sent to client.")
                    Dim buffer1 As Byte() = New Byte(255) {}
                    pipeServer.Read(buffer1, 0, buffer1.Length)

                    Dim message1 As String = Encoding.UTF8.GetString(buffer1).TrimEnd(ChrW(0))
                    Console.WriteLine("Message received from server: " & message1)
                    Dim DATA() As String = message1.Split(",")

                    selectedRow.Cells(7).Value = DATA(0)
                    selectedRow.Cells(8).Value = DATA(1)
                    pipeServer.Disconnect()
                End Using








                ' Extract the name for the text file from the selected row (e.g., from column 1)
                'Dim fileName As String = "text" & ".txt"



                ' Define the file path
                ' Dim filePath As String = "C:\Manage Files\centre Point\" & fileName

                ' Write the value to the text file
                'Try
                '    System.IO.File.WriteAllText(filePath, valu)
                '    MessageBox.Show("Value stored in " & filePath)
                'Catch ex As Exception
                '    MessageBox.Show("Error writing to file: " & ex.Message)
                'End Try
            Else
                MessageBox.Show("Please select a row first.")
            End If
        Catch ex As Exception

        End Try




    End Sub

    'Sub Main1()
    '    Using pipeServer As New NamedPipeServerStream("TestPipe", PipeDirection.InOut)
    '        Console.WriteLine("Waiting for connection...")
    '        pipeServer.WaitForConnection()

    '        Dim message As String = "C:\Users\NMT-Sudarshan\Desktop\EBW8Image8.jpg,C:\Users\NMT-Sudarshan\Desktop\EBW8Image1.jpg"
    '        Dim buffer As Byte() = Encoding.UTF8.GetBytes(message)

    '        pipeServer.Write(buffer, 0, buffer.Length)
    '        Console.WriteLine("Message sent to client.")
    '        Dim buffer1 As Byte() = New Byte(255) {}
    '        pipeServer.Read(buffer1, 0, buffer1.Length)

    '        Dim message1 As String = Encoding.UTF8.GetString(buffer1).TrimEnd(ChrW(0))
    '        Console.WriteLine("Message received from server: " & message1)
    '        Dim DATA() As String = message1.Split(",")
    '        TextBox1.Text = DATA(0)
    '        TextBox2.Text = DATA(1)
    '        pipeServer.Disconnect()
    '    End Using
    'End Sub

    Private Sub datagrdFid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles datagrdFid.CellClick

        ' TextBox4.Text = datagrdFid.SelectedRows(0).Cells(13).Value
        SCORE.Text = datagrdFid.SelectedRows(0).Cells(11).Value

    End Sub

    Private Sub Button24_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M200", 1)
    End Sub

    Private Sub Button24_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M200", 0)
    End Sub

    Private Sub Button23_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M201", 1)
    End Sub

    Private Sub Button23_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M201", 0)
    End Sub

    Private Sub Button21_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M206", 1)
    End Sub

    Private Sub Button21_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M206", 0)
    End Sub

    Private Sub Button22_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M205", 1)
    End Sub

    Private Sub Button22_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M205", 0)
    End Sub

    Private Sub Guna2ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Guna2ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim selectedValue As String = SPEED_1.SelectedItem.ToString()
        Select Case selectedValue
            Case "HIGH"
                plc.SetDevice("D358", 3)
            Case "MEDIUM"
                plc.SetDevice("D358", 2)
            Case "LOW"
                plc.SetDevice("D358", 1)
        End Select
    End Sub



    Private Sub btLoadpos_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub RadioButton2_MouseDown(sender As Object, e As MouseEventArgs) Handles STOPPER_UP_DOWN.MouseDown
        plc.SetDevice("M237", 1)
    End Sub

    Private Sub RadioButton2_MouseUp(sender As Object, e As MouseEventArgs) Handles STOPPER_UP_DOWN.MouseUp
        plc.SetDevice("M237", 0)
    End Sub



    Private Sub LiveTriggerOnce_Click(sender As Object, e As EventArgs)
        Dim nRet As Int32
        nRet = Home_Page.LiveCamera1.SetCommandValue("TriggerSoftware")
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs)
        'If LiveTriggerOn.Checked Then
        plc.SetDevice("M247", 1)  ''''''''''' RED LIGHT
        plc.SetDevice("M246", 0)
        'Thread.Sleep(100)
        'Else
        plc.SetDevice("M247", 0)
        ' End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs)
        ''If FID_1.Checked Then
        plc.SetDevice("M246", 1)
            plc.SetDevice("M247", 0)
        '' Else
        ''plc.SetDevice("M246", 0)
        '''End If

    End Sub







    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick



    End Sub

    Private Async Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If TabControl1.SelectedIndex > 1 Then
            If (FID_CAMERA.Checked = True) Then
                Await FidAsync()
            ElseIf (LIVE_CAM.Checked = True) Or (live_1.Checked = True) Then
                Await lIVEAsync()
            End If
        End If

    End Sub
    Private Async Function lIVEAsync() As Task
        Dim stFrameOut As CCamera.MV_FRAME_OUT = New CCamera.MV_FRAME_OUT()
        Dim stDisplayInfo As CCamera.MV_DISPLAY_FRAME_INFO = New CCamera.MV_DISPLAY_FRAME_INFO()
        Dim nRet As Int32 = Home_Page.FidCam1.GetImageBuffer(stFrameOut, 1000)

        If CCamera.MV_OK = nRet Then

            If stFrameOut.stFrameInfo.nFrameLen > m_nBufSizeForDriver Then
                m_nBufSizeForDriver = stFrameOut.stFrameInfo.nFrameLen
                ReDim m_pBufForDriver(m_nBufSizeForDriver)
            End If
            m_stFrameInfoEx = stFrameOut.stFrameInfo
            Marshal.Copy(stFrameOut.pBufAddr, m_pBufForDriver, 0, stFrameOut.stFrameInfo.nFrameLen)

            If TabControl1.SelectedIndex = 2 Then

                stDisplayInfo.hWnd = PictureBox7.Handle
            End If
            If TabControl1.SelectedIndex = 3 Then
                stDisplayInfo.hWnd = PictureBox2.Handle
            End If
            stDisplayInfo.pData = stFrameOut.pBufAddr
            stDisplayInfo.nDataLen = stFrameOut.stFrameInfo.nFrameLen
            stDisplayInfo.nWidth = stFrameOut.stFrameInfo.nWidth
            stDisplayInfo.nHeight = stFrameOut.stFrameInfo.nHeight
            stDisplayInfo.enPixelType = stFrameOut.stFrameInfo.enPixelType
            Home_Page.FidCam1.DisplayOneFrame(stDisplayInfo)
            Home_Page.FidCam1.FreeImageBuffer(stFrameOut)

        End If
    End Function

    Private Async Function FidAsync() As Task
        Dim stFrameOut As CCamera.MV_FRAME_OUT = New CCamera.MV_FRAME_OUT()
        Dim stDisplayInfo As CCamera.MV_DISPLAY_FRAME_INFO = New CCamera.MV_DISPLAY_FRAME_INFO()
        Dim nRet1 As Int32 = Home_Page.FidCam1.GetImageBuffer(stFrameOut, 1000)
        If CCamera.MV_OK = nRet1 Then

            If stFrameOut.stFrameInfo.nFrameLen > m_nBufSizeForDriver1 Then
                m_nBufSizeForDriver1 = stFrameOut.stFrameInfo.nFrameLen
                ReDim m_pBufForDriver1(m_nBufSizeForDriver1)
            End If

            m_stFrameInfoEx = stFrameOut.stFrameInfo
            Marshal.Copy(stFrameOut.pBufAddr, m_pBufForDriver1, 0, stFrameOut.stFrameInfo.nFrameLen)
            Dim IMAGEPATH1 As String = "" & ConfigurationManager.AppSettings("FiducialImage").ToString().Trim()
            Dim stSaveImageParam As CCamera.MV_SAVE_IMG_TO_FILE_PARAM = New CCamera.MV_SAVE_IMG_TO_FILE_PARAM()
            Dim pData As IntPtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver1, 0)
            stSaveImageParam.pData = pData
            stSaveImageParam.nDataLen = m_stFrameInfoEx.nFrameLen
            stSaveImageParam.enPixelType = m_stFrameInfoEx.enPixelType
            stSaveImageParam.nWidth = m_stFrameInfoEx.nWidth
            stSaveImageParam.nHeight = m_stFrameInfoEx.nHeight
            stSaveImageParam.enImageType = CCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Png
            stSaveImageParam.iMethodValue = 2
            stSaveImageParam.nQuality = 90
            stSaveImageParam.pImagePath = IMAGEPATH1 & "123" & ".Png"
            Dim fid_img_path As String = IMAGEPATH1 & "123" & ".Png"
            Thread.Sleep(5)

            nRet1 = Home_Page.FidCam1.SaveImageToFile(stSaveImageParam)


            Home_Page.FidCam1.DisplayOneFrame(stDisplayInfo)

            Home_Page.FidCam1.FreeImageBuffer(stFrameOut)

            '/'''''''''' gray scale image


            Dim pic As Bitmap = New Bitmap(fid_img_path)
            Dim x As Integer
            Dim y As Integer
            x = pic.Width
            y = pic.Height
            Dim gray = New Bitmap(pic.Width, pic.Height)
            Dim red As Integer
            Dim green As Integer
            Dim blue As Integer
            For x = 0 To (pic.Width) - 1
                For y = 0 To (pic.Height) - 1
                    Dim r As Color = pic.GetPixel(x, y)
                    red = r.R
                    green = r.G
                    blue = r.B
                    Dim d As Integer
                    d = CInt((red + green + blue) / 3)
                    Dim c1 As Integer = CInt(Math.Round(d))
                    gray.SetPixel(x, y, Color.FromArgb(c1, c1, c1))
                    img = gray
                Next
            Next

            Dim pictshow As String = IMAGEPATH1 & "Image_w1" & ".Png"

            Dim ss As Boolean = File.Exists(fid_img_path)
            'Dim ss1 As Boolean = File.Exists("C:\Manage Files\Load\Image_w1.Png")

            If ss = True Then



                If TabControl1.SelectedIndex = 2 Then

                    Dim newImg As New Bitmap(PictureBox7.Width, PictureBox7.Height)

                    ' Draw the resized image
                    Using g As Graphics = Graphics.FromImage(newImg)
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic
                        g.DrawImage(img, 0, 0, newImg.Width, newImg.Height)
                    End Using
                    newImg.Save(pictshow)

                    PictureBox7.LoadAsync(pictshow)

                    newImg.Dispose()

                End If
                If TabControl1.SelectedIndex = 3 Then
                    Dim newImg1 As New Bitmap(PictureBox2.Width, PictureBox2.Height)

                    ' Draw the resized image
                    Using g As Graphics = Graphics.FromImage(newImg1)
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic
                        g.DrawImage(img, 0, 0, PictureBox2.Width, PictureBox2.Height)
                    End Using
                    newImg1.Save(pictshow)

                    PictureBox2.LoadAsync(pictshow)

                    newImg1.Dispose()

                End If

                img.Dispose()



                'Dim newImg As New Bitmap(PictureBox7.Width, PictureBox7.Height)

                '' Draw the resized image
                'Using g As Graphics = Graphics.FromImage(newImg)
                '    g.InterpolationMode = InterpolationMode.HighQualityBicubic
                '    g.DrawImage(img, 0, 0, PictureBox7.Width, PictureBox7.Height)
                'End Using
                'newImg.Save("C:\Manage Files\Load\Image_w1.Png")

                'PictureBox7.LoadAsync("C:\Manage Files\Load\Image_w1.Png")

                'newImg.Dispose()
                'img.Dispose()

            End If




            pic.Dispose()


        End If

    End Function



    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim progname As String
        progname = txt_Sel_Prog_name.Text
        If progname = "" Then
            Return
        ElseIf (progname IsNot "") Then
            Dim isValid1 As Boolean = True
            Dim isValid As Boolean = True
            Dim fname As String = "" & ConfigurationManager.AppSettings("ReceipeFilepath").ToString().Trim()
            Dim Defaultfname As String = "" & ConfigurationManager.AppSettings("DefaultPath").ToString().Trim()
            Dim path As String = fname
            Dim Logdir As String = "" & fname
            Dim DefaultLogdir As String = "" & Defaultfname
            Dim ReceipeFileName As String = String.Empty
            If Not Directory.Exists(DefaultLogdir) Then
                Directory.CreateDirectory(DefaultLogdir)
            End If
            If Not Directory.Exists(Logdir) Then
                Directory.CreateDirectory(Logdir)
            End If
            Dim files As String() = Directory.GetFiles(DefaultLogdir, "*.xml", System.IO.SearchOption.AllDirectories)
            'File.Delete(DefaultLogdir)
            For Each file1 As String In files
                File.Delete(file1)
            Next

            ReceipeFileName = progname
            Dim generatedFile As String = Logdir & ReceipeFileName & ".xml"
            Dim DefaultgeneratedFile As String = DefaultLogdir & "Defualt" & ".xml"

            Dim xmDocument As XmlDocument = New XmlDocument()
            xmDocument.Load(generatedFile)

            xmDocument.Save(DefaultgeneratedFile)
            My.Settings.ProgramName = txt_Sel_Prog_name.Text
            My.Settings.Save()

            rtxtcurrentpg.Text = txt_Sel_Prog_name.Text
            MessageBox.Show("Recipe Download Complete")
            loadData()

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim progname As String
        progname = txt_Sel_Prog_name.Text
        If progname = "" Then
            Return
        ElseIf (progname IsNot "") Then













            Dim fname As String = "" & ConfigurationManager.AppSettings("ReceipeFilepath").ToString().Trim()
            Dim Backupfname As String = "" & ConfigurationManager.AppSettings("BackupReceipeFilepath").ToString().Trim()

            '  Dim XMLOutPutPath As String = "D:\LM- Test/"

            Dim path As String = fname
            Dim Logdir As String = "" & fname
            Dim BackupLogdir As String = "" & Backupfname
            Dim ReceipeFileName As String = String.Empty

            If Not Directory.Exists(BackupLogdir) Then
                Directory.CreateDirectory(BackupLogdir)
            End If
            If Not Directory.Exists(Logdir) Then
                Directory.CreateDirectory(Logdir)
            End If


            ReceipeFileName = progname

            Dim generatedFile As String
            Dim BACK As String
            Dim generatedFile1 As String = Logdir & ReceipeFileName & ".xml"
            Dim xmDocument As XmlDocument = New XmlDocument()
            xmDocument.Load(generatedFile1)










            Dim BaackupgeneratedFile As String = BackupLogdir & ReceipeFileName & ".xml"
            Dim nam As String = ReceipeFileName & ".xml"

            Dim _LaserHeadLocation As String() = Directory.GetFiles(Logdir, nam, System.IO.SearchOption.AllDirectories)
            Dim count As Integer = _LaserHeadLocation.Count()
            ReceipeFileName = progname & "_" & count


            generatedFile = Logdir & ReceipeFileName & ".xml"
            BACK = BackupLogdir & ReceipeFileName & ".xml"

            Dim LASTSAVING As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/LASTSAVINGTIME")

            For Each _PLCTAGnode As XmlNode In LASTSAVING
                _PLCTAGnode.ChildNodes(0).InnerText = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")


            Next




            Dim RECEIPENAMEnodes As XmlNodeList = xmDocument.SelectNodes("JOBList/JOB/TAGTYPE/RECEIPENAME")




            For Each _PLCTAGnodes As XmlNode In RECEIPENAMEnodes
                _PLCTAGnodes.ChildNodes(0).InnerText = ReceipeFileName
                _PLCTAGnodes.ChildNodes(1).InnerText = generatedFile

            Next





            xmDocument.Save(generatedFile)



            xmDocument.Save(BACK)

            LoadReceipe()
        End If

    End Sub

    Private Sub Guna2ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles Guna2ComboBox1.SelectedIndexChanged
        Dim selectedValue As String = Guna2ComboBox1.SelectedItem.ToString()
        Select Case selectedValue
            Case "HIGH"
                plc.SetDevice("D358", 3)
            Case "MEDIUM"
                plc.SetDevice("D358", 2)
            Case "LOW"
                plc.SetDevice("D358", 1)
        End Select

    End Sub

    Private Sub Guna2ComboBox2_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles Guna2ComboBox2.SelectedIndexChanged
        Dim selectedValue As String = Guna2ComboBox2.SelectedItem.ToString()
        Select Case selectedValue
            Case "0.1MM"
                plc.SetDevice("D356", 3)
            Case "1MM"
                plc.SetDevice("D356", 2)
            Case "10MM"
                plc.SetDevice("D356", 1)
            Case "CONITNUES"
                plc.SetDevice("D356", 0)
        End Select
    End Sub

    Private Sub Guna2ComboBox4_SelectedIndexChanged_1(sender As Object, e As EventArgs)
        Dim selectedValue As String = Guna2ComboBox1.SelectedItem.ToString()
        Select Case selectedValue
            Case "HIGH"
                plc.SetDevice("D358", 3)
            Case "MEDIUM"
                plc.SetDevice("D358", 2)
            Case "LOW"
                plc.SetDevice("D358", 1)
        End Select
    End Sub








    Private Sub HEAD_PARK_MouseDown(sender As Object, e As MouseEventArgs) Handles HEAD_PARK.MouseDown
        Dim lengthInt As Integer
        If Integer.TryParse(Panel_Length.Text, lengthInt) AndAlso lengthInt < 525 AndAlso Panel_Width.Text < 510 Then
            plc.SetDevice("M252", 1)
        Else
            MessageBox.Show("Values are not in ")
        End If
    End Sub

    Private Sub HEAD_PARK_MouseUp(sender As Object, e As MouseEventArgs) Handles HEAD_PARK.MouseUp
        plc.SetDevice("M252", 0)
    End Sub

    Private Sub LOAD_POSITION_MouseDown(sender As Object, e As MouseEventArgs) Handles LOAD_POSITION.MouseDown
        plc.SetDevice("M230", 1)
    End Sub

    Private Sub LOAD_POSITION_MouseUp(sender As Object, e As MouseEventArgs) Handles LOAD_POSITION.MouseUp
        plc.SetDevice("M230", 0)
    End Sub

    Private Sub CONV_HOME_MouseDown(sender As Object, e As MouseEventArgs) Handles CONV_HOME.MouseDown
        plc.SetDevice("M254", 1)
    End Sub

    Private Sub CONV_HOME_MouseUp(sender As Object, e As MouseEventArgs) Handles CONV_HOME.MouseUp
        plc.SetDevice("M254", 0)
    End Sub

    Private Sub FLIP_ON_MouseDown(sender As Object, e As MouseEventArgs) Handles FLIP_ON.MouseDown
        plc.SetDevice("M253", 1)
    End Sub

    Private Sub FLIP_ON_MouseUp(sender As Object, e As MouseEventArgs) Handles FLIP_ON.MouseUp
        plc.SetDevice("M253", 0)
    End Sub

    Private Sub Y_REV_MouseDown(sender As Object, e As MouseEventArgs) Handles Button47.MouseDown, Y_FWD_1.MouseDown, Y_FWD.MouseDown
        plc.SetDevice("M201", 1)
    End Sub

    Private Sub Y_REV_MouseUp(sender As Object, e As MouseEventArgs) Handles Button47.MouseUp, Y_FWD_1.MouseUp, Y_FWD.MouseUp
        plc.SetDevice("M201", 0)
    End Sub

    Private Sub Y_FWD_MouseDown(sender As Object, e As MouseEventArgs) Handles Button48.MouseDown, Y_REV_1.MouseDown, Y_REV.MouseDown
        plc.SetDevice("M200", 1)
    End Sub

    Private Sub Y_FWD_MouseUp(sender As Object, e As MouseEventArgs) Handles Button48.MouseUp, Y_REV_1.MouseUp, Y_REV.MouseUp
        plc.SetDevice("M200", 0)
    End Sub

    Private Sub X_REV_MouseDown(sender As Object, e As MouseEventArgs) Handles X_REV.MouseDown, X_REV_1.MouseDown, Button46.MouseDown
        plc.SetDevice("M206", 1)
    End Sub

    Private Sub X_REV_MouseUp(sender As Object, e As MouseEventArgs) Handles X_REV.MouseUp, X_REV_1.MouseUp, Button46.MouseUp
        plc.SetDevice("M206", 0)
    End Sub

    Private Sub X_POSI_MouseDown(sender As Object, e As MouseEventArgs) Handles X_POSI.MouseDown, X_FWD_1.MouseDown, Button45.MouseDown
        plc.SetDevice("M205", 1)
    End Sub

    Private Sub X_POSI_MouseUp(sender As Object, e As MouseEventArgs) Handles X_POSI.MouseUp, X_FWD_1.MouseUp, Button45.MouseUp
        plc.SetDevice("M205", 0)
    End Sub

    Private Sub CONV_FWD_MouseDown(sender As Object, e As MouseEventArgs) Handles CONV_FWD.MouseDown, CONV_FWD_1.MouseDown, Button49.MouseDown
        plc.SetDevice("M250", 1)
    End Sub

    Private Sub CONV_FWD_MouseUp(sender As Object, e As MouseEventArgs) Handles CONV_FWD.MouseUp, CONV_FWD_1.MouseUp, Button49.MouseUp
        plc.SetDevice("M250", 0)
    End Sub

    Private Sub CONV_REV_MouseDown(sender As Object, e As MouseEventArgs) Handles CONV_REV.MouseDown, CONV_REV_1.MouseDown, Button50.MouseDown
        plc.SetDevice("M249", 1)
    End Sub

    Private Sub CONV_REV_MouseUp(sender As Object, e As MouseEventArgs) Handles CONV_REV.MouseUp, CONV_REV_1.MouseUp, Button50.MouseUp
        plc.SetDevice("M249", 0)
    End Sub

    Private Sub LED_LIGHT_ON_MouseDown(sender As Object, e As MouseEventArgs) Handles LED_LIGHT_ON.MouseDown
        plc.SetDevice("M256", 1)
    End Sub

    Private Sub LED_LIGHT_ON_MouseUp(sender As Object, e As MouseEventArgs) Handles LED_LIGHT_ON.MouseUp
        plc.SetDevice("M256", 0)
    End Sub




    Private Sub LIVE_CAM_CheckedChanged(sender As Object, e As EventArgs) Handles LIVE_CAM.CheckedChanged, live_1.CheckedChanged
        If LIVE_CAM.Checked = True Or live_1.Checked = True Then
            Dim nRet As Int32 = Home_Page.FidCam1.SetEnumValue("TriggerSource", CCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0)
            nRet = Home_Page.FidCam1.SetEnumValue("TriggerMode", CCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF)
            FID_CAMERA.Checked = False
            ''FID_1.Checked = False
        End If
    End Sub

    Private Sub FID_CAMERA_CheckedChanged(sender As Object, e As EventArgs) Handles FID_CAMERA.CheckedChanged
        If FID_CAMERA.Checked = True Then
            Dim nRet As Int32 = Home_Page.FidCam1.SetEnumValue("TriggerSource", CCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE)
            nRet = Home_Page.FidCam1.SetEnumValue("TriggerMode", CCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON)
            LIVE_CAM.Checked = False
            live_1.Checked = False
        End If
    End Sub

    Private Sub TRIGGERBTN_Click(sender As Object, e As EventArgs)
        'If ''FID_1.Checked = True Or LIVE_CAM.Checked = True Then
        '    Dim nRet As Int32
        '    nRet = Home_Page.FidCam1.SetCommandValue("TriggerSoftware")
        'End If
    End Sub


    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''
    '''''''     POSITION DATA '''''''''

    Private Sub LOADPOSISATA(filePath As String)
        TreeView1.Nodes.Clear()


        Dim xmlDoc As New XmlDocument()
        xmlDoc.Load(filePath)

        Dim rootElement As XmlElement = xmlDoc.DocumentElement

        ' Traverse each SIDE element
        For Each sideElement As XmlElement In rootElement.GetElementsByTagName("SIDE")
            ' Create a new TreeNode for SIDE and set the text (e.g., SIDE [FRONT])
            Dim sideName As String = sideElement.GetAttribute("name")
            Dim sideNode As New TreeNode($"SIDE [{sideName}]")

            ' Traverse each POSI element within SIDE
            For Each posiElement As XmlElement In sideElement.GetElementsByTagName("POSI")
                ' Extract POSI attributes (number, X, Y)
                Dim posiNumber As String = posiElement.GetAttribute("number")
                Dim posX As String = posiElement.GetAttribute("X")
                Dim posY As String = posiElement.GetAttribute("Y")

                ' Create a new TreeNode for POSI (e.g., POSI [1] [X:234][Y:130])
                Dim posiNode As New TreeNode($"POSI [{posiNumber}] [X:{posX}][Y:{posY}]")

                ' Traverse each CODE element within POSI
                For Each codeElement As XmlElement In posiElement.GetElementsByTagName("CODE")
                    ' Create a new TreeNode for CODE and set the text (e.g., (CODE)<YEAR:2:0:0>...)
                    Dim codeText As String = codeElement.InnerText
                    Dim codeNode As New TreeNode($"(CODE){codeText}")



                    Dim codeT As String
                    Dim codeD As String
                    ' Add CODE node under POSI node
                    posiNode.Nodes.Add(codeNode)

                    For Each codeTElement As XmlElement In posiElement.GetElementsByTagName("CODETYPE")

                        codeT = codeTElement.InnerText
                    Next

                    For Each codeDElement As XmlElement In posiElement.GetElementsByTagName("CODEDATA")

                        codeD = codeDElement.InnerText
                    Next



                    codeNode.Tag = New With {
                          .CODE_TYPE = codeT,
                          .CODE_DATA = codeD
                                  }


                Next
                Dim PROGID As String
                Dim LSRPWR As String
                Dim SCANSPD As String
                Dim CODN As String
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

                    CODN = codeSElement.InnerText
                Next


                posiNode.Tag = New With {
        .Program_Id = PROGID,
        .Laser_Power = LSRPWR,
        .Scan_Speed = SCANSPD,
        .CODENO = CODN
         }

                ' Add POSI node under SIDE node
                sideNode.Nodes.Add(posiNode)
            Next

            ' Add SIDE node to TreeView
            TreeView1.Nodes.Add(sideNode)
        Next

        ' Expand the tree to show all nodes
        TreeView1.ExpandAll()

    End Sub

    Private Sub ADDDATA()

        Dim side As String = BOARD_DIRECTION.SelectedItem.ToString()

        ' Get the position values from TextBoxes
        Dim posX As String = X_Current2.Text
        Dim posY As String = Y_Current2.Text

        Dim programID As String = RichTextBox18.Text
        Dim laserpower As String = RichTextBox16.Text
        Dim scanspeed As String = RichTextBox17.Text




        ' Search for the SIDE node (FRONT or BACK)
        Dim sideNode As TreeNode = Nothing
        For Each node As TreeNode In TreeView1.Nodes
            If node.Text = $"SIDE [{side}]" Then
                sideNode = node
                Exit For
            End If
        Next

        ' If SIDE node does not exist, create it
        If sideNode Is Nothing Then
            sideNode = New TreeNode($"SIDE [{side}]")
            TreeView1.Nodes.Add(sideNode)
        End If

        ' Determine the position number based on existing POSI nodes
        Dim positionNumber As Integer = sideNode.Nodes.Count + 1
        Dim positionNode As New TreeNode($"POSI [{positionNumber}] [X:{posX}][Y:{posY}]")

        positionNode.Tag = New With {
        .Program_Id = programID,
        .Laser_Power = laserpower,
        .Scan_Speed = scanspeed,
        .CODENO = RichTextBox5.Text
         }

        ' Add the POSI node under SIDE
        sideNode.Nodes.Add(positionNode)

        ' Expand the side node to show the newly added data
        positionNode.ExpandAll()

    End Sub










    Private Sub SaveTreeViewToXml(filePath As String, backup As String)
        ' Create a new XML document


        Dim xmlDoc As New XmlDocument()
        xmlDoc.Load(filePath)
        Dim MARKPOSITION As XmlNode = xmlDoc.SelectSingleNode("JOBList/JOB/TAGTYPE/RECEIPE/MARKPOSITION")

        MARKPOSITION.RemoveAll()

        xmlDoc.Save(filePath)





        For Each sideNode As TreeNode In TreeView1.Nodes
            ' Create SIDE element
            Dim sideElement As XmlElement = xmlDoc.CreateElement("SIDE")
            sideElement.SetAttribute("name", sideNode.Text.Replace("SIDE [", "").Replace("]", "")) ' Extracting FRONT or BACK
            MARKPOSITION.AppendChild(sideElement)

            ' Add POSI and CODE nodes
            For Each posiNode As TreeNode In sideNode.Nodes
                ' Create POSI element
                Dim posiElement As XmlElement = xmlDoc.CreateElement("POSI")

                ' Extract position number and coordinates from text
                Dim posiInfo As String = posiNode.Text
                Dim positionPattern As String = "POSI \[(\d+)\] \[X:([\d\.-]+)\]\[Y:([\d\.-]+)\]"
                ' Dim match As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(posiInfo, positionPattern)
                Dim regex As New System.Text.RegularExpressions.Regex(positionPattern)

                ' Match the input string with the pattern
                Dim match As System.Text.RegularExpressions.Match = regex.Match(posiInfo)

                If match.Success Then
                    posiElement.SetAttribute("number", match.Groups(1).Value)
                    posiElement.SetAttribute("X", match.Groups(2).Value)
                    posiElement.SetAttribute("Y", match.Groups(3).Value)
                End If
                sideElement.AppendChild(posiElement)

                ' Add CODE child node under POSI
                For Each codeNode As TreeNode In posiNode.Nodes
                    ' Create CODE element
                    Dim codeElement As XmlElement = xmlDoc.CreateElement("CODE")
                    codeElement.InnerText = codeNode.Text.Replace("(CODE)", "")
                    posiElement.AppendChild(codeElement)
                    Dim tagData1 = codeNode.Tag
                    Dim CODETYP As XmlElement = xmlDoc.CreateElement("CODETYPE")
                    Dim CODEDAT As XmlElement = xmlDoc.CreateElement("CODEDATA")
                    CODETYP.InnerText = tagData1.CODE_TYPE
                    CODEDAT.InnerText = tagData1.CODE_DATA
                    posiElement.AppendChild(CODETYP)
                    posiElement.AppendChild(CODEDAT)
                Next
                Dim tagData = posiNode.Tag
                Dim PROGID As XmlElement = xmlDoc.CreateElement("PROGRAMID")
                Dim LASERPWR As XmlElement = xmlDoc.CreateElement("LASERPOWER")
                Dim SCANSPED As XmlElement = xmlDoc.CreateElement("SCANSPEED")
                Dim CODEN As XmlElement = xmlDoc.CreateElement("CODENO")

                PROGID.InnerText = tagData.Program_Id
                LASERPWR.InnerText = tagData.Laser_Power
                SCANSPED.InnerText = tagData.Scan_Speed
                CODEN.InnerText = tagData.CODENO

                posiElement.AppendChild(PROGID)

                posiElement.AppendChild(LASERPWR)

                posiElement.AppendChild(SCANSPED)

                posiElement.AppendChild(CODEN)






            Next
        Next

        ' Save the XML document to the specified file path
        xmlDoc.Save(filePath)
        xmlDoc.Save(backup)
    End Sub
    Private Sub DeleteSelectedNode()
        ' Check if a node is selected
        If TreeView1.SelectedNode IsNot Nothing Then
            ' Optionally, ask the user for confirmation before deleting
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete the selected node?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ' Remove the selected node
                TreeView1.SelectedNode.Remove()
                MessageBox.Show("Node deleted successfully!")
            End If
        Else
            MessageBox.Show("Please select a node to delete.")
        End If
    End Sub

    ' Trigger this function when the "Delete" button is clicked










    Private Sub RichTextBox21_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox21.TextChanged

        If TreeView1.SelectedNode IsNot Nothing Then
            If RichTextBox21.Text IsNot "" Then
                Dim RED As String = RichTextBox21.Text
                RichTextBox1.Text = ""

                Dim pattern As String = "<[^>]+>|[a-zA-Z]+"

                ' Perform the regex match
                Dim matches As MatchCollection = Regex.Matches(RED, pattern)



                Dim DigitCode As String
                For Each match As Match In matches
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

                            Dim data As String
                            Dim data1 As String
                            Dim DDD1 As Integer
                            ' Display the result in a TextBox or use in any logic
                            If part1 = "YEAR" Then

                                data1 = DateTime.Now.Year

                                ' Display the current year in a TextBox or use it in any logic
                                If part21 < 4 Then
                                    DDD1 = 4 - part21
                                    data = data1.ToString().Substring(DDD1)
                                Else
                                    data = data1.ToString()
                                End If




                            ElseIf part1 = "MONTH" Then
                                data1 = DateTime.Now.Month

                                data = data1.ToString()
                            ElseIf part1 = "DAY" Then
                                data1 = DateTime.Now.Day

                                data = data1.ToString()

                            ElseIf part1 = "HOUR" Then
                                data1 = DateTime.Now.Hour

                                data = data1.ToString()
                            ElseIf part1 = "MINUTE" Then
                                data1 = DateTime.Now.Minute

                                data = data1.ToString()
                            ElseIf part1 = "SEC" Then
                                data1 = DateTime.Now.Second

                                data = data1.ToString()
                            ElseIf part1 = "COUNTER1" Then
                                data = Convert.ToString(Start_Count.Text)

                            ElseIf part1 = "COUNTER2" Then
                                data = Convert.ToString(Start_Count.Text)

                            End If



                            DigitCode = data.PadLeft(part21, "0"c)
                        Else
                            DigitCode = match.Value
                        End If
                    Else
                        DigitCode = match.Value
                    End If

                    RichTextBox1.Text &= DigitCode
                Next


            End If
            RichTextBox4.Text = RichTextBox1.Text.Length



            Dim selectedNode As TreeNode = TreeView1.SelectedNode

            ' Text for the new or updated child node
            Dim newCodeText As String = $"(CODE) {RichTextBox21.Text}"



            If TreeView1.SelectedNode.Text.StartsWith("(CODE)") Then

                TreeView1.SelectedNode.Text = newCodeText
            End If



            ' Expand the parent node to show the newly added/updated child node
            selectedNode.Expand()
        Else
            ' Handle the case where no node is selected
            MessageBox.Show("Please select a node before adding a new one.")
        End If

    End Sub





    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim selectedNode1 As TreeNode = e.Node
        Dim PARENT As String
        Dim CHILD As String
        Dim parentNode As TreeNode
        If selectedNode1.Parent IsNot Nothing Then

            parentNode = selectedNode1.Parent

            PARENT = parentNode.Text

            Dim parts() As String = PARENT.Split(" "c)

            ' Extract the main part and the value inside the brackets
            Dim mainPart As String = parts(0) ' "SIDE"
            Dim valueInsideBrackets As String = parts(1).Trim("["c, "]"c) ' "TOP"


            BOARD_DIRECTION.SelectedItem = valueInsideBrackets




            CHILD = selectedNode1.Text


        End If


        If PARENT IsNot Nothing AndAlso ((PARENT.StartsWith("BOTTOM")) Or (PARENT.StartsWith("TOP"))) Then
            BOARD_DIRECTION.SelectedItem = PARENT
        ElseIf parentNode IsNot Nothing AndAlso parentNode.Parent IsNot Nothing Then

            Dim DDD As TreeNode = parentNode.Parent
            Dim PP As String = DDD.Text

            Dim parts() As String = PP.Split(" "c)

            ' Extract the main part and the value inside the brackets
            Dim mainPart As String = parts(0) ' "SIDE"
            Dim valueInsideBrackets As String = parts(1).Trim("["c, "]"c) ' "TOP"


            BOARD_DIRECTION.SelectedItem = valueInsideBrackets
        End If
        If PARENT IsNot Nothing AndAlso PARENT.StartsWith("POSI") Then





            ''Dim inputString As String = e.Node.Text
            'Dim positionPattern As String = "POSI \[(\d+)\] \[X:([\d\.-]+)\]\[Y:([\d\.-]+)\]"
            ' Regular expression pattern to capture POSI, X, and Y values
            Dim pattern As String = "POSI \[(\d+)\] \[X:([\d\.-]+)\]\[Y:([\d\.-]+)\]"
            Dim match As Match = Regex.Match(PARENT, pattern)

            If match.Success Then
                ' Extracted values
                Dim posi As String = match.Groups(1).Value  ' POSI number
                Dim xValue As String = match.Groups(2).Value ' X coordinate
                Dim yValue As String = match.Groups(3).Value ' Y coordinate

                ' Display or use the extracted values
                X_Current2.Text = xValue
                Y_Current2.Text = yValue




                If parentNode IsNot Nothing AndAlso parentNode.Tag IsNot Nothing Then
                    Dim tagData = parentNode.Tag
                    RichTextBox18.Text = tagData.Program_Id
                    RichTextBox16.Text = tagData.Laser_Power
                    RichTextBox17.Text = tagData.Scan_Speed
                    RichTextBox5.Text = tagData.CODENO
                End If


            End If

        ElseIf CHILD IsNot Nothing AndAlso (CHILD.StartsWith("POSI")) Then

            Dim pattern As String = "POSI \[(\d+)\] \[X:([\d\.-]+)\]\[Y:([\d\.-]+)\]"
            Dim match As Match = Regex.Match(CHILD, pattern)

            If match.Success Then
                ' Extracted values
                Dim posi As String = match.Groups(1).Value  ' POSI number
                Dim xValue As String = match.Groups(2).Value ' X coordinate
                Dim yValue As String = match.Groups(3).Value ' Y coordinate

                ' Display or use the extracted values
                X_Current2.Text = xValue
                Y_Current2.Text = yValue
                If selectedNode1 IsNot Nothing AndAlso selectedNode1.Tag IsNot Nothing Then
                    Dim tagData = selectedNode1.Tag

                    RichTextBox18.Text = tagData.Program_Id
                    RichTextBox16.Text = tagData.Laser_Power
                    RichTextBox17.Text = tagData.Scan_Speed
                    RichTextBox5.Text = tagData.CODENO

                End If

            End If



        End If
        If CHILD IsNot Nothing AndAlso CHILD.StartsWith("(CODE)") Then





            Dim inputString As String = e.Node.Text
            Dim trimmedString As String = inputString.Replace("(CODE)", "").Trim()
            ' Regular expression pattern to capture POSI, X, and Y values

            If selectedNode1 IsNot Nothing AndAlso selectedNode1.Tag IsNot Nothing Then
                Dim tagData = selectedNode1.Tag

                Guna2ComboBox7.SelectedIndex = Convert.ToInt16(tagData.CODE_TYPE)


                'RichTextBox1.Text = tagData.CODE_DATA


            End If

            RichTextBox21.Text = trimmedString




        End If




    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        ADDDATA()
    End Sub

    Private Sub Button55_Click(sender As Object, e As EventArgs) Handles Button55.Click
        Dim progname As String

        progname = RichTextBox15.Text



        'Dim filePath As String = "D:/Logs/Recipe/TEST1.xml"
        Dim fname As String = "" & ConfigurationManager.AppSettings("ReceipeFilepath").ToString().Trim()
        Dim Backupfname As String = "" & ConfigurationManager.AppSettings("BackupReceipeFilepath").ToString().Trim()

        Dim FILEPATH As String = fname & progname & ".xml"
        Dim backup As String = Backupfname & progname & ".xml"
        ' Save the TreeView data to XML
        SaveTreeViewToXml(FILEPATH, backup)

    End Sub

    Private Sub Button54_Click(sender As Object, e As EventArgs) Handles Button54.Click
        DeleteSelectedNode()
        Dim progname As String

        progname = RichTextBox15.Text



        'Dim filePath As String = "D:/Logs/Recipe/TEST1.xml"
        Dim fname As String = "" & ConfigurationManager.AppSettings("ReceipeFilepath").ToString().Trim()
        Dim Backupfname As String = "" & ConfigurationManager.AppSettings("BackupReceipeFilepath").ToString().Trim()

        Dim FILEPATH As String = fname & progname & ".xml"
        Dim backup As String = Backupfname & progname & ".xml"

        ' Save the TreeView data to XML
        SaveTreeViewToXml(FILEPATH, backup)
    End Sub

    Private Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        If TreeView1.SelectedNode IsNot Nothing Then
            Dim VARIABLE As String = Guna2ComboBox8.SelectedItem
            Dim STARTCPUNT As String = RichTextBox2.Text
            Dim LENGTH As String = RichTextBox3.Text
            Dim LEN As Int16 = Convert.ToInt16(LENGTH)
            If LEN > 0 Then
                Dim DATA As String = "<" & VARIABLE & ":" & STARTCPUNT & ":" & LENGTH & ":" & "0>"
                RichTextBox21.Text &= DATA
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If TreeView1.SelectedNode IsNot Nothing Then

            Dim newCodeText As String = $"(CODE) {RichTextBox21.Text}"

            Dim selectedNode As TreeNode = TreeView1.SelectedNode

            If TreeView1.SelectedNode.Text.StartsWith("POSI") Then

                Dim newChildNode As New TreeNode(newCodeText)
                selectedNode.Nodes.Add(newChildNode)

                newChildNode.Tag = New With {
       .CODE_TYPE = Guna2ComboBox7.SelectedIndex,
       .CODE_DATA = RichTextBox1.Text
               }

            End If

            selectedNode.Expand()

        End If

    End Sub

    Private Sub SPEED_1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SPEED_1.SelectedIndexChanged
        Dim selectedValue As String = SPEED_1.SelectedItem.ToString()
        Select Case selectedValue
            Case "HIGH"
                plc.SetDevice("D358", 3)
            Case "MEDIUM"
                plc.SetDevice("D358", 2)
            Case "LOW"
                plc.SetDevice("D358", 1)
        End Select
    End Sub

    Private Sub MOTION_1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MOTION_1.SelectedIndexChanged
        Dim selectedValue As String = MOTION_1.SelectedItem.ToString()
        Select Case selectedValue
            Case "0.1MM"
                plc.SetDevice("D356", 3)
            Case "1MM"
                plc.SetDevice("D356", 2)
            Case "10MM"
                plc.SetDevice("D356", 1)
            Case "CONITNUES"
                plc.SetDevice("D356", 0)
        End Select
    End Sub

    Private Sub Guna2ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox5.SelectedIndexChanged
        Dim selectedValue As String = Guna2ComboBox5.SelectedItem.ToString()
        Select Case selectedValue
            Case "HIGH"
                plc.SetDevice("D358", 3)
            Case "MEDIUM"
                plc.SetDevice("D358", 2)
            Case "LOW"
                plc.SetDevice("D358", 1)
        End Select
    End Sub

    Private Sub Guna2ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox6.SelectedIndexChanged
        Dim selectedValue As String = Guna2ComboBox6.SelectedItem.ToString()
        Select Case selectedValue
            Case "0.1MM"
                plc.SetDevice("D356", 3)
            Case "1MM"
                plc.SetDevice("D356", 2)
            Case "10MM"
                plc.SetDevice("D356", 1)
            Case "CONITNUES"
                plc.SetDevice("D356", 0)
        End Select
    End Sub

    Private Sub Button41_Click(sender As Object, e As EventArgs) Handles Button41.Click

        If RichTextBox21.Text IsNot "" Then
            Dim PATT As String = "<[^>]+>|[a-zA-Z]+"
            Dim RED As String = RichTextBox21.Text
            Dim CODE_DATA As String
            Dim matches As MatchCollection = Regex.Matches(RED, PATT)

            For Each match As Match In matches
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
                            dat = Convert.ToString(Start_Count.Text)

                        ElseIf part1 = "COUNTER2" Then
                            dat = Convert.ToString(Start_Count.Text)

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

            Dim LENGTH As Integer = CODE_DATA.Length






            'Dim COD_DATA As String = "uhkuhuunjojn"

            '' //float value = float.Parse(ARRAY);
            Dim arrdata As Integer() = ConvertStringToWord(CODE_DATA)
            Dim SS As Integer

            ''//plc.WriteDeviceBlock2("D2000", 2, ref arrdata[]);
            Dim baseAddress As Integer = 550

            For i As Integer = 0 To 49
                plc.SetDevice("D" & (baseAddress + i).ToString(), arrdata(i))
            Next

            Dim PRN As Integer = Convert.ToInt16(RichTextBox18.Text)
            Dim LEN As Integer = Convert.ToInt16(RichTextBox4.Text)
            plc.SetDevice("D374", PRN)
            plc.SetDevice("D548", LEN)
            plc.SetDevice("M207", 1)
            Dim DATA As Integer

            plc.GetDevice("M207", DATA)

            Do Until DATA = 1
                plc.SetDevice("M207", 1)
                plc.GetDevice("M207", DATA)
            Loop


        End If




    End Sub
    '''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''
    '''



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





    Private Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click, LED_LIGHT_ON.Click
        If LED = 0 Then
            plc.SetDevice("M247", 1)
            LED = 1
        Else
            plc.SetDevice("M247", 0)
            LED = 0
        End If
    End Sub

    Private Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        If POINT = 0 Then
            plc.SetDevice("M208", 1)
            POINT = 1
        Else
            plc.SetDevice("M208", 0)
            POINT = 0
        End If
    End Sub

    Private Sub Button45_MouseDown(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Button45_MouseUp(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Button46_MouseDown(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Button46_MouseUp(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Button48_MouseDown(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Button48_MouseUp(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Button47_MouseDown(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Button47_MouseUp(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Button49_MouseDown(sender As Object, e As MouseEventArgs)

    End Sub
    Private Sub Button49_MouseUp(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Button50_MouseDown(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Button50_MouseUp(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub EDIT_FID_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub FLIP_ON_Click(sender As Object, e As EventArgs) Handles FLIP_ON.Click

    End Sub
    Private Sub SendFloatValues(valueStr As String, address1 As String, address2 As String)
        Dim floatValue As Single
        If Single.TryParse(valueStr, floatValue) Then
            Dim words() As Integer = ConvertFloatToWord(floatValue)
            plc.SetDevice(address1, words(0))
            plc.SetDevice(address2, words(1))
        End If
    End Sub

    Private Sub Panel_Length_TextChanged(sender As Object, e As EventArgs) Handles Panel_Length.TextChanged
        Dim lengthInt As Integer
        If Integer.TryParse(Panel_Length.Text, lengthInt) Then
            plc.SetDevice("D324", lengthInt)
        Else

        End If
    End Sub

    Private Sub Panel_Width_TextChanged(sender As Object, e As EventArgs) Handles Panel_Width.TextChanged
        SendFloatValues(Panel_Width.Text, "D320", "D321")
    End Sub

    Private Sub Button6_MouseDown(sender As Object, e As MouseEventArgs) Handles Button6.MouseDown
        plc.SetDevice("M240", 1)
    End Sub

    Private Sub Button6_MouseUp(sender As Object, e As MouseEventArgs) Handles Button6.MouseUp
        plc.SetDevice("M240", 0)
    End Sub

    Private Sub PictureBox3_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox3.MouseDown
        plc.SetDevice("M226", 1)
    End Sub

    Private Sub PictureBox3_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox3.MouseUp
        plc.SetDevice("M226", 0)
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        plc.SetDevice("M225", 1)
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        plc.SetDevice("M225", 0)
    End Sub

    Private Sub PLCB_UNLOAD_MouseDown(sender As Object, e As MouseEventArgs) Handles PLCB_UNLOAD.MouseDown
        plc.SetDevice("M233", 1)
    End Sub

    Private Sub PLCB_UNLOAD_MouseUp(sender As Object, e As MouseEventArgs) Handles PLCB_UNLOAD.MouseUp
        plc.SetDevice("M233", 0)
    End Sub
End Class











