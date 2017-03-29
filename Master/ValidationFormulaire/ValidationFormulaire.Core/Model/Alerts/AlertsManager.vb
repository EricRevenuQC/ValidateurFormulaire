Public Module AlertsManager
    Private alerts As Dictionary(Of String, String)
    Private alert_title As String
    Private alert_color As String

    Private Const ALERT_TITLE_DEFAULT_VALUE As String = "Attention! Des erreurs ont été trouvées."
    Private Const ALERT_COLOR_DEFAULT As String = "alert-danger"

    Sub New()
        alerts = New Dictionary(Of String, String)
        alert_title = ALERT_TITLE_DEFAULT_VALUE
        alert_color = ALERT_COLOR_DEFAULT
    End Sub

    Public Sub SetAlertTitle(title As String)
        alert_title = title
    End Sub

    Public Sub SetAlertColor(color As String)
        alert_color = color
    End Sub

    Public Sub AddAlert(alert_title As String, Optional alert_message As String = "")
        If Not alerts.ContainsKey(alert_title) Then
            alerts.Add(alert_title, alert_message)
        Else
            alerts(alert_title) += Environment.NewLine + alert_message
        End If
    End Sub

    Public Function GetAllAlerts() As Dictionary(Of String, String)
        Return alerts
    End Function

    Public Function GetAlertTitle() As String
        Return alert_title
    End Function

    Public Function GetAlertColor() As String
        Return alert_color
    End Function

    Public Sub ClearAlerts()
        alerts.Clear()
        alert_title = ALERT_TITLE_DEFAULT_VALUE
        alert_color = ALERT_COLOR_DEFAULT
    End Sub
End Module
