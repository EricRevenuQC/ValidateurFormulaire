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
        Dim substring As String

        For Each change As DiffLib.DiffChange In changes
            If change.Equal Then
                text.Append(AdjustText(text1.Substring(i1, change.Length1)))
            Else
                substring = AdjustText(text1.Substring(i1, change.Length1))
                If Not String.IsNullOrWhiteSpace(substring) Then
                    text.Append("<span style='background-color: #ffcccc; text-decoration: line-through;'>" + substring + "</span>")
                Else
                    text.Append(substring)
                End If
                substring = AdjustText(RemoveTraillingCharacter(text2.Substring(i2, change.Length2)))
                If Not String.IsNullOrWhiteSpace(substring) Then
                    text.Append("<span style='background-color: #ccffcc;'>" + substring + "</span>")
                Else
                    text.Append(substring)
                End If
            End If
            i1 += change.Length1
            i2 += change.Length2
        Next

        Return text.ToString
    End Function

    Private Function AdjustText(substring As String)
        If substring.Contains("¶") Then
            substring = substring.Replace("¶", "</br>")
        End If
        If substring.Contains("A M J") Then
            substring = substring.Replace("A M J", "")
        End If
        Return substring
    End Function

    Private Function RemoveTraillingCharacter(substring As String) As String
        Dim contain_trailling_character As Boolean = False
        Dim new_string As String = substring

        Dim remove_index As Integer = 0
        For i As Integer = 0 To substring.Length - 1
            If substring(i) = "u" OrElse substring(i) = "," Then
                contain_trailling_character = True
                For j As Integer = i To If(i - 1 > 0, i - 1, 0) Step -1
                    If j <> i AndAlso Regex.IsMatch(substring(j), "[^¶,u+÷x\s]") Then
                        contain_trailling_character = False
                    End If
                Next
                For j As Integer = i To If(i + 1 > substring.Length - 1, substring.Length - 1, i + 1)
                    If j <> i AndAlso Regex.IsMatch(substring(j), "[^¶,u+÷x\s]") Then
                        contain_trailling_character = False
                    End If
                Next
                If contain_trailling_character Then
                    new_string = new_string.Remove(i - remove_index, 1)
                    remove_index += 1
                End If
            End If
        Next
        Return new_string
    End Function
End Class
