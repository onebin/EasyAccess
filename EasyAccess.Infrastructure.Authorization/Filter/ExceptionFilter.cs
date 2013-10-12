using System.Web.Mvc;
using EasyAccess.Infrastructure.Constant;

namespace EasyAccess.Authorization.Filter
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.ViewData[ViewConst.ErrorMessage] = filterContext.Exception.Message;
            filterContext.Result = new ViewResult()
            {
                ViewName = ViewConst.ErrorPageName,
                ViewData = filterContext.Controller.ViewData,
            };
            filterContext.ExceptionHandled = true;
        }
    }  
}
