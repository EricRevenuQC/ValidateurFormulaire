Imports ValidationFormulaire.Core.PDFToImageConverter
Imports System.Drawing

Public Interface IVerification
    Function VerifiyWithTemnplate(images() As Drawing.Image, template_image As Image,
                                  Optional distance_treshold As Single = 0,
                                  Optional alerts_manager As AlertsManager = Nothing) As Drawing.Image()
    Function DetermineInputZone(images() As Drawing.Image, alerts_manager As AlertsManager) As Drawing.Image()
End Interface
