Imports ValidationFormulaire.Core.PDFConverter
Imports System.Web
Imports System.Drawing

Public Interface IVerifyReleves
    Function VerifyTextInputPosition(words As Dictionary(Of Point, String),
                                       bar_code_data As KeyValuePair(Of String, BarCodeData),
                                       bar_code_index As Integer) As Boolean
    Function GetSuccessfulData() As Integer
    Function GetFailedData() As Integer
    Function GetNotFoundData() As Integer
    Function GetUncertainData() As Integer
    Sub AddUncertainData()
End Interface
