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

            int MaxHeight = 500;
            int MinHeight = 100;
        }

        private void HidePanel_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataPanel.Visibility == Visibility.Visible)
            {
                this.DataPanel.Visibility = Visibility.Collapsed;
                this.HidePanel.Content = @"˄";
            }
            else
            {
                this.DataPanel.Visibility = Visibility.Visible;
                this.HidePanel.Content = @"˅";
            }
        }
        
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog()
                                      {
                                          Title = "Select the RQF folder for Handback",
                                          FileName = " -----Select This Folder----- ",
                                          Filter = "Query folder (*.*)|*.rqf|RQF files (*.rqf)|*.rqf",
                                          CheckFileExists = false,
                                      };
            
            (this.DataContext as ViewModel.ViewModel).Folder = (openfile.ShowDialog() == true) 
                                                               ? openfile.FileName 
                                                               : (this.DataContext as ViewModel.ViewModel).Folder;

        }

        private void DataPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DataPanel.MouseMove += DataPanel_MouseMove;
        }

        private void DataPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
            this.DataPanel.MouseMove -= DataPanel_MouseMove;
        }

        private void DataPanel_MouseMove(object sender, MouseEventArgs e)
        {
            CaptureMouse();
            this.DataPanel.Height = (Math.Abs(e.GetPosition(null).Y) >= MinHeight)
                                    ? ((Math.Abs(e.GetPosition(null).Y) <= MaxHeight) 
                                        ? Math.Abs(e.GetPosition(null).Y) 
                                        : MaxHeight)
                                    : MinHeight;
        }
    }
}
