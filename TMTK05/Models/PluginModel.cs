#region

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using TMTK05.Classes;

#endregion

namespace TMTK05.Models
{
    public class PluginModel
    {
        #region Public Properties

        public string Author { get; set; }

        public string Description { get; set; }

        public int Enabled { get; set; }

        public string Name { get; set; }

        #endregion Public Properties

        #region Public Methods

        // <summary> Adds a new user to the database </summery> 
        public static List<String> AllPlugins()
        {
            // Initial vars 
            var list = new List<String>();

            // MySQL query 
            const string selectStatment = "SELECT Id, Name, Description, Author, Enabled, Settings " +
                                          "FROM plugins";

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
                                list.Add(myDataReader.GetString(0));
                                list.Add(myDataReader.GetString(1));
                                list.Add(myDataReader.GetString(2));
                                list.Add(myDataReader.GetString(3));
                                list.Add(myDataReader.GetString(4));
                                list.Add(myDataReader.GetString(5));
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
            return list;
        }

        // <summary> Adds a new user to the database </summery> 
        public static int PluginEnableDisable(String id)
        {
            // Initial vars 
            var status = 0;

            // MySQL query 
            const string selectStatment = "SELECT Enabled " +
                                          "FROM plugins " +
                                          "WHERE Id = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var selectCommand = new MySqlCommand(selectStatment, empConnection))
                {
                    selectCommand.Parameters.Add("Id", MySqlDbType.Int16).Value = id;
                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command 
                        using (var myDataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (myDataReader.Read())
                            {
                                // Save the values 
                                status = Convert.ToInt16(myDataReader.GetString(0));

                                switch (status)
                                {
                                    case 0:
                                        status = 1;
                                        break;

                                    case 1:
                                        status = 0;
                                        break;
                                }
                            }
                        }
                        const string updateStatment = "UPDATE plugins " +
                                                      "SET Enabled = ? " +
                                                      "WHERE Id = ?";

                        using (var updateCommand = new MySqlCommand(updateStatment, empConnection))
                        {
                            updateCommand.Parameters.Add("Enabled", MySqlDbType.Int16).Value = status;
                            updateCommand.Parameters.Add("Id", MySqlDbType.Int16).Value = id;
                            try
                            {
                                DatabaseConnection.DatabaseOpen(empConnection);
                                // Execute command 
                                updateCommand.ExecuteScalar();
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
            return status;
        }

        // <summary> Adds a new user to the database </summery> 
        public static bool PluginStatus(String id)
        {
            // Initial vars 
            const bool status = false;

            // MySQL query 
            const string selectStatment = "SELECT Enabled " +
                                          "FROM plugins " +
                                          "WHERE Id = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var selectCommand = new MySqlCommand(selectStatment, empConnection))
                {
                    selectCommand.Parameters.Add("Id", MySqlDbType.VarChar).Value = id;
                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command 
                        using (var myDataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (myDataReader.Read())
                            {
                                // Save the values 
                                return myDataReader.GetString(0) == "1";
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
            return status;
        }

        #endregion Public Methods
    }
}