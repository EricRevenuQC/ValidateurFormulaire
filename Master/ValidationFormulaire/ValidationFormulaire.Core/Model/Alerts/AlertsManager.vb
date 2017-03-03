Public Class AlertsManager

    Private alerts As Dictionary(Of String, String)

    Private Const chars_not_found_message As String = "Au moins un champ du code à bar n'a pas été trouvé!"
    Private Const zeros_found_message As String = "Des zéros ont été trouvés au lieu de cases vides!"

    Public Sub New()
        alerts = New Dictionary(Of String, String)
    End Sub

    Public Sub AddAlert(alert_title As String, Optional alert_message As String = "")
        alerts.Add(alert_title, alert_message)
    End Sub

    Public Sub AddMessageToTitle(alert_title As String, Optional alert_message As String = "")
        alerts(alert_title) += Environment.NewLine + alert_message
    End Sub

    Public Function GetAllAlerts() As Dictionary(Of String, String)
        Return alerts
    End Function

    Public Function GetCharNotFoundMsg() As String
        Return chars_not_found_message
    End Function

    Public Function GetZerosFoundMsg() As String
        Return zeros_found_message
    End Function
End Class
