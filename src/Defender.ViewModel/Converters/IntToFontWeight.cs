namespace Defender.ViewModel
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    
    /// <summary>
    /// Converts any integer over 0 into font weight bold. Otherwise regular weight font.
    /// </summary>
    public class IntToFontWeight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value > 0 ? FontWeights.Bold : FontWeights.Regular;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
