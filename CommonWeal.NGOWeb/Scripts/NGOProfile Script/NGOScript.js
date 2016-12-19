//About Us Page Script

$("#editbtn").click(function () {
    $(this).hide();
});
$(document).ready(function () {
    $("input").prop('disabled', true);
    $("#submitbtn").prop('disabled', true);
    $("#Canclebtn").hide();
    $("#submitbtn").hide();


    $("#editbtn").click(function () {
        $(this).hide();
        $("#Canclebtn").show();
        $("#submitbtn").show();
        $("input").prop('disabled', false);
        $("#submitbtn").prop('disabled', false);

    });
    $("#Canclebtn").click(function () {

        $("input").prop('disabled', true);
        $("#submitbtn").hide();
        $(this).hide();
        $("#editbtn").show();
        //$("#submitbtn").prop('disabled', true);
    });





    $("textarea").prop('disabled', true);
    $(".SaveSummaryId").hide();
    $("#Cancel").hide();
    $("#edit").click(function () {
        $(this).hide();
        $("#Cancel").show();
        $(".SaveSummaryId").show();
        $("textarea").prop('disabled', false);
    });


    $("#Cancel").click(function () {
        $("textarea").prop('disabled', true);
        $(".SaveSummaryId").hide();
        $(this).hide();
        $("#edit").show();
        //$("#submitbtn").prop('disabled', true);
    });

    $(".SaveSummaryId").live('click', function () {
        var Loginid = $(this).attr('id').split('-')[1];
        var summary = $('#textSumamry').val();
        $.post("/NGOProfile/SubmitSummary?summary=" + summary + "&Loginid=" + Loginid, function (result) {

            alert("summary saved successfully");
            $("#Cancel").hide();
            $(".SaveSummaryId").hide();
            $("#edit").show();
            //console.log(result);
        });


    });

});
$('img').on('click', function () {
    var image = $(this).attr('src');
    $('#myModal2').on('show.bs.modal', function () {
        $(".img-responsive").attr("src", image);
    });
});

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});
