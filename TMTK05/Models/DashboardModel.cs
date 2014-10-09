using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using TMTK05.Classes;

namespace TMTK05.Models
{
    public class DashboardModel
    {
        public int UserCount()
        {
            var count = 1;

            // MySQL query 
            const string selectStatment = "SELECT COUNT(*) " +
                                          "FROM users ";

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
                                count = Convert.ToInt16(myDataReader.GetValue(0));
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
            return count;
        }
    }
}