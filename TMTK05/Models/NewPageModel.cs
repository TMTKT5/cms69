using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMTK05.Models
{
    public class NewPageModel
    {
        [Display(Name = "Title:")]
        public string Title { get; set; }

        [Display(Name = "Description:")]
        public string Description { get; set; }

        [Display(Name = "Content:")]
        public string Content { get; set; }

        [Display(Name = "Image:")]
        public string Image { get; set; }
    }
}