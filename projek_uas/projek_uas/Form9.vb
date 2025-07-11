Imports MySql.Data.MySqlClient

Public Class Form9
    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()

        Label1.Text = "Jadwal Les"

        tampil_data()
    End Sub

    Sub tampil_data()
        'menampilkan data di datagrid
        da = New MySqlDataAdapter("select * from jadwal order by id_jadwal", con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "jadwal")
        Me.DataGridView1.DataSource = ds.Tables("jadwal")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        '--------------mencari nama barang di datagrid------------
        da = New MySqlDataAdapter("select * from jadwal where hari like '%" & Me.TextBox1.Text & "%'", con)
        ds = New DataSet
        ' ds.clear()
        da.Fill(ds, "jadwal")
        DataGridView1.DataSource = (ds.Tables("jadwal"))
    End Sub
End Class