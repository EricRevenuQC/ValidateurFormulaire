Imports Emgu.CV
Imports Emgu.CV.Structure

Public Class ImageMask
    Public Function ApplyRedMaskToImage(image As Image(Of Bgr, Byte)) As Image(Of Bgr, Byte)
        'Get all non black pixels in image
        Dim image_mask As Image(Of Gray, Byte) = image.InRange(New Bgr(10, 10, 10), New Bgr(255, 255, 255))

        'Set their value to teal to get a red mask since (black - teal = red).
        image.SetValue(New Bgr(255, 255, 0), image_mask)

        Return image
    End Function
End Class
