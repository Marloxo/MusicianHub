﻿using System.Web.Optimization;

namespace MusicianHub
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

            //Add Facebook SDK to bundles
            bundles.Add(new ScriptBundle("~/bundles/facebook").Include(
                        "~/Content/Facebook/facebook.js"));
            //Add Google Analytics
            bundles.Add(new ScriptBundle("~/bundles/googleAnalytics").Include(
                        "~/Content/Google/GoogleAnalytics.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datepicker.js",    // ** NEW for Bootstrap Datepicker
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-social/bootstrap-social.css",
                      "~/Content/bootstrap-datepicker.css",  // ** NEW for Bootstrap Datepicker
                      "~/Content/site.css"));
        }
    }
}
