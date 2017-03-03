Imports System.Data.Entity

Namespace Models
    Public Class Movie
        Public Property ID As Integer
        Public Property Title As String
        Public Property ReleaseDate As DateTime
        Public Property Genre As String
        Public Property Price As Decimal
    End Class

    Public Class CodeBarDB
        Inherits DbContext

        Public Property CodeBar As DbSet(Of Movie)
    End Class

End Namespace