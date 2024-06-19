Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Diagnostics
Imports System.Runtime.InteropServices.ComTypes
Imports System.Data
Imports System.Windows.Forms
Imports System.Xml

Public Class Form1

    Private WithEvents searchTimer As New Timer()

    ' Estructuras definidas para los RANDOM
    Structure CAT_MA
        <VBFixedString(6), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=6)> Public B1 As String
        <VBFixedString(32), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=32)> Public B2 As String
        <VBFixedString(16), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=16)> Public B3 As String
        <VBFixedString(5), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=5)> Public B4 As String
        <VBFixedString(5), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=5)> Public B5 As String
    End Structure
    Structure Ope
        <VBFixedString(6), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=6)> Public CTA As String
        <VBFixedString(30), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=30)> Public reda As String
        <VBFixedString(2), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=2)> Public fecha As String
        <VBFixedString(16), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=16)> Public impo As String
        <VBFixedString(1), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=1)> Public clav As String
        <VBFixedString(9), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=9)> Public real As String
    End Structure
    Structure Otr
        <VBFixedString(7), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=7)> Public CTA As String
        <VBFixedString(30), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=30)> Public reda As String
        <VBFixedString(2), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=2)> Public fecha As String
        <VBFixedString(16), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=16)> Public impo As String
        <VBFixedString(1), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=1)> Public clav As String
        <VBFixedString(9), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=9)> Public real As String
    End Structure
    Structure DAT_OS
        <VBFixedString(64), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=64)> Public D1 As String
        <VBFixedString(60), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=60)> Public D2 As String
        <VBFixedString(45), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=45)> Public D3 As String
        <VBFixedString(15), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=15)> Public No_arch As String
        <VBFixedString(5), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=5)> Public a_o As String
        <VBFixedString(25), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=25)> Public others1 As String
        <VBFixedString(5), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=5)> Public ultimaPol1 As String
        <VBFixedString(5), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=5)> Public ultimaOperacionReg As String
        <VBFixedString(12), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=12)> Public others As String
    End Structure
    Public Structure Oper_aciones
        <VBFixedString(6), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=6)> Public CTA As String
        <VBFixedString(30), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=30)> Public descr As String
        <VBFixedString(2), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=2)> Public fe As String
        <VBFixedString(16), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=16)> Public impte As String
        <VBFixedString(2), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=2)> Public indenti As String
        <VBFixedString(8), MarshalAs(UnmanagedType.ByValTStr, SizeConst:=8)> Public real As String
    End Structure

    Structure CAT_AX
        <VBFixedString(6), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=6)> Public C1 As String
        <VBFixedString(32), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=32)> Public C2 As String
        <VBFixedString(16), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=16)> Public C3 As String
        <VBFixedString(5), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=5)> Public C4 As String
        <VBFixedString(5), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=5)> Public C5 As String
    End Structure
    Structure Per
        <VBFixedString(20), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=20)> Public nom As String
        <VBFixedString(20), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=20)> Public ape1 As String
        <VBFixedString(20), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=20)> Public ape2 As String
        <VBFixedString(18), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=18)> Public RFC As String
        <VBFixedString(18), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=18)> Public imss As String
        <VBFixedString(12), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=12)> Public fal As String
        <VBFixedString(12), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=12)> Public fab As String
        Public ingr As Long
        Public viat As Long
        Public otras As Long
        Public integrado As Long
    End Structure
    Structure ContCat
        <VBFixedString(5), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=5)> Public g1 As String
        <VBFixedString(5), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=5)> Public g2 As String
        <VBFixedString(6), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=6)> Public g3 As String
    End Structure
    Structure Basini
        <VBFixedString(255), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=255)> Public datoArch As String
    End Structure
    Structure Fol_io
        <VBFixedString(36), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=36)> Public fol As String
    End Structure
    Structure OtrasCh
        <VBFixedString(30), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=30)> Public CURP As String
        <VBFixedString(30), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=30)> Public otra As String
        <VBFixedString(30), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=30)> Public yotra As String
        <VBFixedString(30), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=30)> Public yporsi As String
    End Structure
    Structure Ct
        Public Num As Integer
        Public Num1 As Integer
        Public Num2 As Integer
        <VBFixedString(32), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=32)> Public Nom1 As String
        <VBFixedString(32), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=32)> Public Nom2 As String
        <VBFixedString(32), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=32)> Public Nom3 As String
        Public Sdo As Double
        Public Sdo1 As Double
        Public Sdo2 As Double
    End Structure
    Structure Empresa
        <VBFixedString(60), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=60)> Public name As String
        Public añoEmpresa As Integer
        Public sm As Integer
        Public psub As Integer
        <VBFixedString(14), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=14)> Public fecha As String
    End Structure
    Structure Tra_cta
        Public num As Integer
        <VBFixedString(32), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=32)> Public nombre As String
        Public donde As Integer
        Public incia As Integer
        Public termina As Integer
        <VBFixedString(1), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=1)> Public clave As String
    End Structure

    Structure Tra_Scta
        Public num As Integer
        <VBFixedString(32), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=32)> Public nombre As String
        Public donde As Integer
        Public refer As Integer
        <VBFixedString(1), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=1)> Public clave As String
    End Structure

    Structure Sc
        <VBFixedString(64), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=64)> Public guarda As String
    End Structure
    Structure Nis_cal
        <VBFixedString(36), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=36)> Public folio As String
        Public estado As Boolean
    End Structure
    Structure Clabnx
        <VBFixedString(16), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=16)> Public Q1 As String
    End Structure
    Structure Nomco
        <VBFixedString(50), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=50)> Public ArchImp As String
        Public PSubDi As Long
        Public subdio As Long
        Public subapl As Long
        Public subNap As Long
        Public CreTot As Long
        Public CredNe As Long
        Public ImpTot As Long
    End Structure
    Structure Rg
        Public De As Integer
        Public A As Integer
        Public Ubic As Integer
    End Structure

    Structure NominaQuincenal
        Public dias As Long
        Public hsnor As Long
        Public hs_no As Long
        Public hsdbl As Long
        Public hs_db As Long
        Public hstri As Long
        Public hs_tr As Long
        Public ispt As Long
        Public crdsal As Long
        Public imss As Long
        Public sueldo As Long
        Public hs_nor As Long
        Public hs_dbl As Long
        Public hs_tri As Long
        Public viaticos As Long
        Public pvac As Long
        Public otras As Long
        Public aguin As Long
        Public ptu As Long
        Public exentos As Long
        Public prestamos As Long
        Public fonacot As Long
        Public telefono As Long
        Public otraded As Long
    End Structure
    Structure NuevaNominaCompleta
        <VBFixedString(50), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=50)> Public ArchImp As String
        Public PSubDi As Long
        Public subapl As Long
        Public subNap As Long
        Public Cretot As Long
        Public CredNe As Long
        Public ImpTOt As Long
    End Structure

    Structure NominaAnterior
        Public inggrav As Long
        Public imptoret As Long
        Public credcal As Long
        Public subapl As Long
        Public subtotal As Long
        Public subnoapl As Long
    End Structure

    Structure Ob
        Public O_1 As Integer
        Public por_1 As Integer
        Public im_1 As Long
        Public O_2 As Integer
        Public por_2 As Integer
        Public im_2 As Long
        Public O_3 As Integer
        Public por_3 As Integer
        Public im_3 As Long
        Public O_4 As Integer
        Public por_4 As Integer
        Public im_4 As Long
        Public O_5 As Integer
        Public por_5 As Integer
        Public im_5 As Long
        Public O_6 As Integer
        Public por_6 As Integer
        Public im_6 As Long
        Public O_7 As Integer
        Public por_7 As Integer
        Public im_7 As Long
        Public O_8 As Integer
        Public por_8 As Integer
        Public im_8 As Long
        Public O_9 As Long
        Public por_9 As Integer
        Public im_9 As Long
        Public O_10 As Integer
        Public por_10 As Integer
        Public im_10 As Long
        Public O_11 As Integer
        Public por_11 As Integer
        Public im_11 As Long
        Public O_12 As Integer
        Public por_12 As Integer
        Public im_12 As Long
        Public O_13 As Integer
        Public por_13 As Integer
        Public im_13 As Long
        Public O_14 As Integer
        Public por_14 As Integer
        Public im_14 As Long
        Public O_15 As Integer
        Public por_15 As Integer
        Public im_15 As Long
        Public O_16 As Integer
        Public por_16 As Integer
        Public im_16 As Long
        Public O_17 As Integer
        Public por_17 As Integer
        Public im_17 As Long
        Public O_18 As Integer
        Public por_18 As Integer
        Public im_18 As Long
        Public O_19 As Integer
        Public por_19 As Integer
        Public im_19 As Long
        Public O_20 As Integer
        Public por_20 As Integer
        Public im_20 As Long
    End Structure
    Structure Cheques
        Public num As Integer
        Public beneficiario As String
        Public importe As Long
        Public clave As String
        Public numreal As Integer
        Public refer As Integer
        Public conta As Integer
    End Structure
    Structure Mvtos
        Public Inc As Long
        Public Ene As Long
        Public Feb As Long
        Public Mar As Long
        Public Abr As Long
        Public May As Long
        Public Jun As Long
        Public Jul As Long
        Public Ago As Long
        Public Sep As Long
        Public Oct As Long
        Public Nov As Long
        Public Dic As Long
    End Structure

    Structure ultitmaOperacion
        Public numeroOperacion As Long
        Public ubicacionOperacion As Integer
        Public renglonOperacion As Long
        Public textoOperacion As String
        Public poliza As Integer
        Public impresionOperacion As Integer
        Public tipoOperacion As Integer
        Public redaccionOperacion As String
    End Structure
    Structure ult
        Public num As Long
        Public Ubi As Integer
        Public renglon As Long
        Public texto As String
        Public poliza As Integer
        Public Impresion As Integer
        Public TipoCap As Integer
        Public redaccion As String
    End Structure
    Structure Al
        Public Cos As Integer
        Public Rda As String
        Public Imt As Long
        Public Gto As Integer
        Public Dep As Integer
        Public Clt As Integer
        Public Otr As Integer
    End Structure
    Structure Su
        Public Parcial As Long
        Public Debe As Long
        Public Haber As Long
    End Structure

    ' Aqui se le da formato xdxdxd
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
        GuardarDataGridViewEnXML(DataGridView2)
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
            Dim yPos As Single = destinationRect.Top + DataGridView2.ColumnHeadersHeight

            For i As Integer = 0 To rowsToPrint - 1
                Dim row As DataGridViewRow = DataGridView2.Rows(rowIndex + i)
                Dim xPos As Single = destinationRect.Left
                Dim rowHeight As Single = row.Height

                For Each cell As DataGridViewCell In row.Cells
                    Dim cellWidth As Single = DataGridView2.Columns(cell.ColumnIndex).Width
                    Dim cellRect As New RectangleF(xPos, yPos, cellWidth, rowHeight)
                    DrawCell(cell, e.Graphics, cellRect)
                    xPos += cellWidth
                Next

                yPos += rowHeight
            Next

            ' Indicar que hay más páginas si quedan filas por imprimir
            rowIndex += rowsToPrint
            If rowIndex < DataGridView2.Rows.Count Then
                e.HasMorePages = True
                currentPage += 1
            Else
                e.HasMorePages = False
                currentPage = 0
            End If
        End If
    End Sub

    Private Sub DrawHeader(dataGridView As DataGridView, graphics As Graphics, rect As RectangleF)
        Dim xPos As Single = rect.Left
        For Each column As DataGridViewColumn In dataGridView.Columns
            Dim cellRect As New RectangleF(xPos, rect.Top, column.Width, rect.Height)
            graphics.FillRectangle(Brushes.LightGray, cellRect)
            graphics.DrawRectangle(Pens.Black, Rectangle.Round(cellRect))
            graphics.DrawString(column.HeaderText, dataGridView.Font, Brushes.Black, cellRect)
            xPos += column.Width
        Next
    End Sub

    Private Sub DrawCell(cell As DataGridViewCell, graphics As Graphics, rect As RectangleF)
        graphics.DrawRectangle(Pens.Black, Rectangle.Round(rect))
        If cell.Value IsNot Nothing Then
            graphics.DrawString(cell.Value.ToString(), cell.DataGridView.Font, Brushes.Black, rect)
        End If
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
            End If
        Next
    End Sub


    Private Sub ConvertirRANDOMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertirRANDOMToolStripMenuItem.Click
        Dim openFileDialog1 As New OpenFileDialog()

        ' Configurar las propiedades del OpenFileDialog
        openFileDialog1.InitialDirectory = "C:\" ' Carpeta inicial
        openFileDialog1.Filter = "All files (*.*)|*.*" ' Permitir todos los tipos de archivo
        openFileDialog1.FilterIndex = 1 ' Índice del filtro predeterminado
        openFileDialog1.RestoreDirectory = True ' Restaurar el directorio actual si el usuario cambia el directorio

        ' Mostrar el OpenFileDialog y verificar si el usuario seleccionó un archivo
        If openFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Obtener la ruta del archivo seleccionado
            Dim selectedFilePath As String = openFileDialog1.FileName

            Try
                ' Cargar los datos en DataGridView1 usando la función ConversorDeDatos
                Dim fileName As String = Path.GetFileName(selectedFilePath)
                Select Case True
                    Case fileName.ToUpper().Contains("CAT")
                        ConversorDeDatos(selectedFilePath, New CAT_MA(), Marshal.SizeOf(GetType(CAT_MA)), DataGridView2)
                    Case fileName.ToUpper().Contains("SAT")
                        ConversorDeDatos(selectedFilePath, New CAT_AX(), Marshal.SizeOf(GetType(CAT_AX)), DataGridView2)
                    Case fileName.ToUpper().Contains("SAC")
                        ConversorDeDatos(selectedFilePath, New Oper_aciones(), Marshal.SizeOf(GetType(Oper_aciones)), DataGridView2)
                    Case fileName.ToUpper().Contains("COR")
                        ConversorDeDatos(selectedFilePath, New Oper_aciones(), Marshal.SizeOf(GetType(Oper_aciones)), DataGridView2)
                    Case fileName.ToUpper().Contains("HABER")
                        ConversorDeDatos(selectedFilePath, New Oper_aciones(), Marshal.SizeOf(GetType(Oper_aciones)), DataGridView2)
                    Case fileName.ToUpper().Contains("CHEQ")
                        ConversorDeDatos(selectedFilePath, New Cheques(), Marshal.SizeOf(GetType(Cheques)), DataGridView2)
                    Case fileName.ToUpper().Contains("PER")
                        ConversorDeDatos(selectedFilePath, New Per(), Marshal.SizeOf(GetType(Per)), DataGridView2)
                    Case fileName.ToUpper().Contains("OtrasCh")
                        ConversorDeDatos(selectedFilePath, New OtrasCh(), Marshal.SizeOf(GetType(OtrasCh)), DataGridView2)
                    Case fileName.ToUpper().Contains("ContCat")
                        ConversorDeDatos(selectedFilePath, New ContCat(), Marshal.SizeOf(GetType(ContCat)), DataGridView2)
                    Case fileName.ToUpper().Contains("CAT")
                        ConversorDeDatos(selectedFilePath, New CAT_MA(), Marshal.SizeOf(GetType(CAT_MA)), DataGridView2)
                    Case fileName.ToUpper().Contains("DATOS")
                        ConversorDeDatos(selectedFilePath, New DAT_OS(), Marshal.SizeOf(GetType(DAT_OS)), DataGridView2)
                    Case Regex.IsMatch(fileName, "^Poliza(\d+)$", RegexOptions.IgnoreCase)
                        ' Lógica específica para archivos de póliza
                    Case Else
                        MessageBox.Show("Nombre de archivo no reconocido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                End Select
                MessageBox.Show("Archivo cargado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error al cargar el archivo: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub


    ' Método para cargar datos de archivos de acceso aleatorio en el DataGridView 1
    Private Sub ConversorDeDatos(filePath As String, estructura As Object, longitudRegistro As Integer, dataGridView As DataGridView)
        ' Limpiar las columnas y filas existentes en el DataGridView antes de cargar los archivos
        dataGridView.Columns.Clear()
        dataGridView.Rows.Clear()

        Try
            ' Definir las columnas basadas en la estructura
            Dim campos = estructura.GetType().GetFields()
            For Each campo In campos
                dataGridView.Columns.Add(campo.Name, campo.Name)
            Next

            ' Leer registros del archivo de acceso aleatorio
            Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
                Dim numRegistros As Integer = fs.Length \ longitudRegistro
                Dim buffer(longitudRegistro - 1) As Byte

                For i As Integer = 0 To numRegistros - 1
                    fs.Read(buffer, 0, longitudRegistro)

                    ' Convertir el buffer a la estructura
                    Dim handle As GCHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned)
                    estructura = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), estructura.GetType())
                    handle.Free()

                    ' Añadir los valores al DataGridView
                    Dim valores As New List(Of String)
                    For Each campo In campos
                        valores.Add(campo.GetValue(estructura).ToString())
                    Next
                    dataGridView.Rows.Add(valores.ToArray())
                Next
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al leer el archivo: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub CorrexionPolizasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CorrexionPolizasToolStripMenuItem.Click
        Dim nuevoFormulario As New Polizas ' Cambia Form2 al nombre de tu formulario
        nuevoFormulario.Show() ' Para mostrar el formulario de manera no modal
    End Sub

End Class
