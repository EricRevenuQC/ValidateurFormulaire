Imports ValidationFormulaire.Core.Config

Public Interface IConfig
    Function GetVerificationImageTemplatePath() As String
    Function GetBlankImagePath() As String
    Function GetRedCirclePath() As String
    Function GetDPI() As Integer
End Interface
