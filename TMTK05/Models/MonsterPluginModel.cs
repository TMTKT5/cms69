using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using TMTK05.Classes;

namespace TMTK05.Models
{
    public class MonsterPluginModel
    {
        public static void AddMonster()
        {
            var count = 0;

            // MySQL query 
            const string selectStatment = "SELECT Cans " +
                                          "FROM monster " +
                                          "WHERE UserId = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var selectCommand = new MySqlCommand(selectStatment, empConnection))
                {
                    selectCommand.Parameters.Add("UserId", MySqlDbType.Int16).Value = IdentityModel.CurrentUserId;
                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command 
                        using (var myDataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (myDataReader.Read())
                            {
                                count = Convert.ToInt16(myDataReader.GetValue(0)) + 1;
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
                    const string updateStatement = "UPDATE monster " +
                                                   "SET Cans = ? " +
                                                   "WHERE UserId = ?";

                    using (var updateCommand = new MySqlCommand(updateStatement, empConnection))
                    {
                        updateCommand.Parameters.Add("Cans", MySqlDbType.Int16).Value = count;
                        updateCommand.Parameters.Add("UserId", MySqlDbType.Int16).Value = IdentityModel.CurrentUserId;
                        try
                        {
                            DatabaseConnection.DatabaseOpen(empConnection);
                            updateCommand.ExecuteNonQuery();
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
            }
        }
    }
}