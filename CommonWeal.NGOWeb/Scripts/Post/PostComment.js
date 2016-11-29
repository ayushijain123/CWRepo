

function fileclick() {
    $("#file").click();

}

$(document).ready(function () {
    //    $("#file").filestyle({ badge: false });


    $(".btnPost").live('click', function () {
        var postid = $(this).attr('id').split('-')[1];
        console.log(postid);
        var TextComment = $("#txtComment-" + postid).val().trim();
        var userName = "";
        var createdOn = "";
        if (TextComment != "") {
            $.post("/Post/SumitComment?strComment=" + TextComment + "&postId=" + postid, function (userinfo) {
                userName = userinfo[0];
                createdOn = userinfo[1];

                $("#" + postid).append('    <div class="form-group col-md-12 ">' +
                '<div class="form-inline col-md-12" id="comentpost">' +
                    '<span class="col-md-1 fa fa-user commentimage " ></span>' +
                    '<span class="commentusername "><b>' + userName + ' </b></span>' +
                    '<span class="commentdate">' + createdOn + '</span>' +
                    '<p id="postcommentcontent">' + TextComment + '</p>' +

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
        var postid = $(this).attr('id').split('-')[1];
        var like = true;
        $.post("/Post/SubmitLike?Like=" + like + "&postId=" + postid, function (result) {

            window.location.reload();
            //console.log(result);
        });


    });

    $(".showLikeuser").click(function () {
        console.log('clicked');
        var postid = $(this).attr("id").split('-')[1];
        console.log('clicked-' + postid);
        $("#likelist-" + postid).html("");
        $.post("/Post/getLikeList?postid=" + postid, function (postlikelist) {
       
            $.each(postlikelist, function (i, value) {
                console.log(postlikelist);
                var content = '<div id="userlist" style="margin-bottom:5px;border-bottom:ridge">' +
                '<div class="form-inline">' +
                '<span class="fa fa-user" id="images" ></span>' +
                '  <b id="usernamepost">' + value.userName + '</b>' +

                '</div>' +
                '</div>';

                $("#likelist-" + postid).append(content);

             
            });
        });

    });


    //end Likecount








    //full image script

    // Get the modal

    $(".imageurlpost").click(function () {

        var postid = $(this).attr("id").split('-')[1];
      
        //$("#myModal2-" + postid).css('display', 'block');

        $("#img01-" + postid).attr('src', this.src);
        //  //$("#caption").html(this.alt);
        //    console.log(postid);
        //    var comment = $("#postcommentbox-" + postid).html();
        //   // $("#comnt-" + postid).html("");
        //   // $("#comnt-" + postid).append(comment);
        // console.log(comment);
     
        //    // $(".topfix").css("z-index", 0);
        //    // $("#uploadpost").css("display", 'none');
        //    //$('body').css("filter", "blur(2px)");
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
     
        var category = $("#selectcategory").val();
        $.post("/post/onLoadPost?count=" + loadcount + "&category=" + category, function (result) {

            console.log(result);
            $("#loadMoreSection").append(result);
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
    $("#submitcategory").click(function () {

        var categorylist = [];
        $.each($(".category:checked"), function () {
            var CategoryID = $(this).val();
            categorylist.push(CategoryID);
            $('#' + CategoryID).remove();
        });
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            url: "/NGOProfile/areaOfIntrest",
            dataType: "json",
            traditional: true,
            data: JSON.stringify(categorylist)
        });
        //  $.post("/NGOProfile/areaOfIntrest?List=" + list, function (result) {

        //console.log(result);
        //  }, 'json');

    });



    /*script for admin page methods (activeUsers)*/
    $(".BlockButton").click(function () {
        var postid = $(this).attr('id').split('-')[1];

        $.post("/Admin/Block?PostID=" + postid, function (result) {

            $('#activeUser-' + postid).remove();
            
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
    var stickyOffset = $('.sticky').offset().top;

    $(window).scroll(function () {
        var sticky = $('.sticky'),
            scroll = $(window).scrollTop();

        if (scroll >= stickyOffset - 130) sticky.addClass('fixed');
        else sticky.removeClass('fixed');
    });


    /*js for replace category tag on header search bar*/

});


///////

//Added by Neha M. on 25-11-16
//Admin Module Script

//Donut Chart Graph Script
Morris.Donut({
    element: 'ornek-donut',
    data: [
      { label: "Active NGOs", value: 50 },
      { label: "Blocked NGOs", value: 30 },
      { label: "Active Users", value: 20 },
      { label: "Warned NGOs/Users", value: 30 },
      { label: "Blocked Users", value: 20 }
    ],
    labelColor: '#000000',
    colors: [
        '#77af3b',
        '#8C1717',
        '#364359',
        '#f79219',
        '#dd2c2b'
    ],
    formatter: function (x) { return x }
});



//Added by Neha M. on 28-11-16
//Discrete bar graph script
var chart = nv.models.multiBarChart();
d3.select('#chart svg').datum([
  {
      key: "NGOs",
      color: "#77af3b",
      values:
      [
        { x: "Jan", y: 40 },
        { x: "Feb", y: 30 },
        { x: "Mar", y: 20 },
        { x: "Apr", y: 40 },
        { x: "May", y: 30 },
        { x: "Jun", y: 20 },
        { x: "Jul", y: 40 },
        { x: "Aug", y: 30 },
        { x: "Sep", y: 20 },
        { x: "Oct", y: 40 },
        { x: "Nov", y: 30 },
        { x: "Dec", y: 20 }
      ]
  },
{
    key: "Users",
    color: "#364359",
    values:
    [
        { x: "Jan", y: 30 },
        { x: "Feb", y: 30 },
        { x: "Mar", y: 10 },
        { x: "Apr", y: 40 },
        { x: "May", y: 50 },
        { x: "Jun", y: 20 },
        { x: "Jul", y: 40 },
        { x: "Aug", y: 30 },
        { x: "Sep", y: 50 },
        { x: "Oct", y: 40 },
        { x: "Nov", y: 60 },
        { x: "Dec", y: 20 }
    ]
}
]).transition().duration(500).call(chart);
