Imports System.Drawing

Public Interface ITextCleaner
    Function CleanPDFWords(words() As Dictionary(Of Point, String)) As Dictionary(Of Point, String)()
End Interface
