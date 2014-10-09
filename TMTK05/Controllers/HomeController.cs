#region

using System;
using System.Web.Mvc;
using TMTK05.Classes;
using TMTK05.Models;

#endregion

namespace TMTK05.Controllers
{
    public class HomeController : Controller
    {
        #region Public Methods

        //
        // GET: /Home/Index 
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Home/Contact
        public ActionResult Contact()
        {
            return View(new ContactModel());
        }

        //
        // POST: /Home/Contact
        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            model.SendMail();
            return View(model);
        }

        //
        // GET: /Home/Page/
        public ActionResult Page(String id)
        {
            int x;
            if (!Int32.TryParse(id, out x)) return RedirectToAction("Index", "Home");
            return View();
        }

        //
        // AJAX:
        // GET: /Home/MailCheck/
        public string MailCheck(string input)
        {
            return ValidateEmail.IsValidEmail(input) ? "valid" : String.Empty;
        }

        #endregion Public Methods
    }
}