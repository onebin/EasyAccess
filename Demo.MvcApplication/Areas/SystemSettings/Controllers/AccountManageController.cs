using System.Web.Mvc;
using EasyAccess.Infrastructure.Attr;
using EasyAccess.Authorization.Controllers;

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

    }
}
