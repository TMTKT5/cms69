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

            var commonStylesBundle = new CustomStyleBundle("~/Bundles/CommonStyles");
            commonStylesBundle.Include(
                "~/Css/bootstrap.min.css",
                "~/Css/sb-admin.css",
                "~/font-awesome-4.1.0/css/font-awesome.min.css");
            commonStylesBundle.Orderer = nullOrderer;
            bundles.Add(commonStylesBundle);

            var commonScriptsBundle = new CustomScriptBundle("~/Bundles/CommonScripts");
            commonScriptsBundle.Include(
                "~/Js/jquery-1.11.0.js",
                "~/Js/bootstrap.min.js");

            commonScriptsBundle.Orderer = nullOrderer;
            bundles.Add(commonScriptsBundle);
        }

        #endregion Public Methods
    }
}