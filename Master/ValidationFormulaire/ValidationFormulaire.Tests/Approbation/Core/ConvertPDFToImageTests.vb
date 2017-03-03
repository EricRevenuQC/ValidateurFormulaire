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
Public Class ConvertPDFToImageTests
    Private converter As Converter
    Private file_path As String
    Private file_mock As Mock(Of HttpPostedFileBase)
    Private file_stream As FileStream

    <TestInitialize()> _
    Public Sub Initialize()
        converter = New Converter()
        file_path = Path.GetFullPath("../../Files/4 blank pages.pdf")
        file_stream = New FileStream(file_path, FileMode.Open)
    End Sub

    <TestCleanup()> _
    Public Sub CleanUp()
        file_stream.Close()
    End Sub

    <TestMethod()> _
    Public Sub ConvertPDFToImageSetCorrectNumberOfPage()
        file_mock = New Mock(Of HttpPostedFileBase)
        file_mock.Setup(Function(f) f.InputStream).Returns(file_stream)

        converter.PDFToImage(file_mock.Object)

        Assert.AreEqual(5, converter.GetPageNumber)
    End Sub

    <TestMethod()> _
    Public Sub ConvertPDFToImageReturnArrayOfBitMapImages()
        Dim images() As Image
        file_mock = New Mock(Of HttpPostedFileBase)
        file_mock.Setup(Function(f) f.InputStream).Returns(file_stream)

        images = converter.PDFToImage(file_mock.Object)

        For i As Integer = 1 To images.Length - 1
            Assert.IsTrue(images(i).RawFormat.Equals(ImageFormat.MemoryBmp))
        Next
    End Sub
End Class