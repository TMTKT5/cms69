#region

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using TMTK05.Attributes;
using TMTK05.Classes;
using TMTK05.Models;

#endregion

namespace TMTK05.Controllers
{
    public class AdminController : Controller
    {
        #region Public Methods

        //
        // GET: /Admin/NewPage/
        public ActionResult NewPage()
        {
            // Redirect if the user isn't logged in
            if (!IdentityModel.CurrentUserLoggedIn || !IdentityModel.CurrentUserOwner)
            {
                return RedirectToAction("Login", "Admin");
            }
            
            return View();
        }

        //
        // POST: /Admin/NewPage/
        [HttpPost]
        public ActionResult NewPage(NewPageModel model)
        {
            // Redirect if the user isn't logged in 
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }

            model.NewPage();
            return View(model);
        }

        //
        // GET: /Admin/AddUser/ 
        [EnableCompression]
        public ActionResult AddUser()
        {
            // Redirect if the user isn't logged in 
            if (!IdentityModel.CurrentUserLoggedIn || !IdentityModel.CurrentUserOwner)
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(new UserModel());
        }

        //
        // POST: /Admin/AddUser/ 
        [HttpPost]
        [EnableCompression]
        public ActionResult AddUser(UserModel model)
        {
            // Redirect if the user isn't logged in 
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }

            model.AddUser();
            return View(model);
        }

        //
        // GET: /Admin/AllUsers/ 
        [EnableCompression]
        public ActionResult AllUsers()
        {
            // Redirect if the user isn't logged in 
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }

            return View();
        }

        //
        // AJAX:
        // GET: /Admin/DeleteUser/
        [EnableCompression]
        public bool DeleteUser(int input)
        {
            return UserModel.DeleteUser(input);
        }

        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            // Redirect if the user isn't logged in
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }

            return View();
        }

        //
        // GET: /Admin/Login/
        [EnableCompression]
        public ActionResult Login()
        {
            // Redirect if the user is logged in
            if (IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(new UserModel());
        }

        //
        // POST: /Admin/Login/
        [HttpPost]
        [EnableCompression]
        public ActionResult Login(UserModel model)
        {
            model.Login();
            return model.Done ? View("LoggedIn") : View(model);
        }

        //
        // GET: /Admin/Plugins/
        public ActionResult Plugins()
        {
            // Redirect if the user isn't logged in 
            if (!IdentityModel.CurrentUserLoggedIn || !IdentityModel.CurrentUserOwner)
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        //
        // AJAX:
        // GET: /Admin/PluginStatus/
        [EnableCompression]
        public string PluginStatus(string input)
        {
            return PluginModel.PluginStatus(input).ToString();
        }

        //
        // AJAX:
        // GET: /Admin/PluginEnableDisable/
        [EnableCompression]
        public string PluginEnableDisable(string input)
        {
            return PluginModel.PluginEnableDisable(input).ToString(CultureInfo.InvariantCulture);
        }

        //
        // GET: /Admin/Settings/
        [EnableCompression]
        public ActionResult Settings()
        {
            // Redirect if the user isn't logged in
            if (!IdentityModel.CurrentUserLoggedIn || !IdentityModel.CurrentUserOwner)
            {
                return RedirectToAction("Index", "Admin");
            }

            var model = new SettingsModel();
            model.LoadSettings();
            return View(model);
        }

        //
        // POST: /Admin/Settings/
        [HttpPost]
        [EnableCompression]
        public ActionResult Settings(SettingsModel model)
        {
            // Redirect if the user isn't logged in
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }

            model.SaveSettings();
            return View(model);
        }

        //
        // GET: /Admin/SignOut/
        [EnableCompression]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        //
        // AJAX:
        // GET: /Admin/TfaCheck/
        [EnableCompression]
        public string TfaCheck(string input)
        {
            return UserModel.TfaCheck(SqlInjection.SafeSqlLiteral(StringManipulation.ToLowerFast(input))) > 0
                ? String.Empty
                : "tfa";
        }

        //
        // GET: /Admin/TwoFactorAuthentication/
        [EnableCompression]
        public ActionResult TwoFactorAuthentication()
        {
            // Redirect if the user isn't logged in
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }

            var model = new UserModel();
            model.LoadTfaSettings();
            return View(model);
        }

        //
        // POST: /Admin/TwoFactorAuthentication/
        [HttpPost]
        [EnableCompression]
        public ActionResult TwoFactorAuthentication(UserModel model)
        {
            model.SaveTfaSettings();
            return View(model);
        }

        //
        // GET: /Admin/MediaUpload
        [EnableCompression]
        public ActionResult MediaUpload()
        {
            // Redirect if the user isn't logged in
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(new UploadImageModel());
        }

        //
        // POST: /Logged/ProfilePicture
        [HttpPost]
        [EnableCompression]
        public ActionResult MediaUpload(UploadImageModel model)
        {
            // Redirect if the user isn't logged in
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }

            if (!ModelState.IsValid) return View(model);
            Bitmap original = null;

            if (model.IsUrl)
            {
                original = UploadImageModel.GetImageFromUrl(model.Url);
            }
            else if (model.File != null)
            {
                original = Image.FromStream(model.File.InputStream) as Bitmap;
            }

            if (original != null)
            {
                var img = UploadImageModel.CreateImage(original, model.X, model.Y, model.Width, model.Height);

                var pictureName = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                var fn = Server.MapPath("~/database/" + pictureName + ".png");
                img.Save(fn, ImageFormat.Png);

                /*const string result = "UPDATE gebruiker " +
                                      "SET profielfoto = ? " +
                                      "WHERE id = ?";

                var user = User.Identity as FormsIdentity;
                // ReSharper disable PossibleNullReferenceException
                var ticket = user.Ticket;
                // ReSharper restore PossibleNullReferenceException

                var id = ticket.UserData;

                using (var empConnection = DatabaseConnection.DatabaseConnect())
                {
                    using (var showresult = new MySqlCommand(result, empConnection))
                    {
                        showresult.Parameters.Add("profielfoto", MySqlDbType.VarChar).Value = pictureName + ".png";
                        showresult.Parameters.Add("id", MySqlDbType.Int16).Value = id;

                        try
                        {
                            DatabaseConnection.DatabaseOpen(empConnection);
                            showresult.ExecuteNonQuery(); */
                            model = new UploadImageModel { Done = true };/*
                        }
                        catch (MySqlException)
                        {
                            model.Wrong = false;
                        }
                        finally
                        {
                            DatabaseConnection.DatabaseClose(empConnection);
                        }
                    }
                } */
            }
            else
            {
                model.NotFile = true;
            }

            return View(model);
        }

        //
        // AJAX:
        // GET: /Admin/UsernameCheck/
        [EnableCompression]
        public string UsernameCheck(string input)
        {
            return UserModel.UsernameCheck(SqlInjection.SafeSqlLiteral(StringManipulation.ToLowerFast(input))) > 0
                ? "taken"
                : String.Empty;
        }

        //
        // GET: /Admin/ViewProfile/
        [EnableCompression]
        public ActionResult ViewProfile()
        {
            // Redirect if the user isn't logged in
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }

            return View(new UserModel());
        }

        //
        // POST: /Admin/ViewProfile/
        [HttpPost]
        [EnableCompression]
        public ActionResult ViewProfile(UserModel model)
        {
            // Redirect if the user isn't logged in
            if (!IdentityModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Login", "Admin");
            }
            model.UpdateUser();
            return View(model);
        }

        #endregion Public Methods
    }
}