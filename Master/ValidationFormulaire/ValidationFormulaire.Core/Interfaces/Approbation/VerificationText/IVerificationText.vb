Imports System.Drawing
Imports System.Web

Public Interface IVerificationText
    Function VerificationText(file As HttpPostedFileBase, file_template As HttpPostedFileBase) As String()
End Interface
