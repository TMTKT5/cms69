using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI;
using TMTK05.Classes;

namespace TMTK05
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            var viewEngine = new RazorViewEngine();
            viewEngine.ViewLocationCache = new TwoLevelViewCache(viewEngine.ViewLocationCache);
            ViewEngines.Engines.Add(viewEngine);

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