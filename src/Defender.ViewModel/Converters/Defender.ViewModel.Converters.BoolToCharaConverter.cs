namespace Defender.ViewModel
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    
    /// <summary>
    /// Converter class to convert a given boolean value to character, used for indicating system/execution status.
    /// true = ✓ (checkmark), false = X
    /// </summary>
    public class BoolToCharaConverter : IValueConverter
    {
        private const string X = @"X";
        private const string Check = @"✓";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Check : X;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value == Check) ? true : false;
        }
    }
}
