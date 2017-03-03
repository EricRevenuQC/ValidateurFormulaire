Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web.Mvc
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports ValidationFormulaire
Imports ValidationFormulaire.Core

<TestClass()> _
Public Class PaginationTests
    Private pagination As DeterminePage
    Private current_page, result As Integer
    Private page_action As DeterminePage.PageAction

    <TestInitialize()> _
    Public Sub Initialize()
        pagination = New DeterminePage()
    End Sub

    <TestMethod()> _
    Public Sub DeterminePageChangeForNextPageActionIncrementCurrentPageIndex()
        current_page = 3
        page_action = DeterminePage.PageAction.NextPage

        result = pagination.DeterminePageChange(page_action, current_page, Nothing)

        Assert.AreEqual(4, result)
    End Sub

    <TestMethod()> _
    Public Sub DeterminePageChangeForPreviousPageActionDecrementCurrentPageIndex()
        current_page = 3
        page_action = DeterminePage.PageAction.PreviousPage

        result = pagination.DeterminePageChange(page_action, current_page, Nothing)

        Assert.AreEqual(2, result)
    End Sub

    <TestMethod()> _
    Public Sub DeterminePageChangeForJumpToPageActionChangeCurrentPageIndexToCorrectPageNumber()
        Dim page As Integer
        page_action = DeterminePage.PageAction.JumpToPage
        page = 10

        result = pagination.DeterminePageChange(page_action, Nothing, page)

        Assert.AreEqual(10, result)
    End Sub
End Class
