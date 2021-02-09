using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTetris.Model
{
    public class TetrisModel
    {
        public static readonly Area PlayAreaSize = new Area(25, 60);

        private readonly List<Tetrimino> _tetriminos = new List<Tetrimino>();
        private readonly Random _random = new Random();

        public TetrisModel()
        {
            EndGame();
        }

        public Tetrimino CurrentMovingTetrimino { get; private set; }
        public int NextBlockType { get; private set; }
        public int Score { get; private set; }
        public int Waves { get; private set; }
        public bool IsGameOver { get; private set; }

        public void EndGame()
        {
            IsGameOver = true;
            OnGameLost();
        }

        public void StartGame()
        {
            IsGameOver = false;

            foreach (var tetrimino in _tetriminos)
            {
                foreach (var block in tetrimino.Blocks)
                {
                    OnBlockChanged(block, true);
                }
            }
            _tetriminos.Clear();
        }

        public event EventHandler<BlockChangedEventArgs> BlockChanged;

        private void OnBlockChanged(Block block, bool disappeared)
        {
            var blockChanged = BlockChanged;
            blockChanged?.Invoke(this, new BlockChangedEventArgs(block, disappeared));
        }

        public event EventHandler GameLost;
        
        private void OnGameLost()
        {
            var gameLost = GameLost;
            gameLost?.Invoke(this, new EventArgs());
        }
    }
}
