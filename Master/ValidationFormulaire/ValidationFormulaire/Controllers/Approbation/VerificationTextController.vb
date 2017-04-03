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
    Private verification As IVerificationText

    Public Sub New()
        pagination = New DeterminePage()
        config = New Config()
        converter = New PDFConverter()
        session_value_provider = New SessionValueProvider()
        verification = New VerificationText()
    End Sub

    Public Sub New(Optional pagination As IPagination = Nothing, Optional converter As IConverter = Nothing,
                   Optional verification As IVerificationText = Nothing, Optional config As IConfig = Nothing,
                   Optional session_value_provider As SessionValueProvider = Nothing)
        Me.pagination = pagination
        Me.converter = converter
        Me.config = config
        Me.session_value_provider = session_value_provider
        Me.verification = verification
    End Sub

    Function VerificationText() As ActionResult
        Return View("../Approbation/VerificationText/VerificationText")
    End Function

    <HttpGet> _
    <[RequireRequestValue]("page_action")> _
    Function VerificationText(page_action As DeterminePage.PageAction,
                                Optional page As Integer = Nothing) As ActionResult
        session_value_provider.SetValue("current_page_text", pagination.DeterminePageChange(
                                        page_action, session_value_provider.GetValue("current_page_text"),
                                        page))
        Return View("../Approbation/VerificationText/VerificationText")
    End Function

    <HttpPost> _
    Public Function VerificationText(image_file_text As HttpPostedFileBase,
                                     image_file_template_text As HttpPostedFileBase) As ActionResult
        AlertsManager.ClearAlerts()
        Dim model As New ApprobationModel()

        If image_file_template_text Is Nothing OrElse image_file_template_text.ContentLength = 0 Then
            'Set default template.
        End If

        If image_file_text IsNot Nothing AndAlso image_file_text.ContentLength > 0 Then
            Dim text_result() As String

            text_result = verification.VerificationText(image_file_text, image_file_template_text)

            session_value_provider.SetValue("page_number_text", text_result.Count - 1)
            session_value_provider.SetValue("current_page_text", 1)

            model.analysed_text = text_result

            model.alert_messages = New Dictionary(Of String, String)()
            For Each alert As KeyValuePair(Of String, String) In AlertsManager.GetAllAlerts
                model.alert_messages.Add(alert.Key, alert.Value)
            Next
            model.alert_title = AlertsManager.GetAlertTitle
        End If

        Return View("../Approbation/VerificationText/VerificationText", model)
    End Function
End Class