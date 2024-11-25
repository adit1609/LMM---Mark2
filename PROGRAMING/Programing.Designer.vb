<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Programing
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
        Me.pnProg = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pnProg)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(772, 749)
        Me.Panel1.TabIndex = 1
        '
        'pnProg
        '
        Me.pnProg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnProg.Location = New System.Drawing.Point(0, 0)
        Me.pnProg.Name = "pnProg"
        Me.pnProg.Size = New System.Drawing.Size(772, 749)
        Me.pnProg.TabIndex = 8
        '
        'Programing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(772, 749)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Programing"
        Me.Text = "Programing"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel29 As Panel
    Friend WithEvents pnProg As Windows.Forms.Panel
    Friend WithEvents Panel1 As Windows.Forms.Panel
End Class
