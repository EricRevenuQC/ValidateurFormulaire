Imports ValidationFormulaire.Core.PDFConverter
Imports System.Web

Public Interface IConverter
    Function PDFToImage(file As HttpPostedFileBase) As Drawing.Image()
    Function GetPageNumber() As Integer
End Interface
