$(function () {
    tableInit();
    eventInit();
})

function getUserClaims(ClientId) {
    $("#clientModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/user/claimView',
            {
                ClientId: ClientId
            },
            function (responseView) {
                $("#contentBody").html("");
                $("#contentBody").html(responseView);
            },
            {});
}

function getUserRoles(ClientId) {
    $("#clientModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/user/roleView',
        {
            ClientId: ClientId
        },
        function (responseView) {
            $("#contentBody").html("");
            $("#contentBody").html(responseView);
            $("#clientModal").modal('show');
        },
        {});
}

function removeUserClaim(ClientId, claimType, claimValue) {
    $.app.get('/user/removeClaim',
    {
        claimType: claimType,
        claimValue: claimValue,
        ClientId: ClientId
    },
    function (responseView) {
        getUserClaims(ClientId);
    },
    {});
}

function addUserClaimsView(ClientId) {
    $("#clientModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/user/addClaimsView',
        {
            ClientId: ClientId
        },
        function (responseView) {
            $("#contentBody").html("");
            $("#contentBody").html(responseView);
            $("#clientModal").modal('show');
        },
        {});
}

function addUserRoleView(ClientId) {
    $("#clientModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/user/addUserRoleView',
        {
            ClientId: ClientId
        },
        function (responseView) {
            $("#contentBody").html("");
            $("#contentBody").html(responseView);
            $("#clientModal").modal('show');
        },
        {});
}

function addUserRoles(ClientId, roleName) {
    $("#clientModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/user/addToRole',
        {
            ClientId: ClientId,
            roleName: roleName
        },
        function (responseView) {
            $("#contentBody").html("");
            $("#contentBody").html(responseView);
            $("#clientModal").modal('show');
        },
        {});
}

function getClient(id) {
    $("#clientModal").modal('show');
    $("#contentBody").html($.app.loadingConstant);
    $.app.get('/client/get',
        {
            Id: id
        },
        function (responseView) {
            $("#contentBody").html("");
            $("#contentBody").html(responseView);
            $("#clientModal").modal('show');
        },
        {});
}

function removeClient(id) {
    $.app.get('/client/removeClient',
        {
            Id: id
        },
        function (responseData) {
            $.app.msgBox(responseData);
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

            clientName: $("#clientName").val(),
            clientId: $("#clientId").val(),
        };
        return temp;
    }

    function operationFormatter(value, row, index) {
        var scopeCount = row.scopes.length;
        return '<div class="btn-group">' +
        '<button title="' + scopeCount + ' scopes" onclick="getUserRoles(\'' + row.id + '\')"><i class="fa fa-bars"></i>' + scopeCount + '</button>' +
        '<button title"client detaiil" onclick="getClient(\'' + row.id + '\')"><i class="fa fa-bars"></i></button>' +
        '<button title"client detaiil" onclick="removeClient(\'' + row.id + '\')"><i class="fa fa-trash"></i></button>' +
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
            visible: false,
            width: 150
        },
        {
            field: 'clientName',
            title: 'User Name',
            align: 'center',
            valign: 'middle',
            visible: true,
            width: 250
        },
        {
            field: 'clientId',
            title: 'Client ID',
            align: 'center',
            valign: 'middle',
            visible: true,
            width: 250
        },
        {
            field: 'clientUri',
            title: 'Client URI',
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

    $('#tb_client').bootstrapTable({
        url: '/client/getclients',
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
        $("#tb_client").bootstrapTable("refresh");
    });

    $(document).on('click', "#add", function () {
        $("#clientModal").modal('show');
        $("#contentBody").html($.app.loadingConstant);
        $.app.get('/client/addClientView',
            {},
            function (responseView) {
                $("#contentBody").html("");
                $("#contentBody").html(responseView);
            },
            {});
    });
}
