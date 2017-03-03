Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Web.Mvc
Imports Moq
Imports System.Web
Imports ValidationFormulaire.Models
Imports ValidationFormulaire.Core
Imports ValidationFormulaire.Core.Pagination

<TestClass()> _
Public Class VerificationControllerTests
    Private approbation_controller As ApprobationController
    Private session_value_provider As Mock(Of SessionValueProvider)
    Private pagination As Mock(Of IPagination)
    Private converter As Mock(Of IConverter)
    Private verification As Mock(Of IVerification)
    Private config As Mock(Of IConfig)

    <TestInitialize()> _
    Public Sub Initialize()
        approbation_controller = New ApprobationController()
        session_value_provider = New Mock(Of SessionValueProvider)
        pagination = New Mock(Of IPagination)
        converter = New Mock(Of IConverter)
        verification = New Mock(Of IVerification)
        config = New Mock(Of IConfig)

        session_value_provider.Setup(Function(f) f.GetValue("current_page")).Returns(2)
        approbation_controller.SetSessionValueProvider(session_value_provider.Object)
    End Sub

    <TestMethod()> _
    Public Sub VerificationFormatSetSessionForCurrentPageToCorrectPageForNextPageAction()
        pagination.Setup(Function(f) f.DeterminePageChange(PageAction.NextPage, 2, Nothing)).Returns(3)
        approbation_controller.SetPagination(pagination.Object)
        approbation_controller.VerificationFormat(PageAction.NextPage)
        session_value_provider.Verify(Sub(f) f.SetValue("current_page", 3), Times.Exactly(1))
    End Sub

    <TestMethod()> _
    Public Sub VerificationFormatSetSessionForCurrentPageToCorrectPageForPreviousPageAction()
        pagination.Setup(Function(f) f.DeterminePageChange(PageAction.PreviousPage, 2, Nothing)).Returns(1)
        approbation_controller.SetPagination(pagination.Object)
        approbation_controller.VerificationFormat(PageAction.PreviousPage)
        session_value_provider.Verify(Sub(f) f.SetValue("current_page", 1), Times.Exactly(1))
    End Sub

    <TestMethod()> _
    Public Sub VerificationFormatSetSessionForCurrentPageToCorrectPageForJumpToPageAction()
        pagination.Setup(Function(f) f.DeterminePageChange(PageAction.JumpToPage, 2, 5)).Returns(5)
        approbation_controller.SetPagination(pagination.Object)
        approbation_controller.VerificationFormat(PageAction.JumpToPage, 5)
        session_value_provider.Verify(Sub(f) f.SetValue("current_page", 5), Times.Exactly(1))
    End Sub

    <TestMethod()> _
    Public Sub VerificationFormatInitializeCurrentPageSessionCorrectly()
        Dim model As New Mock(Of ApprobationModel)
        Dim file As New Mock(Of HttpPostedFileBase)
        Dim images(1) As Drawing.Image

        file.Setup(Function(f) f.ContentLength).Returns(5)
        model.Setup(Function(f) f.File).Returns(file.Object)
        converter.Setup(Function(f) f.PDFToImage(file.Object)).Returns(images)
        verification.Setup(Function(f) f.Verifiy(images, "")).Returns(images)
        config.Setup(Function(f) f.GetVerificationImageTemplatePath()).Returns("")

        approbation_controller.SetConverter(converter.Object)
        approbation_controller.SetVerification(verification.Object)
        approbation_controller.SetConfig(config.Object)

        approbation_controller.VerificationFormat(model.Object)
        session_value_provider.Verify(Sub(f) f.SetValue("current_page", 1), Times.Exactly(1))
    End Sub

    <TestMethod()> _
    Public Sub VerificationFormatSetSessionForImagesToCorrectImageArray()
        Dim model As New Mock(Of ApprobationModel)
        Dim file As New Mock(Of HttpPostedFileBase)
        Dim images(1) As Drawing.Image

        file.Setup(Function(f) f.ContentLength).Returns(5)
        model.Setup(Function(f) f.File).Returns(file.Object)
        converter.Setup(Function(f) f.PDFToImage(file.Object)).Returns(images)
        verification.Setup(Function(f) f.Verifiy(images, "")).Returns(images)
        config.Setup(Function(f) f.GetVerificationImageTemplatePath()).Returns("")

        approbation_controller.SetConverter(converter.Object)
        approbation_controller.SetVerification(verification.Object)
        approbation_controller.SetConfig(config.Object)

        approbation_controller.VerificationFormat(model.Object)
        session_value_provider.Verify(Sub(f) f.SetValue("images", images), Times.Exactly(1))
    End Sub

    <TestMethod()> _
    Public Sub VerificationFormatSetSessionForPageNumberToCorrectNumber()
        Dim model As New Mock(Of ApprobationModel)
        Dim file As New Mock(Of HttpPostedFileBase)
        Dim images(1) As Drawing.Image

        file.Setup(Function(f) f.ContentLength).Returns(5)
        model.Setup(Function(f) f.File).Returns(file.Object)
        converter.Setup(Function(f) f.PDFToImage(file.Object)).Returns(images)
        converter.Setup(Function(f) f.GetPageNumber()).Returns(8)
        verification.Setup(Function(f) f.Verifiy(images, "")).Returns(images)
        config.Setup(Function(f) f.GetVerificationImageTemplatePath()).Returns("")

        approbation_controller.SetConverter(converter.Object)
        approbation_controller.SetVerification(verification.Object)
        approbation_controller.SetConfig(config.Object)

        approbation_controller.VerificationFormat(model.Object)
        session_value_provider.Verify(Sub(f) f.SetValue("page_number", 8), Times.Exactly(1))
    End Sub
End Class