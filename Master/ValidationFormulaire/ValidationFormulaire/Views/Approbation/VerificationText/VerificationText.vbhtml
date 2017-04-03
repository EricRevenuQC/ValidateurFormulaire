@ModelType ValidationFormulaire.Models.ApprobationModel

@Code
    ViewData("Title") = "VerificationText"
	Layout = "~/Views/Approbation/VerificationText/Layouts/VerificationTextLayout.vbhtml"
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
            Display text.
        -->
		@If Model IsNot Nothing AndAlso Model.analysed_text IsNot Nothing AndAlso Model.analysed_text.Count > 0 Then
			@<div class="text-center">
				<div style="display: inline-block; margin: 0 auto;">
					<div class="col-md-12 text-left" style="width: auto;">
						<h4 class="text-center">Résultat de l'analyse du text :</h4>
						<br />
						<br />
			
						@For i As Integer = 1 To Model.analysed_text.Count - 1
						Dim image_id As String = "image_text" + i.ToString
						Dim image_display_block As String
						If (Session("current_page_text") = i) Then
						image_display_block = "display:block"
						Else
						image_display_block = "display:none"
						End If
				
						@<div id="@image_id" style="@image_display_block">
							@Html.Raw(Model.analysed_text(i))
						</div>
						Next
					</div>
				</div>
			</div>
			@<div class="row">
				@code
					Dim display_block As String
					@If (Session("current_page_text") > 1) Then
                		display_block = "display:block"
					Else
                		display_block = "display:none"
					End If
					@<div class="col-md-6">
						<div style="@display_block" id="previous-page-text">
							<button type="button" class="btn btn-primary btn-sm btn-block" 
								onclick="ChangePage('@ValidationFormulaire.Core.FormulairePosition.text',
									'@ValidationFormulaire.Core.DeterminePage.PageAction.PreviousPage')">
								Page précédente</button>
						</div>
					</div>
					@If (Session("current_page_text") < Model.analysed_text.Count - 1) Then
					display_block = "display:block"
					Else
					display_block = "display:none"
					End If
					@<div class="col-md-6">
						<div style="@display_block" id="next-page-text">
							<button type="button" class="btn btn-primary btn-sm btn-block"
								onclick="ChangePage('@ValidationFormulaire.Core.FormulairePosition.text',
									'@ValidationFormulaire.Core.DeterminePage.PageAction.NextPage')">
								Page suivante</button>
						 </div>
					</div>
				End code
			</div>
		End If
	</div>
    <div class="row top10"></div>
</div>

<!--
    Set javacript global variables for verification view.
-->
<script type="text/javascript">
	var current_page_text = parseInt('@Session("current_page_text")');
	var page_number_text = parseInt('@Session("page_number_text")');
</script>

<script type="text/javascript" src="/content/js/Approbation/pagination.js"></script>


