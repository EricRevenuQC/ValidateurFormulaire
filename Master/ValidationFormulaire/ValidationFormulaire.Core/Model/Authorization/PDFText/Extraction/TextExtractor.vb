Imports iTextSharp
Imports System.Web
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports System.Text
Imports System.Drawing

Public Class TextExtractor
    Implements ITextExtractor

    Private text_seperator As ITextGroupSeperator
    Private text_extraction_strategy As TextExtractionStrategy

    Sub New()
        text_seperator = New TextGroupSeperator()
        text_extraction_strategy = New TextExtractionStrategy()
    End Sub

    Sub New(text_seperator As ITextGroupSeperator, text_extraction_strategy As TextExtractionStrategy)
        Me.text_seperator = text_seperator
        Me.text_extraction_strategy = text_extraction_strategy
    End Sub

    Public Function PDFToText(file As HttpPostedFileBase) As Dictionary(Of Point, String)() Implements ITextExtractor.PDFToText
        Using reader As New PdfReader(file.InputStream)
            Dim text_result(reader.NumberOfPages) As String
            Dim words(reader.NumberOfPages) As Dictionary(Of Point, String)

            For i As Integer = 1 To reader.NumberOfPages
                text_result(i) = PdfTextExtractor.GetTextFromPage(reader, i, text_extraction_strategy)
                words(i) = text_seperator.SeperateTextIntoGroups(text_extraction_strategy)
            Next

            Return words
        End Using
    End Function
End Class
