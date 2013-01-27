$(function () {
    $("input,select,textarea").focusout(checkValidity);
});

var checkValidity = function () {
    //If there is an input
    if ($.trim(this.value) !== "") {
        $(this).removeClass("input-error");
        $(this).addClass("input-success");
    }
    else {
        $(this).removeClass("input-success");
        $(this).addClass("input-error");
    }
};