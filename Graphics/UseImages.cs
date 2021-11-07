using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Controller;
using Model;

namespace Graphics
{
    public static class UseImages
    {
        private static readonly Dictionary<string, Bitmap> Images = new Dictionary<string, Bitmap>();

        public static Bitmap CachingAndLoading(string parameter)
        {
            if (!Images.ContainsKey(parameter))
            {
                Images.Add(parameter, new Bitmap(parameter));
            }

            return (Bitmap)Images[parameter].Clone();
        }

        public static void Clear()
        {
            Images.Clear();
        }

        public static Bitmap GetEmptyBitmap(int x, int y)
        {
            if (!Images.ContainsKey("empty"))
            {
                Images.Add("empty", new Bitmap(x, y));
            }
            return (Bitmap)Images["empty"].Clone();
        }

        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static Bitmap Get(string key)
        {
            if (!Images.ContainsKey(key))
            {

                Bitmap bitmap = (Bitmap)Image.FromFile(key);
                Images.Add(key, bitmap);
            }
            return (Bitmap)Images[key].Clone();
            //try
            //{
            //    if (!Images.ContainsKey(key))
            //    {

            //        Bitmap bitmap = new Bitmap(key);
            //        Images.Add(key, bitmap);
            //    }
            //    return (Bitmap)Images[key].Clone();
            //} catch(Exception e)
            //{
            //    return (Bitmap)Images[key].Clone();
            //}
        }
    }
}
