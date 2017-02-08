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



//Index Script
//$(document).ready(function () {
$(document).ready(function () {

    $("#country").change(function () {
        $("#State").empty();
        console.log("Hit");
        $.ajax({
            type: 'POST',

            url: '@Url.Action("GetJsonState", "Home")',
            dataType: 'json',

            data: { id: $("#country").val() },


            success: function (states) {
               
                $.each(states, function (i, state) {
                    
                    $("#State").val("");
                    $("#State").append('<option value="' + state.Value + '">' +
                         state.Text + '</option>');

                });
            },
            error: function (ex) {
                alert('Failed to retrieve country states.' + ex);
            }
        });
        return false;
    });

    $("#State").change(function () {
        $("#city").empty();
        
        $.ajax({
            type: 'POST',

            url: '@Url.Action("GetJsonCity", "Home")',
            dataType: 'json',

            data: { id: $("#State").val() },


            success: function (cities) {
                

                $.each(cities, function (i, city) {
                    
                    $("#city").val("");
                    $("#city").append('<option value="' + city.Value + '">' +
                         city.Text + '</option>');

                });
            },
            error: function (ex) {
                alert('Failed to retrieve country cities.' + ex);
            }
        });
        return false;
    });
});

