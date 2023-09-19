using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.Streams;

namespace DivDiv
{
    internal class Loader
    {
        public static int LimitedWidth;

        public static async IAsyncEnumerable<ImageSource> Load(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                yield break;
            }

            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                await foreach (ImageSource imageSource in LoadFile(fileInfo))
                {
                    yield return imageSource;
                }

                yield break;
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (directoryInfo.Exists)
            {
                foreach (FileInfo subFileInfo in directoryInfo.EnumerateFiles())
                {
                    await foreach (ImageSource imageSource in LoadFile(subFileInfo))
                    {
                        yield return imageSource;
                    }
                }
            }
        }

        private static async IAsyncEnumerable<ImageSource> LoadFile(FileInfo fileInfo)
        {
            switch (fileInfo.Extension.ToLower())
            {
                case ".pdf":
                    await foreach (ImageSource imageSource in LoadPDF(fileInfo))
                    {
                        yield return imageSource;
                    }
                    break;

                case ".png":
                case ".jpg":
                    yield return await LoadImage(fileInfo);
                    break;
            }
        }

        private static async IAsyncEnumerable<ImageSource> LoadPDF(FileInfo fileInfo)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(fileInfo.FullName);
            PdfDocument pdfDocument;

            try
            {
                pdfDocument = await PdfDocument.LoadFromFileAsync(file);
            }
            catch
            {
                    yield break;
            }

            if (pdfDocument != null)
            {
                for (uint i = 0; i < pdfDocument.PageCount; i++)
                {
                    using (PdfPage page = pdfDocument.GetPage(i))
                    {
                        using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                        {
                            PdfPageRenderOptions renderOptions = new PdfPageRenderOptions();
                            renderOptions.DestinationWidth = (uint)Math.Round(page.Dimensions.ArtBox.Width / 96.0 * LimitedWidth);
                            await page.RenderToStreamAsync(stream, renderOptions);
                            PngBitmapDecoder decoder = new PngBitmapDecoder(stream.AsStream(), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                            BitmapFrame bitmapFrame = decoder.Frames[0];
                            bitmapFrame.Freeze();
                            yield return bitmapFrame;
                        }
                    }
                }
            }
        }

        private static Task<ImageSource> LoadImage(FileInfo fileInfo) => Task.Run(() =>
          {
              try
              {
                  BitmapImage bitmapImage = new BitmapImage();
                  bitmapImage.BeginInit();
                  bitmapImage.UriSource = new Uri(fileInfo.FullName);
                  bitmapImage.DecodePixelWidth = LimitedWidth;
                  bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                  bitmapImage.CreateOptions = BitmapCreateOptions.None;
                  bitmapImage.EndInit();
                  bitmapImage.Freeze();

                  return (ImageSource)bitmapImage;
              }
              catch { }

              return null;
          });
    }
}
