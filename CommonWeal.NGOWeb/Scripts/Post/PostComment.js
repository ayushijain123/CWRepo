

function fileclick() {
    $("#file").click();

}

$(document).ready(function () {
    //    $("#file").filestyle({ badge: false });


    $(".btnPost").live('click', function () {
        var postid = $(this).attr('id').split('-')[1];
        console.log(postid);
        var controller = $("#controllername").html().trim();
        var TextComment = $("#txtComment-" + postid).val().trim();
        var IsCurrentNgoProfile = $("#IeThisCurrentNgoProfile").html().trim();
       
        var userName = "";
        var createdOn = "";
        var spamicon = "";
        var commentid = 0;
        var DeleteComment = "s";
        if (TextComment != "") {
            $.post("/Post/SumitComment?strComment=" + TextComment + "&postId=" + postid, function (userinfo) {
                userName = userinfo[0];
                createdOn = userinfo[1];
                commentid = parseInt(userinfo[2]);
                if (controller == "NGOProfile"){
                    if(IsCurrentNgoProfile == "True") {
                  
                           spamicon = '<span id="reportAbuse-' + commentid + '" class="reportAbuse fa fa-exclamation-circle float-right" title="Report abuse"></span>';
                            DeleteComment ='<span id="deleteComment-'+ commentid+'" class="deleteComment float-right fa fa-remove" title="Delete comment"></span>';
                        }
                }
                $("#" + postid).append('<div class="form-group col-md-12 "id="CommentBox-'+commentid+'">' +
                '<div class="form-inline col-md-12 comentpost ">' +
                    '<span class="col-md-1 fa fa-user commentimage " ></span>' +
                    '<span class="commentusername "><b>' + userName + ' </b></span>' +
                    '<span class="commentdate">' + createdOn + '</span>' +
                    spamicon+DeleteComment+

                    '<p id="postcommentcontent"class="postcommentcontent">' + TextComment + '</p>' +

                '</div>' +
            '</div>');
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

        }
        else {

            alert('Please enter your Comment');
        }
        //$("#newcomment").html('<div><img src="/Images/orderedList1.png"></img></div>');
    });


    $(".commentIcon").live('click', function () {

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
            if ($("#txtComment-" + postid).val().trim() != null) {
                $("#postimage-" + postid).click(); // Submit form code
            }
        } else {
            text += ': keycode ' + code;
        }
    });

    //start Likecount and update ajax
    $(".LikeIcon").live('click', function () {
        console.log('clicked');
        var controllerNAME = $("#controllername").html();
      //  alert(controllerNAME);
        var postid = $(this).attr('id').split('-')[1];
        var like = true;
        $.post("/Post/SubmitLike?Like=" + like + "&controllerNAME="+controllerNAME+"&postId=" + postid, function (result) {
            if (result != null)
            {
                //alert(result);
                $("#liketemplate-" + postid).html("");

                $("#liketemplate-" + postid).append(result);
            }

          //  window.location.reload();
            //console.log(result);
        });


    });

    $(".showLikeuser2").click(function () {
        console.log('clicked');
        var postid = $(this).attr("id").split('-')[1];
        console.log('clicked-' + postid);
        $("#likelist-" + postid).html("");
       
        $.post("/Post/getLikeList?postid=" + postid, function (postlikelist) {
            //alert('hi');
            $.each(postlikelist, function (i, value) {
                console.log(postlikelist);
                var content = '<div id="userlist" style="margin-bottom:5px;border-bottom:ridge">' +
                '<div class="form-inline">' +
                '<span id="images fa fa-user" ></span>' +
                '  <b id="usernamepost">' + value.userName + '</b>' +
                
                '</div>' +
                '</div>';

                $("#likelist-" + postid).append(content);

                // console.log(value.userName);
                // console.log(value.ModifiedOn);
            });
        });

    });


    //end Likecount

    $(".showLikeuser").live('mouseover',function () {
        console.log('clicked');
        var postid = $(this).attr("id").split('-')[1];
        console.log('clicked-' + postid);
        $("#likelist-" + postid).html("");
        $.post("/Post/getLikeListAjax?postid=" + postid, function (postlikelist) {
            //alert('hi'); 
            $("#catlist-" + postid).html("");
            $.each(postlikelist, function (i, value) {
                console.log(postlikelist);

                var content = value.userName + '<br>';

                $("#catlist-" + postid).append(content);

                // console.log(value.userName);
                // console.log(value.ModifiedOn);
            });
        });

    });




    /*submit abuse user*/
    $(".reportAbuse").live('click', function () {
        var commentId = $(this).attr('id').split('-')[1];
        var like = true;
        var commentusername = $("#commentusername-" + commentId).text();
        $.post("/Post/AbuseUser?CommentId=" + commentId, function (result) {
            if (result != null) {
                //alert(result);

                if (result == true) {
                    //$("#reportAbuse-" + commentId).html("Reported");
                    alert(commentusername+"reported abuse");
                }
                else { alert(commentusername + "already reported abuse"); }
               

               
            }

            //  window.location.reload();
            //console.log(result);
        });


    });

    //full image script

    // Get the modal

    $(".imageurlpost").live('click',function () {

        var postid = $(this).attr("id").split('-')[1];
        
      //  $("#myModal2-" + postid).css('display', 'block');

        $("#img01-" + postid).attr('src', this.src);
        
          //$("#caption").html(this.alt);
            //console.log(postid);
           // var comment = $("#postcommentbox-" + postid).html();
           // $("#comnt-" + postid).html("");
           // $("#comnt-" + postid).append(comment);
         //console.log(comment);
        // alert(comment);
             //$(".topfix").css("z-index", 0);
            //$("#uploadpost").css("display", 'none');
            //$('body').css("filter", "blur(2px)");
    });

    //$(".likemodaldialog").click(function () {
    //    // $("#uploadpost").css("display", 'block');
    //    $("#myModal2-" + postid).css('display', 'none');
    //});
    //$(".fullImageModal").click(function () {
    //    // $("#uploadpost").css("display", 'block');
    //    //  $(".topfix").css("z-index", 1);
    //    $("#myModal2-" + postid).css('display', 'none');
    //});

    //upload image name in Ngohome
    $("#file").change(function (event) {

        var fileName = window.URL.createObjectURL(event.target.files[0])

        $("#uploadFileName").html("");
        $("#uploadFileName").append('<img src="' + fileName + '" style="height:40px;width:40px">');


    });



    //on load more button ajax call

    var loadcount = 0;
    $("#btnLoad").click(function () {
        loadcount++;
        // alert('hbtnload');
        var controller = $("#controllername").html();
        //alert(controller);
        var id = 0;
        var categorylist = $("#selectcategory").val();
        console.log(category);
        var category = JSON.stringify({ 'category': categorylist, 'controller': controller, 'count': loadcount, 'NgoID': id });
      
        $.ajax({
            contentType: 'application/json; charset=utf-8',

            type: 'POST',
            url: "/post/onLoadPost",
            data: category,
            success: function (result) {


                console.log(result);
                $("#loadMoreSection").append(result);
                $.post("/post/getpostCount", function (result) {
                    var total = result - (loadcount + 1) * 5;

                    if (total <= 0 && result > 0) {
                        $(".btnLoad").val('No more post');
                        $(".btnLoad").attr('disabled', true);
                        //alert(result + "" + total);
                        loadcount = 0;
                    }
                    if (result <= 0) {
                        $(".btnLoad").val('No post found');
                        $(".btnLoad").attr('disabled', true);
                        // alert(result + "" + total);
                        loadcount = 0;
                    }

                });
                //console.log(result);
            }
        });

       
    });
    //on seemore from ngoprofile ajax call
    var loadcnt = 0;
    $(".btnLoadNGOPfrofile").click(function () {
        //console.log('clicked');
        loadcnt++;
        var controller = $("#controllername").html();
        //alert('hi');
        var currentId = $('.btnLoadNGOPfrofile').attr('id').split('-')[1];
        if (currentId == null) {
            currentId = 1;
        }
        
        var categorylist = $("#selectcategory").val();
        if (categorylist == null)
        { categorylist = null; }
        var category = JSON.stringify({ 'category': categorylist,'controller': controller, 'count': loadcnt, 'NgoID': currentId });
        $.ajax({
            contentType: 'application/json; charset=utf-8',

            type: 'POST',
            url: "/post/onLoadPost",
            data: category,
            success: function (result) {

                console.log(result);
                $("#loadMoreSection").append(result);
                $.post("/post/getpostCount", function (result1) {
                    var total = result1 - (loadcnt + 1) * 5;
                    // alert(total);
                    if (total <= 0 && result1 > 0) {
                        $(".btnLoadNGOPfrofile").val('No more post');
                        $(".btnLoadNGOPfrofile").attr('disabled', true);
                       // alert(result1 + "h" + total);
                        loadcnt = 0;
                    }
                    if (result1 <= 0) {
                        $(".btnLoadNGOPfrofile").val('No post found');
                        $(".btnLoadNGOPfrofile").attr('disabled', true);
                        //  alert(result1 + "" + total);
                        loadcnt = 0;
                    }

                });
                /*hide see more  when no post*/
            }
            //console.log(result);
        });
    });


  


    /*date picked ngo registration page*/
    //$("#DateOfRegistration").datepicker({
    //    dateFormat: "yy-mm-dd",
    //    changeMonth: true,
    //    changeYear: true,
    //    yearRange: "-60:+0",
    //    maxDate: '0'
    //});

    /*date picked ngo registration page*/
    /*method for getting checked category*/
    $("").click(function () {
       
        var categorylist = [];
        $.each($(".category:checked"), function () {
            var CategoryID = $(this).val();
            categorylist.push(CategoryID);
            $('#' + CategoryID).remove();
        });
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            url: "/NGOHome/PostImage",
            dataType: "json",
            traditional: true,
            data: JSON.stringify(categorylist)
        });
        //  $.post("/NGOProfile/areaOfIntrest?List=" + list, function (result) {

        // alert(result);
        //console.log(result);
        //  }, 'json');

    });



    /*start ajax for delete comment on post*/
    $(".deleteComment").live('click', function () {
        var commentid = $(this).attr('id').split('-')[1];
        if (commentid == null)
        { commentid = 0;}
        $.post("/Post/DeleteCommentOnPost?id=" + commentid, function (result) {
            if (result != null) {
                $('#CommentBox-' + commentid).remove();

            }

        });
    });

    /*start ajax for delete Post*/
    $(".deletePost").live('click', function () {
        var postid = $(this).attr('id').split('-')[1];
        if (postid == null)
        { postid = 0; }
        $.post("/Post/DeleteCommentOnPost?ID=" + postid, function (result) {
            if (result != null) {
                $('#Post-' + postid).remove();

            }

        });
    });

    /*start ajax for  NGOProfilePOST partial*/
    $(".NGOProfilepost").live('click',function () {
        var userid = $(this).attr('id').split('-')[1];
       
        $.post("/NGOProfile/NGOProfilePost?id="+userid, function (result) {
          
            $('#NGOProfilecontent').html("");
            $('#NGOProfilecontent').append(result);
          
        });


    });
    /*end ajax for NGOProfilePOST partial*/


    /*start ajax for about us partial*/
    $(".aboutus").live('click', function () {
        var userid = $(this).attr('id').split('-')[1];  
       
        $.post("/NGOProfile/AboutUsPartial?id="+userid, function (result) {
          
            $('#NGOProfilecontent').html("");
            $('#NGOProfilecontent').append(result);
            
        });


    });
    /*end ajax for about us partial*/

    /*script for admin page methods (activeUsers)*/
    $(".BlockButton").click(function () {
        var postid = $(this).attr('id').split('-')[1];

        $.post("/Admin/Block?PostID=" + postid, function (result) {

            $('#activeUser-' + postid).remove();
            alert('hi');
            //console.log(result);
        });


    });
    //$(window).scroll(function () {
    //    var stickyNavTop = $('.ngoprofilenavbar').offset().top;


    //        var scrollTop = $(window).scrollTop();

    //        if (scrollTop > stickyNavTop) { 
    //            $('.ngoprofilenavbar').addClass('sticky');
    //        } else {
    //            $('.ngoprofilenavbar').removeClass('sticky');
    //        }



    //    });


    //$(window).scroll(function () {
    //    var sticky = $('.sticky'),
    //        scroll = $(window).scrollTop();

    //    if (scroll >= 500) sticky.addClass('ngoSticky');
    //    else sticky.removeClass('ngoSticky');
    //});
 ////   var stickyOffset = $('.sticky').offset().top;

 //   $(window).scroll(function () {
 //       var sticky = $('.sticky'),
 //           scroll = $(window).scrollTop();

 //       if (scroll >= stickyOffset - 130) sticky.addClass('fixed');
 //       else sticky.removeClass('fixed');
 //   });


    /*js for replace category tag on header search bar*/

    //$('img').on('click', function () {
    //    var image = $(this).attr('src');
    //    $('#myModal2').on('show.bs.modal', function () {
    //        $(".img-responsive").attr("src", image);
    //    });
    //});


});   

/* added by neha b*/
s
