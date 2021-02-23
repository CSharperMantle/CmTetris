using Periotris.Common;
using Periotris.Model;
using Periotris.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Periotris.ViewModel
{
    public class TetrisViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Scaling factor for proper positioning.
        /// </summary>
        /// <remarks>
        /// TODO.
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

        private readonly TetrisModel _model = new TetrisModel();

        private readonly Dictionary<Position, FrameworkElement> _blocksByPosition =
            new Dictionary<Position, FrameworkElement>();

        private readonly List<FrameworkElement> _assistGridLines =
            new List<FrameworkElement>();

        private readonly ObservableCollection<FrameworkElement> _sprites =
            new ObservableCollection<FrameworkElement>();

        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public TetrisViewModel()
        {
            Scale = 1;

            _model.BlockChanged += ModelBlockChangedEventHandler;
            _model.GameEnd += ModelGameEndEventListener;

            _timer.Interval = TimeSpan.FromSeconds(TetrisConst.UpdateIntervalSeconds);
            _timer.Tick += TimerTickEventHandler;

            EndGame();
        }

        public INotifyCollectionChanged Sprites => _sprites;

        public bool GameOver => _model.GameEnded && !_model.Victory;

        public bool GameWon => _model.GameEnded && _model.Victory;

        public TimeSpan ElapsedTime => _model.ElapsedTime;

        public TimeSpan? CurrentHighestScore => _model.CurrentHighestScore;

        public bool Paused { get; set; }

        public bool RenderColors { get; set; } = true;

        public bool RenderGridAssistance { get; set; } = false;

        private bool _lastPaused = true;

        /// <summary>
        /// Start the underlying game in <see cref="TetrisModel"/>.
        /// </summary>
        public void StartGame()
        {
            RecreateAssistGrids();
            foreach (FrameworkElement block in _blocksByPosition.Values)
            {
                _sprites.Remove(block);
            }
            _model.StartGame();
            OnPropertyChanged(nameof(GameOver));
            OnPropertyChanged(nameof(GameWon));
            Paused = false;
            _timer.Start();
        }

        public void OnKeyDown(Key key)
        {
            if (Paused)
            {
                if (key == Key.Escape)
                {
                    Paused = !Paused;
                }
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
                case Key.Left:
                    _model.MoveActiveTetrimino(MoveDirection.Left);
                    break;
                case Key.Down:
                    _model.MoveActiveTetrimino(MoveDirection.Down);
                    break;
                case Key.Right:
                    _model.MoveActiveTetrimino(MoveDirection.Right);
                    break;
                case Key.Space:
                    _model.RotateActiveTetrimino(RotationDirection.Right);
                    break;
                case Key.Escape:
                    Paused = !Paused;
                    break;
                default:
                    break;
            }
        }

        private void EndGame()
        {
            _timer.Stop();
            OnPropertyChanged(nameof(GameOver));
            OnPropertyChanged(nameof(GameWon));
            OnPropertyChanged(nameof(CurrentHighestScore));
        }

        private void RecreateAssistGrids()
        {
            foreach (FrameworkElement line in _assistGridLines)
                if (_sprites.Contains(line))
                    _sprites.Remove(line);
            _assistGridLines.Clear();
            if (!RenderGridAssistance)
            {
                return;
            }
            for (int x = 0; x < TetrisConst.PlayAreaWidth; x++)
            {
                var scanLine = TetrisControlHelper.VerticalAssistGridLineFactory(
                    x, TetrisConst.PlayAreaHeight, Scale);
                _assistGridLines.Add(scanLine);
                _sprites.Add(scanLine);
            }
            for (int y = 0; y < TetrisConst.PlayAreaHeight; y++)
            {
                var scanLine = TetrisControlHelper.HorizontalAssistGridLineFactory(
                    y, TetrisConst.PlayAreaWidth, Scale);
                _assistGridLines.Add(scanLine);
                _sprites.Add(scanLine);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ModelBlockChangedEventHandler(object sender, BlockChangedEventArgs e)
        {
            if (!e.Disappeared)
            {
                if (!_blocksByPosition.Keys.Contains(e.BlockUpdated.Position))
                {
                    // Create a new BlockControl.
                    FrameworkElement blockControl = TetrisControlHelper.AnnotatedBlockControlFactory(e.BlockUpdated, RenderColors, Scale);
                    _blocksByPosition.Add(e.BlockUpdated.Position, blockControl);
                    _sprites.Add(blockControl);
                }
                else
                {
                    // In this case we come across a overdrawn block, that is, has been called 'appeared' twice.
                    // Should this scenario happen, we simply ignore this.
                    // TODO: Check if this scenario is reasonable.
                    return;
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

        private void TimerTickEventHandler(object sender, EventArgs e)
        {
            if (_lastPaused != Paused)
            {
                OnPropertyChanged(nameof(Paused));
                _lastPaused = Paused;
            }

            if (!Paused)
            {
                _model.Update();
                OnPropertyChanged(nameof(ElapsedTime));
            }
        }
    }
}
