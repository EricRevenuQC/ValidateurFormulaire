Imports System.Drawing

Public Class Threshold
    Private distance_threshold As Single
    Private compare_pixels_color As IComparePixelColor
    Private pixel_to_remove As New List(Of Point)()

    Public Sub New(threshold As Single)
        compare_pixels_color = New ComparePixelColor()
        distance_threshold = threshold
    End Sub

    Public Sub New(threshold As Single, compare_pixels_color As IComparePixelColor)
        Me.compare_pixels_color = compare_pixels_color
        distance_threshold = threshold
    End Sub

    Public Sub SearchForDifferencesBetweenImages(first_image_data As Byte(,,), second_image_data As Byte(,,))
        Dim threshold_applied As Point?
        For current_row As Integer = first_image_data.GetLength(0) - 1 To 0 Step -1
            For current_col As Integer = first_image_data.GetLength(1) - 1 To 0 Step -1
                If compare_pixels_color.DifferentPixelColor(first_image_data(current_row, current_col, 0),
                                                            second_image_data(current_row, current_col, 0),
                                        first_image_data(current_row, current_col, 1), second_image_data(current_row, current_col, 1),
                                        first_image_data(current_row, current_col, 2), second_image_data(current_row, current_col, 2)) Then
                    threshold_applied = ApplyThreshold(first_image_data, second_image_data, current_row, current_col)
                    If threshold_applied IsNot Nothing Then
                        pixel_to_remove.Add(threshold_applied)
                    End If
                End If
            Next
        Next
    End Sub

    Public Sub RemovePixels(data As Byte(,,))
        For Each pixel As Point In pixel_to_remove
            data(pixel.X, pixel.Y, 0) = 0
            data(pixel.X, pixel.Y, 1) = 0
            data(pixel.X, pixel.Y, 2) = 0
        Next
        pixel_to_remove.Clear()
    End Sub

    Private Function ApplyThreshold(data As Byte(,,), data2 As Byte(,,), current_row As Integer, current_col As Integer) As Point?
        Dim k As Integer = current_row + distance_threshold
        While k >= current_row - distance_threshold AndAlso k >= 0 AndAlso k <= data.GetLength(0) - 1
            Dim w As Integer = current_col + distance_threshold
            While w >= current_col - distance_threshold AndAlso w >= 0 AndAlso w <= data.GetLength(1) - 1
                If Not compare_pixels_color.DifferentPixelColor(data(current_row, current_col, 0), data2(k, w, 0),
                                                                data(current_row, current_col, 1), data2(k, w, 1),
                                                                data(current_row, current_col, 2), data2(k, w, 2), 150) Then
                    Return New Point(current_row, current_col)
                End If
                w -= 1
            End While
            k -= 1
        End While
        Return Nothing
    End Function
End Class
