function onSubmit(form, postURL) {

    $.validator.unobtrusive.parse(form);

    $(form).validate();
    if ($(form).valid()) {

        var form_data = new FormData($(form)[0]);

        $.ajax({
            type: 'POST',
            url: postURL,
            processData: false,
            contentType: false,
            async: false,
            cache: false,
            data: form_data,
            success: function (msg) {
                if (msg !== "OK") {
                    console.log(msg.data);
                    $("#errorMessage").text(msg).show();
                }
                else {
                    $('#myModal').modal('hide');
                    location.reload();
                }
            }
        });
    }
}