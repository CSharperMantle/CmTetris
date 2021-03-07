using System;
using System.Windows;

namespace Periotris.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
    }
}