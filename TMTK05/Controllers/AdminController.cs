#region

using System;
using System.Web.Security;
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
        // GET: /Admin/AddUser/ 
        [EnableCompression]
        public ActionResult AddUser()
        {
            // Redirect if the user isn't logged in 
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(new UserModel());
        }

        //
        // POST: /Admin/AddUser/ 
        [HttpPost]
        [EnableCompression]
        public ActionResult AddUser(UserModel model)
        {
            // Redirect if the user isn't logged in 
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }

            model.AddUser();
            return View(model);
        }

        //
        // GET: /Admin/Home/ 
        public ActionResult Index()
        {
            // Redirect if the user isn't logged in 
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }

            return View();
        }

        //
        // GET: /Admin/Login/ 
        [EnableCompression]
        public ActionResult Login()
        {
            // Redirect if the user is logged in 
            if (IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(new UserModel());
        }

        //
        // POST: /Admin/Login/ 
        [HttpPost]
        [EnableCompression]
        public ActionResult Login(UserModel model)
        {
            model.Login();
            return model.Done ? View("LoggedIn") : View(model);
        }

        //
        // GET: /Admin/Settings/ 
        [EnableCompression]
        public ActionResult Settings()
        {
            // Redirect if the user isn't logged in 
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new SettingsModel();
            model.LoadSettings();
            return View(model);
        }

        //
        // POST: /Admin/Settings/ 
        [HttpPost]
        [EnableCompression]
        public ActionResult Settings(SettingsModel model)
        {
            // Redirect if the user isn't logged in 
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }

            model.SaveSettings();
            return View(model);
        }

        //
        // GET: /Admin/SignOut/ 
        [EnableCompression]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        //
        // GET: /Admin/ViewProfile/ 
        public ActionResult ViewProfile()
        {
            // Redirect if the user isn't logged in 
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        //
        // POST: /Admin/ViewProfile/ 
        [HttpPost]
        public ActionResult ViewProfile(UserModel model)
        {
            // Redirect if the user isn't logged in 
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }
            model.UpdateUser();
            return View(model);
        }

        //
        // AJAX:
        // GET: /Admin/UsernameCheck/
        [EnableCompression]
        public string UsernameCheck(string input)
        {
            return UserModel.UsernameCheck(SqlInjection.SafeSqlLiteral(StringManipulation.ToLowerFast(input))) > 0 ? "taken" : String.Empty;
        }

        #endregion Public Methods
    }
}