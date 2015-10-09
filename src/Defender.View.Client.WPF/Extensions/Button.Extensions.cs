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
    public static class ButtonExtensions
    {
        public static void ToggleVisibility(this Button @this)
        {
            @this.Visibility = (@this.Visibility == Visibility.Visible)
                               ? Visibility.Collapsed
                               : Visibility.Visible;
        }

    }
}
