Imports System.Web

Public Class Config
    Implements IConfig

    Private Const BLANK_IMAGE_PATH As String = "~/Images/Blank.png"
    Private Const VERIFICATION_IMAGE_TEMPLATE As String = "~/Images/VerificationTemplate.png"
    Private Const RED_TRANSPARENT_CIRCLE As String = "~/Images/red_circle_transparent.png"
    Private Const DPI As Integer = 300

    Public Function GetBlankImagePath() As String Implements IConfig.GetBlankImagePath
        Return HttpContext.Current.Server.MapPath(BLANK_IMAGE_PATH)
    End Function

    Public Function GetRedCirclePath() As String Implements IConfig.GetRedCirclePath
        Return HttpContext.Current.Server.MapPath(RED_TRANSPARENT_CIRCLE)
    End Function

    Public Function GetDPI() As Integer Implements IConfig.GetDPI
        Return DPI
    End Function

    Public Function GetVerificationImageTemplatePath() As String Implements IConfig.GetVerificationImageTemplatePath
        Return HttpContext.Current.Server.MapPath(VERIFICATION_IMAGE_TEMPLATE)
    End Function
End Class
