Imports ActUtlTypeLib

Public Class Communication
    Dim plc As New ActUtlType
    Private Sub Panel31_Paint(sender As Object, e As PaintEventArgs) Handles Panel31.Paint

    End Sub

    Private Sub Panel7_Paint(sender As Object, e As PaintEventArgs) Handles Panel7.Paint

    End Sub

    Private Sub Rbtn_hmi_Set_CheckedChanged(sender As Object, e As EventArgs) Handles Rbtn_hmi_Set.CheckedChanged

    End Sub
    Private Sub Rbtn_vision_Set_MouseDown(sender As Object, e As MouseEventArgs) Handles Rbtn_vision_Set.MouseDown
        plc.SetDevice("M207", 1)
    End Sub

    Private Sub Rbtn_vision_Rst_MouseUp(sender As Object, e As MouseEventArgs) Handles Rbtn_vision_Rst.MouseUp
        plc.SetDevice("M207", 0)
    End Sub

    Private Sub Rbtn_barcod_Set_MouseDown(sender As Object, e As MouseEventArgs) Handles Rbtn_barcod_Set.MouseDown
        plc.SetDevice("M231", 1)
    End Sub

    Private Sub Rbtn_barcod_Rst_MouseUp(sender As Object, e As MouseEventArgs) Handles Rbtn_barcod_Rst.MouseUp
        plc.SetDevice("M208", 0)
    End Sub

    Private Sub Rbtn_lm_Set_MouseDown(sender As Object, e As MouseEventArgs) Handles Rbtn_lm_Set.MouseDown
        plc.SetDevice("M209", 1)
    End Sub

    Private Sub Rbtn_lm_Rst_MouseUp(sender As Object, e As MouseEventArgs) Handles Rbtn_lm_Rst.MouseUp
        plc.SetDevice("M209", 0)
    End Sub

    Private Sub Rbtn_servo_Set_MouseDown(sender As Object, e As MouseEventArgs) Handles Rbtn_servo_Set.MouseDown
        plc.SetDevice("M210", 1)
    End Sub

    Private Sub Rbtn_servo_Rset_MouseUp(sender As Object, e As MouseEventArgs) Handles Rbtn_servo_Rset.MouseUp
        plc.SetDevice("M210", 0)
    End Sub

    Private Sub Rbtn_hmi_Set_MouseDown(sender As Object, e As MouseEventArgs) Handles Rbtn_hmi_Set.MouseDown
        plc.SetDevice("M211", 1)
    End Sub

    Private Sub Rbtn_hmi_Rst_MouseUp(sender As Object, e As MouseEventArgs) Handles Rbtn_hmi_Rst.MouseUp
        plc.SetDevice("M211", 0)
    End Sub

    Private Sub Communication_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        plc.ActLogicalStationNumber = 1
        plc.Open()
    End Sub

    Private Sub Rbtn_barcod_Set_MouseUp(sender As Object, e As MouseEventArgs) Handles Rbtn_barcod_Set.MouseUp
        plc.SetDevice("M231", 0)
    End Sub
    Private Async Function barcode_scannerAsync() As Task
        Dim startRegister As Integer = 280
        Dim endRegister As Integer = 299
        Dim numRegisters As Integer = endRegister - startRegister + 1


        Dim words(numRegisters - 1) As Integer


        For i As Integer = 0 To numRegisters - 1
            plc.GetDevice("D" & (startRegister + i).ToString(), words(i))
        Next

        ' Create a byte array to hold the combined byte values
        Dim bytes(numRegisters * 2 - 1) As Byte

        ' Convert the 16-bit integers to a byte array
        For i As Integer = 0 To words.Length - 1
            Dim wordBytes() As Byte = BitConverter.GetBytes(words(i))
            bytes(i * 2) = wordBytes(0)
            bytes(i * 2 + 1) = wordBytes(1)
        Next

        ' Convert the byte array to a string
        Dim strValue As String = System.Text.Encoding.ASCII.GetString(bytes)

        ' Display the string in the RichTextBox
        RichTextBox3.Text = strValue.TrimEnd(Chr(0)) ' Remove any trailing null characters

    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        barcode_scannerAsync()
    End Sub

    Private Sub Rbtn_barcod_Set_CheckedChanged(sender As Object, e As EventArgs) Handles Rbtn_barcod_Set.CheckedChanged
        Timer1.Start()
    End Sub

    Dim PASSMODE As Integer = 0

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        PASSMODE = PASSMODE + 1
        If PASSMODE = 1 Then
            plc.SetDevice("M244", 1)
            Button4.BackColor = Color.Green

        Else
            PASSMODE = 0
            plc.SetDevice("M244", 0)
            Button4.BackColor = Color.White
        End If
    End Sub
End Class