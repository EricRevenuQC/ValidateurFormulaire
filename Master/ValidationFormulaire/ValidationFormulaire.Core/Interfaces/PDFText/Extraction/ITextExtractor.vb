Imports System.Web
Imports System.Drawing

Public Interface ITextExtractor
    Sub PDFToText(file As HttpPostedFileBase, reverse_order As TextExtractor.ordering)
    Function GetWords() As Dictionary(Of Point, String)()
End Interface
