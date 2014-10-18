#region

using RazorGenerator.Mvc;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using TMTK05.App_Start;
using TMTK05.Classes;
using WebActivatorEx;

#endregion

[assembly: PostApplicationStartMethod(typeof(RazorGeneratorMvcStart), "Start")]

namespace TMTK05.App_Start
{
    public static class RazorGeneratorMvcStart
    {
        #region Public Methods

        public static void Start()
        {
            var engine = new PrecompiledMvcEngine(typeof(RazorGeneratorMvcStart).Assembly)
            {
                UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal
            };

            engine.ViewLocationCache = new TwoLevelViewCache(engine.ViewLocationCache);

            //ViewEngines.Engines.Clear();
            //ViewEngines.Engines.Add(engine);
            ViewEngines.Engines.Insert(0, engine);

            // StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }

        #endregion Public Methods
    }
}