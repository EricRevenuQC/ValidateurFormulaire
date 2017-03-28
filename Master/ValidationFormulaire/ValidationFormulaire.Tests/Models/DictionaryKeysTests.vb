Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports ValidationFormulaire.Core
Imports System.Drawing

<TestClass()> Public Class DictionaryKeysTests
    Private dictionary_operations As DictionaryKeys

    <TestInitialize()> _
    Public Sub Initialize()
        dictionary_operations = New DictionaryKeys()
    End Sub

    <TestMethod()> Public Sub VerifyThatGetValueOfKeyAsStringReturnCorrectValueWhenKeyIsFound()
        Dim dict As New Dictionary(Of String, BarCodeData)()
        Dim expected_result As String

        dict.Add("txtA", New BarCodeData With {.value = "test"})

        expected_result = dictionary_operations.GetValueOfKeyAsString(BarCodeProperties.txtA, dict)

        Assert.AreEqual(expected_result, "test")
    End Sub

    <TestMethod()> Public Sub VerifyThatGetValueOfKeyAsStringReturnEmptyStringWhenKeyIsNotFound()
        Dim dict As New Dictionary(Of String, BarCodeData)()
        Dim expected_result As String

        dict.Add("txtA", New BarCodeData With {.value = "test"})

        expected_result = dictionary_operations.GetValueOfKeyAsString(BarCodeProperties.txtAdresseEmployeur, dict)

        Assert.AreEqual(expected_result, "")
    End Sub

    <TestMethod()> Public Sub VerifyThatGetKeyFromDictionaryValueReturnCorrectKeyWhenValueIsFoundForFirstOccurence()
        Dim dict As New Dictionary(Of Point, String)()
        Dim expected_result As Point

        dict.Add(New Point(0, 0), "test")
        dict.Add(New Point(0, 1), "test2")
        dict.Add(New Point(1, 0), "test3")
        dict.Add(New Point(1, 1), "test")

        expected_result = dictionary_operations.GetKeyFromDictionaryValue(dict, "test")

        Assert.AreEqual(expected_result, New Point(0, 0))
    End Sub

    <TestMethod()> Public Sub VerifyThatGetKeyFromDictionaryValueReturnCorrectKeyWhenValueIsFoundForSecondOccurence()
        Dim dict As New Dictionary(Of Point, String)()
        Dim expected_result As Point

        dict.Add(New Point(0, 0), "test")
        dict.Add(New Point(0, 1), "test2")
        dict.Add(New Point(1, 0), "test3")
        dict.Add(New Point(1, 1), "test")

        expected_result = dictionary_operations.GetKeyFromDictionaryValue(dict, "test", 2)

        Assert.AreEqual(expected_result, New Point(1, 1))
    End Sub

    <TestMethod()> Public Sub VerifyThatGetKeyFromDictionaryValueReturnNothingWhenValueIsNotFound()
        Dim dict As New Dictionary(Of Point, String)()
        Dim expected_result As Point

        dict.Add(New Point(0, 0), "test")
        dict.Add(New Point(0, 1), "test2")
        dict.Add(New Point(1, 0), "test3")
        dict.Add(New Point(1, 1), "test")

        expected_result = dictionary_operations.GetKeyFromDictionaryValue(dict, "test4")

        Assert.AreEqual(expected_result, Nothing)
    End Sub

    <TestMethod()> Public Sub VerifyThatGetKeyFromDictionaryIfCountainValueReturnCorrectValueWhenValueIsFoundForFirstOccurence()
        Dim dict As New Dictionary(Of Point, String)()
        Dim expected_result As Point

        dict.Add(New Point(0, 0), "test")
        dict.Add(New Point(0, 1), "test2")
        dict.Add(New Point(1, 0), "test3")
        dict.Add(New Point(1, 1), "test")

        expected_result = dictionary_operations.GetKeyFromDictionaryIfContainValue(dict, "te")

        Assert.AreEqual(expected_result, New Point(0, 0))
    End Sub

    <TestMethod()> Public Sub VerifyThatGetKeyFromDictionaryIfCountainValueReturnCorrectValueWhenValueIsFoundForSecondOccurence()
        Dim dict As New Dictionary(Of Point, String)()
        Dim expected_result As Point

        dict.Add(New Point(0, 0), "test")
        dict.Add(New Point(0, 1), "test2")
        dict.Add(New Point(1, 0), "test3")
        dict.Add(New Point(1, 1), "test")

        expected_result = dictionary_operations.GetKeyFromDictionaryIfContainValue(dict, "te", 2)

        Assert.AreEqual(expected_result, New Point(0, 1))
    End Sub

    <TestMethod()> Public Sub VerifyThatGetKeyFromDictionaryIfCountainValueReturnNothingWhenValueIsNotFound()
        Dim dict As New Dictionary(Of Point, String)()
        Dim expected_result As Point?

        dict.Add(New Point(0, 0), "test")
        dict.Add(New Point(0, 1), "test2")
        dict.Add(New Point(1, 0), "test3")
        dict.Add(New Point(1, 1), "test")

        expected_result = dictionary_operations.GetKeyFromDictionaryIfContainValue(dict, "blabla")

        Assert.AreEqual(expected_result, Nothing)
    End Sub

    <TestMethod()> Public Sub VerifyThatGetKeyFromDictionaryIfCountainValueReturnCorrectValueWhenValueIsFoundForLastOccurence()
        Dim dict As New Dictionary(Of Point, String)()
        Dim expected_result As Point

        dict.Add(New Point(0, 0), "test")
        dict.Add(New Point(0, 1), "test2")
        dict.Add(New Point(1, 0), "test3")
        dict.Add(New Point(1, 1), "test")

        expected_result = dictionary_operations.GetKeyFromDictionaryIfContainValue(dict, "te", 0)

        Assert.AreEqual(expected_result, New Point(1, 1))
    End Sub
End Class