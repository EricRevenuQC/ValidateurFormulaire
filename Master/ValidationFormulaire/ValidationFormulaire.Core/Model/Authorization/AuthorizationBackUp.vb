'Imports System.Web
'Imports System.Data.OleDb
'Imports System.Text.RegularExpressions

'Public Class Authorization2
'    Private extractor As New TextExtractor()
'    Private text_adjustor As New TextAdjuster()
'    Private bar_code_reader As New BarCodeReader()
'    Private is_first_word As Boolean

'    Private Const ID_INDEX As Integer = 0
'    Private Const DESCRIPTION_INDEX As Integer = 1
'    Private Const POSITION_INDEX_START As Integer = 2
'    Private Const POSITION_INDEX_END As Integer = 3
'    Private Const LENGTH_INDEX As Integer = 4
'    Private Const CODE_INDEX As Integer = 6

'    Public Function AuthorizePDF(file As HttpPostedFileBase) As Dictionary(Of String, String)
'        Dim pdf_text(), bar_code_text, bar_code_id As String
'        Dim data As DataSet

'        bar_code_reader.ReadBarCode(file)
'        bar_code_text = bar_code_reader.GetBarCodeText
'        bar_code_id = bar_code_reader.GetBarCodeId
'        file.InputStream.Position = 0
'        pdf_text = extractor.PDFToText(file)
'        data = New BarCodeImporter().ImportExcelData(bar_code_id)

'        Return SearchForAllBarCodeCharactersInPDFText(bar_code_text, pdf_text, data)
'    End Function

'    Private Function SearchForAllBarCodeCharactersInPDFText(bar_code_text As String, pdf_text() As String,
'                                                            data As DataSet) As Dictionary(Of String, String)
'        Dim chars_not_found_remaining, current_string As String
'        Dim checked_row As List(Of String)
'        Dim data_dictionary As New Dictionary(Of String, String)()
'        Dim search_text As SearchForText
'        is_first_word = True

'        'System.Diagnostics.Debug.WriteLine(bar_code_text)

'        For current_page As Integer = 1 To pdf_text.Length - 1 Step 1
'            chars_not_found_remaining = ""
'            checked_row = New List(Of String)()
'            search_text = New SearchForText()

'            pdf_text(current_page) = text_adjustor.RemoveCharsFromNumbersInString({",", ".", " "}, pdf_text(current_page))

'            For i As Integer = 0 To data.Tables(0).Rows.Count - 1
'                If Not IsDBNull(data.Tables(0).Rows(i)(ID_INDEX)) AndAlso Not checked_row.Contains(data.Tables(0).Rows(i)(CODE_INDEX)) Then
'                    checked_row.Add(data.Tables(0).Rows(i)(CODE_INDEX))
'                    current_string = bar_code_text.Substring(data.Tables(0).Rows(i)(POSITION_INDEX_START) - 1,
'                                                                data.Tables(0).Rows(i)(LENGTH_INDEX)).ToString
'                    If Not String.IsNullOrWhiteSpace(current_string) Then
'                        pdf_text(current_page) = search_text.RemoveStringFromString(current_string, pdf_text(current_page),
'                                                                                    save_remaining_chars:=True)
'                    End If
'                    If Not data_dictionary.ContainsKey(data.Tables(0).Rows(i)(CODE_INDEX)) Then
'                        data_dictionary.Add(data.Tables(0).Rows(i)(CODE_INDEX), current_string)
'                    End If
'                End If
'            Next

'            If Not pdf_text(current_page) = String.Empty Then
'                chars_not_found_remaining = search_text.GetRemainingCharacters

'                chars_not_found_remaining = search_text.RemoveStringFromString(pdf_text(current_page), chars_not_found_remaining,
'                                                                               char_minimum_length:=1)


'                FindMissingCharIndex(chars_not_found_remaining, bar_code_text, data, pdf_text.Length - 1, current_page)

'                System.Diagnostics.Debug.WriteLine(chars_not_found_remaining)
'                System.Diagnostics.Debug.WriteLine("Number of characters not found : " + chars_not_found_remaining.Length.ToString)
'            Else
'                AlertsManager.AddAlert(New AlertMessages().GetImageFormatMsg)
'                Exit For
'            End If
'        Next

'        Return data_dictionary
'    End Function

'    Private Sub FindMissingCharIndex(to_find As String, to_search As String, data As DataSet,
'                                     Optional page_number As Integer = 1, Optional current_page As Integer = 1)
'        Dim current_word As String = ""
'        Dim empty_word As String = ""
'        Dim new_page As Boolean = True
'        Dim bar_code_word, bar_code_current_word As String

'        For Each character As Char In to_find
'            If Not character = Environment.NewLine AndAlso Not character = vbCrLf AndAlso
'                    Not character = vbCr AndAlso Not character = vbLf AndAlso Not character = " " Then
'                current_word += character
'            Else
'                If current_word.Length > 0 Then
'                    For i As Integer = 0 To data.Tables(0).Rows.Count - 1
'                        If Not IsDBNull(data.Tables(0).Rows(i)(POSITION_INDEX_START)) AndAlso
'                                data.Tables(0).Rows(i)(POSITION_INDEX_START) <= (to_search.IndexOf(current_word) + 1) AndAlso
'                                data.Tables(0).Rows(i)(POSITION_INDEX_END) >= (to_search.IndexOf(current_word) + 1) Then

'                            'Make sure it is the exact word found in the bar code
'                            bar_code_current_word = ""
'                            bar_code_word = to_search.Substring(data.Tables(0).Rows(i)(POSITION_INDEX_START) - 1, data.Tables(0).Rows(i)(LENGTH_INDEX))
'                            For Each bar_code_chars As Char In bar_code_word
'                                If Not bar_code_chars = Environment.NewLine AndAlso Not bar_code_chars = vbCrLf AndAlso
'                                            Not bar_code_chars = vbCr AndAlso Not bar_code_chars = vbLf AndAlso Not bar_code_chars = " " Then
'                                    bar_code_current_word += bar_code_chars
'                                End If
'                            Next

'                            If current_word = bar_code_current_word Then
'                                'Add an alert message for missing word
'                                If page_number > 1 AndAlso new_page = True Then
'                                    AlertsManager.AddAlert(New AlertMessages().GetCharNotFoundMsg,
'                                                            "Page " + current_page.ToString + " :" + Environment.NewLine +
'                                                            data.Tables(0).Rows(i)(DESCRIPTION_INDEX) + " : " + current_word)
'                                    new_page = False
'                                Else
'                                    AlertsManager.AddAlert(New AlertMessages().GetCharNotFoundMsg,
'                                                            data.Tables(0).Rows(i)(DESCRIPTION_INDEX) + " : " + current_word)
'                                End If

'                                'Replace the word with an emtpy string
'                                For j As Integer = 1 To current_word.Length
'                                    empty_word += " "
'                                Next
'                                to_search = ReplaceFirstInstance(to_search, current_word, empty_word)
'                                Exit For
'                            Else
'                                'Word not found?
'                                'Drop reliability
'                                System.Diagnostics.Debug.WriteLine("Word not found : " + current_word)
'                            End If
'                        End If
'                    Next
'                End If
'                current_word = ""
'                empty_word = ""
'            End If
'        Next
'    End Sub

'    Private Function ReplaceFirstInstance(source As String, find As String, replace As String) As String
'        Dim index As Integer = source.IndexOf(find)
'        Return If(index < 0, source, (source.Substring(0, index) & replace) + source.Substring(index + find.Length))
'    End Function
'End Class
