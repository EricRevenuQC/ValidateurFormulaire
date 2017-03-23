Public Module AlertsManager
    Private alerts As Dictionary(Of String, String)

    Sub New()
        alerts = New Dictionary(Of String, String)
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

    Public Sub ClearAlerts()
        alerts.Clear()
    End Sub
End Module
