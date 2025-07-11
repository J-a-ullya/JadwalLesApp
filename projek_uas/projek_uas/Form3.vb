
Imports MySql.Data.MySqlClient

Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()

        Me.ContextMenuStrip = ContextMenuStrip1
        Label1.Text = "No"
        Label2.Text = "Hari"
        Label3.Text = "Jam"
        Label4.Text = "Guru"
        Label5.Text = "Murid"
        Label6.Text = "Materi"
        Label7.Text = "Jadwal Les Privat Bahasa Jepang"
        Button1.Text = "Simpan"
        Button2.Text = "Edit"
        Button3.Text = "Hapus Data"
        Button4.Text = "Hapus"


        TextBox1.ReadOnly = True 'kode untuk data di textbox1 tidak diotak-atik

        'data combobox hari
        Dim hari(6) As String
        hari(0) = "Senin"
        hari(1) = "Selasa"
        hari(2) = "Rabu"
        hari(3) = "Kamis"
        hari(4) = "Jumat"
        hari(5) = "Sabtu"
        hari(6) = "Minggu"
        'looping untuk mengeluarkan data array
        For Each element As String In hari
            ComboBox1.Items.Add(element)
        Next

        'data combobox jam
        Dim jam(4) As String
        jam(0) = "13.00-14.30"
        jam(1) = "15.00-16.30"
        jam(2) = "17.00-18.30"
        jam(3) = "19.00-20.30"
        jam(4) = "21.00-22.30"
        'looping untuk mengeluarkan data array
        For Each elemen As String In jam
            ComboBox2.Items.Add(elemen)
        Next


        bersih() 'prosedur untuk membersihkan objek

        ComboBox()
    End Sub

    Sub combobox()
        'data guru
        Dim dsguru As New DataSet
        Dim daguru As New MySqlDataAdapter("select id_guru, nama from data_guru", con)
        daguru.Fill(dsguru, "data_guru")
        ComboBox3.DataSource = dsguru.Tables("data_guru")
        ComboBox3.DisplayMember = "nama"
        ComboBox3.ValueMember = "id_guru"

        'data murid
        Dim dsmurid As New DataSet
        Dim damurid As New MySqlDataAdapter("select id_murid, nama from data_murid", con)
        damurid.Fill(dsmurid, "data_murid")
        ComboBox4.DataSource = dsmurid.Tables("data_murid")
        ComboBox4.DisplayMember = "nama"
        ComboBox4.ValueMember = "id_murid"
    End Sub
    Sub bersih()
        TextBox1.Text = ""

        'untuk mematikan tombol
        Me.Button1.Enabled = True
        Me.Button2.Enabled = False
        Me.Button4.Enabled = False

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

        nomor()
    End Sub

    Sub nomor()
        Dim DR As DataRow
        Dim s As String
        'mengambil kode dari data flied id, kemudian dicari nilai yang paling besar (max)
        'kemudian hasilnya ditampung di field buatan dengan nomor
        DR = SQLTABLE("select max(right(id_jadwal, 1)) as nomor from jadwal").Rows(0)

        'jika berisi null atau tidak ditemukan
        If DR.IsNull("nomor") Then
            s = "JP-001" 'member nilai awal
        Else
            s = "JP-" & Format(DR("nomor") + 1, "000")
        End If
        TextBox1.Text = s
        TextBox1.Enabled = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Me.ComboBox1.SelectedItem Is Nothing Or ComboBox2.SelectedItem Is Nothing Or ComboBox3.SelectedValue Is Nothing Or
            ComboBox4.SelectedValue Is Nothing Or Me.TextBox2.Text = "" Then
            MsgBox("Pastikan data terisi semua", MsgBoxStyle.Exclamation)

        Else

            Dim simpan As String = "INSERT INTO jadwal (id_jadwal, hari, jam, id_guru, id_murid, materi, status) 
                        VALUES (@id, @hari, @jam, @guru, @murid, @materi, 'belum')"

            cmd = New MySqlCommand(simpan, con)
            cmd.Parameters.AddWithValue("@id", TextBox1.Text)
            cmd.Parameters.AddWithValue("@hari", ComboBox1.Text)
            cmd.Parameters.AddWithValue("@jam", ComboBox2.Text)
            cmd.Parameters.AddWithValue("@guru", ComboBox3.SelectedValue)
            cmd.Parameters.AddWithValue("@murid", ComboBox4.SelectedValue)
            cmd.Parameters.AddWithValue("@materi", TextBox2.Text)

            If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.ComboBox1.SelectedItem Is Nothing Or ComboBox2.SelectedValue Is Nothing Or ComboBox3.SelectedValue Is Nothing Or
            ComboBox4.SelectedValue Is Nothing Or Me.TextBox2.Text = "" Then
            MsgBox("Pastikan data terisi semua", MsgBoxStyle.Exclamation)

        Else
            Dim edit As String
            MsgBox("Data di-update")
            edit = "update jadwal set hari ='" & Me.ComboBox1.Text & "', jam ='" & Me.ComboBox2.Text & "',
            id_guru ='" & Me.ComboBox3.SelectedValue & "', id_murid ='" & ComboBox4.SelectedValue & "', materi ='" & Me.TextBox2.Text & "'where
            id_jadwal ='" & Me.TextBox1.Text & "'"

            cmd = New MySqlCommand(edit, con) 'untuk menghubungkan ke database dan tabel lalu update
            cmd.ExecuteNonQuery() 'mengeksekusi datanya
            bersih()
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Me.ComboBox1.SelectedItem Is Nothing Or ComboBox2.SelectedItem Is Nothing Or ComboBox3.SelectedValue Is Nothing Or
            ComboBox4.SelectedValue Is Nothing Or Me.TextBox2.Text = "" Then
            MsgBox("Maaf, tidak ada data yang dihapus")
        Else
            '------------coding hapus data--------------"
            If MessageBox.Show("Anda yakin akan menghapus data?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim hapus As String
                hapus = "delete from jadwal where id_jadwal ='" & Me.TextBox1.Text & "'"
                cmd = New MySqlCommand(hapus, con)
                cmd.ExecuteNonQuery()
                bersih()
            Else
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MessageBox.Show("Yakin ingin menghapus semua data jadwal?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Dim hapusSemua As String = "DELETE FROM jadwal"
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
        ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        ComboBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        ComboBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        ComboBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value

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
        Form5.Hide()
        Form2.Hide()
        Form1.Show()
    End Sub
End Class