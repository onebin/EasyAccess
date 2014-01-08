using System.Linq;
using System.Web.Mvc;
using EasyAccess.Infrastructure.Attr;
using EasyAccess.Authorization.Controllers;
using EasyAccess.Infrastructure.Constant;
using EasyAccess.Infrastructure.Util;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.EasyUi;
using EasyAccess.Infrastructure.Util.PagingData;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;
using EasyAccess.Service.IServices;

namespace Demo.MvcApplication.Areas.SystemSettings.Controllers
{
    [Menu("M0201", "用户管理", "/SystemSettings/AccountManage/Index")]
    public class AccountManageController : AuthorizationController
    {

        [Permission("M0201P01", "浏览", "/SystemSettings/AccountManage/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Permission("M0201P0101", "检索用户信息", "/SystemSettings/AccountManage/GetAccountInfo")]
        public JsonResult GetAccountInfo(FormCollection formData)
        {
            var pagingCodition = GetPagingCondition(formData);
            var pg = Account.GetPagingDtoData<AccountDto>(pagingCodition);
            return Json(new EasyUiDataGrid().SetRows(pg.RecordCount, pg.RecordData.ToList()).GetSerializableModel());
        }


        [Permission("M0201P010101", "编辑用户信息", "/SystemSettings/AccountManage/EditAccountInfo")]
        public JsonResult EditAccountInfo(Account account)
        {
            var result = new OperationResult(StatusCode.Failed);

            return Json(result);
        }

        [Permission("M0201P010102", "删除用户信息", "/SystemSettings/AccountManage/DeleteAccountInfo")]
        public JsonResult DeleteAccountInfo(long accountId)
        {
            var result = new OperationResult(StatusCode.Failed);

            return Json(result);
        }
    }
}
