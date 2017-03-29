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
					@code
						Dim verification_format As String = ""
						Dim comparison As String = ""
						Dim verification_text As String = ""
				
						@If Request.Url.AbsoluteUri.Contains("/VerificationFormat") Then
						verification_format = "active"
						ElseIf Request.Url.AbsoluteUri.Contains("/ComparisonFormulaire") Then
						comparison = "active"
						ElseIf Request.Url.AbsoluteUri.Contains("/VerificationText") Then
						verification_text = "active"
						End If
						@<li class="@verification_format">
							<a href="~/Verification/VerificationFormat">Vérification de formulaire</a>
						</li>
						@<li class="@comparison">
							<a href="~/Comparison/ComparisonFormulaire">Comparaison de formulaire</a>
						</li>
						@<li class="@verification_text">
							<a href="~/Verification/VerificationText">Vérification du text</a>
						</li>
					End code
				</ul>
				@RenderBody()
			</div>
		</div>
	</div>
</div>


