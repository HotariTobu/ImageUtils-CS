using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;

if (args.Length < 2)
{
    Console.WriteLine("Require 2 args...");
    Console.ReadKey();
    return;
}

DirectoryInfo fromPath = new DirectoryInfo(args[0]);
DirectoryInfo toPath = new DirectoryInfo(args[1]);

if (!fromPath.Exists || !toPath.Exists)
{
    Console.WriteLine("Arg must be directory path...");
    Console.ReadKey();
    return;
}

byte threshold = 255;

string location = Assembly.GetEntryAssembly()?.Location ?? "";
string optionFile = Path.Combine(Path.GetDirectoryName(location) ?? "", "o.txt");
if (File.Exists(optionFile))
{
    string text = File.ReadAllText(optionFile);
    byte.TryParse(text, out threshold);
}

Console.WriteLine($"Threshold = {threshold}");

foreach (FileInfo fileInfo in fromPath.EnumerateFiles("*.png"))
{
    try
    {
        using Bitmap bitmap = new Bitmap(fileInfo.FullName);
        binarize(bitmap);

        string path = Path.Combine(toPath.FullName, fileInfo.Name);
        bitmap.Save(path, ImageFormat.Png);
    }
    catch (Exception e)
    {
        Console.WriteLine($"ERROR: {fileInfo.Name} {e.Message}");
    }
}

Console.WriteLine("Done...");
Console.ReadKey();

void binarize(Bitmap bitmap)
{
    BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

    // メモリの幅のバイト数を取得
    int stride = Math.Abs(bitmapData.Stride);
    // チャンネル数を取得（8, 24, 32bitのBitmapのみを想定）
    int channel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;

    // 元の画像データ用配列
    byte[] srcData = new byte[stride * bitmapData.Height];
    // 処理後の画像データ用配列
    byte[] dstData = new byte[stride * bitmapData.Height];

    // Bitmapデータを配列へコピー
    Marshal.Copy(bitmapData.Scan0, srcData, 0, srcData.Length);

    for (int y = 0; y < bitmapData.Height; y++)
    {
        for (int x = 0; x < bitmapData.Width; x++)
        {
            //(x,y)のデータ位置
            int pos = y * stride + x * channel;

            byte a = 255, r = 0, g = 0, b = 0;

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

            byte v = Math.Max(Math.Max(r, g), b);

            byte c;

            if (v < threshold)
            {
                c = 0;
            }
            else
            {
                c = 255;
            }

            switch (channel)
            {
                case 1:
                    dstData[pos] = c;
                    break;
                case 3:
                    dstData[pos] = c;
                    dstData[pos + 1] = c;
                    dstData[pos + 2] = c;
                    break;
                case 4:
                    dstData[pos] = c;
                    dstData[pos + 1] = c;
                    dstData[pos + 2] = c;
                    dstData[pos + 3] = 255;
                    break;
            }
        }
    }

    Marshal.Copy(dstData, 0, bitmapData.Scan0, dstData.Length);

    // アンロック
    bitmap.UnlockBits(bitmapData);
}