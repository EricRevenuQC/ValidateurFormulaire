Imports System.Text.RegularExpressions
Imports System.Text
Imports DiffLib

Public Class CompareTexts
    Private changes As DiffChange

    Private bold_marker_count As Integer

    Public Function CompareTexts(text1 As String, text2 As String) As String
        Dim changes As New Diff(Of Char)(text1, text2)
        Dim text As New StringBuilder()
        Dim i1 As Integer = 0
        Dim i2 As Integer = 0
        Dim substring As String
        Dim bold_text As Boolean = False
        Dim italic_text As Boolean = False
        Dim string_check As String
        bold_marker_count = 0

        For Each change As DiffLib.DiffChange In changes
            If change.Equal Then
                substring = AdjustText(text1.Substring(i1, change.Length1))
                string_check = substring
                If string_check.Contains("<span style='background-color: #ff0000; text-decoration: line-through;'>") Then
                    string_check.Replace("<span style='background-color: #ff0000; text-decoration: line-through;'>", "")
                End If
                If string_check.Contains("</span>") Then
                    string_check.Replace("</span>", "")
                End If
                If Not String.IsNullOrWhiteSpace(string_check) Then
                    text.Append(substring)
                Else
                    text.Append("<span style='background-color: #ffffff; text-decoration: none;'>" + substring + "</span>")
                End If
            Else
                substring = AdjustText(text1.Substring(i1, change.Length1))

                If (substring.Contains("<b>") AndAlso Not substring.Contains("</b>") OrElse
                        Not substring.Contains("<b>") AndAlso substring.Contains("</b>")) Then
                    bold_text = Not bold_text

                    If bold_text Then
                        text.Append("<span style='background-color: #ff0000; text-decoration: line-through;'>")
                    Else
                        text.Append("</span>")
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(substring) Then
                    text.Append("<span style='background-color: #ffcccc; text-decoration: line-through;'>" + substring + "</span>")
                Else
                    text.Append("<span style='background-color: #ffffff; text-decoration: none;'>" + substring + "</span>")
                End If

                '--------------------------------

                substring = AdjustText(RemoveTraillingCharacter(text2.Substring(i2, change.Length2)))

                If (substring.Contains("<b>") AndAlso Not substring.Contains("</b>") OrElse
                        Not substring.Contains("<b>") AndAlso substring.Contains("</b>")) Then
                    bold_text = Not bold_text

                    If bold_text Then
                        text.Append("<span style='background-color: #ff0000; text-decoration: line-through;'>")
                    Else
                        text.Append("</span>")
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(substring) Then
                    text.Append("<span style='background-color: #ccffcc;'>" + substring + "</span>")
                Else
                    text.Append("<span style='background-color: #ffffff; text-decoration: none;'>" + substring + "</span>")
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
        If substring.Contains("Ƃ") Then
            System.Diagnostics.Debug.WriteLine(bold_marker_count)
            If bold_marker_count > 0 Then
                System.Diagnostics.Debug.WriteLine("Fix")
                substring = substring.Replace("Ƃ", "</b><b>")
            Else
                substring = substring.Replace("Ƃ", "<b>")
                bold_marker_count += 1
            End If
        End If
        If substring.Contains("Ƌ") Then
            substring = substring.Replace("Ƌ", "</b>")
            bold_marker_count -= 1
        End If
        If substring.Contains("A M J") Then
            substring = substring.Replace("A M J", "")
        End If
        Return substring
    End Function

    Private Function AdjustItalicText(substring As String)
        If substring.Contains("<i>") Then
            substring = substring.Replace("<i>", "")
        End If
        If substring.Contains("</i>") Then
            substring = substring.Replace("</i>", "")
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
                    If j <> i AndAlso Regex.IsMatch(substring(j), "[^<>¶,u+÷x\s]") Then
                        contain_trailling_character = False
                    End If
                Next
                For j As Integer = i To If(i + 1 > substring.Length - 1, substring.Length - 1, i + 1)
                    If j <> i AndAlso Regex.IsMatch(substring(j), "[^<>¶,u+÷x\s]") Then
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
