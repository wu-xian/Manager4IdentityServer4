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
            userId: $("#UserId").val(),
        };
        return temp;
    }

    $('#tb_user').bootstrapTable({
        url: '/user/getusers',
        pagination: true,
        pageNumber: 1,
        pageSize: 5,
        queryParams: queryParams,
        sidePagination: "server",
        idField: "id",

        columns: [
       {
           field: 'operation',
           title: '操作',
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

function operationFormatter(value, row, index) {
    return '<div class="btn-group">' +
    '<button class="glyphicon glyphicon-home"></button>' +
    '<button class="glyphicon glyphicon-cog" title="modify" onclick="modifyUser(\'' + row.userId + '\')"></button>' +
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