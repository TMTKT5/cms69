#region

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using TMTK05.Classes;

#endregion

namespace TMTK05.Models
{
    public class PageModel
    {
        #region Public Constructors

        public PageModel()
        {
            Done = false;
        }

        #endregion Public Constructors

        #region Public Properties

        [Display(Name = "Content:")]
        public string Content { get; set; }

        [Display(Name = "Description:")]
        public string Description { get; set; }

        public bool Done { get; set; }

        [Display(Name = "Image:")]
        public string Image { get; set; }

        [Display(Name = "Title:")]
        public string Title { get; set; }

        public int Type { get; set; }

        #endregion Public Properties

        #region Public Methods

        public static bool DeletePage(int id)
        {
            // MySQL query 
            const string deleteStatment = "DELETE " +
                                          "FROM pages " +
                                          "WHERE Id = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var deleteCommand = new MySqlCommand(deleteStatment, empConnection))
                {
                    // Bind parameters 
                    deleteCommand.Parameters.Add("Id", MySqlDbType.Int16).Value = id;

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command 
                        deleteCommand.ExecuteScalar();
                        return true;
                    }
                    catch (MySqlException)
                    {
                        // MySqlException 
                        return false;
                    }
                    finally
                    {
                        // Always close the connection 
                        DatabaseConnection.DatabaseClose(empConnection);
                    }
                }
            }
        }

        public void NewPage()
        {
            // Run model through sql injection prevention 
            var title = SqlInjection.SafeSqlLiteral(Title);
            var description = SqlInjection.SafeSqlLiteral(Description);

            // MySql query 
            const string insertStatement = "INSERT INTO pages " +
                                           "(Title, Description, Blog) " +
                                           "VALUES (?, ?, ?)";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var insertCommand = new MySqlCommand(insertStatement, empConnection))
                {
                    // Bind parameters 
                    insertCommand.Parameters.Add("Title", MySqlDbType.VarChar).Value = title;
                    insertCommand.Parameters.Add("Description", MySqlDbType.VarChar).Value = description;
                    insertCommand.Parameters.Add("Blog", MySqlDbType.VarChar).Value = Type;

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

        public static List<String> AllPages()
        {
            // Initial vars 
            var list = new List<String>();

            // MySQL query 
            const string selectStatment = "SELECT Id, Title, Description, Blog " +
                                          "FROM pages";

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