#region

using System.Collections.Generic;
using System.Linq.Expressions;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using TMTK05.Classes;

#endregion

namespace TMTK05.Models
{
    public class UserModel
    {
        #region Public Constructors

        public UserModel()
        {
            Done = false;
        }

        #endregion Public Constructors

        #region Public Properties

        [Display(Name = "Full name:")]
        public string Name { get; set; }

        [Display(Name = "Username:")]
        public string Username { get; set; }

        [Display(Name = "Password:")]
        public string Password { get; set; }

        public string Salt { get; set; }
        public bool Done { get; set; }

        #endregion Public Properties

        #region Public Methods

        // <summary>
        // Adds a new user to the database
        // </summery>
        public void AddUser()
        {
            // Run model through sql injection prevention
            var fullName = SqlInjection.SafeSqlLiteral(Name);
            var username = SqlInjection.SafeSqlLiteral(StringManipulation.ToLowerFast(Username));
            var salt = Crypt.GetRandomSalt();

            // MySql query
            const string insertStatement = "INSERT INTO users " +
                                           "(Name, Username, Password, Salt) " +
                                           "VALUES (?, ?, ?, ?)";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var insertCommand = new MySqlCommand(insertStatement, empConnection))
                {
                    // Bind parameters
                    insertCommand.Parameters.Add("Name", MySqlDbType.VarChar).Value = fullName;
                    insertCommand.Parameters.Add("Username", MySqlDbType.VarChar).Value = username;
                    insertCommand.Parameters.Add("Password", MySqlDbType.VarChar).Value = Crypt.HashPassword(Password, salt);
                    insertCommand.Parameters.Add("Salt", MySqlDbType.VarChar).Value = salt;

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command
                        insertCommand.ExecuteNonQuery();
                    }
                    catch (MySqlException)
                    {
                        // MySqlException bail out
                        return;
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

        // <summary>
        // Check if there is already an user with this username
        // </summary>
        public static int UsernameCheck(string username)
        {
            var count = 0;

            // MySQL query
            const string countStatment = "SELECT COUNT(*) " +
                                         "FROM users " +
                                         "WHERE Username = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var countCommand = new MySqlCommand(countStatment, empConnection))
                {
                    // Bind parameters
                    countCommand.Parameters.Add("Username", MySqlDbType.VarChar).Value = username;

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command
                        count = Convert.ToInt16(countCommand.ExecuteScalar());
                    }
                    catch (MySqlException)
                    {
                        // MySqlException
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

        #endregion Public Methods
    }
}