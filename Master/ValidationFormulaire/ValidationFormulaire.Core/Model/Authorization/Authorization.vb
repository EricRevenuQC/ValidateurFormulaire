Imports System.Web
Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Imports System.Drawing

Public Class Authorization
    Private extractor As ITextExtractor
    Private bar_code_reader As IBarCodeReader
    Private search_pdf_text As ISearchPDFText
    Private bar_code_converter As IBarCodeConverter
    Private text_cleaner As ITextCleaner
    Private data_dictionary As New Dictionary(Of String, BarCodeData)()
    Private failed_data_dictionary As New Dictionary(Of String, BarCodeData)()

    Private Const ID_INDEX As Integer = 0
    Private Const DESCRIPTION_INDEX As Integer = 1
    Private Const POSITION_INDEX_START As Integer = 2
    Private Const POSITION_INDEX_END As Integer = 3
    Private Const LENGTH_INDEX As Integer = 4
    Private Const CODE_INDEX As Integer = 6

    Sub New()
        extractor = New TextExtractor(New CombineCloseText)
        bar_code_reader = New BarCodeReader()
        search_pdf_text = New SearchPDFText()
        bar_code_converter = New BarCodeConverter()
        text_cleaner = New TextCleaner()
    End Sub

    Sub New(extractor As ITextExtractor, bar_code_reader As IBarCodeReader, search_pdf_text As ISearchPDFText,
            bar_code_converter As IBarCodeConverter, text_cleaner As ITextCleaner)
        Me.extractor = extractor
        Me.bar_code_reader = bar_code_reader
        Me.search_pdf_text = search_pdf_text
        Me.bar_code_converter = bar_code_converter
        Me.text_cleaner = text_cleaner
    End Sub

    Public Sub AuthorizePDF(file As HttpPostedFileBase)
        Dim bar_code_text, bar_code_id As String
        Dim data As DataSet
        Dim pdf_words() As Dictionary(Of Point, String)

        bar_code_reader.ReadBarCode(file)
        bar_code_text = bar_code_reader.GetBarCodeText
        bar_code_id = bar_code_reader.GetBarCodeId
        file.InputStream.Position = 0

        If bar_code_text Is Nothing Then
            AlertsManager.AddAlert("Incapable de lire le code à bar de données!")
        End If

        If bar_code_id Is Nothing Then
            AlertsManager.AddAlert("Incapable de lire le code à bar d'identification!")
        End If

        If bar_code_id Is Nothing OrElse bar_code_text Is Nothing Then
            Exit Sub
        End If

        pdf_words = extractor.PDFToText(file, TextExtractor.ordering.AscendingAscending)

        data = bar_code_converter.ConvertBarCodeToData(bar_code_id)

        FillBarCodeDataIntoDictionary(bar_code_text, data)

        pdf_words = text_cleaner.RemoveOffLimitsWords(pdf_words)

        failed_data_dictionary = search_pdf_text.FindBarCodeValues(pdf_words, data_dictionary)
    End Sub

    Private Sub FillBarCodeDataIntoDictionary(bar_code_text As String, data As DataSet)
        Dim current_string As String
        Dim checked_row As New List(Of String)()
        Try
            For i As Integer = 0 To data.Tables(0).Rows.Count - 1
                If Not IsDBNull(data.Tables(0).Rows(i)(ID_INDEX)) AndAlso Not checked_row.Contains(data.Tables(0).Rows(i)(CODE_INDEX)) Then
                    checked_row.Add(data.Tables(0).Rows(i)(CODE_INDEX))
                    current_string = bar_code_text.Substring(data.Tables(0).Rows(i)(POSITION_INDEX_START) - 1,
                                                                data.Tables(0).Rows(i)(LENGTH_INDEX)).ToString
                    current_string = current_string.Trim()
                    If Not data_dictionary.ContainsKey(data.Tables(0).Rows(i)(CODE_INDEX)) Then
                        data_dictionary.Add(data.Tables(0).Rows(i)(CODE_INDEX),
                                            New BarCodeData With {.value = current_string,
                                                                        .description = data.Tables(0).Rows(i)(DESCRIPTION_INDEX),
                                                                        .start_position = data.Tables(0).Rows(i)(POSITION_INDEX_START),
                                                                        .end_position = data.Tables(0).Rows(i)(POSITION_INDEX_END),
                                                                        .length = data.Tables(0).Rows(i)(LENGTH_INDEX)})
                    End If
                End If
            Next
        Catch ex As Exception
            AlertsManager.AddAlert("Incapable de trouver le gabarit excel associé au code à bar d'identification!")
            System.Diagnostics.Debug.WriteLine(ex)
        End Try
    End Sub

    Public Function GetData() As Dictionary(Of String, BarCodeData)
        Return data_dictionary
    End Function

    Public Function GetFailedData() As Dictionary(Of String, BarCodeData)
        Return failed_data_dictionary
    End Function
End Class
