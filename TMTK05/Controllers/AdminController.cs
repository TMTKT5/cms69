#region

using System;
using Bibliotheek.Attributes;
using System.Web.Mvc;
using TMTK05.Classes;
using TMTK05.Models;

#endregion

namespace TMTK05.Controllers
{
    public class AdminController : Controller
    {
        #region Public Methods

        //
        // GET: /AddUser/ 
        [EnableCompression]
        public ActionResult AddUser()
        {
            // Redirect is the user isn't an admin 
            /*if (!IdentityModel.CurrentUserAdmin)
            {
                return RedirectToAction("Index", "Home");
            }*/

            return View(new UserModel());
        }

        //
        // POST: /AddUser/ 
        [HttpPost]
        [EnableCompression]
        public ActionResult AddUser(UserModel model)
        {
            // Redirect is the user isn't an admin 
            /*if (!IdentityModel.CurrentUserAdmin)
            {
                return RedirectToAction("Index", "Home");
            }*/

            model.AddUser();
            return View(model);
        }

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
        // GET: /Login/ 
        public ActionResult Login()
        {
            return View(new UserModel());
        }

        //
        // POST: /Login/ 
        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            model.Login();
            if (model.Done)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(model);
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

        //
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

            model.SaveSettings();
            return View(model);
        }

        //
        // AJAX:
        // GET: UsernameCheck
        public string UsernameCheck(string input)
        {
            return UserModel.UsernameCheck(SqlInjection.SafeSqlLiteral(StringManipulation.ToLowerFast(input))) > 0 ? "taken" : String.Empty;
        }

        #endregion Public Methods
    }
}