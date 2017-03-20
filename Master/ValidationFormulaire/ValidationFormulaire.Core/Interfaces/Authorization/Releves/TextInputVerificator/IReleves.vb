Public Interface IReleves
    Sub SetReleveMarkers(bar_code_index As Integer)
    Function GetNorthMarker()
    Function GetSouthMarker()
    Function GetWestMarker()
    Function GetEastMarker()
    Function GetNorthMarkerOccurence()
    Function GetSouthMarkerOccurence()
    Function GetWestMarkerOccurence()
    Function GetEastMarkerOccurence()
End Interface
