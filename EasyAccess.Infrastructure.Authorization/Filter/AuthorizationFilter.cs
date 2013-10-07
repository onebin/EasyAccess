using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using EasyAccess.Infrastructure.Attr;
using EasyAccess.Infrastructure.Constant;

namespace EasyAccess.Infrastructure.Authorization.Filter
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
                filterContext.Result = RedirectLoginPage();
            }
            else
            {
                VerifyPermission(filterContext);
            }
        }

        private void VerifyPermission(AuthorizationContext filterContext)
        {
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(false);

            for (int i = 0; i < attrs.Length; i++)
            {
                object attr = attrs[i];
                if (attr is PermissionAttribute)
                {
                    var permission = attr as PermissionAttribute;
                    if (!permission.UnverifyByFilter)
                    {
                        string token;
                        if (IsUserSessionOutOfDate(filterContext, out token))
                        {
                            _httpContext.Response.StatusCode = (int)StatusCode.Forbidden;
                            filterContext.Result = RedirectLoginPage();
                        }
                        else if (!AuthorizationManager.GetInstance().VerifyPermission(permission.Id, token))
                        {
                            _httpContext.Response.StatusCode = (int)StatusCode.Unauthorized;
                            filterContext.Result = new ViewResult() { ViewName = "NoPermission" };
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 判断用户Session是否过期
        /// </summary>
        public bool IsUserSessionOutOfDate(AuthorizationContext filterContext, out string roleKey)
        {
            roleKey = filterContext.HttpContext.Session[SessionConst.Token] as string;
            return roleKey == null;
        }

        /// <summary>
        /// 重定向到登录页
        /// </summary>
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
