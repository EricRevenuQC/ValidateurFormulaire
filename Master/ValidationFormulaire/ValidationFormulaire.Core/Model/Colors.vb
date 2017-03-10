Public Class Colors
    Public red As Integer
    Public blue As Integer
    Public green As Integer

    Public Sub New()
        red = 0
        blue = 0
        green = 0
    End Sub

    Public Sub New(blue As Integer, green As Integer, red As Integer)
        Me.red = red
        Me.blue = blue
        Me.green = green
    End Sub
End Class
