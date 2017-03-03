$("#file").fileinput({
    maxFileCount: 1,
    showUpload: false,
    language: "fr",
    allowedFileExtensions: ["pdf"]
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
        { caption: "Gabarit",width: "120px", key: 1 }
    ],
    purifyHtml: true,
    allowedFileExtensions: ["pdf"]
});

$("#file_left").fileinput({
    maxFileCount: 1,
    showUpload: false,
    language: "fr",
    allowedFileExtensions: ["pdf"]
});

$("#file_right").fileinput({
    maxFileCount: 1,
    showUpload: false,
    language: "fr",
    allowedFileExtensions: ["pdf"]
});