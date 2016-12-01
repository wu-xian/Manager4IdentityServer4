$(function () {
    tableInit();
    eventInit();
})

var currentUserId = '';

function getUserClaims(userId) {
    $.ajax({
        url: '/user/claimView',
        method: 'GET',
        data: {
            userId: userId
        },
        success: function (responseView) {
            $("#modalTitle").html("User Claims");
            $("#contentBody").html("");
            $("#contentBody").html(responseView);
            $("#userModal").modal('show');
        },
        error: function (info, execption) {
            alert("请求失败 , info:" + info + ",execption:" + execption);
        }
    })
}

function getUserRoles(userId) {
    $.ajax({
        url: '/user/roleView',
        method: 'GET',
        data: {
            userId: userId
        },
        success: function (responseView) {
            $("#modalTitle").html("User Roles");
            $("#contentBody").html("");
            $("#contentBody").html(responseView);
            $("#userModal").modal('show');
        },
        error: function (info, execption) {
            alert("请求失败 , info:" + info + ",execption:" + execption);
        }
    })
}

function removeUserClaim(claimType, claimValue, userId) {
    userId = 'e34c0c82-a66f-4ccb-a420-3acff32baef7';
    $.ajax({
        url: '/user/removeClaim',
        method: 'GET',
        data: {
            claimType: claimType,
            claimValue: claimValue,
            userId: userId
        },
        success: function (responseData) {
            getUserClaims(userId);
        },
        error: function (info, execption) {
            alert("请求失败 , info:" + info + ",execption:" + execption);
        }
    });
}

function tableInit() {

    function queryParams(params) {
        var temp = {
            limit: params.limit,
            offset: params.offset,
            order: 'Id',
            isAsc: true,

            userName: $("#UserName").val(),
            userId: $("#UserId").val(),
        };
        return temp;
    }


    function operationFormatter(value, row, index) {
        var claimsCount = row.userClaims.length;
        var rolesCount = row.userRoles.length;
        return '<div class="btn-group">' +
        '<button title="' + rolesCount + ' roles" onclick="getUserRoles(\'' + row.id + '\')"><i class="fa fa-users"></i>' + rolesCount + '</button>' +
        '<button title="' + claimsCount + ' claims" onclick="getUserClaims(\'' + row.id + '\')"><i class="glyphicon glyphicon-list"></i>' + claimsCount + '</button>' +
        '<button><i class="glyphicon glyphicon-cog"></i></button>' +
        '</div>';
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
        $("#contentBody").append("123");
        $("#userModal").modal('show');
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
