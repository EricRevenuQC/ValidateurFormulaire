Imports System.Drawing

Public Class SameColorDiagonally
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
        Dim current_point As New Point(starting_point)
        Dim positive_search_direction As Boolean = True
        Dim i As Integer = 1
        Dim j As Integer = 1

        While True
            If (Not compare_pixels_color.DifferentPixelColor(image.GetPixel(current_point.X, current_point.Y).B, color.blue,
                                                image.GetPixel(current_point.X, current_point.Y).G, color.green,
                                                image.GetPixel(current_point.X, current_point.Y).R, color.red)) Then
                Return current_point
            End If
            If positive_search_direction = True Then
                positive_search_direction = False
                current_point.X = starting_point.X + i
                current_point.Y = starting_point.Y - i
                If current_point.X > image.Width - 1 Or current_point.Y < 0 Then
                    Exit While
                End If
                i += 1
            Else
                positive_search_direction = True
                current_point.X = starting_point.X - j
                current_point.Y = starting_point.Y + j
                If current_point.X < 0 Or current_point.Y > image.Height - 1 Then
                    Exit While
                End If
                j += 1
            End If
        End While
        Return Nothing
    End Function
End Class
