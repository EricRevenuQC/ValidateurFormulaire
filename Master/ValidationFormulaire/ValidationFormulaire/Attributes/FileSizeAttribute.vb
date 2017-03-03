Imports System.ComponentModel.DataAnnotations

Public Class FileSizeAttribute
    Inherits ValidationAttribute
    Private ReadOnly _maxSize As Integer

    Public Sub New(maxSize As Integer)
        _maxSize = maxSize
    End Sub

    Public Overrides Function IsValid(value As Object) As Boolean
        If value Is Nothing Then
            Return True
        End If

        Return TryCast(value, HttpPostedFileBase).ContentLength <= _maxSize
    End Function

    Public Overrides Function FormatErrorMessage(name As String) As String
        Return String.Format("The file size should not exceed {0}", _maxSize)
    End Function
End Class