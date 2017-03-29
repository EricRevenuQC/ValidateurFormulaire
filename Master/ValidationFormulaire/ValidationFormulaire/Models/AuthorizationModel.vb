Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel
Imports ValidationFormulaire.Core

Namespace Models
    Public Class AuthorizationModel
        Public Property txtCodeGabrt As String
        Public Property txtNumLogiciel As String
        Public Property txtNumPreparateur As String
        Public Property txtAnnee As String
        Public Property txtCodeReleve As String
        Public Property txtDernierNum As String
        Public Property txtNumOriginal As String
        Public Property txtNumPreImprimer As String
        Public Property txtA As String
        Public Property txtB As String
        Public Property txtC As String
        Public Property txtD As String
        Public Property txtE As String
        Public Property txtF As String
        Public Property txtG As String
        Public Property txtH As String
        Public Property txtI As String
        Public Property txtJ As String
        Public Property txtK As String
        Public Property txtL As String
        Public Property txtM As String
        Public Property txtN As String
        Public Property txtO As String
        Public Property txtP As String
        Public Property txtQ As String
        Public Property txtR As String
        Public Property txtS As String
        Public Property txtIndS As String
        Public Property txtT As String
        Public Property txtU As String
        Public Property txtV As String
        Public Property txtW As String
        Public Property txtCodeO As String
        Public Property txtNas As String
        Public Property txtNumRef As String
        Public Property txtNomFamille As String
        Public Property txtPrenom As String
        Public Property txtAdresse As String
        Public Property txtAdresse2 As String
        Public Property txtVille As String
        Public Property txtProv As String
        Public Property txtCodePostal As String
        Public Property txtNomEmployeur As String
        Public Property txtNomEmployeur2 As String
        Public Property txtAdresseEmployeur As String
        Public Property txtAdresseEmployeur2 As String
        Public Property txtVilleEmployeur As String
        Public Property txtProvEmployeur As String
        Public Property txtCodePostalEmployeur As String
        Public Property txtCode1 As String
        Public Property txtInfo1 As String
        Public Property txtCode2 As String
        Public Property txtInfo2 As String
        Public Property txtCode3 As String
        Public Property txtInfo3 As String
        Public Property txtCode4 As String
        Public Property txtInfo4 As String

        Public Property alert_title As String
        Public Property alert_messages As Dictionary(Of String, String)
        Public Property alert_color As String
        Public Property bar_code_data As New Dictionary(Of String, BarCodeData)()
        Public Property bar_code_unverified_data As New Dictionary(Of String, BarCodeData)()
    End Class

    Public Class AuthorizationDB
        Inherits DbContext

        Public Property Authorization As DbSet(Of AuthorizationModel)
    End Class
End Namespace