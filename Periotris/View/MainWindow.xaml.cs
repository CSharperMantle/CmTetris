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
        private readonly TetrisViewModel _viewModel = null;

        public MainWindow()
        {
            InitializeComponent();

            if (Resources["ViewModel"] is TetrisViewModel viewModel)
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
            UpdatePlayAreaSize(new Size(e.NewSize.Width, e.NewSize.Height - 160));
        }

        private void PlayArea_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePlayAreaSize(PlayArea.RenderSize);
        }

        private void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StartGame();
        }

        private void UpdatePlayAreaSize(Size newSize)
        {
            double targetWidth;
            double targetHeight;
            if (newSize.Width > newSize.Height)
            {
                targetWidth = newSize.Height * (TetrisConst.PlayAreaWidth / (double)TetrisConst.PlayAreaHeight);
                targetHeight = newSize.Height;
            }
            else
            {
                targetHeight = newSize.Width * (TetrisConst.PlayAreaHeight / (double)TetrisConst.PlayAreaWidth);
                targetWidth = newSize.Width;
            }

            PlayArea.Width = targetWidth;
            PlayArea.Height = targetHeight;
            _viewModel.PlayAreaSize = new Size(targetWidth, targetHeight);
        }
    }
}
