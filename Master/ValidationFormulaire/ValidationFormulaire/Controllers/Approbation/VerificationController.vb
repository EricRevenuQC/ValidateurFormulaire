Imports System.IO
Imports ValidationFormulaire.Models
Imports System.Drawing
Imports ValidationFormulaire.Core

Public Class VerificationController
    Inherits System.Web.Mvc.Controller

    Private pagination As IPagination
    Private config As IConfig
    Private converter As IConverter
    Private verification As IVerification
    Private session_value_provider As SessionValueProvider

    Public Sub New()
        pagination = New DeterminePage()
        config = New Config()
        converter = New PDFConverter()
        verification = New Verification()
        session_value_provider = New SessionValueProvider()
    End Sub

    Public Sub New(Optional pagination As IPagination = Nothing, Optional converter As IConverter = Nothing,
                   Optional verification As IVerification = Nothing, Optional config As IConfig = Nothing,
                   Optional session_value_provider As SessionValueProvider = Nothing)
        Me.pagination = pagination
        Me.converter = converter
        Me.verification = verification
        Me.config = config
        Me.session_value_provider = session_value_provider
    End Sub

    Function VerificationFormat() As ActionResult
        Return View("VerificationFormat")
    End Function

    <HttpGet> _
    <[RequireRequestValue]("page_action")> _
    Function VerificationFormat(page_action As DeterminePage.PageAction,
                                Optional formulaire As FormulairePosition = Nothing,
                                Optional page As Integer = Nothing) As ActionResult
        session_value_provider.SetValue("current_page", pagination.DeterminePageChange(
                                        page_action, session_value_provider.GetValue("current_page"),
                                        page))
        Return View("VerificationFormat")
    End Function

    <HttpPost> _
    Public Function VerificationFormat(image_file As HttpPostedFileBase,
                                       image_file_template As HttpPostedFileBase,
                                       Optional verif_threshold As Single = 0) As ActionResult
        AlertsManager.ClearAlerts()
        Dim model As New ApprobationModel()
        Dim template_image As Image

        Dim TimerStart As DateTime
        Dim TimeSpent As System.TimeSpan

        If image_file_template IsNot Nothing AndAlso image_file_template.ContentLength > 0 Then
            template_image = converter.PDFToImage(image_file_template).First
        Else
            template_image = Image.FromFile(config.GetVerificationImageTemplatePath)
        End If

        If image_file IsNot Nothing AndAlso image_file.ContentLength > 0 Then
            Dim images() As Image

            TimerStart = Now

            images = converter.PDFToImage(image_file)

            TimeSpent = Now.Subtract(TimerStart)
            System.Diagnostics.Debug.WriteLine(TimeSpent.TotalSeconds.ToString + " seconds spent on this task")

            images = verification.CalculateThreshold(images, template_image, verif_threshold)
            images = verification.Verification(images)
            session_value_provider.SetValue("images", images)
            session_value_provider.SetValue("page_number", converter.GetPageNumber())
            session_value_provider.SetValue("current_page", 1)

            model.alert_messages = New Dictionary(Of String, String)()
            For Each alert As KeyValuePair(Of String, String) In AlertsManager.GetAllAlerts
                model.alert_messages.Add(alert.Key, alert.Value)
            Next
        End If
        Return View(model)
    End Function

    Public Function GetImage(Optional formulaire As FormulairePosition = Nothing,
                             Optional current_page As Integer = 1) As ActionResult
        Dim get_image As New FormulaireImage()
        Return File(get_image.GetImageData(formulaire, current_page), "image/png")
    End Function
End Class