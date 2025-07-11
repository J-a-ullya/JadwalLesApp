Imports MySql.Data.MySqlClient

Public Class Form5
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()

        Me.ContextMenuStrip = ContextMenuStrip1
        Label1.Text = "ID Murid"
        Label2.Text = "Nama Murid"
        Label3.Text = "Level"
        Label4.Text = "Target"
        Label5.Text = "No Telp"
        Label6.Text = "Data Murid"
        Label7.Text = "Alamat"
        Button1.Text = "Simpan"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Hapus Semua Data"

        TextBox1.ReadOnly = True 'agar data textbox1 tidak dirubah

        'data combobox level
        Dim level(2) As String
        level(0) = "Basic"
        level(1) = "Pre-Intermediate"
        level(2) = "Intermediate"
        'looping untuk mengeluarkan data array
        For Each element As String In level
            ComboBox1.Items.Add(element)
        Next

        bersih()
    End Sub

    Sub bersih()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""

        'mematikan tombol
        Me.Button1.Enabled = True
        Me.Button2.Enabled = False
        Me.Button3.Enabled = False
        Me.Button4.Enabled = True

        tampil_data()
    End Sub

    Sub tampil_data()
        'menampilkan data di datagrid
        da = New MySqlDataAdapter("select * from data_murid order by id_murid", con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "data_murid")
        Me.DataGridView1.DataSource = (ds.Tables("data_murid"))
        nomor()
    End Sub

    Sub nomor()
        Dim DR As DataRow
        Dim s As String
        'mengambil kode dari data flied id, kemudian dicari nilai yang paling besar (max)
        'kemudian hasilnya ditampung di field buatan dengan nomor
        DR = SQLTABLE("select max(right(id_murid, 1)) as nomor from data_murid").Rows(0)

        'jika berisi null atau tidak ditemukan
        If DR.IsNull("nomor") Then
            s = "MP-1" 'member nilai awal
        Else
            s = "MP-" & Format(DR("nomor") + 1, "0")
        End If
        TextBox1.Text = s
        TextBox1.Enabled = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.TextBox1.Text = "" Or Me.TextBox2.Text = "" Or Me.TextBox3.Text = "" Or Me.TextBox4.Text = "" Or
            Me.TextBox5.Text = "" Or Me.ComboBox1.Text Is Nothing Then
            MsgBox("Pastikan data terisi semua", MsgBoxStyle.Exclamation)

        Else
            Dim simpan As String

            MsgBox("Data tersimpan")
            simpan = "insert into data_murid (id_murid, nama, level, target, alamat, notelp) values ('" & Me.TextBox1.Text & "',
            '" & Me.TextBox2.Text & "','" & Me.ComboBox1.Text & "','" & Me.TextBox4.Text & "', '" & Me.TextBox3.Text & "',
            '" & Me.TextBox5.Text & "')"
            cmd = New MySqlCommand(simpan, con) 'untuk menghubungkan tabel ke database dan data lalu sistem
            cmd.ExecuteNonQuery() 'mengeksekusi data

            bersih()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.TextBox1.Text = "" Or Me.TextBox2.Text = "" Or Me.TextBox3.Text = "" Or Me.TextBox4.Text = "" Or
            Me.TextBox5.Text = "" Or Me.ComboBox1.Text Is Nothing Then
            MsgBox("Maaf, tidak ada data yang diupdate", MsgBoxStyle.Exclamation)

        Else
            Dim edit As String
            MsgBox("Data di-update")
            edit = "update data_murid set nama ='" & Me.TextBox2.Text & "', 
            level='" & Me.ComboBox1.Text & "', target ='" & Me.TextBox4.Text & "', alamat ='" & Me.TextBox3.Text & "', notelp ='" & Me.TextBox5.Text & "' where
            id_murid ='" & Me.TextBox1.Text & "'"
            cmd = New MySqlCommand(edit, con) 'untuk menghubungkan database lalu tabel diedit
            cmd.ExecuteNonQuery() 'mengeksekusi data
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Me.TextBox1.Text = "" Or Me.TextBox2.Text = "" Or Me.TextBox3.Text = "" Or Me.TextBox4.Text = "" Or
            Me.TextBox5.Text = "" Or Me.ComboBox1.Text Is Nothing Then
            MsgBox("Maaf, tidak ada data yang dihapus")
        Else
            '------------coding hapus data--------------"
            If MessageBox.Show("Anda yakin akan menghapus data?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim hapus As String
                hapus = "delete from data_murid where id_murid ='" & Me.TextBox1.Text & "'"
                cmd = New MySqlCommand(hapus, con)
                cmd.ExecuteNonQuery()
                bersih()
            Else
            End If

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MessageBox.Show("Yakin ingin menghapus semua data murid?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Dim hapusSemua As String = "DELETE FROM data_murid"
                cmd = New MySqlCommand(hapusSemua, con)
                If con.State = ConnectionState.Closed Then con.Open()
                cmd.ExecuteNonQuery()
                con.Close()

                MessageBox.Show("Semua data berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Panggil ulang prosedur untuk menampilkan data jika ada
                tampil_data()

            Catch ex As Exception
                MessageBox.Show("Terjadi kesalahan: " & ex.Message)
            End Try
        End If
    End Sub


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'fungsi untuk men-klik data yang akan dipilih
        Dim kodesd As String

        kodesd = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        TextBox1.Text = kodesd
        TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value

        Me.Button1.Enabled = False
        Me.Button2.Enabled = True
        Me.Button3.Enabled = True
        Me.Button4.Enabled = True
    End Sub

    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        Form2.Show()

    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Me.Hide()
        Form4.Hide()
        Form3.Hide()
        Form2.Hide()
        Form1.Show()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub
End Class