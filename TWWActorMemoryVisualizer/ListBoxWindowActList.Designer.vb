<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ListBoxWindowActList
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader_Name = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Instance = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_Status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader_ID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(96, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 18)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Actor List:"
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.AutoArrange = False
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader_ID, Me.ColumnHeader_Name, Me.ColumnHeader_Instance, Me.ColumnHeader_Status})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(12, 43)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(251, 349)
        Me.ListView1.Sorting = System.Windows.Forms.SortOrder.Descending
        Me.ListView1.TabIndex = 10
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader_Name
        '
        Me.ColumnHeader_Name.Tag = "Text"
        Me.ColumnHeader_Name.Text = "Name"
        Me.ColumnHeader_Name.Width = 80
        '
        'ColumnHeader_Instance
        '
        Me.ColumnHeader_Instance.Tag = "Numeric"
        Me.ColumnHeader_Instance.Text = "Count"
        Me.ColumnHeader_Instance.Width = 50
        '
        'ColumnHeader_Status
        '
        Me.ColumnHeader_Status.Tag = "Text"
        Me.ColumnHeader_Status.Text = "Status"
        Me.ColumnHeader_Status.Width = 80
        '
        'ColumnHeader_ID
        '
        Me.ColumnHeader_ID.Tag = "Numeric"
        Me.ColumnHeader_ID.Text = "ID"
        Me.ColumnHeader_ID.Width = 30
        '
        'ListBoxWindowActList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(275, 404)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "ListBoxWindowActList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Actor List"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ColumnHeader_Name As ColumnHeader
    Friend WithEvents ColumnHeader_Instance As ColumnHeader
    Friend WithEvents ColumnHeader_Status As ColumnHeader
    Friend WithEvents ColumnHeader_ID As ColumnHeader
End Class
