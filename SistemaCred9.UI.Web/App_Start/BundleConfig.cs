using System.Web;
using System.Web.Optimization;

namespace SistemaCred9.Web.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/methods_pt.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/AdminLTE/app.min.js"
                        /*"~/Scripts/chartsconfiguration.js"*/));

            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
                        "~/Scripts/AdminLTE/pages/dashboard2.js",
                        "~/Scripts/AdminLTE/demo.js"
                        /*"~/Scripts/AdminLTE/plugins/chartjs/Chart.js"*/));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                //Daqui para baixo é referente ao Template utilizado
                      "~/Scripts/AdminLTE/plugins/morris/morris.css",
                      "~/Scripts/AdminLTE/plugins/jvectormap/jquery-jvectormap-1.2.2.css",
                      "~/Scripts/AdminLTE/plugins/daterangepicker/daterangepicker-bs3.css",
                      "~/Content/AdminLTE/AdminLTE.min.css",
                      "~/Content/AdminLTE/skins/skin-blue.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
                "~/Scripts/jquery.inputmask/inputmask.js",
                "~/Scripts/jquery.inputmask/jquery.inputmask.js",
                "~/Scripts/jquery.inputmask/inputmask.extensions.js",
                "~/Scripts/jquery.inputmask/inputmask.date.extensions.js",
                //and other extensions you want to include
                "~/Scripts/jquery.inputmask/inputmask.numeric.extensions.js"));

        }
    }
}
