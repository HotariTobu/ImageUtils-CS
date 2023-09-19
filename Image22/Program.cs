using System;
using System.Drawing;
using System.IO;

namespace Image22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                try
                {
                    string name = Path.GetFileNameWithoutExtension(arg);
                    string path = Path.Combine(Path.GetDirectoryName(arg), name);
                    string extension = Path.GetExtension(arg);

                    Console.WriteLine(path);

                    Image image = Image.FromFile(arg);

                    int width = image.Width / 2;
                    int height = image.Height;

                    Size size = new Size(width, height);
                    Rectangle rectangle = new Rectangle(Point.Empty, size);

                    Bitmap bitmap1 = new Bitmap(width, height);
                    using (Graphics graphics1 = Graphics.FromImage(bitmap1))
                    {
                        graphics1.DrawImage(image, rectangle, new Rectangle(Point.Empty, size), GraphicsUnit.Pixel);
                    }
                    bitmap1.Save($"{path}_1{extension}", image.RawFormat);

                    Bitmap bitmap2 = new Bitmap(width, height);
                    using (Graphics graphics2 = Graphics.FromImage(bitmap2))
                    {
                        graphics2.DrawImage(image, rectangle, new Rectangle(new Point(width, 0), size), GraphicsUnit.Pixel);
                    }
                    bitmap2.Save($"{path}_2{extension}", image.RawFormat);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}