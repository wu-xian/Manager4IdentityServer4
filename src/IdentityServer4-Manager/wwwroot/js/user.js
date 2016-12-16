$(function () {
    tableInit();
    eventInit();
})

function getUserClaims(userId) {
    $("#userModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/user/claims',
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
    $.app.get('/user/roles',
        {
            userId: userId
        },
        function (responseView) {
            $("#contentBody").html("");
            $("#contentBody").html(responseView);
        },
        {});
}

function createUserRole() {
    var requestData = {
        userId: $("#txt_userId").val(),
        roleName: $("#newRoleName").val()
    };
    if (requestData.roleName) {
        $.app.post('/user/addToRole',
            requestData,
            function (responseData) {
                getUserRoles(requestData.userId);
                $.app.msgBox(JSON.stringify(responseData));
            });
    }
    else {
        $.app.resetLoadingBtn("#btn_create_role");
        $.app.msgBox("role name could not be empty");
    }
}

function deleteUserRole() {
    var requestData = {
        roleName: roleName,
        userId: $("#txt_userId").val()
    };
    $.app.post('/user/removeFromRole',
    requestData,
    function (responseData) {
        $.app.msgBox(responseData);
        getUserRoles(userId);
    },
    {});
}

function createUserClaim() {
    var requestData = {
        userId: $("#txt_userId").val(),
        claimType: $("#newClaimType").val(),
        claimValue: $("#newClaimValue").val()
    };
    if (requestData.claimType && requestData.claimValue) {
        $.app.post('/user/createClaim',
            requestData,
            function (responseData) {
                getUserClaims(requestData.userId);
                $.app.msgBox(JSON.stringify(responseData));
            });
    }
    else {
        $.app.resetLoadingBtn("#btn_create_claim");
        $.app.msgBox("claim type and claim value could not be empty");
    }
}

function deleteUserClaim(userId, claimType, claimValue) {
    $.app.post('/user/deleteClaim',
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

function createUser() {
    var requestData = {
        userName: $("#txt_new_username").val(),
        password: $("#txt_new_password").val()
    };
    if (requestData.userName && requestData.password) {
        $.app.post('/user/create',
            requestData,
            function (responseData) {
                tableReload();
                $.app.msgBox(JSON.stringify(responseData));
                $("#userModal").modal('hide');
            });
    }
    else {
        $.app.msgBox("user name and password could not be empty");
    }
}

function tableReload() {
    $("#tb").bootstrapTable("refresh");
}

function addNewLineForClaim() {
    if (!$("#newLineForClaim")[0]) {
        var newLine = '<tr id="newLineForClaim">' +
            '<td> <button id="btn_create_claim" data-loading-text="<i class=\'fa fa-spinner fa-spin\'></i>Saving..." title="save the claim" ><i class="fa fa-users"></i>Save</button> </td>' +
            '<td><input id="newClaimType" type="text" /></td><td><input id="newClaimValue" type="text" /></td>' +
            '</tr>';
        $("#userClaim tbody").prepend(newLine);
    }
}

function addNewLineForRole() {
    if (!$("#newLineForRole")[0]) {
        var newLine = '<tr id="newLineForRole">' +
            '<td> <button id="btn_create_role" data-loading-text="<i class=\'fa fa-spinner fa-spin\'></i>Saving..." title="save the claim" ><i class="fa fa-users"></i>Save</button> </td>' +
            '<td><input id="newRoleName" type="text" /></td>' +
            '</tr>';
        $("#userRole tbody").prepend(newLine);
    }
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

    $('#tb').bootstrapTable({
        url: '/user/getpaged',
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

    $.app.setLoadingBtn("#btn_create_role", function () {
        createUserRole();
    });

    $.app.setLoadingBtn("#btn_create_claim", function () {
        createUserClaim();
    });

    $.app.setLoadingBtn("#btn_create_user", function () {
        createUser();
    });

    //query button
    $.app.setLoadingBtn("#query", function () {
        $("#tb").bootstrapTable("refresh");
    });

    $(document).on('click', "#btn_open_roleview", function () {
        $("#userModal").modal('show');
        $("#contentBody").html($.app.loadingConstant);
        $.app.get('/user/create',
                {},
                function (responseView) {
                    $("#contentBody").html("");
                    $("#contentBody").html(responseView);
                },
                {});
    });
}
