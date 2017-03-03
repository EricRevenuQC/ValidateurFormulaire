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

        alerts_manager.AddAlert("KEY", "VALEUR")
        manager = alerts_manager.GetAllAlerts()

        Assert.IsTrue(manager.ContainsKey("KEY"))
        Assert.IsTrue(manager.ContainsValue("VALEUR"))
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
    Public Sub VerifyThatMessageAddedToTitle1()

        Dim manager As New Dictionary(Of String, String)


        alerts_manager.AddAlert("KEY")

        alerts_manager.AddMessageToTitle(alert_title:="KEY", alert_message:="VALEUR")
        manager = alerts_manager.GetAllAlerts()

        Assert.IsTrue(manager.ContainsKey("KEY"))
        Assert.IsTrue(manager.ContainsValue(Environment.NewLine + "VALEUR"))

    End Sub


    <TestMethod()> _
    Public Sub VerifyThatMessageAddedToTitleIsNotCorrect()

        Dim manager As New Dictionary(Of String, String)


        alerts_manager.AddAlert("KEY")

        alerts_manager.AddMessageToTitle(alert_title:="KEY", alert_message:="VALEUR")
        manager = alerts_manager.GetAllAlerts()


        Assert.IsFalse(manager.ContainsValue(Environment.NewLine + "VALEUR1"))

    End Sub
End Class
