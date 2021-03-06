using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using SimpleTetris.Common;
using SimpleTetris.Model;
using SimpleTetris.View;

namespace SimpleTetris.ViewModel
{
    public class TetrisViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<Position, FrameworkElement> _blocksByPosition =
            new Dictionary<Position, FrameworkElement>();

        private readonly TetrisModel _model = new TetrisModel();

        private readonly ObservableCollection<FrameworkElement> _sprites =
            new ObservableCollection<FrameworkElement>();

        private readonly DispatcherTimer _timer = new DispatcherTimer();

        private bool _lastPaused = true;

        public TetrisViewModel()
        {
            Scale = 1;

            _model.BlockChanged += ModelBlockChangedEventHandler;
            _model.NextTetriminoKindChanged += ModelNextTetriminoKindChanged;

            _timer.Interval = TimeSpan.FromSeconds(TetrisConst.UpdateIntervalSeconds);
            _timer.Tick += TimerTickEventHandler;

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
                Scale = value.Width / (TetrisConst.PlayAreaWidth + 1);
                _model.UpdateAllBlocks();
            }
        }

        public INotifyCollectionChanged Sprites => _sprites;

        public bool GameOver => _model.GameOver;

        public bool Paused { get; set; }

        public int RowsCleared { get; private set; }

        public TetriminoKind? NextTetriminoKind => _model.NextTetriminoKind;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Start the underlying game in <see cref="TetrisModel" />.
        /// </summary>
        public void StartGame()
        {
            Paused = false;
            foreach (var block in _blocksByPosition.Values) _sprites.Remove(block);
            _model.StartGame();
            OnPropertyChanged(nameof(GameOver));
            _timer.Start();
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
                case Key.Space:
                    _model.RotateActiveTetrimino(RotationDirection.Right);
                    break;
                case Key.Escape:
                    Paused = !Paused;
                    break;
            }
        }

        private void EndGame()
        {
            _model.EndGame();
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
                    var blockControl = TetrisControlHelper.BlockControlFactory(e.BlockUpdated, Scale);
                    _blocksByPosition.Add(e.BlockUpdated.Position, blockControl);
                    _sprites.Add(blockControl);
                }
                else
                {
                    // In this case we come across a overdrawn block, that is, has been called 'appeared' twice.
                    // Should this scenario happen, we simply ignore this.
                    // TODO: Check if this scenario is reasonable.
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

        private void TimerTickEventHandler(object sender, EventArgs e)
        {
            if (_lastPaused != Paused)
            {
                OnPropertyChanged(nameof(Paused));
                _lastPaused = Paused;
            }

            if (!Paused) _model.Update();

            if (RowsCleared != _model.RowsCleared)
            {
                RowsCleared = _model.RowsCleared;
                OnPropertyChanged(nameof(RowsCleared));
            }

            if (_model.GameOver)
            {
                OnPropertyChanged(nameof(GameOver));
                _timer.Stop();
            }
        }

        private void ModelNextTetriminoKindChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(NextTetriminoKind));
        }
    }
}