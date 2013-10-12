using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System.Web.Security;
using EasyAccess.Infrastructure.Attr;
using EasyAccess.Infrastructure.Constant;

namespace EasyAccess.Authorization.Filter
{
    public class AuthorizationFilter : AuthorizeAttribute
    {
        private HttpContextBase _httpContext;

        public AuthorizationFilter() { }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            _httpContext = httpContext;
            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                VerifyPermission(filterContext);
            }
        }

        private void VerifyPermission(AuthorizationContext filterContext)
        {
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(false);
            var identity = filterContext.HttpContext.User.Identity as FormsIdentity;
            var mgr = AuthorizationManager.GetInstance();
            if (identity != null)
            {
                var token = identity.Ticket.UserData;

                if (mgr.IsExistToken(token))
                {
                    foreach (var attr in attrs)
                    {
                        if (attr is PermissionAttribute)
                        {
                            var permission = attr as PermissionAttribute;
                            if (!permission.UnverifyByFilter)
                            {
                                if (!mgr.VerifyPermission(permission.Id, token))
                                {
                                    _httpContext.Response.StatusCode = (int) StatusCode.Unauthorized;
                                    filterContext.Controller.ViewData[ViewConst.ErrorMessage] = "没有【" + permission.Name + "】的权限";
                                    filterContext.Result = new ViewResult()
                                        {
                                            ViewName = ViewConst.ErrorPageName,
                                            ViewData = filterContext.Controller.ViewData
                                        };
                                }
                            }
                        }
                    }
                }
                else
                {
                    _httpContext.Response.StatusCode = (int)StatusCode.Error;
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
            else
            {
                _httpContext.Response.StatusCode = (int)StatusCode.Forbidden;
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        /// <summary>
        /// 重定向到登录页
        /// </summary>
        [Obsolete("使用FormsAuthentication.RedirectToLoginPage代替")]
        public ActionResult RedirectLoginPage()
        {
            return new RedirectToRouteResult("Default",
                new RouteValueDictionary(
                    new
                    {
                        controller = "Login",
                        action = "Logout"
                    }
                )
            );
        }
    }
}
