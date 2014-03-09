using System.Web.Mvc;
using EasyAccess.Authorization.Attr;

namespace Demo.MvcApplication.Areas.SystemSettings
{
    [Menu("M02", "系统设置", 99)]
    public class SystemSettingsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SystemSettings";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SystemSettings_default",
                "SystemSettings/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
