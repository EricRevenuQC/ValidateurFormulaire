@Code
    Layout = "~/Views/Shared/Bootstrap.vbhtml"
End Code

<!DOCTYPE html>
<html lang="fr">
    <body>
<nav class="navbar navbar-inverse ">
      <div class="container">
        <div class="navbar-header">
          <a class="navbar-brand" href="#">Validateur de formulaire</a>
        </div>
        <div id="navbar" class="collapse navbar-collapse">
          <ul class="nav navbar-nav">
             <li><a href="#Approbation">Approbation</a></li>
            <li><a href="#CAB">CAB</a></li>
              <li ><a href="#">Autorisation</a></li>
            <li><a href="#Homolagation">Homologation</a></li>
          </ul>
        </div>
      </div>
    </nav>
     @RenderBody()
</body>
</html>