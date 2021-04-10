﻿using System;
using System.Diagnostics;
using System.Windows;
using MahApps.Metro.Controls;

namespace Periotris.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            NavigationHelper.NavigateTo(PageType.StartPage);
        }

        public void NavigateTo(string relativeUri)
        {
            MainFrame.Navigate(new Uri(relativeUri, UriKind.Relative));
        }

        private void LaunchGitHubRepo(object sender, RoutedEventArgs eventArgs)
        {
            Process.Start("https://github.com/CSharperMantle/CmTetris");
        }
    }
}