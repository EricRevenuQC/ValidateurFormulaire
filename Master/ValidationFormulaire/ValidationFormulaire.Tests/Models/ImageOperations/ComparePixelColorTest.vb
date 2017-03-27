Imports System
Imports ValidationFormulaire.Core


<TestClass()> _
Public Class ComparePixelColorTest
    Private result As Boolean
    Private DiffrentColorPixel As ComparePixelColor

    <TestInitialize()> _
    Public Sub Initialize()
        DiffrentColorPixel = New ComparePixelColor()
    End Sub

    <TestMethod()> _
    Public Sub VerifyThatSamePixelColorsAreNotDifferent()
        Dim color1a As Integer = 0
        Dim color2a As Integer = 0
        Dim color1b As Integer = 0
        Dim color2b As Integer = 0
        Dim color1c As Integer = 255
        Dim color2c As Integer = 255

        result = DiffrentColorPixel.DifferentPixelColor(color1a, color2a, color1b, color2b, color1c, color2c)

        Assert.AreEqual(False, result)
    End Sub

    <TestMethod()> _
    Public Sub VerifyThatDifferentPixelColorsAreDifferent()
        Dim color1a As Integer = 0
        Dim color2a As Integer = 255
        Dim color1b As Integer = 0
        Dim color2b As Integer = 255
        Dim color1c As Integer = 0
        Dim color2c As Integer = 255

        result = DiffrentColorPixel.DifferentPixelColor(color1a, color2a, color1b, color2b, color1c, color2c)

        Assert.AreEqual(True, result)
    End Sub

End Class
