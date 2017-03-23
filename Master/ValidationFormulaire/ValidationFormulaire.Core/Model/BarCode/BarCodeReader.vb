Imports System.Web
Imports GdPicture9

Public Class BarCodeReader
    Private bar_code_text As String
    Private bar_code_id As String

    Private Sub ActivateGDPictureLicense()
        Dim GDPICTURE_CLE_LICENSE = "211862729908279711114164022435896"
        Dim oLicenseManager As New LicenseManager()
        oLicenseManager.RegisterKEY(GDPICTURE_CLE_LICENSE)
    End Sub

    Public Sub ReadBarCode(file As HttpPostedFileBase)
        ActivateGDPictureLicense()

        Dim codeBarreDidentification = String.Empty
        Dim oGdPicturePDF As New GdPicturePDF()
        Dim dictionnaireCodeBarres = New Dictionary(Of Int32, String)()

        If oGdPicturePDF.LoadFromStream(file.InputStream) = GdPictureStatus.OK Then
            oGdPicturePDF.SelectPage(0)
            Dim imageID As Integer = oGdPicturePDF.RenderPageToGdPictureImage(200, False)
            If imageID <> 0 Then
                Dim oGdPictureImaging As New GdPictureImaging()
                oGdPictureImaging.BarcodeDataMatrixReaderDoScan(imageID, BarcodeDataMatrixReaderScanMode.BestQuality)
                oGdPictureImaging.ReleaseGdPictureImage(imageID)

                For i As Integer = 1 To oGdPictureImaging.BarcodeDataMatrixReaderGetBarcodeCount()
                    dictionnaireCodeBarres.Add(i, oGdPictureImaging.BarcodeDataMatrixReaderGetBarcodeValue(i))
                Next
                oGdPictureImaging.BarcodeDataMatrixReaderClear()
            End If
            oGdPicturePDF.CloseDocument()
        End If

        For Each bar_code As KeyValuePair(Of Int32, String) In dictionnaireCodeBarres
            If bar_code.Value.Length > 10 Then
                bar_code_text = bar_code.Value
            Else
                bar_code_id = bar_code.Value
            End If
        Next
    End Sub

    Public Function GetBarCodeText()
        Return bar_code_text
    End Function

    Public Function GetBarCodeId()
        Return bar_code_id
    End Function
End Class
