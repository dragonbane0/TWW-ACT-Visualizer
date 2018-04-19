<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class VisualBar
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
        Me.PictureBox_Main = New System.Windows.Forms.PictureBox()
        Me.Label_Arrow = New System.Windows.Forms.Label()
        CType(Me.PictureBox_Main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox_Main
        '
        Me.PictureBox_Main.BackColor = System.Drawing.Color.White
        Me.PictureBox_Main.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox_Main.Name = "PictureBox_Main"
        Me.PictureBox_Main.Size = New System.Drawing.Size(1073, 110)
        Me.PictureBox_Main.TabIndex = 7
        Me.PictureBox_Main.TabStop = False
        '
        'Label_Arrow
        '
        Me.Label_Arrow.AutoSize = True
        Me.Label_Arrow.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Arrow.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label_Arrow.Location = New System.Drawing.Point(885, 115)
        Me.Label_Arrow.Name = "Label_Arrow"
        Me.Label_Arrow.Size = New System.Drawing.Size(38, 39)
        Me.Label_Arrow.TabIndex = 8
        Me.Label_Arrow.Text = "⇑"
        Me.Label_Arrow.Visible = False
        '
        'VisualBar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1097, 147)
        Me.Controls.Add(Me.PictureBox_Main)
        Me.Controls.Add(Me.Label_Arrow)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "VisualBar"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Visual Memory Bar"
        CType(Me.PictureBox_Main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox_Main As PictureBox
    Friend WithEvents Label_Arrow As Label
End Class
