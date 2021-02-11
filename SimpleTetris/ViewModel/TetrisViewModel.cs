using SimpleTetris.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SimpleTetris.ViewModel
{
    public class TetrisViewModel : INotifyPropertyChanged
    {
        private readonly TetrisModel _model = new TetrisModel();

        private readonly Dictionary<Block, FrameworkElement> _blocks =
            new Dictionary<Block, FrameworkElement>();

        private readonly ObservableCollection<FrameworkElement> _sprites =
            new ObservableCollection<FrameworkElement>();

        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public TetrisViewModel()
        {
            Scale = 1;

            _model.BlockChanged += ModelBlockChangedEventHandler;
            _model.GameLost += ModelGameLostEventHandler;

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimerTickEventHandler;
        }

        public INotifyCollectionChanged Sprites => _sprites;

        public bool GameOver => _model.GameOver;

        public bool Paused { get; set; }

        public static double Scale { get; private set; }

        public int RowsCleared { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string propertyName = null)
        {
            var propertyChanged = PropertyChanged;
            propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ModelGameLostEventHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ModelBlockChangedEventHandler(object sender, BlockChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TimerTickEventHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
