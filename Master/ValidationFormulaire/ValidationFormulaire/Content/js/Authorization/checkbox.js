$('#show-template-checkbox').checkboxpicker({
    html: true,
    offLabel: '<span class="glyphicon glyphicon-remove">',
    onLabel: '<span class="glyphicon glyphicon-ok">'
});

$('#show-template-checkbox').on('change', function () {
    if (document.getElementById("template").style.display == "none") {
        document.getElementById("template").style.display = "block";
        $("#pdf-image").removeClass("col-md-12");
        $("#pdf-image").addClass("col-md-6");
    }
    else {
        document.getElementById("template").style.display = "none";
        $("#pdf-image").removeClass("col-md-6");
        $("#pdf-image").addClass("col-md-12");
    }
});