using System.Web;
using System.Web.Optimization;

namespace WorkFlowManagement
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/script").Include(
                      "~/Scripts/core/popper.min.js",
                      "~/Scripts/core/bootstrap-material-design.min.js",
                      "~/Scripts/plugins/perfect-scrollbar.jquery.min.js",
                      "~/Scripts/plugins/moment.min.js",
                      "~/Scripts/plugins/sweetalert2.js",
                      "~/Scripts/plugins/jquery.dataTables.min.js",
                      "~/Scripts/plugins/bootstrap-notify.js",
                      "~/Scripts/material-dashboard.js"
                      ));

            bundles.Add(new StyleBundle("~/css").Include(
                      "~/Content/style.css",
                      "~/Content/custom.css"));
        }
    }
}
