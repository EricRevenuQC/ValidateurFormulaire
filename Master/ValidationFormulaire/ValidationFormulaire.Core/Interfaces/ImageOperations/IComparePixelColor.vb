Public Interface IComparePixelColor
    Function DifferentPixelColor(color1a As Byte, color2a As Byte, color1b As Byte,
                                        color2b As Byte, color1c As Byte, color2c As Byte,
                                        Optional color_threshold As Integer = 50) As Boolean
End Interface
