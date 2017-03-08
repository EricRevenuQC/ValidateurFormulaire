Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
Imports ValidationFormulaire.Core
Imports System.Drawing

<TestClass()> Public Class FindFirstPixelTests
    Private compare_pixels_color As Mock(Of IComparePixelColor)
    Private image_test As Image
    Private find_first_pixel As IFindFirstPixel

    <TestInitialize()> _
    Public Sub Initialize()
        compare_pixels_color = New Mock(Of IComparePixelColor)

        '(36, 28, 237) = red
        '(76, 177, 34) = greed
        '(255, 255, 255) = white
        '- - - - - - - - - - - -
        '- - R - - - - - - - - -
        '- - R - - - - - - - - -
        '- - R - - - - - R - - -
        '- - R G G G G G R - - -
        '- - R - - - - - R - - -
        '- - - - - - - - R - - -
        '- - - - - - - - - - - -
        '- - - - - - - - - - - -
        Dim image_byte_array As Byte() = {137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 12, 0,
                            0, 0, 9, 8, 6, 0, 0, 0, 6, 184, 205, 84, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174,
                            206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0,
                            0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100,
                            0, 0, 0, 48, 73, 68, 65, 84, 40, 83, 99, 248, 79, 34, 128, 107, 120, 43, 163, 2,
                            101, 225, 7, 212, 215, 128, 46, 142, 162, 65, 105, 163, 15, 6, 198, 171, 1, 27,
                            192, 169, 1, 23, 32, 89, 3, 58, 32, 81, 195, 255, 255, 0, 72, 107, 148, 186, 166,
                            4, 103, 19, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130}
        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(image_byte_array)
        image_test = System.Drawing.Image.FromStream(ms)
    End Sub

    <TestMethod()> Public Sub VerifyThatFindFirstPixelOfColorReturnCorrectPoint()
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(255, 0, 255, 0, 255, 255)).Returns(True)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(36, 0, 28, 0, 237, 255)).Returns(False)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(76, 0, 177, 0, 34, 255)).Returns(True)

        find_first_pixel = New SameColor(compare_pixels_color.Object)
        Dim starting_point As New Point(0, 0)
        Dim ending_point As New Point(image_test.Width - 1, image_test.Height - 1)
        Dim steps As New Point(1, 1)
        Dim color As New Colors(0, 0, 255)
        Dim pixel As New Point(find_first_pixel.Search(image_test, starting_point, ending_point, steps, color))
        Assert.AreEqual(New Point(2, 1), pixel)
    End Sub

    <TestMethod()> Public Sub VerifyThatFindFirstPixelOfDifferentColorReturnCorrectPoint()
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(255, 255, 255, 255, 255, 255)).Returns(False)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(36, 255, 28, 255, 237, 255)).Returns(True)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(76, 255, 177, 255, 34, 255)).Returns(True)

        find_first_pixel = New DifferentColor(compare_pixels_color.Object)
        Dim starting_point As New Point(0, 0)
        Dim ending_point As New Point(image_test.Width - 1, image_test.Height - 1)
        Dim steps As New Point(1, 1)
        Dim color As New Colors(255, 255, 255)
        Dim pixel As New Point(find_first_pixel.Search(image_test, starting_point, ending_point, steps, color))
        Assert.AreEqual(New Point(2, 1), pixel)
    End Sub

    <TestMethod()> Public Sub VerifyThatFindFirstPixelOfColorDiagonallyReturnCorrectPoint()
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(255, 0, 255, 255, 255, 0)).Returns(True)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(36, 0, 28, 255, 237, 0)).Returns(True)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(76, 0, 177, 255, 34, 0)).Returns(False)

        find_first_pixel = New SameColorDiagonally(compare_pixels_color.Object)
        Dim starting_point As New Point(5, 2)
        Dim color As New Colors(0, 255, 0)
        Dim pixel As New Point(find_first_pixel.Search(image_test, starting_point, Nothing, Nothing, color))
        Assert.AreEqual(New Point(3, 4), pixel)
    End Sub

    <TestMethod()> Public Sub VerifyThatFindFirstPixelOfColorDiagonallyReturnNothingWhenOutOfRange()
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(255, 0, 255, 255, 255, 0)).Returns(True)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(36, 0, 28, 255, 237, 0)).Returns(True)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(76, 0, 177, 255, 34, 0)).Returns(False)

        find_first_pixel = New SameColorDiagonally(compare_pixels_color.Object)
        Dim starting_point As New Point(8, 5)
        Dim color As New Colors(0, 255, 0)
        Dim pixel As New Point(find_first_pixel.Search(image_test, starting_point, Nothing, Nothing, color))
        Assert.AreEqual(Nothing, pixel)
    End Sub
End Class