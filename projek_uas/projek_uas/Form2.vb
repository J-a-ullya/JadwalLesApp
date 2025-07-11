Public Class Form2
    Private Sub Dashboard_Click(sender As Object, e As EventArgs) Handles Dashboard.Click
        Form5.Hide()
        Form4.Hide()
        Form8.Hide()
        Form9.Hide()
        Form3.Show()
        Form3.MdiParent = Me
    End Sub

    Private Sub DataGuru_Click(sender As Object, e As EventArgs) Handles DataGuru.Click
        Form3.Hide()
        Form5.Hide()
        Form8.Hide()
        Form9.Hide()
        Form4.Show()
        Form4.MdiParent = Me
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Me.Hide()
        Form3.Hide()
        Form4.Hide()
        Form5.Hide()
        Form8.Hide()
        Form9.Hide()
        Form1.Show()

    End Sub

    Private Sub DataMurid_Click(sender As Object, e As EventArgs) Handles DataMurid.Click
        Form3.Hide()
        Form4.Hide()
        Form8.Hide()
        Form9.Hide()
        Form5.Show()
        Form5.MdiParent = Me
    End Sub

    Private Sub pembayaran_Click(sender As Object, e As EventArgs) Handles pembayaran.Click
        Form3.Hide()
        Form4.Hide()
        Form5.Hide()
        Form9.Hide()
        Form8.Show()
        Form8.MdiParent = Me
    End Sub

    Private Sub JadwalLesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JadwalLesToolStripMenuItem.Click
        Form3.Hide()
        Form4.Hide()
        Form5.Hide()
        Form8.Hide()
        Form9.Show()
        Form9.MdiParent = Me
    End Sub
End Class