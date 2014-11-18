#region

using System.Web.UI.WebControls;
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
    public class PostModel
    {
        #region Public Constructors

        public PostModel()
        {
            Done = false;
        }

        #endregion Public Constructors

        #region Public Properties

        [AllowHtml]
        [HiddenInput(DisplayValue = false)]
        public string Content { get; set; }

        public bool Done { get; set; }

        public string Error { get; set; }

        [Display(Name = "Title:")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        #endregion Public Properties

        public static List<String> AllPosts()
        {
            // Initial vars 
            var list = new List<String>();

            // MySQL query 
            const string selectStatment = "SELECT Id, Title " +
                                          "FROM posts";

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

        public static List<String> AllPostsStatic()
        {
            // Initial vars 
            var list = new List<String>();

            // MySQL query 
            const string selectStatment = "SELECT Title, Post, Author " +
                                          "FROM posts " +
                                          "ORDER BY Id DESC";

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

        public static bool DeletePost(int id)
        {
            // MySQL query 
            const string deleteStatment = "DELETE " +
                                          "FROM posts " +
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
            // MySQL query 
            const string selectStatment = "SELECT Post " +
                                          "FROM posts " +
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

        public void GetSinglePost(int id)
        {
            // Initial vars 
            var list = new List<String>();

            // MySQL query 
            const string selectStatment = "SELECT Title " +
                                          "FROM posts " +
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

        public void SavePost(int id)
        {
            // Run model through sql injection prevention 
            var title = SqlInjection.SafeSqlLiteral(Title);

            // MySQL query 
            const string updateStatement = "UPDATE posts " +
                                           "SET Title = ?, " +
                                           "Post = ? " +
                                           "WHERE Id = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var updateCommand = new MySqlCommand(updateStatement, empConnection))
                {
                    updateCommand.Parameters.Add("Title", MySqlDbType.VarChar).Value = title;
                    updateCommand.Parameters.Add("Post", MySqlDbType.VarChar).Value = Content;
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

        public void NewPost()
        {
            // Run model through sql injection prevention 
            var title = SqlInjection.SafeSqlLiteral(Title);

            // MySql query 
            const string insertStatement = "INSERT INTO posts " +
                                           "(Title, Post, Author) " +
                                           "VALUES (?, ?, ?)";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var insertCommand = new MySqlCommand(insertStatement, empConnection))
                {
                    // Bind parameters 
                    insertCommand.Parameters.Add("Title", MySqlDbType.VarChar).Value = title;
                    insertCommand.Parameters.Add("Post", MySqlDbType.VarChar).Value = Content;
                    insertCommand.Parameters.Add("Author", MySqlDbType.VarChar).Value = IdentityModel.CurrentUserName.CaptalizeFirstLetter();

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