$(function() {
    global.easyuiHelper = easyuiHelper;
});


var easyuiHelper = {
    tabs: {
        add: function (title, url, debug) {
            var appendParam = "debug=0";
            if (debug) {
                appendParam = "debug=1";
            }
            if (url.indexOf("?") > 0) {
                url += "&" + appendParam;
            } else {
                url += "?" + appendParam;
            }
            if ($('#container-tabs').tabs('exists', title)) {
                $('#container-tabs').tabs('select', title);
            } else {
                if (debug) {
                    var content = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
                    $('#container-tabs').tabs('add', {
                        title: title,
                        content: content,
                        closable: true
                    });
                } else {
                    $('#container-tabs').tabs('add', {
                        title: title,
                        href: url,
                        closable: true
                    });
                }
            }
        }
        
    }
};