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

    $('#tb_user').bootstrapTable({
        url: '/admin/user/query',
        pagination: true,
        pageNumber: 1,
        pageSize: 5,
        queryParams: queryParams,
        sidePagination: "server",
        idField: "id",

        columns: [
       {
           field: 'userId',
           title: '用户编号',
           align: 'center',
           valign: 'middle',
           sortable: true,
           visible: false,
           width: 150
       },
       {
           field: 'userName',
           title: '用户名',
           align: 'center',
           valign: 'middle',
           width: 250
       },
        {
            field: 'roleId',
            title: '角色编号',
            align: 'center',
            valign: 'middle',
            visible: false,
            width: 250
        },
        {
            field: 'roleName',
            title: '角色',
            align: 'center',
            valign: 'middle',
            width: 250
        },
       {
           field: 'realName',
           title: '姓名',
           align: 'center',
           valign: 'middle',
           width: 250
       },
       {
           field: 'phoneNo',
           title: '电话号',
           align: 'center',
           valign: 'middle',
           width: 250
       },
       {
           field: 'email',
           title: '电子邮件',
           align: 'center',
           valign: 'middle',
           width: 250
       },
       {
           field: 'loginTimes',
           title: '登陆次数',
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
    '<button class="glyphicon glyphicon-home"></button>' +
    '<button class="glyphicon glyphicon-cog" title="modify" onclick="modifyUser(\'' + row.userId + '\',\'' + row.userName + '\')"></button>' +
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