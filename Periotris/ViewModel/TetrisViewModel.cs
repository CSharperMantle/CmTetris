﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Periotris.Common;
using Periotris.Model;
using Periotris.View;

namespace Periotris.ViewModel
{
    public class TetrisViewModel : INotifyPropertyChanged
    {
        private readonly List<FrameworkElement> _assistGridLines =
            new List<FrameworkElement>();

        private readonly Dictionary<Position, FrameworkElement> _blocksByPosition =
            new Dictionary<Position, FrameworkElement>();

        private readonly DispatcherTimer _gameTimer = new DispatcherTimer();

        private readonly TetrisModel _model = new TetrisModel();

        private readonly ObservableCollection<FrameworkElement> _sprites =
            new ObservableCollection<FrameworkElement>();

        private readonly DispatcherTimer _timeDisplayRefreshTimer = new DispatcherTimer();

        private bool _lastPaused = true;

        public TetrisViewModel()
        {
            Scale = 1;

            _model.BlockChanged += ModelBlockChangedEventHandler;
            _model.GameEnd += ModelGameEndEventListener;

            _gameTimer.Interval = TimeSpan.FromSeconds(TetrisConst.GameUpdateIntervalSeconds);
            _gameTimer.Tick += GameUpdateTimerTickEventHandler;

            _timeDisplayRefreshTimer.Interval = TimeSpan.FromSeconds(TetrisConst.TimeDisplayUpdateIntervalSeconds);
            _timeDisplayRefreshTimer.Tick += TimeDisplayTimerTickEventHandler;

            EndGame();
        }

        /// <summary>
        ///     Scaling factor for proper positioning.
        /// </summary>
        /// <remarks>
        ///     TODO.
        /// </remarks>
        public static double Scale { get; private set; }

        public Size PlayAreaSize
        {
            set
            {
                Scale = value.Width / TetrisConst.PlayAreaWidth;
                _model.UpdateAllBlocks();
                RecreateAssistGrids();
            }
        }

        public INotifyCollectionChanged Sprites => _sprites;

        public bool GameOver => _model.GameEnded && !_model.Victory;

        public bool GameWon => _model.GameEnded && _model.Victory;

        public TimeSpan ElapsedTime => _model.ElapsedTime;

        public TimeSpan? CurrentHighestScore => _model.CurrentHighestScore;

        public bool Paused { get; set; }

        public bool RenderColors { get; set; } = true;

        public bool RenderGridAssistance { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Start the underlying game in <see cref="TetrisModel" />.
        /// </summary>
        public void StartGame()
        {
            RecreateAssistGrids();
            foreach (var block in _blocksByPosition.Values) _sprites.Remove(block);
            _model.StartGame();
            OnPropertyChanged(nameof(GameOver));
            OnPropertyChanged(nameof(GameWon));
            Paused = false;
            _gameTimer.Start();
            _timeDisplayRefreshTimer.Start();
        }

        public void OnKeyDown(Key key)
        {
            if (Paused)
            {
                if (key == Key.Escape) Paused = !Paused;
                return;
            }

            switch (key)
            {
                case Key.A:
                    _model.MoveActiveTetrimino(MoveDirection.Left);
                    break;
                case Key.S:
                    _model.MoveActiveTetrimino(MoveDirection.Down);
                    break;
                case Key.D:
                    _model.MoveActiveTetrimino(MoveDirection.Right);
                    break;
                case Key.W:
                    _model.RotateActiveTetrimino(RotationDirection.Right);
                    break;
                case Key.Left:
                    _model.MoveActiveTetrimino(MoveDirection.Left);
                    break;
                case Key.Down:
                    _model.MoveActiveTetrimino(MoveDirection.Down);
                    break;
                case Key.Right:
                    _model.MoveActiveTetrimino(MoveDirection.Right);
                    break;
                case Key.Up:
                    _model.RotateActiveTetrimino(RotationDirection.Right);
                    break;
                case Key.Space:
                    _model.InstantDropActiveTetrimino();
                    break;
                case Key.Escape:
                    Paused = !Paused;
                    break;
            }
        }

        private void EndGame()
        {
            _gameTimer.Stop();
            _timeDisplayRefreshTimer.Stop();
            OnPropertyChanged(nameof(GameOver));
            OnPropertyChanged(nameof(GameWon));
            OnPropertyChanged(nameof(CurrentHighestScore));
        }

        private void RecreateAssistGrids()
        {
            foreach (var line in _assistGridLines)
                if (_sprites.Contains(line))
                    _sprites.Remove(line);
            _assistGridLines.Clear();
            if (!RenderGridAssistance) return;
            for (var x = 0; x < TetrisConst.PlayAreaWidth; x++)
            {
                var scanLine = TetrisControlHelper.VerticalAssistGridLineFactory(
                    x, TetrisConst.PlayAreaHeight, Scale);
                _assistGridLines.Add(scanLine);
                _sprites.Add(scanLine);
            }

            for (var y = 0; y < TetrisConst.PlayAreaHeight; y++)
            {
                var scanLine = TetrisControlHelper.HorizontalAssistGridLineFactory(
                    y, TetrisConst.PlayAreaWidth, Scale);
                _assistGridLines.Add(scanLine);
                _sprites.Add(scanLine);
            }
        }

        private void OnPropertyChanged(string propertyName = null)
        {
            var propertyChanged = PropertyChanged;
            propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ModelBlockChangedEventHandler(object sender, BlockChangedEventArgs e)
        {
            if (!e.Disappeared)
            {
                if (!_blocksByPosition.Keys.Contains(e.BlockUpdated.Position))
                {
                    // Create a new BlockControl.
                    var blockControl =
                        TetrisControlHelper.AnnotatedBlockControlFactory(e.BlockUpdated, RenderColors, Scale);
                    _blocksByPosition.Add(e.BlockUpdated.Position, blockControl);
                    _sprites.Add(blockControl);
                }
            }
            else
            {
                if (_blocksByPosition.Keys.Contains(e.BlockUpdated.Position))
                {
                    _sprites.Remove(_blocksByPosition[e.BlockUpdated.Position]);
                    _blocksByPosition.Remove(e.BlockUpdated.Position);
                }
            }
        }

        private void ModelGameEndEventListener(object sender, EventArgs e)
        {
            EndGame();
        }

        private void GameUpdateTimerTickEventHandler(object sender, EventArgs e)
        {
            if (_lastPaused != Paused)
            {
                OnPropertyChanged(nameof(Paused));
                _lastPaused = Paused;
            }

            if (!Paused) _model.Update();
            // Update interval
            _gameTimer.Interval = TimeSpan.FromSeconds(TetrisConst.GameUpdateIntervalSeconds);
        }

        private void TimeDisplayTimerTickEventHandler(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(ElapsedTime));
        }
    }
}