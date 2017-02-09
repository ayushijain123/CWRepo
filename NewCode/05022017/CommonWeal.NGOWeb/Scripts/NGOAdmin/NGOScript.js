$(document).ready(function () {
    $(".clickProfile").click(function () {
        $("#profilefile").click();

    });
    $("#editbtn").click(function () {
        $(this).hide();
    });

    /*about us hide and show*/

    //Profile Image Upload hide and show

    $("#profilefile").hide();
    $("#uploadprofile").hide();
    $("#CancelImage").hide();

    $("#profileimage").click(function () {
        $("#profilefile").show();
        $("#uploadprofile").show();
        $("#CancelImage").show();
    });

    $("#uploadprofile").click(function () {
        $("#profilefile").hide();
        $("#uploadprofile").hide();
        $("#CancelImage").hide();
    });

    $("#CancelImage").click(function () {
        $("#profilefile").hide();
        $("#uploadprofile").hide();
        $("#CancelImage").hide();
    });

    $(".uploadprofile").click(function () {
        if (window.FormData !== undefined) {
            var fileUpload = $("#profilefile").get(0);
            var files = fileUpload.files;
            // Create FormData object  
            var fileData = new FormData();
            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }
            // Adding one more key to FormData object  
            fileData.append('username', 'manas');
            $.ajax({
                url: '/NGOProfile/PostImage',
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    console.log(result[0]);
                    $(".profileimage").attr('src', '/NGOHome/GetImage?imagePath=' + result[0]);
                    $("#profileimage1").attr('src', '/NGOHome/GetImage?imagePath=' + result[0])
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {
            alert("FormData is not supported.");
        }
    });

    $('.fieldchange').focus(function () {
        $("#submitbtn").prop('disabled', false);
    });

    $('#textSumamry').focus(function () {
        $(".SaveSummaryId").prop('disabled', false);
    });


    var email = "a";
    var mobile = "b";
    var NgoAdress = "c";
    var Ngoname = "d";
    var chairman = "e";
    var textSumamry = "f"
    $(".fieldchange").prop('disabled', true);
    $("#uploadprofile").prop('disabled', false);
    $("#profilefile").prop('disabled', false);
    $("#submitbtn").prop('disabled', true);
    $("#Canclebtn").hide();
    $("#submitbtn").hide();
    $("#editbtn").click(function () {
        email = $("#email").val();
        mobile = $("#mobile").val();
        NgoAdress = $("#NgoAdress").val();
        Ngoname = $("#Ngoname").val();
        chairman = $("#chairman").val();
        $(this).hide();
        $("#Canclebtn").show();
        $("#submitbtn").show();
        $(".fieldchange").prop('disabled', false);
        $("#submitbtn").prop('disabled', true);
    });
    $("#Canclebtn").click(function () {
        $("#email").val(email);
        $("#mobile").val(mobile);
        $("#NgoAdress").val(NgoAdress);
        $("#Ngoname").val(Ngoname);
        $("#chairman").val(chairman);
        $(".fieldchange").prop('disabled', true);
        $("#submitbtn").hide();
        $(this).hide();
        $("#editbtn").show();
        //$("#submitbtn").prop('disabled', true);
    });


    $("textarea").prop('disabled', true);
    $(".SaveSummaryId").hide();
    $("#Cancel").hide();
    $("#edit").click(function () {
        summary = $("#textSumamry").val();
        $(this).hide();
        $("#Cancel").show();
        $(".SaveSummaryId").prop('disabled', true);
        $(".SaveSummaryId").show();
        $("textarea").prop('disabled', false);
    });


    $("#Cancel").click(function () {
        $("#textSumamry").val(summary);
        $("textarea").prop('disabled', true);
        $(".SaveSummaryId").hide();
        $(this).hide();
        $("#edit").show();
        //$("#submitbtn").prop('disabled', true);
    });

    $(document).on('click', ".SaveSummaryId", function () {
        $("textarea").prop('disabled', true);
        var Loginid = $(this).attr('id').split('-')[1];
        var summary = $('#textSumamry').val();
        // alert("hi")
        $.post("/NGOProfile/SubmitSummary?summary=" + summary + "&Loginid=" + Loginid, function (result) {
            alert("Saved successfully");
            if (result != null) {
                $("#textSumamry").html(result);
            }
            $("#Cancel").hide();
            $(".SaveSummaryId").hide();
            $("#edit").show();
            //console.log(result);
        });
    });
    /*ajax for editing information of ngo*/
    $(document).on('click', "#submitbtn", function () {
        // console.log('hello');
        var NgoEmail = $("#email").val();
        var NgoMobile = $("#mobile").val();
        var address = $("#NgoAdress").val();
        var Name = $("#Ngoname").val();
        var Chairman_name = $("#chairman").val();
        $.post("/NGOProfile/edit?NGOEmailID=" + NgoEmail + "&Mobile=" + NgoMobile + "&NGOAddress=" + address + "&NGOName=" + Name + "&ChairmanName=" + Chairman_name, function (result1) {
            if (result1 == true) {
                $(".fieldchange").prop('disabled', true);
                $("#Canclebtn").hide();
                $("#submitbtn").hide();
                $("#editbtn").show();
                // alert("Successfully updated");
            }
        });
    });
    $('img').on('click', function () {
        var image = $(this).attr('src');
        $('#myModal2').on('show.bs.modal', function () {
            $(".img-responsive").attr("src", image);
        });
    });
    $('[data-toggle="tooltip"]').tooltip();
});



