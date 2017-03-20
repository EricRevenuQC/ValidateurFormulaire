Imports System.Drawing

Public Class VerifyReleves
    Private adjusted_bar_code_values As String
    Private dict_operations As DictionaryOperations
    Private releve As IReleves

    Private Const TOP_MARKER_VALUE As Integer = 1000
    Private Const EAST_MARKER_VALUE As Integer = 1000
    Private Const MARKER_DISTANCE_THRESHOLD As Integer = 5

    Public Sub New(releves As IReleves)
        dict_operations = New DictionaryOperations()
        releve = releves
    End Sub

    Public Function VerifyTextInputPosition(words As Dictionary(Of Point, String),
                                       bar_code_data As KeyValuePair(Of String, BarCodeData),
                                       bar_code_index As Integer) As Boolean
        Dim north_marker_position As Integer = 0
        Dim south_marker_position As Integer = 0
        Dim east_marker_position As Integer = 0
        Dim west_marker_position As Integer = 0

        adjusted_bar_code_values = bar_code_data.Value.value.Trim()
        releve.SetReleveMarkers(bar_code_index)

        If releve.GetNorthMarker = "TOP" Then
            north_marker_position = TOP_MARKER_VALUE
        ElseIf releve.GetNorthMarker <> "" Then
            north_marker_position = dict_operations.GetKeyFromValueContains(words, releve.GetNorthMarker,
                                                                            releve.GetNorthMarkerOccurence).Y
        End If

        If releve.GetSouthMarker <> "" Then
            south_marker_position = dict_operations.GetKeyFromValueContains(words, releve.GetSouthMarker,
                                                                            releve.GetSouthMarkerOccurence).Y
        End If

        If releve.GetEastMarker = "RIGHT" Then
            east_marker_position = EAST_MARKER_VALUE
        ElseIf releve.GetEastMarker <> "" Then
            east_marker_position = dict_operations.GetKeyFromValueContains(words, releve.GetEastMarker,
                                                                           releve.GetEastMarkerOccurence).X
        End If

        If releve.GetWestMarker <> "" Then
            west_marker_position = dict_operations.GetKeyFromValueContains(words, releve.GetWestMarker,
                                                                           releve.GetWestMarkerOccurence).X
        End If

        'System.Diagnostics.Debug.WriteLine("------------------------------------------------------")
        'System.Diagnostics.Debug.WriteLine(bar_code_index)
        'System.Diagnostics.Debug.WriteLine("Y :")
        'System.Diagnostics.Debug.WriteLine(north_marker + " up : " + north_marker_position.ToString)
        'System.Diagnostics.Debug.WriteLine(adjusted_bar_code_values + " position : " + _
        '                                   dict_operations.GetKeyFromValue(words, adjusted_bar_code_values).Y.ToString)
        'System.Diagnostics.Debug.WriteLine(south_marker + " down : " + south_marker_position.ToString)
        'System.Diagnostics.Debug.WriteLine("X :")
        'System.Diagnostics.Debug.WriteLine(west_marker + " left : " + west_marker_position.ToString)
        'System.Diagnostics.Debug.WriteLine(adjusted_bar_code_values + " position : " + _
        '                                   dict_operations.GetKeyFromValue(words, adjusted_bar_code_values).X.ToString)
        'System.Diagnostics.Debug.WriteLine(east_marker + " right : " + east_marker_position.ToString)

        'If north_marker_position = 0 OrElse south_marker_position = 0 OrElse west_marker_position = 0 _
        '        OrElse east_marker_position = 0 Then
        '    System.Diagnostics.Debug.WriteLine("Could not find references for " + adjusted_bar_code_values)
        'End If

        If String.IsNullOrWhiteSpace(adjusted_bar_code_values) Then
            For Each word As KeyValuePair(Of Point, String) In words
                If north_marker_position >= word.Key.Y + MARKER_DISTANCE_THRESHOLD AndAlso
                        south_marker_position <= word.Key.Y - MARKER_DISTANCE_THRESHOLD AndAlso
                        west_marker_position <= word.Key.X - MARKER_DISTANCE_THRESHOLD AndAlso
                        east_marker_position >= word.Key.X + MARKER_DISTANCE_THRESHOLD Then
                    Return False
                End If
            Next
            Return True
        End If

        If north_marker_position >= dict_operations.GetKeyFromValue(words, adjusted_bar_code_values).Y - MARKER_DISTANCE_THRESHOLD AndAlso
                south_marker_position <= dict_operations.GetKeyFromValue(words, adjusted_bar_code_values).Y + MARKER_DISTANCE_THRESHOLD AndAlso
                west_marker_position <= dict_operations.GetKeyFromValue(words, adjusted_bar_code_values).X + MARKER_DISTANCE_THRESHOLD AndAlso
                east_marker_position >= dict_operations.GetKeyFromValue(words, adjusted_bar_code_values).X - MARKER_DISTANCE_THRESHOLD Then
            Return True
        End If
        Return False
    End Function
End Class
