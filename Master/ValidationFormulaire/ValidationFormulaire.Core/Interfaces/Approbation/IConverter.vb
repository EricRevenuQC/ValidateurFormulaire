Imports ValidationFormulaire.Core.PDFToImageConverter
Imports System.Web

Public Interface IConverter
    Function PDFToImage(file As HttpPostedFileBase, Optional alert_manager As AlertsManager = Nothing) As Drawing.Image()
    Function GetPageNumber() As Integer
End Interface
