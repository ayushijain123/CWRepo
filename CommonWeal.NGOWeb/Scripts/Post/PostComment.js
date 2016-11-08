

function fileclick() {
    $("#file").click();

}

$(document).ready(function () {
    //    $("#file").filestyle({ badge: false });


    $(".btnPost").click(function () {
        var postid = $(this).attr('id').split('-')[1];
        console.log(postid);
        var TextComment = $("#txtComment-" + postid).val();
      
        if (TextComment != "") {
            $.post("/Post/SumitComment?strComment=" + TextComment + "&postId=" + postid, function (result) {
                //console.log(result);
            });
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

            alert('comment section cannot empty ' + $("#TextComment").val());
        }
        //$("#newcomment").html('<div><img src="/Images/orderedList1.png"></img></div>');
    });


    $(".commentIcon").click(function () {
        var postid = $(this).attr('id').split('-')[1];
        $("#txtComment-" + postid).focus();
        //console.log(result);
    });

    $(".textComment").on("keydown", function (e) {
        console.log(e.type, e);
        var text = e.type;
        var code = e.which ? e.which : e.keyCode;
        if (13 === code) {
            // As ASCII code for ENTER key is "13"
            var postid = $(this).attr('id').split('-')[1];
            $("#postimage-" + postid).click(); // Submit form code
        } else {
            text += ': keycode ' + code;
        }
    });

    //start Likecount and update ajax
    $(".LikeIcon").click(function () {
        var postid = $(this).attr('id').split('-')[1];
        var like = true;
        $.post("/Post/SubmitLike?Like=" + like + "&postId=" + postid, function (result) {
            window.location.reload(true);
            //console.log(result);
        });
      

    });

    $(".showLikeuser").click(function () {
        console.log('clicked');
        var postid = $(this).attr("id").split('-')[1];
        console.log('clicked-'+postid);
        $("#likelist-" + postid).html("");
        $.post("/Post/getLikeList?postid=" + postid, function (postlikelist) {
            //alert('hi');
            $.each(postlikelist, function (i, value) {
                console.log(postlikelist);
                var content = '<div id="userlist" style="margin-bottom:5px;border-bottom:ridge">' +
                '<div class="form-inline">'+
                '<img src="/Images/user.png" id="images" />'+
                '  <b id="usernamepost">' + value.userName + '</b>'+

                '</div>'+
                '</div>';

                $("#likelist-" + postid).append(content);
                
               // console.log(value.userName);
               // console.log(value.ModifiedOn);
            });
                
           

        });

    });


    //end Likecount

});