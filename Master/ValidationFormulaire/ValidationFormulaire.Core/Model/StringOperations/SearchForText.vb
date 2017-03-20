Public Class SearchForText

    Private text_adjustor As New TextAdjuster()
    Private exceptions As ExceptionsManager
    Private chars_remaining As String = ""

    Public Function RemoveStringFromString(to_find As String, to_search As String,
                                            Optional char_minimum_length As Integer = 0,
                                            Optional save_remaining_chars As Boolean = False) As String
        Dim current_word As String = ""
        exceptions = New ExceptionsManager()
        to_find += " "

        For Each character As Char In to_find
            If Not character = Environment.NewLine AndAlso Not character = vbCrLf AndAlso
                    Not character = vbCr AndAlso Not character = vbLf AndAlso Not character = " " Then
                current_word += character
            Else
                If current_word.Length > char_minimum_length Then
                    If exceptions.CheckWordForZerosException(current_word) Then
                        If text_adjustor.CharChainExistInText(current_word, to_search) Then
                            to_search = text_adjustor.RemoveCharChainFromText(current_word, to_search)
                        ElseIf save_remaining_chars Then
                            chars_remaining += current_word + " "
                        End If
                    End If
                End If
                current_word = ""
            End If
        Next

        Return to_search
    End Function

    Public Function GetRemainingCharacters() As String
        Return chars_remaining
    End Function
End Class
