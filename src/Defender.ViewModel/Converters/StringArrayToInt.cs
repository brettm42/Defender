namespace Defender.ViewModel.Converters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;
    
    /// <summary>
    /// Converter class to convert a given string array to integer count.
    /// Calculates the number of items in the given array and returns the count.
    /// <see cref="ConvertBack(object, Type, object, CultureInfo)"/> currently not supported (not really possible :P ).
    /// </summary>
    public class StringArrayToInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (!(value as string[])?.Any() ?? true)) return "no";

            return (value as string[])?.Count() ?? 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
