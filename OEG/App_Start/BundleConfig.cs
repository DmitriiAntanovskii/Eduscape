using System.Web;
using System.Web.Optimization;

namespace OEG
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryMember").Include(
                         "~/Scripts/jquery-{version}.js",
                         "~/Scripts/googleMembers.js",
                         "~/Scripts/member_page_functions.js",
                         "~/Scripts/globalize.js",
                         "~/Scripts/cultures/globalize.culture.en-AU.js",
                         "~/Scripts/Highcharts-4.0.1/js/highcharts.js",
                         "~/Scripts/CSVExport.js",
                         "~/Scripts/datepicker/js/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/page_function.js",
                        "~/Scripts/google.js",
                        "~/Scripts/youtube.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/font-awesome/css/font-awesome.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Membercss").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/sb-admin.css",
                        "~/Scripts/datepicker/css/datepicker.css"));
            //).Include("~/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
