Public Class ExceptionsManager
    Public Function CheckWordForZerosException(word As String) As Boolean
        If New System.Text.RegularExpressions.Regex("^0+$").IsMatch(word) Then
            'Return error that the string should be emtpy instead of being zeros
            If Not AlertsManager.GetAllAlerts.ContainsKey(New AlertMessages().GetZerosFoundMsg) Then
                AlertsManager.AddAlert(New AlertMessages().GetZerosFoundMsg)
            End If
        End If
        Return Not New System.Text.RegularExpressions.Regex("^0+$").IsMatch(word)
    End Function
End Class
