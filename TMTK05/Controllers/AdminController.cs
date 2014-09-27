#region

using System.Web.Mvc;
using TMTK05.Models;

#endregion

namespace TMTK05.Controllers
{
    public class AdminController : Controller
    {
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
        public ActionResult Settings(SettingsModel model)
        {
            // Redirect is the user isn't an admin 
            /*if (!IdentityModel.CurrentUserAdmin)
            {
                return RedirectToAction("Index", "Home");
            }*/

            if (!ModelState.IsValid) return View(model);
            /*if (model.SaveSettings())
            {
                ViewBag.Error = "Het boek is toegevoegd!";
                return View(model);
            }*/
            ViewBag.Error = "Het boek is niet toegevoegd, probeer het later nog eens!";
            return View(model);
        }
    }
}