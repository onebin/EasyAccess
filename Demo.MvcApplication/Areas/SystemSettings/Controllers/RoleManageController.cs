using System.Linq;
using System.Web.Mvc;
using EasyAccess.Infrastructure.Attr;
using EasyAccess.Authorization.Controllers;
using EasyAccess.Infrastructure.Constant;
using EasyAccess.Infrastructure.Util;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.EasyUi;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Service.IServices;

namespace Demo.MvcApplication.Areas.SystemSettings.Controllers
{

    [Menu("M0202", "角色管理", "/SystemSettings/RoleManage/Index")]
    public class RoleManageController : AuthorizationController
    {

        [Permission("M0202P01", "浏览", "/SystemSettings/RoleManage/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Permission("M0202P0101", "检索角色信息", "/SystemSettings/RoleManage/GetRoleInfo")]
        public JsonResult GetRoleInfo(FormCollection formData)
        {
            var pagingCodition = GetPagingCondition(formData);
            var pg = Role.GetPagingDtoData<RoleDto>(pagingCodition);
            return Json(new EasyUiDataGrid().SetRows(pg.RecordCount, pg.RecordData.ToList()).GetSerializableModel());
        }


        [Permission("M0202P010101", "编辑角色信息", "/SystemSettings/RoleManage/EditRoleInfo")]
        public JsonResult EditRoleInfo(Account account)
        {
            var result = new OperationResult(StatusCode.Failed);

            return Json(result);
        }

        [Permission("M0202P010102", "删除角色信息", "/SystemSettings/RoleManage/DeleteRoleInfo")]
        public JsonResult DeleteRoleInfo(long accountId)
        {
            var result = new OperationResult(StatusCode.Failed);

            return Json(result);
        }
    }
}
