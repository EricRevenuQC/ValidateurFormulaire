Public Class ComparePixelColor
    Implements IComparePixelColor

    Public Function DifferentPixelColor(color1a As Byte, color2a As Byte, color1b As Byte,
                                        color2b As Byte, color1c As Byte, color2c As Byte,
                                        Optional color_threshold As Integer = 50) As Boolean Implements IComparePixelColor.DifferentPixelColor
        Return ((color1a <= color2a - color_threshold) OrElse (color1b <= color2b - color_threshold) OrElse
                 (color1c <= color2c - color_threshold) OrElse
             (color1a >= color2a + color_threshold) OrElse (color1b >= color2b + color_threshold) OrElse
              (color1c >= color2c + color_threshold))
    End Function
End Class
