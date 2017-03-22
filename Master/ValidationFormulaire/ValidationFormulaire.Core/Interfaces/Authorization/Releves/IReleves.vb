Public Interface IReleves
    Sub SetReleveMarkers(bar_code_index As Integer)
    Function GetNorthMarker() As List(Of String)
    Function GetSouthMarker() As List(Of String)
    Function GetWestMarker() As List(Of String)
    Function GetEastMarker() As List(Of String)
    Function GetNorthMarkerOccurence() As List(Of Integer)
    Function GetSouthMarkerOccurence() As List(Of Integer)
    Function GetWestMarkerOccurence() As List(Of Integer)
    Function GetEastMarkerOccurence() As List(Of Integer)
End Interface
