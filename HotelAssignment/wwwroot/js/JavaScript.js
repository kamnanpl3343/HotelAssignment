$(function () {
    $('#chooseImg').on('change', function (e) {
        var url = $('#chooseImg').val();
        var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
        if (this.files && this.files[0] && (ext == "gif" || ext == "jpg" || ext == "jfif" || ext == "png" || ext == "bmp")) {
            var reader = new FileReader();
            reader.onload = function (e) {
                var output = document.getElementById('imgPreview');
                output.src = e.target.result;
            };
            reader.readAsDataURL(this.files[0]);
        }
    });
});
