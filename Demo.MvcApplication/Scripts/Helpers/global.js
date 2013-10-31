var global = {
    Toast: function (val, msg) {
        var toast = $("#toastMsg");
        if (toast.length == 0) {
            toast = $("<div id='toastMsg'></div>");
            $("body").append(toast);
        }
        if (val) {
            toast.addClass("toastSuccess");
        } else {
            toast.addClass("toastFail");
        }
        var divWidth = msg.length * 12;
        toast.html(msg);
        toast.css({ display: 'block', 'margin-left': '-' + divWidth / 2 + 'px', width: divWidth + 'px' }).animate({ left: '50%' }, 500, function () {
            setTimeout(function () {
                toast.animate({ opacity: 'toggle' }, 500, function () {
                    toast.html("");
                    toast.removeAttr("style class");
                });
            }, 3500);
        });
    }
};