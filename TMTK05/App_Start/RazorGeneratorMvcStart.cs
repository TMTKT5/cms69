using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using RazorGenerator.Mvc;
using TMTK05.Classes;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(TMTK05.App_Start.RazorGeneratorMvcStart), "Start")]

namespace TMTK05.App_Start {
    public static class RazorGeneratorMvcStart {
        public static void Start() {
            var engine = new PrecompiledMvcEngine(typeof(RazorGeneratorMvcStart).Assembly) {
                UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal
            };

            engine.ViewLocationCache = new TwoLevelViewCache(engine.ViewLocationCache);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(engine);

            // StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }
    }
}
