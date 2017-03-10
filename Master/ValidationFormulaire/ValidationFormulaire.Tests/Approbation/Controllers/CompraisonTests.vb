Imports System.IO
Imports ValidationFormulaire.Models
Imports System.Drawing
Imports ValidationFormulaire.Core
Imports System.Web
Imports Moq
Imports System.Drawing.Imaging

<TestClass()> _
Public Class CompraisonTests
    Private comparaison_controleur As ComparisonController
    Private file_left_test As Mock(Of HttpPostedFileBase)
    Private session_value_provider As SessionValueProvider
    Private converter As PDFToImageConverter
    Private file_stream As FileStream
    Private file_Path = Path.GetFullPath("../../Files/formulaire111.pdf")



    <TestInitialize()> _
    Public Sub Initialize()
        comparaison_controleur = New ComparisonController()
        converter = New PDFToImageConverter()
        file_Path = Path.GetFullPath("../../Files/formulaire111.pdf")
        file_stream = New FileStream(file_Path, FileMode.Open)
    End Sub

    <TestMethod()> _
    Public Sub ComparisonControllerFileLeftConversionPDFToImage()
        Dim images() As Image
        file_left_test = New Mock(Of HttpPostedFileBase)
        file_left_test.Setup(Function(f) f.InputStream).Returns(file_stream)

        images = converter.PDFToImage(file_left_test.Object)
        images = comparaison_controleur.ComparisonFormulaire
        For i As Integer = 1 To images.Length - 1
            Assert.IsTrue(images(i).RawFormat.Equals(ImageFormat.MemoryBmp))
        Next


    End Sub
End Class
