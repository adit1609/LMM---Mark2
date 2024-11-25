Imports ActUtlTypeLib
Public Class Input_Output
    Dim plc As New ActUtlType
    Dim X(35) As Integer
    Dim Y(37) As Integer

    Public Sub plcon()
        plc.ActLogicalStationNumber = 1
        plc.Open()
        Timer1.Start()
    End Sub

    Private Async Function Input_Output_Load(sender As Object, e As EventArgs) As Task Handles MyBase.Load
        plcon()
    End Function

    Private Async Function Timer1_Tick(sender As Object, e As EventArgs) As Task Handles Timer1.Tick
        plc.GetDevice("X0", X(0))
        plc.GetDevice("X1", X(1))
        plc.GetDevice("X2", X(2))
        plc.GetDevice("X3", X(3))
        plc.GetDevice("X4", X(4))
        plc.GetDevice("X5", X(5))
        plc.GetDevice("X6", X(6))
        plc.GetDevice("X7", X(7))
        plc.GetDevice("X10", X(10))
        plc.GetDevice("X11", X(11))
        plc.GetDevice("X12", X(12))
        plc.GetDevice("X13", X(13))
        plc.GetDevice("X14", X(14))
        plc.GetDevice("X15", X(15))
        plc.GetDevice("X16", X(16))
        plc.GetDevice("X17", X(17))
        plc.GetDevice("X20", X(20))
        plc.GetDevice("X21", X(21))
        plc.GetDevice("X22", X(22))
        plc.GetDevice("X23", X(23))
        plc.GetDevice("X24", X(24))
        plc.GetDevice("X25", X(25))
        plc.GetDevice("X26", X(26))
        plc.GetDevice("X27", X(27))
        plc.GetDevice("X30", X(30))
        plc.GetDevice("X31", X(31))
        plc.GetDevice("X32", X(32))
        plc.GetDevice("X33", X(33))
        plc.GetDevice("X34", X(34))
        plc.GetDevice("X35", X(35))

        plc.GetDevice("Y0", Y(0))
        plc.GetDevice("Y1", Y(1))
        plc.GetDevice("Y2", Y(2))
        plc.GetDevice("Y3", Y(3))
        plc.GetDevice("Y4", Y(4))
        plc.GetDevice("Y5", Y(5))
        plc.GetDevice("Y6", Y(6))
        plc.GetDevice("Y7", Y(7))
        plc.GetDevice("Y10", Y(10))
        plc.GetDevice("Y11", Y(11))
        plc.GetDevice("Y12", Y(12))
        plc.GetDevice("Y13", Y(13))
        plc.GetDevice("Y14", Y(14))
        plc.GetDevice("Y15", Y(15))
        plc.GetDevice("Y16", Y(16))
        plc.GetDevice("Y17", Y(17))
        plc.GetDevice("Y20", Y(20))
        plc.GetDevice("Y21", Y(21))
        plc.GetDevice("Y22", Y(22))
        plc.GetDevice("Y23", Y(23))
        plc.GetDevice("Y24", Y(24))
        plc.GetDevice("Y25", Y(25))
        plc.GetDevice("Y26", Y(26))
        plc.GetDevice("Y27", Y(27))
        plc.GetDevice("Y30", Y(30))
        plc.GetDevice("Y31", Y(31))
        plc.GetDevice("Y32", Y(32))
        plc.GetDevice("Y33", Y(33))
        plc.GetDevice("Y34", Y(34))
        plc.GetDevice("Y35", Y(35))
        plc.GetDevice("Y36", Y(36))
        plc.GetDevice("Y37", Y(37))

        UpdatePictureBox(Label0, X(0))
        UpdatePictureBox(Label1, X(1))
        UpdatePictureBox(Label2, X(2))
        UpdatePictureBox(Label3, X(3))
        UpdatePictureBox(Label4, X(4))
        UpdatePictureBox(Label5, X(5))
        UpdatePictureBox(Label6, X(6))
        UpdatePictureBox(Label7, X(7))              'Spare'
        UpdatePictureBox(Label10, X(10))
        UpdatePictureBox(Label11, X(11))
        UpdatePictureBox(Label12, X(12))
        UpdatePictureBox(Label13, X(13))
        UpdatePictureBox(Label14, X(14))
        UpdatePictureBox(Label15, X(15))
        UpdatePictureBox(Label16, X(16))
        UpdatePictureBox(Label17, X(17))
        UpdatePictureBox(Label20, X(20))
        UpdatePictureBox(Label21, X(21))
        UpdatePictureBox(Label22, X(22))
        UpdatePictureBox(Label23, X(23))
        UpdatePictureBox(Label24, X(24))
        UpdatePictureBox(Label25, X(25))
        UpdatePictureBox(Label26, X(26))
        UpdatePictureBox(Label27, X(27))
        UpdatePictureBox(Label30, X(30))
        UpdatePictureBox(Label31, X(31))
        UpdatePictureBox(Label32, X(32))
        UpdatePictureBox(Label33, X(33))
        UpdatePictureBox(Label34, X(34))
        UpdatePictureBox(Label35, X(35))


        'for the indicator of output '

        UpdatePictureBox(Label2, Y(0))
        UpdatePictureBox(Label3, Y(1))
        'UpdatePictureBox(Label2, Y(2))            '//spare'
        'UpdatePictureBox(Label3, Y(3))       '//spare'
        UpdatePictureBox(Label4, Y(4))
        UpdatePictureBox(Label5, Y(5))
        UpdatePictureBox(Label6, Y(6))
        UpdatePictureBox(Label7, Y(7))
        UpdatePictureBox(Label10, Y(10))
        UpdatePictureBox(Label11, Y(11))
        UpdatePictureBox(Label12, Y(12))
        UpdatePictureBox(Label13, Y(13))
        UpdatePictureBox(Label14, Y(14))
        UpdatePictureBox(Label15, Y(15))
        UpdatePictureBox(Label16, Y(16))
        UpdatePictureBox(Label17, Y(17))
        'UpdatePictureBox(Label20, Y(20))  ' // spare'
        'UpdatePictureBox(Label21, Y(21))   '//spare'
        'UpdatePictureBox(Label22, Y(22))    '    //spare'
        'UpdatePictureBox(Label23, Y(23))     '     //spare'
        'UpdatePictureBox(Label24, Y(24))  '//spare'
        'UpdatePictureBox(Label25, Y(25))     ' //spare'
        'UpdatePictureBox(Label26, Y(26))     ' //spare'
        'UpdatePictureBox(Label27, Y(27))     ' //spare'
        UpdatePictureBox(Label30, Y(30))
        UpdatePictureBox(Label31, Y(31))
        UpdatePictureBox(Label32, Y(32))
        UpdatePictureBox(Label33, Y(33))
        UpdatePictureBox(Label34, Y(34))
        UpdatePictureBox(Label35, Y(35))
        UpdatePictureBox(Label36, Y(36))
        UpdatePictureBox(Label37, Y(37))

    End Function
    Private Sub UpdatePictureBox(label As Label, value As Integer)
        If value = 1 Then
            label.BackColor = Color.Green
        Else
            label.BackColor = Color.Red
        End If
    End Sub
End Class