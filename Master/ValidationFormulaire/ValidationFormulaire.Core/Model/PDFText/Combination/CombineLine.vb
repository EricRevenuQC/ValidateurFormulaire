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

    Public Function CombineText(text_groups As Object, Optional bold As Boolean = False,
                                Optional italic As Boolean = False) As Object Implements ICombineText.CombineText
        Dim words As New Dictionary(Of Side, TextProperties)()
        Dim current_y As Integer = 0
        Dim word_list As New Dictionary(Of Point, String)()
        Dim word_string As String

        For Each text As KeyValuePair(Of Point, TextProperties) In text_groups
            If (bold AndAlso Not text.Value.bold) Then
                Continue For
            End If
            If (italic AndAlso Not text.Value.italic) Then
                Continue For
            End If
            If Math.Abs(text.Key.Y - current_y) > MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_Y Then
                If words.Count > 0 Then
                    word_string = AddNewLine(words)
                    If Not word_list.ContainsKey(New Point(words.First.Key.left_side, current_y)) Then
                        word_list.Add(New Point(words.First.Key.left_side, current_y), word_string)
                    Else
                        word_list(New Point(words.First.Key.left_side, current_y)) = word_string
                    End If
                End If
                current_y = text.Key.Y
                words = New Dictionary(Of Side, TextProperties)()
                words.Add(New Side(text.Key.X, text.Value.right_side), New TextProperties With {.text = text.Value.text,
                                                                                                .bold = text.Value.bold,
                                                                                                .italic = text.Value.italic})
            Else
                If Not words.ContainsKey(New Side(text.Key.X, text.Value.right_side)) Then
                    words.Add(New Side(text.Key.X, text.Value.right_side),
                              New TextProperties With {.text = text.Value.text, .bold = text.Value.bold,
                                  .italic = text.Value.italic})
                Else
                    'Attention : utiliser += pour ajouter à la valeur d'un dictionnaire crash VS2012!!!
                    words(New Side(text.Key.X, text.Value.right_side)).text = words(New Side(text.Key.X, text.Value.right_side)).text + text.Value.text
                End If
            End If
        Next

        word_string = AddNewLine(words)
        If word_list.Count > 0 Then
            If Not word_list.ContainsKey(New Point(words.First.Key.left_side, current_y)) Then
                word_list.Add(New Point(words.First.Key.left_side, current_y), word_string)
            Else
                word_list(New Point(words.First.Key.left_side, current_y)) = word_string
            End If
        End If

        Return word_list
    End Function

    Private Function AddNewLine(words As Dictionary(Of Side, TextProperties)) As String
        Dim word_string As String = ""
        Dim previous_word_x As Integer = 0
        Dim bold_word As Boolean = False
        Dim italic_word As Boolean = False
        Dim bold_counter As Integer = 0
        Dim italic_counter As Integer = 0

        words = words.OrderBy(Function(x) x.Key.left_side).ToDictionary(Function(x) x.Key, Function(y) y.Value)
        
        For Each word As KeyValuePair(Of Side, TextProperties) In words
            If word.Value.bold <> bold_word Then
                bold_word = word.Value.bold
                If word.Value.bold Then
                    word_string += "Ƃ"
                    bold_counter += 1
                Else
                    word_string += "Ƌ"
                    bold_counter -= 1
                End If
            End If
            'If word.Value.italic <> italic_word Then
            '    italic_word = word.Value.italic
            '    If word.Value.italic Then
            '        word_string += "<i>"
            '        italic_counter += 1
            '    Else
            '        word_string += "</i>"
            '        italic_counter -= 1
            '    End If
            'End If
            If previous_word_x <> 0 AndAlso Math.Abs(word.Key.left_side - previous_word_x) > MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_X Then
                word_string += " "
            End If
            word_string += word.Value.text
            previous_word_x = word.Key.right_side
        Next
        While bold_counter > 0
            word_string += "Ƌ"
            bold_counter -= 1
        End While
        'If italic_counter > 0 Then
        '    word_string += "</i>"
        'End If

        Return word_string
    End Function
End Class
