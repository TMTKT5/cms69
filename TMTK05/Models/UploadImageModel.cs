#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

#endregion

namespace TMTK05.Models
{
    public class UploadImageModel
    {
        #region Public Constructors

        public UploadImageModel()
        {
            Done = false;
            Wrong = false;
            NotFile = false;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool Done { get; set; }

        [Display(Name = "Upload file: ")]
        public HttpPostedFileBase File { get; set; }

        [Range(1, int.MaxValue)]
        [HiddenInput(DisplayValue = false)]
        public int Height { get; set; }

        public bool IsFile { get; set; }

        public bool IsUrl { get; set; }

        [Display(Name = "Flickr image")]
        [HiddenInput(DisplayValue = false)]
        public string Flickr { get; set; }

        public bool IsFlickr { get; set; }

        public bool NotFile { get; set; }

        [Display(Name = "Internet URL: ")]
        public string Url { get; set; }

        [Range(1, int.MaxValue)]
        [HiddenInput(DisplayValue = false)]
        public int Width { get; set; }

        public bool Wrong { get; set; }

        [Range(0, int.MaxValue)]
        [HiddenInput(DisplayValue = false)]
        public int X { get; set; }

        [Range(0, int.MaxValue)]
        [HiddenInput(DisplayValue = false)]
        public int Y { get; set; }

        #endregion Public Properties

        #region Public Methods

        public static Bitmap CreateImage(Image original, int x, int y, int width, int height)
        {
            var img = new Bitmap(width, height);

            using (var g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            }

            return img;
        }

        public static Bitmap GetImageFromUrl(string url)
        {
            const int buffer = 1024;
            Bitmap image;

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return null;

            using (var ms = new MemoryStream())
            {
                var req = WebRequest.Create(url);

                using (var resp = req.GetResponse())
                {
                    using (var stream = resp.GetResponseStream())
                    {
                        var bytes = new byte[buffer];
                        int n;

                        while (stream != null && (n = stream.Read(bytes, 0, buffer)) != 0)
                            ms.Write(bytes, 0, n);
                    }
                }

                image = Image.FromStream(ms) as Bitmap;
            }

            return image;
        }

        #endregion Public Methods
    }
}