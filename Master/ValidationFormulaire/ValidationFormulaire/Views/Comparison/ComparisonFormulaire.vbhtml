@ModelType ValidationFormulaire.Models.ApprobationModel

@Code
    ViewData("Title") = "VerificationFormat"
    Layout = "~/Views/Comparison/Layouts/ComparisonFormulaireLayout.vbhtml"
End Code

<div class="container-fluid">
	<div class="row top10">
        <div class="col-md-12">
            <div class="row">
                <!--
                    Display left side images.
                -->
		        <div class="col-md-6">
                    <div id="compared-image-left" style="display:@Session("comparing")">
                        <div id="partial-image-compared-left">
                            @Html.Partial("Partials/ApprobationImagesPartial", Nothing, New ViewDataDictionary() From { _
                                                            {"current_page", 0}, _
                                                            {"formulaire", ValidationFormulaire.Core.FormulairePosition.compared} _
                                                        })
                        </div>
                    </div>
                    <div id="non-compared-image-left" style="display:@Session("non_comparing")">
                        @For i As Integer = 1 To Session("page_number_left")
                            Dim image_id_left As String = "image_left" + i.ToString
                            Dim image_display_block_left As String
                            If (Session("current_page_left") = i) Then
                                image_display_block_left = "display:block"
                            Else
                                image_display_block_left = "display:none"
                            End If
		                    @<div id="@image_id_left" style="@image_display_block_left">
                                <div id="partial-image-left">
                                    @Html.Partial("Partials/ApprobationImagesPartial", Nothing, New ViewDataDictionary() From { _
                                                            {"current_page", i}, _
                                                            {"formulaire", ValidationFormulaire.Core.FormulairePosition.left} _
                                                        })
                                </div>
		                    </div>
                        Next
                    </div>
                    <!--
                        Left side pagination.
                    -->
                    <div class="row">
                        @code
                            Dim display_block As String
                            @If (Session("current_page_left") > 1) Then
                                display_block = "display:block"
                            Else
                                display_block = "display:none"
                            End If
                            @<div class="col-md-6">
                                <div style="@display_block" id="previous-page-left">
                                    <button type="button" class="btn btn-primary btn-sm btn-block" 
                                        onclick="ChangePage('@ValidationFormulaire.Core.FormulairePosition.left',
                                            '@ValidationFormulaire.Core.DeterminePage.PageAction.PreviousPage')">
                                        Page précédente
                                    </button>
                                </div>
                            </div>
                            @If (Session("current_page_left") < (Session("page_number_left"))) Then
                                display_block = "display:block"
                            Else
                                display_block = "display:none"
                            End If
                            @<div class="col-md-6">
                                <div style="@display_block" id="next-page-left">
                                    <button type="button" class="btn btn-primary btn-sm btn-block"
                                        onclick="ChangePage('@ValidationFormulaire.Core.FormulairePosition.left',
                                            '@ValidationFormulaire.Core.DeterminePage.PageAction.NextPage')">
                                        Page suivante
                                    </button>
                                </div>
                            </div>
                        End code
                    </div>
                </div>
                <!--
                    Display right side images.
                -->
                <div class="col-md-6">
                    <div id="compared-image-right" style="display:@Session("comparing")">
                        <div id="partial-image-compared-right">
                            @Html.Partial("Partials/ApprobationImagesPartial", Nothing, New ViewDataDictionary() From { _
                                                            {"current_page", 1}, _
                                                            {"formulaire", ValidationFormulaire.Core.FormulairePosition.compared} _
                                                        })
                        </div>
                    </div>
                    <div id="non-compared-image-right" style="display:@Session("non_comparing")">
                        @For i As Integer = 1 To Session("page_number_right")
                            Dim image_id_right As String = "image_right" + i.ToString
                            Dim image_display_block_right As String
                            If (Session("current_page_right") = i) Then
                                image_display_block_right = "display:block"
                            Else
                                image_display_block_right = "display:none"
                            End If
			                @<div id="@image_id_right" style="@image_display_block_right">
                                <div id="partial-image-right">
                                @Html.Partial("Partials/ApprobationImagesPartial", Nothing, New ViewDataDictionary() From { _
                                                        {"current_page", i}, _
                                                        {"formulaire", ValidationFormulaire.Core.FormulairePosition.right} _
                                                    })
                                </div>
			                </div>
                        Next
                    </div>
                    <!--
                        Right side pagination.
                    -->
                    <div class="row">
                        @code
                            Dim display_block_right As String
                            @If (Session("current_page_right") > 1) Then
                                display_block_right = "display:block"
                            Else
                                display_block_right = "display:none"
                            End If
                            @<div class="col-md-6">
                                <div style="@display_block_right" id="previous-page-right">
                                    <button type="button" class="btn btn-primary btn-sm btn-block" 
                                        onclick="ChangePage('@ValidationFormulaire.Core.FormulairePosition.right',
                                            '@ValidationFormulaire.Core.DeterminePage.PageAction.PreviousPage')">
                                        Page précédente
                                    </button>
                                </div>
                            </div>
                            @If (Session("current_page_right") < (Session("page_number_right"))) Then
                                display_block_right = "display:block"
                            Else
                                display_block_right = "display:none"
                            End If
                            @<div class="col-md-6">
                                <div style="@display_block_right" id="next-page-right">
                                    <button type="button" class="btn btn-primary btn-sm btn-block"
                                        onclick="ChangePage('@ValidationFormulaire.Core.FormulairePosition.right',
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
    <div class="row top17">
        <!--
            Comparison threshold and button.
        -->
        <div class="col-md-12">
            <div class="text-center">
                <input id="comp-threshold" type="text" data-slider-min="0" data-slider-max="10" data-slider-step="1" data-slider-value="@Session("threshold")"/>
                <span id="comp-threshold-CurrentSliderValLabel" class="left10">Tolérance : <span id="comp-threshold-SliderVal">0</span></span>
                @If (Session("images_left") IsNot Nothing) And (Session("images_right") IsNot Nothing) Then
                    @<div class="top5">
                        <button type="submit" class="btn btn-success btn-sm btn-block active"
                            onclick = "Compare()" >
                            Comparer
                        </button>
                    </div>
                Else
                    @<div class="top5">
                        <button type="submit" class="btn btn-success btn-sm btn-block disabled">
                            Comparer
                        </button>
                    </div>
                End If
            </div>
        </div>
	</div>
    <div class="row top10"></div>
</div>

<!--
    Set javacript global variables for comparison view.
-->
<script type="text/javascript">
    var current_page_left = parseInt('@Session("current_page_left")');
    var current_page_right = parseInt('@Session("current_page_right")');
    var page_number_left = parseInt('@Session("page_number_left")');
    var page_number_right = parseInt('@Session("page_number_right")');
    var threshold_slider_comp = new Slider("#comp-threshold");

    threshold_slider_comp.on("slide", function (sliderValue) {
        document.getElementById("comp-threshold-SliderVal").textContent = sliderValue;
    });
    document.getElementById("comp-threshold-SliderVal").textContent = '@Session("threshold")';

</script>

<script type="text/javascript" src="/content/js/Approbation/slider.js"></script>
<script type="text/javascript" src="/content/js/Comparison/pagination.js"></script>
<script type="text/javascript" src="/content/js/Comparison/comparison.js"></script>