namespace Defender.ViewModel
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    
    /// <summary>
    /// Converter class to convert a given string array to concatenated string.
    /// Enumerates each item in the array and adds it to a string using ', ' (comma &amp; space) as the separator.
    /// </summary>
    public class StringArrayToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (!(value as string[])?.Any() ?? true)) return "no";

            return new StringBuilder()
                           .AppendSequence(
                               (string[])value,
                               (sb, str) => sb.AppendFormat("{0}, ", str))?
                           .ToString()
                           .Trim(", ".ToCharArray());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string)?.Split(',') ?? new string[0];
        }
    }
}