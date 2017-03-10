Imports Emgu.CV
Imports Emgu.CV.Structure
Imports System.Drawing

Public Class Comparaison

    Private Const LEFT_IMAGE_INDEX As Integer = 0
    Private Const RIGHT_IMAGE_INDEX As Integer = 1
    Private Const NUMBER_OF_IMAGES As Integer = 2

    Public Function Compare(image_left As Image, image_right As Image, distance_threshold As Single) As Image()
        Dim image_result(NUMBER_OF_IMAGES) As Bitmap
        Dim threshold As New Threshold(distance_threshold)

        Dim emgu_image_left As New Image(Of Bgr, Byte)(New Bitmap(image_left))
        Dim emgu_image_right As New Image(Of Bgr, Byte)(New Bitmap(image_right))

        Dim first_image_result_left As Image(Of Bgr, [Byte]) = emgu_image_left - emgu_image_right
        Dim second_image_result_left As Image(Of Bgr, [Byte]) = emgu_image_right - emgu_image_left

        Dim first_image_result_right As Image(Of Bgr, [Byte]) = emgu_image_left - emgu_image_right
        Dim second_image_result_right As Image(Of Bgr, [Byte]) = emgu_image_right - emgu_image_left

        threshold.SearchForDifferencesBetweenImages(first_image_result_left.Data, second_image_result_left.Data)
        threshold.RemovePixels(first_image_result_left.Data)

        threshold.SearchForDifferencesBetweenImages(second_image_result_right.Data, first_image_result_right.Data)
        threshold.RemovePixels(second_image_result_right.Data)

        first_image_result_left = New ImageMask().ApplyRedMaskToImage(first_image_result_left)
        second_image_result_right = New ImageMask().ApplyRedMaskToImage(second_image_result_right)

        image_result(LEFT_IMAGE_INDEX) = (emgu_image_left - first_image_result_left).ToBitmap
        image_result(RIGHT_IMAGE_INDEX) = (emgu_image_right - second_image_result_right).ToBitmap

        Return image_result
    End Function
End Class
