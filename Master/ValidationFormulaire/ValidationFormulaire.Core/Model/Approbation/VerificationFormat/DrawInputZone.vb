Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging

Public Class DrawInputZone
    Private image As Bitmap
    Private compare_pixels_color As ComparePixelColor

    Private LINE_COLOR As Color = Color.Green
    Private Const LINE_SIZE As Integer = 1

    Public Sub New(image As Bitmap)
        Me.image = image
        compare_pixels_color = New ComparePixelColor()
    End Sub

    Public Sub DrawInputZoneFromAnchors(bot_left_anchor_position As Point, top_right_anchor_position As Point)
        Dim starting_position As New Point(bot_left_anchor_position.X, bot_left_anchor_position.Y - LINE_SIZE)
        Dim ending_position As New Point(top_right_anchor_position.X, bot_left_anchor_position.Y)
        Dim steps As New Point(1, 1)

        DrawLine(starting_position, ending_position, steps)

        starting_position = New Point(bot_left_anchor_position.X, top_right_anchor_position.Y + LINE_SIZE)
        ending_position = New Point(top_right_anchor_position.X, top_right_anchor_position.Y)
        steps = New Point(1, -1)

        DrawLine(starting_position, ending_position, steps)

        starting_position = New Point(bot_left_anchor_position.X, top_right_anchor_position.Y)
        ending_position = New Point(bot_left_anchor_position.X + LINE_SIZE, bot_left_anchor_position.Y)
        steps = New Point(1, 1)

        DrawLine(starting_position, ending_position, steps)

        starting_position = New Point(top_right_anchor_position.X, bot_left_anchor_position.X + LINE_SIZE)
        ending_position = New Point(top_right_anchor_position.X - LINE_SIZE, bot_left_anchor_position.Y)
        steps = New Point(-1, 1)

        DrawLine(starting_position, ending_position, steps)
    End Sub

    Private Sub DrawLine(starting_position As Point, ending_position As Point, steps As Point)
        Dim position As Integer
        Dim image_data As BitmapData = image.LockBits(
            New Rectangle(0, 0, image.Width, image.Height),
            ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb)
        Dim image_bytes As Byte() = New Byte(Math.Abs(image_data.Stride) * image.Height) {}
        Dim scan0 As IntPtr = image_data.Scan0
        Marshal.Copy(scan0, image_bytes, 0, image_bytes.Length - 1)

        For x As Integer = starting_position.X To ending_position.X Step steps.X
            For y As Integer = starting_position.Y To ending_position.Y Step steps.Y
                position = (y * image_data.Stride) + (x * Drawing.Image.GetPixelFormatSize(image_data.PixelFormat) / 8)
                If (Not compare_pixels_color.DifferentPixelColor(image_bytes(position), 255,
                                                   image_bytes(position + 1), 255,
                                                   image_bytes(position + 2), 255)) Then
                    image_bytes(position) = 0
                    image_bytes(position + 1) = 255
                    image_bytes(position + 2) = 0
                End If
            Next
        Next

        Marshal.Copy(image_bytes, 0, scan0, image_bytes.Length - 1)
        image.UnlockBits(image_data)
    End Sub
End Class
