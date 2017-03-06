@ModelType ValidationFormulaire.Models.AuthorizationModel

@Code
    ViewData("Title") = "ValidationDonnees"
	Layout = "~/Views/Authorization/Layouts/ValidationDonneesLayout.vbhtml"
End Code      

<style>
	.ui-widget-overlay.custom-overlay { 
		opacity: 0 !important;
	}
	.ui-widget-overlay.custom-overlay-black { 
		background-color: black;
		background-image: none;
		opacity: .25 !important;
	}
	.ui-dialog-titlebar-close {
		visibility: hidden;
	}

	.nopadding {
	   padding: 1px !important;
	   margin-left: 0 !important;
	}
</style>

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
</div>
<div style="display: none;">
	@If Model.bar_code_data.Count > 0 Then
		@For i As Integer = 0 To Model.bar_code_data.Count - 1
		 Dim dialog_id As String = "dialog_id" + i.ToString
		 Dim dialog_valider_id As String = "dialog_valider_id" + i.ToString
		 Dim dialog_annuler_id As String = "dialog_annuler_id" + i.ToString
		 Dim dialog_precedent_id As String = "dialog_precedent_id" + i.ToString
		 Dim dialog_passer_id As String = "dialog_passer_id" + i.ToString
		 Dim dialog_input_id As String = "dialog_input_id" + i.ToString
		 Dim dialog_label_id As String = "dialog_label_id" + i.ToString
		 Dim dialog_page_text As String = "dialog_page_text" + i.ToString
		 Dim col_width As String = "col-md-8"
		@<div id="@dialog_id"  title="Veuillez entrer les données demandées inscrites sur le relevé "style="background:lightblue">
			<div data-role="body">
				<label for="dialog_input_id" id="@dialog_label_id" style="background:lightblue">
					@If Session("page_number_releve_donnees") > 1 Then
						@<span id="@dialog_page_text">
							Page @Session("current_page_releve_donnees")
						 </span>
						@<br />
					End If
					@i - @Model.bar_code_data([Enum].GetName(GetType(ValidationFormulaire.Core.BarCodeProperties), i)).description
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
	<div id="dialog-confirmation" title="Veuillez confirmer"style="background:lightblue">
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
	var current_dialog = 0;
	var wrong_data_list_messages = new Array();
	var data_log = [];
	for (var i = 0; i < page_number_releve_donnees; i++) {
		data_log[i] = {}
		$.each(Razor(@Html.Raw(Json.Encode(Model.bar_code_data))), function (key, value) {
			data_log[i][key] = {
				description: value.description,
				bar_code_value: value.value,
				input_value: ""
			};
		});
	}

	function Razor(obj) { return obj; }

	function keypressHandler(e) {
		if (e.which == 13) {
			e.preventDefault(); //stops default action: submitting form
			$(this).blur();
			$("#dialog_valider_id".concat(current_dialog)).focus().click();
		}
	}

	$('#show-template-checkbox').checkboxpicker({
		html: true,
		offLabel: '<span class="glyphicon glyphicon-remove">',
		onLabel: '<span class="glyphicon glyphicon-ok">'
	});

	$('#show-template-checkbox').on('change', function () {
		if (document.getElementById("template").style.display == "none") {
			document.getElementById("template").style.display = "block";
			$("#pdf-image").removeClass("col-md-12");
			$("#pdf-image").addClass("col-md-6");
		}
		else {
			document.getElementById("template").style.display = "none";
			$("#pdf-image").removeClass("col-md-6");
			$("#pdf-image").addClass("col-md-12");
		}
	});

	for (i = 0; i < Razor(@Model.bar_code_data.Count); i++) {
		$("#dialog_input_id".concat(i)).keypress(keypressHandler);
	}

	$("#dialog-annuler").click(function () {
		$("#dialog-confirmation").dialog('close');
		wrong_data_list_messages = [];
		$("#dialog_input_id".concat(current_dialog)).focus()
	});

	$("#dialog-confirmer").click(function () {
		$("#dialog-confirmation").dialog('close');
		NextDialog();
		wrong_data_list_messages.push($("#dialog_label_id".concat(current_dialog)).text().replace(/\r?\n|\r/g, '') + " - saisie: " +
			$("#dialog-text-input").text() + ", CAB: " + $("#dialog-text-barcode").text());
		var collection = Razor(@Html.Raw(Json.Encode(Model.bar_code_data)));
		var data_index = 0;

		$.each(collection, function (key, value) {
			if (data_index === current_dialog) {
				data_log[current_page_releve_donnees][key]["input_value"] = $("#dialog-text-input").text();
			}
			data_index += 1;
		});
	});

	$("#dialog-confirmation").dialog({
		uiLibrary: 'bootstrap',
		resizable: false,
		draggable: false,
		autoOpen: false,
		height: "auto",
		width: "auto",
		autofocus: true,
		open: function () {
			$('.ui-widget-overlay').addClass('custom-overlay-black');
		},
		close: function () {
			$('.ui-widget-overlay').removeClass('custom-overlay-black');
		},
		modal: true
	});

	dialog = $("#dialog_id0").dialog({
		uiLibrary: 'bootstrap',
		autoOpen: true,
		resizable: true,
		minWidth: 200,
		maxWidth: 600,
		minHeight: 100,
		maxHeight: 450,
		height: "auto",
		width: 500,
		position: {
			my: "center",
			at: "bottom",
			of: window
		},
		open: function () {
			$('.ui-widget-overlay').addClass('custom-overlay');
		},
		close: function () {
			$('.ui-widget-overlay').removeClass('custom-overlay');
		},
		autofocus: true
	});

	for (i = 0; i < Razor(@Model.bar_code_data.Count); i++) {
		$("#dialog_annuler_id".concat(i)).click(function () {
			$("#dialog_id".concat(current_dialog)).dialog('close');
			current_dialog = 0;
		});
		$("#dialog_precedent_id".concat(i)).click(function () {
			PreviousDialog();
		});
		$("#dialog_valider_id".concat(i)).click(function () {
			ValidateValue();
		});
		$("#dialog_passer_id".concat(i)).click(function () {
			NextDialog();
		});
	}

	function ValidateValue() {
		var collection = Razor(@Html.Raw(Json.Encode(Model.bar_code_data)));
		var data_index = 0;

		$.each(collection, function (key, value) {
			if (data_index === current_dialog) {
				var bar_code_value = value.value;

				// Remove spaces from all values except expected strings.
				if (current_dialog != 35 && current_dialog != 36 && current_dialog != 37 && current_dialog != 38
					 && current_dialog != 39 && current_dialog != 40 && current_dialog != 42 && current_dialog != 43
					 && current_dialog != 44 && current_dialog != 45 && current_dialog != 46 && current_dialog != 47) {
					bar_code_value = bar_code_value.replace(/\s/g, '');
				}
				else {
					bar_code_value = bar_code_value.replace(/\s+/g, " ");
					bar_code_value = bar_code_value.replace(/\s+$/, '');
				}

				if (bar_code_value === $("#dialog_input_id".concat(current_dialog)).val()) {
					NextDialog();
					data_log[current_page_releve_donnees][key]["input_value"] = $("#dialog_input_id".concat(current_dialog)).val();
					return false
				}
				else {
					$("#dialog-confirmation").dialog('open');
					$("#dialog-text-input").text($("#dialog_input_id".concat(current_dialog)).val());
					$("#dialog-text-barcode").text(bar_code_value);
					$("#dialog-confirmer").focus()
					return false

				}
			}
			data_index += 1;
		});
	}

	function NextDialog() {
		var height = $("#dialog_id".concat(current_dialog)).closest('.ui-dialog').outerHeight();
		var width = $("#dialog_id".concat(current_dialog)).closest('.ui-dialog').outerWidth();
		var offset = $("#dialog_id".concat(current_dialog)).closest('.ui-dialog').offset();
		var last_position = "left+".concat(offset.left).concat(" ").concat("top+").concat(
			offset.top - (document.documentElement.scrollTop || document.body.scrollTop));

		if ($("#dialog_id".concat(current_dialog + 1)).hasClass('ui-dialog-content')) {
			$("#dialog_id".concat(current_dialog + 1)).dialog({
				position: {
					my: "left top",
					at: last_position,
					of: window
				}
			});
			$("#dialog_id".concat(current_dialog + 1)).dialog("option", "height", height);
			$("#dialog_id".concat(current_dialog + 1)).dialog("option", "width", width);
		}
		else {
			$("#dialog_id".concat(current_dialog + 1)).dialog({
				uiLibrary: 'bootstrap',
				resizable: true,
				autoOpen: false,
				minWidth: 200,
				maxWidth: 600,
				minHeight: 100,
				maxHeight: 450,
				height: height,
				width: width,
				position: {
					my: "left top",
					at: last_position
				},
				open: function () {
					$('.ui-widget-overlay').addClass('custom-overlay');
				},
				close: function () {
					$('.ui-widget-overlay').removeClass('custom-overlay');
				},
				autofocus: true
			});
		}
		$("#dialog_id".concat(current_dialog)).dialog('close');

		if (current_dialog < Razor(@Model.bar_code_data.Count - 1)) {
			$("#dialog_id".concat(current_dialog + 1)).dialog('open');
			$("#dialog_page_text".concat(current_dialog + 1)).text("Page " + current_page_releve_donnees);
			current_dialog += 1
		}
		else if (current_page_releve_donnees < page_number_releve_donnees) {
			alert("Prochaine page");
			current_dialog = 0;
			$("#dialog_id".concat(current_dialog)).dialog({
				position: {
					my: "left top",
					at: last_position,
					of: window
				}
			});
			$("#dialog_id".concat(current_dialog)).dialog("option", "height", height);
			$("#dialog_id".concat(current_dialog)).dialog("option", "width", width);
			$("#dialog_id".concat(current_dialog)).dialog('open');
			ChangePage('@ValidationFormulaire.Core.FormulairePosition.donnees',
				'@ValidationFormulaire.Core.DeterminePage.PageAction.NextPage')
			$("#dialog_page_text".concat(current_dialog)).text("Page " + current_page_releve_donnees);
		}
		else {
			alert("Terminer");
			var text = "";
			for (var i = 1; i < data_log.length + 1; i++) {
				text = text + "Page " + i + "\n";
				var obj = data_log[i];
				for (var key in obj) {
					if (!obj.hasOwnProperty(key)) continue;
					text = text + "Description : " + data_log[i][key].description + "\n";
					text = text + "Code bar : " + data_log[i][key].bar_code_value + "\n";
					text = text + "Saisie : " + data_log[i][key].input_value + "\n\n";
				}
			}
			alert(text);
		}
	}

	function PreviousDialog() {
		if (current_dialog > 0) {
			var height = $("#dialog_id".concat(current_dialog)).closest('.ui-dialog').outerHeight();
			var width = $("#dialog_id".concat(current_dialog)).closest('.ui-dialog').outerWidth();
			var offset = $("#dialog_id".concat(current_dialog)).closest('.ui-dialog').offset();
			var last_position = "left+".concat(offset.left).concat(" ").concat("top+").concat(
				offset.top - (document.documentElement.scrollTop || document.body.scrollTop));

			$("#dialog_id".concat(current_dialog - 1)).dialog({
				position: {
					my: "left top",
					at: last_position,
					of: window
				}
			});
			$("#dialog_id".concat(current_dialog - 1)).dialog("option", "height", height);
			$("#dialog_id".concat(current_dialog - 1)).dialog("option", "width", width);
			$("#dialog_id".concat(current_dialog)).dialog('close');
			$("#dialog_id".concat(current_dialog - 1)).dialog('open');
			$("#dialog_page_text".concat(current_dialog - 1)).text("Page " + current_page_releve_donnees);
			
			current_dialog -= 1;
		}
	}

	function ChangePageWithDialog(formulaire_side, action) {
		ChangePage(formulaire_side, action);
		$("#dialog_page_text".concat(current_dialog)).text("Page " + current_page_releve_donnees);
	}
</script>

<script type="text/javascript" src="/content/js/Authorization/pagination.js"></script>