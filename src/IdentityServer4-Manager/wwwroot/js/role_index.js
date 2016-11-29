$(function () {
    tableInit();
    eventInit();
})

function tableInit() {

    function queryParams(params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的  
            limit: params.limit,   //页面大小  
            offset: params.offset,  //页码  

            userName: $("#UserName").val(),
            roleId: $("#RoleId").val(),
            inTimeSDate: $("#InTimeSDate").val(),
            inTimeEDate: $("#InTimeEDate").val()
        };
        return temp;
    }

    $('#tb_role').bootstrapTable({
        url: '/admin/role/list',
        pagination: true,
        pageNumber: 1,
        pageSize: 5,
        queryParams: queryParams,
        sidePagination: "server",
        idField: "id",
        detailView: true,
        detailFormatter: roleDetailFormatter,
        columns: [
       {
           field: 'id',
           title: '权限编号',
           align: 'center',
           valign: 'middle',
           sortable: true,
           visible: false,
           width: 150
       },
       {
           field: 'name',
           title: '权限名',
           align: 'center',
           valign: 'middle',
           width: 250
       },
        {
            field: 'description',
            title: '描述',
            align: 'center',
            valign: 'middle',
            width: 250
        },
       {
           field: 'inTime',
           title: '添加时间',
           align: 'center',
           valign: 'middle',
           width: 250
       },
       {
           field: 'operation',
           title: '操作',
           align: 'center',
           valign: 'middle',
           width: 250,
           formatter: operationFormatter
       }]
    });

}

function eventInit() {
    //query button 
    $(document).on("click", "#query", function () {
        $("#tb_user").bootstrapTable('refresh');
    });

    $(document).on("click", "#saveChange", function () {
        var userModel = {
            userId: $("#modifyBody #UserId").val(),
            roleId: $("#modifyBody #RoleId").val(),
            realName: $("#modifyBody #RealName").val()
        };
        $.ajax({
            url: '/admin/user/modify',
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
    '<button class="glyphicon glyphicon-plus"></button>' +
    '<button class="glyphicon glyphicon-trash"></button>' +
    '<button class="glyphicon glyphicon-cog" title="modify" onclick="modifyUser(\'' + row.userId + '\',\'' + row.userName + '\')"></button>' +
    '</div>';
}

function roleDetailFormatter(index, row) {
    var tableString = "";
    var programData = [];
    $.ajax({
        url: '/admin/role/programforweb',
        async: false,
        data: {
            roleId: row.id
        },
        contentType: 'json',
        success: function (response) {
            programData = response;
        },
        error: function () {
            alert("请求失败");
        }
    })
    tableString += "<table class='table'>";
    tableString += "<thead>"
    tableString += "<tr><td>Area</td><td>Controller</td><td>Action</td><td class='td-operator'>&nbsp;</td></tr>";
    $.each(programData, function (index, data) {
        tableString += "<tr>";
        tableString += "<td>" + data.area + "</td>";
        tableString += "<td>" + data.controller + "</td>";
        tableString += "<td>" + data.action + "</td>";
        tableString += "<td class='td-operator'><button class='glyphicon glyphicon-minus'></button></td>";
        tableString += "</tr>";
    });
    tableString += "</thead>";
    tableString += "</table>";
    return tableString;
}