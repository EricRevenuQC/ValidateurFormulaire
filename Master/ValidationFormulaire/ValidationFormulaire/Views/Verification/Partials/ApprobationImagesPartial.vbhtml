<img alt="Formulaire" src="@Url.Action("GetImage", "Verification", New With { _
                Key .formulaire = Me.ViewData("formulaire"), _
                Key .current_page = Me.ViewData("current_page") _
                                                                        })" 
class="img-rounded img-responsive center-block"/> 