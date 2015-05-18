function memberdealclick(event, dealid, memberid) {
    //event.preventDefault();
    $.ajax({
        type: "POST",
        url: "/Member/DealLinkClick?dealID=" + dealid + "&memberID=" + memberid,
        success: function (result) {
            _gaq.push(['_trackEvent', 'Member Deal Click', dealid, memberid]);
        }
    });
}

$(document).ready(function () {
    
    var end = $("#DebtTypeID").val();
    if (end == 1 || end == 4) {
        $("#minrepayment").hide();
    }
    else {
        $("#minrepayment").show();
    }

    


$("#DebtTypeID").change(function () {
    var end = this.value;
    if (end == 1 || end == 4)
    {
        $("#minrepayment").hide();
    }
    else
    {
        $("#minrepayment").show();
    }
});
});

