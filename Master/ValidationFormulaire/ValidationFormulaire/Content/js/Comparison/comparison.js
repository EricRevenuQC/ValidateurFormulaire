function Compare() {
    var threshold_value = threshold_slider_comp.getValue();
    $.ajax({
        url: "/comparison/ComparisonFormulaire",
        method: 'GET',
        data: {
            page_action: "compare",
            threshold: threshold_value
        },
        success: function (data) { }
    });
    document.getElementById("compared-image-left").style.display = "none";
    document.getElementById("non-compared-image-left").style.display = "none";
    document.getElementById("compared-image-right").style.display = "none";
    document.getElementById("non-compared-image-right").style.display = "none";

    window.location.href = window.location.href;
}