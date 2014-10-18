#region

using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI;

#endregion

namespace TMTK05
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelMetadataProviders.Current = new CachedDataAnnotationsModelMetadataProvider();

            MvcHandler.DisableMvcResponseHeader = true;

            ScriptManager.ScriptResourceMapping.AddDefinition("jQuery", new ScriptResourceDefinition
            {
                Path = "~/Js/jquery-1.11.0.js",
                DebugPath = "~/Js/jquery-1.11.0.js",
                CdnPath = "https://code.jquery.com/jquery-1.11.0.min.js",
                CdnDebugPath = "https://code.jquery.com/jquery-1.11.0.min.js",
                CdnSupportsSecureConnection = true,
                LoadSuccessExpression = "window.jQuery"
            });
        }
    }
}