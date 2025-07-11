Imports MySql.Data.MySqlClient

Public Class Form7
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()

        Me.ContextMenuStrip = ContextMenuStrip1
        Label1.Text = "Jadwal Les Bahasa Jepang"
        RadioButton1.Text = "Selesai"
        RadioButton2.Text = "Batal"
        Button1.Text = "Submit"

        Button1.Enabled = False 'mematikan tombol

        tampil_data()
    End Sub

    Sub tampil_data()
        'menampilkan data di datagrid
        Dim query As String = "select j.id_jadwal, j.hari, j.jam, " & "g.nama as nama_guru, m.nama as nama_murid, j.materi, j.status " &
                                  "from jadwal j " &
                                  "JOIN data_guru g ON j.id_guru = g.id_guru " &
                                  "JOIN data_murid m ON j.id_murid = m.id_murid " &
                                  "Order BY j.id_jadwal"
        da = New MySqlDataAdapter(query, con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "jadwal")
        Me.DataGridView1.DataSource = ds.Tables("jadwal")

    End Sub


    Dim selectedId As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim status As String = ""

        If RadioButton1.Checked Then
            status = RadioButton1.Text
        ElseIf RadioButton2.Checked Then
            status = RadioButton2.Text
        End If

        Select Case status
            Case "Selesai"
                If selectedId <> "" Then
                    Dim edit As String
                    edit = "update jadwal set status = 'Selesai' where id_jadwal = '" & selectedId & "'"
                    cmd = New MySqlCommand(edit, con)
                    cmd.ExecuteNonQuery()
                    MsgBox("Status diupdate menjadi SELESAI")
                End If
            Case "Batal"
                If selectedId <> "" Then
                    Dim edit As String
                    edit = "update jadwal set status = 'Batal' where id_jadwal = '" & selectedId & "'"
                    cmd = New MySqlCommand(edit, con)
                    cmd.ExecuteNonQuery()
                    MsgBox("Status diupdate menjadi BATAL")
                End If
            Case Else
                MsgBox("Silahkan pilih statusnya")
        End Select
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            selectedId = DataGridView1.Rows(e.RowIndex).Cells("id_jadwal").Value.ToString()
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Button1.Enabled = True
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Button1.Enabled = True
    End Sub

    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        Form6.Show()
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Me.Hide()
        Form6.Hide()
        Form1.Show()
    End Sub
End Class