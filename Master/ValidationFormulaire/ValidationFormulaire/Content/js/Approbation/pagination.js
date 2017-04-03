function ChangePage(formulaire_side, action) {
    var page_data = UpdateCurrentPage(formulaire_side, action);

    $.ajax({
        url: page_data.url,
        method: 'GET',
        cache: false,
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

    if (formulaire_side == "center") {
        if (action == "NextPage") {
            current_page += 1;
        }
        else {
            current_page -= 1;
        }
        url_action = "/VerificationFormat/VerificationFormat";
        page = current_page;
    }
    else if (formulaire_side == "text") {
        if (action == "NextPage") {
            current_page_text += 1;
        }
        else {
            current_page_text -= 1;
        }
        url_action = "/VerificationText/VerificationText";
        page = current_page_text;
    }
    else if (formulaire_side == "left") {
        if (action == "NextPage") {
            current_page_left += 1; 
        }
        else {
            current_page_left -= 1;
        }
        url_action = "/Comparison/ComparisonFormulaire";
        page = current_page_left;
    }
    else if (formulaire_side == "right") {
        if (action == "NextPage") {
            current_page_right += 1;
        }
        else {
            current_page_right -= 1;
        }
        url_action = "/Comparison/ComparisonFormulaire";
        page = current_page_right;
    }

    return {
        url: url_action,
        current_page: page
    };
}

function UpdateImages(page, formulaire_side) {
    if (formulaire_side == "center") {
        for (i = 1; i <= page_number; i++) {
            document.getElementById("image" + i).style.display = "none";
        }
        document.getElementById("image" + page).style.display = "block";
    }
    else if (formulaire_side == "text") {
        for (i = 1; i <= page_number_text; i++) {
            document.getElementById("image_text" + i).style.display = "none";
        }
        document.getElementById("image_text" + page).style.display = "block";
    }
    else {
        if (formulaire_side == "left") {
            for (i = 1; i <= page_number_left; i++) {
                document.getElementById("image_left" + i).style.display = "none";
            }
            document.getElementById("image_left" + page).style.display = "block";
        }
        else {
            for (i = 1; i <= page_number_right; i++) {
                document.getElementById("image_right" + i).style.display = "none";
            }
            document.getElementById("image_right" + page).style.display = "block";
        }
        document.getElementById("compared-image-left").style.display = "none";
        document.getElementById("non-compared-image-left").style.display = "block";
        document.getElementById("compared-image-right").style.display = "none";
        document.getElementById("non-compared-image-right").style.display = "block";
    }
}

function DisplayPaginationButtons(page, formulaire_side) {
    if (formulaire_side == "center") {
        if (page >= page_number) {
            document.getElementById("next-page").style.display = "none";
        }
        else {
            document.getElementById("next-page").style.display = "block";
        }

        if (page <= 1) {
            document.getElementById("previous-page").style.display = "none";
        }
        else {
            document.getElementById("previous-page").style.display = "block";
        }
    }
    else if (formulaire_side == "text") {
        if (page >= page_number_text) {
            document.getElementById("next-page-text").style.display = "none";
        }
        else {
            document.getElementById("next-page-text").style.display = "block";
        }

        if (page <= 1) {
            document.getElementById("previous-page-text").style.display = "none";
        }
        else {
            document.getElementById("previous-page-text").style.display = "block";
        }
    }
    else if (formulaire_side == "left") {
        if (page >= page_number_left) {
            document.getElementById("next-page-left").style.display = "none";
        }
        else {
            document.getElementById("next-page-left").style.display = "block";
        }

        if (page <= 1) {
            document.getElementById("previous-page-left").style.display = "none";
        }
        else {
            document.getElementById("previous-page-left").style.display = "block";
        }
    }
    else {
        if (page >= page_number_right) {
            document.getElementById("next-page-right").style.display = "none";
        }
        else {
            document.getElementById("next-page-right").style.display = "block";
        }

        if (page <= 1) {
            document.getElementById("previous-page-right").style.display = "none";
        }
        else {
            document.getElementById("previous-page-right").style.display = "block";
        }
    }
}