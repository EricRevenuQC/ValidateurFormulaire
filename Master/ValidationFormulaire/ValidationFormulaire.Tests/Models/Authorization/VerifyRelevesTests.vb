Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Drawing
Imports ValidationFormulaire.Core
Imports Moq

<TestClass()> Public Class VerifyRelevesTests
    Private words As Dictionary(Of Point, String)
    Private dictionary_operations As Mock(Of IDictionaryKeys)
    Private releve As Mock(Of IReleves)
    Private verify_releves As VerifyReleves

    <TestInitialize()> _
    Public Sub Initialize()
        dictionary_operations = New Mock(Of IDictionaryKeys)()
        releve = New Mock(Of IReleves)()
        words = New Dictionary(Of Point, String)()

        words.Add(New Point(10, 0), "13RS")
        words.Add(New Point(3, 0), "FS9999999")
        words.Add(New Point(2, 20), "2017")
        words.Add(New Point(5, 20), "Z")
        words.Add(New Point(2, 7), "Province")
        words.Add(New Point(2, 5), "Province")
        words.Add(New Point(1, 22), "Année")
        words.Add(New Point(10, 10), "Code du relevé")

        releve.Setup(Function(f) f.GetNorthMarker()).Returns(New List(Of String)({"Année"}))
        releve.Setup(Function(f) f.GetSouthMarker()).Returns(New List(Of String)({"Province"}))
        releve.Setup(Function(f) f.GetWestMarker()).Returns(New List(Of String)({"Année"}))
        releve.Setup(Function(f) f.GetEastMarker()).Returns(New List(Of String)({"13RS"}))

        releve.Setup(Function(f) f.GetNorthMarkerOccurence()).Returns(New List(Of Integer)({1}))
        releve.Setup(Function(f) f.GetSouthMarkerOccurence()).Returns(New List(Of Integer)({1}))
        releve.Setup(Function(f) f.GetWestMarkerOccurence()).Returns(New List(Of Integer)({1}))
        releve.Setup(Function(f) f.GetEastMarkerOccurence()).Returns(New List(Of Integer)({1}))
    End Sub

    <TestMethod()> Public Sub VerifyThatVerifyTextInputPositionReturnFalseWhenMarkerReferencesAreNotFound()
        Dim test_data As New KeyValuePair(Of String, BarCodeData)("txtCodeGabrt", New BarCodeData With {.value = "13RS"})

        Dim return_nothing As Point? = Nothing
        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "Province", 1)).Returns(return_nothing)
        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "Année", 1)).Returns(return_nothing)
        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "13RS", 1)).Returns(return_nothing)

        verify_releves = New VerifyReleves(releve.Object, dictionary_operations.Object)

        Assert.IsFalse(verify_releves.VerifyTextInputPosition(words, test_data, 0))
    End Sub

    <TestMethod()> Public Sub VerifyThatVerifyRelevesReturnFalseWhenTextIsFoundWhereItShouldBeEmpty()
        Dim test_data As New KeyValuePair(Of String, BarCodeData)("txtAnnee", New BarCodeData With {.value = ""})

        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "Province", 1)).Returns(New Point(5, 0))
        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "Année", 1)).Returns(New Point(1, 20))
        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "13RS", 1)).Returns(New Point(20, 5))

        verify_releves = New VerifyReleves(releve.Object, dictionary_operations.Object)

        Assert.IsFalse(verify_releves.VerifyTextInputPosition(words, test_data, 0))
    End Sub

    <TestMethod()> Public Sub VerifyThatVerifyRelevesReturnTrueWhenTextIsEmptyWhenItShouldBeEmpty()
        Dim test_data As New KeyValuePair(Of String, BarCodeData)("txtAnnee", New BarCodeData With {.value = ""})

        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "Province", 1)).Returns(New Point(5, 0))
        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "Année", 1)).Returns(New Point(1, 10))
        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "13RS", 1)).Returns(New Point(10, 5))

        verify_releves = New VerifyReleves(releve.Object, dictionary_operations.Object)

        Assert.IsTrue(verify_releves.VerifyTextInputPosition(words, test_data, 0))
    End Sub

    <TestMethod()> Public Sub VerifyThatVerifyRelevesReturnTrueWhenWordIsFoundAtTheCorrectPosition()
        Dim test_data As New KeyValuePair(Of String, BarCodeData)("txtAnnee", New BarCodeData With {.value = "2017"})

        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "Province", 1)).Returns(New Point(0, 20))
        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "Année", 1)).Returns(New Point(1, 22))
        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "13RS", 1)).Returns(New Point(10, 5))

        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryValue(words, "2017", 1)).Returns(New Point(2, 20))

        verify_releves = New VerifyReleves(releve.Object, dictionary_operations.Object)

        Assert.IsTrue(verify_releves.VerifyTextInputPosition(words, test_data, 0))
    End Sub

    <TestMethod()> Public Sub VerifyThatVerifyRelevesReturnFalseWhenWordIsNotFoundAtCorrectPosition()
        Dim test_data As New KeyValuePair(Of String, BarCodeData)("txtAnnee", New BarCodeData With {.value = "2017"})

        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "Province", 1)).Returns(New Point(0, 20))
        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "Année", 1)).Returns(New Point(1, 22))
        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryIfContainValue(words, "13RS", 1)).Returns(New Point(10, 5))

        dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryValue(words, "2017", 1)).Returns(New Point(2, 30))

        verify_releves = New VerifyReleves(releve.Object, dictionary_operations.Object)

        Assert.IsFalse(verify_releves.VerifyTextInputPosition(words, test_data, 0))
    End Sub
End Class