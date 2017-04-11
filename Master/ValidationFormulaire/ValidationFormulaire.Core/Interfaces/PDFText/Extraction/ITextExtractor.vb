Imports System.Web
Imports System.Drawing

Public Interface ITextExtractor
    Function PDFToText(file As HttpPostedFileBase, reverse_order As TextExtractor.ordering) As Dictionary(Of Point, String)()
End Interface
