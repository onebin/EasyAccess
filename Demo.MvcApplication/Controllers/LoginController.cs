﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EasyAccess.Infrastructure.Authorization.Controllers;
using EasyAccess.Infrastructure.Constant;
using EasyAccess.Infrastructure.Util;
using EasyAccess.Model.DTOs;
using EasyAccess.Service.IServices;

namespace Demo.MvcApplication.Controllers
{
    public class LoginController : BaseController
    {
        public ILoginService LoginService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginUser loginUser, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewData[ViewDataConst.ErrorMessage] = "用户名和密码不能为空";
            }
            else if (!LoginService.Login(loginUser))
            {
                ViewData[ViewDataConst.FailureMessage] = "用户名或密码无效";
            }
            else
            {
                return RedirectToUrl(returnUrl);
            }
            return View(loginUser);
        }

        public ActionResult Logout()
        {
            LoginService.Logout();
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
