using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Project01
{
    class Root
    {
        public static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                try
                {
                    if (!Directory.Exists(arg))
                    {
                        continue;
                    }

                    string[] files = Directory.GetFiles(arg, "*.txt", SearchOption.AllDirectories);

                    int size = 48;
                    float padding = size * 0.1f;
                    Font font = new Font("游明朝", size - padding * 2 - 6, FontStyle.Regular, GraphicsUnit.Pixel);
                    Brush brush = Brushes.Black;
                    Pen pen = Pens.Black;

                    int l0 = (int)(size * 1.0f + padding * 2);
                    int l1 = size * 5;
                    int l2 = size * 8;
                    int l3 = l0 + l1 + l2 + size;

                    Bitmap bitmap = new Bitmap(size + l3 * files.Length, size * 42);
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.Clear(Color.White);
                    //graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                    //graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                    int i = 0;
                    foreach (string file in files)
                    {
                        graphics.DrawString(Path.GetFileNameWithoutExtension(file) + "組", font, brush, new PointF(size + l3 * i + padding, size + padding));
                        graphics.DrawRectangle(pen, new Rectangle(size + l3 * i, size, l3 - size, size));

                        int j = 1;
                        StreamReader reader = File.OpenText(file);
                        while (true)
                        {
                            j++;

                            string line = reader.ReadLine();
                            if (line == null)
                            {
                                break;
                            }

                            if (line.Length == 0)
                            {
                                j--;
                                continue;
                            }

                            if (j % 2 == 0)
                            {
                                graphics.FillRectangle(Brushes.WhiteSmoke, new Rectangle(size + l3 * i, size * j, l3 - size, size));
                            }

                            graphics.DrawString((j - 1).ToString(), font, brush, new PointF(size + l3 * i + padding, size * j + padding));
                            graphics.DrawRectangle(pen, new Rectangle(size + l3 * i, size * j, l0, size));

                            string[] names = line.Split('（');
                            graphics.DrawString(names[0], font, brush, new PointF(size + l3 * i + l0 + padding, size * j + padding));
                            graphics.DrawRectangle(pen, new Rectangle(size + l3 * i + l0, size * j, l1, size));

                            if (names.Length > 1)
                            {
                                graphics.DrawString(names[1].Replace('）', ' '), font, brush, new PointF(size + l3 * i + l0 + l1 + padding, size * j + padding));
                                graphics.DrawRectangle(pen, new Rectangle(size + l3 * i + l0 + l1, size * j, l2, size));
                            }
                        }
                        reader.Close();
                        i++;
                    }

                    graphics.Dispose();
                    bitmap.Save(Path.Combine(arg, "result.png"));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
