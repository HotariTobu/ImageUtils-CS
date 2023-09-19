using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps;

namespace DivDiv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MWVM VM;

        public MainWindow()
        {
            InitializeComponent();

            VM = (MWVM)DataContext;
        }

        private async void VM_SelectedItemChanged(MWVM vm)
        {
            if (vm.SizeTypeItem is ComboBoxItem sizeTypeItem && vm.DividingModeItem is ComboBoxItem dividingModeItem)
            {
                if (sizeTypeItem.Tag is SizeType sizeType && dividingModeItem.Tag is DividingMode dividingMode)
                {
                    vm.SizeType = sizeType;
                    vm.DividingMode = dividingMode;

                    List<DividedImage> dividedImages = vm.Images.ToList();

                    vm.Images.Clear();

                    foreach (DividedImage dividedImage in dividedImages)
                    {
                        await dividedImage.UpdateAsync(sizeType, dividingMode);
                        vm.Images.Add(dividedImage);
                    }

                    GC.Collect();
                }
            }
        }

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            VM.IsLoadButtonEnabled = false;
            VM.Images.Clear();
            Loader.LimitedWidth = 100;
            await foreach (ImageSource imageSource in Loader.Load(VM.InputPath))
            {
                if (imageSource is BitmapSource bitmapSource)
                {
                    DividedImage dividedImage = new DividedImage(bitmapSource);
                    await dividedImage.UpdateAsync(VM.SizeType, VM.DividingMode);
                    VM.Images.Add(dividedImage);
                }
            }
            VM.IsLoadButtonEnabled = true;
        }

        private async void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            if (!VM.Images.Any())
            {
                return;
            }

            PrintDialog printDialog = new PrintDialog();
            if (!(printDialog.ShowDialog() is bool b) || !b)
            {
                return;
            }

            VM.IsPrintButtonEnabled = false;
            SizeType sizeType = VM.SizeType;
            DividingMode dividingMode = VM.DividingMode;
            FixedDocument fixedDocument = new FixedDocument();
            double marginValue = VM.MarginValue;
            Loader.LimitedWidth = VM.Quality;
            await foreach (ImageSource imageSource in Loader.Load(VM.InputPath))
            {
                if (imageSource is BitmapSource bitmapSource)
                {
                    DividedImage dividedImage = new DividedImage(bitmapSource);
                    await dividedImage.UpdateAsync(sizeType, dividingMode);
                    
                    foreach (BitmapSource bitmapImage in dividedImage.Images)
                    {
                        Image image = new Image();
                        image.Source = bitmapImage;

                        Canvas canvas = new Canvas();
                        Canvas.SetLeft(image, marginValue);
                        Canvas.SetTop(image, marginValue);
                        canvas.Children.Add(image);

                        FixedPage fixedPage = new FixedPage();
                        fixedPage.Width = bitmapImage.Width + marginValue * 2;
                        fixedPage.Height = bitmapImage.Height + marginValue * 2;
                        fixedPage.Children.Add(canvas);

                        PageContent pageContent = new PageContent();
                        pageContent.Child = fixedPage;

                        fixedDocument.Pages.Add(pageContent);
                    }
                }
            }
            PrintQueue printQueue = printDialog.PrintQueue;
            XpsDocumentWriter xpsDocumentWriter = PrintQueue.CreateXpsDocumentWriter(printQueue);
            xpsDocumentWriter.Write(fixedDocument);
            GC.Collect();
            VM.IsPrintButtonEnabled = true;
        }
    }
}
