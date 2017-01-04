using System.Web.Optimization;

namespace CommonWeal.NGOWeb
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            //************************************** jquery bundle **********************************************


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                       "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                         "~/Content/themes/base/jquery.ui.css",
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));



            //************************************** modernizr bundle **********************************************
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //************************************** bootstrap bundle **********************************************


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/bootstrap/js/bootstrap.js",
                       "~/Scripts/bootstrap-multiselect.js"
                      ));

            
            //************************************** datapicker bundle **********************************************

            bundles.Add(new ScriptBundle("~/bundles/datapicker").Include(
                          "~/Scripts/bootstrap-datepicker.min.js"));

            //************************************** datapicker bundle **********************************************
            bundles.Add(new StyleBundle("~/Content/sitecss").Include(
                "~/Content/bootstrap.css",
                 "~/bootstrap/css/bootstrap-theme.css",
                 "~/Content/site.css",
                 "~/Content/bootstrap-multiselect.css"
                 ));


            //css  
            bundles.Add(new StyleBundle("~/Content/cssjqryUI").Include(
                   "~/Content/jquery-ui.css"));
            //***************************************Added on 28/12/2016***********************************************



            bundles.Add(new StyleBundle("~/Content/chosencss").Include(
                "~/Content/chosen.css"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/jschosen").Include(
           "~/Scripts/chosen.jquery.js"));


            bundles.Add(new StyleBundle("~/Contents/cssdynamiclist").Include(
                "~/Content/dyanamiclist.css"
                ));
            bundles.Add(new StyleBundle("~/Contents/cssdngosearch").Include(
           "~/Content/NgoSearchBox/NgoSearch.css"
              ));
        }
    }
}

