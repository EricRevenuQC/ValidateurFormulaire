$("#dialog-annuler").click(function () {
    $("#dialog-confirmation").dialog('close');
    $("#dialog_input_id".concat(current_dialog)).focus()
});

$("#dialog-confirmer").click(function () {
    $("#dialog-confirmation").dialog('close');
    data_log[current_dialog].input_value = $("#dialog-text-input").text();
    NextDialog();
});

$("#dialog-confirmation").dialog({
    uiLibrary: 'bootstrap',
    resizable: false,
    draggable: false,
    autoOpen: false,
    height: "auto",
    width: "auto",
    autofocus: true,
    open: function () {
        $('.ui-widget-overlay').addClass('custom-overlay-black');
    },
    close: function () {
        $('.ui-widget-overlay').removeClass('custom-overlay-black');
    },
    modal: true
});