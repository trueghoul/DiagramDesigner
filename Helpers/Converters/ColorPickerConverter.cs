using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace DiagramDesigner
{
    [ValueConversion(typeof(Color), typeof(Brush))]
    public class ColorPickerConverter : IValueConverter
    {
        static ColorPickerConverter()
        {
            Instance = new ColorPickerConverter();
        }

        public static ColorPickerConverter Instance
        {
            get;
            private set;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush colorBrush = (SolidColorBrush)value;
            Color color = colorBrush.Color;
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = (Color)value;
            SolidColorBrush brush = new SolidColorBrush(color);
            return brush;
        }
    }
}
