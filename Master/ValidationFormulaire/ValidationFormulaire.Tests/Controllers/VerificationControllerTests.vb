Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Web.Mvc
Imports Moq
Imports System.Web
Imports ValidationFormulaire.Models
Imports ValidationFormulaire.Core
Imports ValidationFormulaire.Core.DeterminePage
Imports System.Drawing

<TestClass()> _
Public Class VerificationControllerTests
    Private verification_controller As VerificationController
    Private session_value_provider As Mock(Of SessionValueProvider)
    Private pagination As Mock(Of IPagination)
    Private converter As Mock(Of IConverter)
    Private verification As Mock(Of IVerification)
    Private config As Mock(Of IConfig)

    <TestInitialize()> _
    Public Sub Initialize()
        session_value_provider = New Mock(Of SessionValueProvider)
        pagination = New Mock(Of IPagination)
        converter = New Mock(Of IConverter)
        verification = New Mock(Of IVerification)
        config = New Mock(Of IConfig)

        session_value_provider.Setup(Function(f) f.GetValue("current_page")).Returns(2)
        
    End Sub

    Private Sub InitController()
        verification_controller = New VerificationController(pagination:=pagination.Object, converter:=converter.Object,
                                                             session_value_provider:=session_value_provider.Object,
                                                             config:=config.Object, verification:=verification.Object)
    End Sub

    <TestMethod()> _
    Public Sub VerificationFormatSetSessionForCurrentPageToCorrectPageForNextPageAction()
        pagination.Setup(Function(f) f.DeterminePageChange(PageAction.NextPage, 2, Nothing)).Returns(2)
        InitController()
        verification_controller.VerificationFormat(PageAction.NextPage)
        session_value_provider.Verify(Sub(f) f.SetValue("current_page", 2), Times.Exactly(1))
    End Sub

    <TestMethod()> _
    Public Sub VerificationFormatSetSessionForCurrentPageToCorrectPageForPreviousPageAction()
        pagination.Setup(Function(f) f.DeterminePageChange(PageAction.PreviousPage, 2, Nothing)).Returns(4)
        InitController()
        verification_controller.VerificationFormat(PageAction.PreviousPage)
        session_value_provider.Verify(Sub(f) f.SetValue("current_page", 4), Times.Exactly(1))
    End Sub

    <TestMethod()> _
    Public Sub VerificationFormatSetSessionForCurrentPageToCorrectPageForJumpToPageAction()
        pagination.Setup(Function(f) f.DeterminePageChange(PageAction.JumpToPage, 2, 5)).Returns(8)
        InitController()
        verification_controller.VerificationFormat(PageAction.JumpToPage, page:=5)
        session_value_provider.Verify(Sub(f) f.SetValue("current_page", 8), Times.Exactly(1))
    End Sub
End Class