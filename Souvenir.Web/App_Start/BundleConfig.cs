using System.Web.Optimization;

namespace Souvenir.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new Bundle("~/bundles/jqueryAjax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new Bundle("~/bundles/Swal").Include(
            "~/Areas/Admin/Content/Swal/sweetalert2.all.min.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new Bundle("~/bundles/AdminTest").Include(
                "~/Areas/AdminTest/Css/bootstrap.min.rtl.css",
                "~/Areas/AdminTest/Css/admin.css"));

            bundles.Add(new Bundle("~/bundles/Admin").Include(
                "~/Areas/Admin/Content/Css/bootstrap.rtl.min.css" ,
                "~/Areas/Admin/Content/Css/main.css" 
                ));

        }
    }
}
