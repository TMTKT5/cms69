#region

using Bibliotheek.Attributes;
using System.Web.Mvc;
using TMTK05.Models;

#endregion

namespace TMTK05.Controllers
{
    public class AdminController : Controller
    {
        #region Public Methods

        #region Public Methods

        //
        // GET: /Home/ 
        public ActionResult Index()
        {
            // Redirect is the user isn't an admin 
            /*if (!IdentityModel.CurrentUserAdmin)
            {
                return RedirectToAction("Index", "Home");
            }*/

            return View();
        }

        //
        // GET: /Settings/ 
        [EnableCompression]
        public ActionResult Settings()
        {
            // Redirect is the user isn't an admin 
            /*if (!IdentityModel.CurrentUserAdmin)
            {
                return RedirectToAction("Index", "Home");
            }*/

            var model = new SettingsModel();
            model.LoadSettings();
            return View(model);
        }

        // POST: /Settings/ 
        [HttpPost]
        [EnableCompression]
        public ActionResult Settings(SettingsModel model)
        {
            // Redirect is the user isn't an admin 
            /*if (!IdentityModel.CurrentUserAdmin)
            {
                return RedirectToAction("Index", "Home");
            }*/

            if (model.SaveSettings())
            {
                ViewBag.Return = 0;
                return View(model);
            }
            ViewBag.Return = 1;
            return View(model);
        }

        #endregion Public Methods

        #endregion Public Methods
    }
}