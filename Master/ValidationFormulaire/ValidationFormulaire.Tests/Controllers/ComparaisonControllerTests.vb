﻿Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Web.Mvc
Imports Moq
Imports System.Web
Imports ValidationFormulaire.Models
Imports ValidationFormulaire.Core
Imports ValidationFormulaire.Core.DeterminePage
Imports System.Drawing

<TestClass()> _
Public Class ComparaisonControllerTests
    Private comparaison_controller As ComparisonController
    Private session_value_provider As Mock(Of SessionValueProvider)
    Private pagination As Mock(Of IPagination)
    Private converter As Mock(Of IConverter)
    Private config As Mock(Of IConfig)


    <TestInitialize()> _
    Public Sub Initialize()
        session_value_provider = New Mock(Of SessionValueProvider)
        pagination = New Mock(Of IPagination)
        converter = New Mock(Of IConverter)

        config = New Mock(Of IConfig)

        session_value_provider.Setup(Function(f) f.GetValue("current_page_left")).Returns(2)
    End Sub

    <TestInitialize()> _
    Private Sub InitController()
        comparaison_controller = New ComparisonController(pagination:=pagination.Object, converter:=converter.Object,
                                                             session_value_provider:=session_value_provider.Object,
                                                             config:=config.Object)


    End Sub

    <TestMethod()> _
    Public Sub ComparaisonFormulaireSetSessionForCurrentPageToCorrectPageForNextPageAction()
        pagination.Setup(Function(f) f.DeterminePageChange(PageAction.NextPage, 2, Nothing)).Returns(3)
        InitController()
        comparaison_controller.ComparisonFormulaire(PageAction.NextPage)
        session_value_provider.Verify(Sub(f) f.SetValue("comparing", "none"), Times.Exactly(1))

    End Sub



End Class
