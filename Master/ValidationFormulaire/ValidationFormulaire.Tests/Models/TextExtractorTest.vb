Imports iTextSharp
Imports System.Web
Imports System.Text
Imports ValidationFormulaire.Core
Imports System.IO
Imports Moq


<TestClass()> _
Public Class TextExtractorTest
    Private text_extractor As TextExtractor
    Private file_path As String
    Private file_stream As FileStream
    Private file_mock As Mock(Of HttpPostedFileBase)

    <TestInitialize()> _
    Public Sub initialize()
        text_extractor = New TextExtractor()
        file_path = Path.GetFullPath("../../Files/pdftest1.pdf")
        file_stream = New FileStream(file_path, FileMode.Open)
    End Sub

    <TestCleanup()> _
    Public Sub CleanUp()
        file_stream.Close()
    End Sub
    <TestMethod()> _
    Public Sub ExtractTextFromPDFReturnText()
        file_mock = New Mock(Of HttpPostedFileBase)
        file_mock.Setup(Function(f) f.InputStream).Returns(file_stream)
        Dim text_result() As String

        text_result = text_extractor.PDFToText(file_mock.Object)

        For i As Integer = 1 To text_result.Length - 1
            Assert.AreEqual("pdftestx ", text_result(i))
        Next


    End Sub

End Class

