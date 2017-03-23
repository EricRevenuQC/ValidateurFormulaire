Imports System.Drawing

Public Class ColorSelector
    Private compare_pixels_color As IComparePixelColor
    Private pixels As List(Of Point)
    Private image As Bitmap

    Public Sub New(image As Bitmap)
        Me.image = image
        compare_pixels_color = New ComparePixelColor()
    End Sub

    Public Sub New(image As Bitmap, compare_pixels_color As IComparePixelColor)
        Me.image = image
        Me.compare_pixels_color = compare_pixels_color
    End Sub

    Public Function FindAllPixelsOfColorFromPoint(starting_point As Point, color As BGRColors) As List(Of Point)
        pixels = New List(Of Point)()

        FindHorizontalPixels(starting_point, image.Width - 1, 1, color)
        FindHorizontalPixels(starting_point, 0, -1, color)

        Return pixels
    End Function

    Private Sub FindHorizontalPixels(starting_point As Point, ending_point As Integer,
                             steps As Integer, color As BGRColors)
        For current_row As Integer = starting_point.X To ending_point Step steps
            If Not (compare_pixels_color.DifferentPixelColor(image.GetPixel(current_row, starting_point.Y).B, color.blue,
                                                    image.GetPixel(current_row, starting_point.Y).G, color.green,
                                                    image.GetPixel(current_row, starting_point.Y).R, color.red)) Then
                FindVerticalPixels(current_row, starting_point.Y, image.Height - 1, 1, color)
                FindVerticalPixels(current_row, starting_point.Y, 0, -1, color)
            Else
                Exit For
            End If
        Next
    End Sub

    Private Sub FindVerticalPixels(horizontal_pixel As Integer, starting_point As Integer, ending_point As Integer,
                             steps As Integer, color As BGRColors)
        For current_col As Integer = starting_point To ending_point Step steps
            If (Not compare_pixels_color.DifferentPixelColor(image.GetPixel(horizontal_pixel, current_col).B, color.blue,
                                                image.GetPixel(horizontal_pixel, current_col).G, color.green,
                                                image.GetPixel(horizontal_pixel, current_col).R, color.red)) Then
                If Not pixels.Contains(New Point(horizontal_pixel, current_col)) Then
                    pixels.Add(New Point(horizontal_pixel, current_col))
                End If
            Else
                Exit For
            End If
        Next
    End Sub
End Class
