namespace Defender.ViewModel.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    /// Converter class to convert a given boolean value to color, used for indicating system/execution status.
    /// true = green, false = red
    /// </summary>
    public class BoolToFillConverter : IValueConverter
    {
        private static readonly string Red = Colors.IndianRed.ToString();
        private static readonly string LGreen = Colors.LightGreen.ToString();
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value ? LGreen : Red;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (string)value == LGreen;
    }
}
