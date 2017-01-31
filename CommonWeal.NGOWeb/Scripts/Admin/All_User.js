
$(document).ready(function () {
    $('#myTable').DataTable();
    /*ajax for block user from all user*/
    $(document).on('click','.BlockUser',function () {
        var loginid = $(this).attr('id').split('-')[1];        
        if (loginid == null)
        { loginid = 0; }
        var response = confirm("Are you sure you want to block this user?");
        if (response) {
            $.post("/admin/BlockUsers?id=" + loginid, function (result) {
                if (result == true) {
                    $('#blockrow-' + loginid).remove();
                }
            });
        }
    });

    /*ajax for block ngo from active ngo*/
    $(document).on('click', '.blockngo', function () {
        var loginid = $(this).attr('id').split('-')[1];
        if (loginid == null)
        { loginid = 0; }
        var response = confirm("Are you sure you want to block this ngo?");
        if (response) {
            $.post("/admin/Block?id=" + loginid, function (result) {
                if (result == true) {
                    $('#BlockNGO-' + loginid).remove();
                }
            });
        }
    });



    /*ajax for RequsetEstimation*/

    $(document).on('click', ".RequestEstimation", function () {
        var userid = $(this).attr('id').split('-')[1];

        $.post("/admin/RequestEstiamtion?id=" + userid, function (result) {

            

        });


    });


    /*for cost estimation */
   
    $(document).on('change', ".unitprice", function () {
       
        var id = $(this).attr('id').split('-')[1];
        var itemcount = parseInt($("#received-" + id).text());
        var unitprice = parseInt($("#unitprice-" + id).val());
        var grandtotal = parseInt($("#total").val());
        $("#lineTotal-" + id).text(itemcount * unitprice);
       
        $("#total").val(grandtotal + itemcount * unitprice);
    });

    


    $("#submitEstimation").click(function () {

        var formData = new FormData(); //create form data object
        var obj, Itemlist;
        Itemlist = [];
       

        $(".admintable").each(function () {
            var ItemID = $(this).attr('id').split('-')[1];
            obj = {};

            
            obj["Unitprice"] = $("#unitprice-" + ItemID).val();
            obj["linetotal"] = parseInt($("#lineTotal-"+ItemID).text());
            obj["itemId"] = ItemID;
            Itemlist.push(obj);
            
        });
        console.log(Itemlist);

       

        // append all itemlist to formdata
        
        jQuery.each(Itemlist, function (key, value) {
            formData.append('itemlist[' + key + '].Unitprice', value.Unitprice);
            formData.append('itemlist[' + key + '].linetotal', value.linetotal);
            formData.append('itemlist[' + key + '].itemId', value.itemId);
        });

       
        //formData.append("DonationRequestImg", files[k]);
        $.ajax({
            url: "/Admin/submitCostEstimation",
            type: "POST",
            dataType: "JSON",
            data: formData,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result == true) {
                    
                    alert('successfully uploaded');
                }
                else {
                    alert('Sorry upload Failed try again');

                }



            }
        });
    });












    /*ajax for unspam ngo*/
    $(document).on('click', '.UnspamNGO', function () {
        var loginid = $(this).attr('id').split('-')[1];
        if (loginid == null)
        { loginid = 0; }
        var response = confirm("Are you sure you want to unspam this ngo?");
        if (response) {
            $.post("/admin/UnSpamNGO?id=" + loginid, function (result) {
                if (result == true) {
                    $('#SpamNGO-' + loginid).remove();
                }
            });
        }
    });

    /*ajax for block ngo from spam ngo*/
    $(document).on('click', '.BlockSpamNGO', function () {
        var loginid = $(this).attr('id').split('-')[1];
        if (loginid == null)
        { loginid = 0; }
        var response = confirm("Are you sure you want to block this ngo?");
        if (response) {
            $.post("/admin/BlockFromSpamNGO?id=" + loginid, function (result) {
                if (result == true) {
                    $('#status-' + loginid).html("Blocked");
                    $('#UnspamNGO-' + loginid).remove();
                    $('#BlockSpamNGO-' + loginid).remove();
                    $('#iconcol-' + loginid).html('<span id="UnBlockSpamNGO-' + loginid + '" class="fa fa-unlock fa-2x UnBlockSpamNGO" title="Unblock" aria-hidden="true" style="color:green"></span>');
                }
            });
        }
    });

    /*ajax for UnBlock ngo from Spam NGO*/
    $(document).on('click', '.UnBlockSpamNGO', function () {
        var loginid = $(this).attr('id').split('-')[1];
        if (loginid == null)
        { loginid = 0; }
        var response = confirm("Are you sure you want to unblock this ngo?");
        if (response) {
            $.post("/admin/UnBlockFromSpamNGO?id=" + loginid, function (result) {
                if (result == true) {
                    $('#SpamNGO-' + loginid).remove();
                }
            });
        }
    });

    /*ajax for block user from spam user*/
    $(document).on('click', '.BlockSpamUser', function () {
        var loginid = $(this).attr('id').split('-')[1];
        if (loginid == null)
        { loginid = 0; }
        var response = confirm("Are you sure you want to block this user?");
        if (response) {
            $.post("/admin/BlockFromSpamUser?id=" + loginid, function (result) {
                if (result == true) {
                    $('#status-' + loginid).html("Blocked");
                    $('#UnspamUser-' + loginid).remove();
                    $('#BlockSpamUser-' + loginid).remove();
                    $('#iconcoluser-' + loginid).html('<span id="UnblockSpamUser-'+loginid+'" class="fa fa-unlock fa-2x UnblockSpamUser" title="Unblock" aria-hidden="true" style="color:green"></span>');
                }
            });
        }
    });

    /*ajax for unspam user*/
    $(document).on('click', '.UnspamUser', function () {
        var loginid = $(this).attr('id').split('-')[1];
        if (loginid == null)
        { loginid = 0; }
        var response = confirm("Are you sure you want to unspam this user?");
        if (response) {
            $.post("/admin/UnSpam?id=" + loginid, function (result) {
                if (result == true) {
                    $('#SpamUser-' + loginid).remove();
                }
            });
        }
    });

    /*ajax for unblock user*/
    $(document).on('click', '.UnblockSpamUser', function () {
        var loginid = $(this).attr('id').split('-')[1];
        if (loginid == null)
        { loginid = 0; }
        var response = confirm("Are you sure you want to unblock this user?");
        if (response) {
            $.post("/admin/UnBlockFromSpamUser?id=" + loginid, function (result) {
                if (result == true) {
                    $('#SpamUser-' + loginid).remove();
                }
            });
        }
    });

    /*ajax for warn user from active ngo*/
    $(document).on('click', '.warn', function () {
        var loginid = $(this).attr('id').split('-')[1];
        if (loginid == null)
        { loginid = 0; }
        var response = confirm("Are you sure you want to send warning this ngo?");
        if (response) {
            $.post("/admin/WarnNGO?id=" + loginid, function (result) {
                if (result == true) {
                    alert("warning send");
                }
            });
        }
    });

    /*ajax for unwarn ngo*/
    $(document).on('click', '.unwarn', function () {
        var loginid = $(this).attr('id').split('-')[1];
        if (loginid == null)
        { loginid = 0; }
        var response = confirm("Are you sure you want to unblock this user?");
        if (response) {
            $.post("/admin/UnwarnNGO?id=" + loginid, function (result) {
                if (result == true) {
                    $('#Unwarn-' + loginid).remove();
                }
            });
        }
    });

    /*ajax for accept from blocked ngo*/
    $(document).on('click', '.Accept', function () {
        var loginid = $(this).attr('id').split('-')[1];
        if (loginid == null)
        { loginid = 0; }
        var response = confirm("Are you sure you want to accept the request?");
        if (response) {
            $.post("/admin/Accept?id=" + loginid, function (result) {
                if (result == true) {                   
                    $('#SpamNGO-' + loginid).remove();                  
                }
            });
        }
    });

    /*ajax for deleting a comment(NGO)*/
    $(document).on('click', '.DeleteCommentNGO', function () {
        var commentid = $(this).attr('id').split('-')[1];
        if (commentid == null)
        { commentid = 0; }
        var response = confirm("Are you sure you want to delete this comment?");
        if (response) {
            $.post("/Post/DeleteCommentOnPost?id=" + commentid, function (result) {
                if (result == true) {
                    var loginid = $("#"+commentid).text();
                    $('#SpamNGO-' + loginid).remove();
                }
            });
        }
    });

    /*ajax for deleting a comment(USER)*/
    $(document).on('click', '.DeleteCommentUser', function () {
        var commentid = $(this).attr('id').split('-')[1];
        if (commentid == null)
        { commentid = 0; }
        var response = confirm("Are you sure you want to delete this comment?");
        if (response) {
            $.post("/Post/DeleteCommentOnPost?id=" + commentid, function (result) {
                if (result == true) {
                   
                    var loginid = $("#"+commentid).text();
                    alert(loginid);
                    $('#SpamUser-' + loginid).remove();
                }
            });
        }
    });
});









