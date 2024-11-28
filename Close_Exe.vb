Imports System.Diagnostics
Module Close_Exe
    Sub Main()
        Try
            ' Specify the process name without the .exe extension
            Dim processName As String = "WinFormsApp5" ' Replace with your process name

            ' Get all processes by the specified name
            Dim processes As Process() = Process.GetProcessesByName(processName)

            ' Close each process
            For Each proc As Process In processes
                proc.CloseMainWindow() ' Gracefully close the application
                proc.WaitForExit(3000) ' Wait for the process to exit

                If Not proc.HasExited Then
                    proc.Kill() ' Force close if it doesn't exit
                End If
            Next

            Console.WriteLine("Processes closed successfully.")
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try

    End Sub
End Module
