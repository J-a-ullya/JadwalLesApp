Imports MySql.Data.MySqlClient

Public Class Form8
    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()

        Me.ContextMenuStrip = ContextMenuStrip1

        Label1.Text = "ID Materi"
        Label2.Text = "Nama Materi"
        Label3.Text = "Metode"
        Label4.Text = "Pembayaran"
        Label5.Text = "Tanggal"
        Label6.Text = "Nominal"
        Button1.Text = "Simpan"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Hapus Semua"

        TextBox1.ReadOnly = True

        'data combobox level
        Dim bayarr(1) As String
        bayarr(0) = "Cash"
        bayarr(1) = "Transfer"
        For Each element As String In bayarr
            ComboBox1.Items.Add(element)
        Next


        bersih()

        ComboBox()
    End Sub

    Sub combobox()
        'data murid
        da = New MySqlDataAdapter("select id_murid, nama from data_murid", con)
        ds = New DataSet
        da.Fill(ds, "data_murid")
        ComboBox2.DataSource = ds.Tables("data_murid")
        ComboBox2.DisplayMember = "nama"
        ComboBox2.ValueMember = "id_murid"
    End Sub

    Sub bersih()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox4.Text = ""

        'mematikan tombol
        Me.Button1.Enabled = True
        Me.Button2.Enabled = False
        Me.Button3.Enabled = False
        Me.Button4.Enabled = True

        tampil_data()
    End Sub

    Sub tampil_data()
        'menampilkan data di datagrid
        Dim query As String = "SELECT j.id_bayar, " &
                              "m.nama as nama_murid, j.tanggal, j.jumlah, j.metode " &
                              "from bayar j " &
                              "JOIN data_murid m ON j.id_murid = m.id_murid " &
                              "Order BY j.id_bayar"
        da = New MySqlDataAdapter(query, con)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "bayar")
        Me.DataGridView1.DataSource = ds.Tables("bayar")

        nomor()
    End Sub

    Sub nomor()
        Dim DR As DataRow
        Dim s As String
        'mengambil kode dari data flied id, kemudian dicari nilai yang paling besar (max)
        'kemudian hasilnya ditampung di field buatan dengan nomor
        DR = SQLTABLE("select max(right(id_bayar, 1)) as nomor from bayar").Rows(0)

        'jika berisi null atau tidak ditemukan
        If DR.IsNull("nomor") Then
            s = "PL-1" 'member nilai awal
        Else
            s = "PL-" & Format(DR("nomor") + 1, "0")
        End If
        TextBox1.Text = s
        TextBox1.Enabled = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Me.TextBox1.Text = "" Or Me.ComboBox2.SelectedValue Is Nothing Or Me.TextBox4.Text = "" Or
            Me.ComboBox1.Text = "" Or Me.TextBox2.Text = "" Then
            MsgBox("Pastikan data terisi semua", MsgBoxStyle.Exclamation)
        Else
            Dim simpan As String
            MsgBox("Data tersimpan")
            simpan = "insert into bayar (id_bayar, id_murid, tanggal, jumlah, metode) values ('" & Me.TextBox1.Text & "',
            '" & Me.ComboBox2.SelectedValue & "','" & Me.TextBox2.Text & "','" & Me.TextBox4.Text & "',
            '" & Me.ComboBox1.Text & "')"
            cmd = New MySqlCommand(simpan, con) 'untuk menghubungkan tabel ke database dan data lalu sistem
            cmd.ExecuteNonQuery() 'mengeksekusi data
            bersih()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If Me.ComboBox2.SelectedValue Is Nothing Or Me.TextBox4.Text = "" Or
            Me.ComboBox1.Text Is Nothing Or Me.TextBox2.Text = "" Then
            MsgBox("Maaf, tak ada data yang diedit", MsgBoxStyle.Exclamation)
        Else
            Dim edit As String = "UPDATE bayar SET id_murid = @murid, tanggal = @tgl, jumlah = @jmlh, metode = @metode WHERE id_bayar = @id"
            cmd = New MySqlCommand(edit, con)

            'menambahkan nilai parameter
            cmd.Parameters.AddWithValue("@id", TextBox1.Text)
            cmd.Parameters.AddWithValue("@murid", ComboBox2.SelectedValue)
            cmd.Parameters.AddWithValue("@tgl", TextBox2.Text)
            cmd.Parameters.AddWithValue("@jmlh", TextBox4.Text)
            cmd.Parameters.AddWithValue("@metode", ComboBox1.Text)

            Try
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil Disimpan!", vbInformation, "INFORMASI")
                bersih()
            Catch ex As MySqlException
                MsgBox("Menyimpan Data Gagal! Error: " & ex.Message, vbCritical, "Kesalahan")
            Finally
                con.Close() ' Menutup koneksi setelah selesai
            End Try

            bersih()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If Me.TextBox1.Text = "" Or Me.ComboBox2.SelectedValue Is Nothing Or Me.TextBox4.Text = "" Or
            Me.ComboBox1.Text Is Nothing Or TextBox2.Text = "" Then
            MsgBox("Maaf, tidak ada data yang dihapus")
        Else
            '------------coding hapus data--------------"
            If MessageBox.Show("Anda yakin akan menghapus data?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim hapus As String
                hapus = "delete from bayar where id_bayar ='" & Me.TextBox1.Text & "'"
                cmd = New MySqlCommand(hapus, con)
                cmd.ExecuteNonQuery()
                bersih()
            Else
            End If

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        bersih()
    End Sub

    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Me.Hide()
        Form1.Show()
    End Sub


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'fungsi untuk men-klik data yang akan dipilih
        Dim kodesd As String

        kodesd = DataGridView1.Rows(e.RowIndex).Cells("bayar").Value
        TextBox1.Text = kodesd
        ComboBox2.Text = DataGridView1.Rows(e.RowIndex).Cells("nama_murid").Value
        TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells("tanggal").Value
        TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells("jumlah").Value
        ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells("metode").Value

        Me.Button1.Enabled = False
        Me.Button2.Enabled = True
        Me.Button3.Enabled = True
        Me.Button4.Enabled = True
    End Sub
End Class