Imports Emgu.CV
Imports System.Drawing
Imports Emgu.CV.Structure

Public Class Verification
    Implements IVerification

    Private compare_pixels_color As New ComparePixelColor()
    Private red_circle = Image.FromFile(New Config().GetRedCirclePath)
    Private search_pixel As SearchPixelInImage
    Private anchor As Anchor
    Private draw As New DrawOnImage()

    Protected top_right_anchor, bot_left_anchor As Point
    Protected bitmap_image As Bitmap

    Private Const LINE_SIZE As Integer = 1
    Private Const ANCHOR_LINE_SIZE As Integer = 2

    Public Sub New()
        search_pixel = New SearchPixelInImage()
    End Sub

    Public Function VerifiyWithTemnplate(images() As Image, template_image As Image,
                            Optional distance_threshold As Single = 0) As Image() Implements IVerification.VerifiyWithTemnplate
        Dim image_template As New Image(Of Bgr, Byte)(template_image)
        Dim emgu_images(images.Length) As Image(Of Bgr, Byte)
        Dim image_result As Image(Of Bgr, Byte)
        Dim image_mask As Image(Of Gray, Byte)
        Dim config As New Config()
        Dim threshold As Threshold

        For image As Integer = 1 To 1 'images.Length - 1
            bitmap_image = New Bitmap(images(image))
            threshold = New Threshold(distance_threshold)

            emgu_images(image) = New Image(Of Bgr, Byte)(bitmap_image)

            threshold.SearchDifferences(image_template.Data, emgu_images(image).Data)
            image_result = emgu_images(image) - image_template
            threshold.RemovePixels(image_result.Data)

            image_mask = image_result.InRange(New Bgr(10, 10, 10), New Bgr(255, 255, 255))
            image_result.SetValue(New Bgr(255, 255, 0), image_mask) 'Set color to cyan since black - cyan = red.

            images(image) = (emgu_images(image) - image_result).ToBitmap()
        Next

        Return images
    End Function

    Public Function DetermineInputZone(images() As Image) As Image() Implements IVerification.DetermineInputZone
        For image As Integer = 1 To images.Length - 1
            bitmap_image = New Bitmap(images(image))
            Dim anchor_points_bot_left As New List(Of Point)()
            Dim anchor_points_top_right As New List(Of Point)()
            anchor = New Anchor(bitmap_image)

            'Set the input zone with the first page's anchor only.
            If image = 1 Then
                anchor.FindTemplateAnchors()
                If anchor.GetBotLeftAnchor() = Nothing OrElse anchor.GetTopRightAnchor() = Nothing Then
                    AlertsManager.AddAlert(New AlertMessages().GetBlankPageMsg())
                    Exit For
                End If

                anchor.FindRealAnchorsCorners()

                bot_left_anchor = anchor.GetBotLeftAnchor()
                top_right_anchor = anchor.GetTopRightAnchor()

                'Save all pixels of each anchor into a list of points. This allow us to ignore these pixels
                'when checking for out of bounds pixels later (anchors can't be out of bounds).
                anchor_points_bot_left = search_pixel.FindAllPixelsOfColorNextToPoint(bitmap_image,
                                                                                      bot_left_anchor,
                                                                                      0, 0, 0)
                anchor_points_top_right = search_pixel.FindAllPixelsOfColorNextToPoint(bitmap_image,
                                                                                       top_right_anchor,
                                                                                       0, 0, 0)

                'Set the corner of the starting position of the input zone farther than the real anchor's corner's position.
                'This gives us a treshold to work with when checking out of bound pixels.
                bot_left_anchor.X += ANCHOR_LINE_SIZE
                bot_left_anchor.Y -= ANCHOR_LINE_SIZE
                top_right_anchor.X -= ANCHOR_LINE_SIZE
                top_right_anchor.Y += ANCHOR_LINE_SIZE
            End If

            'Draw the input zone for each zones (left, right, top, bottom).
            draw.DrawZone(bitmap_image, bot_left_anchor.X, top_right_anchor.X,
                          bot_left_anchor.Y - LINE_SIZE, bot_left_anchor.Y, 1, 1, Color.Green)
            draw.DrawZone(bitmap_image, bot_left_anchor.X, top_right_anchor.X,
                          top_right_anchor.Y + LINE_SIZE, top_right_anchor.Y, 1, -1, Color.Green)
            draw.DrawZone(bitmap_image, bot_left_anchor.X, bot_left_anchor.X + LINE_SIZE,
                          top_right_anchor.Y, bot_left_anchor.Y, 1, 1, Color.Green)
            draw.DrawZone(bitmap_image, top_right_anchor.X, top_right_anchor.X - LINE_SIZE,
                          top_right_anchor.Y, bot_left_anchor.Y, -1, 1, Color.Green)

            'Search through all pixels outside the input zone and mark them.
            Dim outside_pixels As New List(Of Point)()
            outside_pixels.AddRange(draw.MarkPixelWithImage(bitmap_image, anchor_points_bot_left, anchor_points_top_right, 0,
                                 bitmap_image.Width - 1, 0, top_right_anchor.Y - 1, 1, 1, red_circle))
            outside_pixels.AddRange(draw.MarkPixelWithImage(bitmap_image, anchor_points_bot_left, anchor_points_top_right, 0,
                                  bitmap_image.Width - 1, bot_left_anchor.Y + 1, bitmap_image.Height - 1, 1, 1, red_circle))
            outside_pixels.AddRange(draw.MarkPixelWithImage(bitmap_image, anchor_points_bot_left, anchor_points_top_right, 0,
                                  bot_left_anchor.X - 1, 0, bitmap_image.Height - 1, 1, 1, red_circle))
            outside_pixels.AddRange(draw.MarkPixelWithImage(bitmap_image, anchor_points_bot_left, anchor_points_top_right, top_right_anchor.X + 1,
                                  bitmap_image.Width - 1, 0, bitmap_image.Height - 1, 1, 1, red_circle))

            If outside_pixels.Count > 0 Then
                If outside_pixels.Count > 1 Then
                    AlertsManager.AddAlert("Page " + image.ToString + " - " + New AlertMessages().GetElementsOutOfZoneMsg)
                Else
                    AlertsManager.AddAlert("Page " + image.ToString + " - " + New AlertMessages().GetElementOutOfZoneMsg)
                End If
            End If

            images(image) = bitmap_image
        Next
        Return images
    End Function
End Class
