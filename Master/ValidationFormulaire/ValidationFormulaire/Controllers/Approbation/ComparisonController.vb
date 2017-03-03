Imports System.IO
Imports ValidationFormulaire.Models
Imports System.Drawing
Imports ValidationFormulaire.Core

Public Class ComparisonController
    Inherits System.Web.Mvc.Controller

    Private pagination As IPagination
    Private config As IConfig
    Private converter As IConverter
    Private session_value_provider As SessionValueProvider
    Dim comparaison As Comparaison

    Public Sub New()
        pagination = New DeterminePage()
        converter = New PDFToImageConverter()
        config = New Config()
        session_value_provider = New SessionValueProvider()
        comparaison = New Comparaison()
    End Sub

    Function ComparisonFormulaire() As ActionResult
        Return View("ComparisonFormulaire")
    End Function

    <HttpGet> _
    <[RequireRequestValue]("page_action")> _
    Function ComparisonFormulaire(page_action As DeterminePage.PageAction,
                                   Optional formulaire As FormulairePosition = Nothing,
                                   Optional page As Integer = Nothing,
                                   Optional threshold As Single = 0) As ActionResult
        If (page_action = Core.DeterminePage.PageAction.Compare) Then
            If (session_value_provider.GetValue("images_left") IsNot Nothing) And
                    (session_value_provider.GetValue("images_right") IsNot Nothing) Then
                session_value_provider.SetValue("threshold", threshold)
                session_value_provider.SetValue("compared_images",
                                            comparaison.Comparer(session_value_provider.GetValue("images_left")(session_value_provider.GetValue("current_page_left")),
                                                                 session_value_provider.GetValue("images_right")(session_value_provider.GetValue("current_page_right")),
                                                                 session_value_provider.GetValue("threshold")))
                session_value_provider.SetValue("comparing", "block")
                session_value_provider.SetValue("non_comparing", "none")
            End If
        Else
            session_value_provider.SetValue("comparing", "none")
            session_value_provider.SetValue("non_comparing", "block")
            If (formulaire = FormulairePosition.left) Then
                session_value_provider.SetValue("current_page_left", pagination.DeterminePageChange(page_action, session_value_provider.GetValue("current_page_left"), page))
            ElseIf (formulaire = FormulairePosition.right) Then
                session_value_provider.SetValue("current_page_right", pagination.DeterminePageChange(page_action, session_value_provider.GetValue("current_page_right"), page))
            End If
        End If
        Return View("ComparisonFormulaire")
    End Function

    <HttpPost> _
    Public Function ComparisonFormulaire(file_left As HttpPostedFileBase, file_right As HttpPostedFileBase) As ActionResult
        If (file_left IsNot Nothing) Then
            session_value_provider.SetValue("images_left", converter.PDFToImage(file_left))
            session_value_provider.SetValue("page_number_left", converter.GetPageNumber())
            session_value_provider.SetValue("current_page_left", 1)
        End If
        If (file_right IsNot Nothing) Then
            session_value_provider.SetValue("images_right", converter.PDFToImage(file_right))
            session_value_provider.SetValue("page_number_right", converter.GetPageNumber())
            session_value_provider.SetValue("current_page_right", 1)
        End If
        session_value_provider.SetValue("comparing", "none")
        session_value_provider.SetValue("non_comparing", "block")
        Return View()
    End Function

    Public Function GetImage(Optional formulaire As FormulairePosition = Nothing,
                             Optional current_page As Integer = 1) As ActionResult
        Dim get_image As New FormulaireImage()
        Return File(get_image.GetImageData(formulaire, current_page), "image/png")
    End Function

    Public Sub SetPagination(pagination As IPagination)
        Me.pagination = pagination
    End Sub

    Public Sub SetConverter(converter As IConverter)
        Me.converter = converter
    End Sub

    Public Sub SetConfig(config As IConfig)
        Me.config = config
    End Sub

    Public Sub SetSessionValueProvider(session_value_provider As SessionValueProvider)
        Me.session_value_provider = session_value_provider
    End Sub
End Class