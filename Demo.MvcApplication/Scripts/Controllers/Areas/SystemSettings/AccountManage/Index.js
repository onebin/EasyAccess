var AccountManage_Index = {
    Initialize: function () {
        this._grid.Initialize();
    },
    
    _grid: {
        jqObj: $("#AccountManage_Index_grid"),
        Initialize: function() {
            if ($("#AccountManage_Index_GetAccountInfo").length > 0) {
                this.jqObj.datagrid({
                    url: $("#AccountManage_Index_GetAccountInfo").val(),
                    fit: true,
                    fitColumns: true,
                    rownumbers: true,
                    remoteSort: false,
                    pagination: true,
                    singleSelect: true,
                    striped: true,
                    columns: [[
                        { field: 'Name', title: '姓名', width: 80 },
                        { field: 'Sex', title: '性别', width: 40 },
                        { field: 'Phone', title: '手机', width: 80 },
                        { field: 'Email', title: '邮箱', width: 80 },
                        { field: 'Operation', title: '操作', align: 'center', width: 50, formatter: AccountManage_Index._grid.OperationFormatter }
                    ]]
                });
            }
        },
        
        OperationFormatter: function (value, row, index) {
            var operations = "";
            if (row.editing) {
                operations += '<a class="inlineBtn_save" title="保存" href="javascript:easyuiHelper.datagrid.endEdit(\'#AccountManage_Index_grid\', ' + index + ');"></a> ';
                operations += '<a class="inlineBtn_cancel" title="取消" href="javascript:easyuiHelper.datagrid.cancelEdit(\'#AccountManage_Index_grid\', ' + index + ');" ></a>';
            } else {
                if ($("#AccountManage_Index_EditAccountInfo").length > 0) {
                    operations += '<a class="inlineBtn_edit" title="编辑" href="javascript: easyuiHelper.datagrid.beginEdit(\'#AccountManage_Index_grid\', ' + index + ');"></a> ';
                }
                if ($("#AccountManage_Index_DeleteAccountInfo").length > 0) {
                    operations += '<a class="inlineBtn_delete" title="删除" href="javascript: AccountManage_Index._grid.RemoveItem(' + index + ', ' + row.Id + ');" ></a>';
                }
            }
            return operations;
        },

        AddItem: function () {
            var gridObj = AccountManage_Index._grid.jqObj;
            var editIndex = gridObj.datagrid("getRows").length;
            gridObj.datagrid("appendRow", {
                Id: 0,
                Name: "",
                Sex: '',
                Phone: '',
                Email: ''
            });
            gridObj.datagrid('selectRow', editIndex).datagrid('beginEdit', editIndex);
        },

        SaveItem: function (index, row) {
            $.post(
                $("#AccountManage_Index_EditAccountInfo").val(),
                {
                },
                function (res) {
                    var gridObj = AccountManage_Index._grid.jqObj;
                    easyuiHelper.messager.alert(
                        {
                            result: res,
                            success: function () {
                                row.Id = res.AppendData;
                                row.editing = false;
                                gridObj.datagrid('updateRow', { index: index });
                            },
                            fail: function () {
                                gridObj.datagrid("beginEdit", index);
                            }
                        }
                    );
                }
            );
        },

        RemoveItem: function (index, id) {
            easyuiHelper.messager.confirm.remove(function () {
                $.post(
                    $("#AccountManage_Index_DeleteAccountInfo").val(),
                    {
                        id: id
                    },
                    function (res) {
                        if (res.ResultType == statusCodeHelper.ok) {
                            global.Toast(true);
                            var gridObj = AccountManage_Index._grid.jqObj;
                            gridObj.datagrid("deleteRow", index);
                            var rowCount = gridObj.datagrid("getData").total;
                            for (var i = index; i < rowCount; i++) {
                                gridObj.datagrid("updateRow", { index: i, row: { editing: false } });
                            }
                        }
                    }
                );
            });
        }
    }
};

$(function () {
    AccountManage_Index.Initialize();
});