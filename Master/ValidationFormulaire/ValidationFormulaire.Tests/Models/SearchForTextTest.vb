Imports System.Web
Imports System.Text
Imports ValidationFormulaire.Core
Imports System.IO

<TestClass()> _
Public Class SearchForTextTest
    Private search_string As SearchForText
    Private to_find As String
    Private to_search, result As String
    Private char_minimum_length As Integer
    Private save_remaining_chars As Boolean

    <TestInitialize()> _
    Public Sub initialize()

        search_string = New SearchForText()

    End Sub


    <TestMethod()> _
    Public Sub SearchStringIntoString()
        Dim to_search As String
        Dim to_find As String
        Dim result As String

        to_search = "abcdefghijklmnopqrstuvwxyz"
        to_find = "mnopq"
        result = search_string.RemoveStringFromString(to_find, to_search)

        Assert.AreEqual("abcdefghijklrstuvwxyz", result)
    End Sub

    <TestMethod()> _
    Public Sub SearchStringNotExistIntoText()
        Dim to_search As String
        Dim to_find As String
        Dim result As String

        to_search = "abcdefghijklmnopqrstuv"
        to_find = "wxyz"

        result = search_string.RemoveStringFromString(to_find, to_search)

        Assert.AreEqual("abcdefghijklmnopqrstuv", result)

    End Sub
    <TestMethod()> _
    Public Sub SearchStringWithMinimumCharLenght()
        Dim to_search As String
        Dim to_find As String
        Dim result As String
        Dim char_minimum_lenght As Integer

        char_minimum_lenght = 4
        to_search = "abcdefghijklmnopqrstuvwxyz"
        to_find = "wxy"

        result = search_string.RemoveStringFromString(to_find, to_search, char_minimum_length:=char_minimum_lenght)

        Assert.AreEqual("abcdefghijklmnopqrstuvwxyz", result)
    End Sub
    <TestMethod()> _
    Public Sub SearchStringWithCorrectCharLenght()
        Dim to_search As String
        Dim to_find As String
        Dim result As String
        Dim char_minimum_lenght As Integer

        char_minimum_lenght = 2
        to_search = "abcdefghijklmnopqrstuvwxyz"
        to_find = "wxy"

        result = search_string.RemoveStringFromString(to_find, to_search, char_minimum_length:=char_minimum_lenght)

        Assert.AreEqual("abcdefghijklmnopqrstuvz", result)


    End Sub

    <TestMethod()> _
    Public Sub SearchStringIntoTextAndSaveRemaing()
        Dim to_search As String
        Dim to_find As String
        Dim result As String
        Dim save_remaining_chars As Boolean

        to_search = "abcdefghijklmnopqrstuvwxyz"
        to_find = "wxyz"
        save_remaining_chars = True

        result = search_string.RemoveStringFromString(to_find, to_search, save_remaining_chars:=True)

        Assert.AreEqual("abcdefghijklmnopqrstuv", result)
        Assert.AreEqual("", search_string.GetRemainingCharacters)
    End Sub
    <TestMethod()> _
    Public Sub SearchStringNotExistingIntoTextAndSaveRemaing()
        Dim to_search As String
        Dim to_find As String
        Dim result As String
        Dim save_remaining_chars As Boolean

        to_search = "abcdefghijklmnopqrstuvwxy"
        to_find = "zz"
        save_remaining_chars = True

        result = search_string.RemoveStringFromString(to_find, to_search, save_remaining_chars:=True)

        Assert.AreEqual("abcdefghijklmnopqrstuvwxy", result)
        Assert.AreEqual("zz ", search_string.GetRemainingCharacters)
    End Sub



End Class



