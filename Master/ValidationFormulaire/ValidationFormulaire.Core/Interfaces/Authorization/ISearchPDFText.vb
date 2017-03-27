Imports System.Drawing

Public Interface ISearchPDFText
    Function FindBarCodeValues(words() As Dictionary(Of Point, String),
                                      bar_code_data As Dictionary(Of String, BarCodeData)) As Dictionary(Of String, BarCodeData)
    Function AdjustWords(words As Dictionary(Of Point, String)) As Dictionary(Of Point, String)
End Interface
