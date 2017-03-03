Imports System.Globalization
Imports System.Web

Public Class SessionValueProvider

    Public Overridable Sub SetValue(key As String, value As Object)
        HttpContext.Current.Session(key) = value
    End Sub

    Public Overridable Sub SetValue(key As String, value() As Drawing.Image)
        HttpContext.Current.Session(key) = value
    End Sub

    Public Overridable Function GetValue(key As String) As Object
        If HttpContext.Current.Session(key) Is Nothing Then
            Return Nothing
        End If

        Return HttpContext.Current.Session(key)
    End Function
End Class
