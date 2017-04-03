@ModelType ValidationFormulaire.Models.ApprobationModel

@Code  
    ViewData("Title") = "VerificationTextLayout"
    Layout = "~/Views/Approbation/Layouts/ApprobationLayout.vbhtml"
End Code

<div class="tab-content">
	<div class="tab-pane active" id="panel-302499">
        <div class="row top10">
            <div class="col-md-12">
                <div class="panel-group" id="panel-3">
				    <div class="panel panel-default">
					    <div class="panel-heading">
						        <a class="panel-title collapsed" data-toggle="collapse" data-parent="#panel-3" href="#panel-element-3">
                                    Ajouter des fichiers
						        </a>
					    </div>
					    <div id="panel-element-3" class="panel-collapse collapse">
						    <div class="panel-body">
							    <form role="form" id="valider-text-form" method="post" enctype="multipart/form-data">
                                    <div class="col-md-6">
                                        <input id="file_text" name="image_file_text" multiple type="file" class="file-loading">
                                    </div>
                                    <div class="col-md-6">
                                        <input id="file_template_text" name="image_file_template_text" multiple type="file" class="file-loading">
                                    </div>
                                    <div class="col-md-12 text-center top10">
                                        <div class="top10">
                                            <button id="valider_text" value="Valider" class="btn btn-success btn-block">
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

<script type="text/javascript" src="/content/js/Approbation/fileinput.js"></script>
