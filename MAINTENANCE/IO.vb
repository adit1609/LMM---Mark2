Imports ActUtlTypeLib
Imports Gui_Tset.RecepieOperation
Public Class IO
    Dim plc As New ActUtlType

    Private Sub IO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        plc.ActLogicalStationNumber = 1
        plc.Open()

        Label64.BackColor = Color.Transparent

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim X(35) As Integer
        Dim Y(37) As Integer

        'input deviceregister'

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

        'output device register'

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

        'for the indicator of input'

        UpdatePictureBox(Lamp_X0, X(0))
        UpdatePictureBox(Lamp_X1, X(1))
        UpdatePictureBox(Lamp_X2, X(2))
        UpdatePictureBox(Lamp_X3, X(3))
        UpdatePictureBox(Lamp_X4, X(4))
        UpdatePictureBox(Lamp_X5, X(5))
        UpdatePictureBox(Lamp_X6, X(6))
        UpdatePictureBox(Lamp_X7, X(7))              'Spare'
        UpdatePictureBox(Lamp_X10, X(10))
        UpdatePictureBox(Lamp_X11, X(11))
        UpdatePictureBox(Lamp_X12, X(12))
        UpdatePictureBox(Lamp_X13, X(13))
        UpdatePictureBox(Lamp_X14, X(14))
        UpdatePictureBox(Lamp_X15, X(15))
        UpdatePictureBox(Lamp_X16, X(16))
        UpdatePictureBox(Lamp_X17, X(17))
        UpdatePictureBox(Lamp_X20, X(20))
        UpdatePictureBox(Lamp_X21, X(21))
        UpdatePictureBox(Lamp_X22, X(22))
        UpdatePictureBox(Lamp_X23, X(23))
        UpdatePictureBox(Lamp_X24, X(24))
        UpdatePictureBox(Lamp_X25, X(25))
        UpdatePictureBox(Lamp_X26, X(26))
        UpdatePictureBox(Lamp_X27, X(27))
        UpdatePictureBox(Lamp_X30, X(30))
        UpdatePictureBox(Lamp_X31, X(31))
        UpdatePictureBox(Lamp_X32, X(32))
        UpdatePictureBox(Lamp_X33, X(33))
        UpdatePictureBox(Lamp_X34, X(34))
        UpdatePictureBox(Lamp_X35, X(35))


        'for the indicator of output '

        UpdatePictureBox(Lamp_Y0, Y(0))
        UpdatePictureBox(Lamp_Y1, Y(1))
        UpdatePictureBox(Lamp_Y2, Y(2))            '//spare'
        UpdatePictureBox(Lamp_Y3, Y(3))       '//spare'
        UpdatePictureBox(Lamp_Y4, Y(4))
        UpdatePictureBox(Lamp_Y5, Y(5))
        UpdatePictureBox(Lamp_Y6, Y(6))
        UpdatePictureBox(Lamp_Y7, Y(7))
        UpdatePictureBox(Lamp_Y10, Y(10))
        UpdatePictureBox(Lamp_Y11, Y(11))
        UpdatePictureBox(Lamp_Y12, Y(12))
        UpdatePictureBox(Lamp_Y13, Y(13))
        UpdatePictureBox(Lamp_Y14, Y(14))
        UpdatePictureBox(Lamp_Y15, Y(15))
        UpdatePictureBox(Lamp_Y16, Y(16))
        UpdatePictureBox(Lamp_Y17, Y(17))
        UpdatePictureBox(Lamp_Y20, Y(20))  ' // spare'
        UpdatePictureBox(Lamp_Y21, Y(21))   '//spare'
        UpdatePictureBox(Lamp_Y22, Y(22))    '    //spare'
        UpdatePictureBox(Lamp_Y23, Y(23))     '     //spare'
        UpdatePictureBox(Lamp_Y24, Y(24))  '//spare'
        UpdatePictureBox(Lamp_Y25, Y(25))     ' //spare'
        UpdatePictureBox(Lamp_Y26, Y(26))     ' //spare'
        UpdatePictureBox(Lamp_Y27, Y(27))     ' //spare'
        UpdatePictureBox(Lamp_Y30, Y(30))
        UpdatePictureBox(Lamp_Y31, Y(31))
        UpdatePictureBox(Lamp_Y32, Y(32))
        UpdatePictureBox(Lamp_Y33, Y(33))
        UpdatePictureBox(Lamp_Y34, Y(34))
        UpdatePictureBox(Lamp_Y35, Y(35))
        UpdatePictureBox(Lamp_Y36, Y(36))
        UpdatePictureBox(Lamp_Y37, Y(37))


    End Sub


    Private Sub UpdatePictureBox(pictureBox As PictureBox, value As Integer)



        If value = 1 Then
            pictureBox.BackColor = Color.Green
        Else
            pictureBox.BackColor = Color.Red
        End If
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Panel54_Paint(sender As Object, e As PaintEventArgs) Handles Panel54.Paint

    End Sub

    Private Sub Label76_Click(sender As Object, e As EventArgs) Handles Label76.Click

    End Sub

    Private Sub Lamp_X14_Click(sender As Object, e As EventArgs) Handles Lamp_X14.Click

    End Sub

    Private Sub Label50_Click(sender As Object, e As EventArgs) Handles Label50.Click

    End Sub

    Private Sub Lamp_Y25_Click(sender As Object, e As EventArgs) Handles Lamp_Y25.Click

    End Sub

    Private Sub Label99_Click(sender As Object, e As EventArgs) Handles Label99.Click

    End Sub

    Private Sub Label24_Click(sender As Object, e As EventArgs) Handles Label24.Click

    End Sub

    Private Sub Panel53_Paint(sender As Object, e As PaintEventArgs) Handles Panel53.Paint

    End Sub

    Private Sub Label121_Click(sender As Object, e As EventArgs) Handles Label121.Click

    End Sub

    Private Sub Label63_Click(sender As Object, e As EventArgs) Handles Label63.Click

    End Sub

    Private Sub Lamp_Y33_Click(sender As Object, e As EventArgs) Handles Lamp_Y33.Click

    End Sub

    Private Sub Lamp_Y5_Click(sender As Object, e As EventArgs) Handles Lamp_Y5.Click

    End Sub

    Private Sub Label86_Click(sender As Object, e As EventArgs) Handles Label86.Click

    End Sub

    Private Sub Lamp_X25_Click(sender As Object, e As EventArgs) Handles Lamp_X25.Click

    End Sub

    Private Sub Panel52_Paint(sender As Object, e As PaintEventArgs) Handles Panel52.Paint

    End Sub

    Private Sub Label105_Click(sender As Object, e As EventArgs) Handles Label105.Click

    End Sub

    Private Sub Label48_Click(sender As Object, e As EventArgs) Handles Label48.Click

    End Sub

    Private Sub Lamp_Y13_Click(sender As Object, e As EventArgs) Handles Lamp_Y13.Click

    End Sub

    Private Sub Label23_Click(sender As Object, e As EventArgs) Handles Label23.Click

    End Sub

    Private Sub Label70_Click(sender As Object, e As EventArgs) Handles Label70.Click

    End Sub

    Private Sub Lamp_X5_Click(sender As Object, e As EventArgs) Handles Lamp_X5.Click

    End Sub

    Private Sub Panel51_Paint(sender As Object, e As PaintEventArgs) Handles Panel51.Paint

    End Sub

    Private Sub Label91_Click(sender As Object, e As EventArgs) Handles Label91.Click

    End Sub

    Private Sub Lamp_X33_Click(sender As Object, e As EventArgs) Handles Lamp_X33.Click

    End Sub

    Private Sub Label47_Click(sender As Object, e As EventArgs) Handles Label47.Click

    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click

    End Sub

    Private Sub Label37_Click(sender As Object, e As EventArgs) Handles Label37.Click

    End Sub

    Private Sub Label115_Click(sender As Object, e As EventArgs) Handles Label115.Click

    End Sub

    Private Sub Panel50_Paint(sender As Object, e As PaintEventArgs) Handles Panel50.Paint

    End Sub

    Private Sub Label75_Click(sender As Object, e As EventArgs) Handles Label75.Click

    End Sub

    Private Sub Lamp_X13_Click(sender As Object, e As EventArgs) Handles Lamp_X13.Click

    End Sub

    Private Sub Label46_Click(sender As Object, e As EventArgs) Handles Label46.Click

    End Sub

    Private Sub Lamp_Y24_Click(sender As Object, e As EventArgs) Handles Lamp_Y24.Click

    End Sub

    Private Sub Label98_Click(sender As Object, e As EventArgs) Handles Label98.Click

    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click

    End Sub

    Private Sub Panel49_Paint(sender As Object, e As PaintEventArgs) Handles Panel49.Paint

    End Sub

    Private Sub Label120_Click(sender As Object, e As EventArgs) Handles Label120.Click

    End Sub

    Private Sub Label62_Click(sender As Object, e As EventArgs) Handles Label62.Click

    End Sub

    Private Sub Lamp_Y32_Click(sender As Object, e As EventArgs) Handles Lamp_Y32.Click

    End Sub

    Private Sub Lamp_Y4_Click(sender As Object, e As EventArgs) Handles Lamp_Y4.Click

    End Sub

    Private Sub Label85_Click(sender As Object, e As EventArgs) Handles Label85.Click

    End Sub

    Private Sub Lamp_X24_Click(sender As Object, e As EventArgs) Handles Lamp_X24.Click

    End Sub

    Private Sub Panel48_Paint(sender As Object, e As PaintEventArgs) Handles Panel48.Paint

    End Sub

    Private Sub Label104_Click(sender As Object, e As EventArgs) Handles Label104.Click

    End Sub

    Private Sub Label44_Click(sender As Object, e As EventArgs) Handles Label44.Click

    End Sub

    Private Sub Lamp_Y12_Click(sender As Object, e As EventArgs) Handles Lamp_Y12.Click

    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click

    End Sub

    Private Sub Label69_Click(sender As Object, e As EventArgs) Handles Label69.Click

    End Sub

    Private Sub Lamp_X4_Click(sender As Object, e As EventArgs) Handles Lamp_X4.Click

    End Sub

    Private Sub Panel47_Paint(sender As Object, e As PaintEventArgs) Handles Panel47.Paint

    End Sub

    Private Sub Label126_Click(sender As Object, e As EventArgs) Handles Label126.Click

    End Sub

    Private Sub Lamp_X32_Click(sender As Object, e As EventArgs) Handles Lamp_X32.Click

    End Sub

    Private Sub Label43_Click(sender As Object, e As EventArgs) Handles Label43.Click

    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click

    End Sub

    Private Sub Label33_Click(sender As Object, e As EventArgs) Handles Label33.Click

    End Sub

    Private Sub Label114_Click(sender As Object, e As EventArgs) Handles Label114.Click

    End Sub

    Private Sub Label56_Click(sender As Object, e As EventArgs) Handles Label56.Click

    End Sub

    Private Sub Panel46_Paint(sender As Object, e As PaintEventArgs) Handles Panel46.Paint

    End Sub

    Private Sub Label74_Click(sender As Object, e As EventArgs) Handles Label74.Click

    End Sub

    Private Sub Lamp_X12_Click(sender As Object, e As EventArgs) Handles Lamp_X12.Click

    End Sub

    Private Sub Label42_Click(sender As Object, e As EventArgs) Handles Label42.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Panel38_Paint(sender As Object, e As PaintEventArgs) Handles Panel38.Paint

    End Sub

    Private Sub Label72_Click(sender As Object, e As EventArgs) Handles Label72.Click

    End Sub

    Private Sub Lamp_X10_Click(sender As Object, e As EventArgs) Handles Lamp_X10.Click

    End Sub

    Private Sub Label34_Click(sender As Object, e As EventArgs) Handles Label34.Click

    End Sub

    Private Sub Label117_Click(sender As Object, e As EventArgs) Handles Label117.Click

    End Sub

    Private Sub Lamp_Y27_Click(sender As Object, e As EventArgs) Handles Lamp_Y27.Click

    End Sub

    Private Sub Label101_Click(sender As Object, e As EventArgs) Handles Label101.Click

    End Sub

    Private Sub Panel39_Paint(sender As Object, e As PaintEventArgs) Handles Panel39.Paint

    End Sub

    Private Sub Label89_Click(sender As Object, e As EventArgs) Handles Label89.Click

    End Sub

    Private Sub Lamp_X30_Click(sender As Object, e As EventArgs) Handles Lamp_X30.Click

    End Sub

    Private Sub Label35_Click(sender As Object, e As EventArgs) Handles Label35.Click

    End Sub

    Private Sub Label32_Click(sender As Object, e As EventArgs) Handles Label32.Click

    End Sub

    Private Sub Lamp_Y7_Click(sender As Object, e As EventArgs) Handles Lamp_Y7.Click

    End Sub

    Private Sub Label88_Click(sender As Object, e As EventArgs) Handles Label88.Click

    End Sub

    Private Sub Panel40_Paint(sender As Object, e As PaintEventArgs) Handles Panel40.Paint

    End Sub

    Private Sub Label102_Click(sender As Object, e As EventArgs) Handles Label102.Click

    End Sub

    Private Sub Label36_Click(sender As Object, e As EventArgs) Handles Label36.Click

    End Sub

    Private Sub Lamp_Y10_Click(sender As Object, e As EventArgs) Handles Lamp_Y10.Click

    End Sub

    Private Sub Lamp_X27_Click(sender As Object, e As EventArgs) Handles Lamp_X27.Click

    End Sub

    Private Sub Label31_Click(sender As Object, e As EventArgs) Handles Label31.Click

    End Sub

    Private Sub Label81_Click(sender As Object, e As EventArgs) Handles Label81.Click

    End Sub

    Private Sub Panel41_Paint(sender As Object, e As PaintEventArgs) Handles Panel41.Paint

    End Sub

    Private Sub Label118_Click(sender As Object, e As EventArgs) Handles Label118.Click

    End Sub

    Private Sub Label49_Click(sender As Object, e As EventArgs) Handles Label49.Click

    End Sub

    Private Sub Lamp_Y30_Click(sender As Object, e As EventArgs) Handles Lamp_Y30.Click

    End Sub

    Private Sub Lamp_X7_Click(sender As Object, e As EventArgs) Handles Lamp_X7.Click

    End Sub

    Private Sub Label41_Click(sender As Object, e As EventArgs) Handles Label41.Click

    End Sub

    Private Sub Label30_Click(sender As Object, e As EventArgs) Handles Label30.Click

    End Sub

    Private Sub Panel42_Paint(sender As Object, e As PaintEventArgs) Handles Panel42.Paint

    End Sub

    Private Sub Label73_Click(sender As Object, e As EventArgs) Handles Label73.Click

    End Sub

    Private Sub Lamp_X11_Click(sender As Object, e As EventArgs) Handles Lamp_X11.Click

    End Sub

    Private Sub Label38_Click(sender As Object, e As EventArgs) Handles Label38.Click

    End Sub

    Private Sub Label116_Click(sender As Object, e As EventArgs) Handles Label116.Click

    End Sub

    Private Sub Lamp_Y26_Click(sender As Object, e As EventArgs) Handles Lamp_Y26.Click

    End Sub

    Private Sub Label100_Click(sender As Object, e As EventArgs) Handles Label100.Click

    End Sub

    Private Sub Panel43_Paint(sender As Object, e As PaintEventArgs) Handles Panel43.Paint

    End Sub

    Private Sub Label90_Click(sender As Object, e As EventArgs) Handles Label90.Click

    End Sub

    Private Sub Lamp_X31_Click(sender As Object, e As EventArgs) Handles Lamp_X31.Click

    End Sub

    Private Sub Label39_Click(sender As Object, e As EventArgs) Handles Label39.Click

    End Sub

    Private Sub Label28_Click(sender As Object, e As EventArgs) Handles Label28.Click

    End Sub

    Private Sub Lamp_Y6_Click(sender As Object, e As EventArgs) Handles Lamp_Y6.Click

    End Sub

    Private Sub Label87_Click(sender As Object, e As EventArgs) Handles Label87.Click

    End Sub

    Private Sub Panel44_Paint(sender As Object, e As PaintEventArgs) Handles Panel44.Paint

    End Sub

    Private Sub Label103_Click(sender As Object, e As EventArgs) Handles Label103.Click

    End Sub

    Private Sub Label40_Click(sender As Object, e As EventArgs) Handles Label40.Click

    End Sub

    Private Sub Lamp_Y11_Click(sender As Object, e As EventArgs) Handles Lamp_Y11.Click

    End Sub

    Private Sub Lamp_X26_Click(sender As Object, e As EventArgs) Handles Lamp_X26.Click

    End Sub

    Private Sub Label27_Click(sender As Object, e As EventArgs) Handles Label27.Click

    End Sub

    Private Sub Label71_Click(sender As Object, e As EventArgs) Handles Label71.Click

    End Sub

    Private Sub Panel45_Paint(sender As Object, e As PaintEventArgs) Handles Panel45.Paint

    End Sub

    Private Sub Label119_Click(sender As Object, e As EventArgs) Handles Label119.Click

    End Sub

    Private Sub Label53_Click(sender As Object, e As EventArgs) Handles Label53.Click

    End Sub

    Private Sub Lamp_Y31_Click(sender As Object, e As EventArgs) Handles Lamp_Y31.Click

    End Sub

    Private Sub Lamp_X6_Click(sender As Object, e As EventArgs) Handles Lamp_X6.Click

    End Sub

    Private Sub Label45_Click(sender As Object, e As EventArgs) Handles Label45.Click

    End Sub

    Private Sub Label26_Click(sender As Object, e As EventArgs) Handles Label26.Click

    End Sub

    Private Sub Label113_Click(sender As Object, e As EventArgs) Handles Label113.Click

    End Sub

    Private Sub Label95_Click(sender As Object, e As EventArgs) Handles Label95.Click

    End Sub

    Private Sub Panel64_Paint(sender As Object, e As PaintEventArgs) Handles Panel64.Paint

    End Sub

    Private Sub Panel76_Paint(sender As Object, e As PaintEventArgs) Handles Panel76.Paint

    End Sub

    Private Sub Label108_Click(sender As Object, e As EventArgs) Handles Label108.Click

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click

    End Sub

    Private Sub Lamp_Y16_Click(sender As Object, e As EventArgs) Handles Lamp_Y16.Click

    End Sub

    Private Sub Label82_Click(sender As Object, e As EventArgs) Handles Label82.Click

    End Sub

    Private Sub Lamp_X21_Click(sender As Object, e As EventArgs) Handles Lamp_X21.Click

    End Sub

    Private Sub Lamp_X20_Click(sender As Object, e As EventArgs) Handles Lamp_X20.Click

    End Sub

    Private Sub Label66_Click(sender As Object, e As EventArgs) Handles Label66.Click

    End Sub

    Private Sub Panel65_Paint(sender As Object, e As PaintEventArgs) Handles Panel65.Paint

    End Sub

    Private Sub Panel77_Paint(sender As Object, e As PaintEventArgs) Handles Panel77.Paint

    End Sub

    Private Sub Label124_Click(sender As Object, e As EventArgs) Handles Label124.Click

    End Sub

    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click

    End Sub

    Private Sub Lamp_Y36_Click(sender As Object, e As EventArgs) Handles Lamp_Y36.Click

    End Sub

    Private Sub Lamp_X1_Click(sender As Object, e As EventArgs) Handles Lamp_X1.Click

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click

    End Sub

    Private Sub Label110_Click(sender As Object, e As EventArgs) Handles Label110.Click

    End Sub

    Private Sub Panel66_Paint(sender As Object, e As PaintEventArgs) Handles Panel66.Paint

    End Sub

    Private Sub Panel81_Paint(sender As Object, e As PaintEventArgs) Handles Panel81.Paint

    End Sub

    Private Sub Label79_Click(sender As Object, e As EventArgs) Handles Label79.Click

    End Sub

    Private Sub Lamp_X17_Click(sender As Object, e As EventArgs) Handles Lamp_X17.Click

    End Sub

    Private Sub Label65_Click(sender As Object, e As EventArgs) Handles Label65.Click

    End Sub

    Private Sub Lamp_Y20_Click(sender As Object, e As EventArgs) Handles Lamp_Y20.Click

    End Sub

    Private Sub Panel67_Paint(sender As Object, e As PaintEventArgs) Handles Panel67.Paint

    End Sub

    Private Sub Panel80_Paint(sender As Object, e As PaintEventArgs) Handles Panel80.Paint

    End Sub

    Private Sub Label94_Click(sender As Object, e As EventArgs) Handles Label94.Click

    End Sub

    Private Sub Lamp_Y23_Click(sender As Object, e As EventArgs) Handles Lamp_Y23.Click

    End Sub

    Private Sub Lamp_Y0_Click(sender As Object, e As EventArgs) Handles Lamp_Y0.Click

    End Sub

    Private Sub Label80_Click(sender As Object, e As EventArgs) Handles Label80.Click

    End Sub

    Private Sub Panel68_Paint(sender As Object, e As PaintEventArgs) Handles Panel68.Paint

    End Sub

    Private Sub Panel79_Paint(sender As Object, e As PaintEventArgs) Handles Panel79.Paint

    End Sub

    Private Sub Label109_Click(sender As Object, e As EventArgs) Handles Label109.Click

    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click

    End Sub

    Private Sub Lamp_Y17_Click(sender As Object, e As EventArgs) Handles Lamp_Y17.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label64_Click(sender As Object, e As EventArgs) Handles Label64.Click

    End Sub

    Private Sub Panel69_Paint(sender As Object, e As PaintEventArgs) Handles Panel69.Paint

    End Sub

    Private Sub Label125_Click(sender As Object, e As EventArgs) Handles Label125.Click

    End Sub

    Private Sub Label59_Click(sender As Object, e As EventArgs) Handles Label59.Click

    End Sub

    Private Sub Lamp_Y37_Click(sender As Object, e As EventArgs) Handles Lamp_Y37.Click

    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Panel70_Paint(sender As Object, e As PaintEventArgs) Handles Panel70.Paint

    End Sub

    Private Sub Label77_Click(sender As Object, e As EventArgs) Handles Label77.Click

    End Sub

    Private Sub Lamp_X15_Click(sender As Object, e As EventArgs) Handles Lamp_X15.Click

    End Sub

    Private Sub Label54_Click(sender As Object, e As EventArgs) Handles Label54.Click

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Label57_Click(sender As Object, e As EventArgs) Handles Label57.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

    End Sub

    Private Sub Panel55_Paint(sender As Object, e As PaintEventArgs) Handles Panel55.Paint

    End Sub

    Private Sub Label92_Click(sender As Object, e As EventArgs) Handles Label92.Click

    End Sub

    Private Sub Lamp_X34_Click(sender As Object, e As EventArgs) Handles Lamp_X34.Click

    End Sub

    Private Sub Label51_Click(sender As Object, e As EventArgs) Handles Label51.Click

    End Sub

    Private Sub Label97_Click(sender As Object, e As EventArgs) Handles Label97.Click

    End Sub

    Private Sub Lamp_Y3_Click(sender As Object, e As EventArgs) Handles Lamp_Y3.Click

    End Sub

    Private Sub Label84_Click(sender As Object, e As EventArgs) Handles Label84.Click

    End Sub

    Private Sub Panel56_Paint(sender As Object, e As PaintEventArgs) Handles Panel56.Paint

    End Sub

    Private Sub Label106_Click(sender As Object, e As EventArgs) Handles Label106.Click

    End Sub

    Private Sub Label52_Click(sender As Object, e As EventArgs) Handles Label52.Click

    End Sub

    Private Sub Lamp_Y14_Click(sender As Object, e As EventArgs) Handles Lamp_Y14.Click

    End Sub

    Private Sub Lamp_X23_Click(sender As Object, e As EventArgs) Handles Lamp_X23.Click

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click

    End Sub

    Private Sub Label68_Click(sender As Object, e As EventArgs) Handles Label68.Click

    End Sub

    Private Sub Panel57_Paint(sender As Object, e As PaintEventArgs) Handles Panel57.Paint

    End Sub

    Private Sub Label122_Click(sender As Object, e As EventArgs) Handles Label122.Click

    End Sub

    Private Sub Label60_Click(sender As Object, e As EventArgs) Handles Label60.Click

    End Sub

    Private Sub Lamp_Y34_Click(sender As Object, e As EventArgs) Handles Lamp_Y34.Click

    End Sub

    Private Sub Lamp_X3_Click(sender As Object, e As EventArgs) Handles Lamp_X3.Click

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub Label29_Click(sender As Object, e As EventArgs) Handles Label29.Click

    End Sub

    Private Sub Label112_Click(sender As Object, e As EventArgs) Handles Label112.Click

    End Sub

    Private Sub Panel59_Paint(sender As Object, e As PaintEventArgs) Handles Panel59.Paint

    End Sub

    Private Sub Panel71_Paint(sender As Object, e As PaintEventArgs) Handles Panel71.Paint

    End Sub

    Private Sub Label93_Click(sender As Object, e As EventArgs) Handles Label93.Click

    End Sub

    Private Sub Lamp_X35_Click(sender As Object, e As EventArgs) Handles Lamp_X35.Click

    End Sub

    Private Sub Label55_Click(sender As Object, e As EventArgs) Handles Label55.Click

    End Sub

    Private Sub Lamp_Y21_Click(sender As Object, e As EventArgs) Handles Lamp_Y21.Click

    End Sub

    Private Sub Lamp_Y22_Click(sender As Object, e As EventArgs) Handles Lamp_Y22.Click

    End Sub

    Private Sub Label96_Click(sender As Object, e As EventArgs) Handles Label96.Click

    End Sub

    Private Sub Lamp_Y2_Click(sender As Object, e As EventArgs) Handles Lamp_Y2.Click

    End Sub

    Private Sub Panel60_Paint(sender As Object, e As PaintEventArgs) Handles Panel60.Paint

    End Sub

    Private Sub Panel72_Paint(sender As Object, e As PaintEventArgs) Handles Panel72.Paint

    End Sub

    Private Sub Label107_Click(sender As Object, e As EventArgs) Handles Label107.Click

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Lamp_Y15_Click(sender As Object, e As EventArgs) Handles Lamp_Y15.Click

    End Sub

    Private Sub Label83_Click(sender As Object, e As EventArgs) Handles Label83.Click

    End Sub

    Private Sub Lamp_X22_Click(sender As Object, e As EventArgs) Handles Lamp_X22.Click

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub Label67_Click(sender As Object, e As EventArgs) Handles Label67.Click

    End Sub

    Private Sub Panel61_Paint(sender As Object, e As PaintEventArgs) Handles Panel61.Paint

    End Sub

    Private Sub Panel73_Paint(sender As Object, e As PaintEventArgs) Handles Panel73.Paint

    End Sub

    Private Sub Label123_Click(sender As Object, e As EventArgs) Handles Label123.Click

    End Sub

    Private Sub Label61_Click(sender As Object, e As EventArgs) Handles Label61.Click

    End Sub

    Private Sub Lamp_Y35_Click(sender As Object, e As EventArgs) Handles Lamp_Y35.Click

    End Sub

    Private Sub Lamp_X2_Click(sender As Object, e As EventArgs) Handles Lamp_X2.Click

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs) Handles Label25.Click

    End Sub

    Private Sub Label111_Click(sender As Object, e As EventArgs) Handles Label111.Click

    End Sub

    Private Sub Panel63_Paint(sender As Object, e As PaintEventArgs) Handles Panel63.Paint

    End Sub

    Private Sub Panel62_Paint(sender As Object, e As PaintEventArgs) Handles Panel62.Paint

    End Sub

    Private Sub Panel74_Paint(sender As Object, e As PaintEventArgs) Handles Panel74.Paint

    End Sub

    Private Sub Label78_Click(sender As Object, e As EventArgs) Handles Label78.Click

    End Sub

    Private Sub Lamp_X16_Click(sender As Object, e As EventArgs) Handles Lamp_X16.Click

    End Sub

    Private Sub Label58_Click(sender As Object, e As EventArgs) Handles Label58.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Lamp_Y1_Click(sender As Object, e As EventArgs) Handles Lamp_Y1.Click

    End Sub

    Private Sub Panel37_Paint(sender As Object, e As PaintEventArgs) Handles Panel37.Paint

    End Sub

    Private Sub Panel28_Paint(sender As Object, e As PaintEventArgs) Handles Panel28.Paint

    End Sub

    Private Sub Panel27_Paint(sender As Object, e As PaintEventArgs) Handles Panel27.Paint

    End Sub

    Private Sub Panel26_Paint(sender As Object, e As PaintEventArgs) Handles Panel26.Paint

    End Sub

    Private Sub Panel25_Paint(sender As Object, e As PaintEventArgs) Handles Panel25.Paint

    End Sub

    Private Sub Panel24_Paint(sender As Object, e As PaintEventArgs) Handles Panel24.Paint

    End Sub

    Private Sub Panel23_Paint(sender As Object, e As PaintEventArgs) Handles Panel23.Paint

    End Sub

    Private Sub Panel22_Paint(sender As Object, e As PaintEventArgs) Handles Panel22.Paint

    End Sub

    Private Sub Panel21_Paint(sender As Object, e As PaintEventArgs) Handles Panel21.Paint

    End Sub

    Private Sub Lamp_X0_Click(sender As Object, e As EventArgs) Handles Lamp_X0.Click

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub

    Private Sub Panel36_Paint(sender As Object, e As PaintEventArgs) Handles Panel36.Paint

    End Sub

    Private Sub Panel35_Paint(sender As Object, e As PaintEventArgs) Handles Panel35.Paint

    End Sub

    Private Sub Panel34_Paint(sender As Object, e As PaintEventArgs) Handles Panel34.Paint

    End Sub

    Private Sub Panel33_Paint(sender As Object, e As PaintEventArgs) Handles Panel33.Paint

    End Sub

    Private Sub Panel32_Paint(sender As Object, e As PaintEventArgs) Handles Panel32.Paint

    End Sub

    Private Sub Panel31_Paint(sender As Object, e As PaintEventArgs) Handles Panel31.Paint

    End Sub

    Private Sub Panel30_Paint(sender As Object, e As PaintEventArgs) Handles Panel30.Paint

    End Sub

    Private Sub Panel29_Paint(sender As Object, e As PaintEventArgs) Handles Panel29.Paint

    End Sub

    Private Sub Panel20_Paint(sender As Object, e As PaintEventArgs) Handles Panel20.Paint

    End Sub

    Private Sub Panel19_Paint(sender As Object, e As PaintEventArgs) Handles Panel19.Paint

    End Sub

    Private Sub Panel18_Paint(sender As Object, e As PaintEventArgs) Handles Panel18.Paint

    End Sub

    Private Sub Panel17_Paint(sender As Object, e As PaintEventArgs) Handles Panel17.Paint

    End Sub

    Private Sub Panel16_Paint(sender As Object, e As PaintEventArgs) Handles Panel16.Paint

    End Sub

    Private Sub Panel15_Paint(sender As Object, e As PaintEventArgs) Handles Panel15.Paint

    End Sub

    Private Sub Panel14_Paint(sender As Object, e As PaintEventArgs) Handles Panel14.Paint

    End Sub

    Private Sub Panel13_Paint(sender As Object, e As PaintEventArgs) Handles Panel13.Paint

    End Sub

    Private Sub Panel12_Paint(sender As Object, e As PaintEventArgs) Handles Panel12.Paint

    End Sub

    Private Sub Panel11_Paint(sender As Object, e As PaintEventArgs) Handles Panel11.Paint

    End Sub

    Private Sub Panel10_Paint(sender As Object, e As PaintEventArgs) Handles Panel10.Paint

    End Sub

    Private Sub Panel9_Paint(sender As Object, e As PaintEventArgs) Handles Panel9.Paint

    End Sub

    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs) Handles Panel7.Paint

    End Sub

    Private Sub Panel6_Paint(sender As Object, e As PaintEventArgs) Handles Panel6.Paint

    End Sub

    Private Sub Panel8_Paint(sender As Object, e As PaintEventArgs) Handles Panel8.Paint

    End Sub

    Private Sub Panel58_Paint(sender As Object, e As PaintEventArgs) Handles Panel58.Paint

    End Sub
End Class