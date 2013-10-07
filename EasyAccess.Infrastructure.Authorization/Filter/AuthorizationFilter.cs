using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System.Web.Security;
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
            var identity = filterContext.HttpContext.User.Identity as FormsIdentity;
            var token = string.Empty;
            if (identity != null)
            {
                token = identity.Ticket.UserData;
            }
            else
            {
                _httpContext.Response.StatusCode = (int)StatusCode.Forbidden;
                filterContext.Result = RedirectLoginPage();
            }

            foreach (var attr in attrs)
            {
                if (attr is PermissionAttribute)
                {
                    var permission = attr as PermissionAttribute;
                    if (!permission.UnverifyByFilter)
                    {
                        if (!AuthorizationManager.GetInstance().VerifyPermission(permission.Id, token))
                        {
                            _httpContext.Response.StatusCode = (int)StatusCode.Unauthorized;
                            filterContext.Result = new ViewResult() { ViewName = "NoPermission" };
                        }
                    }
                }
            }
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
