Imports ActUtlTypeLib

Module ModuleRec
    Dim plc As New ActUtlType
    Public Sub HandleButtonClick(isCellClicked As Boolean, TXM As TextBox, TYM As TextBox, TCM As TextBox, plc As ActUtlType)
        If Not isCellClicked Then
            MessageBox.Show("Please select a recipe first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim floatValueX As Single
        If Single.TryParse(TXM.Text, floatValueX) Then
            ' Convert the float value to two 16-bit integers
            Dim words() As Integer = ConvertFloatToWord(floatValueX)

            ' Write the integers to the PLC registers
            plc.SetDevice("D370", words(0))
            plc.SetDevice("D371", words(1))
        Else
            ' If parsing fails, you can handle the invalid input here
        End If

        Dim floatValueY As Single
        If Single.TryParse(TYM.Text, floatValueY) Then
            ' Convert the float value to two 16-bit integers
            Dim words() As Integer = ConvertFloatToWord(floatValueY)

            ' Write the integers to the PLC registers
            plc.SetDevice("D372", words(0))
            plc.SetDevice("D373", words(1))
        Else
            ' If parsing fails, you can handle the invalid input here
        End If

        Dim floatValueCW As Single
        If Single.TryParse(TCM.Text, floatValueCW) Then
            ' Convert the float value to two 16-bit integers
            Dim words() As Integer = ConvertFloatToWord(floatValueCW)

            ' Write the integers to the PLC registers
            plc.SetDevice("D312", words(0))
            plc.SetDevice("D313", words(1))
        Else
            ' If parsing fails, you can handle the invalid input here
        End If
    End Sub

    ' Function to convert a single-precision float to two 16-bit integers
    Public Function ConvertFloatToWord(ByVal value As Single) As Integer()
        Dim floatBytes As Byte() = BitConverter.GetBytes(value)
        Dim lowWord As Integer = BitConverter.ToInt16(floatBytes, 0)
        Dim highWord As Integer = BitConverter.ToInt16(floatBytes, 2)
        Return {lowWord, highWord}
    End Function
End Module
