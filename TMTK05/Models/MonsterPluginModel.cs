#region

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using TMTK05.Classes;

#endregion

namespace TMTK05.Models
{
    public static class MonsterPluginModel
    {
        #region Public Methods

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

        public static List<String> AllCans()
        {
            // Initial vars 
            var list = new List<String>();

            // MySQL query 
            const string selectStatment = "SELECT `users`.`Id`, " +
                                          "`users`.`Name`, " +
                                          "`users`.`Username`, " +
                                          "`monster`.`Cans` " +
                                          "FROM users " +
                                          "LEFT JOIN `dbtmtk_05`.`monster` " +
                                          "ON `users`.`Id` = `monster`.`UserId` " +
                                          "WHERE `users`.`Id` = 6 " +
                                          "OR `users`.`Id` = 7 " +
                                          "OR `users`.`Id` = 8";

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

        #endregion Public Methods
    }
}