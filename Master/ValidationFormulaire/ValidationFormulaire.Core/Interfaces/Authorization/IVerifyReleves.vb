Imports ValidationFormulaire.Core.PDFConverter
Imports System.Web
Imports System.Drawing

Public Interface IVerifyReleves
    Function VerifyTextInputPosition(words As Dictionary(Of Point, String),
                                       bar_code_data As KeyValuePair(Of String, BarCodeData),
                                       bar_code_index As Integer) As Boolean
End Interface
