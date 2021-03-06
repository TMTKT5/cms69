﻿#region

using System.Web;
using System.Web.Security;

#endregion

namespace TMTK05.Models
{
    public static class IdentityModel
    {
        #region Public Properties

        public static string CurrentUserId
        {
            get
            {
                var user = HttpContext.Current.User.Identity as FormsIdentity;
                // ReSharper disable PossibleNullReferenceException 
                var ticket = user.Ticket;
                // ReSharper restore PossibleNullReferenceException 
                return ticket.UserData.Split('|')[0];
            }
        }

        public static bool CurrentUserLoggedIn
        {
            get { return HttpContext.Current.Request.IsAuthenticated; }
        }

        public static string CurrentUserName
        {
            get
            {
                var user = HttpContext.Current.User.Identity as FormsIdentity;
                // ReSharper disable PossibleNullReferenceException 
                var ticket = user.Ticket;
                // ReSharper restore PossibleNullReferenceException 
                return ticket.Name;
            }
        }

        public static bool CurrentUserOwner
        {
            get
            {
                var user = HttpContext.Current.User.Identity as FormsIdentity;
                // ReSharper disable PossibleNullReferenceException 
                var ticket = user.Ticket;
                // ReSharper restore PossibleNullReferenceException 
                return ticket.UserData.Split('|')[1] == "1";
            }
        }

        #endregion Public Properties
    }
}