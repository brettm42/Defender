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
        private static int ElemMaxHeight { get; set; }
        private static int ElemMinHeight { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();
            
            ElemMinHeight = (int)this.Height / 6;
            ElemMaxHeight = (int)this.Height - ElemMinHeight;
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

            (this.DataContext as ViewModel.ViewModel).ValidateFiles();

        }

        private void DataPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(this.DataPanel);
            this.DataPanel.MouseMove += DataPanel_MouseMove;
        }

        private void DataPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            this.DataPanel.MouseMove -= DataPanel_MouseMove;
        }

        private void DataPanel_MouseMove(object sender, MouseEventArgs e)
        {
            this.DataPanel.Height = this.ActualHeight - e.GetPosition(this).Y >= ElemMinHeight
                                    ? this.ActualHeight - e.GetPosition(this).Y <= ElemMaxHeight
                                        ? this.ActualHeight - e.GetPosition(this).Y
                                        : ElemMaxHeight
                                    : ElemMinHeight;
        }

        #region TouchEvents
        private void LoadButton_TouchDown(object sender, TouchEventArgs e)
        {
            LoadButton_Click(null, null);
        }

        private void HidePanel_TouchDown(object sender, TouchEventArgs e)
        {
            HidePanel_Click(null, null);
        }

        private void DataPanel_TouchDown(object sender, TouchEventArgs e)
        {
            CaptureTouch(null);
            this.DataPanel.TouchMove += DataPanel_TouchMove;
        }
        
        private void DataPanel_TouchUp(object sender, TouchEventArgs e)
        {
            ReleaseTouchCapture(null);
            this.DataPanel.TouchMove -= DataPanel_TouchMove;
        }

        private void DataPanel_TouchMove(object sender, TouchEventArgs e)
        {
            this.DataPanel.Height = this.ActualHeight - e.GetTouchPoint(this).Position.Y >= ElemMinHeight
                                    ? this.ActualHeight - e.GetTouchPoint(this).Position.Y <= ElemMaxHeight
                                        ? this.ActualHeight - e.GetTouchPoint(this).Position.Y
                                        : ElemMaxHeight
                                    : ElemMinHeight;
        }
        #endregion

        private void RQFPath_KeyDown(object sender, KeyEventArgs e)
        {
            (this.DataContext as ViewModel.ViewModel).ValidateFiles();
        }
    }
}
