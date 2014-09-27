using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using TMTK05.Classes;

namespace TMTK05.Models
{
    public class SettingsModel
    {
        [Display(Name = "Header:")]
        public int Header { get; set; }

        [Display(Name = "Footer:")]
        public int Footer { get; set; }

        [Display(Name = "Footer text:")]
        public string FooterText { get; set; }

        [Display(Name = "Color scheme:")]
        public string ColorScheme { get; set; }
    }
}