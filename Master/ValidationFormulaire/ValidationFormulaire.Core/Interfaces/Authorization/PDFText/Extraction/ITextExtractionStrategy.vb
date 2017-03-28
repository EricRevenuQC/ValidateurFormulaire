Imports iTextSharp.text.pdf.parser

Public Interface ITextExtractionStrategy
    Sub RenderText(render_info As TextRenderInfo)
    Function GetRectanglePoints() As List(Of TextRectangle)
End Interface
