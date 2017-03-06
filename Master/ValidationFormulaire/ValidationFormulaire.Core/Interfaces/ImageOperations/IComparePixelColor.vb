Public Interface IComparePixelColor
    Function DifferentPixelColor(color1a As Integer, color2a As Integer, color1b As Integer,
                                        color2b As Integer, color1c As Integer, color2c As Integer,
                                        Optional color_threshold As Integer = 50) As Boolean
End Interface
