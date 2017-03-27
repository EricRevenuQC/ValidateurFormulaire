Imports System.Drawing

Public Class TextCleaner
    Implements ITextCleaner

    Private dict_operations As New DictionaryKeys()

    Private Const HEIGHT_THRESHOLD_TOLERANCE As Integer = 5

    Public Function CleanPDFWords(words() As Dictionary(Of Point, String)) As Dictionary(Of Point, String)() _
            Implements ITextCleaner.CleanPDFWords
        Dim height_threshold As Point
        Dim temp_words As New Dictionary(Of Point, String)

        For page As Integer = 1 To words.Count - 1
            temp_words = New Dictionary(Of Point, String)(words(page))
            height_threshold = dict_operations.GetKeyFromDictionaryValue(temp_words, "Année")

            If height_threshold.Y > 0 Then
                For Each word As KeyValuePair(Of Point, String) In temp_words
                    If word.Key.Y > height_threshold.Y + HEIGHT_THRESHOLD_TOLERANCE Then
                        words(page).Remove(word.Key)
                    End If
                Next
            End If
        Next

        Return words
    End Function
End Class
