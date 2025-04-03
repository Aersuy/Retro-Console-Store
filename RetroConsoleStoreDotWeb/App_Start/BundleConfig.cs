using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

using System.Web.Routing;
using System.Web.UI.WebControls;


namespace RetroConsoleStoreDotWeb.App_Start
{
    
    public class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include("~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bundles/tiny-slider/css").Include("~/Content/tiny-slider.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/main/css").Include("~/Content/style.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/tiny-slider/js").Include("~/Scripts/tiny-slider.js"));

            bundles.Add(new ScriptBundle("~/bundles/main/js").Include("~/Scripts/custom.js"));

            bundles.Add(new StyleBundle("~/bundles/site/css").Include("~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/bundles/catalog/css").Include("~/Content/catalog.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/product/css").Include("~/Content/product.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/auth/css").Include("~/Content/auth.css", new CssRewriteUrlTransform()));



        }
    }
}