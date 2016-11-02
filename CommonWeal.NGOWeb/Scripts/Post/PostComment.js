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
    //Script for post
    $("#post").click(function () {

        $("#newuploadpost").append('<div style="background-color: lightgray;" id="postuserdetail">'
            + '<div class="form-inline"><img src="/Images/orderedList3.png"></img>'
            + '<a href="#">Satish Patidar</a>'
            + ' <a href="#">@DateTime.Now.ToString()</a>'
             + '</div>'
         + '</div>'
       // Text for image*
         + '<div name="Textmessage">'
            + ' <div class="form-inline" style="height: 50px;">'
+ '<p>welcome to NGO</p>'
            + ' </div>'
+ '</div>'

        + ' <div id="image/video">'
            + ' <img src="/images/Penguins.jpg" style="height: 500px; width: 600px;" />'



          ///edited on 24/10/16

            + ' <div style="background-color: lightgray">'
                 + '<div class="form-inline" style="height: 30px;">'
                     + '<a href="">Like</a>'
                     + '<a href="">Comment</a>'
                + ' </div>'
             + '</div>'

          ///Comment div action

           + '  <div class="form-inline" style="background-color: lightgray">'

                + ' <div class="form-group">'
                   + '  <div class="form-inline" style="border-bottom: ridge; width: 600px;">'
                         + '<img src="/Images/orderedList3.png"></img>'
+ '<a href="#"><span style="font-size: 16px;"><b>Ayushi Jain</b></span></a>'
                         + '<a href="#">@DateTime.Now.ToString()</a><br />'
                             + '<p style="margin-left: 55px;">wow!!!! penguins looks awesome</p>'
                         + '<div class="form-inline" style="height: 30px; margin-left: 10px;">'
                            + ' <a href="#">Like</a>'
                            + '<a href="#">Reply</a>'
                       + '  </div>'
                     + '</div>'
                 + '</div>'

                 + '<div class="form-group">'
                   + '<div class="form-inline" style="border-bottom: ridge; width: 600px;">'
                        + ' <img src="/Images/orderedList3.png"></img>'
                        + ' <a href="#"><span style="font-size: 16px;"><b>Ayushi Jain</b></span></a>'
                        + ' <a href="#">@DateTime.Now.ToString()</a><br />'
                         + '<p style="margin-left: 55px;">wow!!!! penguins looks awesome</p>'
                         + '<div class="form-inline" style="height: 30px; margin-left: 10px;">'
                            + ' <a href="#">Like</a>'
                          + '  <a href="#">Reply</a>'
                        + ' </div>'
                    + ' </div>'
                + ' </div>'

               + '  <div id="newcomment">'
                    + ' <br />'
                + ' </div>'
                //user Comment section*

                 + '<div class="form-inline">'
                 + '<img src="/Images/orderedList3.png"></img>'

                    + '<textarea style="width: 480px; height:35px;" class="form-control" placeholder="Write your Comment" id="TextComment"/> '
                    + '<input type="button" id="postimage" name="button" class="btn1 btn-primary" value="post" />'


                + '  </div>'
          + '  </div>'

        + ' </div>'
   + '  </div><br/><br/>');

    });


});
 
