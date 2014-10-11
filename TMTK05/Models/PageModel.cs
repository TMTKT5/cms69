#region

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;
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

        [AllowHtml]
        [HiddenInput(DisplayValue = false)]
        public string Content { get; set; }

        [Display(Name = "Description:")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        public bool Done { get; set; }

        public string Error { get; set; }

        [Display(Name = "Image:")]
        public string Image { get; set; }

        [Display(Name = "Title:")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        public int Type { get; set; }

        [Display(Name = "Show in menu:")]
        public int Menu { get; set; }

        #endregion Public Properties

        #region Public Methods

        public static List<String> AllPages()
        {
            // Initial vars 
            var list = new List<String>();

            // MySQL query 
            const string selectStatment = "SELECT Id, Title, Description, Blog, Menu " +
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
                                list.Add(myDataReader.GetString(4));
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

        public static string GetContent(int id)
        {
            // Initial vars 
            var list = new List<String>();

            // MySQL query 
            const string selectStatment = "SELECT Content " +
                                          "FROM pages " +
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
            return String.Empty;
        }

        public void GetSinglePage(int id)
        {
            // Initial vars 
            var list = new List<String>();

            // MySQL query 
            const string selectStatment = "SELECT Title, Description, Blog, Menu " +
                                          "FROM pages " +
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
                                Title = SqlInjection.SafeSqlLiteralRevert(myDataReader.GetString(0));
                                Description = SqlInjection.SafeSqlLiteralRevert(myDataReader.GetString(1));
                                Type = myDataReader.GetInt16(2);
                                Menu = myDataReader.GetInt16(3);
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
        }

        public static List<String> GetSinglePageStaticList(int id)
        {
            // Initial vars 
            var list = new List<String>();

            // MySQL query 
            const string selectStatment = "SELECT Title, Description, Content, Blog " +
                                          "FROM pages " +
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
                                list.Add(myDataReader.GetString(0));
                                list.Add(myDataReader.GetString(1));
                                list.Add(myDataReader.GetString(3));

                                list.Add(!myDataReader.IsDBNull(2) ? myDataReader.GetString(2) : String.Empty);
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

        public void NewPage()
        {
            // Run model through sql injection prevention 
            var title = SqlInjection.SafeSqlLiteral(Title);
            var description = SqlInjection.SafeSqlLiteral(Description);

            // MySql query 
            const string insertStatement = "INSERT INTO pages " +
                                           "(Title, Description, Blog, Menu) " +
                                           "VALUES (?, ?, ?, ?)";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var insertCommand = new MySqlCommand(insertStatement, empConnection))
                {
                    // Bind parameters 
                    insertCommand.Parameters.Add("Title", MySqlDbType.VarChar).Value = title;
                    insertCommand.Parameters.Add("Description", MySqlDbType.VarChar).Value = description;
                    insertCommand.Parameters.Add("Blog", MySqlDbType.VarChar).Value = Type;
                    insertCommand.Parameters.Add("Menu", MySqlDbType.VarChar).Value = Menu;

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

        public void SavePage(int id)
        {
            // Run model through sql injection prevention 
            var title = SqlInjection.SafeSqlLiteral(Title);
            var description = SqlInjection.SafeSqlLiteral(Description);

            // MySQL query 
            const string updateStatement = "UPDATE pages " +
                                           "SET Title = ?, " +
                                           "Description = ?, " +
                                           "Blog = ?," +
                                           "Menu = ?," +
                                           "Content = ? " +
                                           "WHERE Id = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var updateCommand = new MySqlCommand(updateStatement, empConnection))
                {
                    updateCommand.Parameters.Add("Title", MySqlDbType.VarChar).Value = title;
                    updateCommand.Parameters.Add("Description", MySqlDbType.VarChar).Value = description;
                    updateCommand.Parameters.Add("Blog", MySqlDbType.VarChar).Value = Type;
                    updateCommand.Parameters.Add("Menu", MySqlDbType.VarChar).Value = Menu;
                    updateCommand.Parameters.Add("Content", MySqlDbType.VarChar).Value = Content;
                    updateCommand.Parameters.Add("Id", MySqlDbType.Int16).Value = id;

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        updateCommand.ExecuteNonQuery();
                        Done = true;
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

        #endregion Public Methods
    }
}