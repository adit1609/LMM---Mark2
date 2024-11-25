Imports ActUtlTypeLib
Public Class Servo
    Private currentChildform As Form
    Dim plc As New ActUtlType

    Private Sub Home_Page(_childfrm As Form)
        If currentChildform IsNot Nothing Then
            currentChildform.Close()
        End If
        currentChildform = _childfrm

        _childfrm.TopLevel = False
        _childfrm.FormBorderStyle = FormBorderStyle.None
        _childfrm.Dock = DockStyle.Fill
        Panel3.Controls.Add(_childfrm)
        _childfrm.BringToFront()
        _childfrm.Show()


    End Sub
    Private Sub Servo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Home_Page(New X)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Home_Page(New X)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Home_Page(New Y)
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Home_Page(New Y)
    End Sub
    Private Sub btnBack_MouseDown(sender As Object, e As MouseEventArgs) Handles btnBack.MouseDown
        plc.SetDevice("M215", 1)
        Dim input As Integer
        plc.GetDevice("M101", input)
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

    End Sub

    Private Sub btnBack_MouseUp(sender As Object, e As MouseEventArgs) Handles btnBack.MouseUp
        plc.SetDevice("M215", 0)

    End Sub

    Private Sub btnForward_MouseDown(sender As Object, e As MouseEventArgs) Handles btnForward.MouseDown
        plc.SetDevice("M216", 1)
    End Sub

    Private Sub btnForward_MouseUp(sender As Object, e As MouseEventArgs) Handles btnForward.MouseUp
        plc.SetDevice("M216", 0)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SERVOSPEED.SelectedIndexChanged
        If plc IsNot Nothing Then



            plc.SetDevice("D150", SERVOSPEED.SelectedIndex)

        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim selects As Integer

        plc.GetDevice("D150", selects)

        SERVOSPEED.SelectedIndex = selects



        ' Set the mode text directly to the ComboBox's text property

    End Sub

    Private Sub btn_ServoRST_MouseDown(sender As Object, e As MouseEventArgs) Handles btn_ServoRST.MouseDown
        plc.SetDevice("M223", 1)
        Dim input As Integer
        plc.GetDevice("D151", input)
    End Sub

    Private Sub btn_ServoRST_MouseUp(sender As Object, e As MouseEventArgs) Handles btn_ServoRST.MouseUp
        plc.SetDevice("M223", 0)
    End Sub

    Private Sub btn_ServoH_MouseDown(sender As Object, e As MouseEventArgs) Handles btn_ServoH.MouseDown
        plc.SetDevice("M222", 1)
        Dim input As Integer
        plc.GetDevice("D152", input)
    End Sub

    Private Sub btn_ServoH_MouseUp(sender As Object, e As MouseEventArgs) Handles btn_ServoH.MouseUp
        plc.SetDevice("M222", 0)
    End Sub

    Private Sub btn_ServoON_MouseDown(sender As Object, e As MouseEventArgs) Handles btn_ServoON.MouseDown
        plc.SetDevice("M221", 1)
        Dim input As Integer
        plc.GetDevice("D153", input)
    End Sub

    Private Sub btn_ServoON_MouseUp(sender As Object, e As MouseEventArgs) Handles btn_ServoON.MouseUp
        plc.SetDevice("M221", 0)
    End Sub

    Private Sub btn_JogD1_MouseDown(sender As Object, e As MouseEventArgs) Handles btn_JogD1.MouseDown

        plc.SetDevice("M225", 1)
        Dim input As Integer
        plc.GetDevice("D155", input)
    End Sub

    Private Sub btn_JogD1_MouseUp(sender As Object, e As MouseEventArgs) Handles btn_JogD1.MouseUp

        plc.SetDevice("M225", 0)
    End Sub

    Private Sub btn_jogD2_MouseDown(sender As Object, e As MouseEventArgs) Handles btn_jogD2.MouseDown
        plc.SetDevice("M226", 1)
        Dim input As Integer
        plc.GetDevice("D156", input)
    End Sub

    Private Sub btn_jogD2_MouseUp(sender As Object, e As MouseEventArgs) Handles btn_jogD2.MouseUp
        plc.SetDevice("M226", 0)
    End Sub

    Private Sub btn_jogD3_MouseDown(sender As Object, e As MouseEventArgs) Handles btn_jogD3.MouseDown
        plc.SetDevice("M227", 1)
        Dim input As Integer
        plc.GetDevice("D157", input)
    End Sub

    Private Sub btn_jogD3_MouseUp(sender As Object, e As MouseEventArgs) Handles btn_jogD3.MouseUp
        plc.SetDevice("M227", 0)
    End Sub

    Private Sub btn_jogD4_MouseDown(sender As Object, e As MouseEventArgs) Handles btn_jogD4.MouseDown
        plc.SetDevice("M228", 1)
        Dim input As Integer
        plc.GetDevice("D158", input)
    End Sub

    Private Sub btn_jogD4_MouseUp(sender As Object, e As MouseEventArgs) Handles btn_jogD4.MouseUp
        plc.SetDevice("M228", 0)
    End Sub

    Private Sub btn_jogDConti_MouseDown(sender As Object, e As MouseEventArgs) Handles btn_jogDConti.MouseDown
        plc.SetDevice("M229", 1)

        Dim input As Integer
        plc.GetDevice("D158", input)
    End Sub

    Private Sub btn_jogDConti_MouseUp(sender As Object, e As MouseEventArgs) Handles btn_jogDConti.MouseUp
        plc.SetDevice("M229", 0)
    End Sub

    Private Sub btn_ServoRST_Click(sender As Object, e As EventArgs) Handles btn_ServoRST.Click

    End Sub
End Class