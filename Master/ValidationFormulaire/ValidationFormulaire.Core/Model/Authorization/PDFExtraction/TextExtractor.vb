Imports iTextSharp
Imports System.Web
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports System.Text
Imports System.Drawing

Public Class TextExtractor
    Public Function PDFToText(file As HttpPostedFileBase) As Dictionary(Of Point, String)
        Using reader As New PdfReader(file.InputStream)
            Dim text_result(reader.NumberOfPages) As String
            Dim text_extraction_strategy As New TextExtractionStrategy()
            Dim text_seperator As New TextGroupSeperator()
            Dim words As New Dictionary(Of Point, String)()

            For i As Integer = 1 To reader.NumberOfPages
                text_result(i) = PdfTextExtractor.GetTextFromPage(reader, i, text_extraction_strategy)
            Next

            words = text_seperator.SeperateTextIntoGroups(text_extraction_strategy)

            Return words
        End Using
    End Function
End Class
