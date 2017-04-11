Imports System.Web
Imports System.Drawing
Imports System.Text

Public Class VerificationText
    Implements IVerificationText

    Private extractor As ITextExtractor
    Private verify_texts As VerifyTexts
    Private compare_texts As CompareTexts

    Sub New()
        extractor = New TextExtractor(New CombineLine)
        verify_texts = New VerifyTexts()
        compare_texts = New CompareTexts()
    End Sub

    Sub New(extractor As ITextExtractor)
        Me.extractor = extractor
    End Sub

    Public Function VerificationText(file As HttpPostedFileBase, file_template As HttpPostedFileBase) _
            As String() Implements IVerificationText.VerificationText
        Dim pdf_words() As Dictionary(Of Point, String)
        Dim pdf_words_template() As Dictionary(Of Point, String)

        pdf_words = extractor.PDFToText(file, TextExtractor.ordering.DescendingAscending)
        pdf_words_template = extractor.PDFToText(file_template, TextExtractor.ordering.DescendingAscending)

        Dim text1(pdf_words.Count - 1) As String
        Dim text2(pdf_words_template.Count - 1) As String
        Dim new_text(If(pdf_words.Count >= pdf_words_template.Count, pdf_words_template.Count - 1, pdf_words.Count - 1)) As String

        'For page As Integer = 1 To new_text.Count - 1
        '    For Each data2 As KeyValuePair(Of Point, String) In pdf_words(page)
        '        System.Diagnostics.Debug.WriteLine("Word : " + data2.Value + " at : " + data2.Key.X.ToString + "," + data2.Key.Y.ToString)
        '    Next
        '    For Each data2 As KeyValuePair(Of Point, String) In pdf_words_template(page)
        '        System.Diagnostics.Debug.WriteLine("Word : " + data2.Value + " at : " + data2.Key.X.ToString + "," + data2.Key.Y.ToString)
        '    Next
        'Next

        For page As Integer = 1 To new_text.Count - 1
            For Each word As KeyValuePair(Of Point, String) In pdf_words(page)
                text1(page) += (word.Value) + "¶"
            Next
        Next
        For page As Integer = 1 To new_text.Count - 1
            For Each word As KeyValuePair(Of Point, String) In pdf_words_template(page)
                text2(page) += (word.Value) + "¶"
            Next
        Next

        For page As Integer = 1 To new_text.Count - 1
            text1(page) = text1(page).Replace(vbTab, " ")
            text2(page) = text2(page).Replace(vbTab, " ")
            new_text(page) = compare_texts.CompareTexts(text1(page), text2(page))
        Next

        Return new_text
    End Function

    Private Function TrimWords(words() As Dictionary(Of Point, String)) As Dictionary(Of Point, String)()
        Dim words_adjusted As New Dictionary(Of Point, String)()

        For page As Integer = 1 To words.Count - 1
            words_adjusted = New Dictionary(Of Point, String)(words(page))

            For Each word As KeyValuePair(Of Point, String) In words_adjusted
                words(page)(word.Key) = word.Value.Trim
            Next
        Next

        Return words
    End Function
End Class
