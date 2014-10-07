using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using WebGrease.Css.Ast;
using TMTK05.Classes;

namespace TMTK05.Models
{
    public class PageModel
    {
        public PageModel()
        {
            Done = false;
        }
        public bool Done { get; set; }

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

            // MySql query 
            const string insertStatement = "INSERT INTO pages " +
                                           "(Title, Description) " +
                                           "VALUES (?, ?)";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var insertCommand = new MySqlCommand(insertStatement, empConnection))
                {
                    // Bind parameters 
                    insertCommand.Parameters.Add("Title", MySqlDbType.VarChar).Value = title;
                    insertCommand.Parameters.Add("Description", MySqlDbType.VarChar).Value = description;

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command 
                        insertCommand.ExecuteNonQuery();
                    }
                    catch (MySqlException)
                    {
                        // MySqlException bail out 
                    }
                    finally
                    {
                        // Always close the connection 
                        DatabaseConnection.DatabaseClose(empConnection);
                    }
                }
            }
            Done = true;
        }
    }
}