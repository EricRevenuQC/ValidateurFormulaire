﻿Imports System.Drawing

Public Interface IFindFirstPixel
    Function Search(image As Bitmap, starting_point As Point, ending_point As Point, steps As Point, color As Colors) As Point
End Interface
