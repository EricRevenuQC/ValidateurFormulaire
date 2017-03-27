Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports ValidationFormulaire.Core

<TestClass()> Public Class Releve1Tests
    Private releve1 As Releve1

    <TestInitialize()> _
    Public Sub Initialize()
        releve1 = New Releve1()
    End Sub

    <TestMethod()> Public Sub VerifyThatAllListsOfMarkersAreTheSameSizeAsTheListOfTheirOccurenceNumber()
        For i As Integer = 0 To 56
            releve1.SetReleveMarkers(i)
            System.Diagnostics.Debug.WriteLine(i)
            Assert.AreEqual(releve1.GetNorthMarker.Count, releve1.GetNorthMarkerOccurence.Count)
            Assert.AreEqual(releve1.GetSouthMarker.Count, releve1.GetSouthMarkerOccurence.Count)
            Assert.AreEqual(releve1.GetWestMarker.Count, releve1.GetWestMarkerOccurence.Count)
            Assert.AreEqual(releve1.GetEastMarker.Count, releve1.GetEastMarkerOccurence.Count)
        Next
    End Sub
End Class