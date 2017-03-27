Public Class TextRectangle
    Public rectangle As iTextSharp.text.Rectangle
    Public text As [String]
    Public Sub New(rect As iTextSharp.text.Rectangle, text As [String])
        Me.rectangle = rect
        Me.text = text
    End Sub
End Class
