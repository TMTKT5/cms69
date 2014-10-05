using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebGrease.Css.Ast;
using TMTK05.Classes;

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

        public void NewPage()
        {
            // Run model through sql injection prevention 
            var title = SqlInjection.SafeSqlLiteral(Title);
            var description = SqlInjection.SafeSqlLiteral(Description);
            var contect = SqlInjection.SafeSqlLiteral(Content);
            var image = SqlInjection.SafeSqlLiteral(Image);

            // MySql query 
            const string insertStatement = "INSERT INTO pages " +
                                           "(Title, Description, Content, Images) " +
                                           "VALUES (?, ?, ?, ?)";
        }
    }
}