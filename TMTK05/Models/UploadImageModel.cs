using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace TMTK05.Models
{
    public class UploadImageModel
    {
        public UploadImageModel()
        {
            Done = false;
            Wrong = false;
            NotFile = false;
        }

        [Display(Name = "Link to image: ")]
        public string Url { get; set; }

        public bool IsUrl { get; set; }

        [Display(Name = "Upload file: ")]
        public HttpPostedFileBase File { get; set; }

        public bool IsFile { get; set; }

        [Range(0, int.MaxValue)]
        public int X { get; set; }

        [Range(0, int.MaxValue)]
        public int Y { get; set; }

        [Range(1, int.MaxValue)]
        public int Width { get; set; }

        [Range(1, int.MaxValue)]
        public int Height { get; set; }

        public bool NotFile { get; set; }

        public bool Done { get; set; }

        public bool Wrong { get; set; }

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
    }
}