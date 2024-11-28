Imports Gui_Tset.RecepieOperation
Imports OfficeOpenXml
Imports System.Configuration
Imports System.IO

Public Class alarmval
    Private Sub alarmval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadExcelData()
    End Sub
    Private Sub LoadExcelData()
        Dim folderPath As String = ConfigurationManager.AppSettings("Warn")
        Dim fileName As String = Path.Combine(folderPath, DateTime.Now.ToString("yyyy-MM-dd") & ".xlsx")
        Dim fileInfo As New FileInfo(fileName)
        If Not fileInfo.Exists Then
            MessageBox.Show("No data available for today.")
            Return
        End If
        Using package As New ExcelPackage(fileInfo)
            Dim worksheet As ExcelWorksheet = package.Workbook.Worksheets(1)
            Dim startRow As Integer = 2
            For row As Integer = startRow To worksheet.Dimension.End.Row
                Dim sNo As String = worksheet.Cells(row, 1).Text
                Dim alarmName As String = worksheet.Cells(row, 2).Text
                Dim alarmCode As String = worksheet.Cells(row, 3).Text
                Dim time As String = worksheet.Cells(row, 4).Text
                Dim recipe As String = worksheet.Cells(row, 5).Text
                Dim alarmNameCode As String = $"{alarmName} - {alarmCode}"
                Guna2DataGridView1.Rows.Add(sNo, alarmNameCode, time, recipe)
            Next
        End Using
    End Sub

    Private Sub alarmval_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Home_Page.FidCam1.CloseDevice()
        Home_Page.FidCam1.DestroyDevice()
        Home_Page.LiveCamera1.CloseDevice()
        Home_Page.LiveCamera1.DestroyDevice()

        Close_Exe.Main()
    End Sub

    Private Sub alarmval_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Home_Page.FidCam1.CloseDevice()
        Home_Page.FidCam1.DestroyDevice()
        Home_Page.LiveCamera1.CloseDevice()
        Home_Page.LiveCamera1.DestroyDevice()

        Close_Exe.Main()
    End Sub
End Class