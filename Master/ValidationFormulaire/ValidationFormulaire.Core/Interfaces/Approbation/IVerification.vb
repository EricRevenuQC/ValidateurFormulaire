Imports ValidationFormulaire.Core.PDFToImageConverter
Imports System.Drawing

Public Interface IVerification
    Function VerifiyWithTemnplate(images() As Drawing.Image, template_image As Image,
                                  Optional distance_treshold As Single = 0) As Drawing.Image()
    Function DetermineInputZone(images() As Drawing.Image) As Drawing.Image()
End Interface
