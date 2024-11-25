Public Class Position
    Private currentChildform As Form
    Private Sub Home_Page(_childfrm As Form)
        If currentChildform IsNot Nothing Then
            currentChildform.Close()
        End If
        currentChildform = _childfrm

        _childfrm.TopLevel = False
        _childfrm.FormBorderStyle = FormBorderStyle.None
        _childfrm.Dock = DockStyle.Fill
        Panel13.Controls.Add(_childfrm)
        _childfrm.BringToFront()
        _childfrm.Show()


    End Sub
    Private Sub Position_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Home_Page(New Xm)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Home_Page(New Xm)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Home_Page(New Ym)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Home_Page(New Xm)
    End Sub
End Class