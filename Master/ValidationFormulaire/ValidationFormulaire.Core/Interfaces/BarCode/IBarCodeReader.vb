Imports System.Web

Public Interface IBarCodeReader
    Sub ReadBarCode(file As HttpPostedFileBase)
    Function GetBarCodeText()
    Function GetBarCodeId()
End Interface
