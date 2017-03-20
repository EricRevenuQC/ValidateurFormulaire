Imports System.Drawing

Public Class Releve1
    Implements IReleves

    Private north_marker As String
    Private south_marker As String
    Private east_marker As String
    Private west_marker As String
    Private north_occurence_number As Integer
    Private south_occurence_number As Integer
    Private east_occurence_number As Integer
    Private west_occurence_number As Integer

    Public Sub SetReleveMarkers(bar_code_index As Integer) Implements IReleves.SetReleveMarkers
        north_marker = ""
        south_marker = ""
        east_marker = ""
        west_marker = ""
        north_occurence_number = 1
        south_occurence_number = 1
        east_occurence_number = 1
        west_occurence_number = 1

        If bar_code_index = 3 Then
            'Année
            north_marker = "Année"
            south_marker = "A-"
            west_marker = "Année"
            east_marker = "Code du relevé"
        ElseIf bar_code_index = 4 Then
            'Code du relevé
            north_marker = "Code du relevé"
            south_marker = "A-"
            west_marker = "Code du relevé"
            east_marker = "dernier relevé"
        ElseIf bar_code_index = 5 Then
            'Dernier relevé transmis
            north_marker = "dernier relevé"
            south_marker = "A-"
            west_marker = "dernier relevé"
            east_marker = "RL-1"
        ElseIf bar_code_index = 6 Then
            north_marker = "TOP"
            south_marker = "dernier relevé"
            west_marker = "dernier relevé"
            east_marker = "RIGHT"
        ElseIf bar_code_index = 7 Then
            north_marker = "dernier relevé"
            south_marker = "A-"
            west_marker = "dernier relevé"
            east_marker = "RIGHT"
        ElseIf bar_code_index = 8 Then
            'A
            north_marker = "A-"
            south_marker = "G-"
            west_marker = "A-"
            east_marker = "B-"
        ElseIf bar_code_index = 9 Then
            'B
            north_marker = "B-"
            south_marker = "H-"
            west_marker = "B-"
            east_marker = "C-"
        ElseIf bar_code_index = 10 Then
            'C
            north_marker = "C-"
            south_marker = "I-"
            west_marker = "C-"
            east_marker = "D-"
        ElseIf bar_code_index = 11 Then
            'D
            north_marker = "D-"
            south_marker = "J-"
            west_marker = "D-"
            east_marker = "E-"
        ElseIf bar_code_index = 12 Then
            'E
            north_marker = "E-"
            south_marker = "K-"
            west_marker = "E-"
            east_marker = "F-"
        ElseIf bar_code_index = 13 Then
            'F
            north_marker = "F-"
            south_marker = "L- Autres"
            west_marker = "F-"
            east_marker = "RIGHT"
        ElseIf bar_code_index = 14 Then
            'G
            north_marker = "G-"
            south_marker = "M-"
            west_marker = "G-"
            east_marker = "H-"
        ElseIf bar_code_index = 15 Then
            'H
            north_marker = "H-"
            south_marker = "N-"
            west_marker = "H-"
            east_marker = "I-"
        ElseIf bar_code_index = 16 Then
            'I
            north_marker = "I-"
            south_marker = "O-"
            west_marker = "I-"
            east_marker = "J-"
        ElseIf bar_code_index = 17 Then
            'J
            north_marker = "J-"
            south_marker = "P-"
            west_marker = "J-"
            east_marker = "K-"
        ElseIf bar_code_index = 18 Then
            'K
            north_marker = "K-"
            south_marker = "Q-"
            west_marker = "K-"
            east_marker = "L- Autres"
        ElseIf bar_code_index = 19 Then
            'L
            north_marker = "L- Autres"
            south_marker = "R-"
            west_marker = "L- Autres"
            east_marker = "RIGHT"
        ElseIf bar_code_index = 20 Then
            'M
            north_marker = "M-"
            south_marker = "S-"
            west_marker = "M-"
            east_marker = "N-"
        ElseIf bar_code_index = 21 Then
            'N
            north_marker = "N-"
            south_marker = "T-"
            west_marker = "N-"
            east_marker = "O-"
        ElseIf bar_code_index = 22 Then
            'O
            north_marker = "O-"
            south_marker = "U-"
            west_marker = "O-"
            east_marker = "P-"
        ElseIf bar_code_index = 23 Then
            'P
            north_marker = "P-"
            south_marker = "V-"
            west_marker = "P-"
            east_marker = "Q-"
        ElseIf bar_code_index = 24 Then
            'Q
            north_marker = "Q-"
            south_marker = "W-"
            west_marker = "Q-"
            east_marker = "R-"
        ElseIf bar_code_index = 25 Then
            'R
            north_marker = "R-"
            south_marker = "Code ("
            west_marker = "R-"
            east_marker = "RIGHT"
        ElseIf bar_code_index = 26 Then
            'S
            north_marker = "S-"
            south_marker = "Renseignements"
            west_marker = "S-"
            east_marker = "T-"
        ElseIf bar_code_index = 28 Then
            'T
            north_marker = "T-"
            south_marker = "Renseignements"
            west_marker = "T-"
            east_marker = "U-"
        ElseIf bar_code_index = 29 Then
            'U
            north_marker = "U-"
            south_marker = "Renseignements"
            west_marker = "U-"
            east_marker = "V-"
        ElseIf bar_code_index = 30 Then
            'V
            north_marker = "V-"
            south_marker = "Renseignements"
            west_marker = "V-"
            east_marker = "W-"
        ElseIf bar_code_index = 31 Then
            'W
            north_marker = "W-"
            south_marker = "Renseignements"
            west_marker = "W-"
            east_marker = "Code ("
        ElseIf bar_code_index = 32 Then
            'Code (case O)
            north_marker = "Code ("
            south_marker = "Renseignements"
            west_marker = "Code ("
            east_marker = "RIGHT"
        ElseIf bar_code_index = 33 Then
            'NAS
            north_marker = "assurance sociale"
            south_marker = "Nom et adresse"
            west_marker = "assurance sociale"
            east_marker = "Numéro de référence"
        ElseIf bar_code_index = 34 Then
            'Numéro de référence
            north_marker = "Numéro de référence"
            south_marker = "Nom et adresse"
            west_marker = "Numéro de référence"
            east_marker = "RIGHT"
        ElseIf bar_code_index = 35 Then
            'Nom de famille
            north_marker = "Nom de famille"
            south_marker = "Prénom"
            west_marker = "Nom de famille"
            east_marker = "assurance sociale"
        ElseIf bar_code_index = 36 Then
            'Prénom
            north_marker = "Prénom"
            south_marker = "Numéro, rue,"
            west_marker = "Prénom"
            east_marker = "Nom et adresse"
        ElseIf bar_code_index = 37 Then
            'Numéro, rue, appartement, case postale
            north_marker = "Numéro, rue,"
            south_marker = "Numéro, rue,"
            west_marker = "Numéro, rue,"
            east_marker = "Numéro, rue,"
            south_occurence_number = 2
            east_occurence_number = 2
        ElseIf bar_code_index = 38 Then
            'Numéro, rue, appartement, case postale #2
            north_marker = "Numéro, rue,"
            south_marker = "Ville,"
            west_marker = "Numéro, rue,"
            east_marker = "Numéro, rue,"
            north_occurence_number = 2
            east_occurence_number = 2
        ElseIf bar_code_index = 39 Then
            'Ville, village ou municipalité
            north_marker = "Ville,"
            south_marker = "Province"
            west_marker = "Ville,"
            east_marker = "Ville,"
            east_occurence_number = 2
        ElseIf bar_code_index = 40 Then
            'Province
            north_marker = "Province"
            south_marker = "Province"
            west_marker = "Province"
            east_marker = "Code postal"
            south_occurence_number = 2
        ElseIf bar_code_index = 41 Then
            'Code postal
            north_marker = "Code postal"
            south_marker = "Code postal"
            west_marker = "Code postal"
            east_marker = "Province"
            south_occurence_number = 2
            east_occurence_number = 2
        ElseIf bar_code_index = 42 Then
            'Nom et adresse de l'employeur
            north_marker = "Nom et adresse"
            south_marker = "Numéro, rue,"
            west_marker = "Nom et adresse"
            east_marker = "RIGHT"
        ElseIf bar_code_index = 43 Then
            'Nom et adresse de l'employeur #2
            north_marker = "Numéro, rue,"
            south_marker = "Numéro, rue,"
            west_marker = "Nom et adresse"
            east_marker = "RIGHT"
            south_occurence_number = 2
        ElseIf bar_code_index = 44 Then
            'Numéro, rue, appartement, case postale employeur
            north_marker = "Numéro, rue,"
            south_marker = "Ville,"
            west_marker = "Numéro, rue,"
            east_marker = "RIGHT"
            north_occurence_number = 2
            west_occurence_number = 2
        ElseIf bar_code_index = 45 Then
            'Numéro, rue, appartement, case postale employeur #2
            north_marker = "Ville,"
            south_marker = "Ville,"
            west_marker = "Numéro, rue,"
            east_marker = "RIGHT"
            south_occurence_number = 2
            west_occurence_number = 2
        ElseIf bar_code_index = 46 Then
            'Ville, village ou municipalité employeur
            north_marker = "Ville,"
            south_marker = "Province"
            west_marker = "Ville,"
            east_marker = "RIGHT"
            north_occurence_number = 2
            south_occurence_number = 2
            west_occurence_number = 2
        ElseIf bar_code_index = 47 Then
            'Province employeur
            north_marker = "Province"
            south_marker = "Guide du relevé"
            west_marker = "Province"
            east_marker = "Code postal"
            north_occurence_number = 2
            west_occurence_number = 2
            east_occurence_number = 2
        ElseIf bar_code_index = 48 Then
            'Code postal employeur
            north_marker = "Code postal"
            south_marker = "Guide du relevé"
            west_marker = "Code postal"
            east_marker = "RIGHT"
            north_occurence_number = 2
            west_occurence_number = 2
        ElseIf bar_code_index = 49 Then
            'Renseignements complémentaires
            north_marker = "Renseignements"
            south_marker = "Nom de famille"
            west_marker = "A-"
            east_marker = "B-"
        ElseIf bar_code_index = 50 Then
            'Renseignements complémentaires
            north_marker = "Renseignements"
            south_marker = "Nom de famille"
            west_marker = "B-"
            east_marker = "C-"
        ElseIf bar_code_index = 51 Then
            'Renseignements complémentaires
            north_marker = "Renseignements"
            south_marker = "Nom de famille"
            west_marker = "B-"
            east_marker = "D-"
        ElseIf bar_code_index = 52 Then
            'Renseignements complémentaires
            north_marker = "Renseignements"
            south_marker = "Nom de famille"
            west_marker = "C-"
            east_marker = "D-"
        ElseIf bar_code_index = 53 Then
            'Renseignements complémentaires
            north_marker = "Renseignements"
            south_marker = "Nom de famille"
            west_marker = "D-"
            east_marker = "E-"
        ElseIf bar_code_index = 54 Then
            'Renseignements complémentaires
            north_marker = "Renseignements"
            south_marker = "Nom de famille"
            west_marker = "D-"
            east_marker = "F-"
        ElseIf bar_code_index = 55 Then
            'Renseignements complémentaires
            north_marker = "Renseignements"
            south_marker = "Nom de famille"
            west_marker = "E-"
            east_marker = "F-"
        ElseIf bar_code_index = 56 Then
            'Renseignements complémentaires
            north_marker = "Renseignements"
            south_marker = "Nom de famille"
            west_marker = "F-"
            east_marker = "RIGHT"
        End If
    End Sub

    Public Function GetNorthMarker() Implements IReleves.GetNorthMarker
        Return north_marker
    End Function

    Public Function GetSouthMarker() Implements IReleves.GetSouthMarker
        Return south_marker
    End Function

    Public Function GetWestMarker() Implements IReleves.GetWestMarker
        Return west_marker
    End Function

    Public Function GetEastMarker() Implements IReleves.GetEastMarker
        Return east_marker
    End Function

    Public Function GetNorthMarkerOccurence() Implements IReleves.GetNorthMarkerOccurence
        Return north_occurence_number
    End Function

    Public Function GetSouthMarkerOccurence() Implements IReleves.GetSouthMarkerOccurence
        Return south_occurence_number
    End Function

    Public Function GetWestMarkerOccurence() Implements IReleves.GetWestMarkerOccurence
        Return west_occurence_number
    End Function

    Public Function GetEastMarkerOccurence() Implements IReleves.GetEastMarkerOccurence
        Return east_occurence_number
    End Function
End Class
