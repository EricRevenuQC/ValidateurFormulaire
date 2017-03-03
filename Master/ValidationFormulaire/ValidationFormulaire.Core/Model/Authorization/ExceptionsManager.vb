Public Class ExceptionsManager

    Private alerts_manager As AlertsManager

    Public Sub New(alerts_manager As AlertsManager)
        Me.alerts_manager = alerts_manager
    End Sub

    Public Function CheckWordForZerosException(word As String) As Boolean
        If New System.Text.RegularExpressions.Regex("^0+$").IsMatch(word) Then
            'Return error that the string should be emtpy instead of being zeros
            If Not alerts_manager.GetAllAlerts.ContainsKey(New AlertMessages().GetZerosFoundMsg) Then
                alerts_manager.AddAlert(New AlertMessages().GetZerosFoundMsg)
            End If
        End If
        Return Not New System.Text.RegularExpressions.Regex("^0+$").IsMatch(word)
    End Function
End Class
