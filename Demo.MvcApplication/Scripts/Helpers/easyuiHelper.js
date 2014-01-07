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
                    {
                        iconCls: 'icon-mini-refresh',
                        handler: function () {
                            $('#container-tabs').tabs('select', title);
                            var tab = $('#container-tabs').tabs('getTab', title);
                            if (debug) {
                                tab.find("iframe")[0].contentWindow.location.href = url;
                            } else {
                                $('#container-tabs').tabs('update', {
                                    tab: tab,
                                    options: {
                                        href: url
                                    }
                                });
                            }
                        }
                    }
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
                        tools: tools,
                    });
                }
            }

        },

        closeAll: function () {
            easyuiHelper.messager.confirm.custom(function () {
                var tabsCount = $("#container-tabs").tabs("tabs").length;
                for (var i = 1; i < tabsCount; i++) {
                    $("#container-tabs").tabs("close", 1);
                }
            }, "确定要关闭全部标签页？");
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
            remove: function (param, msg) {
                if (msg && msg.length != 0) {
                } else {
                    msg = "真的要删除吗?(>﹏<) ";
                }
                easyuiHelper.messager.confirm.custom(param, msg);
            },

            custom: function (param, msg) {
                if (msg && msg.length != 0) {
                } else {
                    msg = "你确定要这么做？";
                }
                $.messager.confirm('操作提示', msg, function (r) {
                    if (r) {
                        if (typeof param === "function") {
                            param(param.param);
                        } else if (typeof param === "object" && typeof param.todo === "function") {
                            param.todo(param.param);
                        }
                    } else {
                        if (typeof param === "object" && typeof param.undo === "function") {
                            param.undo(param.param);
                        }
                    }
                });
            }
        },

        alert: function (param) {
            if (param.result) {
                if (param.result.StatusCode == statusCodeHelper.ok) {
                    if (typeof param.success === "function") {
                        if (param.result.Message && param.result.Message.length > 0) {
                            global.toast(true, param.result.Message);
                        } else {
                            global.toast(true);
                        }
                        param.success(param.param);
                    }
                } else {
                    if (param.result.Message && param.result.Message.length > 0) {
                        var title;
                        var alertType;
                        switch (param.result.StatusCode) {
                            case statusCodeHelper.error:
                                alertType = "error";
                                title = "错误";
                                break;
                            case statusCodeHelper.failed:
                            case statusCodeHelper.notModified:
                            case statusCodeHelper.notFound:
                                alertType = "warning";
                                title = "警告";
                                break;
                            default:
                                alertType = "info";
                                title = "提示";
                                break;
                        }
                        $.messager.alert(title, param.result.Message, alertType);
                        if (typeof param.fail === "function") {
                            param.fail(param.param);
                        }
                    }
                }
            }
        },

        show: function (msg, pos) {
            var position = {
                topLeft: {
                    right: '',
                    left: 0,
                    top: document.body.scrollTop + document.documentElement.scrollTop,
                    bottom: ''
                },
                topCenter: {
                    right: '',
                    top: document.body.scrollTop + document.documentElement.scrollTop,
                    bottom: ''
                },
                topRight: {
                    left: '',
                    right: 0,
                    top: document.body.scrollTop + document.documentElement.scrollTop,
                    bottom: ''
                },
                centerLeft: {
                    left: 0,
                    right: '',
                    bottom: ''
                },
                center: {
                    right: '',
                    bottom: ''
                },
                centerRight: {
                    left: '',
                    right: 0,
                    bottom: ''
                },
                bottomLeft: {
                    left: 0,
                    right: '',
                    top: '',
                    bottom: -document.body.scrollTop - document.documentElement.scrollTop
                },
                bottomCenter: {
                    right: '',
                    top: '',
                    bottom: -document.body.scrollTop - document.documentElement.scrollTop
                },
                bottomRight: {
                    right: 0,
                    top: '',
                    left: '',
                    bottom: -document.body.scrollTop - document.documentElement.scrollTop
                }
            };
            var style = position["topCenter"];
            if (pos) {
                style = position[pos];
            }
            $.messager.show({
                msg: msg,
                showType: 'show',
                style: style
            });
        }
    },

    formatter: {
        

        booleanValueData: [{ value: 'True', text: '是' }, { value: 'False', text: '否' }],
        booleanValueFormatter: function (value, row, index) {
            if (value == "True" || value == "true" || value == "1") {
                return "是";
            }
            return "否";
        },

        dateFormatter: function (date) {
            return new Date(date).format("yyyy-MM-dd");
        },

        datetimeFormatter: function (datetime) {
            return new Date(datetime).format("yyyy-MM-dd HH:mm");
        },

        numberboxFormatter: function (val) {
            if (val) {
                return parseFloat(val);
            }
            return val;
        }
    }

};

$(function () {
    global.easyuiHelper = easyuiHelper;
});


$.fn.pagination.defaults.pageSize = 20;
$.fn.pagination.defaults.pageList = [20, 40, 60, 80, 100];
$.fn.datagrid.defaults.pageSize = 20;
$.fn.datagrid.defaults.pageList = [20, 40, 60, 80, 100];

(function () {
    $.extend($.fn.tabs.methods, {
        //显示遮罩
        loading: function (jq, msg) {
            return jq.each(function () {
                var panel = $(this).tabs("getSelected");
                if (msg == undefined) {
                    msg = "正在加载数据，请稍候...";
                }
                $("<div class=\"datagrid-mask\"></div>").css({ display: "block", "margin-top": "32px", width: panel.width(), height: panel.height() }).appendTo(panel);
                $("<div class=\"datagrid-mask-msg\"></div>").html(msg).appendTo(panel).css({ display: "block", left: (panel.width() - $("div.datagrid-mask-msg", panel).outerWidth()) / 2, top: (panel.height() - $("div.datagrid-mask-msg", panel).outerHeight()) / 2 });
            });
        }
,
        //隐藏遮罩
        loaded: function (jq) {
            return jq.each(function () {
                var panel = $(this).tabs("getSelected");
                panel.find("div.datagrid-mask-msg").remove();
                panel.find("div.datagrid-mask").remove();
            });
        }
    });
})(jQuery);

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
        message: '请输入有效的范围({0}-{1})和检查数据格式(如: 0-1)'
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
    numberRange: { //验证有效数值范围（左右闭合）
        validator: function (value, param) {
            var val = parseFloat(value);
            if (!isNaN(val)) {
                var min = parseFloat(param[0]);
                var max = parseFloat(param[1]);
                if (min > max) {
                    var temp = min;
                    min = max;
                    max = temp;
                }
                if (isNaN(min)) {
                    min = Number.MIN_VALUE;
                }
                if (isNaN(max)) {
                    max = Number.MAX_VALUE;
                }
                return val >= min && val <= max;
            }
            return false;

        },
        message: '请输入{0}到{1}之间的数值'
    },
    minNum: {
        validator: function (value, param) {
            var val = parseFloat(value);
            if (!isNaN(val)) {
                var min = parseFloat(param[0]);
                if (isNaN(min)) {
                    min = Number.MIN_VALUE;
                }
                return val >= min;
            }
            return false;

        },
        message: '输入数值必须大于或等于{0}'
    },
    maxNum: {
        validator: function (value, param) {
            var val = parseFloat(value);
            if (!isNaN(val)) {
                var max = parseFloat(param[0]);
                if (isNaN(max)) {
                    max = Number.MAX_VALUE;
                }
                return val <= max;
            }
            return false;

        },
        message: '输入数值必须小于或等于{0}'
    },
    integer: {  // 验证整数
        validator: function (value) {
            return /^([+-]?[1-9]+\d*|0)$/i.test(value);
        },
        message: '请输入整数'
    },
    positiveInt: {  // 验证正整数
        validator: function (value) {
            return /^[+]?[1-9]+\d*$/i.test(value);
        },
        message: '请输入正整数'
    },
    negativeInt: {  // 验证负整数
        validator: function (value) {
            return /^-[1-9]+\d*$/i.test(value);
        },
        message: '请输入负整数'
    },
    naturalNum: {  // 验证自然数
        validator: function (value) {
            return /^([+]?[1-9]\d*|0)$/i.test(value);
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
    zip: { // 验证国内邮政编码
        validator: function (value) {
            return /^\d{6}$/i.test(value);
        },
        message: '邮政编码格式不正确'
    },
    ip: { // 验证IP地址
        validator: function (value) {
            return /d+.d+.d+.d+/i.test(value);
        },
        message: 'IP地址格式不正确'
    }
})