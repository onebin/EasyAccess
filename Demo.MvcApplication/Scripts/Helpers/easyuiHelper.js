$(function() {
    global.easyuiHelper = easyuiHelper;
});


var easyuiHelper = {
    addTab: function (title, url) {
        if ($('#container-tabs').tabs('exists', title)) {
            $('#container-tabs').tabs('select', title);
        } else {
            var content = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
            $('#container-tabs').tabs('add', {
            title: title,
            content: content,
            closable: true
            });
        }
    }
};