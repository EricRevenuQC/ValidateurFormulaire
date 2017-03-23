dialog = $("#dialog_id0").dialog({
    uiLibrary: 'bootstrap',
    autoOpen: true,
    resizable: false,
    draggable: false,
    height: "auto",
    width: 600,
    position: {
        my: "center",
        at: "bottom",
        of: window
    },
    open: function () {
        $('.ui-widget-overlay').addClass('custom-overlay');
        $('.ui-dialog').addClass('fixed-dialog');
    },
    close: function () {
        $('.ui-widget-overlay').removeClass('custom-overlay');
    },
    autofocus: true
});

for (i = 0; i < data_count; i++) {
    $("#dialog_annuler_id".concat(i)).click(function () {
        $("#dialog_id".concat(current_dialog)).dialog('close');
        document.getElementById("dialog-bottom-space").style.display = "none";
        current_dialog = 0;
    });
    $("#dialog_precedent_id".concat(i)).click(function () {
        PreviousDialog();
    });
    $("#dialog_valider_id".concat(i)).click(function () {
        ValidateValue();
    });
    $("#dialog_passer_id".concat(i)).click(function () {
        NextDialog();
    });
}

for (i = 0; i < data_count; i++) {
    $("#dialog_input_id".concat(i)).keypress(keypressHandler);
}

function keypressHandler(e) {
    if (e.which == 13) { //13 is the enter key.
        e.preventDefault();
        $(this).blur();
        $("#dialog_valider_id".concat(current_dialog)).focus().click();
    }
}

function ValidateValue() {
    var data_index = 0;

    $.each(collection, function (key, value) {
        if (data_index === current_dialog) {
            var bar_code_value = value.value;

            if (bar_code_value === $("#dialog_input_id".concat(current_dialog)).val()) {
                delete data_log[current_dialog];
                NextDialog();
                return false
            }
            else {
                $("#dialog-confirmation").dialog('open');
                $("#dialog-text-input").text($("#dialog_input_id".concat(current_dialog)).val());
                $("#dialog-text-barcode").text(bar_code_value);
                $("#dialog-confirmer").focus()
                return false

            }
        }
        data_index += 1;
    });
}

function NextDialog() {
    var height = $("#dialog_id".concat(current_dialog)).closest('.ui-dialog').outerHeight();
    var width = $("#dialog_id".concat(current_dialog)).closest('.ui-dialog').outerWidth();
    var offset = $("#dialog_id".concat(current_dialog)).closest('.ui-dialog').offset();
    var last_position = "left+".concat(offset.left).concat(" ").concat("top+").concat(
        offset.top - (document.documentElement.scrollTop || document.body.scrollTop));
    if ($("#dialog_id".concat(current_dialog + 1)).hasClass('ui-dialog-content')) {
        $("#dialog_id".concat(current_dialog + 1)).dialog({
            position: {
                my: "left top",
                at: last_position
            }
        });
        $("#dialog_id".concat(current_dialog + 1)).dialog("option", "height", height);
        $("#dialog_id".concat(current_dialog + 1)).dialog("option", "width", width);
    }
    else {
        last_position = "left+".concat(offset.left).concat(" ").concat("top+").concat(
        offset.top - (document.documentElement.scrollTop || document.body.scrollTop) * 2);
        $("#dialog_id".concat(current_dialog + 1)).dialog({
            uiLibrary: 'bootstrap',
            resizable: false,
            draggable: false,
            autoOpen: false,
            height: height,
            width: width,
            position: {
                my: "left top",
                at: last_position,
                of: window
            },
            open: function () {
                $('.ui-widget-overlay').addClass('custom-overlay');
                $('.ui-dialog').addClass('fixed-dialog');
            },
            close: function () {
                $('.ui-widget-overlay').removeClass('custom-overlay');
            },
            autofocus: true
        });
    }
    $("#dialog_id".concat(current_dialog)).dialog('close');

    var current_dialog_on_current_page = current_dialog;
    if (current_page_releve_donnees > 1) {
        current_dialog_on_current_page = current_dialog - dialog_per_page[current_page_releve_donnees - 1]
    }

    if (current_dialog_on_current_page < dialog_per_page[current_page_releve_donnees] - 1) {
        $("#dialog_id".concat(current_dialog + 1)).dialog('open');
        $("#dialog_page_text".concat(current_dialog + 1)).text("Page " + current_page_releve_donnees);
        current_dialog += 1;
    }
    else if (current_page_releve_donnees < page_number_releve_donnees) {
        alert("Prochaine page");
        current_dialog += 1;
        $("#dialog_id".concat(current_dialog)).dialog({
            position: {
                my: "left top",
                at: last_position,
                of: window
            }
        });
        $("#dialog_id".concat(current_dialog)).dialog("option", "height", height);
        $("#dialog_id".concat(current_dialog)).dialog("option", "width", width);
        $("#dialog_id".concat(current_dialog)).dialog('open');
        ChangePage(formulaire_position, page_action);
        $("#dialog_page_text".concat(current_dialog)).text("Page " + current_page_releve_donnees);
    }
    else {
        alert("Terminer");
        var text = "";
        var current_page = 1;
        var index_on_page = 0

        text += "---------------------------------------------\r\n";
        text = text + "Page " + current_page;
        text += "\r\n---------------------------------------------\r\n\r\n";

        for (var i = 0; i < data_log.length; i++) {
            if (index_on_page == dialog_per_page[current_page]) {
                current_page += 1;
                index_on_page = 0;
                text += "\r\n---------------------------------------------\r\n";
                text = text + "Page " + current_page;
                text += "\r\n---------------------------------------------\r\n\r\n";
            }
            if (typeof data_log[i] != 'undefined') {
                text = text + "Description : " + data_log[i].description + "\r\n";
                text = text + "Code bar : " + data_log[i].bar_code_value + "\r\n";
                text = text + "Saisie : " + data_log[i].input_value + "\r\n\r\n";
            }
            index_on_page += 1;
        }
        current_dialog = 0;
        document.getElementById("dialog-bottom-space").style.display = "none";
        SaveFile("Rapport.txt", text);
    }
}

function PreviousDialog() {
    if (current_dialog > 0) {
        var height = $("#dialog_id".concat(current_dialog)).closest('.ui-dialog').outerHeight();
        var width = $("#dialog_id".concat(current_dialog)).closest('.ui-dialog').outerWidth();
        var offset = $("#dialog_id".concat(current_dialog)).closest('.ui-dialog').offset();
        var last_position = "left+".concat(offset.left).concat(" ").concat("top+").concat(
            offset.top - (document.documentElement.scrollTop || document.body.scrollTop));

        $("#dialog_id".concat(current_dialog - 1)).dialog({
            position: {
                my: "left top",
                at: last_position,
                of: window
            }
        });
        $("#dialog_id".concat(current_dialog - 1)).dialog("option", "height", height);
        $("#dialog_id".concat(current_dialog - 1)).dialog("option", "width", width);
        $("#dialog_id".concat(current_dialog)).dialog('close');
        $("#dialog_id".concat(current_dialog - 1)).dialog('open');
        $("#dialog_page_text".concat(current_dialog - 1)).text("Page " + current_page_releve_donnees);
			
        current_dialog -= 1;
    }
}

function ChangePageWithDialog(formulaire_side, action) {
    ChangePage(formulaire_side, action);
    $("#dialog_page_text".concat(current_dialog)).text("Page " + current_page_releve_donnees);
}