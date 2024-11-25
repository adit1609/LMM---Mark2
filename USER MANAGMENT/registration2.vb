Imports System.Data.SqlClient
Imports System.Data
Public Class registration2
    Private con As New SqlConnection("Data Source=(localdb)\LLMdb;Initial Catalog=useraccess;Integrated Security=True;Encrypt=False")
    Private cmd As SqlCommand
    Private da As SqlDataAdapter

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub registration2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPass.PasswordChar = "*"c
        txtConfirmpass.PasswordChar = "*"c
        LoadUserData()
    End Sub

    Private Sub btn_Register_Click(sender As Object, e As EventArgs) Handles btn_Register.Click
        If String.IsNullOrWhiteSpace(txtNewuser.Text) OrElse
         String.IsNullOrWhiteSpace(txtPass.Text) OrElse
         String.IsNullOrWhiteSpace(txtConfirmpass.Text) Then
            MessageBox.Show("Username and Password are required.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If txtPass.Text <> txtConfirmpass.Text Then
            MessageBox.Show("Passwords do not match. Please re-enter.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPass.Clear()
            txtConfirmpass.Clear()
            Return
        End If

        If cbUserType.SelectedIndex = -1 Then
            MessageBox.Show("Please select a user type.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            con.Open()

            ' Check if the username already exists
            Dim checkUserQuery As String = "SELECT COUNT(*) FROM Table1 WHERE USERNAME = @Username"
            cmd = New SqlCommand(checkUserQuery, con)
            cmd.Parameters.AddWithValue("@Username", txtNewuser.Text)

            Dim userCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If userCount > 0 Then
                MessageBox.Show("Username already exists. Please choose a different username.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                ' Insert new user
                Dim registerQuery As String = "INSERT INTO Table1 ([ACCESSLEVEL], [USERNAME], [PASSWORD], [CREATED_DATE]) VALUES (@ACCESSLEVEL, @USERNAME, @PASSWORD, @CREATED_DATE)"
                cmd = New SqlCommand(registerQuery, con)
                cmd.Parameters.AddWithValue("@ACCESSLEVEL", cbUserType.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@USERNAME", txtNewuser.Text)
                cmd.Parameters.AddWithValue("@PASSWORD", txtPass.Text)
                cmd.Parameters.AddWithValue("@CREATED_DATE", DateTime.Now)

                cmd.ExecuteNonQuery()

                MessageBox.Show("Your account has been created successfully.", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                txtNewuser.Clear()
                txtPass.Clear()
                txtConfirmpass.Clear()
                cbUserType.SelectedIndex = -1 ' Reset to default
                txtNewuser.Focus()

                LoadUserData()

                Me.Close()
                Dim loginForm As New userlogin2()
                loginForm.Show()
            End If
        Catch ex As SqlException
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub LoadUserData()

        If con.State = ConnectionState.Open Then
            con.Close()
        End If

        Try
            con.Open()
            Dim fetchUsersQuery As String = "SELECT * FROM Table1"
            da = New SqlDataAdapter(fetchUsersQuery, con)
            Dim dt As New DataTable()
            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("An error occurred while loading user data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub chkboxShowpass_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxShowpass.CheckedChanged
        If chkboxShowpass.Checked Then
            txtPass.PasswordChar = ChrW(0)
            txtConfirmpass.PasswordChar = ChrW(0)
        Else
            txtPass.PasswordChar = "*"c
            txtConfirmpass.PasswordChar = "*"c
        End If
    End Sub

    Private Sub lblBacktologin_Click(sender As Object, e As EventArgs) Handles lblBacktologin.Click
        Dim loginForm As New userlogin2()
        loginForm.Show()
        Me.Hide()
    End Sub

    Private Sub btn_Delete_Click(sender As Object, e As EventArgs) Handles btn_Delete.Click
        ' Ensure a row is selected
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a row to delete.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Get the selected row
        Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)

        ' Get the username or primary key value of the selected row
        Dim username As String = selectedRow.Cells("USERNAME").Value.ToString() ' Replace "USERNAME" with your primary key column if different

        ' Ask for confirmation
        Dim confirmDelete As DialogResult = MessageBox.Show("Are you sure you want to delete the selected user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmDelete = DialogResult.Yes Then
            Try
                ' Delete the record from the database
                con.Open()
                Dim deleteQuery As String = "DELETE FROM Table1 WHERE USERNAME = @USERNAME" ' Adjust the WHERE clause if your primary key is different
                cmd = New SqlCommand(deleteQuery, con)
                cmd.Parameters.AddWithValue("@USERNAME", username)
                cmd.ExecuteNonQuery()

                ' Refresh the DataGridView
                LoadUserData()

                MessageBox.Show("User deleted successfully.", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As SqlException
                MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End Try
        End If

    End Sub
End Class