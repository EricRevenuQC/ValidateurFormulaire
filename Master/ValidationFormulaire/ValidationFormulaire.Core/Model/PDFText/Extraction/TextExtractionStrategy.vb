Imports iTextSharp.text.pdf.parser
Imports System.Drawing

Public Class TextExtractionStrategy
    Inherits LocationTextExtractionStrategy
    Implements ITextExtractionStrategy


    Private rectangle_points As New List(Of TextRectangle)()

    Public Overrides Sub RenderText(render_info As TextRenderInfo) Implements ITextExtractionStrategy.RenderText
        MyBase.RenderText(render_info)

        Dim bottom_left = render_info.GetDescentLine().GetStartPoint()
        Dim top_right = render_info.GetAscentLine().GetEndPoint()
        Dim text As String = render_info.GetText()

        Dim rect = New iTextSharp.text.Rectangle(bottom_left(Vector.I1), bottom_left(Vector.I2), top_right(Vector.I1), top_right(Vector.I2))

        Me.rectangle_points.Add(New TextRectangle(rect, text, render_info.GetFont.PostscriptFontName))

        If render_info.GetFont.PostscriptFontName.ToLower.Contains("bold") Then
        End If
        If render_info.GetFont.PostscriptFontName.ToLower.Contains("italic") Then
        End If
    End Sub

    Public Function GetRectanglePoints() As List(Of TextRectangle) Implements ITextExtractionStrategy.GetRectanglePoints
        Return rectangle_points
    End Function
End Class