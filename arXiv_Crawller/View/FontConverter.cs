using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace arXiv_Crawller
{
    internal class FontConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double actualWidth = (double)value;
            FrameworkElement element = parameter as FrameworkElement;

            string text = (element.FindName("TitleBlock") as TextBlock)?.Text;
            double maxLength = 100; // 设置一个最大长度，根据需要调整
            double scaleFactor = 0.5; // 调整比例，根据需要调整
            double maxFontSize = 50; // 设置最大字体大小，根据需要调整
            // 然后使用 text 和 actualWidth 计算字体大小...
            
            

            // 使用文本的长度来调整字体大小
            double textLength = text.Length;
            double desiredFontSize = Math.Min(maxLength / Math.Max(actualWidth, 1), actualWidth * scaleFactor) * (maxLength / Math.Max(textLength, 1));
            return Math.Min(desiredFontSize, maxFontSize);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
