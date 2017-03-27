Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Drawing
Imports ValidationFormulaire.Core
Imports Moq

<TestClass()> Public Class SearchPDFTextTests
    Private words(1) As Dictionary(Of Point, String)
    Private bar_code_data As Dictionary(Of String, BarCodeData)
    Private search_pdf_text As SearchPDFText
    Private dictionary_operations As Mock(Of IDictionaryKeys)
    Private verify_releve As Mock(Of IVerifyReleves)

    <TestInitialize()> _
    Public Sub Initialize()
        dictionary_operations = New Mock(Of IDictionaryKeys)()
        verify_releve = New Mock(Of IVerifyReleves)()
        words(1) = New Dictionary(Of Point, String)()
        bar_code_data = New Dictionary(Of String, BarCodeData)()

        words(1).Add(New Point(5, 0), "13RS")
        words(1).Add(New Point(3, 0), "FS9999999")
        words(1).Add(New Point(2, 20), "2017")
        words(1).Add(New Point(5, 20), "Z")
        words(1).Add(New Point(2, 7), "Province")
        words(1).Add(New Point(2, 5), "Province")
        words(1).Add(New Point(1, 22), "Année")
        words(1).Add(New Point(4, 22), "Code du relevé")
    End Sub

    <TestMethod()> Public Sub VerifyThatFindBarCodeValuesReturnAnEmptyDictionaryWhenAllValuesPass()
        Dim result As New Dictionary(Of String, BarCodeData)()
        Dim failed_bar_code_values_index As New Dictionary(Of String, BarCodeData)()
        Dim inversed_words As New Dictionary(Of Point, String)()

        bar_code_data.Add("txtCodeGabrt", New BarCodeData With {.value = "13RS"})
        bar_code_data.Add("txtNumLogiciel", New BarCodeData With {.value = "FS9999999"})
        bar_code_data.Add("txtAnnee", New BarCodeData With {.value = "2017"})
        bar_code_data.Add("txtCodeReleve", New BarCodeData With {.value = "Z"})

        inversed_words = words(1).OrderByDescending(
                Function(x) x.Key.Y).ThenBy(Function(x) x.Key.X).ToDictionary(Function(x) x.Key, Function(y) y.Value)

        Dim bar_code_index As Integer = 0
        For Each data As KeyValuePair(Of String, BarCodeData) In bar_code_data
            verify_releve.Setup(Function(f) f.VerifyTextInputPosition(inversed_words, data, bar_code_index)).Returns(True)
            dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryValue(inversed_words, data.Value.value.Trim())).Returns(New Point(0, 0))
            bar_code_index += 1
        Next

        search_pdf_text = New SearchPDFText(dictionary_operations.Object, verify_releve.Object)
        failed_bar_code_values_index = search_pdf_text.FindBarCodeValues(words, bar_code_data)

        Assert.IsTrue(failed_bar_code_values_index.Count = 0)
    End Sub

    <TestMethod()> Public Sub VerifyThatFindBarCodeValuesReturnCorrectFailingValuesWhenValuesAreNotAtTheirCorrectPosition()
        Dim result As New Dictionary(Of String, BarCodeData)()
        Dim failed_bar_code_values_index As New Dictionary(Of String, BarCodeData)()
        Dim inversed_words As New Dictionary(Of Point, String)()

        bar_code_data.Add("txtCodeGabrt", New BarCodeData With {.value = "13RS"})
        bar_code_data.Add("txtNumLogiciel", New BarCodeData With {.value = "FS9999999"})
        bar_code_data.Add("txtAnnee", New BarCodeData With {.value = "2017"})
        bar_code_data.Add("txtCodeReleve", New BarCodeData With {.value = "Z"})

        inversed_words = words(1).OrderByDescending(
                Function(x) x.Key.Y).ThenBy(Function(x) x.Key.X).ToDictionary(Function(x) x.Key, Function(y) y.Value)

        Dim bar_code_index As Integer = 0
        For Each data As KeyValuePair(Of String, BarCodeData) In bar_code_data
            If data.Value.value = "13RS" OrElse data.Value.value = "Z" Then
                verify_releve.Setup(Function(f) f.VerifyTextInputPosition(inversed_words, data, bar_code_index)).Returns(False)
                result.Add("_1" + bar_code_index.ToString, bar_code_data.ElementAt(bar_code_index).Value)
            Else
                verify_releve.Setup(Function(f) f.VerifyTextInputPosition(inversed_words, data, bar_code_index)).Returns(True)
            End If
            dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryValue(inversed_words, data.Value.value.Trim())).Returns(New Point(0, 0))
            bar_code_index += 1
        Next

        search_pdf_text = New SearchPDFText(dictionary_operations.Object, verify_releve.Object)
        failed_bar_code_values_index = search_pdf_text.FindBarCodeValues(words, bar_code_data)

        If result.Count = failed_bar_code_values_index.Count Then
            For i As Integer = 0 To result.Count - 1
                Assert.AreEqual(result.ElementAt(i).Key, failed_bar_code_values_index.ElementAt(i).Key)
                Assert.AreEqual(result.ElementAt(i).Value.value, failed_bar_code_values_index.ElementAt(i).Value.value)
            Next
        Else
            Assert.Fail("Incorrect number of failed values")
        End If
    End Sub

    <TestMethod()> Public Sub VerifyThatFindBarCodeValuesReturnCorrectFailingValuesWhenValuesAreNotFound()
        Dim result As New Dictionary(Of String, BarCodeData)()
        Dim failed_bar_code_values_index As New Dictionary(Of String, BarCodeData)()
        Dim inversed_words As New Dictionary(Of Point, String)()

        bar_code_data.Add("txtCodeGabrt", New BarCodeData With {.value = "13RS"})
        bar_code_data.Add("txtNumLogiciel", New BarCodeData With {.value = "FS9999999"})
        bar_code_data.Add("txtAnnee", New BarCodeData With {.value = "2010"})
        bar_code_data.Add("txtCodeReleve", New BarCodeData With {.value = "M"})

        inversed_words = words(1).OrderByDescending(
                Function(x) x.Key.Y).ThenBy(Function(x) x.Key.X).ToDictionary(Function(x) x.Key, Function(y) y.Value)

        Dim bar_code_index As Integer = 0
        For Each data As KeyValuePair(Of String, BarCodeData) In bar_code_data
            If data.Value.value = "2010" OrElse data.Value.value = "M" Then
                result.Add("_1" + bar_code_index.ToString, bar_code_data.ElementAt(bar_code_index).Value)
            Else
                verify_releve.Setup(Function(f) f.VerifyTextInputPosition(inversed_words, data, bar_code_index)).Returns(True)
            End If
            dictionary_operations.Setup(Function(f) f.GetKeyFromDictionaryValue(inversed_words, data.Value.value.Trim())).Returns(New Point(0, 0))
            bar_code_index += 1
        Next

        search_pdf_text = New SearchPDFText(dictionary_operations.Object, verify_releve.Object)
        failed_bar_code_values_index = search_pdf_text.FindBarCodeValues(words, bar_code_data)

        If result.Count = failed_bar_code_values_index.Count Then
            For i As Integer = 0 To result.Count - 1
                Assert.AreEqual(result.ElementAt(i).Key, failed_bar_code_values_index.ElementAt(i).Key)
                Assert.AreEqual(result.ElementAt(i).Value.value, failed_bar_code_values_index.ElementAt(i).Value.value)
            Next
        Else
            Assert.Fail("Incorrect number of failed values")
        End If
    End Sub
End Class