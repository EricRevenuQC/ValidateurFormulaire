Imports System.Drawing

Public Class CombineLine
    Implements ICombineText

    Private Structure Side
        Public left_side As Integer
        Public right_side As Integer

        Sub New(left As Integer, right As Integer)
            left_side = left
            right_side = right
        End Sub

    End Structure

    Private Const MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_X As Integer = 15
    Private Const MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_Y As Integer = 6

    Public Function CombineText(text_groups As Object) As Object Implements ICombineText.CombineText
        Dim words As New Dictionary(Of Side, String)()
        Dim current_y As Integer = 0
        Dim previous_word_x As Integer = 0
        Dim word_list As New Dictionary(Of Point, String)()
        Dim word_string As String

        For Each row As KeyValuePair(Of Point, TextProperties) In text_groups
            If Math.Abs(row.Key.Y - current_y) > MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_Y Then
                If words.Count > 0 Then
                    word_string = ""
                    words = words.OrderBy(Function(x) x.Key.left_side).ToDictionary(Function(x) x.Key, Function(y) y.Value)
                    previous_word_x = 0
                    For Each word As KeyValuePair(Of Side, String) In words
                        If previous_word_x <> 0 AndAlso Math.Abs(word.Key.left_side - previous_word_x) > MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_X Then
                            word_string += " "
                        End If
                        word_string += word.Value
                        previous_word_x = word.Key.right_side
                    Next
                    If Not word_list.ContainsKey(New Point(words.First.Key.left_side, current_y)) Then
                        word_list.Add(New Point(words.First.Key.left_side, current_y), word_string)
                    Else
                        word_list(New Point(words.First.Key.left_side, current_y)) = word_string
                    End If
                End If
                current_y = row.Key.Y
                words = New Dictionary(Of Side, String)()
                words.Add(New Side(row.Key.X, row.Value.right_side), row.Value.text)
            Else
                If Not words.ContainsKey(New Side(row.Key.X, row.Value.right_side)) Then
                    words.Add(New Side(row.Key.X, row.Value.right_side), row.Value.text)
                Else
                    'Attention : utiliser += pour ajouter à la valeur d'un dictionnaire crash VS2012!!!
                    words(New Side(row.Key.X, row.Value.right_side)) = words(New Side(row.Key.X, row.Value.right_side)) + row.Value.text
                End If
            End If
        Next

        word_string = ""
        words = words.OrderBy(Function(x) x.Key.left_side).ToDictionary(Function(x) x.Key, Function(y) y.Value)
        previous_word_x = 0
        For Each word As KeyValuePair(Of Side, String) In words
            If previous_word_x <> 0 AndAlso Math.Abs(word.Key.left_side - previous_word_x) > MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_X Then
                word_string += " "
            End If
            word_string += word.Value
            previous_word_x = word.Key.right_side
        Next
        If Not word_list.ContainsKey(New Point(words.First.Key.left_side, current_y)) Then
            word_list.Add(New Point(words.First.Key.left_side, current_y), word_string)
        Else
            word_list(New Point(words.First.Key.left_side, current_y)) = word_string
        End If

        Return word_list
    End Function
End Class
