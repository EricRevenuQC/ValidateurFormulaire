Imports System.Drawing

Public Interface ICombineText
    Function CombineText(text_groups As Object, Optional bold As Boolean = False,
                         Optional italic As Boolean = False) As Object
End Interface
