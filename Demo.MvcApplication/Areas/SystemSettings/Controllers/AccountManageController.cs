using System.Linq;
using System.Web.Mvc;
using EasyAccess.Infrastructure.Attr;
using EasyAccess.Authorization.Controllers;
using EasyAccess.Infrastructure.Util.ConditionBuilder;
using EasyAccess.Infrastructure.Util.EasyUi;
using EasyAccess.Infrastructure.Util.PagingData;
using EasyAccess.Model.EDMs;
using EasyAccess.Service.IServices;

namespace Demo.MvcApplication.Areas.SystemSettings.Controllers
{
    [Menu("M0201", "用户管理", "/SystemSettings/AccountManage/Index")]
    public class AccountManageController : AuthorizationController
    {
        IAccountManageService AccountManageService { get; set; }

        [Permission("M0201P01", "浏览", "/SystemSettings/AccountManage/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Permission("M0201P0101", "检索用户数据", "/SystemSettings/AccountManage/GetAccountView")]
        public JsonResult GetAccountView(FormCollection formData)
        {
            var pagingCodition = GetPagingCondition(formData);
            var pg = AccountManageService.GetAccountPagingData(ConditionBuilder<Account>.Create(),
                pagingCodition);
            return Json(new EasyUiDataGrid().SetRows(pg.RecordCount, pg.RecordData.ToList()).GetJsonModel());
        }

    }
}
