'import data mysql dari library VB.net

Imports MySql.Data.MySqlClient
Module Module1
    Public con As MySqlConnection
    Public cmd As MySqlCommand
    Public ds As DataSet
    Public da As MySqlDataAdapter
    Public rd As MySqlDataReader
    Public db As String

    Public Sub koneksi()
        db = "Server=localhost;user id=root;password=;database=data_uas"
        con = New MySqlConnection(db)
        Try
            If con.State = ConnectionState.Closed Then
                con.Open()
                MsgBox("koneksi ke database berhasil", MsgBoxStyle.Information, "informasi")
            End If
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Public Function SQLTABLE(ByVal source As String) As DataTable
        Try
            Dim adp As New MySqlDataAdapter(source, con)
            Dim DT As New DataTable
            adp.Fill(DT)
            SQLTABLE = DT
        Catch ex As MySqlException
            MsgBox(ex.Message)
            SQLTABLE = Nothing
        End Try
    End Function
End Module
