<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Maintenance
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btCycle = New System.Windows.Forms.Button()
        Me.btPos = New System.Windows.Forms.Button()
        Me.btCommu = New System.Windows.Forms.Button()
        Me.btAlarm = New System.Windows.Forms.Button()
        Me.btServo = New System.Windows.Forms.Button()
        Me.btIO = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(772, 749)
        Me.Panel1.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(772, 688)
        Me.Panel3.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.IndianRed
        Me.Panel2.Controls.Add(Me.btCycle)
        Me.Panel2.Controls.Add(Me.btPos)
        Me.Panel2.Controls.Add(Me.btCommu)
        Me.Panel2.Controls.Add(Me.btAlarm)
        Me.Panel2.Controls.Add(Me.btServo)
        Me.Panel2.Controls.Add(Me.btIO)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 688)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(772, 61)
        Me.Panel2.TabIndex = 1
        '
        'btCycle
        '
        Me.btCycle.Dock = System.Windows.Forms.DockStyle.Left
        Me.btCycle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btCycle.Location = New System.Drawing.Point(640, 0)
        Me.btCycle.Margin = New System.Windows.Forms.Padding(2)
        Me.btCycle.Name = "btCycle"
        Me.btCycle.Size = New System.Drawing.Size(130, 61)
        Me.btCycle.TabIndex = 8
        Me.btCycle.Text = "CYCLE OPS"
        Me.btCycle.UseVisualStyleBackColor = True
        '
        'btPos
        '
        Me.btPos.Dock = System.Windows.Forms.DockStyle.Left
        Me.btPos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btPos.Location = New System.Drawing.Point(512, 0)
        Me.btPos.Margin = New System.Windows.Forms.Padding(2)
        Me.btPos.Name = "btPos"
        Me.btPos.Size = New System.Drawing.Size(128, 61)
        Me.btPos.TabIndex = 7
        Me.btPos.Text = "POSITION "
        Me.btPos.UseVisualStyleBackColor = True
        '
        'btCommu
        '
        Me.btCommu.Dock = System.Windows.Forms.DockStyle.Left
        Me.btCommu.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btCommu.Location = New System.Drawing.Point(384, 0)
        Me.btCommu.Margin = New System.Windows.Forms.Padding(2)
        Me.btCommu.Name = "btCommu"
        Me.btCommu.Size = New System.Drawing.Size(128, 61)
        Me.btCommu.TabIndex = 6
        Me.btCommu.Text = "COMMUNICATION"
        Me.btCommu.UseVisualStyleBackColor = True
        '
        'btAlarm
        '
        Me.btAlarm.Dock = System.Windows.Forms.DockStyle.Left
        Me.btAlarm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btAlarm.Location = New System.Drawing.Point(256, 0)
        Me.btAlarm.Margin = New System.Windows.Forms.Padding(2)
        Me.btAlarm.Name = "btAlarm"
        Me.btAlarm.Size = New System.Drawing.Size(128, 61)
        Me.btAlarm.TabIndex = 5
        Me.btAlarm.Text = "ALARM"
        Me.btAlarm.UseVisualStyleBackColor = True
        '
        'btServo
        '
        Me.btServo.Dock = System.Windows.Forms.DockStyle.Left
        Me.btServo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btServo.Location = New System.Drawing.Point(128, 0)
        Me.btServo.Margin = New System.Windows.Forms.Padding(2)
        Me.btServo.Name = "btServo"
        Me.btServo.Size = New System.Drawing.Size(128, 61)
        Me.btServo.TabIndex = 4
        Me.btServo.Text = "SERVO"
        Me.btServo.UseVisualStyleBackColor = True
        '
        'btIO
        '
        Me.btIO.Dock = System.Windows.Forms.DockStyle.Left
        Me.btIO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btIO.Location = New System.Drawing.Point(0, 0)
        Me.btIO.Margin = New System.Windows.Forms.Padding(2)
        Me.btIO.Name = "btIO"
        Me.btIO.Size = New System.Drawing.Size(128, 61)
        Me.btIO.TabIndex = 3
        Me.btIO.Text = "I/O"
        Me.btIO.UseVisualStyleBackColor = True
        '
        'Maintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(772, 749)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Maintenance"
        Me.Text = "Maintenance"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents Panel3 As Windows.Forms.Panel
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents btCycle As Button
    Friend WithEvents btPos As Button
    Friend WithEvents btCommu As Button
    Friend WithEvents btAlarm As Button
    Friend WithEvents btServo As Button
    Friend WithEvents btIO As Button
End Class
