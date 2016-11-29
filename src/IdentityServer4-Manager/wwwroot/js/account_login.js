function login() {
    $.ajax({
        url: '/loginAction',
        method: 'post',
        data: {
            userName: $("#userName").val(),
            password: $("#password").val()
        },
        success: function (responseData) {
            $.galaxies.pipeline(responseData, function (response) {
                window.location.href = response.data.data;
            }, function (response) {
                alert("登陆失败");
            })
        },
        error: function (msg) {
            alert(JSON.stringify(msg));
        }
    })
}