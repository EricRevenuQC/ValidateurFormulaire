Imports System.Drawing

Public Class TextWordSeperator

    Private text_groups As Dictionary(Of Point, TextProperties)
    Private word_list As Dictionary(Of Point, String)

    Private Const MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_X As Integer = 10
    Private Const MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_Y As Integer = 5

    Public Function SeperateTextIntoGroups(text_extraction_strategy As ITextExtractionStrategy) _
            As Dictionary(Of Point, String)
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

        SeperateTextIntoWords()
        Return word_list
    End Function

    Private Sub SeperateTextIntoWords()
        Dim words() As String
        Dim pixel_width As Single

        For Each text As KeyValuePair(Of Point, TextProperties) In text_groups
            words = text.Value.text.Split(" ")
            System.Diagnostics.Debug.WriteLine(text.Value.text + " length : " + text.Value.text.Length.ToString)
            pixel_width = ((text.Value.right_side - text.Key.X) / text.Value.text.Length)
            For Each word As String In words
                word_list.Add(text.Key, word)
            Next
        Next
    End Sub
End Class
