Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
Imports System.Web
Imports System.IO
Imports System.Drawing
Imports System.Web.Mvc
Imports System.Drawing.Imaging
Imports ValidationFormulaire.Core

<TestClass()> _
Public Class TextExtractorTests
    Private extractor As TextExtractor
    Private text_seperator As Mock(Of ITextGroupSeperator)
    Private text_extraction_strategy As Mock(Of TextExtractionStrategy)
    Private file_path As String
    Private file_mock As Mock(Of HttpPostedFileBase)
    Private file_stream As FileStream

    <TestInitialize()> _
    Public Sub Initialize()
        text_seperator = New Mock(Of ITextGroupSeperator)()
        text_extraction_strategy = New Mock(Of TextExtractionStrategy)()
        file_path = Path.GetFullPath("../../Files/pdftest1.pdf")
        file_stream = New FileStream(file_path, FileMode.Open)
        file_mock = New Mock(Of HttpPostedFileBase)
        file_mock.Setup(Function(f) f.InputStream).Returns(file_stream)
    End Sub

    <TestCleanup()> _
    Public Sub CleanUp()
        file_stream.Close()
    End Sub

    <TestMethod()> _
    Public Sub VerifyThatAllPDFToTextLogicFunctionsAreCalledInCorrectOrder()
        Dim words(1) As Dictionary(Of Point, String)
        words(1) = New Dictionary(Of Point, String)()

        text_seperator.Setup(Function(f) f.SeperateTextIntoGroups(text_extraction_strategy.Object)).Returns(words(1))

        extractor = New TextExtractor(text_seperator.Object, text_extraction_strategy.Object)

        extractor.PDFToText(file_mock.Object)

        text_seperator.Verify(Function(f) f.SeperateTextIntoGroups(text_extraction_strategy.Object), Times.Exactly(1))
    End Sub
End Class