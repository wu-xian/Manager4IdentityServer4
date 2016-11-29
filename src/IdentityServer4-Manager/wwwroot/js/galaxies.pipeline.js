//jQuery.galaxies.pipeline = function (response, succeccCallback, errorCallback, forbidenCallback) {
//    if (response.result == "Success") {
//        if (succeccCallback)
//            succeccCallback(response);
//        return;
//    }
//    else if (response.result == "Failed") {
//        if (errorCallback)
//            errorCallback(response);
//        return;
//    }
//    else if (response.result == "Forbiden") {
//        if (succeccCallback)
//            forbidenCallback(response);
//    }
//    return;
//}
(function ($) {
    $.galaxies = {
        pipeline: function (response, succeccCallback, errorCallback, forbidenCallback) {
            if (response.result == "Success") {

                //custom 
                alert(response.message);

                if (succeccCallback)
                    succeccCallback(response);
                return;
            }
            else if (response.result == "Failed") {
                if (errorCallback)
                    errorCallback(response);
                return;
            }
            else if (response.result == "Forbiden") {
                alert("权限禁止");
                if (forbidenCallback)
                    forbidenCallback(response);
            }
        }
    }
})(jQuery);