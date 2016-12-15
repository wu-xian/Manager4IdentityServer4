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

            roleId: $("#Id").val(),
            roleName: $("#Name").val(),
        };
        return temp;
    }

    function operationFormatter(value, row, index) {
        var claimCount = row.claimCount;
        var userCount = row.userCount;
        return '<div class="btn-group">' +
        '<button title="' + claimCount + ' claims" onclick="getClaims(\'' + row.roleId + '\')"><i class="fa fa-users"></i>' + claimCount + '</button>' +
        '<button title="' + userCount + ' users" onclick="getUsers(\'' + row.roleId + '\')"><i class="glyphicon glyphicon-list"></i>' + userCount + '</button>' +
        '<button title="edit role"  onclick="editRole(\'' + row.roleId + '\')"><i class="fa fa-plus"></i></button>' +
        '<button title="remove"  onclick="removeRole(\'' + row.roleId + '\')"><i class="fa fa-plus-square"></i></button>' +
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
            field: 'roleId',
            title: 'ID',
            align: 'center',
            valign: 'middle',
            visible: true,
            width: 150
        },
        {
            field: 'roleName',
            title: 'User Name',
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

    $('#tb').bootstrapTable({
        url: '/role/getpaged',
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
        $("#tb").bootstrapTable("refresh");
    });

    $(document).on('click', "#add", function () {
        $("#contentBody").append("123");
        $("#userModal").modal('show');
    });
}
