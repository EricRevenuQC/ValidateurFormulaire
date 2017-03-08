Imports System.Drawing

Public Class Anchor
    Private search_pixel As New SearchPixelInImage()
    Private bot_left_anchor, top_right_anchor As Point
    Private image As Bitmap
    Private find_first_pixel_of_same_color, find_first_pixel_of_same_color_diagonally As IFindFirstPixel
    Private bot_left_corner, top_right_corner As IAnchorCorner

    Public Sub New(image As Bitmap)
        find_first_pixel_of_same_color = New SameColor()
        find_first_pixel_of_same_color_diagonally = New SameColorDiagonally()
        bot_left_corner = New FindBotLeftAnchorCorner()
        top_right_corner = New FindTopRightAnchorCorner()
        Me.image = image
    End Sub

    Public Sub FindTemplateAnchors()
        Dim template_image As New Bitmap(New Config().GetVerificationImageTemplatePath)
        Dim starting_point As New Point(0, image.Height - 1)
        Dim ending_point As New Point(image.Width - 1, 0)
        Dim steps As New Point(1, -1)
        Dim color As New Colors(0, 0, 0)

        bot_left_anchor = find_first_pixel_of_same_color.Search(template_image, starting_point, ending_point, steps, color)
        starting_point = New Point(image.Width - 1, 0)
        ending_point = New Point(0, image.Height - 1)
        steps = New Point(-1, 1)
        top_right_anchor = find_first_pixel_of_same_color.Search(template_image, starting_point, ending_point, steps, color)
    End Sub

    Public Function FindRealAnchorsCorners() As Point
        Dim color As New Colors(0, 0, 0)
        Dim anchor_found As Point

        anchor_found = find_first_pixel_of_same_color_diagonally.Search(image, bot_left_anchor, Nothing, Nothing, color)

        If anchor_found = Nothing Then
            AlertsManager.AddAlert(New AlertMessages().GetBotLeftAnchorMsg)
        Else
            bot_left_anchor = bot_left_corner.FindAnchorCorner(image, anchor_found)
        End If

        anchor_found = find_first_pixel_of_same_color_diagonally.Search(image, top_right_anchor, Nothing, Nothing, color)

        If anchor_found = Nothing Then
            AlertsManager.AddAlert(New AlertMessages().GetTopRightAnchorMsg)
        Else
            top_right_anchor = top_right_corner.FindAnchorCorner(image, anchor_found)
        End If
    End Function

    Public Function GetBotLeftAnchor()
        Return bot_left_anchor
    End Function

    Public Function GetTopRightAnchor()
        Return top_right_anchor
    End Function
End Class
