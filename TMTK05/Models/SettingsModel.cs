#region

using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using TMTK05.Classes;

#endregion

namespace TMTK05.Models
{
    public class SettingsModel
    {
        #region Public Constructors

        public SettingsModel()
        {
            Done = false;
        }

        #endregion Public Constructors

        #region Public Properties

        [Display(Name = "Color scheme:")]
        public string ColorScheme { get; set; }

        public bool Done { get; set; }

        [Display(Name = "Footer:")]
        public int Footer { get; set; }

        [Display(Name = "Footer text:")]
        public string FooterText { get; set; }

        [Display(Name = "Header:")]
        public int Header { get; set; }

        #endregion Public Properties

        #region Public Methods

        // <summary>
        // Fetch website settings and return the values in a list
        // </summary>
        public List<String> FetchSettings()
        {
            // Initial vars
            var list = new List<String>();

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
                                list.Add(myDataReader.GetString(0));
                                list.Add(myDataReader.GetString(1));
                                list.Add(myDataReader.GetString(2));
                                list.Add(myDataReader.GetString(3));
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
            return list;
        }

        // <summary>
        // Fetch website settings and save the values to the model 
        // </summary>
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

        // <summary>
        // Update website settings 
        // </summary>
        public bool SaveSettings()
        {
            // MySQL query 
            const string result = "UPDATE site " +
                                  "SET " +
                                  "Header = ?, " +
                                  "Footer = ?, " +
                                  "FooterText = ?, " +
                                  "ColorScheme = ? " +
                                  "WHERE id = 1";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var showresult = new MySqlCommand(result, empConnection))
                {
                    // Bind parameters 
                    showresult.Parameters.Add("Header", MySqlDbType.Int16).Value = Header;
                    showresult.Parameters.Add("Footer", MySqlDbType.Int16).Value = Footer;
                    showresult.Parameters.Add("FooterText", MySqlDbType.VarChar).Value = SqlInjection.SafeSqlLiteral(FooterText);
                    showresult.Parameters.Add("ColorScheme", MySqlDbType.VarChar).Value = ColorScheme;

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command 
                        showresult.ExecuteNonQuery();
                    }
                    catch (MySqlException)
                    {
                        // MySqlException bail out 
                        return false;
                    }
                    finally
                    {
                        DatabaseConnection.DatabaseClose(empConnection);
                    }
                }
            }
            Done = true;
            return true;
        }

        #endregion Public Methods
    }
}