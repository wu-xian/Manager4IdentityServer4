$(function () {
    tableInit();
    eventInit();
})

function tableInit() {

    function queryParams(params) {
        var temp = {
            limit: params.limit,
            offset: params.offset,

            userName: $("#UserName").val(),
            userId: $("#UserId").val(),
        };
        return temp;
    }

    var columns = [
       {
           field: 'operation',
           title: 'Operation',
           align: 'center',
           valign: 'middle',
           width: 100,
           formatter: operationFormatter
       },
        {
            field: 'id',
            title: 'ID',
            align: 'center',
            valign: 'middle',
            visible: true,
            width: 150
        },
        {
            field: 'userName',
            title: 'User Name',
            align: 'center',
            valign: 'middle',
            visible: true,
            width: 250
        },
        {
            field: 'email',
            title: 'Email',
            align: 'center',
            valign: 'middle',
            visible: true,
            width: 250
        },
        {
            field: 'phoneNumber',
            title: 'Phone Number',
            align: 'center',
            valign: 'middle',
            visible: true,
            width: 250
        }];

    function onLoadSuccess() {
        $.app.resetLoadingBtn("#query");
    }

    function onLoadError() {
        $.app.resetLoadingBtn("#query");
    }

    function loadingMessageFormatter() {
        return '<div class="overlay"><i class="fa fa-refresh fa-spin"></i></div>';
    }

    $('#tb_user').bootstrapTable({
        url: '/user/getusers',
        pagination: true,
        pageNumber: 1,
        pageSize: 5,
        queryParams: queryParams,
        sidePagination: "server",
        onLoadSuccess: onLoadSuccess,
        onLoadError: onLoadError,
        formatLoadingMessage: loadingMessageFormatter,
        idField: "id",
        columns: columns
    });

}

function eventInit() {

    //query button
    $.app.setLoadingBtn("#query", function () {
        $("#tb_user").bootstrapTable("refresh");
    });

    $(document).on('click', "#add", function () {

    });

    $(document).on("click", "#saveChange", function () {
        var userModel = {
            userId: $("#modifyBody #UserId").val(),
            roleId: $("#modifyBody #RoleId").val(),
            realName: $("#modifyBody #RealName").val()
        };
        $.ajax({
            url: '/user/getusers',
            data: userModel,
            success: function (response) {
                $.galaxies.pipeline(response);
            },
            error: function (msg) {
                alert(JSON.stringify(msg));
            }
        })
    });
}

function operationFormatter(value, row, index) {
    return '<div class="btn-group">' +
    '<button class="glyphicon glyphicon-home"></button>' +
    '<button class="glyphicon glyphicon-cog" title="modify" onclick="modifyUser(\'' + row.userId + '\')"></button>' +
    '</div>';
}

function modifyUser(userId, userName) {
    $("#win_userName").text(userName);
    $.ajax({
        url: '/admin/user/modifyview',
        data: {
            userId: userId
        },
        success: function (responseView) {
            $("#modifyBody").html(responseView);
            $("#modifyModal").modal('show');
        },
        error: function (info, execption) {
            alert("请求失败 , info:" + info + ",execption:" + execption);
        }
    })
}