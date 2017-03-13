Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Public Class SameColor
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
        Dim position As Integer
        Dim image_data As BitmapData = image.LockBits(
            New Rectangle(0, 0, image.Width, image.Height),
            ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb)
        Dim image_bytes As Byte() = New Byte(Math.Abs(image_data.Stride) * image.Height) {}
        Dim scan0 As IntPtr = image_data.Scan0
        Marshal.Copy(scan0, image_bytes, 0, image_bytes.Length - 1)

        For x As Integer = starting_point.X To ending_point.X Step steps.X
            For y As Integer = starting_point.Y To ending_point.Y Step steps.Y
                position = (y * image_data.Stride) + (x * Drawing.Image.GetPixelFormatSize(image_data.PixelFormat) / 8)
                If (Not compare_pixels_color.DifferentPixelColor(image_bytes(position), color.blue,
                                                   image_bytes(position + 1), color.green,
                                                   image_bytes(position + 2), color.red)) Then
                    image.UnlockBits(image_data)
                    Return New Point(x, y)
                End If
            Next
        Next

        image.UnlockBits(image_data)
        Return Nothing
    End Function
End Class
