Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Threading.Tasks

Public Class SPCModuleForm

    Private Const ConnectionString As String = "Data Source=(localdb)\LLMdb;Initial Catalog=useraccess;Integrated Security=True"

    Private Async Sub BtnShowData_Click(sender As Object, e As EventArgs)
        Dim startDate As DateTime = DateTimePickerStartDate.Value
        Dim endDate As DateTime = DateTimePickerEndDate.Value

        Await LoadListViewDataAsync(startDate, endDate)
        Await LoadTrendDataAsync(startDate, endDate)
    End Sub

    Private Async Function LoadListViewDataAsync(startDate As DateTime, endDate As DateTime) As Task
        Dim query As String = "SELECT ROW_NUMBER() OVER (ORDER BY MarkingCompletionDate) AS [S. No.], " &
                              "MarkingCompletionDate AS [Date of marking Completion], " &
                              "InspectionCompletionTime AS [Time of Inspection Completion], " &
                              "ReceipeName AS [Receipe Name], " &
                              "PCBAName AS [PCBA Name], " &
                              "UserName AS [User], " &
                              "Side, " &
                              "ScanSpeed AS [Scan Speed of Laser], " &
                              "LaserHeadPower AS [Laser Head Power], " &
                              "ScanCompleteTime AS [Scan Complete Time], " &
                              "MarkingCode AS [Marking Code], " &
                              "BarcodeReadScanner AS [Barcode Read by Scanner], " &
                              "BarcodeReadCamera AS [Barcode Read by Camera], " &
                              "Status " &
                              "FROM PCBData " &
                              "WHERE MarkingCompletionDate BETWEEN @StartDate AND @EndDate " &
                              "ORDER BY MarkingCompletionDate"

        Await ExecuteQueryAndFillDataGridViewAsync(query, startDate, endDate, DataGridViewListView)
    End Function

    Private Async Function LoadTrendDataAsync(startDate As DateTime, endDate As DateTime) As Task
        ' Clear existing series
        ChartTrend.Series.Clear()

        ' Define series for different trend types
        Dim seriesDayWise As New Series("Day Wise") With {.ChartType = SeriesChartType.Line, .Color = Color.Blue}
        Dim seriesShiftWise As New Series("Shift Wise") With {.ChartType = SeriesChartType.Column, .Color = Color.Green}
        Dim seriesLineWise As New Series("Line Wise") With {.ChartType = SeriesChartType.Pie, .Color = Color.Red}

        ' Populate Day Wise Data
        Dim dayWiseQuery As String = "SELECT CONVERT(DATE, MarkingCompletionDate) AS [Day], " &
                                      "SUM(CASE WHEN Status = 'Good' THEN 1 ELSE 0 END) AS GoodCount, " &
                                      "SUM(CASE WHEN Status = 'NG' THEN 1 ELSE 0 END) AS NGCount, " &
                                      "SUM(CASE WHEN Status = 'Pass' THEN 1 ELSE 0 END) AS PassCount " &
                                      "FROM PCBData " &
                                      "WHERE MarkingCompletionDate BETWEEN @StartDate AND @EndDate " &
                                      "GROUP BY CONVERT(DATE, MarkingCompletionDate) " &
                                      "ORDER BY [Day]"

        Await ExecuteQueryAndPopulateChartAsync(dayWiseQuery, startDate, endDate, seriesDayWise, "Day")

        ' Populate Shift Wise Data
        Dim shiftWiseQuery As String = "SELECT Shift, " &
                                        "SUM(CASE WHEN Status = 'Good' THEN 1 ELSE 0 END) AS GoodCount, " &
                                        "SUM(CASE WHEN Status = 'NG' THEN 1 ELSE 0 END) AS NGCount, " &
                                        "SUM(CASE WHEN Status = 'Pass' THEN 1 ELSE 0 END) AS PassCount " &
                                        "FROM PCBData " &
                                        "WHERE MarkingCompletionDate BETWEEN @StartDate AND @EndDate " &
                                        "GROUP BY Shift " &
                                        "ORDER BY Shift"

        Await ExecuteQueryAndPopulateChartAsync(shiftWiseQuery, startDate, endDate, seriesShiftWise, "Shift")

        ' Populate Line Wise Data
        Dim lineWiseQuery As String = "SELECT Line, " &
                                       "SUM(CASE WHEN Status = 'Good' THEN 1 ELSE 0 END) AS GoodCount, " &
                                       "SUM(CASE WHEN Status = 'NG' THEN 1 ELSE 0 END) AS NGCount, " &
                                       "SUM(CASE WHEN Status = 'Pass' THEN 1 ELSE 0 END) AS PassCount " &
                                       "FROM PCBData " &
                                       "WHERE MarkingCompletionDate BETWEEN @StartDate AND @EndDate " &
                                       "GROUP BY Line " &
                                       "ORDER BY Line"

        Await ExecuteQueryAndPopulateChartAsync(lineWiseQuery, startDate, endDate, seriesLineWise, "Line")

        ' Add series to chart
        ChartTrend.Series.Add(seriesDayWise)
        ChartTrend.Series.Add(seriesShiftWise)
        ChartTrend.Series.Add(seriesLineWise)

        ' Configure chart appearance
        ChartTrend.Titles.Clear()
        ChartTrend.Titles.Add("Trend Analysis")
        ChartTrend.ChartAreas(0).AxisX.Title = "Time/Shift/Line"
        ChartTrend.ChartAreas(0).AxisY.Title = "Count"
    End Function

    Private Async Function ExecuteQueryAndFillDataGridViewAsync(query As String, startDate As DateTime, endDate As DateTime, dgv As DataGridView) As Task
        Using conn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@StartDate", startDate)
                cmd.Parameters.AddWithValue("@EndDate", endDate)
                conn.Open()

                Dim adapter As New SqlDataAdapter(cmd)
                Dim dataTable As New DataTable()
                Await Task.Run(Sub() adapter.Fill(dataTable))

                dgv.DataSource = dataTable
            End Using
        End Using
    End Function

    Private Async Function ExecuteQueryAndPopulateChartAsync(query As String, startDate As DateTime, endDate As DateTime, series As Series, xValueField As String) As Task
        Using conn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@StartDate", startDate)
                cmd.Parameters.AddWithValue("@EndDate", endDate)
                conn.Open()

                Dim reader As SqlDataReader = Await cmd.ExecuteReaderAsync()
                While Await reader.ReadAsync()
                    Dim xValue As Object = reader(xValueField)
                    Dim goodCount As Integer = reader("GoodCount")
                    Dim ngCount As Integer = reader("NGCount")
                    Dim passCount As Integer = reader("PassCount")

                    ' Add data points to series
                    series.Points.AddXY(xValue, goodCount)
                    series.Points.AddXY(xValue, ngCount)
                    series.Points.AddXY(xValue, passCount)
                End While
                reader.Close()
            End Using
        End Using
    End Function

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub Panel2_Paint_1(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub SPCModuleForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'UseraccessDataSet.DATAGRID' table. You can move, or remove it, as needed.
        Me.DATAGRIDTableAdapter.Fill(Me.UseraccessDataSet.DATAGRID)

    End Sub

    Private Sub BtnShowData_Click_1(sender As Object, e As EventArgs) Handles BtnShowData.Click
        Dim startDate As DateTime = DateTimePickerStartDate.Value
        Dim endDate As DateTime = DateTimePickerEndDate.Value


        Dim connectionString As String = "Data Source=(localdb)\LLMdb;Initial Catalog=useraccess;Integrated Security=True;Encrypt=True"

        ' SQL query to fetch data between selected dates
        Dim query As String = "SELECT * FROM DATAGRID WHERE date_time BETWEEN @StartDate AND @EndDate"


        Using connection As New SqlConnection(connectionString)

            Using command As New SqlCommand(query, connection)

                command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate
                command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate


                connection.Open()


                Dim dataTable As New DataTable()
                Dim adapter As New SqlDataAdapter(command)
                adapter.Fill(dataTable)


                DataGrid.DataSource = dataTable
            End Using
        End Using
    End Sub
End Class
