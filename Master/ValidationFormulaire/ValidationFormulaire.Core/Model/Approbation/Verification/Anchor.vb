Imports System.Drawing

Public Class Anchor

    Private search_pixel As New SearchPixelInImage()

    Public Sub FindTemplateAnchors()
        
    End Sub

    Public Function FindRealAnchors() As Point

    End Function

    Public Function FindAnchorCorner(bitmap_image As Bitmap, starting_point As Point, corner As AnchorCorner) As Point
        Dim current_positon As New Point(starting_point)

        If corner = AnchorCorner.bot_left Then
            current_positon.X = search_pixel.FindFirstPixelOfDifferentColor(bitmap_image, starting_point.X, starting_point.Y,
                                                                          0, starting_point.Y, -1, 1,
                                                                          0, 0, 0).X + 1
            current_positon.Y = search_pixel.FindFirstPixelOfDifferentColor(bitmap_image, starting_point.X, starting_point.Y,
                                                                          starting_point.X, bitmap_image.Height - 1, 1, 1,
                                                                          0, 0, 0).Y - 1
        ElseIf corner = AnchorCorner.top_right Then
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
