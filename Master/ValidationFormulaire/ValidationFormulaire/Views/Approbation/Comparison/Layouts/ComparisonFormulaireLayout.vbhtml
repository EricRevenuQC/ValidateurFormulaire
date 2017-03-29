@ModelType ValidationFormulaire.Models.ApprobationModel

@Code  
    ViewData("Title") = "ApprobationModelLayout"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="container-fluid">
	<div class="row">
		<div class="col-md-12">
			<div class="tabbable" id="tabs-924845">
				<ul class="nav nav-tabs">
					<li>
						<a href="~/Verification/Verificationformat">Vérification de formulaire</a>
					</li>
					<li class="active">
						<a href="~/Comparison/Comparisonformulaire">Comparaison de formulaire</a>
					</li>
				</ul>
				<div class="tab-content">
					<div class="tab-pane active" id="panel-473139">
                        <div class="row top10">
                            <div class="col-md-12">
                                <div class="panel-group" id="panel-522491">
				                    <div class="panel panel-default">
					                    <div class="panel-heading">
						                        <a class="panel-title collapsed" data-toggle="collapse" data-parent="#panel-522491" href="#panel-element-992683">
                                                    Ajouter des fichiers
						                        </a>
					                    </div>
					                    <div id="panel-element-992683" class="panel-collapse collapse">
						                    <div class="panel-body">
							                    <form role="form" method="post" enctype="multipart/form-data">
                                                    <div class="col-md-6">
                                                        <input id="file_left" name="file_left" multiple type="file" class="file-loading">
                                                    </div>
                                                    <div class="col-md-6">
                                                        <input id="file_right" name="file_right" multiple type="file" class="file-loading">
                                                    </div>
                                                    <div class="col-md-12 text-center top10">
                                                        <input type="submit" id="valider" value="Ajouter" class="btn btn-success btn-block"/>
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

<script type="text/javascript" src="/content/js/Approbation/fileinput.js"></script>