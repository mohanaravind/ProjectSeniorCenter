$(function () {
    $("input,select,textarea").focusout(checkValidity);
});

//Checks the validity of each of the textbox field
var checkValidity = function () {
    //Check if its not a required field
    var isOptional = !$(this).attr('required');

    //If its an optional field
    if (isOptional)
        return;

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