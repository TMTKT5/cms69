#region

using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using System.Web.Optimization;

#endregion

namespace TMTK05
{
    public static class BundleConfig
    {

        #region Public Methods

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            var nullOrderer = new NullOrderer();

            // Replace a default bundle resolver in order to the debugging HTTP-handler can use
            // transformations of the corresponding bundle
            BundleResolver.Current = new CustomBundleResolver();

            var adminPanelStylesBundle = new CustomStyleBundle("~/Bundles/AdminPanelStyles");
            adminPanelStylesBundle.Include(
                "~/Css/bootstrap.css",
                "~/Css/sb-admin.css",
                "~/font-awesome-4.1.0/css/font-awesome.min.css");
            adminPanelStylesBundle.Orderer = nullOrderer;
            bundles.Add(adminPanelStylesBundle);

            var adminPanelScriptsBundle = new CustomScriptBundle("~/Bundles/AdminPanelScripts");
            adminPanelScriptsBundle.Include(
                "~/Js/jquery-1.11.0.js",
                "~/Js/bootstrap.min.js");
            adminPanelScriptsBundle.Orderer = nullOrderer;
            bundles.Add(adminPanelScriptsBundle);

            var clockScriptsBundle = new CustomScriptBundle("~/Bundles/ClockScripts");
            clockScriptsBundle.Include(
                "~/Js/clockwidget.js");
            clockScriptsBundle.Orderer = nullOrderer;
            bundles.Add(clockScriptsBundle);

            var weatherScriptsBundle = new CustomScriptBundle("~/Bundles/WeatherScripts");
            weatherScriptsBundle.Include(
                "~/Js/jquery.simpleWeather.js",
                "~/Js/Weather.js");
            weatherScriptsBundle.Orderer = nullOrderer;
            bundles.Add(weatherScriptsBundle);

            var websiteScriptsBundle = new CustomScriptBundle("~/Bundles/WebsiteScripts");
            websiteScriptsBundle.Include(
                "~/Js/functions.js",
                "~/Js/custom.modernizr.js",
                "~/Js/headroom.min.js");
            websiteScriptsBundle.Orderer = nullOrderer;
            bundles.Add(websiteScriptsBundle);

            var websiteAlertifyScriptsBundle = new CustomScriptBundle("~/Bundles/WebsiteAlertifyScripts");
            websiteAlertifyScriptsBundle.Include(
                "~/Js/alertify.js");
            websiteAlertifyScriptsBundle.Orderer = nullOrderer;
            bundles.Add(websiteAlertifyScriptsBundle);

            var websiteImageCropScriptsBundle = new CustomScriptBundle("~/Bundles/WebsiteImageCropScripts");
            websiteAlertifyScriptsBundle.Include(
                "~/Js/jquery.imgareaselect.pack");
            websiteImageCropScriptsBundle.Orderer = nullOrderer;
            bundles.Add(websiteImageCropScriptsBundle);

            var websiteFoundationScriptsBundle = new CustomScriptBundle("~/Bundles/WebsiteFoundatationScripts");
            websiteFoundationScriptsBundle.Include(
                "~/Js/foundation.min.js");
            websiteFoundationScriptsBundle.Orderer = nullOrderer;
            bundles.Add(websiteFoundationScriptsBundle);

            var websiteTabsScriptsBundle = new CustomScriptBundle("~/Bundles/WebsiteFoundatationScripts");
            websiteFoundationScriptsBundle.Include(
                "~/Js/jquery.easytabs.min.js");
            websiteTabsScriptsBundle.Orderer = nullOrderer;
            bundles.Add(websiteTabsScriptsBundle);

            var sortableScriptsBundle = new CustomScriptBundle("~/Bundles/SortableScripts");
            sortableScriptsBundle.Include(
                "~/Js/sorttable.js");
            sortableScriptsBundle.Orderer = nullOrderer;
            bundles.Add(sortableScriptsBundle);

            var websiteStylesBundle = new CustomStyleBundle("~/Bundles/WebsiteStyles");
            websiteStylesBundle.Include(
                "~/Css/normalize.css",
                "~/Css/foundation.css",
                "~/Css/style.css",
                "~/Css/ie.css");
            websiteStylesBundle.Orderer = nullOrderer;
            bundles.Add(websiteStylesBundle);

            var websiteAlertifyStyleBundle = new CustomStyleBundle("~/Bundles/WebsiteAlertifyStyles");
            websiteAlertifyStyleBundle.Include(
                "~/Css/alertify.css",
                "~/Css/themes/default.css");
            websiteAlertifyStyleBundle.Orderer = nullOrderer;
            bundles.Add(websiteAlertifyStyleBundle);

            var websiteCropStyleBundle = new CustomStyleBundle("~/Bundles/WebsiteCropStyles");
            websiteCropStyleBundle.Include(
                "~/Css/imgareaselect-animated.css");
            websiteCropStyleBundle.Orderer = nullOrderer;
            bundles.Add(websiteCropStyleBundle);
        }

        #endregion Public Methods

    }
}