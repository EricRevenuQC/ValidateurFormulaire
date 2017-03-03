Imports System.Reflection

Public Class RequireRequestValueAttribute
    Inherits ActionMethodSelectorAttribute

    Public ValueName As String

    Public Sub New(valueName__1 As String)
        ValueName = valueName__1
    End Sub

    Public Overrides Function IsValidForRequest(controllerContext As ControllerContext, methodInfo As MethodInfo) As Boolean
        Return (controllerContext.HttpContext.Request(ValueName) IsNot Nothing)
    End Function
End Class