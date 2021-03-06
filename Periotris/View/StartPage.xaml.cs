using System.Windows;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow)
                .NavigateTo("View/GamePage.xaml");
        }
    }
}