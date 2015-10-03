using System;
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

namespace Defender.View.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void HidePanel_Click(object sender, RoutedEventArgs e)
        {
            if (this.StatsGrid.Visibility == Visibility.Visible)
            {
                this.StatsGrid.Visibility = Visibility.Collapsed;
                this.HidePanel.Content = @"˄";
            }
            else
            {
                this.StatsGrid.Visibility = Visibility.Visible;
                this.HidePanel.Content = @"˅";
            }
        }
        
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog()
            {
                Title = "Select the RQF Folder for Handback",
                FileName = " -----Select This Folder----- ",
                CheckFileExists = false,
                Filter = "Query folder (*.*)|*.rqf|RQF files (*.rqf)|*.rqf"
            };
            
            (this.DataContext as ViewModel.ViewModel).Folder = (openfile.ShowDialog() == true) 
                                                               ? openfile.FileName 
                                                               : (this.DataContext as ViewModel.ViewModel).Folder;

        }
    }
}
