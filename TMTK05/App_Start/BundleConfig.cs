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
                "~/Css/bootstrap.min.css",
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

            var websiteScriptsBundle = new CustomScriptBundle("~/Bundles/WebsiteScripts");
            websiteScriptsBundle.Include(
                "~/Js/functions.js",
                "~/Js/custom.modernizr.js",
                "~/Js/headroom.js");

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
                "~/Js/foundation.js");

            websiteFoundationScriptsBundle.Orderer = nullOrderer;
            bundles.Add(websiteFoundationScriptsBundle);

            var websiteTabsScriptsBundle = new CustomScriptBundle("~/Bundles/WebsiteFoundatationScripts");
            websiteFoundationScriptsBundle.Include(
                "~/Js/jquery.easytabs.js");

            websiteTabsScriptsBundle.Orderer = nullOrderer;
            bundles.Add(websiteTabsScriptsBundle);

            var websiteStylesBundle = new CustomStyleBundle("~/Bundles/WebsiteStyles");
            websiteStylesBundle.Include(
                "~/Css/normalize.css",
                "~/Css/foundation.css",
                "~/Css/style.css",
                "~/Css/ie.css");
            websiteStylesBundle.Orderer = nullOrderer;
            bundles.Add(websiteStylesBundle);

            var websiteAlertifyBundle = new CustomStyleBundle("~/Bundles/WebsiteAlertifyStyles");
            websiteAlertifyBundle.Include(
                "~/Css/alertify.core.css",
                "~/Css/alertify.default.css");
            websiteAlertifyBundle.Orderer = nullOrderer;
            bundles.Add(websiteAlertifyBundle);

            var websiteCropBundle = new CustomStyleBundle("~/Bundles/WebsiteCropStyles");
            websiteCropBundle.Include(
                "~/Css/imgareaselect-animated.css");
            websiteCropBundle.Orderer = nullOrderer;
            bundles.Add(websiteCropBundle);
        }

        #endregion Public Methods
    }
}