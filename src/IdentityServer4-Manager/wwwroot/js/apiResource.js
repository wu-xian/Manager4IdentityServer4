$(function () {
    tableInit();
    eventInit();
})

function removeApiResource(id) {
    if (confirm("delete?")) {
        $.app.get('/apiResource/delete',
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
    $.app.get('/apiResource/detail',
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

            clientName: $("#apiResourceName").val(),
            clientId: $("#apiResourceId").val(),
        };
        return temp;
    }

    function operationFormatter(value, row, index) {
        var scopeCount = row.scopeCount;
        var secretCount = row.secretCount;
        var claimCount = row.claimCount;
        return '<div class="btn-group">' +
        '<button title"client detaiil" onclick="getDetail(\'' + row.id + '\')"><i class="fa fa-eye"></i></button>' +
        '<button title"client detaiil" onclick="removeApiResource(\'' + row.id + '\')"><i class="fa fa-trash"></i></button>' +
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
            title: 'apiResource Name',
            align: 'center',
            valign: 'middle',
            visible: true,
            width: 250
        },
        {
            field: 'displayName',
            title: 'Display Name',
            align: 'center',
            valign: 'middle',
            visible: true,
            width: 250
        },
        {
            field: 'description',
            title: 'Description',
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
        url: '/apiResource/getpaged',
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
    //create api resource
    $.app.setLoadingBtn("#btn_create_apiResource", function () {
        $.app.post('/apiResource/create', {
            name: $("#Name").val(),
            displayName: $("#DisplayName").val(),
            description: $("#Description").val()
        },
        function (responseData) {
            $.app.msgBox(responseData);
            $.app.resetLoadingBtn("#btn_create_apiResource");
        },
        {});
    });

    //query button
    $.app.setLoadingBtn("#query", function () {
        $("#tb").bootstrapTable("refresh");
    });

    $.app.setLoadingBtn("#btn_change_apiResource", function () {
        $.app.post('/apiResource/change',
            editor.get(),
            function (responseData) {
                $.app.msgBox(responseData);
                $.app.resetLoadingBtn("#btn_change_apiResource");
            },
            {});
    });

    $(document).on('click', "#add", function () {
        $("#apiResourceModal").modal('show');
        $("#contentBody").html($.app.loadingConstant);
        $.app.get('/apiResource/create',
            {},
            function (responseView) {
                $("#contentBody").html("");
                $("#contentBody").html(responseView);
            },
            {});
    });
}
