Imports System.Drawing

Public Class VerifyTexts
    Public Function VerifyTexts(text() As Dictionary(Of Point, String), template() As Dictionary(Of Point, String)) _
            As Dictionary(Of Point, String)()
        Dim word_index As Integer = 0
        Dim failed_words(text.Count - 1) As Dictionary(Of Point, String)

        For page As Integer = 1 To text.Count - 1
            For Each word As KeyValuePair(Of Point, String) In text(page)
                If word.Value <> template(page).ElementAt(word_index).Value Then
                    failed_words(page).Add(word.Key, word.Value)
                End If

                word_index += 1
            Next
        Next

        Return failed_words
    End Function
End Class
