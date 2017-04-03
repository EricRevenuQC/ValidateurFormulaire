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
    Private combine_text As ICombineText

    Sub New(combine_text As ICombineText)
        text_seperator = New TextGroupSeperator()
        text_extraction_strategy = New TextExtractionStrategy()
        Me.combine_text = combine_text
    End Sub

    Sub New(text_seperator As ITextGroupSeperator, text_extraction_strategy As TextExtractionStrategy)
        Me.text_seperator = text_seperator
        Me.text_extraction_strategy = text_extraction_strategy
    End Sub

    Public Function PDFToText(file As HttpPostedFileBase, reverse_order As Boolean) As Dictionary(Of Point, String)() Implements ITextExtractor.PDFToText
        Using reader As New PdfReader(file.InputStream)
            Dim text_result(reader.NumberOfPages) As String
            Dim words As New Dictionary(Of Point, TextProperties)()
            Dim combined_words(reader.NumberOfPages) As Dictionary(Of Point, String)

            For i As Integer = 1 To reader.NumberOfPages
                text_extraction_strategy = New TextExtractionStrategy()
                text_result(i) = PdfTextExtractor.GetTextFromPage(reader, i, text_extraction_strategy)
                words = text_seperator.SeperateTextIntoGroups(text_extraction_strategy)
                If reverse_order Then
                    words = words.OrderBy(Function(x) x.Key.Y).ThenBy(Function(x) x.Key.X).ToDictionary(Function(x) x.Key, Function(y) y.Value)
                End If
                combined_words(i) = combine_text.CombineText(words)
            Next

            Return combined_words
        End Using
    End Function
End Class
