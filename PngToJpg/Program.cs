using System.Drawing;
using System.Drawing.Imaging;

namespace PngToJpg
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                try
                {
                    using (Image image = Image.FromFile(arg))
                    {
                        Bitmap bitmap = new Bitmap(image.Width, image.Height);
                        Graphics graphics = Graphics.FromImage(bitmap);
                        graphics.Clear(Color.White);
                        graphics.DrawImage(image, new Point(0, 0));
                        graphics.Dispose();
                        bitmap.Save(arg.Replace("png", "jpg"), ImageFormat.Jpeg);
                    }
                }
                catch
                {

                }
            }
        }
    }
}
