Public Class ComparePixelColor
    Implements IComparePixelColor

    Public Function DifferentPixelColor(color1a As Integer, color2a As Integer, color1b As Integer,
                                        color2b As Integer, color1c As Integer, color2c As Integer,
                                        Optional color_threshold As Integer = 50) As Boolean Implements IComparePixelColor.DifferentPixelColor
        Return ((color1a <= color2a - color_threshold) OrElse (color1b <= color2b - color_threshold) OrElse
                 (color1c <= color2c - color_threshold) OrElse
             (color1a >= color2a + color_threshold) OrElse (color1b >= color2b + color_threshold) OrElse
              (color1c >= color2c + color_threshold))
    End Function
End Class
