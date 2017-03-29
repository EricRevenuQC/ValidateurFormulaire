Imports System.Text.RegularExpressions
Imports System.Drawing

Public Class SearchPDFText
    Implements ISearchPDFText

    Private dictionary_operations As IDictionaryKeys
    Private verify_releve As IVerifyReleves

    Private Const NUMERO_VALIDATION As Integer = 1
    Private Const NUMERO_PREPARATEUR_INDEX As Integer = 2
    Private Const INDICATEUR_EMPLOYE_AU_POURBOIRE_INDEX As Integer = 27

    Sub New()
        dictionary_operations = New DictionaryKeys()
        verify_releve = New VerifyReleves()
    End Sub

    Sub New(dictionary_operations As IDictionaryKeys, verify_releve As IVerifyReleves)
        Me.dictionary_operations = dictionary_operations
        Me.verify_releve = verify_releve
    End Sub

    Public Function FindBarCodeValues(words() As Dictionary(Of Point, String),
                                      bar_code_data As Dictionary(Of String, BarCodeData)) _
                                  As Dictionary(Of String, BarCodeData) Implements ISearchPDFText.FindBarCodeValues
        Dim adjusted_bar_code_values As String
        Dim bar_code_index As Integer
        Dim failed_bar_code_values_index As New Dictionary(Of String, BarCodeData)()

        System.Diagnostics.Debug.WriteLine("Page number : " + words.Count.ToString)

        For page As Integer = 1 To words.Count - 1
            bar_code_index = 0
            words(page) = words(page).OrderByDescending(
                Function(x) x.Key.Y).ThenBy(Function(x) x.Key.X).ToDictionary(Function(x) x.Key, Function(y) y.Value)
            words(page) = AdjustWords(words(page))

            For Each test As KeyValuePair(Of Point, String) In words(page)
                System.Diagnostics.Debug.WriteLine(test.Value)
            Next

            For Each data As KeyValuePair(Of String, BarCodeData) In bar_code_data
                adjusted_bar_code_values = data.Value.value.Trim()

                'On ne vérifie pas le numéro du préparateur et l'indicateur d'employé au pourboire
                If bar_code_index <> NUMERO_VALIDATION AndAlso
                    bar_code_index <> NUMERO_PREPARATEUR_INDEX AndAlso
                        bar_code_index <> INDICATEUR_EMPLOYE_AU_POURBOIRE_INDEX Then
                    If words(page).ContainsValue(adjusted_bar_code_values) OrElse
                            String.IsNullOrWhiteSpace(adjusted_bar_code_values) Then
                        If Not verify_releve.VerifyTextInputPosition(words(page), data, bar_code_index) Then
                            'The _ is because chrome and IE automatically sort dictionaries when encoding a json if the key is
                            'recognized as an integer. Adding a _ makes chrome and IE recognize it as a string.
                            failed_bar_code_values_index.Add("_" + page.ToString + bar_code_index.ToString,
                                                                bar_code_data.ElementAt(bar_code_index).Value)
                        Else
                            words(page).Remove(dictionary_operations.GetKeyFromDictionaryValue(words(page), adjusted_bar_code_values))
                        End If
                    Else
                        'The _ is because chrome and IE automatically sort dictionaries when encoding a json if the key is
                        'recognized as an integer. Adding a _ makes chrome and IE recognize it as a string.
                        verify_releve.AddUncertainData()
                        failed_bar_code_values_index.Add("_" + page.ToString + bar_code_index.ToString,
                                                         bar_code_data.ElementAt(bar_code_index).Value)
                    End If
                End If

                bar_code_index += 1
            Next
        Next

        For Each data As KeyValuePair(Of String, BarCodeData) In failed_bar_code_values_index
            System.Diagnostics.Debug.WriteLine("Failed : " + data.Value.value.Trim())
        Next

        Dim success_percent As Single = (verify_releve.GetSuccessfulData / _
                                      (verify_releve.GetSuccessfulData + verify_releve.GetFailedData + _
                                       verify_releve.GetNotFoundData + verify_releve.GetUncertainData)) * 100
        If success_percent > 0 Then
            AlertsManager.SetAlertTitle("Analyse du relevé")
            AlertsManager.AddAlert("L'analyse s'est terminée avec " + _
                                   success_percent.ToString.Substring(0, Math.Min(success_percent.ToString.Length, 5)) + "% de réussite", _
                                   If((verify_releve.GetSuccessfulData > 0), "Nombre de données validées : " + verify_releve.GetSuccessfulData.ToString, "") + _
                                   If((verify_releve.GetNotFoundData > 0), Environment.NewLine + "Nombre de données incapable à validées : " + verify_releve.GetNotFoundData.ToString, "") + _
                                   If((verify_releve.GetFailedData > 0), Environment.NewLine + "Nombre de données erronées : " + verify_releve.GetFailedData.ToString, "") + _
                                   If((verify_releve.GetUncertainData > 0), Environment.NewLine + "Nombre de données incertaines : " + verify_releve.GetUncertainData.ToString, ""))
            If success_percent >= 75 Then
                AlertsManager.SetAlertColor("alert-success")
            ElseIf success_percent >= 50 Then
                AlertsManager.SetAlertColor("alert-warning")
            Else
                AlertsManager.SetAlertColor("alert-danger")
            End If
        Else
            AlertsManager.AddAlert("L'analyse a échouée.", "Vérifiez que le PDF n'est pas en format image.")
        End If

        Return failed_bar_code_values_index
    End Function

    Private Function AdjustWords(words As Dictionary(Of Point, String)) _
            As Dictionary(Of Point, String) Implements ISearchPDFText.AdjustWords
        Dim words_adjusted As New Dictionary(Of Point, String)()

        For Each word As KeyValuePair(Of Point, String) In words
            words_adjusted.Add(word.Key, Regex.Replace(word.Value, "(?<=\d)\s+(?=\d)", ""))
            words_adjusted(word.Key) = Regex.Replace(words_adjusted(word.Key), "(?<=\d),(?=\d)", "")
            words_adjusted(word.Key) = Regex.Replace(words_adjusted(word.Key), "(?<=\d)\.(?=\d)", "")
            words_adjusted(word.Key) = words_adjusted(word.Key).Trim()
        Next

        Return words_adjusted
    End Function
End Class
