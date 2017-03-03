Imports System.Drawing

Public Class DrawOnImage

    Private compare_pixels_color As New ComparePixelColor()

    Public Sub DrawZone(bitmap_image As Bitmap,
                                   row_start As Integer, row_end As Integer,
                                   col_start As Integer, col_end As Integer,
                                   row_step As Integer, col_step As Integer,
                                   color As Color)
        For current_row As Integer = row_start To row_end Step row_step
            For current_col As Integer = col_start To col_end Step col_step
                If Not compare_pixels_color.DifferentPixelColor(bitmap_image.GetPixel(current_row, current_col).B, 255,
                                               bitmap_image.GetPixel(current_row, current_col).G, 255,
                                               bitmap_image.GetPixel(current_row, current_col).R, 255) Then
                    bitmap_image.SetPixel(current_row, current_col, color)
                End If
            Next
        Next
    End Sub

    Public Function MarkPixelWithImage(bitmap_image As Bitmap, anchor_points_bot_left As List(Of Point),
                                      anchor_points_top_right As List(Of Point),
                                   row_start As Integer, row_end As Integer,
                                   col_start As Integer, col_end As Integer,
                                   row_step As Integer, col_step As Integer,
                                   mark_image As Bitmap) As List(Of Point)
        Dim error_pixels As New List(Of Point)()
        Dim proximity_error_pixels As New List(Of Point)()

        For current_row As Integer = row_start To row_end Step row_step
            For current_col As Integer = col_start To col_end Step col_step
                If compare_pixels_color.DifferentPixelColor(bitmap_image.GetPixel(current_row, current_col).B, 255,
                                               bitmap_image.GetPixel(current_row, current_col).G, 255,
                                               bitmap_image.GetPixel(current_row, current_col).R, 255) And
                                           compare_pixels_color.DifferentPixelColor(bitmap_image.GetPixel(current_row, current_col).B, 0,
                                               bitmap_image.GetPixel(current_row, current_col).G, 0,
                                               bitmap_image.GetPixel(current_row, current_col).R, 255) Then
                    If Not proximity_error_pixels.Contains(New Point(current_row, current_col)) And
                            Not anchor_points_bot_left.Contains(New Point(current_row, current_col)) And
                            Not anchor_points_top_right.Contains(New Point(current_row, current_col)) Then
                        error_pixels.Add(New Point(current_row, current_col))
                        For i As Integer = current_row - 10 To current_row + 10 Step row_step
                            For j As Integer = current_col - 10 To current_col + 10 Step col_step
                                proximity_error_pixels.Add(New Point(i, j))
                            Next
                        Next
                    End If
                End If
            Next
        Next

        For Each pixel In error_pixels
            Dim graphic = Graphics.FromImage(bitmap_image)
            graphic.DrawImage(mark_image, New Point(pixel.X - (mark_image.Width / 2),
                                                    pixel.Y - (mark_image.Height / 2)))
            graphic.Flush()
            graphic.Dispose()
        Next
        Return error_pixels
    End Function
End Class
