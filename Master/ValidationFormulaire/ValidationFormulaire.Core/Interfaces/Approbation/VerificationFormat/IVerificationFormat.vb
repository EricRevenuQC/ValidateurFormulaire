Imports System.Drawing

Public Interface IVerificationFormat
    Function Verification(images() As Image) As Image()
    Function CalculateThreshold(images() As Image, template_image As Image,
                           Optional distance_threshold As Integer = 0) As Image()

End Interface
