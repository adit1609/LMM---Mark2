Imports ActUtlTypeLib

Public Class Alarms
    Dim plc As New ActUtlType
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' plc.SetDevice("M100", 1)
        'plc.SetDevice("D222", 0)
        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Alarms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        plc.ActLogicalStationNumber = 1
        plc.Open()
        Me.Location = New Point(688, 394)
        Me.BringToFront()
    End Sub

    Private Sub Alarms_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' plc.SetDevice("D222", 0)
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button2_MouseDown(sender As Object, e As MouseEventArgs) Handles Button2.MouseDown
        plc.SetDevice("M215", 1)

    End Sub

    Private Sub Button2_MouseUp(sender As Object, e As MouseEventArgs) Handles Button2.MouseUp
        plc.SetDevice("M215", 0)
        Me.Close()
    End Sub
End Class