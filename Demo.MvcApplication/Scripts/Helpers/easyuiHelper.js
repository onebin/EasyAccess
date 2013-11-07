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

                var tools = [
                    //{
                    //    iconCls: 'icon-mini-refresh',
                    //    handler: function() {
                    //        $('#container-tabs').tabs('getTab', title).panel("refresh");
                    //    }
                    //}
                ];

                if (debug) {
                    var content = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
                    $('#container-tabs').tabs('add', {
                        title: title,
                        content: content,
                        closable: true,
                        tools: tools
                    });
                } else {
                    $('#container-tabs').tabs('add', {
                        title: title,
                        href: url,
                        closable: true,
                        tools: tools
                    });
                }
            }

        },

        closeAll: function () {
            easyuiHelper.messager.confirm.custom(function (parameters) {
                var tabsCount = $("#container-tabs").tabs("tabs").length;
                for (var i = 1; i < tabsCount; i++) {
                    $("#container-tabs").tabs("close", 1);
                }
            }, "确定要关闭全部标签页？");
        },

        refresh: function (which) {

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
        beginEdit: function (elemId, rowIndex) {
            $(elemId).datagrid('beginEdit', rowIndex);
        },
        endEdit: function (elemId, rowIndex) {
            $(elemId).datagrid('endEdit', rowIndex);
        },
        cancelEdit: function (elemId, rowIndex) {
            $(elemId).datagrid('cancelEdit', rowIndex);
        },
        localSorter: function (valA, valB) {
            return (valA > valB ? 1 : -1);
        }
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
                    msg = "真的要删除吗?(>﹏<) ";
                }
                easyuiHelper.messager.confirm.custom(event, msg);
            },

            custom: function (event, msg) {
                if (msg && msg.length != 0) {
                } else {
                    msg = "你确定要这么做？";
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
                            global.Toast(true);
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
                            case statusCodeHelper.notModified:
                            case statusCodeHelper.notFound:
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

$.extend($.fn.validatebox.defaults.rules, {
    range: { //验证范围
        validator: function (value, param) {
            if (/^\d+(\.\d*)?[-]\d+(\.\d*)?$/.test(value)) {
                var nums = value.split('-');
                var num1 = parseFloat(nums[0]);
                var num2 = parseFloat(nums[1]);
                if (num1 <= num2) {
                    if (param && param.length == 2) {
                        return num1 >= parseFloat(param[0]) && num2 <= parseFloat(param[1]);
                    }
                    return true;
                }
            }
            return false;
        },
        message: '请输入有效的范围'
    },
    minLength: { // 验证最少字符长度
        validator: function (value, param) {
            return value.length >= param[0];
        },
        message: '最少输入 {0} 个字符。'
    },
    maxLength: { // 验证最大字符长度
        validator: function (value, param) {
            return value.length < param[0];
        },
        message: '最多输入 {0} 个字符。'
    },
    phone: { // 验证固话号码
        validator: function (value) {
            return /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/i.test(value);
        },
        message: '格式不正确,请使用下面格式:020-88888888'
    },
    mobile: { // 验证手机号码
        validator: function (value) {
            return /^(13|15|18)\d{9}$/i.test(value);
        },
        message: '手机号码格式不正确'
    },
    contact: { //验证固话号码/手机号码
        validator: function (value) {
            var val1 = /^(13|15|18)\d{9}$/i.test(value);
            var val2 = /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/i.test(value);
            return val1 ^ val2;
        },
        message: '号码格式不正确'
    },
    idNo: { // 验证身份证
        validator: function (value) {
            return /^\d{15}(\d{2}[A-Za-z0-9])?$/i.test(value);
        },
        message: '身份证号码格式不正确'
    },
    qq: {  // 验证QQ,从10000开始
        validator: function (value) {
            return /^[1-9]\d{4,9}$/i.test(value);
        },
        message: 'QQ号码格式不正确'
    },
    integer: {  // 验证正整数
        validator: function (value) {
            return /^[+]?[1-9]+\d*$/i.test(value);
        },
        message: '请输入正整数'
    },
    naturalNum: {  // 验证自然数
        validator: function (value) {
            return /^([1-9]\d*|0)$/i.test(value);
        },
        message: '请输入自然数'
    },
    chinese: { // 验证中文
        validator: function (value) {
            return /^[\u0391-\uFFE5]+$/i.test(value);
        },
        message: '请输入中文'
    },
    english: { // 验证英文
        validator: function (value) {
            return /^[A-Za-z]+$/i.test(value);
        },
        message: '请输入英文'
    },
    faxNo: { // 验证传真
        validator: function (value) {
            return /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/i.test(value);
        },
        message: '传真号码不正确'
    },
    zip: { // 验证邮政编码
        validator: function (value) {
            return /^[1-9]\d{5}$/i.test(value);
        },
        message: '邮政编码格式不正确'
    },
    ip: { // 验证IP地址
        validator: function (value) {
            return /d+.d+.d+.d+/i.test(value);
        },
        message: 'IP地址格式不正确'
    }
});