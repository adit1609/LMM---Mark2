Imports ActUtlTypeLib
Public Class Panel
    Dim plc As New ActUtlType
    Dim check As Integer
    Private xpath As String
    Public WriteOnly Property Setb As String
        Set(value As String)
            xpath = value
        End Set
    End Property
    Public Shared Widening Operator CType(v As Windows.Forms.Panel) As Panel
        Throw New NotImplementedException()
    End Operator

    Private Sub btpanWide_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M0", 0)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btpanWide_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M0", 1)
    End Sub

    Private Sub btpanN_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M201", 1)
    End Sub

    Private Sub btpanN_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M201", 0)
    End Sub

    Private Sub btpanWide_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btxp_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btxmin_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M206", 0)
    End Sub

    Private Sub btxmin_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M206", 1)
    End Sub

    Private Sub btxmax_MouseUp(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M205", 0)
    End Sub

    Private Sub btxmax_MouseDown(sender As Object, e As MouseEventArgs)
        plc.SetDevice("M205", 1)
    End Sub

    Private Sub Panel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        plc.ActLogicalStationNumber = 1
        check = plc.Open()

    End Sub
End Class