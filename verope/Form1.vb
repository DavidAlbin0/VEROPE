Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Form1

    Private WithEvents searchTimer As New Timer()

    ' Estructuras definidas para los RANDOM
    Structure CAT_MA
        <VBFixedString(6), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=6)> Public B1 As String
        <VBFixedString(32), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> Public B2 As String
        <VBFixedString(16), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=16)> Public B3 As String
        <VBFixedString(5), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=5)> Public B4 As String
        <VBFixedString(5), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=5)> Public B5 As String
    End Structure

    Structure CAT_AX
        <VBFixedString(6), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=6)> Public C1 As String
        <VBFixedString(32), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> Public C2 As String
        <VBFixedString(16), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=16)> Public C3 As String
        <VBFixedString(5), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=5)> Public C4 As String
        <VBFixedString(5), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=5)> Public C5 As String
    End Structure

    ' Aqui se le da formato xdxd
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


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configurar el timer
        searchTimer.Interval = 500 ' 500 ms = 0.5 segundos
    End Sub

    ' Método para cargar datos de archivos de acceso aleatorio en el DataGridView 1
    Private Sub CargarDatosEnDataGridView(filePath As String, dataGridView As DataGridView)
        ' Limpiar las columnas existentes en el DataGridView antes de cargar los archivos
        dataGridView.Columns.Clear()
        ' Limpiar las filas existentes en el DataGridView antes de cargar los archivos
        dataGridView.Rows.Clear()

        ' Asegurar que el encabezado de fila está visible
        dataGridView.RowHeadersVisible = True

        Try
            ' Leer todas las líneas del archivo
            Dim lines As String() = File.ReadAllLines(filePath)

            ' Si hay al menos una línea en el archivo
            If lines.Length > 0 Then
                ' Definir las columnas basadas en el número de campos esperados
                For i As Integer = 1 To 5
                    dataGridView.Columns.Add($"B{i}", $"B{i}")
                Next

                ' Iterar sobre cada línea del archivo
                For Each line As String In lines
                    ' Dividir la línea en campos utilizando el tabulador como delimitador
                    Dim fields As String() = line.Split(New Char() {vbTab}, StringSplitOptions.RemoveEmptyEntries)

                    ' Verificar que haya suficientes campos para llenar todas las columnas
                    If fields.Length >= 5 Then
                        ' Agregar una fila al DataGridView con los campos de esta línea
                        dataGridView.Rows.Add(fields(0), fields(1), FormatearComoContabilidad(fields(2)), fields(3), fields(4))
                    Else
                        ' Si no hay suficientes campos, agregar celdas vacías
                        Dim emptyFields As String() = {"", "", "", "", ""}
                        dataGridView.Rows.Add(emptyFields)
                    End If
                Next

                ' Ajustar el tamaño de las columnas para que se ajusten al contenido
                dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

                ' Manejar el evento RowPostPaint para dibujar los números de fila
                AddHandler dataGridView.RowPostPaint, AddressOf dataGridView_RowPostPaint
            Else
                ' Si el archivo está vacío, mostrar un mensaje de advertencia
                MessageBox.Show("El archivo está vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


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
        ElseIf grid Is DataGridView2 Then
            headerBounds = New Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height)
        Else
            Exit Sub
        End If

        e.Graphics.DrawString(rowIdx, grid.Font, SystemBrushes.ControlText, headerBounds, centerFormat)
    End Sub

    ' Método para guardar los datos del DataGridView en un archivo CSV
    Private Sub GuardarDataGridViewEnCSV(dataGridView As DataGridView)
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Archivos CSV (*.csv)|*.csv|Todos los archivos (*.*)|*.*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = saveFileDialog1.FileName
            Dim csvContent As New StringBuilder()

            ' Determinar la longitud máxima de cada columna
            Dim columnLengths(dataGridView.Columns.Count - 1) As Integer
            For Each row As DataGridViewRow In dataGridView.Rows
                If Not row.IsNewRow Then
                    For columnIndex As Integer = 0 To dataGridView.Columns.Count - 1
                        Dim cellValue As String = If(row.Cells(columnIndex).Value IsNot Nothing, row.Cells(columnIndex).Value.ToString(), "")
                        ' Remover los símbolos "$" y ","
                        cellValue = cellValue.Replace("$", "").Replace(",", "")
                        columnLengths(columnIndex) = Math.Max(columnLengths(columnIndex), cellValue.Length)
                    Next
                End If
            Next

            ' Escribir las filas y columnas del DataGridView en el archivo CSV
            For Each row As DataGridViewRow In dataGridView.Rows
                If Not row.IsNewRow Then
                    For columnIndex As Integer = 0 To dataGridView.Columns.Count - 1
                        Dim cellValue As String = If(row.Cells(columnIndex).Value IsNot Nothing, row.Cells(columnIndex).Value.ToString(), "")
                        ' Remover los símbolos "$" y ","
                        cellValue = cellValue.Replace("$", "").Replace(",", "")
                        ' Alinear el valor a la derecha y rellenar con espacios en blanco para que tenga la longitud máxima de la columna
                        csvContent.Append(cellValue.PadRight(columnLengths(columnIndex)))
                        csvContent.Append(vbTab) ' Agregar un tabulador para separar las celdas (o cualquier otro carácter deseado)
                    Next

                    ' Agregar un salto de línea al final de cada fila
                    csvContent.AppendLine()
                End If
            Next

            ' Escribir el contenido al archivo utilizando la codificación UTF-8 sin BOM
            Using sw As New StreamWriter(filePath, False, New UTF8Encoding(False))
                sw.Write(csvContent.ToString())
            End Using

            ' Informar al usuario que el archivo se guardó correctamente
            MessageBox.Show("Archivo guardado exitosamente.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub



    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        ' Finalizar la edición en DataGridView1 y DataGridView2 para evitar el error
        DataGridView1.EndEdit()
        DataGridView2.EndEdit()

        ' Reiniciar el timer
        searchTimer.Stop()
        searchTimer.Start()
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
        GuardarDataGridViewEnCSV(DataGridView2)
    End Sub


    ' BOTON PARA BORRAR EL CONTENIDO DE LA GRID 2
    Private Sub PictureBox4_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox4.MouseDown
        ' Mueve la imagen un poco hacia abajo y a la derecha
        PictureBox4.Location = New Point(PictureBox4.Location.X + 2, PictureBox4.Location.Y + 2)
    End Sub

    Private Sub PictureBox4_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox4.MouseUp
        ' Restaura la ubicación original de la imagen
        PictureBox4.Location = New Point(PictureBox4.Location.X - 2, PictureBox4.Location.Y - 2)
        ' Limpia todas las filas y columnas del DataGridView
        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Clear()
    End Sub

    ' BOTON PARA BORRAR EL CONTENIDO DE LA GRID 1
    Private Sub PictureBox3_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox3.MouseDown
        ' Mueve la imagen un poco hacia abajo y a la derecha
        PictureBox3.Location = New Point(PictureBox3.Location.X + 2, PictureBox3.Location.Y + 2)
    End Sub

    Private Sub PictureBox3_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox3.MouseUp
        ' Restaura la ubicación original de la imagen
        PictureBox3.Location = New Point(PictureBox3.Location.X - 2, PictureBox3.Location.Y - 2)
        ' Limpia todas las filas y columnas del DataGridView
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
    End Sub

    ' BOTON PARA COPIAR EL CONTENIDO DE LA GRID 1 A LA GRID 2
    Private Sub PictureBox2_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseDown
        ' Mueve la imagen un poco hacia abajo y a la derecha
        PictureBox2.Location = New Point(PictureBox2.Location.X + 2, PictureBox2.Location.Y + 2)
    End Sub

    Private Sub PictureBox2_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseUp
        ' Restaura la ubicación original de la imagen
        PictureBox2.Location = New Point(PictureBox2.Location.X - 2, PictureBox2.Location.Y - 2)
        ' Copia el contenido del DataGridView1 al DataGridView2
        CopiarContenidoDataGridView(DataGridView1, DataGridView2)
    End Sub

    ' Método para copiar el contenido de un DataGridView a otro
    Private Sub CopiarContenidoDataGridView(sourceDataGridView As DataGridView, targetDataGridView As DataGridView)
        ' Limpiar las columnas y filas existentes en el DataGridView de destino
        targetDataGridView.Columns.Clear()
        targetDataGridView.Rows.Clear()

        ' Copiar las columnas
        For Each column As DataGridViewColumn In sourceDataGridView.Columns
            targetDataGridView.Columns.Add(CType(column.Clone(), DataGridViewColumn))
        Next

        ' Copiar las filas
        For Each row As DataGridViewRow In sourceDataGridView.Rows
            If Not row.IsNewRow Then
                Dim newRow As DataGridViewRow = CType(row.Clone(), DataGridViewRow)
                For i As Integer = 0 To row.Cells.Count - 1
                    newRow.Cells(i).Value = row.Cells(i).Value
                Next
                targetDataGridView.Rows.Add(newRow)
            End If
        Next
    End Sub

    ' Manejador del evento click para PictureBox1
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ' Copiar el contenido de DataGridView2 a DataGridView1
        CopiarContenidoDataGridView(DataGridView2, DataGridView1)
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

    Private Sub PictureBox7_MouseDown(sender As Object, e As EventArgs) Handles PictureBox7.MouseDown
        PictureBox7.Location = New Point(PictureBox7.Location.X + 2, PictureBox7.Location.Y + 2)
        PictureBox7.Location = New Point(PictureBox7.Location.X - 2, PictureBox7.Location.Y - 2)


    End Sub
    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        ' Crear una instancia del OpenFileDialog
        Dim openFileDialog1 As New OpenFileDialog()

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
            CargarDatosEnDataGridView(selectedFilePath, DataGridView2)

            ' Mostrar un mensaje indicando que el archivo se cargó correctamente
            MessageBox.Show("Archivo cargado correctamente en DataGridView2.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub



    Private Sub PictureBox8_MouseDown(sender As Object, e As EventArgs) Handles PictureBox8.MouseDown
        PictureBox8.Location = New Point(PictureBox8.Location.X + 2, PictureBox8.Location.Y + 2)
        PictureBox8.Location = New Point(PictureBox8.Location.X - 2, PictureBox8.Location.Y - 2)

    End Sub
    Private currentPage As Integer = 0

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        ' Crear un objeto PrintDocument
        Dim printDocument As New Printing.PrintDocument()

        ' Asignar el control DataGridView2 al PrintDocument para que se imprima
        AddHandler printDocument.PrintPage, AddressOf PrintDocument_PrintPage

        ' Mostrar el cuadro de diálogo de impresión
        Dim printDialog As New PrintDialog()
        printDialog.Document = printDocument
        If printDialog.ShowDialog() = DialogResult.OK Then
            currentPage = 0 ' Restablecer el contador de páginas
            printDocument.Print()
        End If
    End Sub

    Private Sub PrintDocument_PrintPage(sender As Object, e As Printing.PrintPageEventArgs)
        ' Definir el área de impresión
        Dim printArea As RectangleF = e.PageSettings.PrintableArea
        Dim printWidth As Single = printArea.Width
        Dim printHeight As Single = printArea.Height

        ' Definir el rectángulo de destino para dibujar el contenido de la DataGridView
        Dim destinationRect As New RectangleF(printArea.Left, printArea.Top, printWidth, printHeight)

        ' Dibujar el contenido de la DataGridView en la página
        Dim rowsPerPage As Integer = CInt(Math.Floor(printHeight / DataGridView2.RowTemplate.Height))
        Dim rowIndex As Integer = currentPage * rowsPerPage
        Dim rowsToPrint As Integer = Math.Min(rowsPerPage, DataGridView2.Rows.Count - rowIndex)

        If rowsToPrint > 0 Then
            ' Dibujar el encabezado de la DataGridView
            Dim headerRect As New RectangleF(destinationRect.Left, destinationRect.Top, destinationRect.Width, DataGridView2.ColumnHeadersHeight)
            DrawHeader(DataGridView2, e.Graphics, headerRect)

            ' Dibujar las filas de la DataGridView
            Dim bodyRect As New RectangleF(destinationRect.Left, destinationRect.Top + DataGridView2.ColumnHeadersHeight, destinationRect.Width, DataGridView2.RowTemplate.Height * rowsToPrint)
            DrawRows(DataGridView2, e.Graphics, bodyRect, rowIndex, rowsToPrint)

            ' Indicar que hay más páginas si quedan filas por imprimir
            rowIndex += rowsToPrint
            If rowIndex < DataGridView2.Rows.Count Then
                e.HasMorePages = True
                currentPage += 1
            End If
        End If
    End Sub

    Private Sub DrawHeader(dataGridView As DataGridView, graphics As Graphics, headerRect As RectangleF)
        ' Calcular el ancho de cada columna para asegurar que quepan todas las columnas en la página
        Dim columnWidth As Single = headerRect.Width / dataGridView.Columns.Count

        For Each column As DataGridViewColumn In dataGridView.Columns
            Dim cellRect As New RectangleF(headerRect.Left + column.DisplayIndex * columnWidth, headerRect.Top, columnWidth, headerRect.Height)
            graphics.DrawString(column.HeaderText, dataGridView.ColumnHeadersDefaultCellStyle.Font, Brushes.Black, cellRect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            graphics.DrawRectangle(Pens.Black, cellRect.Left, cellRect.Top, cellRect.Width, cellRect.Height)
        Next
    End Sub

    Private Sub DrawRows(dataGridView As DataGridView, graphics As Graphics, bodyRect As RectangleF, rowIndex As Integer, rowCount As Integer)
        ' Calcular el ancho de cada columna para asegurar que quepan todas las columnas en la página
        Dim columnWidth As Single = bodyRect.Width / dataGridView.Columns.Count

        For i As Integer = 0 To rowCount - 1
            Dim currentRow As DataGridViewRow = dataGridView.Rows(rowIndex + i)
            Dim rowRect As New RectangleF(bodyRect.Left, bodyRect.Top + i * dataGridView.RowTemplate.Height, bodyRect.Width, dataGridView.RowTemplate.Height)

            For Each cell As DataGridViewCell In currentRow.Cells
                Dim cellRect As New RectangleF(rowRect.Left + cell.ColumnIndex * columnWidth, rowRect.Top, columnWidth, rowRect.Height)
                graphics.DrawString(cell.FormattedValue.ToString(), dataGridView.DefaultCellStyle.Font, Brushes.Black, cellRect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                graphics.DrawRectangle(Pens.Black, cellRect.Left, cellRect.Top, cellRect.Width, cellRect.Height)
            Next
        Next
    End Sub
    Private Sub BorrarSaldosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BorrarSaldosToolStripMenuItem.Click
        ' Itera a través de cada fila en el DataGridView
        For Each fila As DataGridViewRow In DataGridView2.Rows
            ' Verifica si la celda está vacía
            If fila.Cells(2).Value Is Nothing OrElse String.IsNullOrWhiteSpace(fila.Cells(2).Value.ToString()) Then
                ' Si la celda está vacía, no hacer nada
                Continue For
            Else
                ' Si la celda no está vacía, establecer el valor a "$ 0"
                fila.Cells(2).Value = "$ 0"
            End If
        Next
    End Sub


End Class
