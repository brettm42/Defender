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
        private static readonly FontWeight Bold = FontWeights.Bold;
        private static readonly FontWeight Regular = FontWeights.Regular;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value > 0 ? Bold : Regular;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
