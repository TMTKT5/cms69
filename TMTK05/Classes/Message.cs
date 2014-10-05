#region

using System;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using TMTK05.Models;

#endregion

namespace TMTK05.Classes
{
    public static class Message
    {
        #region Public Methods

        /// <summary>
        ///     Send mail
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static String SendMail(string name, string email)
        {
            const bool error = false;
            var body = PopulateBodyActivate(name, email);
            try
            {
                var mailMessage = new MailMessage();
                mailMessage.To.Add(email);
                mailMessage.From = new MailAddress("noreply@bibliotheek.nl");
                mailMessage.Subject = "Activeer uw account | bibliotheek.nl";
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                var smtpClient = new SmtpClient("145.118.4.13");
                smtpClient.Send(mailMessage);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
                // ReSharper restore EmptyGeneralCatchClause
            {
            }
            return error.ToString();
        }

        /// <summary>
        ///     Send mail
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static String SendMail(string name, string email, string subject, string message)
        {
            const bool error = false;
            var body = PopulateBody(name, message);
            try
            {
                var mailMessage = new MailMessage();
                mailMessage.To.Add(ContactPluginSettingsModel.GetEmail());
                mailMessage.From = new MailAddress(email);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                var smtpClient = new SmtpClient("145.118.4.13");
                smtpClient.Send(mailMessage);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
                // ReSharper restore EmptyGeneralCatchClause
            {
            }
            return error.ToString();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Replace placeholders in the email template with vars
        /// </summary>
        /// <param name="naam"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        private static String PopulateBodyActivate(string naam, string mail)
        {
            String body;
            String hash;

            using (var md5Hash = MD5.Create())
            {
                hash = Crypt.GetMd5Hash(md5Hash, mail);
            }

            using (var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailActivateTemplate.html")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{NAAM}", naam);
            body = body.Replace("{URL}", "http://66164.ict-lab.nl/Account/Activate/" + hash + "/");
            return body;
        }

        /// <summary>
        ///     Replace placeholders in the email template with vars
        /// </summary>
        /// <param name="naam"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static String PopulateBody(string naam, string message)
        {
            String body;

            using (var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate.html")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{NAAM}", naam);
            body = body.Replace("{MESSAGE}", message);
            return body;
        }

        #endregion Private Methods
    }
}