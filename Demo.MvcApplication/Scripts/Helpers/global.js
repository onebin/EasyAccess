var global = {
    Toast: function (val, msg) {
        var div = $("#toast");
        if (val) {
            div.addClass("toastSuccess");
        } else {
            div.addClass("toastFail");
        }
        div.html(msg);
        div.css({ display: 'block', left: '-200px' }).animate({ left: '40%' }, 500, function () { setTimeout(function() {
            div.animate({ right: '40%', opacity: 'toggle' }, 500, function () {
                div.css({ display: 'none', right: '-200px' });
                div.html("");
                div.removeClass();
            });
        }, 3500); });
    }
};