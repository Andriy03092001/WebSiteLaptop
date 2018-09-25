using System.Web;
using System.Web.Optimization;

namespace LaptopWebSite
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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/froala").Include(
                      "~/Scripts/froala-editor/css/plugins/char_counter.css",
                      "~/Scripts/froala-editor/css/plugins/code_view.css",
                      "~/Scripts/froala-editor/css/plugins/colors.css",
                      "~/Scripts/froala-editor/css/plugins/draggable.css",
                      "~/Scripts/froala-editor/css/plugins/emoticons.css",
                      "~/Scripts/froala-editor/css/plugins/image.css",
                      "~/Scripts/froala-editor/css/plugins/image_manager.css",
                      "~/Scripts/froala-editor/css/plugins/line_breaker.css",
                      "~/Scripts/froala-editor/css/plugins/quick_insert.css",
                      "~/Scripts/froala-editor/css/plugins/table.css",
                      "~/Scripts/froala-editor/css/plugins/video.css",
                      "~/Scripts/froala-editor/css/editor_custom.css",
                      "~/Scripts/froala-editor/css/froala_editor.css",
                      "~/Scripts/froala-editor/css/froala_style.css"));


            bundles.Add(new ScriptBundle("~/bundles/froala").Include(
               "~/Scripts/froala-editor/js/ua.js",
               "~/Scripts/froala-editor/js/froala_editor.min.js",
               "~/Scripts/froala-editor/js/plugins/align.min.js",
               "~/Scripts/froala-editor/js/plugins/char_counter.min.js",
               "~/Scripts/froala-editor/js/plugins/code_beautifier.min.js",
               "~/Scripts/froala-editor/js/plugins/code_view.min.js",
               "~/Scripts/froala-editor/js/plugins/colors.min.js",
               "~/Scripts/froala-editor/js/plugins/draggable.min.js",
               "~/Scripts/froala-editor/js/plugins/emoticons.min.js",
               "~/Scripts/froala-editor/js/plugins/entities.min.js",
               "~/Scripts/froala-editor/js/plugins/font_family.min.js",
               "~/Scripts/froala-editor/js/plugins/font_size.min.js",
               "~/Scripts/froala-editor/js/plugins/forms.min.js",
               "~/Scripts/froala-editor/js/plugins/image.min.js",
               "~/Scripts/froala-editor/js/plugins/image_manager.min.js",
               "~/Scripts/froala-editor/js/plugins/line_breaker.min.js",
               "~/Scripts/froala-editor/js/plugins/link.min.js",
               "~/Scripts/froala-editor/js/plugins/lists.min.js",
               "~/Scripts/froala-editor/js/plugins/paragraph_format.min.js",
               "~/Scripts/froala-editor/js/plugins/paragraph_style.min.js",
               "~/Scripts/froala-editor/js/plugins/quick_insert.min.js",
               "~/Scripts/froala-editor/js/plugins/quote.min.js",
               "~/Scripts/froala-editor/js/plugins/save.min.js",
               "~/Scripts/froala-editor/js/plugins/table.min.js",
               "~/Scripts/froala-editor/js/plugins/url.min.js",
               "~/Scripts/froala-editor/js/plugins/video.min.js"));


        }
    }
}
