﻿using System;
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

namespace Defender.View.Reader.WPF
{
    /// <summary>
    /// Interaction logic for View.Reader.Window.Main.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        private static int ElemMaxHeight { get; set; }
        private static int ElemMinHeight { get; set; }

        public WindowMain()
        {
            this.InitializeComponent();

            this.RQFPath.Focus();

            this.CurrentFile.Visibility = Visibility.Hidden;
            this.SuccessButton.Visibility = Visibility.Hidden;

            HidePanel_Click(null, null);

            ElemMinHeight = (int)this.Height / 6;
            ElemMaxHeight = (int)this.Height - (ElemMinHeight / 2);
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
                                          Title  = "Select the Handback file to verify",
                                          Filter = "Handback file (*.hback)|*.hback|Text file (*.txt)|*.txt|All files (*.*)|*.*",
                                      };

            (this.DataContext as ViewModel.ViewModel).ImportResults((openfile.ShowDialog() == true) ? openfile.FileName : null);
            
            this.CurrentFile.Visibility = Visibility.Visible;
            this.SuccessButton.Visibility = Visibility.Visible;

            this.DataPanel.Visibility = Visibility.Visible;
            this.HidePanel.Content = @"˅";
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
                                    ? this.ActualHeight - e.GetPosition(this).Y <= this.ActualHeight
                                        ? this.ActualHeight - e.GetPosition(this).Y
                                        : this.ActualHeight - ElemMinHeight
                                    : ElemMinHeight;
        }

        private void RQFPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && !string.IsNullOrWhiteSpace(this.RQFPath.Text))
            {
                (this.DataContext as ViewModel.ViewModel).ImportResults(this.RQFPath.Text);

                this.CurrentFile.Visibility = Visibility.Visible;
                this.SuccessButton.Visibility = Visibility.Visible;

                this.DataPanel.Visibility = Visibility.Visible;
                this.HidePanel.Content = @"˅";
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
                                    ? this.ActualHeight - e.GetTouchPoint(this).Position.Y <= this.ActualHeight
                                        ? this.ActualHeight - e.GetTouchPoint(this).Position.Y
                                        : this.ActualHeight - ElemMinHeight
                                    : ElemMinHeight;
        }
        #endregion
    }
}
