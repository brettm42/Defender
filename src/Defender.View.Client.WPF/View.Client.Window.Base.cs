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

namespace Defender.View.Client.WPF
{
    public class ViewBase
    {
        public void ToggleVisibility<T>(T[] fields)
        {
            foreach (T item in fields)
            {
                (item as System.Windows.Controls.Control).Visibility = ((item as System.Windows.Controls.Control).Visibility == Visibility.Visible)
                                                                       ? Visibility.Collapsed
                                                                       : Visibility.Visible;
            }
        }

    }
}
