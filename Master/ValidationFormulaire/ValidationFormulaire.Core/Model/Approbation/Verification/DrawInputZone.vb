Imports System.Drawing

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
        For current_row As Long = starting_position.X To ending_position.X Step steps.X
            For current_col As Long = starting_position.Y To ending_position.Y Step steps.Y
                If Not compare_pixels_color.DifferentPixelColor(image.GetPixel(current_row, current_col).B, 255,
                                                image.GetPixel(current_row, current_col).G, 255,
                                                image.GetPixel(current_row, current_col).R, 255) Then
                    image.SetPixel(current_row, current_col, LINE_COLOR)
                End If
            Next
        Next
    End Sub
End Class
