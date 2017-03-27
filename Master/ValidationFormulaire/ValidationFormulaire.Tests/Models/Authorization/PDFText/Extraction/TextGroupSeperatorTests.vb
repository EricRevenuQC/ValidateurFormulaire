Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
Imports ValidationFormulaire.Core

<TestClass()> Public Class TextGroupSeperatorTests
    Private group_seperator As TextGroupSeperator
    Private text_extraction_strategy As Mock(Of TextExtractionStrategy)

    <TestInitialize()> _
    Public Sub Initialize()
        group_seperator = New TextGroupSeperator()
        text_extraction_strategy = New Mock(Of TextExtractionStrategy)()
    End Sub

    <TestMethod()> Public Sub TestMethod1()
        group_seperator.SeperateTextIntoGroups(text_extraction_strategy.Object)
    End Sub
End Class