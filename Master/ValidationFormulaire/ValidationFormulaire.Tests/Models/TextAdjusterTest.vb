Imports System.Web
Imports System.Text
Imports ValidationFormulaire.Core
Imports System.IO


<TestClass()> _
Public Class TextAdjusterTest
    Private remove_char As TextAdjuster
    Private chain, text, result As String
    Private number_position As Integer
    Private characters() As Char
    Private chars_to_remove As TextAdjuster

    <TestInitialize()> _
    Public Sub initialize()

        remove_char = New TextAdjuster()
        chars_to_remove = New TextAdjuster()
    End Sub

    <TestMethod()> _
    Public Sub RemoveCharsChain()
        Dim chain As String
        Dim text As String
        Dim result As String
        Dim number_position As Integer


        number_position = 0
        text = "abcdefgh"
        chain = "abcd"

        result = remove_char.RemoveCharChainFromText(chain, text(4))

        Assert.AreEqual("e", result)
    End Sub

    <TestMethod()> _
    Public Sub RemovCharFromNumbers()
        Dim characters() As Char = " "
        Dim result As String




        result = remove_char.RemoveCharsFromNumbersInString(characters:=",", text:="1235abcd2,efj")

        Assert.AreEqual("1235abcd2,efj", result)

        result = remove_char.RemoveCharsFromNumbersInString(characters:=",", text:="1235abcd2,3efj")

        Assert.AreEqual("1235abcd23efj", result)

    End Sub
End Class
