Imports System.Drawing

Public Interface ITextGroupSeperator
    Function SeperateTextIntoGroups(text_extraction_strategy As ITextExtractionStrategy) As Dictionary(Of Point, String)
End Interface
