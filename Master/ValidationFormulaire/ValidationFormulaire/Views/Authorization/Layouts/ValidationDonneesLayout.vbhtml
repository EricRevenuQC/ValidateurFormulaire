@ModelType ValidationFormulaire.Models.AuthorizationModel

@Code  
    ViewData("Title") = "ValidationLayout"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="container-fluid">
	<div class="row">
		<div class="col-md-12">
            <div class="row top10">
                <div class="col-md-12">
                    <div class="panel-group" id="panel-2">
				        <div class="panel panel-default">
					        <div class="panel-heading">
						        <a class="panel-title collapsed" data-toggle="collapse" data-parent="#panel-2" href="#panel-element-2">
                                    Ajouter un relevé
						        </a>
					        </div>
					        <div id="panel-element-2" class="panel-collapse collapse">
						        <div class="panel-body">
							        <form role="form" id="valider-form" method="post" enctype="multipart/form-data">
                                        <div class="col-md-12">
                                            <input id="file" name="file" multiple type="file" class="file-loading">
                                        </div>
                                        <div class="col-md-12 text-center top10">
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

<script type="text/javascript" src="/content/js/Approbation/fileinput.js"></script>


