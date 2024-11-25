Public Class User
    Private currentChildform As Form
    Private Sub Home_Page(_childfrm As Form)
        If currentChildform IsNot Nothing Then
            currentChildform.Close()
        End If
        currentChildform = _childfrm

        _childfrm.TopLevel = False
        _childfrm.FormBorderStyle = FormBorderStyle.None
        _childfrm.Dock = DockStyle.Fill
        pnUser.Controls.Add(_childfrm)
        _childfrm.BringToFront()
        _childfrm.Show()


    End Sub
    Private Sub User_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Home_Page(New Log)
        btReturnt.Hide()
        btcreate.Dock=Dock
    End Sub

    Private Sub btReturnt_Click(sender As Object, e As EventArgs) Handles btReturnt.Click
        Home_Page(New Log)
    End Sub

    Private Sub btcreate_Click(sender As Object, e As EventArgs) Handles btcreate.Click
        Home_Page(New New_user)
        btReturnt.Show()
    End Sub
End Class