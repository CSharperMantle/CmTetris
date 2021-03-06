﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Periotris.Common;
using Periotris.ViewModel;

namespace Periotris.View
{
    /// <summary>
    ///     Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private readonly TetrisViewModel _viewModel;

        public GamePage()
        {
            InitializeComponent();

            if (Resources["ViewModel"] is TetrisViewModel viewModel)
                _viewModel = viewModel;
            else
                throw new Exception(nameof(viewModel));
        }

        private void GamePage_KeyDown(object sender, KeyEventArgs e)
        {
            _viewModel.OnKeyDown(e.Key);
            e.Handled = true;
        }

        private void GamePage_SizeChanged(object sender, SizeChangedEventArgs e)
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
                targetWidth = newSize.Height * (TetrisConst.PlayAreaWidth / (double) TetrisConst.PlayAreaHeight);
                targetHeight = newSize.Height;
            }
            else
            {
                targetHeight = newSize.Width * (TetrisConst.PlayAreaHeight / (double) TetrisConst.PlayAreaWidth);
                targetWidth = newSize.Width;
            }

            PlayArea.Width = targetWidth;
            PlayArea.Height = targetHeight;
            _viewModel.PlayAreaSize = new Size(targetWidth, targetHeight);
        }

        private void GamePage_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.KeyDown += GamePage_KeyDown;
        }

        private void GamePage_Unloaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.KeyDown -= GamePage_KeyDown;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateTo(PageType.StartPage);
        }
    }
}