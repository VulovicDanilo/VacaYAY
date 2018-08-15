$(function () {
    $(".btnModal").click(function () {
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr("data-id");
        var callback = $buttonClicked.attr("data-url");
        $(".myModelContent").html('');
        var options = {
            "backdrop": false,
            keyboard: true
        };
        $.ajax({
            type: "GET",
            url: callback,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            data: {
                "id": id
            },
            success: function (data) {
                $('#myModalContent').html(data);
                $('#myModal').modal(options);
                $('#myModal').modal('show');
            },
            error: function () {
                alert("Content load failed.");
            }
        });
    });
    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
});