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
        }


    };
})(jQuery)