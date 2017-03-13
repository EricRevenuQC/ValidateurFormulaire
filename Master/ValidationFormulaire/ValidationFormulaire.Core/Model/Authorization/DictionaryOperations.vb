Public Class DictionaryOperations
    Public Function GetValueOfKeyAsString(bar_code_property As BarCodeProperties, dict As Dictionary(Of String, BarCodeData)) As String
        If dict IsNot Nothing Then
            If dict.ContainsKey(bar_code_property.ToString) Then
                Return dict.Item(bar_code_property.ToString).value.ToString
            End If
        End If
        Return ""
    End Function
End Class
