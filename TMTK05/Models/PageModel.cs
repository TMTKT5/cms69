﻿#region

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
        public string Content { get; set; }

        [Display(Name = "Description:")]
        public string Description { get; set; }

        public bool Done { get; set; }

        public string Error { get; set; }

        [Display(Name = "Image:")]
        public string Image { get; set; }

        [Display(Name = "Title:")]
        public string Title { get; set; }

        public int Type { get; set; }

        #endregion Public Properties

        #region Public Methods

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
            const string selectStatment = "SELECT Title, Description, Blog " +
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
                                Title = myDataReader.GetString(0);
                                Description = myDataReader.GetString(1);
                                Type = myDataReader.GetInt16(2);
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

        public void SavePage(int id)
        {
            // MySQL query 
            const string updateStatement = "UPDATE pages " +
                                           "SET Title = ?, " +
                                           "Description = ?, " +
                                           "Blog = ?," +
                                           "Content = ? " +
                                           "WHERE Id = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var updateCommand = new MySqlCommand(updateStatement, empConnection))
                {
                    updateCommand.Parameters.Add("Title", MySqlDbType.VarChar).Value = Title;
                    updateCommand.Parameters.Add("Description", MySqlDbType.VarChar).Value = Description;
                    updateCommand.Parameters.Add("Blog", MySqlDbType.VarChar).Value = Type;
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