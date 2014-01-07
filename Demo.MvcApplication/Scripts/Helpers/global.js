var global = {
    toast: function (val, msg) {
        var toast = $("#toastMsg");
        if (toast.length == 0) {
            toast = $("<div id='toastMsg'></div>");
            $("body").append(toast);
        }
        if (!msg) {
            if (val) {
                msg = "操作成功！";
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
    },

    getDialogContainer: function () {
        var dialogContainer = $("#dialogContainer");
        if (dialogContainer.length == 0) {
            dialogContainer = $("<div id='dialogContainer'></div>");
            $("body").append(dialogContainer);
        }
        return dialogContainer;
    },

    getRandomNumber: function (num) {
        var rnd = {};
        rnd.today = new Date();
        rnd.seed = rnd.today.getTime();
        rnd.val = (rnd.seed * 9301 + 49297) % 233280;
        rnd.val = rnd.val / (233280.0);
        if (num && !isNaN(num)) {
            rnd.val = Math.ceil(rnd.val * num);
        }
        return rnd.val;
    },

    enumData: {
        //LogisticsNodeTypeData 
    },
};

/**  
* 格式化日期 
* 
* @param format  
*            如： yyyy/MM/dd hh:mm:ss:S
* @return  
*/
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    };
    if (/(y+)/.test(format))
        format = format.replace(RegExp.$1,
            (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(format))
            format = format.replace(RegExp.$1,
                RegExp.$1.length == 1 ? o[k] :
                    ("00" + o[k]).substr(("" + o[k]).length));
    return format;
};

/**  
* 左补齐字符串 
* 
* @param nSize  
*            要补齐的长度  
* @param ch  
*            要补齐的字符  
* @return  
*/
String.prototype.padLeft = function (nSize, ch) {
    var len = 0;
    var s = this ? this : "";
    ch = ch ? ch : '0'; // 默认补0  

    len = s.length;
    while (len < nSize) {
        s = ch + s;
        len++;
    }
    return s;
};

/**  
* 右补齐字符串  
*   
* @param nSize  
*            要补齐的长度  
* @param ch  
*            要补齐的字符  
* @return  
*/
String.prototype.padRight = function (nSize, ch) {
    var len = 0;
    var s = this ? this : "";
    ch = ch ? ch : '0'; // 默认补0  

    len = s.length;
    while (len < nSize) {
        s = s + ch;
        len++;
    }
    return s;
};

/**  
* 左移小数点位置（用于数学计算，相当于除以Math.pow(10,scale)）  
*   
* @param scale  
*            要移位的刻度  
* @return  
*/
String.prototype.movePointLeft = function (scale) {
    var s, s1, s2, ch, ps, sign;
    ch = '.';
    sign = '';
    s = this ? this : "";

    if (scale <= 0) return s;
    ps = s.split('.');
    s1 = ps[0] ? ps[0] : "";
    s2 = ps[1] ? ps[1] : "";
    if (s1.slice(0, 1) == '-') {
        s1 = s1.slice(1);
        sign = '-';
    }
    if (s1.length <= scale) {
        ch = "0.";
        s1 = s1.padLeft(scale);
    }
    return sign + s1.slice(0, -scale) + ch + s1.slice(-scale) + s2;
};

/**  
* 右移小数点位置（用于数学计算，相当于乘以Math.pow(10,scale)）  
*   
* @param scale  
*            要移位的刻度  
* @return  
*/
String.prototype.movePointRight = function (scale) {
    var s, s1, s2, ch, ps;
    ch = '.';
    s = this ? this : "";

    if (scale <= 0) return s;
    ps = s.split('.');
    s1 = ps[0] ? ps[0] : "";
    s2 = ps[1] ? ps[1] : "";
    if (s2.length <= scale) {
        ch = '';
        s2 = s2.padRight(scale);
    }
    return s1 + s2.slice(0, scale) + ch + s2.slice(scale, s2.length);
};

/**  
* 移动小数点位置（用于数学计算，相当于（乘以/除以）Math.pow(10,scale)）  
*   
* @param scale  
*            要移位的刻度（正数表示向右移；负数表示向左移动；0返回原值）  
* @return  
*/
String.prototype.movePoint = function (scale) {
    if (scale >= 0)
        return this.movePointRight(scale);
    else
        return this.movePointLeft(-scale);
};

//乘法
Number.prototype.mul = function (arg) {
    var n, n1, n2, s, s1, s2, ps;

    s1 = this.toString();
    ps = s1.split('.');
    n1 = ps[1] ? ps[1].length : 0;

    s2 = arg.toString();
    ps = s2.split('.');
    n2 = ps[1] ? ps[1].length : 0;

    n = n1 + n2;
    s = Number(s1.replace('.', '')) * Number(s2.replace('.', ''));
    s = s.toString().movePoint(-n);
    return Number(s);
};

//除法
Number.prototype.div = function (arg) {
    var n, n1, n2, s, s1, s2, ps;

    s1 = this.toString();
    ps = s1.split('.');
    n1 = ps[1] ? ps[1].length : 0;

    s2 = arg.toString();
    ps = s2.split('.');
    n2 = ps[1] ? ps[1].length : 0;

    n = n1 - n2;
    s = Number(s1.replace('.', '')) / Number(s2.replace('.', ''));
    s = s.toString().movePoint(-n);
    return Number(s);
};

//减法
Number.prototype.sub = function (arg) {
    var n, n1, n2, s, s1, s2, ps;

    s1 = this.toString();
    ps = s1.split('.');
    n1 = ps[1] ? ps[1].length : 0;

    s2 = arg.toString();
    ps = s2.split('.');
    n2 = ps[1] ? ps[1].length : 0;

    n = n1 > n2 ? n1 : n2;
    s = Number(s1.movePoint(n)) - Number(s2.movePoint(n));
    s = s.toString().movePoint(-n);
    return Number(s);
};

//加法
Number.prototype.add = function (arg) {
    var n, n1, n2, s, s1, s2, ps;

    s1 = this.toString();
    ps = s1.split('.');
    n1 = ps[1] ? ps[1].length : 0;

    s2 = arg.toString();
    ps = s2.split('.');
    n2 = ps[1] ? ps[1].length : 0;

    n = n1 > n2 ? n1 : n2;
    s = Number(s1.movePoint(n)) + Number(s2.movePoint(n));
    s = s.toString().movePoint(-n);
    return Number(s);
};

//四舍五入 scale：保留位数
Number.prototype.toFixed = function (scale) {
    var s, s1, s2, start;

    s1 = this + "";
    start = s1.indexOf(".");
    s = s1.movePoint(scale);

    if (start >= 0) {
        s2 = Number(s1.substr(start + scale + 1, 1));
        if (s2 >= 5 && this >= 0 || s2 < 5 && this < 0) {
            s = Math.ceil(s);
        } else {
            s = Math.floor(s);
        }
    }

    return s.toString().movePoint(-scale);
};