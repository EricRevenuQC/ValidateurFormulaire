$("#file").fileinput({
    maxFileCount: 1,
    showUpload: false,
    showPreview: false,
    language: "fr"
});

$("#file_text").fileinput({
    maxFileCount: 1,
    showUpload: false,
    showPreview: false,
    language: "fr"
});

$("#file_template").fileinput({
    maxFileCount: 1,
    showUpload: false,
    language: "fr",
    overwriteInitial: true,
    initialPreview: ['/images/VerificationTemplate.png'],
    initialPreviewAsData: true,
    initialPreviewFileType: 'image',
    initialPreviewConfig: [
        { caption: "Gabarit", width: "120px", key: 1 }
    ],
    purifyHtml: true
});

$("#file_template_text").fileinput({
    maxFileCount: 1,
    showUpload: false,
    language: "fr",
    overwriteInitial: true,
    initialPreview: ['/images/VerificationTemplate.png'],
    initialPreviewAsData: true,
    initialPreviewFileType: 'image',
    initialPreviewConfig: [
        { caption: "Gabarit", width: "120px", key: 1 }
    ],
    purifyHtml: true
});

$("#file_left").fileinput({
    maxFileCount: 1,
    showUpload: false,
    showPreview: false,
    language: "fr"
});

$("#file_right").fileinput({
    maxFileCount: 1,
    showUpload: false,
    showPreview: false,
    language: "fr"
});