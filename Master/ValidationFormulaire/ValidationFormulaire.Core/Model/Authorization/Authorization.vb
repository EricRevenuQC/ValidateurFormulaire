Imports System.Web
Imports System.Data.OleDb
Imports System.Text.RegularExpressions

Public Class Authorization
    Private extractor As New TextExtractor()
    Private text_adjustor As New TextAdjuster()
    Private bar_code_reader As New BarCodeReader()

    Private Const ID_INDEX As Integer = 0
    Private Const DESCRIPTION_INDEX As Integer = 1
    Private Const POSITION_INDEX_START As Integer = 2
    Private Const POSITION_INDEX_END As Integer = 3
    Private Const LENGTH_INDEX As Integer = 4
    Private Const CODE_INDEX As Integer = 6

    Public Function AuthorizePDF(file As HttpPostedFileBase) As Dictionary(Of String, BarCodeData)
        Dim pdf_text(), bar_code_text, bar_code_id As String
        Dim data As DataSet

        bar_code_reader.ReadBarCode(file)
        bar_code_text = bar_code_reader.GetBarCodeText
        bar_code_id = bar_code_reader.GetBarCodeId
        file.InputStream.Position = 0
        pdf_text = extractor.PDFToText(file)
        data = New BarCodeFormatImporter().ImportExcelData(bar_code_id)

        Return FillBarCodeDataIntoDictionary(bar_code_text, pdf_text, data)
    End Function

    Private Function FillBarCodeDataIntoDictionary(bar_code_text As String,
                                                   pdf_text() As String,
                                                   data As DataSet) As Dictionary(Of String, BarCodeData)
        Dim current_string As String
        Dim checked_row As New List(Of String)()
        Dim data_dictionary As New Dictionary(Of String, BarCodeData)()

        Try
            For i As Integer = 0 To data.Tables(0).Rows.Count - 1
                If Not IsDBNull(data.Tables(0).Rows(i)(ID_INDEX)) AndAlso Not checked_row.Contains(data.Tables(0).Rows(i)(CODE_INDEX)) Then
                    checked_row.Add(data.Tables(0).Rows(i)(CODE_INDEX))
                    current_string = bar_code_text.Substring(data.Tables(0).Rows(i)(POSITION_INDEX_START) - 1,
                                                                data.Tables(0).Rows(i)(LENGTH_INDEX)).ToString
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
            AlertsManager.AddAlert("Incapable de trouver le code à bar ou son gabarit excel associé!")
            System.Diagnostics.Debug.WriteLine(ex)
        End Try
        Return data_dictionary
    End Function
End Class
