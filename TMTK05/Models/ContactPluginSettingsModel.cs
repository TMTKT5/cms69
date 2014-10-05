using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using TMTK05.Classes;

namespace TMTK05.Models
{
    public class ContactPluginSettingsModel
    {
        // <summary> Adds a new user to the database </summery> 
        public static String GetEmail()
        {
            // Initial vars 
            var email = String.Empty;

            // MySQL query 
            const string selectStatment = "SELECT Email " +
                                          "FROM contactplugin " +
                                          "Where Id = 1";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var selectCommand = new MySqlCommand(selectStatment, empConnection))
                {
                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command 
                        using (var myDataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (myDataReader.Read())
                            {
                                // Save the values 
                                return myDataReader.GetString(0);
                            }
                        }
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
            return email;
        }


        // <summary> Adds a new user to the database </summery> 
        public static bool SaveEmail(string input)
        {
            // MySQL query 
            const string updateStatment = "UPDATE contactplugin " +
                                          "SET Email = ? " +
                                          "Where Id = 1";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var updateCommand = new MySqlCommand(updateStatment, empConnection))
                {

                    updateCommand.Parameters.Add("Email", MySqlDbType.VarChar).Value = SqlInjection.SafeSqlLiteral(input);

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        updateCommand.ExecuteScalar();
                        return true;
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
            return false;
        }
    }
}