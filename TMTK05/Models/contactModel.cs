using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMTK05.Models
{
    public class ContactModel
    {
        [Display(Name = "Naam")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Onderwerp")]
        public string Subject { get; set; }

        [Display(Name = "Bericht")]
        public string Message { get; set; }
    }
}