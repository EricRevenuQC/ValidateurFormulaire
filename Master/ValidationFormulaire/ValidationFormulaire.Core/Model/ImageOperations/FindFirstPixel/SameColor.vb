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
        'Dim pixel_location As Point
        'Dim zone_width = Math.Abs(ending_point.X - starting_point.X - 1)
        'Dim zone_height = Math.Abs(ending_point.Y - starting_point.Y - 1)

        'System.Diagnostics.Debug.WriteLine("Width : " + zone_width.ToString + " height : " + zone_height.ToString _
        '                                   + " X : " + starting_point.X.ToString + " Y : " + starting_point.Y.ToString)

        'Dim image_data As BitmapData = image.LockBits(
        '    New Rectangle(0, 0, zone_width, zone_height),
        '    ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb)
        'Dim image_bytes As Byte() = New Byte(Math.Abs(image_data.Stride) * zone_height) {}
        'Dim scan0 As IntPtr = image_data.Scan0

        'Marshal.Copy(scan0, image_bytes, 0, image_bytes.Length)

        'For i As Integer = 0 To image_bytes.Length - 1 Step 3
        '    If (Not compare_pixels_color.DifferentPixelColor(image_bytes(i), color.blue,
        '                                           image_bytes(i + 1), color.green,
        '                                           image_bytes(i + 2), color.red)) Then
        '        pixel_location = New Point(i - (zone_width * Math.Floor(i / zone_width)),
        '                                   Math.Ceiling(i / zone_width))
        '        image.UnlockBits(image_data)
        '        System.Diagnostics.Debug.WriteLine("X : " + pixel_location.X.ToString + " Y : " + pixel_location.Y.ToString)
        '        Return pixel_location
        '    End If
        'Next

        For current_row As Integer = starting_point.X To ending_point.X Step steps.X
            For current_col As Integer = starting_point.Y To ending_point.Y Step steps.Y
                If (Not compare_pixels_color.DifferentPixelColor(image.GetPixel(current_row, current_col).B, color.blue,
                                                   image.GetPixel(current_row, current_col).G, color.green,
                                                   image.GetPixel(current_row, current_col).R, color.red)) Then
                    Return New Point(current_row, current_col)
                End If
            Next
        Next

        'image.UnlockBits(image_data)
        Return Nothing
    End Function
End Class
