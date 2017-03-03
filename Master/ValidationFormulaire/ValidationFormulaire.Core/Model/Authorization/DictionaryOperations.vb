Public Class DictionaryOperations
    Public Function GetValueOfKeyAsString(bar_code_property As BarCodeProperties, dict As Dictionary(Of String, BarCodeData)) As String
        If dict.ContainsKey(bar_code_property.ToString) Then
            Return dict.Item(bar_code_property.ToString).value.ToString
        Else
            Return ""
        End If
    End Function
End Class
