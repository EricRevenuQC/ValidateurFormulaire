@ModelType ValidationFormulaire.Models.ApprobationModel

@Code
    ViewData("Title") = "VerificationFormat"
	Layout = "~/Views/Approbation/Verification/Layouts/VerificationFormatLayout.vbhtml"
End Code

<div class="container-fluid">
  

	<div class="row top10">
        @If Model IsNot Nothing AndAlso Model.alert_messages IsNot Nothing AndAlso Model.alert_messages.Count > 0 Then
            @<div class="alert alert-dismissable alert-danger">
			    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                <h4>@Model.alert_title</h4>
                <br />
                @For Each alert In Model.alert_messages
                    @If Not String.IsNullOrWhiteSpace(alert.Key) Then
                        @<strong>• @alert.Key</strong>
                        @If Not String.IsNullOrWhiteSpace(alert.Value) Then
                            @<span style="white-space: pre-line">
                                @alert.Value
                            </span>
                        End If
                        @<br />
                    End If
                Next
		    </div>
        End If
        <!--
            Display image.
        -->
		<div class="col-md-12">
			@For i As Integer = 1 To Session("page_number")
			    Dim image_id As String = "image" + i.ToString
			    Dim image_display_block As String
			    If (Session("current_page") = i) Then
			    image_display_block = "display:block"
			    Else
			    image_display_block = "display:none"
			    End If
			    @<div id="@image_id" style="@image_display_block">
                    <div id="image_partial_view">
                        @Html.Partial("../ImagesPartial", Nothing, New ViewDataDictionary() From { _
                        											{"current_page", i}, _
                        											{"formulaire", ValidationFormulaire.Core.FormulairePosition.center} _
                        										})
                    </div>
			    </div>
			Next
		</div>
        <!--
            Pagination.
        -->
        <div class="row">
            @code
                Dim display_block As String
                @If (Session("current_page") > 1) Then
                    display_block = "display:block"
                Else
                    display_block = "display:none"
                End If
                @<div class="col-md-6">
                    <div style="@display_block" id="previous-page">
                        <button type="button" class="btn btn-primary btn-sm btn-block" 
                            onclick="ChangePage('@ValidationFormulaire.Core.FormulairePosition.center',
                                '@ValidationFormulaire.Core.DeterminePage.PageAction.PreviousPage')">
                            Page précédente</button>
                    </div>
                </div>
                @If (Session("current_page") < (Session("page_number"))) Then
                    display_block = "display:block"
                Else
                    display_block = "display:none"
                End If
                @<div class="col-md-6">
                    <div style="@display_block" id="next-page">
                        <button type="button" class="btn btn-primary btn-sm btn-block"
                            onclick="ChangePage('@ValidationFormulaire.Core.FormulairePosition.center',
                                '@ValidationFormulaire.Core.DeterminePage.PageAction.NextPage')">
                            Page suivante</button>
                     </div>
                </div>
            End code
        </div>
	</div>
    <div class="row top10"></div>
</div>

<!--
    Set javacript global variables for verification view.
-->
<script type="text/javascript">
    var current_page = parseInt('@Session("current_page")');
    var page_number = parseInt('@Session("page_number")');
</script>

<script type="text/javascript" src="/content/js/Approbation/pagination.js"></script>


