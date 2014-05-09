define({
    repository: {
        find: function(parmas) {
            if (parmas.findAll && typeof (parmas.findAll) == "function" && $.isArray(parmas.query.values)) {
                var datas = parmas.findAll();
                var datasToReturn = [];
                for (var idx in datas) {
                    if (parmas.query.values.indexOf(datas[idx][parmas.query.field]) >= 0) {
                        datasToReturn.push(datas[idx]);
                    }
                }
                return datasToReturn;
            }
            return null;
        },
        findOne: function (parmas) {
            if (parmas.findAll && typeof (parmas.findAll) == "function") {
                var datas = parmas.findAll();
                if (!!parmas.query.field) {
                    for (var idx in datas) {
                        if (datas[idx][parmas.query.field] == parmas.query.value) {
                            return datas[idx];
                        }
                    }
                } else {
                    return datas[parmas.query.value];
                }
            }
            return null;
        },
        findAll: function (params, reload) {
            var cur = this;
            if (params.url) {
                if (!this[params.url]) {
                    reload = true;
                }
                if (reload) {
                    $.ajax({
                        type: "post",
                        url: global.baseUrl + params.url,
                        async: false,
                        success: function (res) {
                            if (!!params.cacheField) {
                                cur[params.url] = {};
                                $.each(res, function(idx, data) {
                                    cur[params.url][data[params.cacheField]] = data;
                                });
                            } else {
                                cur[params.url] = res;
                                reload = false;
                            }
                        }
                    });
                }
                if (params.exclude) {
                    var datas = [];
                    if ($.isArray(params.exclude)) {
                        var checkParams = [];
                        var isInArray = [];
                        for (var j in params.exclude) {
                            checkParams.push("arguments[0].exclude[" + j + "].field && arguments[0].exclude[" + j + "].values && $.isArray(arguments[0].exclude[" + j + "].values)");
                            isInArray.push("($.inArray(arguments[1][arguments[0].exclude[" + j + "].field],arguments[0].exclude[" + j + "].values) < 0)");
                        }
                        var checkParamsFunc = new Function("return (" + checkParams.toString().replace(/,/g, " && ") + ")");
                        if (checkParamsFunc(params)) {
                            var isInArrayFunc = new Function("return (" + isInArray.toString().replace(/\),\(/g, ")&&(") + ")");
                            for (var i in cur[params.url]) {
                                if ($.isArray(cur[params.url][i])) {
                                    $.each(cur[params.url][i], function (k, data) {
                                        if (isInArrayFunc(params, data)) {
                                            datas.push(data);
                                        }
                                    });
                                }
                                else if (isInArrayFunc(params, cur[params.url][i])) {
                                    datas.push(cur[params.url][i]);
                                }
                            }
                        }
                    } else {
                        if (params.exclude.field && params.exclude.values && $.isArray(params.exclude.values)) {
                            for (var ii in cur[params.url]) {
                                if ($.isArray(cur[params.url][ii])) {
                                    $.each(cur[params.url][ii], function (k, data) {
                                        if ($.inArray(data[params.exclude.field], params.exclude.values) < 0) {
                                            datas.push(data);
                                        }
                                    });
                                }
                                else if ($.inArray(cur[params.url][ii][params.exclude.field], params.exclude.values) < 0) {
                                    datas.push(cur[params.url][ii]);
                                }
                            }
                        }
                    }
                    return datas;
                }
                return this[params.url];
            }
            return null;
        }
    },
    
    /****************************************************************************/
    /****************************** 动态数据 ************************************/
    /****************************************************************************/
    
    getDatas: function (url, isChanged, params) {
        var cur = require("dataCache");
        var newParams = { url: url };
        $.extend(newParams, params);
        return cur.repository.findAll(newParams, isChanged);
    },

   
    
    /****************************************************************************/
    /******************************** 枚举 **************************************/
    /****************************************************************************/
    
    getEnums: function (url, params) {
        var cur = require("dataCache");
        var newParams = { url: url };
        $.extend(newParams, params);
        var ruturnVal = cur.repository.findAll(newParams);
        return ruturnVal._options ? ruturnVal._options : ruturnVal;
    },

    
});