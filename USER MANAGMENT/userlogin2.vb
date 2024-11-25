Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Xml
Public Class userlogin2
    Dim conn As New SqlConnection
    Dim cmd As New SqlCommand
    Dim sql As String = Nothing
    Private lastUserFilePath As String = "L:\Laser Temp files\Database\userlogin\lastUserloginname.txt"

    Private Sub userlogin2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Data Source=(localdb)\LLMdb;Initial Catalog=useraccess;Integrated Security=True"
        txtPassword.PasswordChar = "*"

        ' Load the last user login
        LoadLastUserLogin()
    End Sub

    Private Sub lblCreateaccount_Click(sender As Object, e As EventArgs) Handles lblCreateaccount.Click
        Using passwordPrompt As New Form()
            ' Create a new form for password input
            passwordPrompt.Text = "Admin Authentication"
            passwordPrompt.FormBorderStyle = FormBorderStyle.FixedDialog
            passwordPrompt.StartPosition = FormStartPosition.CenterParent
            passwordPrompt.MaximizeBox = False
            passwordPrompt.MinimizeBox = False
            passwordPrompt.ClientSize = New Size(250, 100)

            Dim lblPassword As New Label() With {.Left = 10, .Top = 20, .Text = "Please Enter Password:"}
            Dim txtPassword As New TextBox() With {.Left = 10, .Top = 40, .Width = 200, .PasswordChar = "*"c}
            Dim btnOK As New Button() With {.Text = "OK", .Left = 130, .Width = 80, .Top = 60, .DialogResult = DialogResult.OK}

            AddHandler btnOK.Click, Sub(sender2, e2)
                                        If txtPassword.Text = "NMT" Then ' Replace with your admin password
                                            Dim newUserForm As New registration2()
                                            Home_Page.Home_Page(New registration2)
                                            passwordPrompt.Close()
                                            'Me.Hide()
                                        Else
                                            MessageBox.Show("Incorrect password. Access denied.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        End If
                                    End Sub

            passwordPrompt.Controls.Add(lblPassword)
            passwordPrompt.Controls.Add(txtPassword)
            passwordPrompt.Controls.Add(btnOK)
            passwordPrompt.AcceptButton = btnOK

            passwordPrompt.ShowDialog(Me)
        End Using
        registration2.Show()
        Me.Hide()
    End Sub

    Private Sub btn_Login_Click(sender As Object, e As EventArgs) Handles btn_Login.Click
        If txtUsername.Text = "" Or txtPassword.Text = "" Or cbUserType.SelectedIndex = -1 Then
            MessageBox.Show("Please fill all blank fields", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        sql = "SELECT * FROM Table1 WHERE ACCESSLEVEL = @ACCESSLEVEL AND USERNAME = @USERNAME AND PASSWORD = @PASSWORD"
        cmd = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@USERNAME", txtUsername.Text)
        cmd.Parameters.AddWithValue("@PASSWORD", txtPassword.Text)
        cmd.Parameters.AddWithValue("@ACCESSLEVEL", cbUserType.SelectedItem.ToString())

        Dim reader As SqlDataReader = cmd.ExecuteReader()

        If reader.HasRows Then

            Home_Page.Home_Page(New Form1)


            ' Save the current user as the last user
            SaveLastUserLogin(txtUsername.Text)
        Else
            MessageBox.Show("Invalid username, password, or user type. Try Again.", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error)

            txtPassword.Clear()
            cbUserType.SelectedIndex = -1
        End If

        conn.Close()
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub chkboxShowpass_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxShowpass.CheckedChanged
        If chkboxShowpass.Checked Then
            txtPassword.PasswordChar = ""
        Else
            txtPassword.PasswordChar = "*"
        End If
    End Sub

    Private Sub LoadLastUserLogin()
        If File.Exists(lastUserFilePath) Then
            Try
                Dim lastUser As String = File.ReadAllText(lastUserFilePath)
                txtLastuserlogin.Text = lastUser
            Catch ex As Exception
                MessageBox.Show("An error occurred while loading the last user login: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            txtLastuserlogin.Text = "No user logged in yet."
        End If
    End Sub

    Private Sub SaveLastUserLogin(username As String)
        Try
            File.WriteAllText(lastUserFilePath, username)
        Catch ex As Exception
            MessageBox.Show("An error occurred while saving the last user login: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text

        If ValidateCredentials(username, password) Then
            ' If login is successful, open the SPC form
            Dim spcForm As New SPCModuleForm()
            Me.Hide() ' Hide the login form
            spcForm.ShowDialog()
            Me.Close() ' Close the login form after the SPC form is closed
        Else
            MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Function ValidateCredentials(username As String, password As String) As Boolean
        ' Example of hardcoded validation. Replace with your XML or database check.
        ' Example: Using XML for authentication
        Dim doc As New XmlDocument()
        doc.Load("SPCUserAccess.xml")

        For Each userNode As XmlNode In doc.SelectNodes("//User")
            Dim user As String = userNode.SelectSingleNode("Username").InnerText
            Dim pass As String = userNode.SelectSingleNode("Password").InnerText

            If user = username AndAlso pass = password Then
                Return True
            End If
        Next

        Return False
    End Function

End Class