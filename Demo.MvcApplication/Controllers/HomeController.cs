using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyAccess.Infrastructure.Attr;
using EasyAccess.Infrastructure.Authorization.Controllers;
using EasyAccess.Infrastructure.Constant;
using EasyAccess.Service.IServices;

namespace Demo.MvcApplication.Controllers
{
    [Menu("M01", "首页", "/Home/Index")]
    public class HomeController : AuthorizationController
    {
        [Permission("M01P01", "浏览", "/Home/Index")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
