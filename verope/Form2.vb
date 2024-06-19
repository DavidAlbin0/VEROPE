Imports System.IO
Imports System.Windows.Forms

Public Class Polizas
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Aquí puedes manejar los eventos de clic en las celdas si es necesario
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Try
                Dim startingRow As Integer = DataGridView1.CurrentCell.RowIndex
                Dim startingColumn As Integer = DataGridView1.CurrentCell.ColumnIndex

                Dim pasteText As String = Clipboard.GetText()
                Dim pasteRows() As String = pasteText.Split(New String() {Environment.NewLine}, StringSplitOptions.None)

                ' Obtener el número de filas y columnas para pegar
                Dim numRowsToPaste As Integer = pasteRows.Length
                Dim numColumnsToPaste As Integer = If(numRowsToPaste > 0, pasteRows(0).Split(vbTab).Length, 0)

                ' Agregar nuevas filas si es necesario
                If startingRow + numRowsToPaste > DataGridView1.Rows.Count Then
                    DataGridView1.Rows.Add(startingRow + numRowsToPaste - DataGridView1.Rows.Count)
                End If

                For i As Integer = 0 To numRowsToPaste - 1
                    Dim rowData() As String = pasteRows(i).Split(New String() {vbTab}, StringSplitOptions.None)
                    For j As Integer = 0 To Math.Min(rowData.Length - 1, numColumnsToPaste - 1)
                        If startingRow + i < DataGridView1.RowCount AndAlso startingColumn + j < DataGridView1.ColumnCount Then
                            DataGridView1.Rows(startingRow + i).Cells(startingColumn + j).Value = rowData(j)
                        End If
                    Next
                Next
            Catch ex As Exception
                MessageBox.Show("Error al pegar los datos desde el portapapeles: " & ex.Message)
            End Try

            e.Handled = True
        End If
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

    ' Método para guardar los datos del DataGridView en un archivo XML
    Private Sub GuardarDataGridViewEnXML(dataGridView As DataGridView)
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Archivos XML (*.xml)|*.xml|Todos los archivos (*.*)|*.*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = saveFileDialog1.FileName
            Dim dataSet As New DataSet("DataGridViewData")
            Dim dataTable As New DataTable("Row")

            ' Crear las columnas en el DataTable
            For Each column As DataGridViewColumn In dataGridView.Columns
                dataTable.Columns.Add(column.Name, GetType(String))
            Next

            ' Añadir las filas del DataGridView al DataTable
            For Each row As DataGridViewRow In dataGridView.Rows
                If Not row.IsNewRow Then
                    Dim dataRow As DataRow = dataTable.NewRow()
                    For columnIndex As Integer = 0 To dataGridView.Columns.Count - 1
                        Dim cellValue As Object = row.Cells(columnIndex).Value
                        If cellValue IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cellValue.ToString()) Then
                            ' Limpiar el valor de la celda para eliminar comas y símbolos de dólar
                            Dim cleanedValue As String = cellValue.ToString().Replace(",", "").Replace("$", "").Trim()
                            dataRow(columnIndex) = cleanedValue
                        Else
                            dataRow(columnIndex) = DBNull.Value
                        End If
                    Next
                    dataTable.Rows.Add(dataRow)
                End If
            Next

            ' Añadir el DataTable al DataSet
            dataSet.Tables.Add(dataTable)

            ' Guardar el DataSet en un archivo XML
            dataSet.WriteXml(filePath, XmlWriteMode.WriteSchema)

            ' Informar al usuario que el archivo se guardó correctamente
            MessageBox.Show("Archivo guardado exitosamente.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' BOTON PARA GUARDAR LA GRID 2, LA DE LA DERECHA
    Private Sub PictureBox5_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox5.MouseDown
        ' Mueve la imagen un poco hacia abajo y a la derecha
        PictureBox5.Location = New Point(PictureBox5.Location.X + 2, PictureBox5.Location.Y + 2)
    End Sub

    Private Sub PictureBox5_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox5.MouseUp
        ' Restaura la ubicación original de la imagen
        PictureBox5.Location = New Point(PictureBox5.Location.X - 2, PictureBox5.Location.Y - 2)
        ' Ejecuta el código para guardar el DataGridView en CSV
        GuardarDataGridViewEnXML(DataGridView1)
    End Sub

    Private Function FormatearComoContabilidad(valor As String) As String
        ' Verificar si el valor contiene un punto
        Dim tienePunto As Boolean = valor.Contains(".")

        ' Si el valor no tiene un punto, formatearlo como entero con formato de contabilidad
        If Not tienePunto Then
            ' Si el valor es un número válido, formatearlo
            Dim valorDecimal As Decimal
            If Decimal.TryParse(valor, valorDecimal) Then
                ' Utilizar el formato con comas
                Return "$ " & valorDecimal.ToString("#,#0").Replace(".", ",")
            Else
                ' Si no se puede convertir a decimal, devolver el valor original
                Return valor
            End If
        Else

            ' Si el valor tiene un punto, formatearlo con comas para separar miles a partir del tercer dígito antes del punto decimal
            Dim partes As String() = valor.Split(".")
            Dim parteEntera As String = partes(0)
            Dim parteDecimal As String = partes(1)

            ' Formatear la parte entera
            Dim parteEnteraFormateada As String = ""
            Dim contador As Integer = 0

            For i As Integer = parteEntera.Length - 1 To 0 Step -1
                If Char.IsDigit(parteEntera(i)) Then
                    parteEnteraFormateada = parteEntera(i) & parteEnteraFormateada
                    contador += 1
                    If contador Mod 3 = 0 AndAlso i > 0 AndAlso Char.IsDigit(parteEntera(i - 1)) Then
                        parteEnteraFormateada = "," & parteEnteraFormateada
                    End If
                Else
                    parteEnteraFormateada = parteEntera(i) & parteEnteraFormateada
                End If
            Next

            Return "$" & parteEnteraFormateada & "." & parteDecimal
        End If
    End Function

    Private Sub dataGridView_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs)
        ' Dibujar el número de fila en el encabezado de fila
        Dim grid As DataGridView = CType(sender, DataGridView)
        Dim rowIdx As String = (e.RowIndex + 1).ToString()
        Dim centerFormat As New StringFormat() With {
    .Alignment = StringAlignment.Center,
    .LineAlignment = StringAlignment.Center
}

        ' Determine la ubicación del encabezado de fila basándose en la DataGridView
        Dim headerBounds As Rectangle
        If grid Is DataGridView1 Then
            headerBounds = New Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height)
        Else
            Exit Sub
        End If

        e.Graphics.DrawString(rowIdx, grid.Font, SystemBrushes.ControlText, headerBounds, centerFormat)
    End Sub

    ' Método para cargar datos desde un archivo XML en el DataGridView
    Private Sub CargarDatosEnDataGridView(filePath As String, dataGridView As DataGridView)
        ' Limpiar las columnas existentes en el DataGridView antes de cargar los archivos
        dataGridView.Columns.Clear()
        ' Limpiar las filas existentes en el DataGridView antes de cargar los archivos
        dataGridView.Rows.Clear()

        ' Asegurar que el encabezado de fila está visible
        dataGridView.RowHeadersVisible = True

        Try
            ' Crear un DataSet para almacenar los datos del archivo XML
            Dim dataSet As New DataSet()

            ' Leer el archivo XML en el DataSet
            dataSet.ReadXml(filePath)

            ' Verificar si el DataSet contiene alguna tabla
            If dataSet.Tables.Count > 0 Then
                ' Obtener la primera tabla del DataSet
                Dim dataTable As DataTable = dataSet.Tables(0)

                ' Añadir las columnas al DataGridView
                For Each column As DataColumn In dataTable.Columns
                    dataGridView.Columns.Add(column.ColumnName, column.ColumnName)
                Next

                ' Añadir las filas al DataGridView
                For Each row As DataRow In dataTable.Rows
                    ' Convertir la fila en un array de objetos
                    Dim rowValues As Object() = row.ItemArray

                    ' Verificar y formatear el tercer campo si es necesario
                    If rowValues.Length > 2 Then
                        rowValues(2) = FormatearComoContabilidad(rowValues(2).ToString())
                    End If

                    ' Agregar la fila al DataGridView
                    dataGridView.Rows.Add(rowValues)
                Next

                ' Ajustar el tamaño de las columnas para que se ajusten al contenido
                dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

                ' Manejar el evento RowPostPaint para dibujar los números de fila
                AddHandler dataGridView.RowPostPaint, AddressOf dataGridView_RowPostPaint
            Else
                ' Si el archivo no contiene datos, mostrar un mensaje de advertencia
                MessageBox.Show("El archivo no contiene datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PictureBox6_MouseDown(sender As Object, e As EventArgs) Handles PictureBox6.MouseDown
        PictureBox6.Location = New Point(PictureBox6.Location.X - 2, PictureBox6.Location.Y - 2)

    End Sub
    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        ' Crear una instancia del OpenFileDialog
        Dim openFileDialog1 As New OpenFileDialog()
        PictureBox6.Location = New Point(PictureBox6.Location.X + 2, PictureBox6.Location.Y + 2)


        ' Configurar las propiedades del OpenFileDialog
        openFileDialog1.InitialDirectory = "C:\" ' Carpeta inicial
        openFileDialog1.Filter = "Todos los archivos (*.*)|*.*|Archivos de texto (*.txt)|*.txt" ' Permitir todos los tipos de archivo
        openFileDialog1.FilterIndex = 1 ' Índice del filtro predeterminado
        openFileDialog1.RestoreDirectory = True ' Restaurar el directorio actual si el usuario cambia el directorio

        ' Mostrar el OpenFileDialog y verificar si el usuario seleccionó un archivo
        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Obtener la ruta del archivo seleccionado
            Dim selectedFilePath As String = openFileDialog1.FileName

            ' Llamar a la función para cargar los datos en DataGridView2
            CargarDatosEnDataGridView(selectedFilePath, DataGridView1)

            ' Mostrar un mensaje indicando que el archivo se cargó correctamente
            MessageBox.Show("Archivo cargado correctamente en DataGridView2.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

End Class
