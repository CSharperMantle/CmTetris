using Periotris.Common;
using Periotris.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace Periotris.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Navigate("View/GamePage.xaml");
        }

        public void Navigate(string relativeUri)
        {
            MainFrame.Navigate(new Uri(relativeUri, UriKind.Relative));
        }
    }
}
