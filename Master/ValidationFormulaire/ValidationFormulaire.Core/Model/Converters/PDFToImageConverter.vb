Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text

Imports Emgu.CV
Imports Emgu.Util
Imports System.Diagnostics
Imports System.IO
Imports System.Reflection
Imports System.Drawing.Imaging
Imports System.Web

Public Class PDFToImageConverter
    Implements IConverter

    Private page_number As Integer
    Private config As New Config()

    Public Function PDFToImage(file As HttpPostedFileBase) As Image() Implements IConverter.PDFToImage
        Using rasterizer = New Ghostscript.NET.Rasterizer.GhostscriptRasterizer()
            Try
                rasterizer.Open(file.InputStream)
                page_number = rasterizer.PageCount
                Dim images(page_number) As Image
                For i As Integer = 1 To page_number
                    images(i) = rasterizer.GetPage(config.GetDPI, config.GetDPI, i)
                    If (images(i) Is Nothing) Then
                        Throw New System.Exception("Could not convert pdf to image.")
                    End If
                    'Toutes les images font une rotation de 90 degrés dans le sens anti-horraire sauf la première.
                    'Il faut donc leur faire une rotation de 90 degrés dans le sens horraire pour les rendre droite.
                    If (i > 1) Then
                        images(i).RotateFlip(RotateFlipType.Rotate90FlipNone)
                    End If
                Next
                rasterizer.Close()
                Return images
            Catch ex As Exception
                AlertsManager.AddAlert("Incapable de convertir ce fichier en image! Assurez-vous que le fichier n'est pas protégé.")
                System.Diagnostics.Debug.WriteLine(ex)
            End Try
            Return Nothing
        End Using
    End Function

    Public Function GetPageNumber() As Integer Implements IConverter.GetPageNumber
        Return page_number
    End Function
End Class
