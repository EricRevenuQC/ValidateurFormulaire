Imports Emgu.CV
Imports System.Drawing
Imports Emgu.CV.Structure

Public Class Verification
    Implements IVerification

    Public Enum Corner
        top_right
        bot_left
    End Enum

    Private compare_pixels_color As New ComparePixelColor()
    Private red_circle = Image.FromFile(New Config().GetRedCirclePath)
    Private search_pixel As New SearchPixelInImage()
    Private draw As New DrawOnImage()

    Private Const LINE_SIZE As Integer = 1
    Private Const ANCHOR_LINE_SIZE As Integer = 2

    Public Function VerifiyWithTemnplate(images() As Image, template_image As Image,
                            Optional distance_threshold As Single = 0,
                            Optional alerts_manager As AlertsManager = Nothing) As Image() Implements IVerification.VerifiyWithTemnplate
        Dim image_template As New Image(Of Bgr, Byte)(template_image)
        Dim emgu_images(images.Length) As Image(Of Bgr, Byte)
        Dim image_result As Image(Of Bgr, Byte)
        Dim image_mask As Image(Of Gray, Byte)
        Dim config As New Config()
        Dim threshold As Threshold

        For image As Integer = 1 To 1 'images.Length - 1
            Dim bitmap_image As New Bitmap(images(image))
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

    Public Function DetermineInputZone(images() As Image, alerts_manager As AlertsManager) As Image() Implements IVerification.DetermineInputZone
        Dim bitmap_image As Bitmap
        Dim top_right_anchor, bot_left_anchor As Point

        For image As Integer = 1 To images.Length - 1
            bitmap_image = New Bitmap(images(image))
            Dim anchor_points_bot_left As New List(Of Point)()
            Dim anchor_points_top_right As New List(Of Point)()

            'Set the input zone with the first page's anchor only.
            If image = 1 Then
                'Find both anchors on template image by searching for the first black pixel starting by the
                'bot-left and top-right corners.
                bot_left_anchor = search_pixel.FindFirstPixelOfColor(New Bitmap(New Config().GetVerificationImageTemplatePath),
                                                      0, bitmap_image.Height - 1, bitmap_image.Width - 1, 0, 1, -1,
                                                      0, 0, 0)
                top_right_anchor = search_pixel.FindFirstPixelOfColor(New Bitmap(New Config().GetVerificationImageTemplatePath),
                                                       bitmap_image.Width - 1, 0, 0, bitmap_image.Height - 1, -1, 1,
                                                       0, 0, 0)

                'Find the real anchor from the template's anchor's position.
                Dim anchor_found As Point = search_pixel.FindFirstPixelOfColorDiagonally(bitmap_image, bot_left_anchor, 0, 0, 0)
                If anchor_found = Nothing Then
                    alerts_manager.AddAlert(New AlertMessages().GetBotLeftAnchorMsg)
                Else
                    bot_left_anchor = anchor_found
                End If
                anchor_found = search_pixel.FindFirstPixelOfColorDiagonally(bitmap_image, top_right_anchor, 0, 0, 0)
                If anchor_found = Nothing Then
                    alerts_manager.AddAlert(New AlertMessages().GetTopRightAnchorMsg)
                Else
                    top_right_anchor = anchor_found
                End If

                bot_left_anchor = FindAnchorCorner(bitmap_image, bot_left_anchor, Corner.bot_left)
                top_right_anchor = FindAnchorCorner(bitmap_image, top_right_anchor, Corner.top_right)

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
                    alerts_manager.AddAlert(
                        "Page " + image.ToString + " - " + New AlertMessages().GetElementsOutOfZoneMsg)
                Else
                    alerts_manager.AddAlert(
                        "Page " + image.ToString + " - " + New AlertMessages().GetElementOutOfZoneMsg)
                End If
            End If

            images(image) = bitmap_image
        Next
        Return images
    End Function

    Private Function FindAnchorCorner(bitmap_image As Bitmap, starting_point As Point, corner As Corner) As Point
        Dim current_positon As New Point(starting_point)

        If corner = Verification.Corner.bot_left Then
            current_positon.X = search_pixel.FindFirstPixelOfDifferentColor(bitmap_image, starting_point.X, starting_point.Y,
                                                                          0, starting_point.Y, -1, 1,
                                                                          0, 0, 0).X + 1
            current_positon.Y = search_pixel.FindFirstPixelOfDifferentColor(bitmap_image, starting_point.X, starting_point.Y,
                                                                          starting_point.X, bitmap_image.Height - 1, 1, 1,
                                                                          0, 0, 0).Y - 1
        ElseIf corner = Verification.Corner.top_right Then
            current_positon.X = search_pixel.FindFirstPixelOfDifferentColor(bitmap_image, starting_point.X, starting_point.Y,
                                                                          bitmap_image.Width - 1, starting_point.Y, 1, 1,
                                                                          0, 0, 0).X - 1
            current_positon.Y = search_pixel.FindFirstPixelOfDifferentColor(bitmap_image, starting_point.X, starting_point.Y,
                                                                          starting_point.X, 0, 1, -1,
                                                                          0, 0, 0).Y + 1
        End If
        Return current_positon
    End Function
End Class
