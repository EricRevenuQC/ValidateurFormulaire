function ChangePage(formulaire_side, action) {
    var page_data = UpdateCurrentPage(formulaire_side, action);

    $.ajax({
        url: page_data.url,
        method: 'GET',
        data: {
            page_action: action,
            formulaire: formulaire_side
        },
        success: function (data) { }
    });
    UpdateImages(page_data.current_page, formulaire_side);
    DisplayPaginationButtons(page_data.current_page, formulaire_side);
}

function UpdateCurrentPage(formulaire_side, action) {
    var url_action;
    var page;

    if (action == "NextPage") {
        current_page_releve_donnees += 1;
    }
    else {
        current_page_releve_donnees -= 1;
    }
    url_action = "/Authorization/ValidationDonnees";
    page = current_page_releve_donnees;

    return {
        url: url_action,
        current_page: page
    };
}

function UpdateImages(page, formulaire_side) {
    for (i = 1; i <= page_number_releve_donnees; i++) {
        document.getElementById("image_releve_donnees" + i).style.display = "none";
    }
    document.getElementById("image_releve_donnees" + page).style.display = "block";
}

function DisplayPaginationButtons(page, formulaire_side) {
    if (page >= page_number_releve_donnees) {
        document.getElementById("next-page-releve-donnees").style.display = "none";
    }
    else {
        document.getElementById("next-page-releve-donnees").style.display = "block";
    }

    if (page <= 1) {
        document.getElementById("previous-page-releve-donnees").style.display = "none";
    }
    else {
        document.getElementById("previous-page-releve-donnees").style.display = "block";
    }
}