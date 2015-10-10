namespace Defender.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Media;
    using Defender.Model.Extensions;

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
    
    /// <summary>
    /// Converter class to convert a given string array to concatenated string.
    /// Enumerates each item in the array and adds it to a string using ', ' (comma &amp; space) as the separator.
    /// </summary>
    public class StringArrayToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "no";

            return new StringBuilder()
                           .AppendSequence(
                               (string[])value,
                               (sb, str) => sb.AppendFormat("{0}, ", str))?
                           .ToString()
                           .TrimEnd(", ".ToCharArray());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string)?.Split(',') ?? new string[0];
        }
    }
    
    /// <summary>
    /// Converter class to convert a given string array to integer count.
    /// Calculates the number of items in the given array and returns the count.
    /// <see cref="ConvertBack(object, Type, object, CultureInfo)"/> currently not supported (not really possible :P ).
    /// </summary>
    public class StringArrayToInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "no";

            return (value as string[])?.Count() ?? 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
