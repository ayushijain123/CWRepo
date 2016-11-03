

function fileclick() {
    $("#file").click();

}

$(document).ready(function () {
    //    $("#file").filestyle({ badge: false });


    $(".btnPost").click(function () {
        var postid = $(this).attr('id').split('-')[1];
        console.log(postid);
        var TextComment = $("#txtComment-" + postid).val();
        $.post("/Post/SumitComment?strComment=" + TextComment + "&postId=" + postid, function (result) {
            //console.log(result);
        });
        if (TextComment != "") {

            //$("#" + postid).append('<div class="form-group">'
            //  + '<div class="form-inline" style="border-bottom: ridge; width: 600px;">' +
            //       '<img src="/Images/orderedList3.png"></img>' +
            //       '<a href="#"><span style="font-size: 16px;"><b>' + $("#appUserName").val() + '</b></span></a>' +
            //       '<a href="#">@DateTime.Now.ToString()</a><br />' +
            //                 '<p style="margin-left: 55px;">' + TextComment + '</p>' +
            //                 '<div class="form-inline" style="height: 30px; margin-left: 10px;">' +
            //                     '<a href="#">Like</a>' +
            //                     '<a href="#">Reply</a>' +
            //                 '</div>' +
            //             '</div>' +
            //         '</div>');
            $("#txtComment-" + postid).val("");
            window.location.reload(true);
        }
        else {

            alert('hello ' + $("#TextComment").val());
        }
        //$("#newcomment").html('<div><img src="/Images/orderedList1.png"></img></div>');
    });


    $(".commentIcon").click(function () {
        var postid = $(this).attr('id').split('-')[1];
        $("#txtComment-" + postid).focus();
        //console.log(result);
    });


});