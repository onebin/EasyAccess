using System.Web.Mvc;
using EasyAccess.Infrastructure.Attr;
using EasyAccess.Authorization.Controllers;

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

    }
}
