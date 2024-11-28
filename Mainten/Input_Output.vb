Imports ActUtlTypeLib
Public Class Input_Output
    Dim plc As New ActUtlType
    Dim X(50) As Integer
    Dim Y(50) As Integer

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
        plc.GetDevice("X47", X(47))
        plc.GetDevice("X44", X(44))
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
        plc.GetDevice("X35", X(35))
        plc.GetDevice("X34", X(34))
        plc.GetDevice("X36", X(36))
        plc.GetDevice("X37", X(37))
        plc.GetDevice("X40", X(40))
        plc.GetDevice("X41", X(41))
        plc.GetDevice("X42", X(42))
        plc.GetDevice("X43", X(43))
        plc.GetDevice("X45", X(45))
        plc.GetDevice("X46", X(46))


        plc.GetDevice("Y0", Y(0))
        plc.GetDevice("Y1", Y(1))
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
        plc.GetDevice("Y33", Y(33))
        plc.GetDevice("Y35", Y(35))
        plc.GetDevice("Y36", Y(36))
        plc.GetDevice("Y37", Y(37))


        UpdatePictureBox(Label0, X(0))
        UpdatePictureBox(Label1, X(1))
        UpdatePictureBox(Label4, X(2))
        UpdatePictureBox(Label5, X(3))
        UpdatePictureBox(Label8, X(4))
        UpdatePictureBox(Label9, X(5))
        UpdatePictureBox(Label12, X(6))
        UpdatePictureBox(Label13, X(7))
        UpdatePictureBox(Label20, X(10))
        UpdatePictureBox(Label16, X(11))
        UpdatePictureBox(Label17, X(12))
        UpdatePictureBox(Label21, X(13))
        UpdatePictureBox(Label24, X(14))
        UpdatePictureBox(Label25, X(15))
        UpdatePictureBox(Label28, X(16))
        UpdatePictureBox(Label29, X(47))
        UpdatePictureBox(Label32, X(44))
        UpdatePictureBox(Label33, X(21))
        UpdatePictureBox(Label36, X(22))
        UpdatePictureBox(Label37, X(23))
        UpdatePictureBox(Label40, X(24))
        UpdatePictureBox(Label41, X(25))
        UpdatePictureBox(Label44, X(26))
        UpdatePictureBox(Label45, X(27))
        UpdatePictureBox(Label48, X(30))
        UpdatePictureBox(Label49, X(31))
        UpdatePictureBox(Label52, X(32))
        UpdatePictureBox(Label53, X(33))
        UpdatePictureBox(Label57, X(35))
        UpdatePictureBox(Label56, X(34))
        UpdatePictureBox(Label60, X(36))
        UpdatePictureBox(Label61, X(37))
        UpdatePictureBox(Label64, X(40))
        UpdatePictureBox(Label65, X(41))
        UpdatePictureBox(Label68, X(42))
        UpdatePictureBox(Label69, X(43))
        UpdatePictureBox(Label72, X(45))
        UpdatePictureBox(Label73, X(46))


        'for the indicator of output '

        UpdatePictureBox(Label2, Y(0))
        UpdatePictureBox(Label3, Y(1))
        UpdatePictureBox(Label7, Y(3))
        UpdatePictureBox(Label10, Y(4))
        UpdatePictureBox(Label11, Y(5))
        UpdatePictureBox(Label14, Y(6))
        UpdatePictureBox(Label15, Y(7))
        UpdatePictureBox(Label18, Y(10))
        UpdatePictureBox(Label19, Y(11))
        UpdatePictureBox(Label22, Y(12))
        UpdatePictureBox(Label23, Y(13))
        UpdatePictureBox(Label26, Y(14))
        UpdatePictureBox(Label27, Y(15))
        UpdatePictureBox(Label30, Y(16))
        UpdatePictureBox(Label31, Y(17))
        UpdatePictureBox(Label34, Y(20))
        UpdatePictureBox(Label35, Y(21))
        UpdatePictureBox(Label38, Y(22))
        UpdatePictureBox(Label39, Y(23))
        UpdatePictureBox(Label42, Y(24))
        UpdatePictureBox(Label43, Y(25))
        UpdatePictureBox(Label46, Y(26))
        UpdatePictureBox(Label47, Y(27))
        UpdatePictureBox(Label50, Y(30))
        UpdatePictureBox(Label51, Y(31))
        UpdatePictureBox(Label54, Y(32))
        UpdatePictureBox(Label55, Y(33))
        UpdatePictureBox(Label58, Y(33))
        UpdatePictureBox(Label59, Y(35))
        UpdatePictureBox(Label62, Y(36))
        UpdatePictureBox(Label63, Y(37))

    End Function
    Private Sub UpdatePictureBox(label As Label, value As Integer)
        If value = 1 Then
            label.BackColor = Color.Green
            label.ForeColor = Color.White
        Else
            label.BackColor = Color.Red
            label.ForeColor = Color.White

        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub

    Private Sub Label75_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label74_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label73_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label72_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label71_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label70_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label69_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label68_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label67_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label66_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label65_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label64_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label63_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label62_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label61_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label60_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label59_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label58_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label57_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label56_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label55_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label54_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label53_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label52_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label51_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label50_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label49_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label48_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label47_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label46_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label45_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label44_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label43_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label42_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label41_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label40_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label39_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label38_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label37_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label36_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label35_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label34_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label33_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label32_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label31_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label30_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label29_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label28_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label27_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label26_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label24_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label23_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label21_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label76_Click(sender As Object, e As EventArgs) Handles Label76.Click

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub Label77_Click(sender As Object, e As EventArgs) Handles Label77.Click

    End Sub

    Private Sub Label21_Click_1(sender As Object, e As EventArgs) Handles Label21.Click

    End Sub
End Class