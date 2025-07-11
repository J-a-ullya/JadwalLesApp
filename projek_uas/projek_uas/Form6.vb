Public Class Form6


    Private Sub LogoutToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem1.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub jadwalles_Click(sender As Object, e As EventArgs) Handles jadwalles.Click
        Form7.Show()
        Form7.MdiParent = Me
    End Sub
End Class