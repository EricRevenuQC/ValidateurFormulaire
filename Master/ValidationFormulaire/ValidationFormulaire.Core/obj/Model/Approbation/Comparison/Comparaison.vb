Imports Emgu.CV
Imports System.Drawing
Imports Emgu.CV.Structure

Public Class Comparaison
    Private first_image_result_left As Image(Of Bgr, [Byte]), second_image_result_left As Image(Of Bgr, [Byte])
    Private first_image_result_right As Image(Of Bgr, [Byte]), second_image_result_right As Image(Of Bgr, [Byte])
    Private image_color_mask As Image(Of Gray, Byte)

    Public Function Comparer(image_left As Image, image_right As Image, distance_threshold As Single) As Image()
        Dim image_result(2) As Bitmap
        Dim image_mask As Image(Of Gray, Byte)
        Dim bitmap_image_left As New Bitmap(image_left)
        Dim bitmap_image_right As New Bitmap(image_right)
        Dim threshold As New Threshold(distance_threshold)

        Dim emgu_image_left As New Image(Of Bgr, Byte)(bitmap_image_left)
        Dim emgu_image_right As New Image(Of Bgr, Byte)(bitmap_image_right)

        first_image_result_left = emgu_image_left - emgu_image_right
        second_image_result_left = emgu_image_right - emgu_image_left

        first_image_result_right = emgu_image_left - emgu_image_right
        second_image_result_right = emgu_image_right - emgu_image_left

        threshold.SearchDifferences(first_image_result_left.Data, second_image_result_left.Data)
        threshold.RemovePixels(first_image_result_left.Data)
        threshold.SearchDifferences(second_image_result_right.Data, first_image_result_right.Data)
        threshold.RemovePixels(second_image_result_right.Data)

        image_mask = first_image_result_left.InRange(New Bgr(10, 10, 10), New Bgr(255, 255, 255))
        first_image_result_left.SetValue(New Bgr(255, 255, 0), image_mask) 'Set color to teal since black - teal = red.
        image_mask = second_image_result_right.InRange(New Bgr(10, 10, 10), New Bgr(255, 255, 255))
        second_image_result_right.SetValue(New Bgr(255, 255, 0), image_mask) 'Set color to teal since black - teal = red.

        image_result(0) = (emgu_image_left - first_image_result_left).ToBitmap
        image_result(1) = (emgu_image_right - second_image_result_right).ToBitmap

        Return image_result
    End Function
End Class
