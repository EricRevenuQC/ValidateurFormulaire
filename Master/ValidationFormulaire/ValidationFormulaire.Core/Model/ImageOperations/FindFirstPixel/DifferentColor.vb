Imports System.Drawing

Public Class DifferentColor
    Implements IFindFirstPixel
    Private compare_pixels_color As IComparePixelColor

    Public Sub New()
        compare_pixels_color = New ComparePixelColor()
    End Sub

    Public Sub New(compare_pixels_color As IComparePixelColor)
        Me.compare_pixels_color = compare_pixels_color
    End Sub

    Public Function Search(image As Bitmap, starting_point As Point, ending_point As Point, steps As Point,
                            color As Colors) As Point Implements IFindFirstPixel.Search
        For current_row As Integer = starting_point.X To ending_point.X Step steps.X
            For current_col As Integer = starting_point.Y To ending_point.Y Step steps.Y
                If (compare_pixels_color.DifferentPixelColor(image.GetPixel(current_row, current_col).B, color.blue,
                                                   image.GetPixel(current_row, current_col).G, color.green,
                                                   image.GetPixel(current_row, current_col).R, color.red)) Then
                    Return New Point(current_row, current_col)
                End If
            Next
        Next
    End Function
End Class
