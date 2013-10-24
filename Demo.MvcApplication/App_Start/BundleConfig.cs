using System.Web;
using System.Web.Optimization;

namespace Demo.MvcApplication
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
            //            "~/Scripts/Plugins/EasyUi/locale/easyui-lang-zh_CN.js",
            //            "~/Scripts/Plugins/EasyUi/jquery.easyui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/helper").Include(
                        "~/Scripts/Helpers/*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css"));
        }
    }
}