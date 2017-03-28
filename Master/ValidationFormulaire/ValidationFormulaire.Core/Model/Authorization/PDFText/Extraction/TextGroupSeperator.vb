Imports System.Drawing

Public Class TextGroupSeperator
    Implements ITextGroupSeperator

    Private text_groups As Dictionary(Of Point, TextProperties)
    Private word_list As Dictionary(Of Point, String)

    Private Const MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_X As Integer = 10
    Private Const MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_Y As Integer = 5

    Public Function SeperateTextIntoGroups(text_extraction_strategy As ITextExtractionStrategy) _
            As Dictionary(Of Point, String) Implements ITextGroupSeperator.SeperateTextIntoGroups
        Dim text_properties As TextProperties
        word_list = New Dictionary(Of Point, String)()
        text_groups = New Dictionary(Of Point, TextProperties)()

        For Each text_rectangle As TextRectangle In text_extraction_strategy.GetRectanglePoints
            text_properties = New TextProperties()
            text_properties.text = text_rectangle.text
            text_properties.right_side = text_rectangle.rectangle.Right
            If Not text_groups.ContainsKey(New Point(text_rectangle.rectangle.Left, text_rectangle.rectangle.Bottom)) Then
                text_groups.Add(New Point(text_rectangle.rectangle.Left, text_rectangle.rectangle.Bottom), text_properties)
            End If
        Next

        text_groups = text_groups.OrderBy(Function(x) x.Key.Y).ThenBy(Function(x) x.Key.X).ToDictionary(Function(x) x.Key, Function(y) y.Value)

        CombineCloseTextGroupsToFormWords()
        Return word_list
    End Function

    Private Sub CombineCloseTextGroupsToFormWords()
        Dim word As String = ""
        Dim current_x_left As Integer = 0
        Dim current_x_right As Integer = 0
        Dim current_y As Integer = 0

        For Each row As KeyValuePair(Of Point, TextProperties) In text_groups
            'Nouvelle ligne
            If Math.Abs(row.Key.Y - current_y) > MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_Y Then
                If word.Length > 0 Then
                    If Not word_list.ContainsKey(New Point(current_x_left, current_y)) Then
                        word_list.Add(New Point(current_x_left, current_y), word)
                    Else
                        word_list(New Point(current_x_left, current_y)) = word
                    End If
                End If
                current_y = row.Key.Y
                current_x_right = row.Value.right_side
                current_x_left = row.Key.X
                word = row.Value.text
            Else
                'Nouvelle phrase
                If Math.Abs(row.Key.X - current_x_right) > MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_X Then
                    If Not word_list.ContainsKey(New Point(current_x_left, current_y)) Then
                        word_list.Add(New Point(current_x_left, current_y), word)
                    Else
                        word_list(New Point(current_x_left, current_y)) = word
                    End If
                    current_x_right = row.Value.right_side
                    current_x_left = row.Key.X
                    word = row.Value.text
                Else
                    'Même phrase/mot
                    current_x_right = row.Value.right_side
                    word += row.Value.text
                End If
            End If
        Next
        If Not word_list.ContainsKey(New Point(current_x_left, current_y)) Then
            word_list.Add(New Point(current_x_left, current_y), word)
        Else
            word_list(New Point(current_x_left, current_y)) = word
        End If
    End Sub
End Class
