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

List<int> list = new List<int>();

string location = Assembly.GetEntryAssembly()?.Location ?? "";
string optionFile = Path.Combine(Path.GetDirectoryName(location) ?? "", "o.txt");
if (File.Exists(optionFile))
{
    string[] lines = File.ReadAllLines(optionFile);
    foreach (string line in lines)
    {
        if (int.TryParse(line, out int value))
        {
            list.Add(value);
        }
    }
}

if (list.Any())
{
    Console.WriteLine($"default scales = {string.Join(", ", list)}");
}

foreach (FileInfo fileInfo in fromPath.EnumerateFiles("*.png"))
{
    try
    {
        using Bitmap baseBitmap = new Bitmap(fileInfo.FullName);
        int scale = list.FirstOrDefault(x => isFit(x), 0);

        if (scale <= 0)
        {
            scale = getScale(baseBitmap);
        }

        if (!isFit(scale))
        {
            Console.WriteLine($"WARNING: {fileInfo.Name} {baseBitmap.Width} x {baseBitmap.Height}, scale = {scale}");
        }

        int width = baseBitmap.Width / scale;
        int height = baseBitmap.Height / scale;

        Bitmap resizedBitmap = new Bitmap(width, height);
        Graphics graphics = Graphics.FromImage(resizedBitmap);
        graphics.DrawImage(baseBitmap, 0, 0, width, height);
        graphics.Dispose();

        string path = Path.Combine(toPath.FullName, fileInfo.Name);
        resizedBitmap.Save(path, ImageFormat.Png);

        bool isFit(int scale)
        {
            return scale != 0 && baseBitmap.Width % scale == 0 && baseBitmap.Height % scale == 0;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"ERROR: {fileInfo.Name} {e.Message}");
    }
}

Console.WriteLine("Done...");
Console.ReadKey();

int getScale(Bitmap bitmap)
{
    int[,] pixels = getPixels(bitmap);

    int minCount = Math.Max(bitmap.Width, bitmap.Height);

    int x, y;
    int lastColor, count;

    for (x = 0; x < bitmap.Width; x++)
    {
        reset();

        for (y = 0; y < bitmap.Height; y++)
        {
            update();
        }
    }

    for (y = 0; y < bitmap.Height; y++)
    {
        reset();

        for (x = 0; x < bitmap.Width; x++)
        {
            update();
        }
    }

    return minCount;

    void reset()
    {
        lastColor = 0;
        count = 0;
    }

    void update()
    {
        int color = pixels[x, y];
        if (color == lastColor)
        {
            count++;
        }
        else
        {
            if (count > 0 && minCount > count)
            {
                minCount = count;
            }

            lastColor = color;
            count = 1;
        }
    }
}

int[,] getPixels(Bitmap bitmap)
{
    BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

    // メモリの幅のバイト数を取得
    int stride = Math.Abs(bitmapData.Stride);
    // チャンネル数を取得（8, 24, 32bitのBitmapのみを想定）
    int channel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;

    // 元の画像データ用配列
    byte[] srcData = new byte[stride * bitmapData.Height];
    // Bitmapデータを配列へコピー
    Marshal.Copy(bitmapData.Scan0, srcData, 0, srcData.Length);

    // アンロック
    bitmap.UnlockBits(bitmapData);

    int[,] pixels = new int[bitmapData.Width, bitmapData.Height];

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

            int argb = a << 24 | r << 16 | g << 8 | b;

            pixels[x, y] = argb;

        }
    }

    return pixels;
}