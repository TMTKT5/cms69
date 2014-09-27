#region

using System.Web.Mvc;

#endregion

namespace TMTK05.Controllers
{
    public class HomeController : Controller
    {

        #region Public Methods

        // GET: Home 
        public ActionResult Index()
        {
            return View();
        }

        #endregion Public Methods

    }
}