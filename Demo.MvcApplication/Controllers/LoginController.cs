using System.Web.Mvc;
using EasyAccess.Authorization.Controllers;
using EasyAccess.Infrastructure.Constant;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.VOs;
using EasyAccess.Service.IServices;

namespace Demo.MvcApplication.Controllers
{
    public class LoginController : BaseController
    {
        public ILoginSvc LoginSvc { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginUser loginUser, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewData[ViewConst.ErrorMessage] = "用户名和密码不能为空";
            }
            else if (!LoginSvc.Login(loginUser))
            {
                ViewData[ViewConst.FailureMessage] = "用户名或密码无效";
            }
            else
            {
                return RedirectToUrl(returnUrl);
            }
            return View(loginUser);
        }

        public ActionResult Logout()
        {
            LoginSvc.Logout();
            Session.RemoveAll();
            return RedirectToUrl("/Login/Index?ReturnUrl=%2f");
        }

        private ActionResult RedirectToUrl(string url)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
