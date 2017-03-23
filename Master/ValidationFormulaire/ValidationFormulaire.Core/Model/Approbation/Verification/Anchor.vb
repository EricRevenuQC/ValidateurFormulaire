Imports System.Drawing

Public Class Anchor
    Private bot_left_anchor_position, top_right_anchor_position As Point
    Private image As Bitmap

    Private Const ANCHOR_LINE_SIZE As Integer = 2

    Public Sub New(image As Bitmap)
        Me.image = image
    End Sub

    Public Sub FindTemplateAnchors(find_first_pixel As IFindFirstPixel)
        Dim template_image As New Bitmap(New Config().GetVerificationImageTemplatePath)
        Dim color As New BGRColors(0, 0, 0)

        Dim starting_point As New Point(0, image.Height - 1)
        Dim ending_point As New Point(image.Width - 1, 0)
        Dim steps As New Point(1, -1)

        bot_left_anchor_position = find_first_pixel.Search(template_image, starting_point, ending_point, steps, color)

        starting_point = New Point(image.Width - 1, 0)
        ending_point = New Point(0, image.Height - 1)
        steps = New Point(-1, 1)

        top_right_anchor_position = find_first_pixel.Search(template_image, starting_point, ending_point, steps, color)

        If bot_left_anchor_position = Nothing OrElse top_right_anchor_position = Nothing Then
            AlertsManager.AddAlert(New AlertMessages().GetBlankPageMsg())
        End If
    End Sub

    Public Sub FindRealAnchorsCorners(find_first_pixel As IFindFirstPixel)
        Dim color As New BGRColors(0, 0, 0)
        Dim anchor_found As Point
        Dim bot_left_anchor_corner As New FindBotLeftAnchorCorner()
        Dim top_right_anchor_corner As New FindTopRightAnchorCorner()

        anchor_found = find_first_pixel.Search(image, bot_left_anchor_position, Nothing, Nothing, color)

        If anchor_found = Nothing Then
            AlertsManager.AddAlert(New AlertMessages().GetBotLeftAnchorMsg)
        Else
            bot_left_anchor_position = bot_left_anchor_corner.FindAnchorCorner(image, anchor_found)
        End If

        anchor_found = find_first_pixel.Search(image, top_right_anchor_position, Nothing, Nothing, color)

        If anchor_found = Nothing Then
            AlertsManager.AddAlert(New AlertMessages().GetTopRightAnchorMsg)
        Else
            top_right_anchor_position = top_right_anchor_corner.FindAnchorCorner(image, anchor_found)
        End If
    End Sub

    Public Sub AdjustAnchorsPosition()
        bot_left_anchor_position.X += ANCHOR_LINE_SIZE
        bot_left_anchor_position.Y -= ANCHOR_LINE_SIZE
        top_right_anchor_position.X -= ANCHOR_LINE_SIZE
        top_right_anchor_position.Y += ANCHOR_LINE_SIZE
    End Sub

    Public Function GetBotLeftAnchor()
        Return bot_left_anchor_position
    End Function

    Public Function GetTopRightAnchor()
        Return top_right_anchor_position
    End Function
End Class
