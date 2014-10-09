#region

using System;
using System.Web.Mvc;
using TMTK05.Attributes;
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
        [EnableCompression]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Home/Contact
        [EnableCompression]
        public ActionResult Contact()
        {
            return View(new ContactModel());
        }

        //
        // POST: /Home/Contact
        [HttpPost]
        [EnableCompression]
        public ActionResult Contact(ContactModel model)
        {
            model.SendMail();
            return View(model);
        }

        //
        // GET: /Home/Page/
        [EnableCompression]
        public ActionResult Page(String id)
        {
            int x;
            if (!Int32.TryParse(id, out x)) return RedirectToAction("Index", "Home");
            return View();
        }

        //
        // AJAX:
        // GET: /Home/MailCheck/
        [EnableCompression]
        public string MailCheck(string input)
        {
            return ValidateEmail.IsValidEmail(input) ? "valid" : String.Empty;
        }

        #endregion Public Methods
    }
}