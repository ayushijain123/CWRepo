
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
});









