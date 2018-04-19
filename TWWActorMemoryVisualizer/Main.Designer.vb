<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Button_Connect = New System.Windows.Forms.Button()
        Me.Timer_Update = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox_FindSlot = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Button_Connect
        '
        Me.Button_Connect.Location = New System.Drawing.Point(41, 12)
        Me.Button_Connect.Name = "Button_Connect"
        Me.Button_Connect.Size = New System.Drawing.Size(175, 44)
        Me.Button_Connect.TabIndex = 0
        Me.Button_Connect.Text = "Connect to Dolphin"
        Me.Button_Connect.UseVisualStyleBackColor = True
        '
        'Timer_Update
        '
        Me.Timer_Update.Interval = 1000
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(41, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Find free slot for"
        '
        'TextBox_FindSlot
        '
        Me.TextBox_FindSlot.Location = New System.Drawing.Point(155, 87)
        Me.TextBox_FindSlot.MaxLength = 4
        Me.TextBox_FindSlot.Name = "TextBox_FindSlot"
        Me.TextBox_FindSlot.Size = New System.Drawing.Size(27, 20)
        Me.TextBox_FindSlot.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(186, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "KB"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(253, 122)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox_FindSlot)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button_Connect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TWW Actor Memory"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button_Connect As Button
    Friend WithEvents Timer_Update As Timer
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox_FindSlot As TextBox
    Friend WithEvents Label2 As Label
End Class

Class Sorter
    Implements System.Collections.IComparer

    Public Column As Integer = 0
    Public Order As System.Windows.Forms.SortOrder = SortOrder.Ascending

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare

        If Not (TypeOf x Is ListViewItem) Then

            Return (0)

        End If

        If Not (TypeOf y Is ListViewItem) Then

            Return (0)

        End If

        Dim l1 As ListViewItem = DirectCast(x, ListViewItem)
        Dim l2 As ListViewItem = DirectCast(y, ListViewItem)

        If l1.ListView.Columns(Column).Tag Is Nothing Then

            l1.ListView.Columns(Column).Tag = "Text"

        End If

        If l1.ListView.Columns(Column).Tag.ToString() = "Numeric" Then

            Dim fl1 As Single = Single.Parse(l1.SubItems(Column).Text.Replace(".", ","))
            Dim fl2 As Single = Single.Parse(l2.SubItems(Column).Text.Replace(".", ","))

            If Order = SortOrder.Ascending Then

                Return fl1.CompareTo(fl2)
            Else

                Return fl2.CompareTo(fl1)

            End If
        Else

            Dim str1 As String = l1.SubItems(Column).Text
            Dim str2 As String = l2.SubItems(Column).Text

            If Order = SortOrder.Ascending Then

                Return str1.CompareTo(str2)

            Else

                Return str2.CompareTo(str1)

            End If

        End If

    End Function

End Class
