namespace Defender.View.Reader.WPF
{
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

    /// <summary>
    /// Interaction logic for View.Reader.Window.Main.xaml
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

            this.CurrentFile.Visibility = Visibility.Hidden;
            this.SuccessButton.Visibility = Visibility.Hidden;
            //PanelExtensions.HideFields(new object[] { this.CurrentFile, this.SuccessButton });

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
                                          Title = "Select the Handback file to verify",
                                          Filter = "Handback file (*.hback)|*.hback|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                                      };

            if (openfile.ShowDialog() == true)
            {
                (this.DataContext as ViewModel).ImportResults(openfile.FileName);

                this.CurrentFile.Visibility = Visibility.Visible;
                this.SuccessButton.Visibility = Visibility.Visible;
                //PanelExtensions.ShowFields(new object[] { this.CurrentFile, this.SuccessButton });

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
                (this.DataContext as ViewModel).ImportResults(this.RQFPath.Text);

                this.CurrentFile.Visibility = Visibility.Visible;
                this.SuccessButton.Visibility = Visibility.Visible;

                // expands DataPanel
                this.HidePanel.Content = DownArrow;
                this.DataPanel.Maximise(this.ActualHeight);
            }
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
