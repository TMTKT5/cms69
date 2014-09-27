#region

using System.IO.Compression;
using System.Web.Mvc;

#endregion

namespace Bibliotheek.Attributes
{
    public class EnableCompressionAttribute : ActionFilterAttribute
    {
        #region Private Fields

        private const CompressionMode Compress = CompressionMode.Compress;

        #endregion Private Fields

        #region Public Methods

        // <summary>
        // Enable compression if requested 
        // </summary>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;
            var acceptEncoding = request.Headers["Accept-Encoding"];
            // Check for accepted encoding 
            if (acceptEncoding == null)
                return;
            // Deflate is faster but not always supported 
            if (acceptEncoding.ToLower().Contains("deflate"))
            {
                response.Filter = new DeflateStream(response.Filter, Compress);
                response.AppendHeader("Content-Encoding", "deflate");
            }
            // If deflate isn't available use gzip 
            else if (acceptEncoding.ToLower().Contains("gzip"))
            {
                response.Filter = new GZipStream(response.Filter, Compress);
                response.AppendHeader("Content-Encoding", "gzip");
            }
        }

        #endregion Public Methods
    }
}