Imports MySql.Data.MySqlClient

Public Class Form1
    Dim con As MySqlConnection
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader

    Sub koneksi()

        db = "server=localhost;user id = root; password=;database=data_uas"
        con = New MySqlConnection(db)
        Try
            con.Open()
        Catch ex As Exception
            MessageBox.Show("Koneksi gagal: " & ex.Message)
        End Try
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Text = "login"
        Label1.Text = "Username"
        Label2.Text = "Password"

        TextBox2.PasswordChar = "*"

        bersih()
    End Sub

    Sub bersih()
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        koneksi()
        cmd = New MySqlCommand("select * from users where username = @user AND password = @pass", con)
        cmd.Parameters.AddWithValue("@user", TextBox1.Text)
        cmd.Parameters.AddWithValue("@pass", TextBox2.Text)

        dr = cmd.ExecuteReader

        If dr.HasRows Then
            dr.Read()
            Dim level As String = dr("level")

            If level = "admin" Then
                MsgBox("Login sebagai Admin")
                Form2.Show()
            ElseIf level = "guru" Then
                MsgBox("Login sebagai Guru")
                Form6.Show()
            Else
                MsgBox("Login Gagal")
            End If
        Else
            MsgBox("Username atau Password salah")
        End If

        bersih()
        con.Close()
    End Sub

End Class
