Imports iTextSharp
Imports System.Web
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports System.Text
Imports System.Drawing

Public Class TextExtractor
    Implements ITextExtractor

    Public Function PDFToText(file As HttpPostedFileBase) As Dictionary(Of Point, String)() Implements ITextExtractor.PDFToText
        Using reader As New PdfReader(file.InputStream)
            Dim text_result(reader.NumberOfPages) As String
            Dim text_extraction_strategy As New TextExtractionStrategy()
            Dim text_seperator As New TextGroupSeperator()
            Dim words(reader.NumberOfPages) As Dictionary(Of Point, String)

            For i As Integer = 1 To reader.NumberOfPages
                text_result(i) = PdfTextExtractor.GetTextFromPage(reader, i, text_extraction_strategy)
                words(i) = text_seperator.SeperateTextIntoGroups(text_extraction_strategy)
            Next

            Return words
        End Using
    End Function
End Class
