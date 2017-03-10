Imports Emgu.CV
Imports Emgu.CV.Structure
Imports System.Drawing

Public Class Verification
    Implements IVerification

    Private red_circle = Drawing.Image.FromFile(New Config().GetRedCirclePath)
    Private draw_input_zone As DrawInputZone
    Private anchor As Anchor
    Private pixel_marker As PixelMarker

    Private Const FIRST_PAGE As Integer = 1

    Public Function Verification(images() As Image) As Image() Implements IVerification.Verification
        For page As Integer = 1 To images.Length - 1
            draw_input_zone = New DrawInputZone(images(page))
            pixel_marker = New PixelMarker(images(page), red_circle)

            If page = FIRST_PAGE Then
                anchor = New Anchor(images(page))
                anchor.FindTemplateAnchors(New SameColor())
                anchor.FindRealAnchorsCorners(New SameColorDiagonally())
                pixel_marker.FindAllAnchorsPixels(anchor.GetBotLeftAnchor(), anchor.GetTopRightAnchor())
                anchor.AdjustAnchorsPosition()
            End If

            draw_input_zone.DrawInputZoneFromAnchors(anchor.GetBotLeftAnchor(), anchor.GetTopRightAnchor())
            pixel_marker.FindAllOutOfBoundColoredPixels(anchor.GetBotLeftAnchor(), anchor.GetTopRightAnchor(), page)
            images(page) = images(page)
        Next
        Return images
    End Function

    Public Function CalculateThreshold(images() As Image, template_image As Image,
                                       Optional distance_threshold As Integer = 0) _
                                   As Image() Implements IVerification.CalculateThreshold
        Dim threshold = New Threshold(distance_threshold)
        Dim bitmap_image As New Bitmap(images(FIRST_PAGE))
        Dim emgu_image = New Image(Of Bgr, Byte)(bitmap_image)
        Dim emgu_template_image = New Image(Of Bgr, Byte)(template_image)

        threshold.SearchForDifferencesBetweenImages(emgu_template_image.Data, emgu_image.Data)
        Dim image_result As Image(Of Bgr, Byte) = emgu_image - emgu_template_image
        threshold.RemovePixels(image_result.Data)

        image_result = New ImageMask().ApplyRedMaskToImage(image_result)

        images(FIRST_PAGE) = (emgu_image - image_result).ToBitmap()
        Return images
    End Function
End Class
