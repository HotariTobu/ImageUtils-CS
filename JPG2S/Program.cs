using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace JPG2S
{
    class Program
    {
        static void Main(string[] args)
        {
            double sqrt2 = 1 / Math.Sqrt(2);
            foreach (string arg in args)
            {
                try
                {
                    using (Image image = Image.FromFile(arg))
                    {
                        Size size = new Size((int)(image.Width * sqrt2), (int)(image.Height * sqrt2));
                        Bitmap bitmap = new Bitmap(size.Width, size.Height);
                        Graphics graphics = Graphics.FromImage(bitmap);
                        graphics.DrawImage(image, 0, 0, size.Width, size.Height);
                        graphics.Dispose();
                        bitmap.Save(Path.Combine(Path.GetDirectoryName(arg), "_" + Path.GetFileName(arg)), ImageFormat.Jpeg);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
