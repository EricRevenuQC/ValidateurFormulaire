﻿Imports System.Drawing

Public Class DictionaryOperations
    Public Function GetValueOfKeyAsString(bar_code_property As BarCodeProperties, dict As Dictionary(Of String, BarCodeData)) As String
        If dict IsNot Nothing Then
            If dict.ContainsKey(bar_code_property.ToString) Then
                Return dict.Item(bar_code_property.ToString).value.ToString
            End If
        End If
        Return ""
    End Function

    Public Function GetKeyFromValue(dict As Dictionary(Of Point, String), value As String,
                                    Optional occurence As Integer = 1) As Point
        Dim key As Object

        For Each key In dict.Keys
            If dict.Item(key) = value Then
                If occurence = 1 Then
                    Return key
                Else
                    occurence -= 1
                End If
            End If
        Next

        Return Nothing
    End Function

    Public Function GetKeyFromValueContains(dict As Dictionary(Of Point, String), value As String,
                                            Optional occurence As Integer = 1) As Point
        Dim key As Object
        Dim last_key As Object = Nothing

        If occurence > 0 Then
            For Each key In dict.Keys
                If dict.Item(key).Contains(value) Then
                    If occurence = 1 Then
                        Return key
                    Else
                        occurence -= 1
                    End If
                End If
            Next
        Else
            For Each key In dict.Keys
                If dict.Item(key).Contains(value) Then
                    last_key = key
                End If
            Next
        End If

        Return last_key
    End Function
End Class