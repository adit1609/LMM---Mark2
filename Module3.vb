Imports System.Diagnostics
Module Module3

    ' Specify the path to the executable file




    Public Sub DisplayData(data As String)
        Try
            ' Start the executable file
            Process.Start(data)
            Console.WriteLine("Application started successfully.")
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try


    End Sub
End Module
