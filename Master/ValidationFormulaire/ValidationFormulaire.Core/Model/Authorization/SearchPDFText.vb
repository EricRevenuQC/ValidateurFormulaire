Imports System.Text.RegularExpressions
Imports System.Drawing

Public Class SearchPDFText
    Public Function FindBarCodeValues(words As Dictionary(Of Point, String),
                                      bar_code_data As Dictionary(Of String, BarCodeData)) As Dictionary(Of String, BarCodeData)
        Dim adjusted_bar_code_values As String
        Dim bar_code_index As Integer = 0
        Dim releve As New VerifyReleves(New Releve1)
        Dim failed_bar_code_values_index As New Dictionary(Of String, BarCodeData)()

        words = words.OrderByDescending(Function(x) x.Key.Y).ThenBy(Function(x) x.Key.X).ToDictionary(Function(x) x.Key, Function(y) y.Value)
        words = AdjustWords(words)

        For Each data As KeyValuePair(Of String, BarCodeData) In bar_code_data
            adjusted_bar_code_values = data.Value.value.Trim()

            If words.ContainsValue(adjusted_bar_code_values) OrElse String.IsNullOrWhiteSpace(adjusted_bar_code_values) Then
                If bar_code_index <> 0 OrElse bar_code_index <> 1 OrElse bar_code_index <> 2 _
                        OrElse bar_code_index <> 27 Then
                    If Not releve.VerifyTextInputPosition(words, data, bar_code_index) Then
                        failed_bar_code_values_index.Add(bar_code_data.ElementAt(bar_code_index).Key, bar_code_data.ElementAt(bar_code_index).Value)
                    End If
                End If

                words.Remove(New DictionaryOperations().GetKeyFromValue(words, adjusted_bar_code_values))
            Else
                failed_bar_code_values_index.Add(bar_code_data.ElementAt(bar_code_index).Key, bar_code_data.ElementAt(bar_code_index).Value)
            End If

            bar_code_index += 1
        Next

        For Each data As KeyValuePair(Of String, BarCodeData) In failed_bar_code_values_index
            System.Diagnostics.Debug.WriteLine("Failed : " + data.Value.value.Trim())
        Next

        Return failed_bar_code_values_index
    End Function

    Private Function AdjustWords(words As Dictionary(Of Point, String)) As Dictionary(Of Point, String)
        Dim words_adjusted As New Dictionary(Of Point, String)()

        For Each word As KeyValuePair(Of Point, String) In words
            words_adjusted.Add(word.Key, Regex.Replace(word.Value, "(?<=\d)\s+(?=\d)", ""))
            words_adjusted(word.Key) = Regex.Replace(words_adjusted(word.Key), "(?<=\d),(?=\d)", "")
            words_adjusted(word.Key) = Regex.Replace(words_adjusted(word.Key), "(?<=\d)\.(?=\d)", "")
            words_adjusted(word.Key) = words_adjusted(word.Key).Trim()
        Next

        Return words_adjusted
    End Function
End Class
