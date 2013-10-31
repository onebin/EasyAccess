var statusCodeHelper = {
    ok: 200,
    failed: 417,
    error: 500,
    unauthorized: 401,
    forbidden: 403,
    notImplemented: 501
};

$(function () {
    global.statusCodeHelper = statusCodeHelper;
});