Imports System.Drawing

Public Class SearchPixelInImage
    Private compare_pixels_color As IComparePixelColor

    Public Sub New()
        compare_pixels_color = New ComparePixelColor()
    End Sub

    Public Sub New(compare_pixels_color As IComparePixelColor)
        Me.compare_pixels_color = compare_pixels_color
    End Sub

    Public Function FindFirstPixelOfColor(image As Bitmap,
                                         row_start As Integer, col_start As Integer,
                                         row_end As Integer, col_end As Integer,
                                         row_step As Integer, col_step As Integer,
                                         blue As Integer, green As Integer, red As Integer) As Point
        For current_row As Integer = row_start To row_end Step row_step
            For current_col As Integer = col_start To col_end Step col_step
                If (Not compare_pixels_color.DifferentPixelColor(image.GetPixel(current_row, current_col).B, blue,
                                                   image.GetPixel(current_row, current_col).G, green,
                                                   image.GetPixel(current_row, current_col).R, red)) Then
                    Return New Point(current_row, current_col)
                End If
            Next
        Next
        Return Nothing
    End Function

    Public Function FindFirstPixelOfDifferentColor(image As Bitmap,
                                         row_start As Integer, col_start As Integer,
                                         row_end As Integer, col_end As Integer,
                                         row_step As Integer, col_step As Integer,
                                         blue As Integer, green As Integer, red As Integer) As Point
        For current_row As Integer = row_start To row_end Step row_step
            For current_col As Integer = col_start To col_end Step col_step
                If (compare_pixels_color.DifferentPixelColor(image.GetPixel(current_row, current_col).B, blue,
                                                   image.GetPixel(current_row, current_col).G, green,
                                                   image.GetPixel(current_row, current_col).R, red)) Then
                    Return New Point(current_row, current_col)
                End If
            Next
        Next
    End Function

    Public Function FindFirstPixelOfColorDiagonally(image As Bitmap, starting_point As Point,
                                                    blue As Integer, green As Integer, red As Integer) As Point
        Dim current_point As New Point(starting_point)
        Dim positive_search_direction As Boolean = True
        Dim i As Integer = 1
        Dim j As Integer = 1

        While True
            If (Not compare_pixels_color.DifferentPixelColor(image.GetPixel(current_point.X, current_point.Y).B, blue,
                                                image.GetPixel(current_point.X, current_point.Y).G, green,
                                                image.GetPixel(current_point.X, current_point.Y).R, red)) Then
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

    Public Function FindAllPixelsOfColorNextToPoint(image As Bitmap, starting_point As Point,
                                                    blue As Integer, green As Integer, red As Integer) As List(Of Point)
        Dim pixels As New List(Of Point)()

        For current_row As Integer = starting_point.X To image.Width - 1 Step 1
            If (compare_pixels_color.DifferentPixelColor(image.GetPixel(current_row, starting_point.Y).B, blue,
                                                   image.GetPixel(current_row, starting_point.Y).G, green,
                                                   image.GetPixel(current_row, starting_point.Y).R, red)) Then
                Exit For
            End If
            For current_col As Integer = starting_point.Y To image.Height - 1 Step 1
                If (Not compare_pixels_color.DifferentPixelColor(image.GetPixel(current_row, current_col).B, blue,
                                                   image.GetPixel(current_row, current_col).G, green,
                                                   image.GetPixel(current_row, current_col).R, red)) Then
                    If Not pixels.Contains(New Point(current_row, current_col)) Then
                        pixels.Add(New Point(current_row, current_col))
                    End If
                Else
                    Exit For
                End If
            Next
            For current_col As Integer = starting_point.Y To 0 Step -1
                If (Not compare_pixels_color.DifferentPixelColor(image.GetPixel(current_row, current_col).B, blue,
                                                   image.GetPixel(current_row, current_col).G, green,
                                                   image.GetPixel(current_row, current_col).R, red)) Then
                    If Not pixels.Contains(New Point(current_row, current_col)) Then
                        pixels.Add(New Point(current_row, current_col))
                    End If
                Else
                    Exit For
                End If
            Next
        Next
        For current_row As Integer = starting_point.X To 0 Step -1
            If (compare_pixels_color.DifferentPixelColor(image.GetPixel(current_row, starting_point.Y).B, blue,
                                                   image.GetPixel(current_row, starting_point.Y).G, green,
                                                   image.GetPixel(current_row, starting_point.Y).R, red)) Then
                Exit For
            End If
            For current_col As Integer = starting_point.Y To image.Height - 1 Step 1
                If (Not compare_pixels_color.DifferentPixelColor(image.GetPixel(current_row, current_col).B, blue,
                                                   image.GetPixel(current_row, current_col).G, green,
                                                   image.GetPixel(current_row, current_col).R, red)) Then
                    If Not pixels.Contains(New Point(current_row, current_col)) Then
                        pixels.Add(New Point(current_row, current_col))
                    End If
                Else
                    Exit For
                End If
            Next
            For current_col As Integer = starting_point.Y To 0 Step -1
                If (Not compare_pixels_color.DifferentPixelColor(image.GetPixel(current_row, current_col).B, blue,
                                                   image.GetPixel(current_row, current_col).G, green,
                                                   image.GetPixel(current_row, current_col).R, red)) Then
                    If Not pixels.Contains(New Point(current_row, current_col)) Then
                        pixels.Add(New Point(current_row, current_col))
                    End If
                Else
                    Exit For
                End If
            Next
        Next
        Return pixels
    End Function
End Class
