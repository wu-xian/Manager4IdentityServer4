$(function () {
    tableInit();
    eventInit();
})

function removeIdentityResource(id) {
    if (confirm("delete?")) {
        $.app.get('/identityResource/delete',
            {
                Id: id
            },
            function (responseData) {
                $.app.msgBox(responseData);
            },
            {});
    }
}

function getDetail(id) {
    $("#editorModal").modal('show');
    //$("#jsoneditor").html($.app.loadingConstant);
    $.app.get('/identityResource/detail',
        {
            Id: id
        },
        function (responseJson) {
            //$("#jsoneditor").html("");
            editor.set(responseJson);
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

            clientName: $("#identityName").val(),
            clientId: $("#identityId").val(),
        };
        return temp;
    }

    function operationFormatter(value, row, index) {
        var claimCount = row.claimCount;
        return '<div class="btn-group">' +
        '<button title="' + claimCount + 'claims"><i class="fa fa-bars"></i>' + claimCount + '</button>' +
        '<button title="client detaiil" onclick="getDetail(\'' + row.id + '\')"><i class="fa fa-eye"></i></button>' +
        '<button title="client detaiil" onclick="removeIdentityResource(\'' + row.id + '\')"><i class="fa fa-trash"></i></button>' +
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
            field: 'name',
            title: 'Name',
            align: 'center',
            valign: 'middle',
            visible: true,
            width: 250
        },
        {
            field: 'displayName',
            title: 'DisplayName',
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
        url: '/identityResource/getpaged',
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

var container = document.getElementById('jsoneditor');
var options = {};
var editor = new JSONEditor(container, options);
function eventInit() {

    //create identity resource
    $.app.setLoadingBtn("#btn_create_identityResource", function () {
        $.app.post('/identityResource/create', {
            name: $("#Name").val(),
            displayName: $("#DisplayName").val(),
            description: $("#Description").val()
        },
        function (responseData) {
            $.app.msgBox(responseData);
            $.app.resetLoadingBtn("#btn_create_identityResource");
        },
        {});
    });

    //query button
    $.app.setLoadingBtn("#query", function () {
        $("#tb").bootstrapTable("refresh");
    });

    $.app.setLoadingBtn("#btn_change_identityResource", function () {
        $.app.post('/identityResource/change',
            editor.get(),
            function (responseData) {
                $.app.msgBox(responseData);
                $.app.resetLoadingBtn("#btn_change_identityResource");
            },
            {});
    });

    $(document).on('click', "#add", function () {
        $("#identityResourceModal").modal('show');
        $("#contentBody").html($.app.loadingConstant);
        $.app.get('/identityResource/create',
            {},
            function (responseView) {
                $("#contentBody").html("");
                $("#contentBody").html(responseView);
            },
            {});
    });
}
