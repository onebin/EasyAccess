var easyuiHelper = {
    
    tabs: {
        add: function(title, url, debug) {
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
    },
    
    combobox: {
        booleanValueData: [{ value: 'True', text: '是' }, { value: 'False', text: '否' }],
        BooleanValueFormatter: function (value, row, index) {
            if (value == "True" || value == "true" || value == "1") {
                return "是";
            }
            return "否";
        }
    },
    
    datagrid: {
        reload: function (elemId) {
            $(elemId).datagrid('reload');
        },
        beginEdit: function(elemId, rowIndex) {
            $(elemId).datagrid('beginEdit', rowIndex);
        },
        endEdit: function (elemId, rowIndex) {
            $(elemId).datagrid('endEdit', rowIndex);
        },
        cancelEdit: function(elemId, rowIndex) {
            $(elemId).datagrid('cancelEdit', rowIndex);
        },
    },

    treegrid: {
        collapseAll: function (elemId) {
            $(elemId).treegrid('collapseAll');
        },
        expandAll: function (elemId) {
            $(elemId).treegrid('expandAll');
        },
        collapse: function (elemId, idFileName) {
            var node = $(elemId).treegrid('getSelected');
            if (node) {
                if (idFileName && idFileName.length > 0) {
                    $(elemId).treegrid('collapse', node[idFileName]);
                } else {
                    $(elemId).treegrid('collapse', node.Id);
                }
            }
        },
        expand: function (elemId, idFileName) {
            var node = $(elemId).treegrid('getSelected');
            if (node) {
                if (idFileName && idFileName.length > 0) {
                    $(elemId).treegrid('expand', node[idFileName]);
                } else {
                    $(elemId).treegrid('expand', node.Id);
                }
            }
        },
        reload: function (elemId) {
            $(elemId).treegrid('reload');
        }
    },
    
    messager: {
        confirm: {
            remove: function (event, msg) {
                if (msg && msg.length != 0) {
                } else {
                    msg = "是否确认删除?";
                }
                $.messager.confirm('操作提示', msg, function (r) {
                    if (r) {
                        if (event) {
                            event();
                        }
                    }
                });
            }
        },
        
        alert: function (param) {
            if (param.result) {
                if (param.result.ResultType == statusCodeHelper.ok) {
                    if (param.success) {
                        if (param.result.Message && param.result.Message.length > 0) {
                            global.Toast(true, param.result.Message);
                        } else {
                            global.Toast(true, "操作成功！");
                        }
                        param.success();
                    }
                } else {
                    if (param.result.Message && param.result.Message.length > 0) {
                        var alertType;
                        switch (param.result.ResultType) {
                            case statusCodeHelper.error:
                                alertType = "error";
                                break;
                            case statusCodeHelper.failed:
                                alertType = "warning";
                                break;
                            default:
                                alertType = "info";
                                break;
                        }
                        $.messager.alert("系统提示", param.result.Message, alertType);
                        if (param.fail) {
                            param.fail();
                        }
                    }
                }
            }
        }
    }
    
};

$(function () {
    global.easyuiHelper = easyuiHelper;
});