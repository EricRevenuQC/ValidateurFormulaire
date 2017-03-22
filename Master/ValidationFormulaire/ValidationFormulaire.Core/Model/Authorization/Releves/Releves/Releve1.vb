Imports System.Drawing

Public Class Releve1
    Implements IReleves

    Private north_marker As List(Of String)
    Private south_marker As List(Of String)
    Private east_marker As List(Of String)
    Private west_marker As List(Of String)
    Private north_occurence_number As List(Of Integer)
    Private south_occurence_number As List(Of Integer)
    Private east_occurence_number As List(Of Integer)
    Private west_occurence_number As List(Of Integer)

    Public Sub SetReleveMarkers(bar_code_index As Integer) Implements IReleves.SetReleveMarkers
        north_marker = New List(Of String)()
        south_marker = New List(Of String)()
        east_marker = New List(Of String)()
        west_marker = New List(Of String)()
        north_occurence_number = New List(Of Integer)()
        south_occurence_number = New List(Of Integer)()
        east_occurence_number = New List(Of Integer)()
        west_occurence_number = New List(Of Integer)()

        north_occurence_number.Add(1)
        south_occurence_number.Add(1)
        west_occurence_number.Add(1)
        east_occurence_number.Add(1)

        If bar_code_index = 0 Then
            'Code d'identification
            north_marker.Add("Province")
            south_marker.Add("MIN")
            west_marker.Add("MIN")
            east_marker.Add("MAX")
            north_occurence_number(0) = 2
        ElseIf bar_code_index = 1 Then
            'Numéro de validation
            north_marker.Add("Province")
            south_marker.Add("MIN")
            west_marker.Add("MIN")
            east_marker.Add("MAX")
            north_occurence_number(0) = 2
        ElseIf bar_code_index = 3 Then
            'Année
            north_marker.Add("Année")
            south_marker.Add("A-")
            west_marker.Add("Année")
            east_marker.Add("Code du relevé")
        ElseIf bar_code_index = 4 Then
            'Code du relevé
            north_marker.Add("Code du relevé")
            south_marker.Add("A-")
            west_marker.Add("Code du relevé")
            east_marker.AddRange({"Impôt du Québec retenu", "E-", "E -"})
        ElseIf bar_code_index = 5 Then
            'Dernier relevé transmis
            north_marker.Add("dernier relevé")
            south_marker.Add("A-")
            west_marker.AddRange({"Impôt du Québec retenu", "E-", "E -"})
            east_marker.AddRange({"Cotisation syndicale", "F-", "F -"})
        ElseIf bar_code_index = 6 Then
            north_marker.Add("MAX")
            south_marker.Add("dernier relevé")
            west_marker.AddRange({"Cotisation syndicale", "F-", "F -"})
            east_marker.Add("MAX")
        ElseIf bar_code_index = 7 Then
            north_marker.Add("dernier relevé")
            south_marker.Add("A-")
            west_marker.AddRange({"Cotisation syndicale", "F-", "F -"})
            east_marker.Add("MAX")
        ElseIf bar_code_index = 8 Then
            'A
            north_marker.AddRange({"Revenus d'emploi", "A-", "A -"})
            south_marker.AddRange({"Salaire admissible au RRQ", "G-", "G -"})
            west_marker.AddRange({"Revenus d'emploi", "A-", "A -"})
            east_marker.AddRange({"Cotisation au RRQ", "B-", "B -"})
        ElseIf bar_code_index = 9 Then
            'B
            north_marker.AddRange({"Cotisation au RRQ", "B-", "B -"})
            south_marker.AddRange({"Cotisation au RQAP", "H-", "H -"})
            west_marker.AddRange({"Cotisation au RRQ", "B-", "B -"})
            east_marker.AddRange({"Cotisation à l'assurance", "C-", "C -"})
        ElseIf bar_code_index = 10 Then
            'C
            north_marker.AddRange({"Cotisation à l'assurance", "C-", "C -"})
            south_marker.AddRange({"Salaire admissible au RQAP", "I-", "I -"})
            west_marker.AddRange({"Cotisation à l'assurance", "C-", "C -"})
            east_marker.AddRange({"Cotisation à un RPA", "D-", "D -"})
        ElseIf bar_code_index = 11 Then
            'D
            north_marker.AddRange({"Cotisation à un RPA", "D-", "D -"})
            south_marker.AddRange({"Régime privé d'ass", "J-", "J -"})
            west_marker.AddRange({"Cotisation à un RPA", "D-", "D -"})
            east_marker.AddRange({"Impôt du Québec retenu", "E-", "E -"})
        ElseIf bar_code_index = 12 Then
            'E
            north_marker.AddRange({"Impôt du Québec retenu", "E-", "E -"})
            south_marker.AddRange({"Voyages (région éloignée)", "K-", "K -"})
            west_marker.AddRange({"Impôt du Québec retenu", "E-", "E -"})
            east_marker.AddRange({"Cotisation syndicale", "F-", "F -"})
        ElseIf bar_code_index = 13 Then
            'F
            north_marker.AddRange({"Cotisation syndicale", "F-", "F -"})
            south_marker.AddRange({"Autres avantages", "L-", "L -"})
            west_marker.AddRange({"Cotisation syndicale", "F-", "F -"})
            east_marker.Add("MAX")
        ElseIf bar_code_index = 14 Then
            'G
            north_marker.AddRange({"Salaire admissible au RRQ", "G-", "G -"})
            south_marker.AddRange({"Commissions", "M-", "M -"})
            west_marker.AddRange({"Salaire admissible au RRQ", "G-", "G -"})
            east_marker.AddRange({"Cotisation au RQAP", "H-", "H -"})
        ElseIf bar_code_index = 15 Then
            'H
            north_marker.AddRange({"Cotisation au RQAP", "H-", "H -"})
            south_marker.AddRange({"Dons de bienfaisance", "N-", "N -"})
            west_marker.AddRange({"Cotisation au RQAP", "H-", "H -"})
            east_marker.AddRange({"Salaire admissible au RQAP", "I-", "I -"})
        ElseIf bar_code_index = 16 Then
            'I
            north_marker.AddRange({"Salaire admissible au RQAP", "I-", "I -"})
            south_marker.AddRange({"Autres revenus", "O-", "O -"})
            west_marker.AddRange({"Salaire admissible au RQAP", "I-", "I -"})
            east_marker.AddRange({"Régime privé d'ass", "J-", "J -"})
        ElseIf bar_code_index = 17 Then
            'J
            north_marker.AddRange({"Régime privé d'ass", "J-", "J -"})
            south_marker.AddRange({"Régime d'ass", "P-", "P -"})
            west_marker.AddRange({"Régime privé d'ass", "J-", "J -"})
            east_marker.AddRange({"Voyages (région éloignée)", "K-", "K -"})
        ElseIf bar_code_index = 18 Then
            'K
            north_marker.AddRange({"Voyages (région éloignée)", "K-", "K -"})
            south_marker.AddRange({"Salaires différés", "Q-", "Q -"})
            west_marker.AddRange({"Voyages (région éloignée)", "K-", "K -"})
            east_marker.AddRange({"Autres avantages", "L-", "L -"})
        ElseIf bar_code_index = 19 Then
            'L
            north_marker.AddRange({"Autres avantages", "L-", "L -"})
            south_marker.AddRange({"Revenu « situé » dans", "R-", "R -"})
            west_marker.AddRange({"Autres avantages", "L-", "L -"})
            east_marker.Add("MAX")
        ElseIf bar_code_index = 20 Then
            'M
            north_marker.AddRange({"Commissions", "M-", "M -"})
            south_marker.AddRange({"Pourboires reçus", "S-", "S -"})
            west_marker.AddRange({"Commissions", "M-", "M -"})
            east_marker.AddRange({"Dons de bienfaisance", "N-", "N -"})
        ElseIf bar_code_index = 21 Then
            'N
            north_marker.AddRange({"Dons de bienfaisance", "N-", "N -"})
            south_marker.AddRange({"Pourboires attribués", "T-", "T -"})
            west_marker.AddRange({"Dons de bienfaisance", "N-", "N -"})
            east_marker.AddRange({"Autres revenus", "O-", "O -"})
        ElseIf bar_code_index = 22 Then
            'O
            north_marker.AddRange({"Autres revenus", "O-", "O -"})
            south_marker.AddRange({"Retraite progressive", "U-", "U -"})
            west_marker.AddRange({"Autres revenus", "O-", "O -"})
            east_marker.AddRange({"Régime d'ass", "P-", "P -"})
        ElseIf bar_code_index = 23 Then
            'P
            north_marker.AddRange({"Régime d'ass", "P-", "P -"})
            south_marker.AddRange({"Nourriture et logement", "V-", "V -"})
            west_marker.AddRange({"Régime d'ass", "P-", "P -"})
            east_marker.AddRange({"Salaires différés", "Q-", "Q -"})
        ElseIf bar_code_index = 24 Then
            'Q
            north_marker.AddRange({"Salaires différés", "Q-", "Q -"})
            south_marker.AddRange({"Véhicule à moteur", "W-", "W -"})
            west_marker.AddRange({"Salaires différés", "Q-", "Q -"})
            east_marker.AddRange({"Revenu « situé » dans", "R-", "R -"})
        ElseIf bar_code_index = 25 Then
            'R
            north_marker.AddRange({"Revenu « situé » dans", "R-", "R -"})
            south_marker.AddRange({"Code (", "case O"})
            west_marker.AddRange({"Revenu « situé » dans", "R-", "R -"})
            east_marker.Add("MAX")
        ElseIf bar_code_index = 26 Then
            'S
            north_marker.AddRange({"Pourboires reçus", "S-", "S -"})
            south_marker.AddRange({"Renseignements", "complémentaires"})
            west_marker.AddRange({"Pourboires reçus", "S-", "S -"})
            east_marker.AddRange({"Pourboires attribués", "T-", "T -"})
        ElseIf bar_code_index = 28 Then
            'T
            north_marker.AddRange({"Pourboires attribués", "T-", "T -"})
            south_marker.AddRange({"Renseignements", "complémentaires"})
            west_marker.AddRange({"Pourboires attribués", "T-", "T -"})
            east_marker.AddRange({"Retraite progressive", "U-", "U -"})
        ElseIf bar_code_index = 29 Then
            'U
            north_marker.AddRange({"Retraite progressive", "U-", "U -"})
            south_marker.AddRange({"Renseignements", "complémentaires"})
            west_marker.AddRange({"Retraite progressive", "U-", "U -"})
            east_marker.AddRange({"Nourriture et logement", "V-", "V -"})
        ElseIf bar_code_index = 30 Then
            'V
            north_marker.AddRange({"Nourriture et logement", "V-", "V -"})
            south_marker.AddRange({"Renseignements", "complémentaires"})
            west_marker.AddRange({"Nourriture et logement", "V-", "V -"})
            east_marker.AddRange({"Véhicule à moteur", "W-", "W -"})
        ElseIf bar_code_index = 31 Then
            'W
            north_marker.AddRange({"Véhicule à moteur", "W-", "W -"})
            south_marker.AddRange({"Renseignements", "complémentaires"})
            west_marker.AddRange({"Véhicule à moteur", "W-", "W -"})
            east_marker.AddRange({"Code (", "case O"})
        ElseIf bar_code_index = 32 Then
            'Code (case O)
            north_marker.AddRange({"Code (", "case O"})
            south_marker.AddRange({"Renseignements", "complémentaires"})
            west_marker.AddRange({"Code (", "case O"})
            east_marker.Add("MAX")
        ElseIf bar_code_index = 33 Then
            'NAS
            north_marker.Add("assurance sociale")
            south_marker.AddRange({"Nom et adresse", "employeur ou du payeur"})
            west_marker.Add("assurance sociale")
            east_marker.AddRange({"Numéro de référence", "(facultatif)"})
        ElseIf bar_code_index = 34 Then
            'Numéro de référence
            north_marker.AddRange({"Numéro de référence", "(facultatif)"})
            south_marker.AddRange({"Nom et adresse", "employeur ou du payeur"})
            west_marker.AddRange({"Numéro de référence", "(facultatif)"})
            east_marker.Add("MAX")
        ElseIf bar_code_index = 35 Then
            'Nom de famille
            north_marker.Add("Nom de famille")
            south_marker.Add("Prénom")
            west_marker.Add("Nom de famille")
            east_marker.Add("assurance sociale")
        ElseIf bar_code_index = 36 Then
            'Prénom
            north_marker.Add("Prénom")
            south_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            west_marker.Add("Prénom")
            east_marker.AddRange({"Nom et adresse", "employeur ou du payeur"})
        ElseIf bar_code_index = 37 Then
            'Numéro, rue, appartement, case postale
            north_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            south_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            west_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            east_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            south_occurence_number(0) = 2
            east_occurence_number(0) = 2
        ElseIf bar_code_index = 38 Then
            'Numéro, rue, appartement, case postale #2
            north_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            south_marker.AddRange({"Ville,", "village ou municipalité"})
            west_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            east_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            north_occurence_number(0) = 2
            east_occurence_number(0) = 2
        ElseIf bar_code_index = 39 Then
            'Ville, village ou municipalité
            north_marker.AddRange({"Ville,", "village ou municipalité"})
            south_marker.Add("Province")
            west_marker.AddRange({"Ville,", "village ou municipalité"})
            east_marker.AddRange({"Ville,", "village ou municipalité"})
            east_occurence_number(0) = 2
        ElseIf bar_code_index = 40 Then
            'Province
            north_marker.Add("Province")
            south_marker.Add("Province")
            west_marker.Add("Province")
            east_marker.Add("Code postal")
            south_occurence_number(0) = 2
        ElseIf bar_code_index = 41 Then
            'Code postal
            north_marker.Add("Code postal")
            south_marker.Add("Code postal")
            west_marker.Add("Code postal")
            east_marker.Add("Province")
            south_occurence_number(0) = 2
            east_occurence_number(0) = 2
        ElseIf bar_code_index = 42 Then
            'Nom et adresse de l'employeur
            north_marker.AddRange({"Nom et adresse", "employeur ou du payeur"})
            south_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            west_marker.AddRange({"Nom et adresse", "employeur ou du payeur"})
            east_marker.Add("MAX")
        ElseIf bar_code_index = 43 Then
            'Nom et adresse de l'employeur #2
            north_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            south_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            west_marker.AddRange({"Nom et adresse", "employeur ou du payeur"})
            east_marker.Add("MAX")
            south_occurence_number(0) = 2
        ElseIf bar_code_index = 44 Then
            'Numéro, rue, appartement, case postale employeur
            north_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            south_marker.AddRange({"Ville,", "village ou municipalité"})
            west_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            east_marker.Add("MAX")
            north_occurence_number(0) = 2
            west_occurence_number(0) = 2
        ElseIf bar_code_index = 45 Then
            'Numéro, rue, appartement, case postale employeur #2
            north_marker.AddRange({"Ville,", "village ou municipalité"})
            south_marker.AddRange({"Ville,", "village ou municipalité"})
            west_marker.AddRange({"Numéro, rue,", "appartement,", "case postale"})
            east_marker.Add("MAX")
            south_occurence_number(0) = 2
            west_occurence_number(0) = 2
        ElseIf bar_code_index = 46 Then
            'Ville, village ou municipalité employeur
            north_marker.AddRange({"Ville,", "village ou municipalité"})
            south_marker.Add("Province")
            west_marker.AddRange({"Ville,", "village ou municipalité"})
            east_marker.Add("MAX")
            north_occurence_number(0) = 2
            south_occurence_number(0) = 2
            west_occurence_number(0) = 2
        ElseIf bar_code_index = 47 Then
            'Province employeur
            north_marker.Add("Province")
            south_marker.Add("MIN")
            west_marker.Add("Province")
            east_marker.Add("Code postal")
            north_occurence_number(0) = 2
            west_occurence_number(0) = 2
            east_occurence_number(0) = 2
        ElseIf bar_code_index = 48 Then
            'Code postal employeur
            north_marker.Add("Code postal")
            south_marker.Add("MIN")
            west_marker.Add("Code postal")
            east_marker.Add("MAX")
            north_occurence_number(0) = 2
            west_occurence_number(0) = 2
        ElseIf bar_code_index = 49 Then
            'Renseignements complémentaires
            north_marker.AddRange({"Renseignements", "complémentaires"})
            south_marker.Add("Nom de famille")
            west_marker.Add("A-")
            east_marker.Add("B-")
        ElseIf bar_code_index = 50 Then
            'Renseignements complémentaires
            north_marker.AddRange({"Renseignements", "complémentaires"})
            south_marker.Add("Nom de famille")
            west_marker.Add("B-")
            east_marker.AddRange({"Cotisation à l'assurance", "C-", "C -"})
        ElseIf bar_code_index = 51 Then
            'Renseignements complémentaires
            north_marker.AddRange({"Renseignements", "complémentaires"})
            south_marker.Add("Nom de famille")
            west_marker.Add("B-")
            east_marker.AddRange({"Cotisation à un RPA", "D-", "D -"})
        ElseIf bar_code_index = 52 Then
            'Renseignements complémentaires
            north_marker.AddRange({"Renseignements", "complémentaires"})
            south_marker.Add("Nom de famille")
            west_marker.AddRange({"Cotisation à l'assurance", "C-", "C -"})
            east_marker.AddRange({"Cotisation à un RPA", "D-", "D -"})
        ElseIf bar_code_index = 53 Then
            'Renseignements complémentaires
            north_marker.AddRange({"Renseignements", "complémentaires"})
            south_marker.Add("Nom de famille")
            west_marker.AddRange({"Cotisation à un RPA", "D-", "D -"})
            east_marker.AddRange({"Impôt du Québec retenu", "E-", "E -"})
        ElseIf bar_code_index = 54 Then
            'Renseignements complémentaires
            north_marker.AddRange({"Renseignements", "complémentaires"})
            south_marker.Add("Nom de famille")
            west_marker.AddRange({"Cotisation à un RPA", "D-", "D -"})
            east_marker.AddRange({"Cotisation syndicale", "F-", "F -"})
        ElseIf bar_code_index = 55 Then
            'Renseignements complémentaires
            north_marker.AddRange({"Renseignements", "complémentaires"})
            south_marker.Add("Nom de famille")
            west_marker.AddRange({"Impôt du Québec retenu", "E-", "E -"})
            east_marker.AddRange({"Cotisation syndicale", "F-", "F -"})
        ElseIf bar_code_index = 56 Then
            'Renseignements complémentaires
            north_marker.AddRange({"Renseignements", "complémentaires"})
            south_marker.Add("Nom de famille")
            west_marker.AddRange({"Cotisation syndicale", "F-", "F -"})
            east_marker.Add("MAX")
        End If
    End Sub

    Public Function GetNorthMarker() As List(Of String) Implements IReleves.GetNorthMarker
        Return north_marker
    End Function

    Public Function GetSouthMarker() As List(Of String) Implements IReleves.GetSouthMarker
        Return south_marker
    End Function

    Public Function GetWestMarker() As List(Of String) Implements IReleves.GetWestMarker
        Return west_marker
    End Function

    Public Function GetEastMarker() As List(Of String) Implements IReleves.GetEastMarker
        Return east_marker
    End Function

    Public Function GetNorthMarkerOccurence() As List(Of Integer) Implements IReleves.GetNorthMarkerOccurence
        Return north_occurence_number
    End Function

    Public Function GetSouthMarkerOccurence() As List(Of Integer) Implements IReleves.GetSouthMarkerOccurence
        Return south_occurence_number
    End Function

    Public Function GetWestMarkerOccurence() As List(Of Integer) Implements IReleves.GetWestMarkerOccurence
        Return west_occurence_number
    End Function

    Public Function GetEastMarkerOccurence() As List(Of Integer) Implements IReleves.GetEastMarkerOccurence
        Return east_occurence_number
    End Function
End Class
