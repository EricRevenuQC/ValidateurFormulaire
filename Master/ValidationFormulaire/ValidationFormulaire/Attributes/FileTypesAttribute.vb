Imports System.ComponentModel.DataAnnotations

Public Class FileTypesAttribute
    Inherits ValidationAttribute
    Private ReadOnly _types As List(Of String)

    Public Sub New(types As String)
        _types = types.Split(","c).ToList()
    End Sub

    Public Overrides Function IsValid(value As Object) As Boolean
        If value Is Nothing Then
            Return True
        End If

        Dim fileExt = System.IO.Path.GetExtension(TryCast(value, HttpPostedFileBase).FileName).Substring(1)
        Return _types.Contains(fileExt, StringComparer.OrdinalIgnoreCase)
    End Function

    Public Overrides Function FormatErrorMessage(name As String) As String
        Return String.Format("Invalid file type. Only the following types {0} are supported.", [String].Join(", ", _types))
    End Function
End Class