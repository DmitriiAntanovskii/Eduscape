
$(function() {
	$('a[href*=#]:not([href=#])').click(function() {
	    if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') && location.hostname == this.hostname) {

	        var target = $(this.hash);
	        target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
	        if (target.length) {
	            $('html,body').animate({
	                scrollTop: target.offset().top - 20
	            }, 1000);
	            return false;
	        }
	    }
	});
});

function submitform(event,page) {
    $("#submitResult").html("");
    $("#submitResult").removeClass("alert-danger");
    $("#submitResult").removeClass("alert-success");
    event.preventDefault();
    if ($('form').valid()) {
        $("#submitResult").html("Processing... <img src=/img/ajax-loader.gif /> ");
        $("#submitResult").show();
        $.ajax({
            type: "POST",
            url: "/Member/Submit?email=" + $("#email").val(),
            success: function (result) {
                var arr = result.split("//");
                $("#submitResult").addClass(arr[1]);
                $("#submitResult").html(arr[0]);
                $("#form").hide();
                _gaq.push(['_trackEvent', 'Sign Up', page, 'Sign Up']);
            }
        });
    }
}


