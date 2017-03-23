@ModelType ValidationFormulaire.Models.AuthorizationModel

@Code
    ViewData("Title") = "ValidationDonnees"
	Layout = "~/Views/Authorization/Layouts/ValidationDonneesLayout.vbhtml"
End Code      

<div class="container-fluid">
	<div class="row top10">
		@If Model IsNot Nothing AndAlso Model.alert_messages IsNot Nothing AndAlso Model.alert_messages.Count > 0 Then
            @<div class="alert alert-dismissable alert-danger">
			    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
			    <h2>Attention!</h2>
                <h4>Des erreurs ont été trouvées.</h4>
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
		<label for="show-template-checkbox">Montrer le gabarit</label><br />
		<input id="show-template-checkbox" type="checkbox">
        <div class="col-md-12">
            <div class="row">
                <!--
                    Display left side images.
                -->
		        <div class="col-md-6" id="template" style="display: none;">
					@Html.Partial("Releves/Releve1", Model)
				</div>
                <!--
                    Display right side images.
                -->
                <div id="pdf-image" class="col-md-12">
                    @For i As Integer = 1 To Session("page_number_releve_donnees")
                    	Dim image_id_releve_donnees As String = "image_releve_donnees" + i.ToString
                    	Dim image_display_block_right As String
                    	If (Session("current_page_releve_donnees") = i) Then
                    		image_display_block_right = "display:block"
                    	Else
                    		image_display_block_right = "display:none"
                    	End If
			            @<div id="@image_id_releve_donnees" style="@image_display_block_right">
                            <div id="partial-image-releve-donnees">
                            @Html.Partial("Partials/ValidationRelevePartial", Nothing, New ViewDataDictionary() From { _
                            						{"current_page", i}, _
                            						{"formulaire", ValidationFormulaire.Core.FormulairePosition.donnees} _
                            					})
                            </div>
			            </div>
                    Next
                    <!--
                        Right side pagination.
                    -->
                    <div class="row">
                        @code
                            Dim display_block_right As String
                            @If (Session("current_page_releve_donnees") > 1) Then
                            	display_block_right = "display:block"
                            Else
                            	display_block_right = "display:none"
                            End If
                            @<div class="col-md-6">
                                <div style="@display_block_right" id="previous-page-releve-donnees">
                                    <button type="button" class="btn btn-primary btn-sm btn-block" 
                                        onclick="ChangePageWithDialog('@ValidationFormulaire.Core.FormulairePosition.donnees',
                                            '@ValidationFormulaire.Core.DeterminePage.PageAction.PreviousPage')">
                                        Page précédente
                                    </button>
                                </div>
                            </div>
                            @If (Session("current_page_releve_donnees") < (Session("page_number_releve_donnees"))) Then
                            	display_block_right = "display:block"
                            Else
                            	display_block_right = "display:none"
                            End If
                            @<div class="col-md-6">
                                <div style="@display_block_right" id="next-page-releve-donnees">
                                    <button type="button" class="btn btn-primary btn-sm btn-block"
                                        onclick="ChangePageWithDialog('@ValidationFormulaire.Core.FormulairePosition.donnees',
                                            '@ValidationFormulaire.Core.DeterminePage.PageAction.NextPage')">
                                        Page suivante
                                    </button>
                                </div>
                            </div>
                        End code
                    </div>
		        </div>
            </div>
        </div>
    </div>
	<div class="row top5"></div>
	<div id="dialog-bottom-space" class="row top190" style="display: block"></div>
</div>
<div style="display: none;">
	@If Model.bar_code_unverified_data.Count > 0 Then
		@For i As Integer = 0 To Model.bar_code_unverified_data.Count - 1
		 Dim dialog_id As String = "dialog_id" + i.ToString
		 Dim dialog_valider_id As String = "dialog_valider_id" + i.ToString
		 Dim dialog_annuler_id As String = "dialog_annuler_id" + i.ToString
		 Dim dialog_precedent_id As String = "dialog_precedent_id" + i.ToString
		 Dim dialog_passer_id As String = "dialog_passer_id" + i.ToString
		 Dim dialog_input_id As String = "dialog_input_id" + i.ToString
		 Dim dialog_label_id As String = "dialog_label_id" + i.ToString
		 Dim dialog_page_text As String = "dialog_page_text" + i.ToString
		 Dim col_width As String = "col-md-8"
		@<div id="@dialog_id"  title="Veuillez entrer les données demandées inscrites sur le relevé">
			<div data-role="body">
				<label for="dialog_input_id" id="@dialog_label_id">
					@If Session("page_number_releve_donnees") > 1 Then
						@<span id="@dialog_page_text">
							Page @Session("current_page_releve_donnees")
						 </span>
						@<br />
					End If
					@i - @Model.bar_code_unverified_data.ElementAt(i).Value.description
				</label>
				<input type="text" class="form-control" id="@dialog_input_id"/>
			</div>
			<br />
			<div data-role="footer">
				<div class="col-md-2 nopadding text-left">
					<button class="btn btn-primary btn-sm" id="@dialog_annuler_id">Annuler</button>
				</div>
				@If i > 0 Then
					@<div class="col-md-2 nopadding">
						<button class="btn btn-primary btn-sm" id="@dialog_precedent_id">Retour</button>
					</div>
				col_width = "col-md-6"
				End If
				<div class="@col_width nopadding text-right">
					<button class="btn btn-primary btn-sm" id="@dialog_passer_id">Passer</button>
				</div>
				<div class="col-md-2 nopadding text-right">
					<button class="btn btn-primary btn-sm" id="@dialog_valider_id">Valider</button>.
				</div>
			</div>
		</div>
				Next
	End If
</div>
<div style="display: none;">
	<div id="dialog-confirmation" title="Veuillez confirmer">
		<div data-role="body">
			Le champ saisie n'est pas identique à celui indiqué par le code à bar.<br />
			Vous avez entré : <div id="dialog-text-input" style="display: inline"></div><br />
			Le code à bar donne : <div id="dialog-text-barcode" style="display: inline"></div><br />
		</div>
		<br />
		<div data-role="footer">
			<div class="col-md-2 nopadding">
				<button class="btn btn-primary btn-sm" id="dialog-annuler">Annuler</button>
			</div>
			<div class="col-md-10 nopadding text-right">
				<button class="btn btn-primary btn-sm" id="dialog-confirmer">Confirmer</button>
			</div>
		</div>
	</div>
</div>

<!--
    Set javacript global variables for comparison view.
-->
<script type="text/javascript">
	var current_page_releve_donnees = parseInt('@Session("current_page_releve_donnees")');
	var page_number_releve_donnees = parseInt('@Session("page_number_releve_donnees")');
	var formulaire_position = '@ValidationFormulaire.Core.FormulairePosition.donnees';
	var page_action = '@ValidationFormulaire.Core.DeterminePage.PageAction.NextPage';
	var data_count = Razor(@Model.bar_code_unverified_data.Count);
	var collection = Razor(@Html.Raw(Json.Encode(Model.bar_code_unverified_data)));
	var current_dialog = 0;
	var data_log = [];
	var dialog_per_page = [];
	SetDialogPerPage();
	DataLogSetup();	

	function Razor(obj) { return obj; }

	function SaveFile(filename, data) {
		var blob = new Blob([data], { type: "text/plain;charset=utf-8" });
		saveAs(blob, filename);
	}

	function DataLogSetup() {
		var key_index = 0;
		$.each(collection, function (key, value) {
			data_log[key_index] = {
				description: value.description,
				bar_code_value: value.value,
				input_value: ""
			};
			key_index += 1
		});
	}

	function SetDialogPerPage() {
		var data_index = 0;
		var previous_page_indicator = 0;
		var current_page_indicator = 0;

		$.each(collection, function (key, value) {
			data_index += 1;
			current_page_indicator = key.charAt(1);
			if (previous_page_indicator != current_page_indicator) {
				dialog_per_page.push(data_index);
				data_index = 0;
			}
			previous_page_indicator = current_page_indicator;
		});
		dialog_per_page.push(data_index + 1);
	}
</script>

<script type="text/javascript" src="/content/js/Authorization/pagination.js"></script>
<script type="text/javascript" src="/content/js/Authorization/inputDialog.js"></script>
<script type="text/javascript" src="/content/js/Authorization/confirmationDialog.js"></script>
<script type="text/javascript" src="/content/js/Authorization/checkbox.js"></script>