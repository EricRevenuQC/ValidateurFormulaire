Imports System.Drawing

Public Interface ITextCleaner
    Function RemoveOffLimitsWords(words() As Dictionary(Of Point, String)) As Dictionary(Of Point, String)()
End Interface
