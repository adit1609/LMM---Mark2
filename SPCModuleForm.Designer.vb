<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SPCModuleForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.DATAGRID = New System.Windows.Forms.DataGridView()
        Me.DATAGRIDBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.UseraccessDataSet = New Gui_Tset.useraccessDataSet()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DateTimePickerEndDate = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerStartDate = New System.Windows.Forms.DateTimePicker()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ChartTrend = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.BtnShowData = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DATAGRIDTableAdapter = New Gui_Tset.useraccessDataSetTableAdapters.DATAGRIDTableAdapter()
        Me.SNODataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PnameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CwidhtDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DatetimeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.XDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.YDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RPATHDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.DATAGRID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DATAGRIDBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UseraccessDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.ChartTrend, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(764, 1050)
        Me.Panel1.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.TabControl1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 100)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(764, 850)
        Me.Panel4.TabIndex = 2
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft YaHei UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.ItemSize = New System.Drawing.Size(100, 60)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.RightToLeftLayout = True
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(764, 850)
        Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.TabPage1.Controls.Add(Me.Panel6)
        Me.TabPage1.Controls.Add(Me.Panel5)
        Me.TabPage1.Font = New System.Drawing.Font("Microsoft Tai Le", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(756, 782)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "LIST VIEW"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.DATAGRID)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(3, 131)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(750, 648)
        Me.Panel6.TabIndex = 1
        '
        'DATAGRID
        '
        Me.DATAGRID.AutoGenerateColumns = False
        Me.DATAGRID.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders
        Me.DATAGRID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DATAGRID.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNODataGridViewTextBoxColumn, Me.PnameDataGridViewTextBoxColumn, Me.CwidhtDataGridViewTextBoxColumn, Me.DatetimeDataGridViewTextBoxColumn, Me.XDataGridViewTextBoxColumn, Me.YDataGridViewTextBoxColumn, Me.RPATHDataGridViewTextBoxColumn})
        Me.DATAGRID.DataSource = Me.DATAGRIDBindingSource
        Me.DATAGRID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DATAGRID.GridColor = System.Drawing.SystemColors.ActiveBorder
        Me.DATAGRID.Location = New System.Drawing.Point(0, 0)
        Me.DATAGRID.Name = "DATAGRID"
        Me.DATAGRID.ReadOnly = True
        Me.DATAGRID.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DATAGRID.Size = New System.Drawing.Size(750, 648)
        Me.DATAGRID.TabIndex = 0
        '
        'DATAGRIDBindingSource
        '
        Me.DATAGRIDBindingSource.DataMember = "DATAGRID"
        Me.DATAGRIDBindingSource.DataSource = Me.UseraccessDataSet
        '
        'UseraccessDataSet
        '
        Me.UseraccessDataSet.DataSetName = "useraccessDataSet"
        Me.UseraccessDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.DateTimePickerEndDate)
        Me.Panel5.Controls.Add(Me.DateTimePickerStartDate)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(3, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(750, 128)
        Me.Panel5.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 91)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 18)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Select Date To"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(2, 27)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(140, 18)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Select Date From"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DateTimePickerEndDate
        '
        Me.DateTimePickerEndDate.Font = New System.Drawing.Font("Microsoft YaHei UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerEndDate.Location = New System.Drawing.Point(160, 79)
        Me.DateTimePickerEndDate.Name = "DateTimePickerEndDate"
        Me.DateTimePickerEndDate.Size = New System.Drawing.Size(236, 34)
        Me.DateTimePickerEndDate.TabIndex = 1
        '
        'DateTimePickerStartDate
        '
        Me.DateTimePickerStartDate.Font = New System.Drawing.Font("Microsoft YaHei UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerStartDate.Location = New System.Drawing.Point(160, 19)
        Me.DateTimePickerStartDate.Name = "DateTimePickerStartDate"
        Me.DateTimePickerStartDate.Size = New System.Drawing.Size(236, 34)
        Me.DateTimePickerStartDate.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.LightGray
        Me.TabPage2.Controls.Add(Me.ChartTrend)
        Me.TabPage2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(712, 782)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TREND"
        '
        'ChartTrend
        '
        ChartArea1.Name = "ChartArea1"
        Me.ChartTrend.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.ChartTrend.Legends.Add(Legend1)
        Me.ChartTrend.Location = New System.Drawing.Point(33, 251)
        Me.ChartTrend.Name = "ChartTrend"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.ChartTrend.Series.Add(Series1)
        Me.ChartTrend.Size = New System.Drawing.Size(643, 300)
        Me.ChartTrend.TabIndex = 0
        Me.ChartTrend.Text = "Chart1"
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.TabPage3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage3.Location = New System.Drawing.Point(4, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(712, 782)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "BOARD VIEW"
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.TabPage4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage4.Location = New System.Drawing.Point(4, 4)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(712, 782)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "SETUP"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.BtnShowData)
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 950)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(764, 100)
        Me.Panel3.TabIndex = 1
        '
        'BtnShowData
        '
        Me.BtnShowData.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.BtnShowData.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnShowData.Location = New System.Drawing.Point(539, 10)
        Me.BtnShowData.Name = "BtnShowData"
        Me.BtnShowData.Size = New System.Drawing.Size(249, 75)
        Me.BtnShowData.TabIndex = 38
        Me.BtnShowData.Text = "SHOW DATA"
        Me.BtnShowData.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(12, 21)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(249, 67)
        Me.Button1.TabIndex = 39
        Me.Button1.Text = "Refresh"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(764, 100)
        Me.Panel2.TabIndex = 0
        '
        'DATAGRIDTableAdapter
        '
        Me.DATAGRIDTableAdapter.ClearBeforeFill = True
        '
        'SNODataGridViewTextBoxColumn
        '
        Me.SNODataGridViewTextBoxColumn.DataPropertyName = "S_NO"
        Me.SNODataGridViewTextBoxColumn.HeaderText = "S_NO"
        Me.SNODataGridViewTextBoxColumn.Name = "SNODataGridViewTextBoxColumn"
        Me.SNODataGridViewTextBoxColumn.ReadOnly = True
        '
        'PnameDataGridViewTextBoxColumn
        '
        Me.PnameDataGridViewTextBoxColumn.DataPropertyName = "pname"
        Me.PnameDataGridViewTextBoxColumn.HeaderText = "Program Name"
        Me.PnameDataGridViewTextBoxColumn.Name = "PnameDataGridViewTextBoxColumn"
        Me.PnameDataGridViewTextBoxColumn.ReadOnly = True
        '
        'CwidhtDataGridViewTextBoxColumn
        '
        Me.CwidhtDataGridViewTextBoxColumn.DataPropertyName = "cwidht"
        Me.CwidhtDataGridViewTextBoxColumn.HeaderText = "C - Width"
        Me.CwidhtDataGridViewTextBoxColumn.Name = "CwidhtDataGridViewTextBoxColumn"
        Me.CwidhtDataGridViewTextBoxColumn.ReadOnly = True
        '
        'DatetimeDataGridViewTextBoxColumn
        '
        Me.DatetimeDataGridViewTextBoxColumn.DataPropertyName = "date_time"
        Me.DatetimeDataGridViewTextBoxColumn.HeaderText = "Date_Time"
        Me.DatetimeDataGridViewTextBoxColumn.Name = "DatetimeDataGridViewTextBoxColumn"
        Me.DatetimeDataGridViewTextBoxColumn.ReadOnly = True
        '
        'XDataGridViewTextBoxColumn
        '
        Me.XDataGridViewTextBoxColumn.DataPropertyName = "x"
        Me.XDataGridViewTextBoxColumn.HeaderText = "X_Pos."
        Me.XDataGridViewTextBoxColumn.Name = "XDataGridViewTextBoxColumn"
        Me.XDataGridViewTextBoxColumn.ReadOnly = True
        '
        'YDataGridViewTextBoxColumn
        '
        Me.YDataGridViewTextBoxColumn.DataPropertyName = "y"
        Me.YDataGridViewTextBoxColumn.HeaderText = "Y_Pos."
        Me.YDataGridViewTextBoxColumn.Name = "YDataGridViewTextBoxColumn"
        Me.YDataGridViewTextBoxColumn.ReadOnly = True
        '
        'RPATHDataGridViewTextBoxColumn
        '
        Me.RPATHDataGridViewTextBoxColumn.DataPropertyName = "RPATH"
        Me.RPATHDataGridViewTextBoxColumn.HeaderText = "R_PATH"
        Me.RPATHDataGridViewTextBoxColumn.Name = "RPATHDataGridViewTextBoxColumn"
        Me.RPATHDataGridViewTextBoxColumn.ReadOnly = True
        '
        'SPCModuleForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(764, 1050)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SPCModuleForm"
        Me.Text = "SPCModuleForm"
        Me.Panel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        CType(Me.DATAGRID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DATAGRIDBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UseraccessDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.ChartTrend, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents Panel3 As Windows.Forms.Panel
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents Panel4 As Windows.Forms.Panel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Panel6 As Windows.Forms.Panel
    Friend WithEvents Panel5 As Windows.Forms.Panel
    Friend WithEvents DateTimePickerEndDate As DateTimePicker
    Friend WithEvents DateTimePickerStartDate As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents BtnShowData As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents ChartTrend As DataVisualization.Charting.Chart
    Friend WithEvents DATAGRID As DataGridView
    Friend WithEvents UseraccessDataSet As useraccessDataSet
    Friend WithEvents DATAGRIDBindingSource As BindingSource
    Friend WithEvents DATAGRIDTableAdapter As useraccessDataSetTableAdapters.DATAGRIDTableAdapter
    Friend WithEvents SNODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PnameDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents CwidhtDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents DatetimeDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents XDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents YDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents RPATHDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
End Class
