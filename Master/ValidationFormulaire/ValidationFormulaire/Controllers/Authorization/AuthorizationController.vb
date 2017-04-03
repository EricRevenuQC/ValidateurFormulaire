Imports System.IO
Imports ValidationFormulaire.Models
Imports System.Drawing
Imports ValidationFormulaire.Core

Public Class AuthorizationController
    Inherits System.Web.Mvc.Controller

    Private converter As IConverter
    Private authorization As Authorization
    Private session_value_provider As SessionValueProvider
    Private pagination As IPagination

    Public Sub New()
        converter = New PDFConverter()
        authorization = New Authorization()
        session_value_provider = New SessionValueProvider()
        pagination = New DeterminePage()
    End Sub

    Function ValidationDonnees() As ActionResult
        Dim model As New AuthorizationModel()
        session_value_provider.SetValue("current_dialog", session_value_provider.GetValue("current_dialog") + 1)
        Return View(model)
    End Function

    <HttpGet> _
    <[RequireRequestValue]("page_action")> _
    Function ValidationDonnees(model As AuthorizationModel, page_action As DeterminePage.PageAction,
                                Optional formulaire As FormulairePosition = Nothing,
                                Optional page As Integer = Nothing) As ActionResult
        session_value_provider.SetValue(
            "current_page_releve_donnees", pagination.DeterminePageChange(
                page_action, session_value_provider.GetValue("current_page_releve_donnees"), page))
        Return View(model)
    End Function

    <HttpPost> _
    Public Function ValidationDonnees(file As HttpPostedFileBase) As ActionResult
        AlertsManager.ClearAlerts()
        Dim model As New AuthorizationModel()
        If file IsNot Nothing AndAlso file.ContentLength > 0 Then
            Dim images() As Image

            images = converter.PDFToImage(file)
            session_value_provider.SetValue("images_donnees", images)
            session_value_provider.SetValue("page_number_releve_donnees", converter.GetPageNumber())
            session_value_provider.SetValue("current_page_releve_donnees", 1)
            file.InputStream.Position = 0

            authorization.AuthorizePDF(file)
            model.bar_code_data = authorization.GetData
            model.bar_code_unverified_data = authorization.GetFailedData

            model.alert_messages = New Dictionary(Of String, String)()
            For Each alert As KeyValuePair(Of String, String) In AlertsManager.GetAllAlerts
                model.alert_messages.Add(alert.Key, alert.Value)
            Next
            model.alert_title = AlertsManager.GetAlertTitle
            model.alert_color = AlertsManager.GetAlertColor
        End If
        Return View(model)
    End Function

    Public Function GetImage(Optional formulaire As FormulairePosition = Nothing,
                             Optional current_page As Integer = 1) As ActionResult
        Dim get_image As New FormulaireImage()
        Return File(get_image.GetImageData(formulaire, current_page), "image/png")
    End Function
End Class