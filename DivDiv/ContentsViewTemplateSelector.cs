using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DivDiv
{
    internal class ContentsViewTemplateSelector: DataTemplateSelector
    {
        public DataTemplate Into2TemplateH { get; set; }
        public DataTemplate Into2TemplateV { get; set; }
        public DataTemplate Into4Template { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is DividedImage dividedImage)
            {
                switch (dividedImage.Images.Count)
                {
                    case 2:
                        return dividedImage.IsLandScape ? Into2TemplateH : Into2TemplateV;

                    case 4:
                        return Into4Template;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
