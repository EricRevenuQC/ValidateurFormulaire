Imports iTextSharp
Imports System.Web
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports System.Text

Public Class TextExtractor
    Public Function PDFToText(file As HttpPostedFileBase) As String()
        Using reader As New PdfReader(file.InputStream)
            Dim text_result(reader.NumberOfPages) As String

            For i As Integer = 1 To reader.NumberOfPages
                text_result(i) = PdfTextExtractor.GetTextFromPage(reader, i)
            Next

            Return text_result
        End Using
    End Function
End Class
