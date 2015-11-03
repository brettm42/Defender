namespace Defender.ViewModel
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    
    public class IntToFontWeight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value > 0)
                   ? System.Windows.FontWeights.Bold
                   : System.Windows.FontWeights.Regular;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
