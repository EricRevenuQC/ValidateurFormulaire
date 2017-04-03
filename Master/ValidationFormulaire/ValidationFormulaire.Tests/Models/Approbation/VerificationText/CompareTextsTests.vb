Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports ValidationFormulaire.Core
Imports ValidationFormulaire.Core.CompareTexts
Imports System.IO

<TestClass()> Public Class CompareTextsTests
    Private diff As CompareTexts

    <TestMethod()> Public Sub CompareTextsTests()
        diff = New CompareTexts()
        Dim text1 As String = "This long text has some text in the middle that will be found by LCS"
        Dim text2 As String = "This extra long text has some text in the middle that should be found by LCS"

        diff.CompareTexts(text1, text2)
    End Sub
End Class