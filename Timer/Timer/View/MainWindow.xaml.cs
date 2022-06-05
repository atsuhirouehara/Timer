﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Timer.ViewModel;

namespace Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            Uri uri = new Uri("TopPage.xaml", UriKind.Relative);
            frame.Source = uri;
                      
        }
    }
}
