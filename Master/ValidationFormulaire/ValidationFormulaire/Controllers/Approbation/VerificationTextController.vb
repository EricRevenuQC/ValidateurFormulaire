Imports System.IO
Imports ValidationFormulaire.Models
Imports System.Drawing
Imports ValidationFormulaire.Core

Public Class VerificationTextController
    Inherits System.Web.Mvc.Controller

    Private pagination As IPagination
    Private config As IConfig
    Private converter As IConverter
    Private session_value_provider As SessionValueProvider

    Public Sub New()
        pagination = New DeterminePage()
        config = New Config()
        converter = New PDFConverter()
        session_value_provider = New SessionValueProvider()
    End Sub

    Public Sub New(Optional pagination As IPagination = Nothing, Optional converter As IConverter = Nothing,
                   Optional config As IConfig = Nothing, Optional session_value_provider As SessionValueProvider = Nothing)
        Me.pagination = pagination
        Me.converter = converter
        Me.config = config
        Me.session_value_provider = session_value_provider
    End Sub

    Function VerificationText() As ActionResult
        Return View("VerificationText")
    End Function

    <HttpGet> _
    <[RequireRequestValue]("page_action")> _
    Function VerificationText(page_action As DeterminePage.PageAction,
                                Optional page As Integer = Nothing) As ActionResult
        session_value_provider.SetValue("current_page_text", pagination.DeterminePageChange(
                                        page_action, session_value_provider.GetValue("current_page_text"),
                                        page))
        Return View("VerificationText")
    End Function

    <HttpPost> _
    Public Function VerificationText(image_file As HttpPostedFileBase,
                                       image_file_template As HttpPostedFileBase) As ActionResult
        AlertsManager.ClearAlerts()
        Dim model As New ApprobationModel()
        Dim template_image As Image

        If image_file_template IsNot Nothing AndAlso image_file_template.ContentLength > 0 Then
        Else
            template_image = Image.FromFile(config.GetVerificationImageTemplatePath)
        End If

        If image_file IsNot Nothing AndAlso image_file.ContentLength > 0 Then
        End If
        Return View(model)
    End Function

    Public Function GetImage(Optional formulaire As FormulairePosition = Nothing,
                             Optional current_page As Integer = 1) As ActionResult
        Dim get_image As New FormulaireImage()
        Return File(get_image.GetImageData(formulaire, current_page), "image/png")
    End Function
End Class