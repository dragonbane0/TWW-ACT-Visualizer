Public Class ListBoxWindowActList

    Private Sub ListBoxWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ListView1.ListViewItemSorter = New Sorter

    End Sub

    Private Sub ListBoxWindow_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed

        Application.Exit()

    End Sub

    Private Sub ListView1_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles ListView1.ColumnClick

        Dim s As Sorter = ListView1.ListViewItemSorter
        s.Column = e.Column
        Main.CurrentSortingActListColumn = s.Column

        If (s.Order = System.Windows.Forms.SortOrder.Ascending) Then

            s.Order = System.Windows.Forms.SortOrder.Descending

        Else

            s.Order = System.Windows.Forms.SortOrder.Ascending

        End If

        Main.CurrentSortingActListOrder = s.Order

        ListView1.Sort()

    End Sub

End Class