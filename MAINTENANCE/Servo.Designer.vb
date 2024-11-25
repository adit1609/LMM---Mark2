<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Servo
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_ServoH = New System.Windows.Forms.Button()
        Me.btn_ServoON = New System.Windows.Forms.Button()
        Me.btn_ServoRST = New System.Windows.Forms.Button()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.btnForward = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_jogDConti = New System.Windows.Forms.Button()
        Me.btn_jogD4 = New System.Windows.Forms.Button()
        Me.btn_jogD3 = New System.Windows.Forms.Button()
        Me.btn_jogD2 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btn_JogD1 = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.SERVOSPEED = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel2.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(772, 1068)
        Me.Panel1.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Panel9)
        Me.Panel4.Controls.Add(Me.Panel6)
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 572)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(772, 479)
        Me.Panel4.TabIndex = 7
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Panel11)
        Me.Panel9.Controls.Add(Me.Panel8)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(0, 139)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(405, 338)
        Me.Panel9.TabIndex = 3
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.TableLayoutPanel2)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(405, 200)
        Me.Panel11.TabIndex = 13
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.Linen
        Me.TableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btn_ServoH, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_ServoON, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.btn_ServoRST, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.52941!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.47059!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(405, 200)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'btn_ServoH
        '
        Me.btn_ServoH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_ServoH.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ServoH.Location = New System.Drawing.Point(3, 69)
        Me.btn_ServoH.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_ServoH.Name = "btn_ServoH"
        Me.btn_ServoH.Size = New System.Drawing.Size(399, 65)
        Me.btn_ServoH.TabIndex = 8
        Me.btn_ServoH.Text = "SERVO HOME"
        Me.btn_ServoH.UseVisualStyleBackColor = True
        '
        'btn_ServoON
        '
        Me.btn_ServoON.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_ServoON.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ServoON.Location = New System.Drawing.Point(3, 139)
        Me.btn_ServoON.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_ServoON.Name = "btn_ServoON"
        Me.btn_ServoON.Size = New System.Drawing.Size(399, 58)
        Me.btn_ServoON.TabIndex = 8
        Me.btn_ServoON.Text = "SERVO ON"
        Me.btn_ServoON.UseVisualStyleBackColor = True
        '
        'btn_ServoRST
        '
        Me.btn_ServoRST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_ServoRST.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ServoRST.Location = New System.Drawing.Point(3, 3)
        Me.btn_ServoRST.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_ServoRST.Name = "btn_ServoRST"
        Me.btn_ServoRST.Size = New System.Drawing.Size(399, 61)
        Me.btn_ServoRST.TabIndex = 7
        Me.btn_ServoRST.Text = "SERVO RESET"
        Me.btn_ServoRST.UseVisualStyleBackColor = True
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.btnForward)
        Me.Panel8.Controls.Add(Me.Label5)
        Me.Panel8.Controls.Add(Me.Label4)
        Me.Panel8.Controls.Add(Me.btnBack)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel8.Location = New System.Drawing.Point(0, 200)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(405, 138)
        Me.Panel8.TabIndex = 12
        '
        'btnForward
        '
        Me.btnForward.BackgroundImage = Global.Gui_Tset.My.Resources.Resources.icons8_right_arrow_50__1_
        Me.btnForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnForward.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnForward.Location = New System.Drawing.Point(211, 49)
        Me.btnForward.Margin = New System.Windows.Forms.Padding(2)
        Me.btnForward.Name = "btnForward"
        Me.btnForward.Size = New System.Drawing.Size(188, 65)
        Me.btnForward.TabIndex = 9
        Me.btnForward.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(225, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(161, 31)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "FORWARD  SERVO"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(25, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(161, 31)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "BACKWARD  SERVO"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBack
        '
        Me.btnBack.BackgroundImage = Global.Gui_Tset.My.Resources.Resources.icons8_left_arrow_50
        Me.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(10, 49)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(2)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(188, 65)
        Me.btnBack.TabIndex = 8
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.OldLace
        Me.Panel6.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel6.Location = New System.Drawing.Point(405, 139)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(365, 338)
        Me.Panel6.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Linen
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btn_jogDConti, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_jogD4, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_jogD3, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_jogD2, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_JogD1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.64286!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.35714!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(365, 338)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'btn_jogDConti
        '
        Me.btn_jogDConti.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_jogDConti.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_jogDConti.Location = New System.Drawing.Point(3, 289)
        Me.btn_jogDConti.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_jogDConti.Name = "btn_jogDConti"
        Me.btn_jogDConti.Size = New System.Drawing.Size(359, 46)
        Me.btn_jogDConti.TabIndex = 10
        Me.btn_jogDConti.Text = "CONTINUES"
        Me.btn_jogDConti.UseVisualStyleBackColor = True
        '
        'btn_jogD4
        '
        Me.btn_jogD4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_jogD4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_jogD4.Location = New System.Drawing.Point(3, 232)
        Me.btn_jogD4.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_jogD4.Name = "btn_jogD4"
        Me.btn_jogD4.Size = New System.Drawing.Size(359, 52)
        Me.btn_jogD4.TabIndex = 9
        Me.btn_jogD4.Text = "5"
        Me.btn_jogD4.UseVisualStyleBackColor = True
        '
        'btn_jogD3
        '
        Me.btn_jogD3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_jogD3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_jogD3.Location = New System.Drawing.Point(3, 175)
        Me.btn_jogD3.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_jogD3.Name = "btn_jogD3"
        Me.btn_jogD3.Size = New System.Drawing.Size(359, 52)
        Me.btn_jogD3.TabIndex = 8
        Me.btn_jogD3.Text = "1"
        Me.btn_jogD3.UseVisualStyleBackColor = True
        '
        'btn_jogD2
        '
        Me.btn_jogD2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_jogD2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_jogD2.Location = New System.Drawing.Point(3, 115)
        Me.btn_jogD2.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_jogD2.Name = "btn_jogD2"
        Me.btn_jogD2.Size = New System.Drawing.Size(359, 55)
        Me.btn_jogD2.TabIndex = 7
        Me.btn_jogD2.Text = "0.1"
        Me.btn_jogD2.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(357, 49)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "JOG DISTANCE"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btn_JogD1
        '
        Me.btn_JogD1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_JogD1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_JogD1.Location = New System.Drawing.Point(3, 53)
        Me.btn_JogD1.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_JogD1.Name = "btn_JogD1"
        Me.btn_JogD1.Size = New System.Drawing.Size(359, 57)
        Me.btn_JogD1.TabIndex = 6
        Me.btn_JogD1.Text = ".01"
        Me.btn_JogD1.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Panel10)
        Me.Panel5.Controls.Add(Me.Panel7)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(770, 139)
        Me.Panel5.TabIndex = 0
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.Button11)
        Me.Panel10.Controls.Add(Me.Button12)
        Me.Panel10.Controls.Add(Me.Button13)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(403, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(365, 137)
        Me.Panel10.TabIndex = 3
        '
        'Button11
        '
        Me.Button11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button11.Location = New System.Drawing.Point(5, 44)
        Me.Button11.Margin = New System.Windows.Forms.Padding(2)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(100, 57)
        Me.Button11.TabIndex = 11
        Me.Button11.Text = "X"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button12.Location = New System.Drawing.Point(135, 44)
        Me.Button12.Margin = New System.Windows.Forms.Padding(2)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(99, 57)
        Me.Button12.TabIndex = 10
        Me.Button12.Text = "Y"
        Me.Button12.UseVisualStyleBackColor = True
        '
        'Button13
        '
        Me.Button13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button13.Location = New System.Drawing.Point(269, 44)
        Me.Button13.Margin = New System.Windows.Forms.Padding(2)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(93, 57)
        Me.Button13.TabIndex = 9
        Me.Button13.Text = "CW"
        Me.Button13.UseVisualStyleBackColor = True
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Panel12)
        Me.Panel7.Controls.Add(Me.SERVOSPEED)
        Me.Panel7.Controls.Add(Me.Label3)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(403, 137)
        Me.Panel7.TabIndex = 0
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.CheckBox1)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(403, 54)
        Me.Panel12.TabIndex = 3
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(0, 0)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(131, 54)
        Me.CheckBox1.TabIndex = 0
        Me.CheckBox1.Text = "AXIS INHABIT"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'SERVOSPEED
        '
        Me.SERVOSPEED.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.SERVOSPEED.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SERVOSPEED.FormattingEnabled = True
        Me.SERVOSPEED.Items.AddRange(New Object() {"HIGH", "MEDIUM", "LOW", "SET MANUAL"})
        Me.SERVOSPEED.Location = New System.Drawing.Point(56, 94)
        Me.SERVOSPEED.Name = "SERVOSPEED"
        Me.SERVOSPEED.Size = New System.Drawing.Size(295, 23)
        Me.SERVOSPEED.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(123, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(161, 31)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "SERVO SPEED"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 51)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(772, 521)
        Me.Panel3.TabIndex = 6
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(772, 51)
        Me.Panel2.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(305, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(161, 31)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SERVO SETTING"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer1
        '
        '
        'Servo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(772, 1068)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Servo"
        Me.Text = "Servo"
        Me.Panel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents Panel4 As Windows.Forms.Panel
    Friend WithEvents Panel9 As Windows.Forms.Panel
    Friend WithEvents Panel11 As Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents btn_ServoH As Button
    Friend WithEvents btn_ServoON As Button
    Friend WithEvents btn_ServoRST As Button
    Friend WithEvents Panel8 As Windows.Forms.Panel
    Friend WithEvents btnForward As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnBack As Button
    Friend WithEvents Panel6 As Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents btn_jogDConti As Button
    Friend WithEvents btn_jogD4 As Button
    Friend WithEvents btn_jogD3 As Button
    Friend WithEvents btn_jogD2 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents btn_JogD1 As Button
    Friend WithEvents Panel5 As Windows.Forms.Panel
    Friend WithEvents Panel10 As Windows.Forms.Panel
    Friend WithEvents Button11 As Button
    Friend WithEvents Button12 As Button
    Friend WithEvents Button13 As Button
    Friend WithEvents Panel7 As Windows.Forms.Panel
    Friend WithEvents SERVOSPEED As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel3 As Windows.Forms.Panel
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel12 As Windows.Forms.Panel
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Timer1 As Timer
End Class
