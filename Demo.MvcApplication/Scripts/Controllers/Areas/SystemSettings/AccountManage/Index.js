$(function () {
    if ($("#AccountManage_Index_GetAccountInfo").length > 0) {
        $("#AccountManage_Index_grid").datagrid({
            url: $("#AccountManage_Index_GetAccountInfo").val(),
            fitColumns: true,
            rownumbers: true,
            remoteSort: false,
            pagination: true,
            singleSelect: true,
            striped: true,
            onLoadSuccess: function (data) {
            },
            columns: [[
                        { field: 'Name', title: '姓名', width: 80, sortable: true },
                        { field: 'Sex', title: '性别', width: 40, sortable: true},
                        { field: 'Phone', title: '手机', width: 80, sortable: true },
                        { field: 'Email', title: '邮箱', width: 80, sortable: true }
            ]]
        });
    }
})