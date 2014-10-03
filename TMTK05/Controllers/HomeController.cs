#region

using System.Web.Mvc;
<<<<<<< Updated upstream
=======
using TMTK05.Classes;
using TMTK05.Models;
>>>>>>> Stashed changes

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

        public ActionResult Contact()
        {
            return View();
        }

        #endregion Public Methods

    }
}