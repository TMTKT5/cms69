#region

using System.Web.Mvc;

#endregion

namespace TMTK05.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Home/
        public ActionResult Settings()
        {
            return View();
        }
    }
}