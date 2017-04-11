Imports System.Drawing

Public Class TextGroupSeperator
    Implements ITextGroupSeperator

    Private text_groups As Dictionary(Of Point, TextProperties)
    Private word_list As Dictionary(Of Point, String)

    Public Function SeperateTextIntoGroups(text_extraction_strategy As ITextExtractionStrategy) _
            As Dictionary(Of Point, TextProperties) Implements ITextGroupSeperator.SeperateTextIntoGroups
        Dim text_properties As TextProperties
        word_list = New Dictionary(Of Point, String)()
        text_groups = New Dictionary(Of Point, TextProperties)()

        For Each text_rectangle As TextRectangle In text_extraction_strategy.GetRectanglePoints
            text_properties = New TextProperties()
            text_properties.text = text_rectangle.text
            text_properties.right_side = text_rectangle.rectangle.Right
            text_properties.bold = False
            text_properties.italic = False
            If text_rectangle.font.ToLower.Contains("bold") Then
                text_properties.bold = True
            End If
            If text_rectangle.font.ToLower.Contains("italic") Then
                text_properties.italic = True
            End If

            If Not text_groups.ContainsKey(New Point(text_rectangle.rectangle.Left, text_rectangle.rectangle.Bottom)) Then
                text_groups.Add(New Point(text_rectangle.rectangle.Left, text_rectangle.rectangle.Bottom), text_properties)
            End If
        Next

        Return text_groups
    End Function
End Class
