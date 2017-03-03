@ModelType ValidationFormulaire.Models.AuthorizationModel

@code
	Dim dict_operations As New ValidationFormulaire.Core.DictionaryOperations()
End code

<style>
	.crop {
		overflow: hidden;
		white-space: nowrap;
		text-overflow: ellipsis;
		width: 110%;
	}
	.nopadding {
	   padding: 1px !important;
	   margin-left: 0 !important;
	}
	.tinypadding {
	   padding: 5px !important;
	   margin-left: 0 !important;
	}
</style>

<div class="container-fluid">
    <div class="row top10">
		<div class="col-md-12">      
			<div class="row top10">
				<div class="col-md-2">
				</div>
				<div class="col-md-2">
					<div style="font-size:10px" class="btn-block crop">Année</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtAnnee, Model.bar_code_data)">
				</div>
				<div class="col-md-3">
					<div style="font-size:10px" class="btn-block crop">Code du relevé</div>
					<input type="text" name=""  readonly="true" class="btn-block" size="3"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtCodeReleve, Model.bar_code_data)">
				</div>
				<div class="col-md-2" style="width: 20%">
					<div style="font-size:10px" class="btn-block crop">No du dernier relevé</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtDernierNum, Model.bar_code_data)">
				</div>
				<div class="col-md-2" style="width: 21.5%">
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtNumOriginal, Model.bar_code_data)">
					<div class="top5">
						<input type="text" name=""  readonly="true" class="btn-block"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtNumPreImprimer, Model.bar_code_data)">
					</div>
				</div>
			</div>
			<div class="row top10">
				<div style="margin-left:12px; margin-right:12px">
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">A-Revenus d'emploi</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtA, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">B-Cotisation au RRQ</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtB, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">C-Cotisation à A.E</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtC, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">D-Cotisation à un RPA</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtD, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">E-Impot du Québec retenu</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtE, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">F-Cotisation syndicale</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtF, Model.bar_code_data)">
					</div>
				</div>
			</div>
			<div class="row top10">
				<div style="margin-left:12px; margin-right:12px">
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">G-Salaire adm au RRQ</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtG, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">H-Cotisation au RQAP</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtH, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">I-Salaire adm au RQAP</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtI, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">J-Régime privé d'ass maladie</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtJ, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">K-Voyages (rg éloignée)</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtK, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">L-Autres avantages</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtL, Model.bar_code_data)">
					</div>
				</div>
			</div>
			<div class="row top10">
				<div style="margin-left:12px; margin-right:12px">
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">M-Commissions</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtM, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">N-Dons de bienfaisance</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtN, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">O-Autres revenus</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtO, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">P-Régime d'ass interentreprises</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtP, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">Q-Salaires différés</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtQ, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">R-Revenu situé dans une réserve</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtR, Model.bar_code_data)">
					</div>
				</div>
			</div>
			<div class="row top10 ">
				<div style="margin-left:12px; margin-right:12px">
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">S-Pourboire reçus</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtS, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">T-Pourboire attribués</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtT, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">U-Retraite progressive</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtU, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">V-Nourriture et logement</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtV, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">W-Véhicule à moteur</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtW, Model.bar_code_data)">
					</div>
					<div class="col-md-2 tinypadding">
						<div style="font-size:10px" class="btn-block crop">Code (case O)</div>
						<input type="text" name=""  readonly="true" class="btn-block text-right"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtCodeO, Model.bar_code_data)">
					</div>    
				</div>         
			</div>
			<div class="row top10 ">
				<div style="font-size:10px" class="btn-block crop left15">Renseignements complénemtaires</div>
			</div>
			<div class="row">
				<div style="margin-left:12px; margin-right:12px">
					<div class="col-md-1 nopadding">
						<div style="font-size:10px" class="btn-block crop"></div>
						<input type="text" name=""  readonly="true" class="btn-block"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtCode1, Model.bar_code_data)">
					</div> 
					<div class="col-md-2 nopadding">
						<div style="font-size:10px" class="btn-block crop"></div>
						<input type="text" name=""  readonly="true" class="btn-block"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtInfo1, Model.bar_code_data)">
					</div>
					<div class="col-md-1 nopadding">
						<div style="font-size:10px" class="btn-block crop"></div>
						<input type="text" name=""  readonly="true" class="btn-block"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtCode2, Model.bar_code_data)">
					</div> 
					<div class="col-md-2 nopadding">
						<div style="font-size:10px" class="btn-block crop"></div>
						<input type="text" name=""  readonly="true" class="btn-block"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtInfo2, Model.bar_code_data)">
					</div>
					<div class="col-md-1 nopadding">
						<div style="font-size:10px" class="btn-block crop"></div>
						<input type="text" name=""  readonly="true" class="btn-block"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtCode3, Model.bar_code_data)">
					</div> 
					<div class="col-md-2 nopadding">
						<div style="font-size:10px" class="btn-block crop"></div>
						<input type="text" name=""  readonly="true" class="btn-block"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtInfo3, Model.bar_code_data)">
					</div>
					<div class="col-md-1 nopadding">
						<div style="font-size:10px" class="btn-block crop"></div>
						<input type="text" name=""  readonly="true" class="btn-block"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtCode4, Model.bar_code_data)">
					</div> 
					<div class="col-md-2 nopadding">
						<div style="font-size:10px" class="btn-block crop"></div>
						<input type="text" name=""  readonly="true" class="btn-block"
						value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtInfo4, Model.bar_code_data)">
					</div>  
				</div>
			</div>
			<div class="row top10 ">
				<div class="col-md-4">
					<div style="font-size:10px" class="btn-block crop">Nom de famille</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtNomFamille, Model.bar_code_data)">
				</div> 
				<div class="col-md-4">
					<div style="font-size:10px" class="btn-block crop">Numéro d'assurance social</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtNas, Model.bar_code_data)">
				</div>
				<div class="col-md-3">
					<div style="font-size:10px" class="btn-block crop">Numéro de référence</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtNumRef, Model.bar_code_data)">
					</div> 
			</div>
			<div class="row top10 ">
				<div class="col-md-4">
					<div style="font-size:10px" class="btn-block crop">Prénom</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtPrenom, Model.bar_code_data)">
				</div> 
				<div class="col-md-8">
					<div style="font-size:10px" class="btn-block crop">Nom de l'employeur</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtNomEmployeur, Model.bar_code_data)">
				</div>
			</div>
			<div class="row top10 ">
				<div class="col-md-4">
					<div style="font-size:10px" class="btn-block crop">Adresse</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtAdresse, Model.bar_code_data)">
				</div> 
				<div class="col-md-8">
					<div style="font-size:10px" class="btn-block crop"><br></div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtNomEmployeur2, Model.bar_code_data)">
				</div>
			</div>
			<div class="row top10 ">
				<div class="col-md-4">
					<div style="font-size:10px" class="btn-block crop"><br></div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtAdresse2, Model.bar_code_data)">
				</div> 
				<div class="col-md-8">
					<div style="font-size:10px" class="btn-block crop">Adresse de l'employeur</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtAdresseEmployeur, Model.bar_code_data)">
				</div>
			</div>
			<div class="row top10 ">
				<div class="col-md-4">
					<div style="font-size:10px" class="btn-block crop">Ville, village ou municipalité</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtVille, Model.bar_code_data)">
				</div> 
				<div class="col-md-8">
					<div style="font-size:10px" class="btn-block crop"><br></div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtAdresseEmployeur2, Model.bar_code_data)">
				</div>
			</div>
			<div class="row top10 ">
				<div class="col-md-2">
					<div style="font-size:10px" class="btn-block crop">Province</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtProv, Model.bar_code_data)">
				</div> 
				<div class="col-md-2">
					<div style="font-size:10px" class="btn-block crop">Code postal</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtCodePostal, Model.bar_code_data)">
				</div>
				<div class="col-md-8">
					<div style="font-size:10px" class="btn-block crop">ville, village ou municipalité</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtVilleEmployeur, Model.bar_code_data)">
				</div>
			</div>
			<div class="row top10 ">
				<div class="col-md-2">
					<div style="font-size:10px" class="btn-block crop">Code ID formulaire</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtCodeGabrt, Model.bar_code_data)">
				</div>
				<div class="col-md-2">
					<div style="font-size:10px" class="btn-block crop">Numéro de validation</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtNumLogiciel, Model.bar_code_data)">
				</div> 
				<div class="col-md-2">
					<div style="font-size:10px" class="btn-block crop">Province</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtProvEmployeur, Model.bar_code_data)">
				</div>
				<div class="col-md-2">
					<div style="font-size:10px" class="btn-block crop">Code postal</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtCodePostalEmployeur, Model.bar_code_data)">
				</div>
			</div>
			<div class="row top10 ">
				<div class="col-md-3">
					<div style="font-size:10px" class="btn-block crop">Nunéro préparateur</div>
					<input type="text" name=""  readonly="true" class="btn-block"
					value="@dict_operations.GetValueOfKeyAsString(ValidationFormulaire.Core.BarCodeProperties.txtNumPreparateur, Model.bar_code_data)">
				</div>   
			</div>
		</div> 
	</div>
</div>




















