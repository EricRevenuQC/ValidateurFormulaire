Imports iTextSharp.text.pdf.parser

Public Class TextExtractionStrategy
    Inherits LocationTextExtractionStrategy

    Public rectangle_points As New List(Of TextRectangle)()

    Public Overrides Sub RenderText(render_info As TextRenderInfo)
        MyBase.RenderText(render_info)

        Dim bottom_left = render_info.GetDescentLine().GetStartPoint()
        Dim top_right = render_info.GetAscentLine().GetEndPoint()

        Dim rect = New iTextSharp.text.Rectangle(bottom_left(Vector.I1), bottom_left(Vector.I2), top_right(Vector.I1), top_right(Vector.I2))

        Me.rectangle_points.Add(New TextRectangle(rect, render_info.GetText()))
    End Sub
End Class