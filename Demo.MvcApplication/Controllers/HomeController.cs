using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyAccess.Infrastructure.Attr;
using EasyAccess.Infrastructure.Authorization.Controllers;

namespace Demo.MvcApplication.Controllers
{
    [Menu("M01", "首页", "/Home/Index")]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
