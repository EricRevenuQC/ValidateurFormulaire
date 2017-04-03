Imports System.Drawing
Imports System.IO

Public Class FormulaireImage

    Private session_value_provider As New SessionValueProvider()

    Public Function GetImageData(Optional formulaire As FormulairePosition = Nothing,
                             Optional current_page As Integer = 1) As Byte()
        Dim file_path As String = ""
        Dim imageByteData As Byte()
        Dim images() As Image = Nothing
        Dim ms As New MemoryStream()

        If formulaire.Equals(FormulairePosition.center) Then
            images = session_value_provider.GetValue("images")
        ElseIf formulaire.Equals(FormulairePosition.left) Then
            images = session_value_provider.GetValue("images_left")
        ElseIf formulaire.Equals(FormulairePosition.right) Then
            images = session_value_provider.GetValue("images_right")
        ElseIf formulaire.Equals(FormulairePosition.donnees) Then
            images = session_value_provider.GetValue("images_donnees")
        ElseIf formulaire.Equals(FormulairePosition.text) Then
            images = session_value_provider.GetValue("images_text")
        ElseIf formulaire.Equals(FormulairePosition.compared) Then
            If (session_value_provider.GetValue("compared_images") IsNot Nothing) Then
                images = session_value_provider.GetValue("compared_images")
            End If
        End If

        If (images IsNot Nothing) Then
            images(current_page).Save(ms, System.Drawing.Imaging.ImageFormat.Png)
            imageByteData = ms.ToArray
        Else
            imageByteData = System.IO.File.ReadAllBytes(New Config().GetBlankImagePath)
        End If

        Return imageByteData
    End Function
End Class
