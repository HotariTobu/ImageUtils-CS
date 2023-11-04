using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRToHex
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            using (Bitmap bitmap = new Bitmap(args[0]))
            {
                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format1bppIndexed);
                IntPtr ptr = bitmapData.Scan0;

                int arraySize = Math.Abs(bitmapData.Stride) * bitmap.Height;
                byte[] data = new byte[arraySize];

                System.Runtime.InteropServices.Marshal.Copy(ptr, data, 0, arraySize);

                long[] result = new long[(int)Math.Ceiling(arraySize / 8d)];
                for (int i = 0; i < arraySize; i++)
                {
                    result[i / 8] |= ((long)data[i]) << (8 * (i % 8));
                }

                Clipboard.SetText(string.Join(", ", result));
            }
        }
    }
}
