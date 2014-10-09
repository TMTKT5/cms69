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

            // Admin panel styles
            var adminPanelStylesBundle = new CustomStyleBundle("~/Bundles/AdminPanelStyles");
            adminPanelStylesBundle.Include(
                "~/Css/bootstrap.css",
                "~/Css/sb-admin.css",
                "~/font-awesome-4.1.0/css/font-awesome.min.css");
            adminPanelStylesBundle.Orderer = nullOrderer;
            bundles.Add(adminPanelStylesBundle);

            // Admin panel scripts
            var adminPanelScriptsBundle = new CustomScriptBundle("~/Bundles/AdminPanelScripts");
            adminPanelScriptsBundle.Include(
                "~/Js/jquery-1.11.0.js",
                "~/Js/bootstrap.js");
            adminPanelScriptsBundle.Orderer = nullOrderer;
            bundles.Add(adminPanelScriptsBundle);

            //Admin panel widgets
            var widgetScriptsBundle = new CustomScriptBundle("~/Bundles/AdminPanelWidgets");
            widgetScriptsBundle.Include(
                "~/Js/jquery.simpleWeather.js",
                "~/Js/weather.js",
                "~/Js/clockwidget.js",
                "~/Js/underscore-min.js",
                "~/Js/moment-2.2.1.js",
                "~/Js/clndr.js",
                "~/Js/site.js");
            widgetScriptsBundle.Orderer = nullOrderer;
            bundles.Add(widgetScriptsBundle);

            // Website styles
            var websiteStylesBundle = new CustomStyleBundle("~/Bundles/WebsiteStyles");
            websiteStylesBundle.Include(
                "~/Css/normalize.css",
                "~/Css/foundation.css",
                "~/Css/style.css",
                "~/Css/ie.css");
            websiteStylesBundle.Orderer = nullOrderer;
            bundles.Add(websiteStylesBundle);

            // Website scripts
            var websiteScriptsBundle = new CustomScriptBundle("~/Bundles/WebsiteScripts");
            websiteScriptsBundle.Include(
                "~/Js/functions.js",
                "~/Js/custom.modernizr.js",
                "~/Js/headroom.min.js",
                "~/Js/foundation.min.js");
            websiteScriptsBundle.Orderer = nullOrderer;
            bundles.Add(websiteScriptsBundle);

            // Alertify styles
            var websiteAlertifyStyleBundle = new CustomStyleBundle("~/Bundles/WebsiteAlertifyStyles");
            websiteAlertifyStyleBundle.Include(
                "~/Css/alertify.css",
                "~/Css/themes/default.css");
            websiteAlertifyStyleBundle.Orderer = nullOrderer;
            bundles.Add(websiteAlertifyStyleBundle);

            // Alertify script
            var websiteAlertifyScriptsBundle = new CustomScriptBundle("~/Bundles/WebsiteAlertifyScripts");
            websiteAlertifyScriptsBundle.Include(
                "~/Js/alertify.js");
            websiteAlertifyScriptsBundle.Orderer = nullOrderer;
            bundles.Add(websiteAlertifyScriptsBundle);

            // Image crop style
            var websiteCropStyleBundle = new CustomStyleBundle("~/Bundles/WebsiteCropStyles");
            websiteCropStyleBundle.Include(
                "~/Css/imgareaselect-animated.css");
            websiteCropStyleBundle.Orderer = nullOrderer;
            bundles.Add(websiteCropStyleBundle);

            // Image crop script
            var websiteImageCropScriptsBundle = new CustomScriptBundle("~/Bundles/WebsiteImageCropScripts");
            websiteImageCropScriptsBundle.Include(
                "~/Js/jquery.imgareaselect.pack.js");
            websiteImageCropScriptsBundle.Orderer = nullOrderer;
            bundles.Add(websiteImageCropScriptsBundle);

            // Sortable tables script
            var sortableScriptsBundle = new CustomScriptBundle("~/Bundles/SortableScripts");
            sortableScriptsBundle.Include(
                "~/Js/sorttable.js");
            sortableScriptsBundle.Orderer = nullOrderer;
            bundles.Add(sortableScriptsBundle);

            // WYSIWYG style
            var alohaStyleBundle = new CustomStyleBundle("~/Bundles/AlohaStyles");
            alohaStyleBundle.Include(
                "~/Css/aloha-ui.css");
            alohaStyleBundle.Orderer = nullOrderer;
            bundles.Add(alohaStyleBundle);

            // WYSIWYG scripts
            var alohaScriptsBundle = new CustomScriptBundle("~/Bundles/AlohaScripts");
            alohaScriptsBundle.Include(
                "~/Js/aloha.min.js",
                "~/Js/aloha-ui.js",
                "~/Js/aloha-ui-links.js");
            alohaScriptsBundle.Orderer = nullOrderer;
            bundles.Add(alohaScriptsBundle);
        }

        #endregion Public Methods
    }
}