Public Class Programing
    Private currentChildform As Form
    Private Sub Home_Page(_childfrm As Form)
        If currentChildform IsNot Nothing Then
            currentChildform.Close()
        End If
        currentChildform = _childfrm

        _childfrm.TopLevel = False
        _childfrm.FormBorderStyle = FormBorderStyle.None
        _childfrm.Dock = DockStyle.Fill
        pnProg.Controls.Add(_childfrm)
        _childfrm.BringToFront()
        _childfrm.Show()


    End Sub
    Private Sub Programing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Home_Page(New Recipe)

    End Sub

    Private Sub btPan_Click(sender As Object, e As EventArgs)
        Home_Page(New Panel)

    End Sub

    Private Sub btFidu_Click(sender As Object, e As EventArgs)
        Home_Page(New Fiducial)

    End Sub

    Private Sub btLock_Click(sender As Object, e As EventArgs)
        Home_Page(New Location)

    End Sub

    Private Sub btRecipe_Click(sender As Object, e As EventArgs)
        Home_Page(New Recipe)

    End Sub
End Class