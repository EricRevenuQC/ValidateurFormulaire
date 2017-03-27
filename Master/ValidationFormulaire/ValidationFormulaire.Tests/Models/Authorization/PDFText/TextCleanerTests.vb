Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
Imports ValidationFormulaire.Core
Imports System.Drawing

<TestClass()> Public Class TextCleanerTests
    Private text_cleaner As TextCleaner
    Private dictionary_operations As Mock(Of IDictionaryKeys)

    <TestInitialize()> _
    Public Sub Initialize()
        dictionary_operations = New Mock(Of IDictionaryKeys)()
    End Sub

    <TestMethod()> Public Sub VerifyThatRemoveOffLimitsWordsRemoveCorrectWords()
        Dim words(1) As Dictionary(Of Point, String)
        Dim results(1) As Dictionary(Of Point, String)

        words(1) = New Dictionary(Of Point, String)()
        words(1).Add(New Point(0, 50), "Test1")
        words(1).Add(New Point(0, 150), "Test2")
        words(1).Add(New Point(100, 100), "Test3")
        words(1).Add(New Point(200, 50), "Test4")
        words(1).Add(New Point(200, 150), "Test5")

        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryValue(words(1), "Année")).Returns(New Point(0, 100))

        text_cleaner = New TextCleaner(dictionary_operations.Object)

        results = text_cleaner.RemoveOffLimitsWords(words)

        Assert.AreEqual(results(1).ElementAt(0).Value, "Test1")
        Assert.AreEqual(results(1).ElementAt(1).Value, "Test3")
        Assert.AreEqual(results(1).ElementAt(2).Value, "Test4")
    End Sub
End Class