﻿using System.Windows;
using System.Windows.Controls;

namespace Periotris.View
{
    /// <summary>
    ///     Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void NormalGameButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateTo(PageType.GamePage);
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
                mainWindow.SwitchAboutFlyout();
        }
    }
}