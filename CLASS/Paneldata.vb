Module DataModule
    ' Variables to store data from each click
    Public var1 As Int16
    Public var2 As Double
    Public var3 As Double
    Public var4 As Double
    Public var5 As Double
    Public var6 As Double
    Public var7 As Double

    ' Dictionary to store dataArray
    Public dataArray As New Dictionary(Of String, Dictionary(Of String, Object))()

    ' Current row index for displaying values until the last row
    Public currentRowIndex As Integer = 0

    ' Function to initialize module variables with data from the first row
    Public Sub InitializeModuleVariables()
        ' Ensure dataArray is not null and has entries
        If dataArray IsNot Nothing AndAlso dataArray.Count > 0 Then
            ' Get the first row's data
            Dim firstRowName As String = "Row1" ' Adjust the row name if needed
            If dataArray.ContainsKey(firstRowName) Then
                Dim columnData As Dictionary(Of String, Object) = dataArray(firstRowName)

                ' Initialize module variables with data from the first row
                var1 = ConvertWordToFloat({columnData("cb1")})
                var2 = ConvertWordToFloat({columnData("cb2")})
                var3 = ConvertWordToFloat({columnData("cb3")})
                var4 = ConvertWordToFloat({columnData("cb5")})

                ' Handle checkbox (boolean) value in the fifth column
                var5 = If(TypeOf columnData("cb4") Is Boolean, If(CBool(columnData("cb4")), 1.0, 0.0), ConvertWordToFloat({columnData("cb4")}))

                var6 = ConvertWordToFloat({columnData("cb6")})
                var7 = ConvertWordToFloat({columnData("cb7")})
            Else
                Throw New Exception("First row not found in dataArray.")
            End If
        Else
            Throw New Exception("No data available in dataArray.")
        End If
    End Sub

    ' Convert float to word (16-bit integer)
    Public Function ConvertFloatToWord(ByVal value As Single) As Integer()
        Dim floatBytes As Byte() = BitConverter.GetBytes(value)
        Dim lowWord As Integer = BitConverter.ToInt16(floatBytes, 0)
        Dim highWord As Integer = BitConverter.ToInt16(floatBytes, 2)
        Return {lowWord, highWord}
    End Function

    ' Convert word (16-bit integer) to float
    Public Function ConvertWordToFloat(ByVal register As Integer()) As Single
        Dim bytes(3) As Byte
        Dim lowWordBytes() As Byte = BitConverter.GetBytes(register(0))
        Dim highWordBytes() As Byte = BitConverter.GetBytes(register(1))

        Array.Copy(lowWordBytes, 0, bytes, 0, 2)
        Array.Copy(highWordBytes, 0, bytes, 2, 2)

        Return BitConverter.ToSingle(bytes, 0)
    End Function
End Module
