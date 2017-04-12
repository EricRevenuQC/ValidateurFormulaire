Imports System.Web
Imports System.Drawing

Public Interface ITextExtractor
    Function PDFToText(file As HttpPostedFileBase, reverse_order As Boolean) As Dictionary(Of Point, String)()
End Interface
