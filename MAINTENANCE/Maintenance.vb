Imports Microsoft.VisualBasic.ApplicationServices

Public Class Maintenance
    Private currentChildform As Form
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
    Private Sub Maintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Home_Page(New Input_Output)

    End Sub

    Private Sub btIO_Click(sender As Object, e As EventArgs) Handles btIO.Click
        Home_Page(New Input_Output)


    End Sub

    Private Sub btServo_Click(sender As Object, e As EventArgs) Handles btServo.Click
        Home_Page(New Servo)
    End Sub

    Private Sub btAlarm_Click(sender As Object, e As EventArgs) Handles btAlarm.Click
        Home_Page(New alarmval)
    End Sub

    Private Sub btCommu_Click(sender As Object, e As EventArgs) Handles btCommu.Click
        Home_Page(New Communication)
    End Sub

    Private Sub btPos_Click(sender As Object, e As EventArgs) Handles btPos.Click
        Home_Page(New Position)
    End Sub

    Private Sub btCycle_Click(sender As Object, e As EventArgs) Handles btCycle.Click
        Home_Page(New Cycle)
    End Sub
End Class