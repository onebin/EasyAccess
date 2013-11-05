var global = {
    Toast: function (val, msg) {
        var toast = $("#toastMsg");
        if (toast.length == 0) {
            toast = $("<div id='toastMsg'></div>");
            $("body").append(toast);
        }
        if (!msg) {
            if (val) {
                msg = "oh yeah！你离成功又近了一步！=^_^= ";
            } else {
                msg = "I'm so sorry！操作失败了~〒_〒";
            }
        }
        if (toast.html() != msg) {
            if (val) {
                toast.addClass("toastSuccess");
            } else {
                toast.addClass("toastFail");
            }
            var divWidth = msg.replace(/[^\x00-\xff]/g, "**").length * 6;
            toast.html(msg);
            toast.css({ display: 'block', 'margin-right': '-' + divWidth / 2 + 'px', width: divWidth + 'px' }).animate({ right: '50%' }, 500, function () {
                setTimeout(function () {
                    toast.animate({ opacity: 'toggle' }, 500, function () {
                        toast.html("");
                        toast.removeAttr("style class");
                    });
                }, 3500);
            });
        }
    }
};