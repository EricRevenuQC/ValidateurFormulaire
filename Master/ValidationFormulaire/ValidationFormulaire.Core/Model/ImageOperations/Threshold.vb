Imports System.Drawing

Public Class Threshold
    Private distance_threshold As Single
    Private compare_pixels_color As New ComparePixelColor()
    Private pixel_to_remove As List(Of Point)

    Public Sub New(threshold As Single)
        pixel_to_remove = New List(Of Point)()
        distance_threshold = threshold
    End Sub

    Public Sub SearchDifferences(data As Byte(,,), data2 As Byte(,,))
        For current_row As Integer = data.GetLength(0) - 1 To 0 Step -1
            For current_col As Integer = data.GetLength(1) - 1 To 0 Step -1
                If compare_pixels_color.DifferentPixelColor(data(current_row, current_col, 0), data2(current_row, current_col, 0),
                                        data(current_row, current_col, 1), data2(current_row, current_col, 1),
                                        data(current_row, current_col, 2), data2(current_row, current_col, 2)) Then
                    pixel_to_remove.Add(ApplyThreshold(data, data2, current_row, current_col))
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

    Private Function ApplyThreshold(data As Byte(,,), data2 As Byte(,,), current_row As Integer, current_col As Integer) As Point
        Dim k As Integer = current_row + distance_threshold
        While k >= current_row - distance_threshold AndAlso k > 0 AndAlso k <= data.GetLength(0)
            Dim w As Integer = current_col + distance_threshold
            While w >= current_col - distance_threshold AndAlso w > 0 AndAlso w <= data.GetLength(1)
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
