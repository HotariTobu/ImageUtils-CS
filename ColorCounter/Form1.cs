using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorCounter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                try
                {
                    pictureBox1.Load(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);

                    using (Bitmap bmp = new Bitmap(pictureBox1.Image))
                    {
                        // Bitmapをロック
                        System.Drawing.Imaging.BitmapData bmpData =
                            bmp.LockBits(
                                new Rectangle(0, 0, bmp.Width, bmp.Height),
                                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                                bmp.PixelFormat
                            );

                        // メモリの幅のバイト数を取得
                        int stride = Math.Abs(bmpData.Stride);
                        // チャンネル数を取得（8, 24, 32bitのBitmapのみを想定）
                        int channel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;

                        // 元の画像データ用配列
                        byte[] srcData = new byte[stride * bmpData.Height];
                        // 処理後の画像データ用配列
                        byte[] dstData = new byte[stride * bmpData.Height];

                        // Bitmapデータを配列へコピー
                        System.Runtime.InteropServices.Marshal.Copy(
                            bmpData.Scan0,
                            srcData,
                            0,
                            srcData.Length
                            );

                        Dictionary<int, int> dic = new Dictionary<int, int>();

                        for (int y = 0; y < bmpData.Height; y++)
                        {
                            for (int x = 0; x < bmpData.Width; x++)
                            {
                                //(x,y)のデータ位置
                                int pos = y * stride + x * channel;

                                int a = 255, r = 0, g = 0, b = 0;
                                switch (channel)
                                {
                                    case 1:
                                        b = r = g = srcData[pos];
                                        break;
                                    case 3:
                                        b = srcData[pos];
                                        g = srcData[pos + 1];
                                        r = srcData[pos + 2];
                                        break;
                                    case 4:
                                        b = srcData[pos];
                                        g = srcData[pos + 1];
                                        r = srcData[pos + 2];
                                        a = srcData[pos + 3];
                                        break;
                                }

                                int argb = a << 24 |
                                    r << 16 |
                                    g << 8 |
                                    b;

                                if (dic.ContainsKey(argb))
                                {
                                    dic[argb]++;
                                }
                                else
                                {
                                    dic.Add(argb, 1);
                                }
                            }
                        }

                        int count = 0;
                        flowLayoutPanel1.Controls.Clear();
                        foreach (int argb in dic.Keys)
                        {
                            UserControl1 control1 = new UserControl1();
                            control1.ButtonColor = Color.FromArgb(argb);
                            control1.LabelText = dic[argb].ToString();
                            flowLayoutPanel1.Controls.Add(control1);

                            count += dic[argb];
                        }
                        label1.Text = $"Counted {count}";

                        // 配列をBitmapデータへコピー
                        System.Runtime.InteropServices.Marshal.Copy(
                            dstData,
                            0,
                            bmpData.Scan0,
                            dstData.Length
                        );

                        // アンロック
                        bmp.UnlockBits(bmpData);
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
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
    }
}
