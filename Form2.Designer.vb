<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.GroupBoxImageSave = New System.Windows.Forms.GroupBox()
        Me.ButtonSavePng = New System.Windows.Forms.Button()
        Me.ButtonSaveTiff = New System.Windows.Forms.Button()
        Me.ButtonSaveJpg = New System.Windows.Forms.Button()
        Me.ButtonSaveBmp = New System.Windows.Forms.Button()
        Me.GroupBoxDeviceControl = New System.Windows.Forms.GroupBox()
        Me.ButtonOpenDevice = New System.Windows.Forms.Button()
        Me.ButtonCloseDevice = New System.Windows.Forms.Button()
        Me.GroupBoxImageSave.SuspendLayout()
        Me.GroupBoxDeviceControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxImageSave
        '
        Me.GroupBoxImageSave.Controls.Add(Me.ButtonSavePng)
        Me.GroupBoxImageSave.Controls.Add(Me.ButtonSaveTiff)
        Me.GroupBoxImageSave.Controls.Add(Me.ButtonSaveJpg)
        Me.GroupBoxImageSave.Controls.Add(Me.ButtonSaveBmp)
        Me.GroupBoxImageSave.Location = New System.Drawing.Point(116, 117)
        Me.GroupBoxImageSave.Name = "GroupBoxImageSave"
        Me.GroupBoxImageSave.Size = New System.Drawing.Size(246, 89)
        Me.GroupBoxImageSave.TabIndex = 18
        Me.GroupBoxImageSave.TabStop = False
        Me.GroupBoxImageSave.Text = "Picture Storage"
        '
        'ButtonSavePng
        '
        Me.ButtonSavePng.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonSavePng.Location = New System.Drawing.Point(132, 57)
        Me.ButtonSavePng.Name = "ButtonSavePng"
        Me.ButtonSavePng.Size = New System.Drawing.Size(88, 25)
        Me.ButtonSavePng.TabIndex = 3
        Me.ButtonSavePng.Text = "Save as PNG"
        Me.ButtonSavePng.UseVisualStyleBackColor = True
        '
        'ButtonSaveTiff
        '
        Me.ButtonSaveTiff.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonSaveTiff.Location = New System.Drawing.Point(24, 57)
        Me.ButtonSaveTiff.Name = "ButtonSaveTiff"
        Me.ButtonSaveTiff.Size = New System.Drawing.Size(88, 25)
        Me.ButtonSaveTiff.TabIndex = 2
        Me.ButtonSaveTiff.Text = "Save as TIFF"
        Me.ButtonSaveTiff.UseVisualStyleBackColor = True
        '
        'ButtonSaveJpg
        '
        Me.ButtonSaveJpg.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonSaveJpg.Location = New System.Drawing.Point(132, 22)
        Me.ButtonSaveJpg.Name = "ButtonSaveJpg"
        Me.ButtonSaveJpg.Size = New System.Drawing.Size(88, 25)
        Me.ButtonSaveJpg.TabIndex = 1
        Me.ButtonSaveJpg.Text = "Save as JPG"
        Me.ButtonSaveJpg.UseVisualStyleBackColor = True
        '
        'ButtonSaveBmp
        '
        Me.ButtonSaveBmp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonSaveBmp.Location = New System.Drawing.Point(24, 22)
        Me.ButtonSaveBmp.Name = "ButtonSaveBmp"
        Me.ButtonSaveBmp.Size = New System.Drawing.Size(88, 25)
        Me.ButtonSaveBmp.TabIndex = 0
        Me.ButtonSaveBmp.Text = "Save as BMP"
        Me.ButtonSaveBmp.UseVisualStyleBackColor = True
        '
        'GroupBoxDeviceControl
        '
        Me.GroupBoxDeviceControl.Controls.Add(Me.ButtonOpenDevice)
        Me.GroupBoxDeviceControl.Controls.Add(Me.ButtonCloseDevice)
        Me.GroupBoxDeviceControl.Location = New System.Drawing.Point(61, 71)
        Me.GroupBoxDeviceControl.Name = "GroupBoxDeviceControl"
        Me.GroupBoxDeviceControl.Size = New System.Drawing.Size(231, 46)
        Me.GroupBoxDeviceControl.TabIndex = 17
        Me.GroupBoxDeviceControl.TabStop = False
        Me.GroupBoxDeviceControl.Text = "Device Control"
        '
        'ButtonOpenDevice
        '
        Me.ButtonOpenDevice.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonOpenDevice.Location = New System.Drawing.Point(11, 14)
        Me.ButtonOpenDevice.Name = "ButtonOpenDevice"
        Me.ButtonOpenDevice.Size = New System.Drawing.Size(88, 25)
        Me.ButtonOpenDevice.TabIndex = 1
        Me.ButtonOpenDevice.Text = "Open Device"
        Me.ButtonOpenDevice.UseVisualStyleBackColor = True
        '
        'ButtonCloseDevice
        '
        Me.ButtonCloseDevice.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonCloseDevice.Location = New System.Drawing.Point(119, 14)
        Me.ButtonCloseDevice.Name = "ButtonCloseDevice"
        Me.ButtonCloseDevice.Size = New System.Drawing.Size(88, 25)
        Me.ButtonCloseDevice.TabIndex = 2
        Me.ButtonCloseDevice.Text = "Close Device"
        Me.ButtonCloseDevice.UseVisualStyleBackColor = True
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(772, 450)
        Me.Controls.Add(Me.GroupBoxImageSave)
        Me.Controls.Add(Me.GroupBoxDeviceControl)
        Me.Name = "Form2"
        Me.Text = "Form2"
        Me.GroupBoxImageSave.ResumeLayout(False)
        Me.GroupBoxDeviceControl.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBoxImageSave As GroupBox
    Friend WithEvents ButtonSavePng As Button
    Friend WithEvents ButtonSaveTiff As Button
    Friend WithEvents ButtonSaveJpg As Button
    Friend WithEvents ButtonSaveBmp As Button
    Friend WithEvents GroupBoxDeviceControl As GroupBox
    Friend WithEvents ButtonOpenDevice As Button
    Friend WithEvents ButtonCloseDevice As Button
End Class
