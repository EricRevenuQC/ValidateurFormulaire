Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel

Namespace Models
    Public Class ApprobationModel
        <FileSize(1024000)> _
        <FileTypes("png, pdf")> _
        Public Property FilePath As Byte()
        Public Overridable Property File As HttpPostedFileBase
        Public Property File_left As HttpPostedFileBase
        Public Property File_right As HttpPostedFileBase
        Public Property files() As HttpPostedFileBase

        Public Property current_page As Integer
        Public Property current_page_left As Integer
        Public Property current_page_right As Integer
        Public Property page_number As Integer
        Public Property page_number_left As Integer
        Public Property page_number_right As Integer

        Public Property alert_title As String
        Public Property alert_messages As Dictionary(Of String, String)
    End Class

    Public Class ApprobationDB
        Inherits DbContext

        Public Property Approbation As DbSet(Of ApprobationModel)
    End Class
End Namespace