Imports System.Web
Imports System.Drawing

Public Interface ITextExtractor
    Function PDFToText(file As HttpPostedFileBase) As Dictionary(Of Point, String)()
End Interface
