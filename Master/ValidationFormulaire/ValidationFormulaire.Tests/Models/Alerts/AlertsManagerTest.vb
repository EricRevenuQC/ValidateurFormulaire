Imports System
Imports ValidationFormulaire.Core
Imports System.IO
Imports System.Collections.Generic
Imports ValidationFormulaire


<TestClass()> _
Public Class AlertsManagerTest
    Private alerts_manager As AlertsManager
    Private alert_title As String
    Private alert_message As String

    <TestInitialize()> _
    Public Sub Initialize()
        alerts_manager = New AlertsManager()
    End Sub

    <TestMethod()> _
    Public Sub VerifyThatKeyAndValueInAlertsAreFoundInAlerts()
        Dim manager As New Dictionary(Of String, String)

        alerts_manager.AddAlert("test", "oops")
        manager = alerts_manager.GetAllAlerts()

        Assert.IsTrue(manager.ContainsKey("test"))
        Assert.IsTrue(manager.ContainsValue("oops"))
    End Sub

    <TestMethod()> _
    Public Sub VerifyThatKeyAndValueNotInAlertsAreNotFoundInAlerts()
        Dim manager As New Dictionary(Of String, String)

        alerts_manager.AddAlert("test", "oops")
        manager = alerts_manager.GetAllAlerts()

        Assert.IsFalse(manager.ContainsKey("KEY"))
        Assert.IsFalse(manager.ContainsValue("VALEUR"))
    End Sub

    <TestMethod()> _
    Public Sub VerifyThatMessageIsAddedToTitle()
        Dim manager As New Dictionary(Of String, String)

        alerts_manager.AddAlert("test")

        alerts_manager.AddMessageToTitle("test", "oops")
        manager = alerts_manager.GetAllAlerts()

        Assert.IsTrue(manager.ContainsValue(Environment.NewLine + "oops"))
    End Sub
End Class
