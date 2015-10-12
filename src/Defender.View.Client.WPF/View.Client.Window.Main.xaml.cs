namespace Defender.View.Client.WPF
{
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

    /// <summary>
    /// Interaction logic for View.Client.Window.Main.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        internal const string DownArrow = @"˅";

        internal const string UpArrow = @"˄";

        private static int ElemMaxHeight { get; set; }

        private static int ElemMinHeight { get; set; }
        
        public WindowMain()
        {
            this.InitializeComponent();
            
            this.CurrentFile.Visibility   = Visibility.Hidden;
            this.SuccessButton.Visibility = Visibility.Hidden;
            //StackPanelExtensions.HideFields(new object[] { this.CurrentFile, this.SuccessButton });

            HidePanel_Click(null, null);

            ElemMinHeight = (int)this.Height / 8;
            ElemMaxHeight = (int)this.Height - ElemMinHeight;

            this.RQFPath.Focus();
        }

        private void HidePanel_Click(object sender, RoutedEventArgs e)
        {
            this.DataPanel.ToggleVisibility();

            this.HidePanel.Content = (this.DataPanel.Visibility == Visibility.Visible) ? DownArrow : UpArrow;
        }
        
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog()
                                      {
                                          Title    = "Open a folder of queries for Handback",
                                          FileName = "--This Folder--",
                                          Filter   = "Query folder (*.*)|*.rqf|RQF files (*.rqf)|*.rqf",
                                          CheckFileExists = false,
                                      };
            
            if (openfile.ShowDialog() == true)
            {
                (this.DataContext as ViewModel).Folder = openfile.FileName;

                (this.DataContext as ViewModel).ValidateFiles();
                
                this.CurrentFile.Visibility   = Visibility.Visible;
                this.SuccessButton.Visibility = Visibility.Visible;
                //StackPanelExtensions.ShowFields(new object[] { this.CurrentFile, this.SuccessButton });

                // expands DataPanel
                this.HidePanel.Content = DownArrow;
                this.DataPanel.Maximise(this.ActualHeight);
            }
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
                                    ? this.ActualHeight - e.GetPosition(this).Y <= (this.ActualHeight - 20)
                                        ? this.ActualHeight - e.GetPosition(this).Y
                                        : this.ActualHeight - ElemMinHeight
                                    : ElemMinHeight;
        }

        private void RQFPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && !string.IsNullOrWhiteSpace(this.RQFPath.Text))
            {
                (this.DataContext as ViewModel).Folder = this.RQFPath.Text;

                (this.DataContext as ViewModel).ValidateFiles();
                
                this.CurrentFile.Visibility   = Visibility.Visible;
                this.SuccessButton.Visibility = Visibility.Visible;
                //StackPanelExtensions.ShowFields(new object[] { this.CurrentFile, this.SuccessButton });

                // expands DataPanel
                this.HidePanel.Content = DownArrow;
                this.DataPanel.Maximise(this.ActualHeight);
            }
        }

        private void SuccessButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog()
                                      {
                                          Title    = "Save Handback file as...",
                                          Filter   = "Handback file (*.hback)|*.hback|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                                          FileName = $"{(this.DataContext as ViewModel).Folder}.hback",
                                          AddExtension = true,
                                      };

            (this.DataContext as ViewModel).ExportResults(
                                               (savefile.ShowDialog() == true)
                                               ? savefile.FileName
                                               : null);
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
                                    ? this.ActualHeight - e.GetTouchPoint(this).Position.Y <= (this.ActualHeight - 20)
                                        ? this.ActualHeight - e.GetTouchPoint(this).Position.Y
                                        : this.ActualHeight - ElemMinHeight
                                    : ElemMinHeight;
        }
        #endregion
    }
}
