﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Polizas
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Polizas))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Rgto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SubCta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Parcial = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Debe = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Haber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Concepto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Otros = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SubirPolizaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Rgto, Me.Cta, Me.SubCta, Me.Nombre, Me.Parcial, Me.Debe, Me.Haber, Me.CI, Me.Concepto, Me.Otros})
        Me.DataGridView1.Location = New System.Drawing.Point(22, 33)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(1767, 581)
        Me.DataGridView1.TabIndex = 0
        '
        'Rgto
        '
        Me.Rgto.HeaderText = "Rgto"
        Me.Rgto.MinimumWidth = 6
        Me.Rgto.Name = "Rgto"
        Me.Rgto.Width = 125
        '
        'Cta
        '
        Me.Cta.HeaderText = "Cta"
        Me.Cta.MinimumWidth = 6
        Me.Cta.Name = "Cta"
        Me.Cta.Width = 125
        '
        'SubCta
        '
        Me.SubCta.HeaderText = "SubCta"
        Me.SubCta.MinimumWidth = 6
        Me.SubCta.Name = "SubCta"
        Me.SubCta.Width = 125
        '
        'Nombre
        '
        Me.Nombre.HeaderText = "Nombre"
        Me.Nombre.MinimumWidth = 6
        Me.Nombre.Name = "Nombre"
        Me.Nombre.Width = 125
        '
        'Parcial
        '
        Me.Parcial.HeaderText = "Parcial"
        Me.Parcial.MinimumWidth = 6
        Me.Parcial.Name = "Parcial"
        Me.Parcial.Width = 125
        '
        'Debe
        '
        Me.Debe.HeaderText = "Debe"
        Me.Debe.MinimumWidth = 6
        Me.Debe.Name = "Debe"
        Me.Debe.Width = 125
        '
        'Haber
        '
        Me.Haber.HeaderText = "Haber"
        Me.Haber.MinimumWidth = 6
        Me.Haber.Name = "Haber"
        Me.Haber.Width = 125
        '
        'CI
        '
        Me.CI.HeaderText = "CI"
        Me.CI.MinimumWidth = 6
        Me.CI.Name = "CI"
        Me.CI.Width = 125
        '
        'Concepto
        '
        Me.Concepto.HeaderText = "Concepto"
        Me.Concepto.MinimumWidth = 6
        Me.Concepto.Name = "Concepto"
        Me.Concepto.Width = 125
        '
        'Otros
        '
        Me.Otros.HeaderText = "Otros"
        Me.Otros.MinimumWidth = 6
        Me.Otros.Name = "Otros"
        Me.Otros.Width = 125
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SubirPolizaToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1801, 28)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SubirPolizaToolStripMenuItem
        '
        Me.SubirPolizaToolStripMenuItem.Name = "SubirPolizaToolStripMenuItem"
        Me.SubirPolizaToolStripMenuItem.Size = New System.Drawing.Size(100, 24)
        Me.SubirPolizaToolStripMenuItem.Text = "Subir Poliza"
        '
        'Polizas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1801, 626)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Polizas"
        Me.Text = "Polizas"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Rgto As DataGridViewTextBoxColumn
    Friend WithEvents Cta As DataGridViewTextBoxColumn
    Friend WithEvents SubCta As DataGridViewTextBoxColumn
    Friend WithEvents Nombre As DataGridViewTextBoxColumn
    Friend WithEvents Parcial As DataGridViewTextBoxColumn
    Friend WithEvents Debe As DataGridViewTextBoxColumn
    Friend WithEvents Haber As DataGridViewTextBoxColumn
    Friend WithEvents CI As DataGridViewTextBoxColumn
    Friend WithEvents Concepto As DataGridViewTextBoxColumn
    Friend WithEvents Otros As DataGridViewTextBoxColumn
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents SubirPolizaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
End Class