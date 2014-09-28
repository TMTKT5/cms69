#region

using System;
using System.Globalization;
using System.Web;
using System.Web.Security;

#endregion

namespace TMTK05.Models
{
    public static class IdentityModel
    {
        #region Public Properties

        public static bool CurrentUserOwner
        {
            get
            {
                if (!HttpContext.Current.Request.IsAuthenticated) return false;
                var user = HttpContext.Current.User.Identity as FormsIdentity;
                // ReSharper disable PossibleNullReferenceException 
                var ticket = user.Ticket;
                // ReSharper restore PossibleNullReferenceException 
                return ticket.UserData.Split('|')[1] == "1";
            }
        }

        public static string CurrentUserId
        {
            get
            {
                var userId = String.Empty;

                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    userId = HttpContext.Current.User.Identity.Name.Split('|')[0];
                }

                return userId.ToString(CultureInfo.InvariantCulture);
            }
        }

        public static bool CurrentUserLoggedIn
        {
            get
            {
                return HttpContext.Current.Request.IsAuthenticated;
            }
        }

        #endregion Public Properties
    }
}