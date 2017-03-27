Imports System
Imports ValidationFormulaire.Core
Imports System.IO
Imports System.Collections.Generic
Imports ValidationFormulaire


<TestClass()> _
Public Class AlertsManagerTest
    Private alert_title As String
    Private alert_message As String

    <TestInitialize()> _
    Public Sub Initialize()
    End Sub

    <TestMethod()> _
    Public Sub VerifyThatKeyAndValueInAlertsAreFoundInAlerts()
        AlertsManager.ClearAlerts()
        Dim manager As New Dictionary(Of String, String)

        AlertsManager.AddAlert("key", "valeur")
        manager = AlertsManager.GetAllAlerts()

        Assert.IsTrue(manager.ContainsKey("key"))
        Assert.IsTrue(manager.ContainsValue("valeur"))
    End Sub

    <TestMethod()> _
    Public Sub VerifyThatKeyAndValueNotInAlertsAreNotFoundInAlerts()
        AlertsManager.ClearAlerts()
        Dim manager As New Dictionary(Of String, String)

        AlertsManager.AddAlert("key", "valeur")
        manager = AlertsManager.GetAllAlerts()

        Assert.IsFalse(manager.ContainsKey("mauvaise key"))
        Assert.IsFalse(manager.ContainsValue("mauvaise valeur"))
    End Sub

    <TestMethod()> _
    Public Sub VerifyThatMessageIsAddedToTitle()
        AlertsManager.ClearAlerts()
        Dim manager As New Dictionary(Of String, String)

        AlertsManager.AddAlert("key")

        AlertsManager.AddAlert("key", "valeur")
        manager = AlertsManager.GetAllAlerts()

        Assert.IsTrue(manager.ContainsValue(Environment.NewLine + "valeur"))
    End Sub
End Class
