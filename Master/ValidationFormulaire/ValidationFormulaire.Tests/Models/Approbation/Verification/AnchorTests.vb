Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports ValidationFormulaire.Core
Imports System.Drawing
Imports Moq

<TestClass()> Public Class AnchorTests
    Private anchor As Anchor
    Private image As Bitmap
    Private find_first_pixel As Mock(Of IFindFirstPixel)

    <TestInitialize()> _
    Public Sub Initialize()
        Dim image_byte_array As Byte() = Convert.FromBase64String(
            "iVBORw0KGgoAAAANSUhEUgAAAAwAAAAKCAYAAACALL/6AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsIAAA7C" + _
            "ARUoSoAAAAA7SURBVChTY/wPBAwkAJwaGBkZoSwEACkl2gaQASClTFA+0YA8Ddjciw5gLqe9k8ChhCsIsQESI46BAQCrYh/82/CMyQAA" + _
            "AABJRU5ErkJggg==")
        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(image_byte_array)
        image = System.Drawing.Image.FromStream(ms)
        anchor = New Anchor(image)
    End Sub

    <TestMethod()> Public Sub TestMethod1()

    End Sub
End Class