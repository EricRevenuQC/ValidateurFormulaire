Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging

Public Class PixelMarker

    Private image, marker As Bitmap
    Private compare_pixels_color As IComparePixelColor
    Private color_selector As ColorSelector
    Private anchors_pixels As List(Of Point)

    Private Const PROXIMITY_PIXEL_DISTANCE As Integer = 10

    Public Sub New(image As Bitmap, marker As Bitmap)
        Me.image = image
        Me.marker = marker
        compare_pixels_color = New ComparePixelColor()
        color_selector = New ColorSelector(image)
        anchors_pixels = New List(Of Point)()
    End Sub

    Public Sub FindAllAnchorsPixels(bot_left_anchor As Point, top_right_anchor As Point)
        anchors_pixels.AddRange(color_selector.FindAllPixelsOfColorFromPoint(bot_left_anchor, New BGRColors(0, 0, 0)))
        anchors_pixels.AddRange(color_selector.FindAllPixelsOfColorFromPoint(top_right_anchor, New BGRColors(0, 0, 0)))
    End Sub

    Public Sub FindAllOutOfBoundColoredPixels(bot_left_anchor As Point, top_right_anchor As Point, page As Integer)
        Dim out_of_bound_pixels As New List(Of Point)()
        Dim steps As New Point(1, 1)

        Dim starting_position As New Point(0, 0)
        Dim ending_position As New Point(image.Width - 1, top_right_anchor.Y - 1)

        out_of_bound_pixels.AddRange(FindAllColoredPixelsInZone(starting_position, ending_position, steps, page))

        starting_position = New Point(0, bot_left_anchor.Y + 1)
        ending_position = New Point(image.Width - 1, image.Height - 1)

        out_of_bound_pixels.AddRange(FindAllColoredPixelsInZone(starting_position, ending_position, steps, page))

        starting_position = New Point(0, 0)
        ending_position = New Point(bot_left_anchor.X - 1, image.Height - 1)

        out_of_bound_pixels.AddRange(FindAllColoredPixelsInZone(starting_position, ending_position, steps, page))

        starting_position = New Point(top_right_anchor.X + 1, 0)
        ending_position = New Point(image.Width - 1, image.Height - 1)

        out_of_bound_pixels.AddRange(FindAllColoredPixelsInZone(starting_position, ending_position, steps, page))

        MarkPixels(out_of_bound_pixels)
        AddOutOfZoneAlerts(out_of_bound_pixels, page)
    End Sub

    Private Function FindAllColoredPixelsInZone(starting_point As Point, ending_point As Point,
                                  steps As Point, page As Integer) As List(Of Point)
        Dim error_pixels As New List(Of Point)()
        Dim proximity_error_pixels As New List(Of Point)()
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
                If compare_pixels_color.DifferentPixelColor(image_bytes(position), 255,
                                                   image_bytes(position + 1), 255,
                                                   image_bytes(position + 2), 255) And
                                           compare_pixels_color.DifferentPixelColor(image_bytes(position), 0,
                                                   image_bytes(position + 1), 0,
                                                   image_bytes(position + 2), 255) Then
                    If Not proximity_error_pixels.Contains(New Point(x, y)) And
                            Not anchors_pixels.Contains(New Point(x, y)) Then
                        error_pixels.Add(New Point(x, y))
                        For i As Integer = x - PROXIMITY_PIXEL_DISTANCE To x + PROXIMITY_PIXEL_DISTANCE Step steps.X
                            For j As Integer = y - PROXIMITY_PIXEL_DISTANCE To y + PROXIMITY_PIXEL_DISTANCE Step steps.Y
                                proximity_error_pixels.Add(New Point(i, j))
                            Next
                        Next
                    End If
                End If
            Next
        Next

        image.UnlockBits(image_data)
        Return error_pixels
    End Function

    Private Sub MarkPixels(error_pixels As List(Of Point))
        For Each pixel In error_pixels
            Dim graphic = Graphics.FromImage(image)
            graphic.DrawImage(marker, New Point(pixel.X - (marker.Width / 2), pixel.Y - (marker.Height / 2)))
            graphic.Flush()
            graphic.Dispose()
        Next
    End Sub

    Private Sub AddOutOfZoneAlerts(out_of_bound_pixels As List(Of Point), page As Integer)
        If out_of_bound_pixels.Count > 0 Then
            If out_of_bound_pixels.Count > 1 Then
                AlertsManager.AddAlert("Page " + page.ToString + " - " + New AlertMessages().GetElementsOutOfZoneMsg)
            Else
                AlertsManager.AddAlert("Page " + page.ToString + " - " + New AlertMessages().GetElementOutOfZoneMsg)
            End If
        End If
    End Sub
End Class
