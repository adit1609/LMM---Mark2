Imports System.Runtime.InteropServices
Imports ActUtlTypeLib
Imports Guna.UI2.WinForms
Imports System.Configuration
Imports System.Net.Mime.MediaTypeNames
Imports System.Windows.Forms.AxHost
Imports System.Xml
Imports System.IO
Imports System.Xml.Linq
Imports System.Drawing
Imports System.Text.RegularExpressions
'Imports Microsoft.Office.Interop.Excel
Imports System.Net
Imports System.Diagnostics

Public Class Fiducial
    Dim plc As New ActUtlType
    Private xpath As String
    Public WriteOnly Property Setb As String
        Set(value As String)
            xpath = value
        End Set
    End Property
    Dim MyCamera As CCamera = New CCamera

    Dim m_nBufSizeForDriver As UInt32 = 1000 * 1000 * 3
    Dim m_pBufForDriver(m_nBufSizeForDriver) As Byte
    Dim m_stDeviceInfoList As CCamera.MV_CC_DEVICE_INFO_LIST = New CCamera.MV_CC_DEVICE_INFO_LIST
    Dim m_nDeviceIndex As UInt32
    Dim m_bIsGrabbing As Boolean = False
    Dim m_hGrabHandle As System.Threading.Thread
    Dim m_stFrameInfoEx As CCamera.MV_FRAME_OUT_INFO_EX = New CCamera.MV_FRAME_OUT_INFO_EX()
    Dim m_ReadWriteLock As System.Threading.ReaderWriterLock
    Private isDrawing As Boolean = False
    Private startPoint As Point
    Private endPoint As Point
    Private shapes As New List(Of Shape)()
    Private currentID As Integer = 1
    Private currentColor As Color = Color.Blue
    Private drawSquares As Boolean = False
    Private pythonProcess As Process


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub btfidW_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M200", 1)
    End Sub

    Private Sub btfidW_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M200", 0)
    End Sub

    Private Sub btfidN_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M201", 0)
    End Sub

    Private Sub btfidN_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M201", 1)
    End Sub

    Private Sub btxmin_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M206", 0)
    End Sub

    Private Sub btxmin_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M206", 1)
    End Sub

    Private Sub btxmax_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M205", 0)
    End Sub

    Private Sub btxmax_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M205", 1)
    End Sub

    Private Sub btfidW_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Fiducial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler Guna2DataGridView1.CellDoubleClick, AddressOf Guna2DataGridView1_CellDoubleClick
        Control.CheckForIllegalCrossThreadCalls = False
        ' GroupBoxInit.Enabled = True
        GroupBoxDeviceControl.Enabled = False
        GroupBoxGrabImage.Enabled = False
        GroupBoxImageSave.Enabled = False
        GroupBoxParam.Enabled = False

        ComboBoxDeviceList.Enabled = False
        ButtonEnumDevice.Enabled = True
        ButtonOpenDevice.Enabled = False
        ButtonCloseDevice.Enabled = False
        RadioButtonTriggerOff.Enabled = False
        RadioButtonTriggerOn.Enabled = False
        ButtonStartGrabbing.Enabled = False
        ButtonStopGrabbing.Enabled = False
        CheckBoxSoftware.Enabled = False
        ButtonSoftwareOnce.Enabled = False
        ButtonSaveBmp.Enabled = False
        ButtonSaveJpg.Enabled = False
        ButtonSaveTiff.Enabled = False
        ButtonSavePng.Enabled = False
        TextBoxExposureTime.Enabled = False
        TextBoxGain.Enabled = False
        TextBoxFrameRate.Enabled = False
        ButtonParamGet.Enabled = False
        ButtonParamSet.Enabled = False

        ' Initialize savedSquareSize to a valid value

        plc.ActLogicalStationNumber = 1
        plc.Open()


    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub btnstop_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnstart_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ButtonEnumDevice_Click(sender As Object, e As EventArgs) Handles ButtonEnumDevice.Click

        ComboBoxDeviceList.Items.Clear()
        ComboBoxDeviceList.SelectedIndex = -1

        Dim Info As String
        Dim nRet As Int32 = CCamera.MV_OK

        nRet = CCamera.EnumDevices((CCamera.MV_GIGE_DEVICE Or CCamera.MV_USB_DEVICE), m_stDeviceInfoList)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to enumerate devices" & Convert.ToString(nRet))
            Return
        End If

        If (0 = m_stDeviceInfoList.nDeviceNum) Then
            MsgBox("No Find Device !")
            Return
        End If
        Dim i As Int32
        For i = 0 To m_stDeviceInfoList.nDeviceNum - 1
            Dim stDeviceInfo As CCamera.MV_CC_DEVICE_INFO = New CCamera.MV_CC_DEVICE_INFO
            stDeviceInfo = CType(Marshal.PtrToStructure(m_stDeviceInfoList.pDeviceInfo(i), GetType(CCamera.MV_CC_DEVICE_INFO)), CCamera.MV_CC_DEVICE_INFO)

            If (CCamera.MV_GIGE_DEVICE = stDeviceInfo.nTLayerType) Then
                Dim stGigeInfoPtr As IntPtr = Marshal.AllocHGlobal(216)
                Marshal.Copy(stDeviceInfo.stSpecialInfo.stGigEInfo, 0, stGigeInfoPtr, 216)
                Dim stGigeInfo As CCamera.MV_GIGE_DEVICE_INFO
                stGigeInfo = CType(Marshal.PtrToStructure(stGigeInfoPtr, GetType(CCamera.MV_GIGE_DEVICE_INFO)), CCamera.MV_GIGE_DEVICE_INFO)
                Dim nIpByte1 As UInt32 = (stGigeInfo.nCurrentIp And &HFF000000) >> 24
                Dim nIpByte2 As UInt32 = (stGigeInfo.nCurrentIp And &HFF0000) >> 16
                Dim nIpByte3 As UInt32 = (stGigeInfo.nCurrentIp And &HFF00) >> 8
                Dim nIpByte4 As UInt32 = (stGigeInfo.nCurrentIp And &HFF)

                If (stGigeInfo.chUserDefinedName = "") Then
                    Info = "DEV:[" & Convert.ToString(i) & "] " & stGigeInfo.chModelName & "(" & nIpByte1.ToString() & "." & nIpByte2.ToString() & "." & nIpByte3.ToString() & "." & nIpByte4.ToString() & ")"
                Else
                    Info = "DEV:[" & Convert.ToString(i) & "] " & stGigeInfo.chUserDefinedName & "(" & nIpByte1.ToString() & "." & nIpByte2.ToString() & "." & nIpByte3.ToString() & "." & nIpByte4.ToString() & ")"
                End If
                ComboBoxDeviceList.Items.Add(Info)
            Else
                Dim stUsbInfoPtr As IntPtr = Marshal.AllocHGlobal(540)
                Marshal.Copy(stDeviceInfo.stSpecialInfo.stUsb3VInfo, 0, stUsbInfoPtr, 540)
                Dim stUsbInfo As CCamera.MV_USB3_DEVICE_INFO
                stUsbInfo = CType(Marshal.PtrToStructure(stUsbInfoPtr, GetType(CCamera.MV_USB3_DEVICE_INFO)), CCamera.MV_USB3_DEVICE_INFO)
                Dim strUserDefinedName As String = ""

                'If (CCamera.IsTextUTF8(stUsbInfo.chUserDefinedName)) Then
                '    strUserDefinedName = UTF8Encoding.UTF8.GetString(stUsbInfo.chUserDefinedName).TrimEnd("\0")
                'Else
                '    strUserDefinedName = Encoding.GetEncoding("GB2312").GetString(stUsbInfo.chUserDefinedName).TrimEnd("\0")
                'End If

                If (stUsbInfo.chUserDefinedName = "") Then
                    Info = "U3V:[" & Convert.ToString(i) & "] " & stUsbInfo.chModelName & "(" & stUsbInfo.chSerialNumber & ")"
                Else
                    Info = "U3V:[" & Convert.ToString(i) & "] " & stUsbInfo.chUserDefinedName & "(" & stUsbInfo.chSerialNumber & ")"
                End If

                ComboBoxDeviceList.Items.Add(Info)
            End If
        Next i

        If (0 < m_stDeviceInfoList.nDeviceNum) Then
            ComboBoxDeviceList.SelectedIndex = 0
        End If

        'GroupBoxInit.Enabled = True
        'GroupBoxDeviceControl.Enabled = False
        'GroupBoxGrabImage.Enabled = False
        'GroupBoxImageSave.Enabled = False
        'GroupBoxParam.Enabled = False

        'ComboBoxDeviceList.Enabled = True
        'ButtonEnumDevice.Enabled = True
        'ButtonOpenDevice.Enabled = False
        'ButtonCloseDevice.Enabled = False
        'RadioButtonTriggerOff.Enabled = False
        'RadioButtonTriggerOn.Enabled = False
        'ButtonStartGrabbing.Enabled = False
        'ButtonStopGrabbing.Enabled = False
        'CheckBoxSoftware.Enabled = False
        'ButtonSoftwareOnce.Enabled = False
        'ButtonSaveBmp.Enabled = False
        'ButtonSaveJpg.Enabled = False
        'ButtonSaveTiff.Enabled = False
        'ButtonSavePng.Enabled = False
        'TextBoxExposureTime.Enabled = False
        'TextBoxGain.Enabled = False
        'TextBoxFrameRate.Enabled = False
        'ButtonParamGet.Enabled = False
        'ButtonParamSet.Enabled = False
    End Sub

    Private Sub ButtonOpenDevice_Click(sender As Object, e As EventArgs) Handles ButtonOpenDevice.Click
        Dim nRet As Int32 = CCamera.MV_OK
        Dim stDeviceInfo As CCamera.MV_CC_DEVICE_INFO
        stDeviceInfo = CType(Marshal.PtrToStructure(m_stDeviceInfoList.pDeviceInfo(m_nDeviceIndex), GetType(CCamera.MV_CC_DEVICE_INFO)), CCamera.MV_CC_DEVICE_INFO)
        nRet = MyCamera.CreateDevice(stDeviceInfo)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to create handle")
            Return
        End If
        nRet = MyCamera.OpenDevice()
        If CCamera.MV_OK <> nRet Then
            MyCamera.DestroyDevice()
            MsgBox("Open device failed")
            Return
        End If
        If stDeviceInfo.nTLayerType = CCamera.MV_GIGE_DEVICE Then
            Dim nPacketSize As Int32
            nPacketSize = MyCamera.GIGE_GetOptimalPacketSize()
            If nPacketSize > 0 Then
                nRet = MyCamera.SetIntValueEx("GevSCPSPacketSize", nPacketSize)
                If 0 <> nRet Then
                    MsgBox("Set Packet Size failed")
                End If
            Else
                MsgBox("Get Packet Size failed")
            End If
        End If
        Dim stTriggerMode As CCamera.MVCC_ENUMVALUE = New CCamera.MVCC_ENUMVALUE
        nRet = MyCamera.GetEnumValue("TriggerMode", stTriggerMode)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to acquire trigger mode")
            Return
        End If
        If 0 = stTriggerMode.nCurValue Then
            RadioButtonTriggerOff.Checked = True
            RadioButtonTriggerOn.Checked = False
        Else
            RadioButtonTriggerOff.Checked = False
            RadioButtonTriggerOn.Checked = True
        End If
        Dim stTriggerSource As CCamera.MVCC_ENUMVALUE = New CCamera.MVCC_ENUMVALUE
        nRet = MyCamera.GetEnumValue("TriggerSource", stTriggerSource)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to acquire trigger source")
            Return
        End If
        If CCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE = stTriggerSource.nCurValue Then
            CheckBoxSoftware.Checked = True
        Else
            CheckBoxSoftware.Checked = False
        End If
        Dim stExposureTime As CCamera.MVCC_FLOATVALUE = New CCamera.MVCC_FLOATVALUE
        nRet = MyCamera.GetFloatValue("ExposureTime", stExposureTime)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to acquire exposure time")
        End If
        TextBoxExposureTime.Text = Convert.ToString(stExposureTime.fCurValue)
        Dim stGain As CCamera.MVCC_FLOATVALUE = New CCamera.MVCC_FLOATVALUE
        nRet = MyCamera.GetFloatValue("Gain", stGain)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to acquire gain")
        End If
        TextBoxGain.Text = Convert.ToString(stGain.fCurValue)
        Dim stFrameRate As CCamera.MVCC_FLOATVALUE = New CCamera.MVCC_FLOATVALUE
        nRet = MyCamera.GetFloatValue("ResultingFrameRate", stFrameRate)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to acquire frame rate")
        End If
        TextBoxFrameRate.Text = Convert.ToString(stFrameRate.fCurValue)

        If RadioButtonTriggerOn.Checked Then
            RadioButtonTriggerOn.Enabled = False
            RadioButtonTriggerOff.Enabled = True
        Else
            RadioButtonTriggerOn.Enabled = True
            RadioButtonTriggerOff.Enabled = False
        End If

        If (RadioButtonTriggerOn.Checked) Then
            CheckBoxSoftware.Enabled = True
        Else
            CheckBoxSoftware.Enabled = False
        End If

        If (RadioButtonTriggerOn.Checked And CheckBoxSoftware.Checked) Then
            ButtonSoftwareOnce.Enabled = True
        Else
            ButtonSoftwareOnce.Enabled = False
        End If
        GroupBoxDeviceControl.Enabled = True
        GroupBoxGrabImage.Enabled = True
        GroupBoxImageSave.Enabled = True
        GroupBoxParam.Enabled = True

        ComboBoxDeviceList.Enabled = False
        ButtonOpenDevice.Enabled = False
        ButtonCloseDevice.Enabled = True
        ButtonStartGrabbing.Enabled = True
        ButtonStopGrabbing.Enabled = False
        ButtonSaveBmp.Enabled = False
        ButtonSaveJpg.Enabled = False
        ButtonSaveTiff.Enabled = False
        ButtonSavePng.Enabled = False
        TextBoxExposureTime.Enabled = True
        TextBoxGain.Enabled = True
        TextBoxFrameRate.Enabled = True
        ButtonParamGet.Enabled = True
        ButtonParamSet.Enabled = True

    End Sub

    Private Sub ButtonCloseDevice_Click(sender As Object, e As EventArgs) Handles ButtonCloseDevice.Click
        If m_bIsGrabbing Then
            m_bIsGrabbing = False
            m_hGrabHandle.Join()
        End If

        Dim nRet As Int32 = CCamera.MV_OK
        nRet = MyCamera.CloseDevice()
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to close device")
            Return
        End If

        nRet = MyCamera.DestroyDevice()
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to destroy handle")
            Return
        End If

        TextBoxExposureTime.Text = "0"
        TextBoxGain.Text = "0"
        TextBoxFrameRate.Text = "0"
        ComboBoxDeviceList.Enabled = True
        ButtonOpenDevice.Enabled = True
        ButtonCloseDevice.Enabled = False

        GroupBoxGrabImage.Enabled = False
        ButtonStartGrabbing.Enabled = False
        ButtonStopGrabbing.Enabled = False
        ButtonSoftwareOnce.Enabled = False

        GroupBoxImageSave.Enabled = False
        GroupBoxParam.Enabled = False
    End Sub

    Private Sub ButtonStartGrabbing_Click(sender As Object, e As EventArgs) Handles ButtonStartGrabbing.Click
        m_bIsGrabbing = True
        m_ReadWriteLock = New Threading.ReaderWriterLock()
        m_hGrabHandle = New Threading.Thread(AddressOf ReceiveThreadProcess)
        m_hGrabHandle.Start()

        m_stFrameInfoEx.nFrameLen = 0
        Dim nRet As Int32
        nRet = MyCamera.StartGrabbing()
        If CCamera.MV_OK <> nRet Then
            m_bIsGrabbing = False
            m_hGrabHandle.Join()
            MsgBox("Fail to start grabbing")
        End If

        ButtonStartGrabbing.Enabled = False
        ButtonStopGrabbing.Enabled = True
        ButtonSaveBmp.Enabled = True
        ButtonSaveJpg.Enabled = True
        ButtonSaveTiff.Enabled = True
        ButtonSavePng.Enabled = True
    End Sub
    Sub ReceiveThreadProcess()
        Dim stFrameOut As CCamera.MV_FRAME_OUT = New CCamera.MV_FRAME_OUT()
        Dim stDisplayInfo As CCamera.MV_DISPLAY_FRAME_INFO = New CCamera.MV_DISPLAY_FRAME_INFO()

        While (m_bIsGrabbing)
            Dim nRet = MyCamera.GetImageBuffer(stFrameOut, 1000)
            If CCamera.MV_OK = nRet Then

                m_ReadWriteLock.AcquireWriterLock(System.Threading.Timeout.Infinite)
                If stFrameOut.stFrameInfo.nFrameLen > m_nBufSizeForDriver Then
                    m_nBufSizeForDriver = stFrameOut.stFrameInfo.nFrameLen
                    ReDim m_pBufForDriver(m_nBufSizeForDriver)
                End If

                m_stFrameInfoEx = stFrameOut.stFrameInfo
                Marshal.Copy(stFrameOut.pBufAddr, m_pBufForDriver, 0, stFrameOut.stFrameInfo.nFrameLen)
                m_ReadWriteLock.ReleaseWriterLock()

                stDisplayInfo.hWnd = PictureBoxDisplay.Handle
                stDisplayInfo.pData = stFrameOut.pBufAddr
                stDisplayInfo.nDataLen = stFrameOut.stFrameInfo.nFrameLen
                stDisplayInfo.nWidth = stFrameOut.stFrameInfo.nWidth
                stDisplayInfo.nHeight = stFrameOut.stFrameInfo.nHeight
                stDisplayInfo.enPixelType = stFrameOut.stFrameInfo.enPixelType
                MyCamera.DisplayOneFrame(stDisplayInfo)

                MyCamera.FreeImageBuffer(stFrameOut)



            Else
                If RadioButtonTriggerOn.Checked Then
                    Threading.Thread.Sleep(5)
                End If
            End If
        End While

    End Sub

    Private Sub ButtonStopGrabbing_Click(sender As Object, e As EventArgs) Handles ButtonStopGrabbing.Click
        m_bIsGrabbing = False
        m_hGrabHandle.Join()

        Dim nRet As Int32
        nRet = MyCamera.StopGrabbing()
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to stop grabbing")
        End If
        ButtonStartGrabbing.Enabled = True
        ButtonStopGrabbing.Enabled = False
        ButtonSaveBmp.Enabled = False
        ButtonSaveJpg.Enabled = False
        ButtonSaveTiff.Enabled = False
        ButtonSavePng.Enabled = False
    End Sub

    Private Sub RadioButtonTriggerOff_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTriggerOff.CheckedChanged

        Dim nRet As Int32 = CCamera.MV_OK
        nRet = MyCamera.SetEnumValue("TriggerMode", CCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to close trigger mode")
        End If

        RadioButtonTriggerOff.Enabled = False
        RadioButtonTriggerOn.Enabled = True
        If (RadioButtonTriggerOn.Checked) Then
            CheckBoxSoftware.Enabled = True
        Else
            CheckBoxSoftware.Enabled = False
        End If

        If (RadioButtonTriggerOn.Checked And CheckBoxSoftware.Checked) Then
            ButtonSoftwareOnce.Enabled = True
        Else
            ButtonSoftwareOnce.Enabled = False
        End If
    End Sub

    Private Sub RadioButtonTriggerOn_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTriggerOn.CheckedChanged
        Dim nRet As Int32 = CCamera.MV_OK
        nRet = MyCamera.SetEnumValue("TriggerMode", CCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to close trigger mode")
        End If

        RadioButtonTriggerOff.Enabled = True
        RadioButtonTriggerOn.Enabled = False
        If (RadioButtonTriggerOn.Checked) Then
            CheckBoxSoftware.Enabled = True
        Else
            CheckBoxSoftware.Enabled = False
        End If

        If (RadioButtonTriggerOn.Checked And CheckBoxSoftware.Checked) Then
            ButtonSoftwareOnce.Enabled = True
        Else
            ButtonSoftwareOnce.Enabled = False
        End If
    End Sub

    Private Sub CheckBoxSoftware_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSoftware.CheckedChanged
        If (CheckBoxSoftware.Checked) Then

            Dim nRet As Int32
            nRet = MyCamera.SetEnumValue("TriggerSource", CCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE)
            If CCamera.MV_OK <> nRet Then
                MsgBox("Fail to set software trigger")
            End If
        Else
            Dim nRet As Int32
            nRet = MyCamera.SetEnumValue("TriggerSource", CCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0)
            If CCamera.MV_OK <> nRet Then
                MsgBox("Fail to set hardware trigger")
            End If
        End If

        CheckBoxSoftware.Enabled = True
        If (RadioButtonTriggerOn.Checked And CheckBoxSoftware.Checked) Then
            ButtonSoftwareOnce.Enabled = True
        Else
            ButtonSoftwareOnce.Enabled = False
        End If

    End Sub

    Private Sub ButtonSoftwareOnce_Click(sender As Object, e As EventArgs) Handles ButtonSoftwareOnce.Click
        Dim nRet As Int32
        nRet = MyCamera.SetCommandValue("TriggerSoftware")
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to software trigger once")
        End If
    End Sub

    Private Sub ButtonSaveBmp_Click(sender As Object, e As EventArgs) Handles ButtonSaveBmp.Click
        If False = m_bIsGrabbing Then
            MsgBox("Not Start Grabbing!")
            Return
        End If

        Dim stSaveImageParam As CCamera.MV_SAVE_IMG_TO_FILE_PARAM = New CCamera.MV_SAVE_IMG_TO_FILE_PARAM()

        m_ReadWriteLock.AcquireWriterLock(System.Threading.Timeout.Infinite)
        If m_stFrameInfoEx.nFrameLen = 0 Then
            m_ReadWriteLock.ReleaseWriterLock()
            MsgBox("Save Bmp Fail!")
            Return
        End If

        Dim nRet As Int32 = CCamera.MV_OK
        Dim pData As IntPtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0)
        stSaveImageParam.pData = pData
        stSaveImageParam.nDataLen = m_stFrameInfoEx.nFrameLen
        stSaveImageParam.enPixelType = m_stFrameInfoEx.enPixelType
        stSaveImageParam.nWidth = m_stFrameInfoEx.nWidth
        stSaveImageParam.nHeight = m_stFrameInfoEx.nHeight
        stSaveImageParam.enImageType = CCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Bmp
        stSaveImageParam.iMethodValue = 2
        stSaveImageParam.pImagePath = "Image_w" & stSaveImageParam.nWidth.ToString() & "_h" & stSaveImageParam.nHeight.ToString() & "_fn" & m_stFrameInfoEx.nFrameNum.ToString() & ".bmp"
        nRet = MyCamera.SaveImageToFile(stSaveImageParam)
        If (CCamera.MV_OK <> nRet) Then
            m_ReadWriteLock.ReleaseWriterLock()
            MsgBox("Save Image fail!")
            Return
        End If
        m_ReadWriteLock.ReleaseWriterLock()

        MsgBox("Save BMP succeed")
        Return

    End Sub

    Private Sub ButtonSaveJpg_Click(sender As Object, e As EventArgs) Handles ButtonSaveJpg.Click
        If False = m_bIsGrabbing Then
            MsgBox("Not Start Grabbing!")
            Return
        End If

        Dim stSaveImageParam As CCamera.MV_SAVE_IMG_TO_FILE_PARAM = New CCamera.MV_SAVE_IMG_TO_FILE_PARAM()

        m_ReadWriteLock.AcquireWriterLock(System.Threading.Timeout.Infinite)
        If m_stFrameInfoEx.nFrameLen = 0 Then
            m_ReadWriteLock.ReleaseWriterLock()
            MsgBox("Save Jpeg Fail!")
            Return
        End If

        Dim nRet As Int32
        Dim pData As IntPtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0)
        stSaveImageParam.pData = pData
        stSaveImageParam.nDataLen = m_stFrameInfoEx.nFrameLen
        stSaveImageParam.enPixelType = m_stFrameInfoEx.enPixelType
        stSaveImageParam.nWidth = m_stFrameInfoEx.nWidth
        stSaveImageParam.nHeight = m_stFrameInfoEx.nHeight
        stSaveImageParam.enImageType = CCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg
        stSaveImageParam.iMethodValue = 2
        stSaveImageParam.nQuality = 80
        stSaveImageParam.pImagePath = "Image_w" & stSaveImageParam.nWidth.ToString() & "_h" & stSaveImageParam.nHeight.ToString() & "_fn" & m_stFrameInfoEx.nFrameNum.ToString() & ".jpg"
        nRet = MyCamera.SaveImageToFile(stSaveImageParam)
        If (CCamera.MV_OK <> nRet) Then
            m_ReadWriteLock.ReleaseWriterLock()
            MsgBox("Save Image fail!")
            Return
        End If
        m_ReadWriteLock.ReleaseWriterLock()

        MsgBox("Save Jpeg succeed")
        Return
    End Sub

    Private Sub ButtonSaveTiff_Click(sender As Object, e As EventArgs) Handles ButtonSaveTiff.Click
        If False = m_bIsGrabbing Then
            MsgBox("Not Start Grabbing!")
            Return
        End If

        Dim stSaveImageParam As CCamera.MV_SAVE_IMG_TO_FILE_PARAM = New CCamera.MV_SAVE_IMG_TO_FILE_PARAM()

        m_ReadWriteLock.AcquireWriterLock(System.Threading.Timeout.Infinite)
        If m_stFrameInfoEx.nFrameLen = 0 Then
            m_ReadWriteLock.ReleaseWriterLock()
            MsgBox("Save Tiff Fail!")
            Return
        End If

        Dim nRet As Int32
        Dim pData As IntPtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0)
        stSaveImageParam.pData = pData
        stSaveImageParam.nDataLen = m_stFrameInfoEx.nFrameLen
        stSaveImageParam.enPixelType = m_stFrameInfoEx.enPixelType
        stSaveImageParam.nWidth = m_stFrameInfoEx.nWidth
        stSaveImageParam.nHeight = m_stFrameInfoEx.nHeight
        stSaveImageParam.enImageType = CCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Tif
        stSaveImageParam.iMethodValue = 2
        stSaveImageParam.pImagePath = "Image_w" & stSaveImageParam.nWidth.ToString() & "_h" & stSaveImageParam.nHeight.ToString() & "_fn" & m_stFrameInfoEx.nFrameNum.ToString() & ".tif"
        nRet = MyCamera.SaveImageToFile(stSaveImageParam)
        If (CCamera.MV_OK <> nRet) Then
            m_ReadWriteLock.ReleaseWriterLock()
            MsgBox("Save Image fail!")
            Return
        End If
        m_ReadWriteLock.ReleaseWriterLock()

        MsgBox("Save Tiff succeed")
        Return
    End Sub
    Private Sub ButtonSavePng_Click(sender As Object, e As EventArgs) Handles ButtonSavePng.Click
        If False = m_bIsGrabbing Then
            MsgBox("Not Start Grabbing!")
            Return
        End If

        Dim stSaveImageParam As CCamera.MV_SAVE_IMG_TO_FILE_PARAM = New CCamera.MV_SAVE_IMG_TO_FILE_PARAM()

        m_ReadWriteLock.AcquireWriterLock(System.Threading.Timeout.Infinite)
        If m_stFrameInfoEx.nFrameLen = 0 Then
            m_ReadWriteLock.ReleaseWriterLock()
            MsgBox("Save Png Fail!")
            Return
        End If

        Try
            Dim pData As IntPtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0)
            stSaveImageParam.pData = pData
            stSaveImageParam.nDataLen = m_stFrameInfoEx.nFrameLen
            stSaveImageParam.enPixelType = m_stFrameInfoEx.enPixelType
            stSaveImageParam.nWidth = m_stFrameInfoEx.nWidth
            stSaveImageParam.nHeight = m_stFrameInfoEx.nHeight
            stSaveImageParam.enImageType = CCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Png
            stSaveImageParam.nQuality = 8
            stSaveImageParam.iMethodValue = 2

            ' Capture the displayed portion of the PictureBox
            Dim displayedRect As Rectangle = PictureBoxDisplay.ClientRectangle
            Dim displayedBitmap As New Bitmap(displayedRect.Width, displayedRect.Height)

            Using g As Graphics = Graphics.FromImage(displayedBitmap)
                g.CopyFromScreen(PictureBoxDisplay.PointToScreen(Point.Empty), Point.Empty, displayedRect.Size)
            End Using

            ' Save the captured image to a file
            Dim saveFilePath As String = "image.png"
            displayedBitmap.Save(saveFilePath, System.Drawing.Imaging.ImageFormat.Png)

            MsgBox("Save Png succeed")
        Catch ex As Exception
            MsgBox("Save Image fail: " & ex.Message)
        Finally
            m_ReadWriteLock.ReleaseWriterLock()
        End Try
    End Sub
    Private Sub SavePng()
        If False = m_bIsGrabbing Then
            MsgBox("Not Start Grabbing!")
            Return
        End If

        Dim stSaveImageParam As CCamera.MV_SAVE_IMG_TO_FILE_PARAM = New CCamera.MV_SAVE_IMG_TO_FILE_PARAM()

        m_ReadWriteLock.AcquireWriterLock(System.Threading.Timeout.Infinite)
        If m_stFrameInfoEx.nFrameLen = 0 Then
            m_ReadWriteLock.ReleaseWriterLock()
            MsgBox("Save Png Fail!")
            Return
        End If

        Try
            Dim pData As IntPtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0)
            stSaveImageParam.pData = pData
            stSaveImageParam.nDataLen = m_stFrameInfoEx.nFrameLen
            stSaveImageParam.enPixelType = m_stFrameInfoEx.enPixelType
            stSaveImageParam.nWidth = m_stFrameInfoEx.nWidth
            stSaveImageParam.nHeight = m_stFrameInfoEx.nHeight
            stSaveImageParam.enImageType = CCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Png
            stSaveImageParam.nQuality = 8
            stSaveImageParam.iMethodValue = 2

            ' Capture the displayed portion of the PictureBox
            Dim displayedRect As Rectangle = PictureBoxDisplay.ClientRectangle
            Dim displayedBitmap As New Bitmap(displayedRect.Width, displayedRect.Height)

            Using g As Graphics = Graphics.FromImage(displayedBitmap)
                g.CopyFromScreen(PictureBoxDisplay.PointToScreen(Point.Empty), Point.Empty, displayedRect.Size)
            End Using

            ' Save the captured image to a file
            Dim saveFilePath As String = "image.png"
            displayedBitmap.Save(saveFilePath, System.Drawing.Imaging.ImageFormat.Png)

            MsgBox("Save Png succeed")
        Catch ex As Exception
            MsgBox("Save Image fail: " & ex.Message)
        Finally
            m_ReadWriteLock.ReleaseWriterLock()
        End Try
    End Sub


    Private Sub ButtonParamGet_Click(sender As Object, e As EventArgs) Handles ButtonParamGet.Click
        Dim nRet As Int32
        Dim stExposureTime As CCamera.MVCC_FLOATVALUE = New CCamera.MVCC_FLOATVALUE
        nRet = MyCamera.GetFloatValue("ExposureTime", stExposureTime)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to get exposure time")
        End If
        TextBoxExposureTime.Text = Convert.ToString(stExposureTime.fCurValue)

        Dim stGain As CCamera.MVCC_FLOATVALUE = New CCamera.MVCC_FLOATVALUE
        nRet = MyCamera.GetFloatValue("Gain", stGain)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to get gain")
        End If
        TextBoxGain.Text = Convert.ToString(stGain.fCurValue)

        Dim stFrameRate As CCamera.MVCC_FLOATVALUE = New CCamera.MVCC_FLOATVALUE
        nRet = MyCamera.GetFloatValue("ResultingFrameRate", stFrameRate)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to get frame rate")
        End If
        TextBoxFrameRate.Text = Convert.ToString(stFrameRate.fCurValue)
    End Sub

    Private Sub ButtonParamSet_Click(sender As Object, e As EventArgs) Handles ButtonParamSet.Click
        Dim fExposureTime As Single = 0
        Dim fGain As Single = 0
        Dim fFrameRate As Single = 0
        Try
            fExposureTime = Convert.ToSingle(TextBoxExposureTime.Text)
            fGain = Convert.ToSingle(TextBoxGain.Text)
            fFrameRate = Convert.ToSingle(TextBoxFrameRate.Text)
        Catch
            MsgBox("Incorrect parameter format")
            Return
        Finally

        End Try

        Dim nRet As Int32
        If RadioButtonTriggerOff.Checked Then
            nRet = MyCamera.SetEnumValue("ExposureAuto", CCamera.MV_CAM_EXPOSURE_AUTO_MODE.MV_EXPOSURE_AUTO_MODE_OFF)
            If CCamera.MV_OK <> nRet Then
                MsgBox("Fail to close auto-exposure")
            End If
        End If

        nRet = MyCamera.SetEnumValue("GainAuto", CCamera.MV_CAM_GAIN_MODE.MV_GAIN_MODE_OFF)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to close auto-gain")
        End If

        nRet = MyCamera.SetFloatValue("ExposureTime", fExposureTime)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to set exposure time")
        End If

        nRet = MyCamera.SetFloatValue("Gain", fGain)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to set gain")
        End If


        nRet = MyCamera.SetBoolValue("AcquisitionFrameRateEnable", True)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Set frame rate enable fail")
        End If
        nRet = MyCamera.SetFloatValue("AcquisitionFrameRate", fFrameRate)
        If CCamera.MV_OK <> nRet Then
            MsgBox("Fail to set frame rate")
        End If
    End Sub

    Private Sub ComboBoxDeviceList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxDeviceList.SelectedIndexChanged
        m_nDeviceIndex = ComboBoxDeviceList.SelectedIndex

        'GroupBoxInit.Enabled = True
        GroupBoxDeviceControl.Enabled = True
        GroupBoxGrabImage.Enabled = False
        GroupBoxImageSave.Enabled = False
        GroupBoxParam.Enabled = False

        ComboBoxDeviceList.Enabled = True
        ButtonEnumDevice.Enabled = True
        ButtonOpenDevice.Enabled = True
        ButtonCloseDevice.Enabled = False
        RadioButtonTriggerOff.Enabled = False
        RadioButtonTriggerOn.Enabled = False
        ButtonStartGrabbing.Enabled = False
        ButtonStopGrabbing.Enabled = False
        CheckBoxSoftware.Enabled = False
        ButtonSoftwareOnce.Enabled = False
        ButtonSaveBmp.Enabled = False
        ButtonSaveJpg.Enabled = False
        ButtonSaveTiff.Enabled = False
        ButtonSavePng.Enabled = False
        TextBoxExposureTime.Enabled = False
        TextBoxGain.Enabled = False
        TextBoxFrameRate.Enabled = False
        ButtonParamGet.Enabled = False
        ButtonParamSet.Enabled = False
    End Sub

    Private Sub Fiducial_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If m_bIsGrabbing Then
            m_bIsGrabbing = False
            m_hGrabHandle.Join()
        End If

        Dim nRet As Int32 = CCamera.MV_OK
        nRet = MyCamera.CloseDevice()
        MyCamera.DestroyDevice()
    End Sub
    Private Sub DrawPlusSignOnPictureBox(pictureBox As PictureBox, Width As Int32, Hight As Int32)
        Using graphics As Graphics = pictureBox.CreateGraphics()
            Using pen As New Pen(Color.Red, 2)
                Dim centerX As Integer = Width / 2
                Dim centerY As Integer = Hight / 2

                'raw horizontal line
                graphics.DrawLine(pen, centerX - 20, centerY, centerX + 20, centerY)

                'raw vertical line
                graphics.DrawLine(pen, centerX, centerY - 20, centerX, centerY + 20)
            End Using
        End Using
    End Sub







    Private Sub Button11_Click(sender As Object, e As EventArgs)
        Dim imagePath As String = "C:\Users\adity\Downloads\Gui_Tset_visiontest\Gui_Tset\bin\Debug\image.png"

        If File.Exists(imagePath) Then
            PictureBoxDisplay.Image = System.Drawing.Image.FromFile(imagePath)
        Else
            MessageBox.Show("Image file not found.")
        End If


    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs)
    End Sub
    Public Sub RunPython()
        Dim pythonPath As String = "C:/Users/adity/AppData/Local/Programs/Python/Python312/python.exe"
        Dim scriptPath As String = "A:\5.py"
        Dim imagePath As String = String.Empty
        Dim resultImagePath As String = "A:\Program\output.png"

        Dim startInfo As New ProcessStartInfo(pythonPath)
        startInfo.Arguments = scriptPath & " " & imagePath
        startInfo.UseShellExecute = False
        startInfo.RedirectStandardOutput = True
        startInfo.RedirectStandardError = True

        Dim process As New Process()
        process.StartInfo = startInfo
        process.Start()

        Dim output As String = process.StandardOutput.ReadToEnd()
        Dim [error] As String = process.StandardError.ReadToEnd()

        process.WaitForExit()

        ' Extract the x and y coordinates from the output
        Dim regex As New Regex("Detected center at: \((?<x>\d+), (?<y>\d+)\)")
        Dim match As Match = regex.Match(output)
        If match.Success Then
            Dim x As String = match.Groups("x").Value
            Dim y As String = match.Groups("y").Value

            ' Update UI with coordinates
            'RichTextBox1.Text = $"Detected center at: ({x}, {y})"
        Else
            'RichTextBox1.Text = "No coordinates found"
        End If

        Console.WriteLine("Output: " & output)
        Console.WriteLine("Error: " & [error])

        ' Dispose of the previous image if it exists
        If PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
            PictureBox1.Image = Nothing
        End If

        ' Display the resulting image in the picture box
        If File.Exists(resultImagePath) Then
            PictureBox1.Image = System.Drawing.Image.FromFile(resultImagePath)
        Else
            MessageBox.Show("Error: Processed image not found.")
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs)

    End Sub



    Private Sub PictureBoxDisplay_Click(sender As Object, e As EventArgs) Handles PictureBoxDisplay.Click

    End Sub

    Private Sub PictureBoxDisplay_Paint(sender As Object, e As PaintEventArgs) Handles PictureBoxDisplay.Paint
        Dim g As Graphics = e.Graphics
        Dim font As New Font("Arial", 12)
        Dim brush As New SolidBrush(Color.Black)

        ' Draw all stored shapes
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

    Private Sub PictureBoxDisplay_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBoxDisplay.MouseDown
        If e.Button = MouseButtons.Left Then
            startPoint = e.Location
            endPoint = e.Location   ' Set endPoint initially to start point
            isDrawing = True
        End If
    End Sub

    Private Sub PictureBoxDisplay_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBoxDisplay.MouseUp
        If isDrawing Then
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
            PictureBoxDisplay.Invalidate()
        End If
    End Sub
    Private Sub AddShapeToDataGridView(id As Integer, shape As String, x1 As Integer, y1 As Integer, width As Integer, Optional height As Single = 0, Optional centerX As Integer = 0, Optional centerY As Integer = 0)
        Dim centerPoint As String

        If shape = "Square" Then
            Dim x2 As Integer = x1 + width
            Dim y2 As Integer = y1 + height
            Dim centerXInt As Integer = CInt(x1 + width \ 2) ' Calculate center X as integer
            Dim centerYInt As Integer = CInt(y1 + height \ 2) ' Calculate center Y as integer
            centerPoint = $"({centerXInt}, {centerYInt})"
            Dim row As String() = {id.ToString(), shape, x1.ToString(), y1.ToString(), x2.ToString(), y2.ToString(), centerPoint}
            Guna2DataGridView1.Rows.Add(row)
        ElseIf shape = "Circle" Then
            centerPoint = $"({centerX}, {centerY})"
            Dim row As String() = {id.ToString(), shape, x1.ToString(), y1.ToString(), width.ToString(), height.ToString(), centerPoint}
            Guna2DataGridView1.Rows.Add(row)
        Else
            Throw New ArgumentException("Invalid shape type")
        End If
    End Sub

    Private Sub PictureBoxDisplay_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBoxDisplay.MouseMove
        If isDrawing AndAlso e.Button = MouseButtons.Left Then
            endPoint = e.Location
            PictureBoxDisplay.Invalidate()
        End If
    End Sub
    Private Class Shape
        Public Property ID As Integer
        Public Property Color As Color

        Public Sub New(id As Integer, color As Color)
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

    Private Sub Guna2CircleButton1_Click(sender As Object, e As EventArgs) Handles Guna2CircleButton1.Click
        isDrawing = False
        drawSquares = False
        PictureBoxDisplay.Invalidate()
        Panel11.Hide()
        Panel12.Show()
        Guna2CircleButton1.BackColor = Color.Green
        Guna2Button1.BackColor = Color.White
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        drawSquares = True ' Set flag to draw squares
        isDrawing = False ' Ensure we're not drawing circles
        PictureBoxDisplay.Invalidate()
        Panel11.Show()
        Panel12.Hide()
        Guna2Button1.BackColor = Color.Green
        Guna2CircleButton1.BackColor = Color.White

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ' Check if a row is selected
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Remove the corresponding shape from the list
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)
            Dim shapeToRemove As Shape = shapes.FirstOrDefault(Function(s) s.ID = selectedShapeID)
            If shapeToRemove IsNot Nothing Then
                shapes.Remove(shapeToRemove)
            End If

            ' Remove the row from the DataGridView
            Guna2DataGridView1.Rows.RemoveAt(rowIndex)

            ' Redraw PictureBox
            PictureBoxDisplay.Invalidate()
        Else
            MessageBox.Show("Please select a row to delete.")
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            currentColor = ColorDialog1.Color
        End If
    End Sub


    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Increase the boundary from the bottom
                Dim increaseAmount As Integer = 2 ' Amount by which the boundary should be increased
                square.Size = New Size(square.Size.Width, square.Size.Height + increaseAmount)

                ' Update the DataGridView to reflect the changes
                Guna2DataGridView1.Rows(rowIndex).Cells(5).Value = square.TopLeft.Y + square.Size.Height ' Update the Y2 value
                Guna2DataGridView1.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point

                ' Redraw PictureBox
                PictureBoxDisplay.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a square.")
            End If
        Else
            MessageBox.Show("Please select a square from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Increase the boundary from the right
                Dim increaseAmount As Integer = 2 ' Amount by which the boundary should be increased
                square.Size = New Size(square.Size.Width + increaseAmount, square.Size.Height)

                ' Update the DataGridView to reflect the changes
                Guna2DataGridView1.Rows(rowIndex).Cells(4).Value = square.TopLeft.X + square.Size.Width ' Update the X2 value
                Guna2DataGridView1.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point

                ' Redraw PictureBox
                PictureBoxDisplay.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a square.")
            End If
        Else
            MessageBox.Show("Please select a square from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button7_Click(sender As Object, e As EventArgs) Handles Guna2Button7.Click
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Increase the boundary from the left
                Dim increaseAmount As Integer = 2 ' Amount by which the boundary should be increased
                square.TopLeft = New Point(square.TopLeft.X - increaseAmount, square.TopLeft.Y)
                square.Size = New Size(square.Size.Width + increaseAmount, square.Size.Height)

                ' Update the DataGridView to reflect the changes
                Guna2DataGridView1.Rows(rowIndex).Cells(2).Value = square.TopLeft.X ' Update the X1 value
                Guna2DataGridView1.Rows(rowIndex).Cells(4).Value = square.TopLeft.X + square.Size.Width ' Update the X2 value
                Guna2DataGridView1.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point

                ' Redraw PictureBox
                PictureBoxDisplay.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a square.")
            End If
        Else
            MessageBox.Show("Please select a square from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button8_Click(sender As Object, e As EventArgs) Handles Guna2Button8.Click
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Contract the boundary from the top
                Dim contractAmount As Integer = 2 ' Amount by which the boundary should be contracted
                If square.Size.Height > contractAmount Then ' Ensure the height doesn't go negative
                    square.TopLeft = New Point(square.TopLeft.X, square.TopLeft.Y + contractAmount)
                    square.Size = New Size(square.Size.Width, square.Size.Height - contractAmount)

                    ' Update the DataGridView to reflect the changes
                    Guna2DataGridView1.Rows(rowIndex).Cells(3).Value = square.TopLeft.Y ' Update the Y1 value
                    Guna2DataGridView1.Rows(rowIndex).Cells(5).Value = square.TopLeft.Y + square.Size.Height ' Update the Y2 value
                    Guna2DataGridView1.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point

                    ' Redraw PictureBox
                    PictureBoxDisplay.Invalidate()
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
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Contract the boundary from the bottom
                Dim contractAmount As Integer = 2 ' Amount by which the boundary should be contracted
                If square.Size.Height > contractAmount Then ' Ensure the height doesn't go negative
                    square.Size = New Size(square.Size.Width, square.Size.Height - contractAmount)

                    ' Update the DataGridView to reflect the changes
                    Guna2DataGridView1.Rows(rowIndex).Cells(5).Value = square.TopLeft.Y + square.Size.Height ' Update the Y2 value
                    Guna2DataGridView1.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point

                    ' Redraw PictureBox
                    PictureBoxDisplay.Invalidate()
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
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Contract the boundary from the right
                Dim contractAmount As Integer = 2 ' Amount by which the boundary should be contracted
                If square.Size.Width > contractAmount Then ' Ensure the width doesn't go negative
                    square.Size = New Size(square.Size.Width - contractAmount, square.Size.Height)

                    ' Update the DataGridView to reflect the changes
                    Guna2DataGridView1.Rows(rowIndex).Cells(4).Value = square.TopLeft.X + square.Size.Width ' Update the X2 value
                    Guna2DataGridView1.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point

                    ' Redraw PictureBox
                    PictureBoxDisplay.Invalidate()
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

    Private Sub Guna2Button11_Click(sender As Object, e As EventArgs) Handles Guna2Button11.Click
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Contract the boundary from the left
                Dim contractAmount As Integer = 2 ' Amount by which the boundary should be contracted
                If square.Size.Width > contractAmount Then ' Ensure the width doesn't go negative
                    square.TopLeft = New Point(square.TopLeft.X + contractAmount, square.TopLeft.Y)
                    square.Size = New Size(square.Size.Width - contractAmount, square.Size.Height)

                    ' Update the DataGridView to reflect the changes
                    Guna2DataGridView1.Rows(rowIndex).Cells(2).Value = square.TopLeft.X ' Update the X1 value
                    Guna2DataGridView1.Rows(rowIndex).Cells(4).Value = square.TopLeft.X + square.Size.Width ' Update the X2 value
                    Guna2DataGridView1.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point

                    ' Redraw PictureBox
                    PictureBoxDisplay.Invalidate()
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

    Private Sub Guna2Button12_Click(sender As Object, e As EventArgs) Handles Guna2Button12.Click
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim circle As Circle = shapes.OfType(Of Circle)().FirstOrDefault(Function(s) s.ID = selectedShapeID)
            If circle IsNot Nothing Then
                Dim moveAmount As Integer = 2 ' Amount by which the circle should be moved
                circle.Center = New Point(circle.Center.X, circle.Center.Y - moveAmount)
                Guna2DataGridView1.Rows(rowIndex).Cells(3).Value = circle.Center.Y - circle.Radius ' Update the Y1 value
                Guna2DataGridView1.Rows(rowIndex).Cells(6).Value = $"({circle.Center.X}, {circle.Center.Y})" ' Update the center point
                PictureBoxDisplay.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a circle.")
            End If
        Else
            MessageBox.Show("Please select a circle from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button15_Click(sender As Object, e As EventArgs) Handles Guna2Button15.Click
        If Guna2DataGridView1.SelectedRows.Count > 0 Then

            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim circle As Circle = shapes.OfType(Of Circle)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If circle IsNot Nothing Then
                ' Move the circle downwards
                Dim moveAmount As Integer = 2
                circle.Center = New Point(circle.Center.X, circle.Center.Y + moveAmount)

                ' Update the DataGridView to reflect the changes
                Guna2DataGridView1.Rows(rowIndex).Cells(3).Value = circle.Center.Y - circle.Radius ' Update the Y1 value
                Guna2DataGridView1.Rows(rowIndex).Cells(6).Value = $"({circle.Center.X}, {circle.Center.Y})" ' Update the center point

                ' Redraw PictureBox
                PictureBoxDisplay.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a circle.")
            End If
        Else
            MessageBox.Show("Please select a circle from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button13_Click(sender As Object, e As EventArgs) Handles Guna2Button13.Click
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim circle As Circle = shapes.OfType(Of Circle)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If circle IsNot Nothing Then
                ' Move the circle to the left
                Dim moveAmount As Integer = 2 ' Amount by which the circle should be moved
                circle.Center = New Point(circle.Center.X - moveAmount, circle.Center.Y)

                ' Update the DataGridView to reflect the changes
                Guna2DataGridView1.Rows(rowIndex).Cells(2).Value = circle.Center.X - circle.Radius ' Update the X1 value
                Guna2DataGridView1.Rows(rowIndex).Cells(6).Value = $"({circle.Center.X}, {circle.Center.Y})" ' Update the center point

                ' Redraw PictureBox
                PictureBoxDisplay.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a circle.")
            End If
        Else
            MessageBox.Show("Please select a circle from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button14_Click(sender As Object, e As EventArgs) Handles Guna2Button14.Click
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim circle As Circle = shapes.OfType(Of Circle)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If circle IsNot Nothing Then
                ' Move the circle to the left
                Dim moveAmount As Integer = 2 ' Amount by which the circle should be moved
                circle.Center = New Point(circle.Center.X + moveAmount, circle.Center.Y)

                ' Update the DataGridView to reflect the changes
                Guna2DataGridView1.Rows(rowIndex).Cells(2).Value = circle.Center.X - circle.Radius ' Update the X1 value
                Guna2DataGridView1.Rows(rowIndex).Cells(6).Value = $"({circle.Center.X}, {circle.Center.Y})" ' Update the center point

                ' Redraw PictureBox
                PictureBoxDisplay.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a circle.")
            End If
        Else
            MessageBox.Show("Please select a circle from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button16_Click(sender As Object, e As EventArgs) Handles Guna2Button16.Click
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim circle As Circle = shapes.OfType(Of Circle)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If circle IsNot Nothing Then
                ' Increase the size of the circle
                Dim increaseAmount As Integer = 2 ' Amount by which the radius should be increased
                circle.Radius += increaseAmount

                ' Update the DataGridView to reflect the changes
                Guna2DataGridView1.Rows(rowIndex).Cells(2).Value = circle.Center.X - circle.Radius ' Update the X1 value
                Guna2DataGridView1.Rows(rowIndex).Cells(3).Value = circle.Center.Y - circle.Radius ' Update the Y1 value
                Guna2DataGridView1.Rows(rowIndex).Cells(4).Value = circle.Radius * 2 ' Update the width (diameter)

                ' Redraw PictureBox
                PictureBoxDisplay.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a circle.")
            End If
        Else
            MessageBox.Show("Please select a circle from the DataGridView.")
        End If
    End Sub

    Private Sub Guna2Button17_Click(sender As Object, e As EventArgs) Handles Guna2Button17.Click
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim circle As Circle = shapes.OfType(Of Circle)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If circle IsNot Nothing Then
                ' Increase the size of the circle
                Dim increaseAmount As Integer = 2 ' Amount by which the radius should be increased
                circle.Radius -= increaseAmount

                ' Update the DataGridView to reflect the changes
                Guna2DataGridView1.Rows(rowIndex).Cells(2).Value = circle.Center.X - circle.Radius ' Update the X1 value
                Guna2DataGridView1.Rows(rowIndex).Cells(3).Value = circle.Center.Y - circle.Radius ' Update the Y1 value
                Guna2DataGridView1.Rows(rowIndex).Cells(4).Value = circle.Radius * 2 ' Update the width (diameter)

                ' Redraw PictureBox
                PictureBoxDisplay.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a circle.")
            End If
        Else
            MessageBox.Show("Please select a circle from the DataGridView.")
        End If

    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        If Guna2DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected row index
            Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

            ' Get the corresponding shape ID
            Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

            ' Find the shape with the corresponding ID
            Dim square As Square = shapes.OfType(Of Square)().FirstOrDefault(Function(s) s.ID = selectedShapeID)

            If square IsNot Nothing Then
                ' Increase the boundary from the top
                Dim increaseAmount As Integer = 2 ' Amount by which the boundary should be increased
                square.TopLeft = New Point(square.TopLeft.X, square.TopLeft.Y - increaseAmount)
                square.Size = New Size(square.Size.Width, square.Size.Height + increaseAmount)

                ' Update the DataGridView to reflect the changes
                Guna2DataGridView1.Rows(rowIndex).Cells(3).Value = square.TopLeft.Y ' Update the Y1 value
                Guna2DataGridView1.Rows(rowIndex).Cells(5).Value = square.TopLeft.Y + square.Size.Height ' Update the Y2 value
                Guna2DataGridView1.Rows(rowIndex).Cells(6).Value = $"({square.TopLeft.X + square.Size.Width / 2}, {square.TopLeft.Y + square.Size.Height / 2})" ' Update the center point

                ' Redraw PictureBox
                PictureBoxDisplay.Invalidate()
            Else
                MessageBox.Show("Selected shape is not a square.")
            End If
        Else
            MessageBox.Show("Please select a square from the DataGridView.")
        End If
    End Sub

    Private Sub btfidW_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub btReturn_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click

    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs)
        RunPython()

    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles TEST.Click
        SavePng()
        ' Check if any row is selected in the DataGridView
        If Guna2DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Please select a shape from the DataGridView.")
            Return
        End If

        ' Get the selected row index
        Dim rowIndex As Integer = Guna2DataGridView1.SelectedRows(0).Index

        ' Get the corresponding shape ID
        Dim selectedShapeID As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)

        ' Find the shape with the corresponding ID
        Dim shapeToSave As Shape = shapes.FirstOrDefault(Function(s) s.ID = selectedShapeID)

        If shapeToSave Is Nothing Then
            MsgBox("Selected shape not found.")
            Return
        End If

        ' Determine the bounding box of the shape
        Dim boundingBox As Rectangle
        If TypeOf shapeToSave Is Square Then
            Dim square As Square = CType(shapeToSave, Square)
            boundingBox = New Rectangle(square.TopLeft, square.Size)
        ElseIf TypeOf shapeToSave Is Circle Then
            Dim circle As Circle = CType(shapeToSave, Circle)
            boundingBox = New Rectangle(circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2)
        Else
            MsgBox("Unknown shape type.")
            Return
        End If

        ' Create a new bitmap with the size of the bounding box
        Dim bitmap As New Bitmap(boundingBox.Width, boundingBox.Height)

        ' Draw the relevant portion of the PictureBox image onto the new bitmap
        Using g As Graphics = Graphics.FromImage(bitmap)
            ' Adjust the source rectangle to match the shape's bounding box within the PictureBox
            Dim sourceRect As New Rectangle(boundingBox.Location, boundingBox.Size)
            Dim destRect As New Rectangle(0, 0, boundingBox.Width, boundingBox.Height)
            g.DrawImage(PictureBoxDisplay.Image, destRect, sourceRect, GraphicsUnit.Pixel)
        End Using

        ' Save the new bitmap to the specified path
        Dim savePath As String = "A:\Project\Visual Path\image.png"
        bitmap.Save(savePath, System.Drawing.Imaging.ImageFormat.Png)

        'MsgBox("Image saved successfully to " & savePath)
        Dim result As DialogResult = MessageBox.Show("Do you want to load fiducials?", "Load Fiducials", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            RunPython()
        End If

    End Sub

    Private Sub PictureBoxDisplay_MouseHover(sender As Object, e As EventArgs) Handles PictureBoxDisplay.MouseHover
        If PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
            PictureBox1.Image = Nothing
        End If
        If pythonProcess IsNot Nothing AndAlso Not pythonProcess.HasExited Then
            pythonProcess.Kill()
            pythonProcess.Dispose()
            pythonProcess = Nothing
        End If
    End Sub



    Private Sub btfidW_MouseLeave(sender As Object, e As EventArgs) Handles btfidW.MouseLeave
        btfidW.BackColor = SystemColors.Control
    End Sub

    Private Sub btfidW_MouseEnter(sender As Object, e As EventArgs) Handles btfidW.MouseEnter
        btfidW.BackColor = Color.LightBlue
    End Sub
    ' Method to capture the current frame from PictureBoxDisplay
    Private Function CaptureCurrentFrame() As Bitmap
        Dim bmp As New Bitmap(PictureBoxDisplay.Width, PictureBoxDisplay.Height)
        PictureBoxDisplay.DrawToBitmap(bmp, New Rectangle(0, 0, PictureBoxDisplay.Width, PictureBoxDisplay.Height))
        Return bmp
    End Function

    ' Method to pause the camera feed and display the captured frame
    Private Sub PauseCameraFeed()
        If m_bIsGrabbing Then
            MyCamera.StopGrabbing()
            m_bIsGrabbing = False
        End If
        ' Capture the current frame and set it as the image in PictureBoxDisplay
        PictureBoxDisplay.Image = CaptureCurrentFrame()
    End Sub

    ' Method to resume the camera feed
    Private Sub ResumeCameraFeed()
        If Not m_bIsGrabbing Then
            PictureBoxDisplay.Image = Nothing ' Clear the paused image
            m_bIsGrabbing = True
            Dim nRet As Int32
            nRet = MyCamera.StartGrabbing()
            If CCamera.MV_OK <> nRet Then
                m_bIsGrabbing = False
                MsgBox("Fail to start grabbing")
            End If
        End If
    End Sub

    ' Event handler for MouseEnter
    Private Sub PictureBoxDisplay_MouseEnter(sender As Object, e As EventArgs) Handles PictureBoxDisplay.MouseEnter
        PauseCameraFeed()
    End Sub

    ' Event handler for MouseLeave
    Private Sub PictureBoxDisplay_MouseLeave(sender As Object, e As EventArgs) Handles PictureBoxDisplay.MouseLeave
        ResumeCameraFeed()
    End Sub


    Private doubleClickCounter As Integer = 0 ' Variable to track the number of double-clicks on the cell

    Private Sub Guna2DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        ' Ensure a valid row index is selected
        If e.RowIndex >= 0 AndAlso e.RowIndex < Guna2DataGridView1.Rows.Count Then
            ' Get the selected row index
            Dim rowIndex As Integer = e.RowIndex

            ' Extract the values from the DataGridView
            Dim serialNo As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(0).Value)
            Dim shape As String = Guna2DataGridView1.Rows(rowIndex).Cells(1).Value.ToString()
            Dim x1 As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(2).Value)
            Dim y1 As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(3).Value)
            Dim x2 As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(4).Value)
            Dim y2 As Integer = Convert.ToInt32(Guna2DataGridView1.Rows(rowIndex).Cells(5).Value)
            Dim center As String = Guna2DataGridView1.Rows(rowIndex).Cells(6).Value.ToString()

            ' Determine which module to save the data based on the double-click count
            If doubleClickCounter = 0 Then
                ' Save to FiducialData1 on the first double-click
                FiducialData1.SelectedShapeData1 = New FiducialData1.DataGridValue1() With {
                .SerialNo1 = serialNo,
                .Shape1 = shape,
                .X11 = x1,
                .Y11 = y1,
                .X21 = x2,
                .Y21 = y2,
                .Center1 = center
            }

                ' Optionally, display a confirmation message
                MessageBox.Show("Shape data saved in FiducialData1 module.")

                ' Increment the double-click counter
                doubleClickCounter = 1
            Else
                ' Save to FiducialData2 on the second double-click
                FiducialData2.SelectedShapeData2 = New FiducialData2.DataGridValue2() With {
                .SerialNo2 = serialNo,
                .Shape2 = shape,
                .X12 = x1,
                .Y12 = y1,
                .X22 = x2,
                .Y22 = y2,
                .Center2 = center
            }

                ' Optionally, display a confirmation message
                MessageBox.Show("Shape data saved in FiducialData2 module.")

                ' Reset the double-click counter for the next cycle
                doubleClickCounter = 0
            End If
        Else
            MessageBox.Show("Invalid row selected.")
        End If
    End Sub




    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick

    End Sub
End Class