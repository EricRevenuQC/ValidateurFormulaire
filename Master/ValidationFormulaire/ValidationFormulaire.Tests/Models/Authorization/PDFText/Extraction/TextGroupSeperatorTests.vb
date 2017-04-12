Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
Imports ValidationFormulaire.Core
Imports System.Drawing

<TestClass()> Public Class TextGroupSeperatorTests
    Private group_seperator As TextGroupSeperator
    Private text_extraction_strategy As Mock(Of ITextExtractionStrategy)

    <TestInitialize()> _
    Public Sub Initialize()
        group_seperator = New TextGroupSeperator()
        text_extraction_strategy = New Mock(Of ITextExtractionStrategy)()
    End Sub

    <TestMethod()> Public Sub VerifyThatSeperateTextIntoGroupsCombineCorrectWordsTogether()
        Dim rectangle_points As New List(Of TextRectangle)()
        Dim word_list As New Dictionary(Of Point, TextProperties)()
        Dim expected_results As New Dictionary(Of Point, String)()

        rectangle_points.Add(New TextRectangle(New iTextSharp.text.Rectangle(10, 10, 20, 15), "Ceci est ", ""))
        rectangle_points.Add(New TextRectangle(New iTextSharp.text.Rectangle(25, 10, 30, 15), "un test", ""))
        rectangle_points.Add(New TextRectangle(New iTextSharp.text.Rectangle(25, 30, 40, 35), "pour séparer", ""))
        rectangle_points.Add(New TextRectangle(New iTextSharp.text.Rectangle(40, 50, 50, 55), " des mots", ""))
        rectangle_points.Add(New TextRectangle(New iTextSharp.text.Rectangle(55, 70, 65, 75), "en différent", ""))
        rectangle_points.Add(New TextRectangle(New iTextSharp.text.Rectangle(90, 70, 93, 75), "grou", ""))
        rectangle_points.Add(New TextRectangle(New iTextSharp.text.Rectangle(97, 75, 98, 80), ".", ""))
        rectangle_points.Add(New TextRectangle(New iTextSharp.text.Rectangle(94, 70, 96, 75), "pes", ""))

        text_extraction_strategy.Setup(Function(f) f.GetRectanglePoints()).Returns(rectangle_points)

        word_list = group_seperator.SeperateTextIntoGroups(text_extraction_strategy.Object)
        expected_results.Add(New Point(10, 10), "Ceci est un test")
        expected_results.Add(New Point(25, 30), "pour séparer")
        expected_results.Add(New Point(40, 50), " des mots")
        expected_results.Add(New Point(55, 70), "en différent")
        expected_results.Add(New Point(90, 70), "groupes.")

        For i As Integer = 0 To word_list.Count - 1
            Assert.AreEqual(word_list.ElementAt(i).Key, expected_results.ElementAt(i).Key)
            Assert.AreEqual(word_list.ElementAt(i).Value, expected_results.ElementAt(i).Value)
        Next
    End Sub
End Class