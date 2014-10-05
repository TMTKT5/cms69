using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMTK05.Attributes;
using TMTK05.Models;

namespace TMTK05.Controllers
{
    public class ContactPluginController : Controller
    {
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
            return ContactPluginSettingsModel.SaveEmail(input);
        }
    }
}