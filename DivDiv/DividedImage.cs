using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DivDiv
{
    internal class DividedImage: ViewModelBase
    {
        #region == IsLandScape ==

        private bool _IsLandScape;
        public bool IsLandScape
        {
            get => _IsLandScape;
            set
            {
                if (_IsLandScape != value)
                {
                    _IsLandScape = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion
        #region == Images ==

        private readonly ObservableCollection<BitmapSource> _Images = new ObservableCollection<BitmapSource>();
        public ObservableCollection<BitmapSource> Images => _Images;

        #endregion

        private BitmapSource BaseImage { get; }

        public DividedImage(BitmapSource baseImage)
        {
            BaseImage = baseImage;
        }

        public Task UpdateAsync(SizeType sizeType, DividingMode dividingMode) => Task.Run(() =>
          {
              Images.Clear();

              double width = BaseImage.Width;
              double height = BaseImage.Height;

              IsLandScape = width > height;

              double ratio = sizeType.Ratio();
              if (IsLandScape)
              {
                  ratio = 1 / ratio;
              }

              if (height < width * ratio)
              {
                  height = width * ratio;
              }
              else
              {
                  width = height / ratio;
              }

              Application.Current.Dispatcher.Invoke(() =>
              {
                  switch (dividingMode)
                  {
                      case DividingMode.Into2:
                          DividInto2(width, height);
                          return;
                      case DividingMode.Into4:
                          DividInto4(width, height);
                          return;
                  }
              });
          });

        private void DividInto2(double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;

            double halfBaseWidth = BaseImage.Width / 2;
            double halfBaseHeight = BaseImage.Height / 2;

            int halfPixelWidth = BaseImage.PixelWidth / 2;
            int halfPixelHeight = BaseImage.PixelHeight / 2;

            if (IsLandScape)
            {
                AddClippedImage(
                    new Int32Rect(0, 0, halfPixelWidth, BaseImage.PixelHeight),
                    new Rect(halfWidth - halfBaseWidth, halfHeight - halfBaseHeight, halfBaseWidth, BaseImage.Height),
                    new Size(halfWidth, height));
                AddClippedImage(
                    new Int32Rect(halfPixelWidth, 0, halfPixelWidth, BaseImage.PixelHeight),
                    new Rect(0, halfHeight - halfBaseHeight, halfBaseWidth, BaseImage.Height),
                    new Size(halfWidth, height));
            }
            else
            {
                AddClippedImage(
                    new Int32Rect(0, 0, BaseImage.PixelWidth, halfPixelHeight),
                    new Rect(halfWidth - halfBaseWidth, halfHeight - halfBaseHeight, BaseImage.Width, halfBaseHeight),
                    new Size(width, halfHeight));
                AddClippedImage(
                    new Int32Rect(0, halfPixelHeight, BaseImage.PixelWidth, halfPixelHeight),
                    new Rect(halfWidth - halfBaseWidth, 0, BaseImage.Width, halfBaseHeight),
                    new Size(width, halfHeight));
            }
        }

        private void DividInto4(double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;

            double halfBaseWidth = BaseImage.Width / 2;
            double halfBaseHeight = BaseImage.Height / 2;

            int halfPixelWidth = BaseImage.PixelWidth / 2;
            int halfPixelHeight = BaseImage.PixelHeight / 2;

            Size size = new Size(halfWidth, halfHeight);

            AddClippedImage(
                new Int32Rect(0, 0, halfPixelWidth, halfPixelHeight),
                new Rect(halfWidth - halfBaseWidth, halfHeight - halfBaseHeight, halfBaseWidth, halfBaseHeight),
                size);
            AddClippedImage(
                new Int32Rect(halfPixelWidth, 0, halfPixelWidth, halfPixelHeight),
                new Rect(0, halfHeight - halfBaseHeight, halfBaseWidth, halfBaseHeight),
                size);
            AddClippedImage(
                new Int32Rect(0, halfPixelHeight, halfPixelWidth, halfPixelHeight),
                new Rect(halfWidth - halfBaseWidth, 0, halfBaseWidth, halfBaseHeight),
                size);
            AddClippedImage(
                new Int32Rect(halfPixelWidth, halfPixelHeight, halfPixelWidth, halfPixelHeight),
                new Rect(0, 0, halfBaseWidth, halfBaseHeight),
                size);
        }

        private void AddClippedImage(Int32Rect sourceRect, Rect targetRect, Size bitmapSize)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContent = drawingVisual.RenderOpen())
            {
                drawingContent.DrawImage(new CroppedBitmap(BaseImage, sourceRect), targetRect);
            }

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)bitmapSize.Width, (int)bitmapSize.Height, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(drawingVisual);
            renderTargetBitmap.Freeze();
            Images.Add(renderTargetBitmap);
        }
    }
}
