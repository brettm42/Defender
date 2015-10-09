using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Defender.ViewModel;

namespace System.Windows.Controls
{
    public static class StackPanelExtensions
    {
        public static void ToggleVisibility<T>(T[] fields)
        {
            foreach (T field in fields)
            {
                (field as StackPanel).Visibility = ((field as StackPanel).Visibility == Visibility.Visible)
                                                   ? Visibility.Collapsed
                                                   : Visibility.Visible;
            }
        }

        public static void ToggleVisibility(this StackPanel @this)
        {
            @this.Visibility = (@this.Visibility == Visibility.Visible)
                               ? Visibility.Collapsed
                               : Visibility.Visible;
        }

    }
}
