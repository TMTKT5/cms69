#region

using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
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
            Error = false;
            ErrorCode = false;
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
        public bool Error { get; set; }
        public bool ErrorCode { get; set; }

        public int Owner { get; set; }

        [Display(Name = "Two factor authentication")]
        public int TwoFactorEnabled { get; set; }

        [Display(Name = "Code")]
        public string TwoFactorCode { get; set; }

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

            // TFA code
            var buffer = new byte[9];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(buffer);
            }

            // Generates a 10 character string of A-Z, a-z, 0-9
            // Don't need to worry about any = padding from the
            // Base64 encoding, since our input buffer is divisible by 3
            var secret = Convert.ToBase64String(buffer).Substring(0, 10).Replace('/', '0').Replace('+', '1');

            // MySql query
            const string insertStatement = "INSERT INTO users " +
                                           "(Name, Username, Password, Salt, Secret) " +
                                           "VALUES (?, ?, ?, ?, ?)";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var insertCommand = new MySqlCommand(insertStatement, empConnection))
                {
                    // Bind parameters
                    insertCommand.Parameters.Add("Name", MySqlDbType.VarChar).Value = fullName;
                    insertCommand.Parameters.Add("Username", MySqlDbType.VarChar).Value = username;
                    insertCommand.Parameters.Add("Password", MySqlDbType.VarChar).Value = Crypt.HashPassword(Password, salt);
                    insertCommand.Parameters.Add("Salt", MySqlDbType.VarChar).Value = salt;
                    insertCommand.Parameters.Add("Secret", MySqlDbType.VarChar).Value = secret;

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

        // <summary>
        // Fetch user TFA settings and save the values to the model 
        // </summary>
        public void LoadTfaSettings()
        {
            // MySQL query Select book in the database 
            const string result = "SELECT Secret, Tfa " +
                                  "FROM users " +
                                  "Where Id = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var showresult = new MySqlCommand(result, empConnection))
                {
                    // Bind parameters
                    showresult.Parameters.Add("Id", MySqlDbType.VarChar).Value = IdentityModel.CurrentUserId;
                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command 
                        using (var myDataReader = showresult.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (myDataReader.Read())
                            {
                                // Save the values 
                                TwoFactorCode = new Base32Encoder().Encode(Encoding.ASCII.GetBytes(myDataReader.GetString(0))); ;
                                TwoFactorEnabled = Convert.ToInt32(myDataReader.GetValue(1));
                            }
                        }
                    }
                    catch (MySqlException)
                    {
                        // MySqlException 
                    }
                    finally
                    {
                        DatabaseConnection.DatabaseClose(empConnection);
                    }
                }
            }
        }
        
        // <summary>
        // Check if the username and password are the same as in the database
        // </summery>
        public void Login()
        {
            // Run model through sql injection prevention
            var username = SqlInjection.SafeSqlLiteral(StringManipulation.ToLowerFast(Username));
            var savedPassword = String.Empty;
            var savedSalt = String.Empty;
            var savedId = String.Empty;
            var code = String.Empty;

            // MySql query
            const string result = "SELECT Id, Password, Salt, Owner, Secret, Tfa " +
                                  "FROM users " +
                                  "WHERE Username = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var showResult = new MySqlCommand(result, empConnection))
                {
                    // Bind parameters
                    showResult.Parameters.Add("Username", MySqlDbType.VarChar).Value = username;

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command
                        using (var myDataReader = showResult.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (myDataReader.Read())
                            {
                                savedId = myDataReader.GetValue(0).ToString();
                                savedPassword = myDataReader.GetString(1);
                                savedSalt = myDataReader.GetString(2);
                                Owner = Convert.ToInt16(myDataReader.GetValue(3));
                                code = myDataReader.GetString(4);
                                TwoFactorEnabled = Convert.ToInt16(myDataReader.GetValue(5));
                            }
                        }

                        // Hash the password and check if the hash is the same as the saved password
                        if (Crypt.ValidatePassword(Password, savedPassword, savedSalt))
                        {
                            if (TwoFactorEnabled == 0)
                            {
                                if (TimeBasedOneTimePassword.IsValid(code, TwoFactorCode))
                                {
                                    Cookies.MakeCookie(username, savedId, Owner.ToString(CultureInfo.InvariantCulture));
                                    Done = true;
                                }
                                else
                                {
                                    ErrorCode = true;
                                }
                            }
                            else
                            {
                                Cookies.MakeCookie(username, savedId, Owner.ToString(CultureInfo.InvariantCulture));
                                Done = true;
                            }
                        }
                    }
                    catch (MySqlException)
                    {
                        // MySqlException bail out
                        Error = true;
                    }
                    finally
                    {
                        // Always close the connection
                        DatabaseConnection.DatabaseClose(empConnection);
                    }
                }
            }
            Error = true;
        }

        // <summary>
        // Update TFA settings
        // </summery>
        public void SaveTfaSettings()
        {
            // MySql query
            const string updateStatement = "UPDATE users " +
                                           "SET Tfa = ? " +
                                           "WHERE Id = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var updateCommand = new MySqlCommand(updateStatement, empConnection))
                {
                    // Bind parameters
                    updateCommand.Parameters.Add("Tfa", MySqlDbType.VarChar).Value = TwoFactorEnabled;
                    updateCommand.Parameters.Add("Id", MySqlDbType.Int16).Value = IdentityModel.CurrentUserId;

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command
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
            Done = true;
        }

        // <summary>
        // Check if the is already an user with this username
        // </summary>
        public static int TfaCheck(string username)
        {
            var tfa = 0;

            // MySQL query
            const string selectStatment = "SELECT Tfa " +
                                          "FROM users " +
                                          "WHERE Username = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var selectCommand = new MySqlCommand(selectStatment, empConnection))
                {
                    // Bind parameters
                    selectCommand.Parameters.Add("Username", MySqlDbType.VarChar).Value = username;

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command
                        using (var myDataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (myDataReader.Read())
                            {
                                tfa = Convert.ToInt16(myDataReader.GetValue(0));
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
            return tfa;
        }
        
        // <summary>
        // Update saved password
        // </summery>
        public void UpdateUser()
        {
            var salt = Crypt.GetRandomSalt();

            // MySql query
            const string updateStatement = "UPDATE users " +
                                           "SET Password = ?, " +
                                           "Salt = ? " +
                                           "WHERE Id = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var updateCommand = new MySqlCommand(updateStatement, empConnection))
                {
                    // Bind parameters
                    updateCommand.Parameters.Add("Password", MySqlDbType.VarChar).Value = Crypt.HashPassword(Password, salt);
                    updateCommand.Parameters.Add("Salt", MySqlDbType.VarChar).Value = salt;
                    updateCommand.Parameters.Add("Id", MySqlDbType.Int16).Value = IdentityModel.CurrentUserId;

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command
                        updateCommand.ExecuteNonQuery();
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