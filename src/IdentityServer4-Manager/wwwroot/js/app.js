/**
 * - app.js -
 * A Light jQuery notification plugin. For websites and mobile apps.
 *
 * author: wu-xian
 * date: Dec 1, 2016
 * version: 0.0.1
 * copyright - WU XIAN
 */

(function ($) {

    $.app = {
        //loadingBox
        loadingBox: function (selector) {
            $(selector).append('<div class="overlay"><i class="fa fa-refresh fa-spin"></i></div>');
        },
        //unLoadBox
        unLoadBox: function (selector) {
            $(selector + ' .overlay').remove();
        },
        //setLoadingBtn set the style to loading on button click
        setLoadingBtn: function (selector, func, callback) {
            $(selector).on('click', function () {
                var $this = $(this);
                $this.button('loading');
                if (func) {
                    func();
                    if (callback)
                        callback();
                }
            })
        },
        resetLoadingBtn: function (selector) {
            $(selector).button('reset');
        },
        //http get
        get: function (url, data, successCallback, errorCallback, finallyCallback) {
            $.ajax({
                url: url,
                method: 'GET',
                data: data,
                success: function (response) {
                    if (successCallback)
                        successCallback(response)
                },
                error: function (info, execption) {
                    if (errorCallback)
                        errBox("请求失败 , info:" + info + ",execption:" + execption);
                },
                complete: function () {
                    if (finallyCallback)
                        finallyCallback();
                }
            });
        },
        //http post
        post: function (url, data, successCallback, errorCallback, finallyCallback) {
            $.ajax({
                url: url,
                method: 'POST',
                data: data,
                success: function (response) {
                    if (successCallback)
                        successCallback(response)
                },
                error: function (info, execption) {
                    if (errorCallback)
                        errBox("请求失败 , info:" + info + ",execption:" + execption);
                },
                complete: function () {
                    if (finallyCallback)
                        finallyCallback();
                }
            });
        },
        msgBox: function (text) {
            msgBox(text);
        },
        errBox: function (text) {
            errBox(text);
        },
        successBox: function (text) {
            successBox(text);
        },
        loadingConstant: '<div class="box"><div class="overlay" style="height:200px;"><i class="fa fa-refresh fa-spin"></i></div></div>'

    };
})(jQuery)