Public Class TextAdjuster
    Public Function CharChainExistInText(chain As String, text As String) As Boolean
        Return text.Contains(chain)
    End Function

    Public Function RemoveCharChainFromText(chain As String, text As String) As String
        Dim number_position As Integer
        number_position = text.IndexOf(chain)
        If number_position >= 0 Then
            text = text.Remove(number_position, chain.Length)
        End If
        Return text
    End Function

    Public Function RemoveCharsFromNumbersInString(characters() As Char, text As String) As String
        Dim chars_to_remove = New List(Of Integer)()
        Dim removed_char_index_change As Integer = 0

        For character_index As Integer = 1 To text.Length - 1 Step 1
            If characters.Contains(text.ElementAt(character_index)) Then
                If New System.Text.RegularExpressions.Regex("^[0-9]$").IsMatch(text.ElementAt(character_index - 1)) AndAlso
                    New System.Text.RegularExpressions.Regex("^[0-9]$").IsMatch(text.ElementAt(character_index + 1)) Then
                    chars_to_remove.Add(character_index)
                End If
            End If
        Next
        For Each character_index In chars_to_remove
            text = text.Remove(character_index - removed_char_index_change, 1)
            removed_char_index_change += 1
        Next

        Return text
    End Function
End Class
