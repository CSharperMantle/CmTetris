using SimpleTetris.Common;
using SimpleTetris.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

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

            TetrisViewModel viewModel = Resources["ViewModel"] as TetrisViewModel;
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
                targetWidth = newSize.Height * ((double)(TetrisConst.PlayAreaWidth + 1) / (double)(TetrisConst.PlayAreaHeight + 1));
                targetHeight = newSize.Height;
            }
            else
            {
                targetHeight = newSize.Width * ((double)(TetrisConst.PlayAreaHeight + 1) / (double)(TetrisConst.PlayAreaWidth + 1));
                targetWidth = newSize.Width;
            }
            //targetWidth *= ((double)TetrisConst.PlayAreaWidth + 1) / ((double)TetrisConst.PlayAreaWidth);
            //targetHeight *= ((double)TetrisConst.PlayAreaHeight + 1) / ((double)TetrisConst.PlayAreaHeight);

            PlayArea.Width = targetWidth;
            PlayArea.Height = targetHeight;
            _viewModel.PlayAreaSize = new Size(targetWidth, targetHeight);
        }
    }
}
