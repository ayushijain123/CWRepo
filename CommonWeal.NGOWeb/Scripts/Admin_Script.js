//Admin script
//For removing accept and decline

$(document).ready(function () {
    $(".accept").click(function () {
        $(this).parent().remove();
    });
});
$(document).ready(function () {
    $(".decline").click(function () {
        $(this).parent().remove();
    });
});
