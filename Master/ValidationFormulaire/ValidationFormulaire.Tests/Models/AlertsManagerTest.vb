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

        AlertsManager.AddAlert("test", "oops")
        manager = AlertsManager.GetAllAlerts()

        Assert.IsTrue(manager.ContainsKey("test"))
        Assert.IsTrue(manager.ContainsValue("oops"))
    End Sub

    <TestMethod()> _
    Public Sub VerifyThatKeyAndValueNotInAlertsAreNotFoundInAlerts()
        AlertsManager.ClearAlerts()
        Dim manager As New Dictionary(Of String, String)

        AlertsManager.AddAlert("test", "oops")
        manager = AlertsManager.GetAllAlerts()

        Assert.IsFalse(manager.ContainsKey("KEY"))
        Assert.IsFalse(manager.ContainsValue("VALEUR"))
    End Sub

    <TestMethod()> _
    Public Sub VerifyThatMessageIsAddedToTitle()
        AlertsManager.ClearAlerts()
        Dim manager As New Dictionary(Of String, String)

        AlertsManager.AddAlert("test")

        AlertsManager.AddAlert("test", "oops")
        manager = AlertsManager.GetAllAlerts()

        Assert.IsTrue(manager.ContainsValue(Environment.NewLine + "oops"))
    End Sub
End Class
