using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Project02
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (File.Exists(((string[])e.Data.GetData(DataFormats.FileDrop))[0]))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                foreach (string path in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    if (File.Exists(path) && path.EndsWith("xlsx"))
                    {
                        A(path);
                    }
                }
            }
        }

        private void A(string path)
        {
            float width = (float)WidthBox.Value;
            float height = (float)HeightBox.Value;
            float radius = (float)RadiusBox.Value;
            float margin = (float)MarginBox.Value;
            float thickness = (float)ThicknessBox.Value;

            float padding = radius / 1.41421356f;

            Pen pen = new Pen(Color.Black, thickness);
            Font font = new Font(DefaultFont.FontFamily, height - padding * 2, GraphicsUnit.Pixel);

            string parent = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
            Directory.CreateDirectory(parent);

            try
            {
                XLWorkbook workbook = new XLWorkbook(path);
                foreach (IXLWorksheet worksheet in workbook.Worksheets)
                {
                    int rowCount = worksheet.LastRowUsed().RowNumber();
                    int columnCount = worksheet.LastColumnUsed().ColumnNumber();

                    Bitmap bitmap = new Bitmap((int)(width * columnCount + margin * (columnCount + 1)), (int)(height * rowCount + margin * (rowCount + 1)));
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                    for (int row = 1; row <= rowCount; row++)
                    {
                        for (int column = 1; column <= columnCount; column++)
                        {
                            string value = worksheet.Cell(row, column).Value.ToString();
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                float x = width * (column - 1) + margin * column;
                                float y = height * (row - 1) + margin * row;
                                DrawRoundRectangle(graphics, x, y, width, height, radius, pen);

                                SizeF textSize = graphics.MeasureString(value, font);
                                if (textSize.Width < width - padding * 2)
                                {
                                    graphics.DrawString(value, font, Brushes.Black, x + (width - textSize.Width) / 2f, y + padding);
                                }
                                else
                                {
                                    Bitmap text = new Bitmap((int)textSize.Width, (int)textSize.Height);
                                    using (Graphics g = Graphics.FromImage(text))
                                    {
                                        g.DrawString(value, font, Brushes.Black, new PointF());
                                    }

                                    float destHeight = width / textSize.Width * textSize.Height;
                                    graphics.DrawImage(text, new RectangleF(new PointF(x, y + (height - destHeight) / 2f), new SizeF(width, destHeight)), new RectangleF(new PointF(), textSize), GraphicsUnit.Pixel);
                                }
                            }
                        }
                    }

                    graphics.Dispose();
                    bitmap.Save($"{Path.Combine(parent, worksheet.Name)}.png");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}\n\n{e.StackTrace}\n\n{e.Source}");
            }
        }

        private void DrawRoundRectangle(Graphics g, float x, float y, float w, float h, float r, Pen pen)
        {
            float a = (float)(4 * (1.41421356 - 1) / 3 * r);

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddBezier(x, y + r, x, y + r - a, x + r - a, y, x + r, y); /* 左上 */
            path.AddBezier(x + w - r, y, x + w - r + a, y, x + w, y + r - a, x + w, y + r); /* 右上 */
            path.AddBezier(x + w, y + h - r, x + w, y + h - r + a, x + w - r + a, y + h, x + w - r, y + h); /* 右下 */
            path.AddBezier(x + r, y + h, x + r - a, y + h, x, y + h - r + a, x, y + h - r); /* 左下 */
            path.CloseFigure();

            g.DrawPath(pen, path);
        }
    }
}
