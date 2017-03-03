@ModelType ValidationFormulaire.Models.ApprobationModel

@Code  
    ViewData("Title") = "ApprobationLayout"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="container-fluid">
	<div class="row">
		<div class="col-md-12">
			<div class="tabbable" id="tabs-924845">
				<ul class="nav nav-tabs">
					<li class="active">
						<a href="~/Verification/VerificationFormat">Vérification de formulaire</a>
					</li>
					<li>
						<a href="~/Comparison/Comparisonformulaire">Comparaison de formulaire</a>
					</li>
				</ul>
				<div class="tab-content">
					<div class="tab-pane active" id="panel-302498">
                        <div class="row top10">
                            <div class="col-md-12">
                                <div class="panel-group" id="panel-1">
				                    <div class="panel panel-default">
					                    <div class="panel-heading">
						                        <a class="panel-title collapsed" data-toggle="collapse" data-parent="#panel-1" href="#panel-element-1">
                                                    Ajouter des fichiers
						                        </a>
					                    </div>
					                    <div id="panel-element-1" class="panel-collapse collapse">
						                    <div class="panel-body">
							                    <form role="form" id="valider-form" method="post" enctype="multipart/form-data">
                                                    <div class="col-md-6">
                                                        <input id="file" name="image_file" multiple type="file" class="file-loading">
                                                    </div>
                                                    <div class="col-md-6">
                                                        <input id="file_template" name="image_file_template" multiple type="file" class="file-loading">
                                                    </div>
                                                    <div class="col-md-12 text-center top10">
                                                        <input id="verif-threshold" name="verif_threshold" type="text" data-slider-min="0" data-slider-max="10" data-slider-step="1" data-slider-value="0"/>
                                                        <span id="verif-threshold-CurrentSliderValLabel" class="left10">Tolérance : <span id="verif-threshold-SliderVal">0</span></span>
                                                        <div class="top10">
                                                            <button id="valider" value="Valider" class="btn btn-success btn-block">
                                                                Valider
                                                            </button>
                                                        </div>
                                                    </div>
                                                </form>
						                    </div>
					                    </div>
                                    </div>
			                    </div>
		                    </div>
                        </div>
						@RenderBody()
					</div>
				</div>
			</div>
		</div>
	</div>
</div>

<script type="text/javascript">
    var threshold_slider_verif = new Slider("#verif-threshold");
</script>

<script type="text/javascript" src="/content/js/Approbation/slider.js"></script>
<script type="text/javascript" src="/content/js/Approbation/fileinput.js"></script>


