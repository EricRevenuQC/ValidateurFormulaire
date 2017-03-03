Public Class DeterminePage
    Implements IPagination

    Public Enum PageAction
        NextPage
        PreviousPage
        JumpToPage
        Compare
    End Enum

    Public Function DeterminePageChange(action As PageAction, current_page As Integer, page As Integer) As Integer Implements IPagination.DeterminePageChange
        If (action = PageAction.NextPage) Then
            Return NextPage(current_page)
        ElseIf (action = PageAction.PreviousPage) Then
            Return PreviousPage(current_page)
        End If
        Return JumpToPage(page)
    End Function

    Private Function JumpToPage(page As Integer) As Integer
        Return page
    End Function

    Private Function NextPage(current_page As Integer) As Integer
        current_page += 1
        Return current_page
    End Function

    Private Function PreviousPage(current_page As Integer) As Integer
        current_page -= 1
        Return current_page
    End Function
End Class
