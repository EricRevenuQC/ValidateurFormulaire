Imports System
Imports ValidationFormulaire.Core


<TestClass()> _
Public Class ComparePixelColorTest
    Private color1a, color2a, color1b, color2b, color1c, color2c As Integer
    Private result As Boolean
    Private DiffrentColorPixel As ComparePixelColor



    <TestInitialize()> _
    Public Sub Initialize()
        DiffrentColorPixel = New ComparePixelColor()
    End Sub



    <TestMethod()> _
    Public Sub DeterminateForSameColorPixel()

        Dim color1a As Integer = RGB(0, 0, 255)
        Dim color2a As Integer = RGB(0, 0, 255)

        Dim color1b As Integer = RGB(0, 0, 255)
        Dim color2b As Integer = RGB(0, 0, 255)

        Dim color1c As Integer = RGB(0, 0, 255)
        Dim color2c As Integer = RGB(0, 0, 255)

        result = DiffrentColorPixel.DifferentPixelColor(color1a, color2a, color1b, color2b, color1c, color2c)

        Assert.AreEqual(False, result)

    End Sub

    <TestMethod()> _
    Public Sub DeterminateForDifferentColorPixel()

        Dim color1a As Integer = RGB(0, 0, 255)
        Dim color2a As Integer = RGB(0, 255, 255)

        Dim color1b As Integer = RGB(255, 255, 255)
        Dim color2b As Integer = RGB(255, 0, 255)

        Dim color1c As Integer = RGB(255, 0, 0)
        Dim color2c As Integer = RGB(0, 255, 0)

        result = DiffrentColorPixel.DifferentPixelColor(color1a, color2a, color1b, color2b, color1c, color2c)

        Assert.AreEqual(True, result)

    End Sub

End Class
