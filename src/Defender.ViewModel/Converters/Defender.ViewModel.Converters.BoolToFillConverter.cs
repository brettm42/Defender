namespace Defender.ViewModel
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Converter class to convert a given boolean value to color, used for indicating system/execution status.
    /// true = green, false = red
    /// </summary>
    public class BoolToFillConverter : IValueConverter
    {
        private static string Red = System.Windows.Media.Colors.Red.ToString();
        private static string LGreen = System.Windows.Media.Colors.LightGreen.ToString();
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? LGreen : Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value == LGreen) ? true : false;
        }
    }
}
