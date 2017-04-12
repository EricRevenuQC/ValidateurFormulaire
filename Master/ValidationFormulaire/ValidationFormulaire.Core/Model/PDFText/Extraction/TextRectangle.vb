Public Class TextRectangle
    Public rectangle As iTextSharp.text.Rectangle
    Public text As [String]
    Public font As String

    Public Sub New(rect As iTextSharp.text.Rectangle, text As [String], font As String)
        Me.rectangle = rect
        Me.text = text
        Me.font = font
    End Sub
End Class
