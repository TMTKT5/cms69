/*
 * Copyright (C) 2014 Owain van Brakel.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region

using System;
using System.Web;
using System.Web.Security;

#endregion

namespace TMTK05.Classes
{
    public abstract class Cookies
    {
        #region Public Methods

        // <summary>
        // Create a HttpOnly cookie
        // </summary>
        public static void MakeCookie(string email, string savedId, string admin)
        {
            var tkt = new FormsAuthenticationTicket(1, email, DateTime.Now,
                DateTime.Now.AddMinutes((44640)), false, savedId + "|" + admin);
            var cookiestr = FormsAuthentication.Encrypt(tkt);
            var ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr)
            {
                Expires = tkt.Expiration,
                Path = FormsAuthentication.FormsCookiePath,
                HttpOnly = true
            };
            FormsAuthentication.SetAuthCookie(email, false);
            HttpContext.Current.Response.Cookies.Add(ck);
        }

        #endregion Public Methods
    }
}