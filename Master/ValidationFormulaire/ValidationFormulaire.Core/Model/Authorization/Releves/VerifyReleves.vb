Imports System.Drawing

Public Class VerifyReleves
    Private adjusted_bar_code_values As String
    Private dict_operations As DictionaryOperations
    Private releve As IReleves
    Private north_marker_position As Integer = 0
    Private south_marker_position As Integer = 0
    Private east_marker_position As Integer = 0
    Private west_marker_position As Integer = 0

    Private marker_reference As List(Of String)

    Private Const MAX_MARKER_VALUE As Integer = 1000
    Private Const MIN_MARKER_VALUE As Integer = 0
    Private Const MARKER_DISTANCE_THRESHOLD As Integer = 5

    Public Sub New(releves As IReleves)
        dict_operations = New DictionaryOperations()
        releve = releves
    End Sub

    Public Function VerifyTextInputPosition(words As Dictionary(Of Point, String),
                                       bar_code_data As KeyValuePair(Of String, BarCodeData),
                                       bar_code_index As Integer) As Boolean
        adjusted_bar_code_values = bar_code_data.Value.value.Trim()
        releve.SetReleveMarkers(bar_code_index)
        marker_reference = New List(Of String)()

        north_marker_position = FindMarkerPosition(words, releve.GetNorthMarker, releve.GetNorthMarkerOccurence).Y
        south_marker_position = FindMarkerPosition(words, releve.GetSouthMarker, releve.GetSouthMarkerOccurence).Y
        west_marker_position = FindMarkerPosition(words, releve.GetWestMarker, releve.GetWestMarkerOccurence).X
        east_marker_position = FindMarkerPosition(words, releve.GetEastMarker, releve.GetEastMarkerOccurence).X

        If marker_reference.Count > 0 Then
            System.Diagnostics.Debug.WriteLine("Could not find references for " + String.Join(" | ", marker_reference))
            Return False
        End If

        'If north_marker_position = 0 Then
        '    System.Diagnostics.Debug.WriteLine("Could not find north reference for " + adjusted_bar_code_values)
        '    Return False
        'ElseIf south_marker_position = 0 Then
        '    System.Diagnostics.Debug.WriteLine("Could not find south reference for " + adjusted_bar_code_values)
        '    Return False
        'ElseIf west_marker_position = 0 Then
        '    System.Diagnostics.Debug.WriteLine("Could not find west reference for " + adjusted_bar_code_values)
        '    Return False
        'ElseIf east_marker_position = 0 Then
        '    System.Diagnostics.Debug.WriteLine("Could not find east reference for " + adjusted_bar_code_values)
        '    Return False
        'End If

        System.Diagnostics.Debug.WriteLine("------------------------------------------------------")
        System.Diagnostics.Debug.WriteLine(bar_code_index.ToString + " : " + adjusted_bar_code_values)
        System.Diagnostics.Debug.WriteLine("Y : " + north_marker_position.ToString + " > " + _
                                           dict_operations.GetKeyFromValue(words, adjusted_bar_code_values).Y.ToString + _
                                           " > " + south_marker_position.ToString)
        System.Diagnostics.Debug.WriteLine("X : " + east_marker_position.ToString + " > " + _
                                           dict_operations.GetKeyFromValue(words, adjusted_bar_code_values).X.ToString + _
                                           " > " + west_marker_position.ToString)


        If String.IsNullOrWhiteSpace(adjusted_bar_code_values) Then
            For Each word As KeyValuePair(Of Point, String) In words
                If north_marker_position >= word.Key.Y + MARKER_DISTANCE_THRESHOLD AndAlso
                        south_marker_position <= word.Key.Y - MARKER_DISTANCE_THRESHOLD AndAlso
                        west_marker_position <= word.Key.X - MARKER_DISTANCE_THRESHOLD AndAlso
                        east_marker_position >= word.Key.X + MARKER_DISTANCE_THRESHOLD Then
                    System.Diagnostics.Debug.WriteLine("Fail, should be empty.")
                    Return False
                End If
            Next
            System.Diagnostics.Debug.WriteLine("Pass, is empty.")
            Return True
        End If

        If north_marker_position >= dict_operations.GetKeyFromValue(words, adjusted_bar_code_values).Y - MARKER_DISTANCE_THRESHOLD AndAlso
                south_marker_position <= dict_operations.GetKeyFromValue(words, adjusted_bar_code_values).Y + MARKER_DISTANCE_THRESHOLD AndAlso
                west_marker_position <= dict_operations.GetKeyFromValue(words, adjusted_bar_code_values).X + MARKER_DISTANCE_THRESHOLD AndAlso
                east_marker_position >= dict_operations.GetKeyFromValue(words, adjusted_bar_code_values).X - MARKER_DISTANCE_THRESHOLD Then
            System.Diagnostics.Debug.WriteLine("Pass")
            Return True
        End If
        System.Diagnostics.Debug.WriteLine("Fail")
        Return False
    End Function

    Private Function FindMarkerPosition(words As Dictionary(Of Point, String), markers As List(Of String),
                                   marker_occurence As List(Of Integer)) As Point
        Dim marker_position As Point?
        Dim current_marker_reference = New List(Of String)()

        For Each marker As String In markers
            If marker = "MAX" Then
                Return New Point(MAX_MARKER_VALUE, MAX_MARKER_VALUE)
            ElseIf marker = "MIN" Then
                Return New Point(MIN_MARKER_VALUE, MIN_MARKER_VALUE)
            Else
                marker_position = dict_operations.GetKeyFromValueContains(words, marker, marker_occurence(marker.IndexOf(marker)))
                If marker_position IsNot Nothing Then
                    Return marker_position
                Else
                    current_marker_reference.Add(marker)
                End If
            End If
        Next

        marker_reference.AddRange(current_marker_reference)
        Return New Point(0, 0)
    End Function
End Class
