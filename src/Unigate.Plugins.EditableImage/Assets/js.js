
$(document).ready(function () {
    $(".filebrowser").click(function (event) {

        var $code = $(this).data("code");
        var $img = $(this);

        var finder = new CKFinder();
        finder.BasePath = '~/areas/unigate/Static/ckeditor/ckfinder';
        finder.selectActionFunction = function (fileUrl, data) {
            SelectFile(fileUrl, data, $code, $img);
        }

        finder.popup();
    });
})

function SelectFile(fileUrl, data, imagecode, img) {

    $.ajax({
        url: "/plugins/Unigate.Plugins.EditableImage/EditableImage/Save",
        type: "POST",
        data: { "code": imagecode, "imageFile": fileUrl },
        success: function (result) {
            var code = img.attr('data-code');
            $("[data-code='" + code + "']").next().attr('src', fileUrl);

        },
        beforeSend: function () {

        }
    });
}
