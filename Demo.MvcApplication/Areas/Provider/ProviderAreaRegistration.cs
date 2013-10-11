using System.Web.Mvc;
using EasyAccess.Infrastructure.Attr;

namespace Demo.MvcApplication.Areas.Provider
{
    [Menu("M03", "供应商管理", 99)]
    public class ProviderAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Provider";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Provider_default",
                "Provider/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
