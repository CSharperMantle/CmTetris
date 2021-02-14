using SimpleTetris.Model;
using SimpleTetris.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleTetris.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TetrisViewModel _viewModel = null;

        public MainWindow()
        {
            InitializeComponent();

            var viewModel = Resources["ViewModel"] as TetrisViewModel;
            if (viewModel != null)
            {
                _viewModel = viewModel;
            }
            else
            {
                throw new Exception(nameof(viewModel));
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            _viewModel.OnKeyDown(e.Key);
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void PlayArea_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StartGame();
        }

        private void UpdatePlayAreaSize(Size newSize)
        {

        }
    }
}
