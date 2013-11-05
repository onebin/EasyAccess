var statusCodeHelper = {
    ok: 200,
    notModified: 304,
    unauthorized: 401,
    forbidden: 403,
    notFound: 404,
    failed: 417,
    error: 500,
    notImplemented: 501
};

$(function () {
    global.statusCodeHelper = statusCodeHelper;
});