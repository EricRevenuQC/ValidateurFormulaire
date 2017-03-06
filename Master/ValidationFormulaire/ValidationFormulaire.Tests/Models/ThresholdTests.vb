Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports ValidationFormulaire.Core
Imports Moq

<TestClass()> Public Class ThresholdTests
    Private first_image_data(10, 10, 3) As Byte
    Private second_image_data(10, 10, 3) As Byte
    Private compare_pixels_color As Mock(Of IComparePixelColor)

    <TestInitialize()> _
    Public Sub Initialize()
        Dim first_image_color As Integer = 255
        Dim second_image_color As Integer = 255
        compare_pixels_color = New Mock(Of IComparePixelColor)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(0, 255, 0, 255, 0, 255)).Returns(True)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(0, 255, 0, 255, 0, 255, 150)).Returns(True)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(255, 0, 255, 0, 255, 0)).Returns(True)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(255, 0, 255, 0, 255, 0, 150)).Returns(True)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(255, 255, 255, 255, 255, 255)).Returns(False)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(255, 255, 255, 255, 255, 255, 150)).Returns(False)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(0, 0, 0, 0, 0, 0)).Returns(False)
        compare_pixels_color.Setup(Function(f) f.DifferentPixelColor(0, 0, 0, 0, 0, 0, 150)).Returns(False)

        'Set byte array pixel colors like so :
        '----------------------------------------------------------------------------------
        '            First image                                 Second image
        '255,255,255,255,255,255,255,255,255,255    255,255,255,255,255,000,000,000,000,000
        '255,255,255,255,255,255,255,255,255,255    255,255,255,255,255,000,000,000,000,000
        '255,255,255,255,255,255,255,255,255,255    255,255,255,255,255,000,000,000,000,000
        '255,255,255,255,255,255,255,255,255,255    255,255,255,255,255,000,000,000,000,000
        '255,255,255,255,255,255,255,255,255,255    255,255,255,255,255,000,000,000,000,000
        '000,000,000,000,000,000,000,000,000,000    255,255,255,255,255,000,000,000,000,000
        '000,000,000,000,000,000,000,000,000,000    255,255,255,255,255,000,000,000,000,000
        '000,000,000,000,000,000,000,000,000,000    255,255,255,255,255,000,000,000,000,000
        '000,000,000,000,000,000,000,000,000,000    255,255,255,255,255,000,000,000,000,000
        '000,000,000,000,000,000,000,000,000,000    255,255,255,255,255,000,000,000,000,000
        For x As Integer = 0 To 9 Step 1
            first_image_color = 255
            If x = 5 Then
                second_image_color = 0
            End If
            For y As Integer = 0 To 9 Step 1
                If y = 5 Then
                    first_image_color = 0
                End If
                For colors As Integer = 0 To 2 Step 1
                    first_image_data(x, y, colors) = first_image_color
                    second_image_data(x, y, colors) = second_image_color
                Next
            Next
        Next
    End Sub

    <TestMethod()> _
    Public Sub VerifyThatAllByteValuesStayTheSameWhenNotApplyingAThreshold()
        Dim result(10, 10, 3) As Byte
        Dim result_color As Integer = 255

        Dim threshold As New Threshold(0, compare_pixels_color.Object)
        threshold.SearchDifferences(first_image_data, second_image_data)
        threshold.RemovePixels(first_image_data)

        'Set result byte array pixel colors :
        '---------------------------------------
        '               Result
        '255,255,255,255,255,255,255,255,255,255
        '255,255,255,255,255,255,255,255,255,255
        '255,255,255,255,255,255,255,255,255,255
        '255,255,255,255,255,255,255,255,255,255
        '255,255,255,255,255,255,255,255,255,255
        '000,000,000,000,000,000,000,000,000,000
        '000,000,000,000,000,000,000,000,000,000
        '000,000,000,000,000,000,000,000,000,000
        '000,000,000,000,000,000,000,000,000,000
        '000,000,000,000,000,000,000,000,000,000
        For x As Integer = 0 To 9 Step 1
            For y As Integer = 0 To 9 Step 1
                If y < 5 Then
                    result_color = 255
                Else
                    result_color = 0
                End If
                For colors As Integer = 0 To 2 Step 1
                    result(x, y, colors) = result_color
                Next
            Next
        Next

        For x As Integer = 0 To 9 Step 1
            For y As Integer = 0 To 9 Step 1
                For colors As Integer = 0 To 2 Step 1
                    Assert.AreEqual(result(x, y, colors), first_image_data(x, y, colors))
                Next
            Next
        Next
    End Sub

    <TestMethod()> _
    Public Sub VerifyThatAllDifferentByteValuesAffectedByThresholdAreRemovedWhenApplyingAThreshold()
        Dim result(10, 10, 3) As Byte
        Dim result_color As Integer = 255

        Dim threshold As New Threshold(3, compare_pixels_color.Object)
        threshold.SearchDifferences(first_image_data, second_image_data)
        threshold.RemovePixels(first_image_data)

        'Set result byte array pixel colors :
        '---------------------------------------
        '               Result
        '255,255,255,255,255,000,000,000,255,255
        '255,255,255,255,255,000,000,000,255,255
        '255,255,255,255,255,000,000,000,255,255
        '255,255,255,255,255,000,000,000,255,255
        '255,255,255,255,255,000,000,000,255,255
        '000,000,000,000,000,000,000,000,000,000
        '000,000,000,000,000,000,000,000,000,000
        '000,000,000,000,000,000,000,000,000,000
        '000,000,000,000,000,000,000,000,000,000
        '000,000,000,000,000,000,000,000,000,000
        For x As Integer = 0 To 9 Step 1
            For y As Integer = 0 To 9 Step 1
                If y < 5 Then
                    result_color = 255
                Else
                    result_color = 0
                End If
                If (x = 5 Or x = 6 Or x = 7) AndAlso y < 5 Then
                    result_color = 0
                End If
                For colors As Integer = 0 To 2 Step 1
                    result(x, y, colors) = result_color
                Next
            Next
        Next

        For x As Integer = 0 To 9 Step 1
            For y As Integer = 0 To 9 Step 1
                For colors As Integer = 0 To 2 Step 1
                    Assert.AreEqual(result(x, y, colors), first_image_data(x, y, colors))
                Next
            Next
        Next
    End Sub
End Class