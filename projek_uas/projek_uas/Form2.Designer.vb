<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Dashboard = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataGuru = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataMurid = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogoutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.pembayaran = New System.Windows.Forms.ToolStripMenuItem()
        Me.JadwalLesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem, Me.JadwalLesToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 28)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Dashboard, Me.DataGuru, Me.DataMurid, Me.LogoutToolStripMenuItem, Me.pembayaran})
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(60, 24)
        Me.MenuToolStripMenuItem.Text = "Menu"
        '
        'Dashboard
        '
        Me.Dashboard.Name = "Dashboard"
        Me.Dashboard.Size = New System.Drawing.Size(173, 26)
        Me.Dashboard.Text = "Dashboard"
        '
        'DataGuru
        '
        Me.DataGuru.Name = "DataGuru"
        Me.DataGuru.Size = New System.Drawing.Size(173, 26)
        Me.DataGuru.Text = "Data Guru"
        '
        'DataMurid
        '
        Me.DataMurid.Name = "DataMurid"
        Me.DataMurid.Size = New System.Drawing.Size(173, 26)
        Me.DataMurid.Text = "Data Murid"
        '
        'LogoutToolStripMenuItem
        '
        Me.LogoutToolStripMenuItem.Name = "LogoutToolStripMenuItem"
        Me.LogoutToolStripMenuItem.Size = New System.Drawing.Size(173, 26)
        Me.LogoutToolStripMenuItem.Text = "Logout"
        '
        'pembayaran
        '
        Me.pembayaran.Name = "pembayaran"
        Me.pembayaran.Size = New System.Drawing.Size(173, 26)
        Me.pembayaran.Text = "Pembayaran"
        '
        'JadwalLesToolStripMenuItem
        '
        Me.JadwalLesToolStripMenuItem.Name = "JadwalLesToolStripMenuItem"
        Me.JadwalLesToolStripMenuItem.Size = New System.Drawing.Size(93, 24)
        Me.JadwalLesToolStripMenuItem.Text = "Jadwal Les"
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.projek_uas.My.Resources.Resources.Situs_Web_Tautan_Bio_Kesehatan_dan_Kebugaran_UI_Warna_warni_Kuning
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.MenuStrip1)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form2"
        Me.Text = "Form2"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents MenuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataGuru As ToolStripMenuItem
    Friend WithEvents DataMurid As ToolStripMenuItem
    Friend WithEvents Dashboard As ToolStripMenuItem
    Friend WithEvents LogoutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents pembayaran As ToolStripMenuItem
    Friend WithEvents JadwalLesToolStripMenuItem As ToolStripMenuItem
End Class
