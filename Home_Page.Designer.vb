<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Home_Page
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Home_Page))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnDock = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btProg = New System.Windows.Forms.Button()
        Me.btoper = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btReturn = New System.Windows.Forms.Button()
        Me.btsetup = New System.Windows.Forms.Button()
        Me.btmain = New System.Windows.Forms.Button()
        Me.btUser = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pnDock)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(772, 749)
        Me.Panel1.TabIndex = 1
        '
        'pnDock
        '
        Me.pnDock.BackColor = System.Drawing.Color.IndianRed
        Me.pnDock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnDock.Location = New System.Drawing.Point(0, 90)
        Me.pnDock.Name = "pnDock"
        Me.pnDock.Size = New System.Drawing.Size(772, 598)
        Me.pnDock.TabIndex = 2
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(772, 90)
        Me.Panel3.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label1.Location = New System.Drawing.Point(380, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(192, 21)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "USERNAME"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 46)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 25)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Curr"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label2.Location = New System.Drawing.Point(576, 7)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(192, 21)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "lblDateTime"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.Controls.Add(Me.Button9)
        Me.Panel4.Controls.Add(Me.Button7)
        Me.Panel4.Controls.Add(Me.Button6)
        Me.Panel4.Location = New System.Drawing.Point(71, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(210, 41)
        Me.Panel4.TabIndex = 0
        '
        'Button9
        '
        Me.Button9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button9.Image = CType(resources.GetObject("Button9.Image"), System.Drawing.Image)
        Me.Button9.Location = New System.Drawing.Point(3, 0)
        Me.Button9.Margin = New System.Windows.Forms.Padding(2)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(69, 41)
        Me.Button9.TabIndex = 7
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.Location = New System.Drawing.Point(72, 0)
        Me.Button7.Margin = New System.Windows.Forms.Padding(2)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(69, 41)
        Me.Button7.TabIndex = 6
        Me.Button7.Text = "SPC"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.Image = Global.Gui_Tset.My.Resources.Resources.icons8_logout_64
        Me.Button6.Location = New System.Drawing.Point(141, 0)
        Me.Button6.Margin = New System.Windows.Forms.Padding(2)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(69, 41)
        Me.Button6.TabIndex = 5
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.IndianRed
        Me.Panel2.Controls.Add(Me.btProg)
        Me.Panel2.Controls.Add(Me.btoper)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.btReturn)
        Me.Panel2.Controls.Add(Me.btsetup)
        Me.Panel2.Controls.Add(Me.btmain)
        Me.Panel2.Controls.Add(Me.btUser)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 688)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(772, 61)
        Me.Panel2.TabIndex = 0
        '
        'btProg
        '
        Me.btProg.Dock = System.Windows.Forms.DockStyle.Left
        Me.btProg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btProg.Location = New System.Drawing.Point(384, 0)
        Me.btProg.Margin = New System.Windows.Forms.Padding(2)
        Me.btProg.Name = "btProg"
        Me.btProg.Size = New System.Drawing.Size(128, 61)
        Me.btProg.TabIndex = 5
        Me.btProg.Text = "RECIPE CREATION"
        Me.btProg.UseVisualStyleBackColor = True
        '
        'btoper
        '
        Me.btoper.Dock = System.Windows.Forms.DockStyle.Left
        Me.btoper.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btoper.Location = New System.Drawing.Point(256, 0)
        Me.btoper.Margin = New System.Windows.Forms.Padding(2)
        Me.btoper.Name = "btoper"
        Me.btoper.Size = New System.Drawing.Size(128, 61)
        Me.btoper.TabIndex = 4
        Me.btoper.Text = "OPERATION"
        Me.btoper.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(128, 0)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(128, 61)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "HOMING "
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btReturn
        '
        Me.btReturn.Dock = System.Windows.Forms.DockStyle.Right
        Me.btReturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btReturn.Location = New System.Drawing.Point(506, 0)
        Me.btReturn.Margin = New System.Windows.Forms.Padding(2)
        Me.btReturn.Name = "btReturn"
        Me.btReturn.Size = New System.Drawing.Size(10, 61)
        Me.btReturn.TabIndex = 8
        Me.btReturn.Text = "RETURN"
        Me.btReturn.UseVisualStyleBackColor = True
        '
        'btsetup
        '
        Me.btsetup.Dock = System.Windows.Forms.DockStyle.Right
        Me.btsetup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btsetup.Location = New System.Drawing.Point(516, 0)
        Me.btsetup.Margin = New System.Windows.Forms.Padding(2)
        Me.btsetup.Name = "btsetup"
        Me.btsetup.Size = New System.Drawing.Size(128, 61)
        Me.btsetup.TabIndex = 7
        Me.btsetup.Text = "SETUP"
        Me.btsetup.UseVisualStyleBackColor = True
        '
        'btmain
        '
        Me.btmain.Dock = System.Windows.Forms.DockStyle.Right
        Me.btmain.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btmain.Location = New System.Drawing.Point(644, 0)
        Me.btmain.Margin = New System.Windows.Forms.Padding(2)
        Me.btmain.Name = "btmain"
        Me.btmain.Size = New System.Drawing.Size(128, 61)
        Me.btmain.TabIndex = 6
        Me.btmain.Text = "MAINTENANCE"
        Me.btmain.UseVisualStyleBackColor = True
        '
        'btUser
        '
        Me.btUser.Dock = System.Windows.Forms.DockStyle.Left
        Me.btUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btUser.Location = New System.Drawing.Point(0, 0)
        Me.btUser.Margin = New System.Windows.Forms.Padding(2)
        Me.btUser.Name = "btUser"
        Me.btUser.Size = New System.Drawing.Size(128, 61)
        Me.btUser.TabIndex = 3
        Me.btUser.Text = "USER"
        Me.btUser.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'Timer2
        '
        '
        'Timer3
        '
        '
        'Button2
        '
        Me.Button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(2, 2)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(69, 41)
        Me.Button2.TabIndex = 8
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Button2)
        Me.Panel5.Controls.Add(Me.Panel4)
        Me.Panel5.Location = New System.Drawing.Point(487, 46)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(284, 44)
        Me.Panel5.TabIndex = 12
        '
        'Home_Page
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(772, 749)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Home_Page"
        Me.Text = "LASER MARKING MACHINE"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents Button9 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents btReturn As Button
    Friend WithEvents btsetup As Button
    Friend WithEvents btmain As Button
    Friend WithEvents btProg As Button
    Friend WithEvents btoper As Button
    Friend WithEvents btUser As Button
    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents pnDock As Windows.Forms.Panel
    Friend WithEvents Panel3 As Windows.Forms.Panel
    Friend WithEvents Panel4 As Windows.Forms.Panel
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Button1 As Button
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Timer3 As Timer
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel5 As Windows.Forms.Panel
    Friend WithEvents Button2 As Button
End Class
