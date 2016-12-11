$(function () {
    tableInit();
    eventInit();
})

function getUserClaims(userId) {
    $("#userModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/user/claimView',
            {
                userId: userId
            },
            function (responseView) {
                $("#contentBody").html("");
                $("#contentBody").html(responseView);
            },
            {});
}

function getUserRoles(userId) {
    $("#userModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/user/roleView',
        {
            userId: userId
        },
        function (responseView) {
            $("#contentBody").html("");
            $("#contentBody").html(responseView);
        },
        {});
}

function removeUserClaim(userId, claimType, claimValue) {
    $.app.get('/user/removeClaim',
    {
        claimType: claimType,
        claimValue: claimValue,
        userId: userId
    },
    function (responseView) {
        getUserClaims(userId);
    },
    {});
}

function addUserClaimsView(userId) {
    $("#userModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/user/addClaimsView',
        {
            userId: userId
        },
        function (responseView) {
            $("#contentBody").html("");
            $("#contentBody").html(responseView);
        },
        {});
}

function addUserRoleView(userId) {
    $("#userModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/user/addUserRoleView',
        {
            userId: userId
        },
        function (responseView) {
            $("#contentBody").html("");
            $("#contentBody").html(responseView);
        },
        {});
}

function addUserRoles(userId, roleName) {
    $("#userModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/user/addToRole',
        {
            userId: userId,
            roleName: roleName
        },
        function (responseView) {
            $("#contentBody").html("");
            $("#contentBody").html(responseView);
        },
        {});
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
        '<button title="add claims"  onclick="addUserClaimsView(\'' + row.id + '\')"><i class="fa fa-plus"></i></button>' +
        '<button title="add roles"  onclick="addUserRoleView(\'' + row.id + '\')"><i class="fa fa-plus-square"></i></button>' +
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
}
