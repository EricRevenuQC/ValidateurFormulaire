Imports System.Text.RegularExpressions
Imports System.Text
Imports DiffLib

Public Class CompareTexts
    Private changes As DiffChange

    Public Function CompareTexts(text1 As String, text2 As String) As String
        Dim changes As New Diff(Of Char)(text1, text2)
        Dim text As New StringBuilder()
        Dim i1 As Integer = 0
        Dim i2 As Integer = 0

        For Each change As DiffLib.DiffChange In changes
            If change.Equal Then
                text.Append(text1.Substring(i1, change.Length1))
            Else
                text.Append("<span style='background-color: #ffcccc; text-decoration: line-through;'>" + text1.Substring(i1, change.Length1) + "</span>")
                text.Append("<span style='background-color: #ccffcc;'>" + text2.Substring(i2, change.Length2) + "</span>")
            End If
            i1 += change.Length1
            i2 += change.Length2
        Next

        System.Diagnostics.Debug.WriteLine(text.Length)

        Return text.ToString
    End Function
End Class
