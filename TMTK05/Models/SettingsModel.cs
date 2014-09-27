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

        public void LoadSettings()
        {
            // MySQL query Select book in the database 
            const string result = "SELECT Header, Footer, FooterText, ColorScheme " +
                                  "FROM site " +
                                  "Where id = 1";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var showresult = new MySqlCommand(result, empConnection))
                {
                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command 
                        using (var myDataReader = showresult.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (myDataReader.Read())
                            {
                                // Save the values 
                                Header = Convert.ToInt32(myDataReader.GetString(0));
                                Footer = Convert.ToInt32(myDataReader.GetString(1));
                                FooterText = myDataReader.GetString(2);
                                ColorScheme = myDataReader.GetString(3);
                            }
                        }
                    }
                    catch (MySqlException)
                    {
                        // MySqlException 
                    }
                    finally
                    {
                        DatabaseConnection.DatabaseClose(empConnection);
                    }
                }
            }
        }
    }
}