#region

using System.Web.Mvc;

#endregion

namespace TMTK05.Classes
{
    public class CSharpRazorViewEngine : RazorViewEngine
    {
        #region Public Constructors

        /// <summary>
        ///     Use our own View engine to improve the speed of the website
        /// </summary>
        public CSharpRazorViewEngine()
        {
            AreaViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
            AreaMasterLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
            AreaPartialViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
            ViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
            MasterLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
            PartialViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
        }

        #endregion Public Constructors
    }
}