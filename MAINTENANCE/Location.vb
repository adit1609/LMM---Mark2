Imports ActUtlTypeLib
Public Class Location
    Dim plc As ActUtlType
    Private Sub btlocw_MouseUp(sender As Object, e As MouseEventArgs) Handles btlocw.MouseUp
        plc.SetDevice("M200", 0)
    End Sub

    Private Sub btlocw_MouseDown(sender As Object, e As MouseEventArgs) Handles btlocw.MouseDown
        plc.SetDevice("M200", 1)
    End Sub

    Private Sub btlocN_MouseUp(sender As Object, e As MouseEventArgs) Handles btlocN.MouseUp
        plc.SetDevice("M201", 0)
    End Sub

    Private Sub btlocN_MouseDown(sender As Object, e As MouseEventArgs) Handles btlocN.MouseDown
        plc.SetDevice("M201", 1)
    End Sub

    Private Sub btlocw_Click(sender As Object, e As EventArgs) Handles btlocw.Click

    End Sub

    Private Sub btmax_MouseUp(sender As Object, e As MouseEventArgs) Handles btmax.MouseUp
        plc.SetDevice("M206", 0)
    End Sub

    Private Sub btmax_MouseDown(sender As Object, e As MouseEventArgs) Handles btmax.MouseDown
        plc.SetDevice("M206", 1)
    End Sub

    Private Sub btmin_MouseUp(sender As Object, e As MouseEventArgs) Handles btmin.MouseUp
        plc.SetDevice("M205", 0)
    End Sub

    Private Sub btmin_MouseDown(sender As Object, e As MouseEventArgs) Handles btmin.MouseDown
        plc.SetDevice("M205", 1)
    End Sub

    Private Sub Location_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class