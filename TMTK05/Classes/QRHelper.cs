#region

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Controls;

#endregion

namespace TMTK05.Classes
{
    public static class QrHelper
    {
        #region Public Methods

        /// <summary>
        ///     Generates an img tag with a data uri encoded image of the QR code from the content given.
        /// </summary>
        /// <param name="html">   </param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static IHtmlString QrCode(this HtmlHelper html, string content)
        {
            var enc = new QrEncoder(ErrorCorrectionLevel.H);
            var code = enc.Encode(content);

            var r = new Renderer(5, Brushes.Black, Brushes.White);

            using (var ms = new MemoryStream())
            {
                r.WriteToStream(code.Matrix, ms, ImageFormat.Png);

                var image = ms.ToArray();

                return
                    html.Raw(String.Format(@"<img src=""data:image/png;base64,{0}"" alt=""{1}"" />",
                        Convert.ToBase64String(image), content));
            }
        }

        #endregion Public Methods
    }
}