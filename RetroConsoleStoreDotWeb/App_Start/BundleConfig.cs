using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

using System.Web.Routing;


namespace RetroConsoleStoreDotWeb.App_Start
{
    
    public class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include("~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include("~/Scripts/jquery-3.2.1.min.js"));

            bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/toastr/css").Include("~/Content/toastr.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/datatables/css").Include("~/Vendors/datatables/datatables.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/animate/css").Include("~/content/animate.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/style/css").Include("~/content/style.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/scripts/js").Include("~/Scripts/scripts.js"));


        }
    }
}