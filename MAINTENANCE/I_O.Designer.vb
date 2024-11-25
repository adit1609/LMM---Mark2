<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class I_O
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel16)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(772, 749)
        Me.Panel1.TabIndex = 1
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.Button10)
        Me.Panel16.Controls.Add(Me.Button8)
        Me.Panel16.Controls.Add(Me.Button7)
        Me.Panel16.Controls.Add(Me.Button6)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(0, 924)
        Me.Panel16.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(772, 57)
        Me.Panel16.TabIndex = 13
        '
        'Button10
        '
        Me.Button10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button10.Location = New System.Drawing.Point(428, 0)
        Me.Button10.Margin = New System.Windows.Forms.Padding(2)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(86, 57)
        Me.Button10.TabIndex = 28
        Me.Button10.Text = "ON"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.Location = New System.Drawing.Point(514, 0)
        Me.Button8.Margin = New System.Windows.Forms.Padding(2)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(86, 57)
        Me.Button8.TabIndex = 27
        Me.Button8.Text = "SAVE"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.BackgroundImage = Global.Gui_Tset.My.Resources.Resources.icons8_chevron_up_50
        Me.Button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button7.Location = New System.Drawing.Point(600, 0)
        Me.Button7.Margin = New System.Windows.Forms.Padding(2)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(86, 57)
        Me.Button7.TabIndex = 26
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.BackgroundImage = Global.Gui_Tset.My.Resources.Resources.icons8_chevron_down_50
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button6.Location = New System.Drawing.Point(686, 0)
        Me.Button6.Margin = New System.Windows.Forms.Padding(2)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(86, 57)
        Me.Button6.TabIndex = 25
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.DataGridView1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 51)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(772, 873)
        Me.Panel3.TabIndex = 5
        '
        'DataGridView1
        '
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.OP})
        Me.DataGridView1.Cursor = System.Windows.Forms.Cursors.Default
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 62
        Me.DataGridView1.Size = New System.Drawing.Size(772, 873)
        Me.DataGridView1.TabIndex = 1
        '
        'Column1
        '
        Me.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column1.HeaderText = "INPUT"
        Me.Column1.MinimumWidth = 8
        Me.Column1.Name = "Column1"
        '
        'OP
        '
        Me.OP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OP.DefaultCellStyle = DataGridViewCellStyle3
        Me.OP.HeaderText = "OUTPUT"
        Me.OP.MinimumWidth = 8
        Me.OP.Name = "OP"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(772, 51)
        Me.Panel2.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(308, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(161, 31)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "INPUT - OUTPUT"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'I_O
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(772, 749)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "I_O"
        Me.Text = "I_O"
        Me.Panel1.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents Panel16 As Windows.Forms.Panel
    Friend WithEvents Button10 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Panel3 As Windows.Forms.Panel
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents OP As DataGridViewTextBoxColumn
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents Label1 As Label
End Class
