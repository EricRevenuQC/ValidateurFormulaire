﻿@Code
    Layout = "~/Views/Shared/Bootstrap.vbhtml"
End Code

<!DOCTYPE html>
<html lang="fr">
<body>
	<nav class="navbar navbar-inverse ">
		  <div class="container">
			<div class="navbar-header">
			  <a class="navbar-brand" href="/verification/verificationformat">Validateur de formulaire</a>
				<ul class="nav navbar-nav">
					@code
						Dim approbation_active As String = ""
						Dim authorization_active As String = ""
				
						@If Request.Url.AbsoluteUri.Contains("/VerificationFormat/") OrElse
							Request.Url.AbsoluteUri.Contains("/VerificationText/") OrElse
							Request.Url.AbsoluteUri.Contains("/Comparison/") Then
						approbation_active = "active"
						ElseIf Request.Url.AbsoluteUri.Contains("/Authorization/") Then
						authorization_active = "active"
						End If
						@<li class="nav-item @approbation_active">
							<a class="nav-link" href="/VerificationFormat/VerificationFormat">Approbation<span class="sr-only">(current)</span></a>
						</li>
						@<li class="nav-item">
							<a class="nav-link" href="#">CAB</a>
						</li>
						@<li class="nav-item @authorization_active">
							<a class="nav-link" href="/Authorization/ValidationDonnees">Autorisation</a>
						</li>
						@<li class="nav-item">
							<a class="nav-link" href="#">Homologation</a>
						</li>
					End code
				</ul>
			</div>
		</div>
	</nav>
     @RenderBody()
</body>
</html>
    
       