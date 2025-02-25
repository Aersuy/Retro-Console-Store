using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

using System.Web.Routing;


namespace RetroConsoleStoreDotWeb.App_Start
{
    public class Global : HttpApplication
    {
        void Aplication_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundle(BundleTable.Bundles);
        }
    }
    public class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~bundles/bootstrap/css").Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~bundles/bootstrap/js").Include("~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~bundles/font-awesome/css").Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~bundles/toaster/css").Include("~/Content/toastr.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~bundles/datatables/css").Include("~/Vendors/datatables/datatables.min.css", new CssRewriteUrlTransform()));
        }
    }
}