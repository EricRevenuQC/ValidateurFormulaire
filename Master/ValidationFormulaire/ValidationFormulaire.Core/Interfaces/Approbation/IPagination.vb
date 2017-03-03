Imports ValidationFormulaire.Core.DeterminePage

Public Interface IPagination
    Function DeterminePageChange(action As DeterminePage.PageAction, current_page As Integer, page As Integer) As Integer
End Interface
