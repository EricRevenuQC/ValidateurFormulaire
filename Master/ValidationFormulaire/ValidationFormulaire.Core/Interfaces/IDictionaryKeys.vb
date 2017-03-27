Imports ValidationFormulaire.Core.PDFConverter
Imports System.Web
Imports System.Drawing

Public Interface IDictionaryKeys
    Function GetValueOfKeyAsString(bar_code_property As BarCodeProperties, dict As Dictionary(Of String, BarCodeData)) As String
    Function GetKeyFromDictionaryValue(dict As Dictionary(Of Point, String),
                                       value As String, Optional occurence As Integer = 1) As Point
    Function GetKeyFromDictionaryIfContainValue(dict As Dictionary(Of Point, String), value As String,
                                                Optional occurence As Integer = 1) As Point?
End Interface
