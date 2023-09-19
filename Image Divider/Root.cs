using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Image_Divider
{
    class Root
    {
        public static void Main(string[] args)
        {
            int i = 1;

            foreach (string arg in args)
            {
                try
                {
                    Image image = Image.FromFile(arg);
                    Size size = new Size((int)(image.Height * 1.4141), image.Height);
                    if (size.Width < image.Width)
                    {
                        size.Width = image.Width;
                        size.Height = (int)(size.Width / 1.4141);
                    }

                    Bitmap bitmap = new Bitmap(size.Width, size.Height);
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.DrawImage(image, new Rectangle(new Point(0, 0), size), new Rectangle(new Point((image.Width - size.Width) / 2, (image.Height - size.Height) / 2), size), GraphicsUnit.Pixel);
                    graphics.Dispose();

                    string name = Path.GetFileNameWithoutExtension(arg);
                    string path = Path.GetDirectoryName(arg) + "\\" + name;
                    string extension = Path.GetExtension(arg);
                    Directory.CreateDirectory(path);

                    size.Width /= 2;
                    size.Height /= 2;
                    Rectangle rectangle = new Rectangle(new Point(0, 0), size);

                    Bitmap bitmap1 = new Bitmap(size.Width, size.Height);
                    Graphics graphics1 = Graphics.FromImage(bitmap1);
                    graphics1.DrawImage(bitmap, rectangle, new Rectangle(new Point(0, 0), size), GraphicsUnit.Pixel);
                    graphics1.Dispose();
                    bitmap1.Save($"{path}\\{i:0000}_{name}_1{extension}", image.RawFormat);

                    Bitmap bitmap2 = new Bitmap(size.Width, size.Height);
                    Graphics graphics2 = Graphics.FromImage(bitmap2);
                    graphics2.DrawImage(bitmap, rectangle, new Rectangle(new Point(size.Width, 0), size), GraphicsUnit.Pixel);
                    graphics2.Dispose();
                    bitmap2.Save($"{path}\\{i:0000}_{name}_2{extension}", image.RawFormat);

                    Bitmap bitmap3 = new Bitmap(size.Width, size.Height);
                    Graphics graphics3 = Graphics.FromImage(bitmap3);
                    graphics3.DrawImage(bitmap, rectangle, new Rectangle(new Point(0, size.Height), size), GraphicsUnit.Pixel);
                    graphics3.Dispose();
                    bitmap3.Save($"{path}\\{i:0000}_{name}_3{extension}", image.RawFormat);

                    Bitmap bitmap4 = new Bitmap(size.Width, size.Height);
                    Graphics graphics4 = Graphics.FromImage(bitmap4);
                    graphics4.DrawImage(bitmap, rectangle, new Rectangle(new Point(size), size), GraphicsUnit.Pixel);
                    graphics4.Dispose();
                    bitmap4.Save($"{path}\\{i:0000}_{name}_4{extension}", image.RawFormat);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                i++;
            }
        }
    }
}
