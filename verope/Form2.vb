Imports System.IO
Imports System.Windows.Forms

Public Class Polizas
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Aquí puedes manejar los eventos de clic en las celdas si es necesario
    End Sub

    Private Sub SubirPolizaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubirPolizaToolStripMenuItem.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
        openFileDialog.Title = "Seleccione el archivo de pólizas"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                Dim filePath As String = openFileDialog.FileName
                Dim csvData As DataTable = GetDataTableFromCSV(filePath)
                DataGridView1.DataSource = csvData
            Catch ex As Exception
                MessageBox.Show("Error al cargar el archivo: " & ex.Message)
            End Try
        End If
    End Sub




    Private Function GetDataTableFromCSV(filePath As String) As DataTable
        Dim dt As New DataTable()
        Try
            Using sr As New StreamReader(filePath)
                Dim headers As String() = sr.ReadLine().Split(","c)
                For Each header As String In headers
                    dt.Columns.Add(header)
                Next

                While Not sr.EndOfStream
                    Dim rows As String() = sr.ReadLine().Split(","c)
                    dt.Rows.Add(rows)
                End While
            End Using
        Catch ex As Exception
            Throw New Exception("Error al leer el archivo CSV: " & ex.Message)
        End Try
        Return dt
    End Function
End Class
