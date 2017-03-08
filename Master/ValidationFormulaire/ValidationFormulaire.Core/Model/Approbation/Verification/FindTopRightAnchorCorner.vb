Imports System.Drawing

Public Class FindTopRightAnchorCorner
    Implements IAnchorCorner
    Private find_first_pixel_of_different_color As IFindFirstPixel

    Public Sub New()
        find_first_pixel_of_different_color = New DifferentColor()
    End Sub

    Public Function FindAnchorCorner(bitmap_image As Bitmap,
                                     starting_point As Point) As Point Implements IAnchorCorner.FindAnchorCorner
        Dim current_positon As New Point(starting_point)
        Dim ending_point As New Point(bitmap_image.Width - 1, starting_point.Y)
        Dim color As New Colors(0, 0, 0)
        Dim steps As New Point(1, 1)

        current_positon.X = find_first_pixel_of_different_color.Search(bitmap_image, starting_point, ending_point, steps, color).X - 1

        ending_point = New Point(starting_point.X, 0)
        steps = New Point(1, -1)
        current_positon.Y = find_first_pixel_of_different_color.Search(bitmap_image, starting_point, ending_point, steps, color).Y + 1

        Return current_positon
    End Function
End Class
