#region

using System;
using System.Web.Mvc;
using TMTK05.Attributes;
using TMTK05.Classes;
using TMTK05.Models;

#endregion

namespace TMTK05.Controllers
{
    public class ContactPluginController : Controller
    {
        #region Public Methods

        //
        // AJAX:
        // GET: /ContactPlugin/GetEmail/
        [EnableCompression]
        public String GetEmail()
        {
            return ContactPluginSettingsModel.GetEmail();
        }

        //
        // AJAX:
        // GET: /ContactPlugin/SaveEmail/
        [EnableCompression]
        public bool SaveEmail(string input)
        {
            return ValidateEmail.IsValidEmail(input) && ContactPluginSettingsModel.SaveEmail(input);
        }

        #endregion Public Methods
    }
}