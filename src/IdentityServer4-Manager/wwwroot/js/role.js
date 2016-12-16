$(function () {
    tableInit();
    eventInit();
})

function getClaims(roleId) {
    $("#operationModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/role/claims',
            {
                roleId: roleId
            },
            function (responseView) {
                $("#contentBody").html("");
                $("#contentBody").html(responseView);
            },
            {});
}

function getUsers(roleName) {
    $("#operationModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/role/users',
            {
                roleName: roleName
            },
            function (responseView) {
                $("#contentBody").html("");
                $("#contentBody").html(responseView);
            },
            {});
}

function getCreateView() {
    $("#operationModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/role/create',
            {},
            function (responseView) {
                $("#contentBody").html("");
                $("#contentBody").html(responseView);
            },
            {});
}

function createClaim() {
    var roleId = $("#claimRoleId").val();
    var claimType = $("#newClaimType").val();
    var claimValue = $("#newClaimValue").val();
    if (claimType && claimValue) {
        $.app.post('/role/createClaim', {
            roleId: roleId,
            claimType: claimType,
            claimValue: claimValue
        }, function (responseData) {
            getClaims(roleId);
        },
        function () {
            $.app.resetLoadingBtn("#btn_create_claim");
        },
        function () {
            $.app.resetLoadingBtn("#btn_create_claim");
        });
    }
    else {
        $.app.msgBox("claim type and claim value could not be empty");
    }
}

function createRole() {
    var newRole = {
        roleName: $("#txt_role_name").val(),
        roleNormalizedName: $("#txt_role_normalizedname").val()
    };
    $.app.post('/role/create',
        newRole,
        function () {
            $("#operationModal").modal('hide');
            tableReload();
        },
        {})
}

function deleteRoleClaim(roleId, claimType) {
    var requestData = {
        roleId: roleId,
        claimType: claimType
    };
    $.app.post('/role/deleteClaim',
        requestData,
        function (responseData) {
            getClaims(requestData.roleId);
            $.app.msgBox(JSON.stringify(responseData));
        },
        {});
}

function deleteRoleUser(roleName, userId) {
    if (confirm("delete?")) {
        var requestData = {
            roleName: roleName,
            userId: userId
        };
        $.app.post('/role/deleteuserfromrole',
            requestData,
            function (responseData) {
                getUsers();
                $.app.msgBox(JSON.stringify(responseData));
            },
            {});
    }
}

function deleteRole(roleId) {
    if (confirm("delete?")) {
        $.app.post('/role/delete',
            {
                roleId: roleId
            },
            function (responseData) {
                $.app.msgBox(JSON.stringify(responseData));
                tableReload();
            });
    }
}

function tableReload() {
    $("#tb").bootstrapTable("refresh");
}

function addNewLine() {
    if (!$("#newLine")[0]) {
        var newLine = '<tr id="newLine">' +
            '<td> <button id="btn_create_claim" data-loading-text="<i class=\'fa fa-spinner fa-spin\'></i>Saving..." title="save the claim" ><i class="fa fa-users"></i>Save</button> </td>' +
            '<td><input id="newClaimType" type="text" /></td><td><input id="newClaimValue" type="text" /></td>' +
            '</tr>';
        $("#roleClaim tbody").prepend(newLine);
    }
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
        '<button title="' + claimCount + ' claims" onclick="getClaims(\'' + row.roleId + '\')"><i class="glyphicon glyphicon-list"></i>' + claimCount + '</button>' +
        '<button title="' + userCount + ' users" onclick="getUsers(\'' + row.roleName + '\')"><i class="fa fa-users"></i>' + userCount + '</button>' +
        '<button title="remove"  onclick="deleteRole(\'' + row.roleId + '\')"><i class="fa fa-trash"></i></button>' +
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
            title: 'Role Name',
            align: 'center',
            valign: 'middle',
            visible: true,
            width: 250
        },
        {
            field: 'normalizeName',
            title: 'Normalize Name',
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

    //save new role
    $.app.setLoadingBtn("#btn_create_role", function () {
        createRole();
    })

    //save new line
    $.app.setLoadingBtn("#btn_create_claim", function () {
        createClaim();
    });

    //query button
    $.app.setLoadingBtn("#query", function () {
        tableReload();
    });

    $(document).on('click', "#btn_open_createview", function () {
        getCreateView();
    });
}
