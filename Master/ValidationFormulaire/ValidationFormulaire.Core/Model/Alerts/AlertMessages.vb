Public Class AlertMessages
    Private Const chars_not_found_message As String = "Au moins un champ du code à bar n'a pas été trouvé!"
    Private Const zeros_found_message As String = "Des zéros ont été trouvés au lieu de cases vides!"
    Private Const format_image_message As String = "Impossible de lire le PDF car il est de format image!"
    Private Const anchor_top_right_message As String = "L'ancrage droit-haut n'a pas été trouvé!"
    Private Const anchor_bot_left_message As String = "L'ancrage gauche-bas n'a pas été trouvé!"
    Private Const element_out_of_zone_message As String = "Il y a un élément en dehors de la zone définie par les points d'ancrage!"
    Private Const elements_out_of_zone_message As String = "Il y a plusieurs éléments en dehors de la zone définie par les points d'ancrage!"
    Private Const blank_page As String = "La première page du PDF est vide!"

    Public Function GetCharNotFoundMsg() As String
        Return chars_not_found_message
    End Function

    Public Function GetZerosFoundMsg() As String
        Return zeros_found_message
    End Function

    Public Function GetImageFormatMsg() As String
        Return format_image_message
    End Function

    Public Function GetBotLeftAnchorMsg() As String
        Return anchor_bot_left_message
    End Function

    Public Function GetTopRightAnchorMsg() As String
        Return anchor_top_right_message
    End Function

    Public Function GetElementOutOfZoneMsg() As String
        Return element_out_of_zone_message
    End Function

    Public Function GetElementsOutOfZoneMsg() As String
        Return elements_out_of_zone_message
    End Function

    Public Function GetBlankPageMsg() As String
        Return blank_page
    End Function
End Class
