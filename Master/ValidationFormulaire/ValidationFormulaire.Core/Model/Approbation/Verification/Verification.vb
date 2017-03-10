Imports Emgu.CV
Imports System.Drawing
Imports Emgu.CV.Structure

Public Class Verification
    Implements IVerification

    Private red_circle = Image.FromFile(New Config().GetRedCirclePath)
    Private draw_input_zone As DrawInputZone
    Private anchor As Anchor
    Private pixel_marker As PixelMarker
    Private bitmap_image As Bitmap

    Private Const FIRST_PAGE As Integer = 1

    Public Sub New()
        anchor = New Anchor(bitmap_image)
    End Sub

    Public Function VerifiyWithTemnplate(images() As Image, template_image As Image,
                            Optional distance_threshold As Single = 0) As Image() _
                        Implements IVerification.VerifiyWithTemnplate
        Dim image_template As New Image(Of Bgr, Byte)(template_image)
        Dim emgu_images(images.Length) As Image(Of Bgr, Byte)
        Dim image_result As Image(Of Bgr, Byte)
        Dim image_mask As Image(Of Gray, Byte)
        Dim config As New Config()
        Dim threshold As Threshold

        For page As Integer = 1 To FIRST_PAGE
            bitmap_image = New Bitmap(images(page))
            threshold = New Threshold(distance_threshold)

            emgu_images(page) = New Image(Of Bgr, Byte)(bitmap_image)

            threshold.SearchForDifferencesBetweenImages(image_template.Data, emgu_images(page).Data)
            image_result = emgu_images(page) - image_template
            threshold.RemovePixels(image_result.Data)

            image_mask = image_result.InRange(New Bgr(10, 10, 10), New Bgr(255, 255, 255))
            image_result.SetValue(New Bgr(255, 255, 0), image_mask) 'Set color to cyan since black - cyan = red.

            images(page) = (emgu_images(page) - image_result).ToBitmap()
        Next

        Return images
    End Function

    Public Function DetermineInputZone(images() As Image) As Image() Implements IVerification.DetermineInputZone
        For page As Integer = 1 To images.Length - 1
            bitmap_image = New Bitmap(images(page))
            draw_input_zone = New DrawInputZone(bitmap_image)
            pixel_marker = New PixelMarker(bitmap_image, red_circle)

            If page = FIRST_PAGE Then
                anchor = New Anchor(bitmap_image)
                anchor.FindTemplateAnchors(New SameColor())
                anchor.FindRealAnchorsCorners(New SameColorDiagonally())
                pixel_marker.FindAllAnchorsPixels(anchor.GetBotLeftAnchor(), anchor.GetTopRightAnchor())
                anchor.AdjustAnchorsPosition()
            End If

            draw_input_zone.DrawInputZoneFromAnchors(anchor.GetBotLeftAnchor(), anchor.GetTopRightAnchor())
            pixel_marker.FindAllOutOfBoundPixels(anchor.GetBotLeftAnchor(), anchor.GetTopRightAnchor(), page)
            images(page) = bitmap_image
        Next
        Return images
    End Function
End Class
