Imports System.Drawing

Public Class CombineCloseText
    Implements ICombineText

    Private Const MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_X As Integer = 10
    Private Const MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_Y As Integer = 5

    Public Function CombineText(text_groups As Object, Optional bold As Boolean = False,
                                Optional italic As Boolean = False) As Object Implements ICombineText.CombineText
        Dim word As String = ""
        Dim current_x_left As Integer = 0
        Dim current_x_right As Integer = 0
        Dim current_y As Integer = 0
        Dim word_list As New Dictionary(Of Point, String)()

        For Each row As Object In text_groups
            'Nouvelle ligne
            If Math.Abs(row.Key.Y - current_y) > MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_Y Then
                If word.Length > 0 Then
                    If Not word_list.ContainsKey(New Point(current_x_left, current_y)) Then
                        word_list.Add(New Point(current_x_left, current_y), word)
                    Else
                        word_list(New Point(current_x_left, current_y)) = word
                    End If
                End If
                current_y = row.Key.Y
                current_x_right = row.Value.right_side
                current_x_left = row.Key.X
                word = row.Value.text
            Else
                'Nouvelle phrase
                If Math.Abs(row.Key.X - current_x_right) > MAX_DISTANCE_BETWEEN_CHARACTERS_FOR_X Then
                    If Not word_list.ContainsKey(New Point(current_x_left, current_y)) Then
                        word_list.Add(New Point(current_x_left, current_y), word)
                    Else
                        word_list(New Point(current_x_left, current_y)) = word
                    End If
                    current_x_right = row.Value.right_side
                    current_x_left = row.Key.X
                    word = row.Value.text
                Else
                    'Même phrase/mot
                    current_x_right = row.Value.right_side
                    word += row.Value.text
                End If
            End If
        Next
        If Not word_list.ContainsKey(New Point(current_x_left, current_y)) Then
            word_list.Add(New Point(current_x_left, current_y), word)
        Else
            word_list(New Point(current_x_left, current_y)) = word
        End If

        Return word_list
    End Function
End Class
