Imports System.Drawing

Public Class TextGroupSeperator
    Implements ITextGroupSeperator

    Private text_groups As Dictionary(Of Point, TextProperties)
    Private word_list As Dictionary(Of Point, String)

    Private Const MAX_DISTANCE_BETWEEN_CHARACTERS As Integer = 10

    Public Function SeperateTextIntoGroups(text_extraction_strategy As TextExtractionStrategy) _
            As Dictionary(Of Point, String) Implements ITextGroupSeperator.SeperateTextIntoGroups
        Dim text_properties As TextProperties
        word_list = New Dictionary(Of Point, String)()
        text_groups = New Dictionary(Of Point, TextProperties)()

        For Each text_rectangle As TextRectangle In text_extraction_strategy.rectangle_points
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
            If row.Key.Y <> current_y Then
                If word.Length > 0 Then
                    word_list.Add(New Point(current_x_left, current_y), word)
                End If
                current_y = row.Key.Y
                current_x_right = row.Value.right_side
                current_x_left = row.Key.X
                word = row.Value.text
            Else
                'Nouvelle phrase
                If (row.Key.X - current_x_right) > MAX_DISTANCE_BETWEEN_CHARACTERS Then
                    word_list.Add(New Point(current_x_left, current_y), word)
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
    End Sub
End Class
