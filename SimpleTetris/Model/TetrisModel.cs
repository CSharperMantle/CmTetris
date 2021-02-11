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

        /// <summary>
        /// "Frozen" or inactive blocks. They can not be moved by user.
        /// </summary>
        private readonly List<Block> _frozenBlocks = new List<Block>();

        /// <summary>
        /// Random number generator used to generate new <see cref="NextTetriminoKind"/>.
        /// </summary>
        private readonly Random _random = new Random();

        /// <summary>
        /// The active and only user-controllable <see cref="Tetrimino"/> on the field.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Operation to <see cref="_activeTetrimino"/> should be done through <see cref="MoveActiveTetrimino(MoveDirection)"/>
        /// and <see cref="RotateActiveTetrimino(RotationDirection)"/>
        /// </para>
        /// <para>
        /// This is the only <see cref="Tetrimino"/> exists. After a <see cref="Tetrimino"/> is hit,
        /// it will be "frozen" and the <see cref="Tetrimino.Blocks"/> will be transferred to
        /// <see cref="_frozenBlocks"/>.
        /// </para>
        /// </remarks>
        private Tetrimino _activeTetrimino = null;

        /// <summary>
        /// Construct a new <see cref="TetrisModel"/> whose game is initially ended.
        /// </summary>
        public TetrisModel()
        {
            EndGame();
        }

        /// <summary>
        /// Scheduled <see cref="TetriminoKind"/> of the next <see cref="Tetrimino"/>.
        /// </summary>
        /// <remarks>
        /// Null when the game has not started.
        /// </remarks>
        public TetriminoKind? NextTetriminoKind { get; private set; }

        /// <summary>
        /// Number of rows cleared.
        /// </summary>
        public int RowsCleared { get; private set; }

        /// <summary>
        /// Whether the game is in progress.
        /// </summary>
        public bool GameOver { get; private set; }

        /// <summary>
        /// End the current game.
        /// </summary>
        public void EndGame()
        {
            GameOver = true;
            NextTetriminoKind = null;
            OnGameLost();
        }

        /// <summary>
        /// Reset and start a new game.
        /// </summary>
        public void StartGame()
        {
            // Clear frozen blocks
            foreach (var block in _frozenBlocks)
            {
                OnBlockChanged(block, true);
            }
            _frozenBlocks.Clear();

            // Clear active tetrimino (if we have to do so)
            if (_activeTetrimino != null)
            {
                UpdateActiveTetrimino(true);
                _activeTetrimino = null;
            }

            // Ready to start a new game
            RowsCleared = 0;
            SpawnRandomTetrimino();
            GameOver = false;
        }

        /// <summary>
        /// Move <see cref="_activeTetrimino"/>, freeze and spawn a new <see cref="Tetrimino"/> if necessary.
        /// </summary>
        /// <param name="direction">The direction to move to</param>
        public void MoveActiveTetrimino(MoveDirection direction)
        {
            if (GameOver)
            {
                return;
            }

            // First, notify that the old Blocks are removed...
            UpdateActiveTetrimino(true);

            if (direction == MoveDirection.Down)
            {
                // When we need to move down, we will have to freeze the blocks if the
                // Tetrimino hit the ground or other blocks.
                if (!_activeTetrimino.TryMove(direction, CheckBlockCollision))
                {
                    // Move them to the frozen block list and replace it.
                    FreezeActiveTetrimino();
                    // Re-add them and spawn a new Tetrimino.
                    UpdateActiveTetrimino(false);
                    SpawnRandomTetrimino();
                }
                // Go normally.
            }
            else
            {
                // Move sideways.
                _activeTetrimino.TryMove(direction, CheckBlockCollision);
            }
            // Re-add moved blocks.
            UpdateActiveTetrimino(false);
        }

        /// <summary>
        /// Rotate <see cref="_activeTetrimino"/>.
        /// </summary>
        /// <param name="direction">The direction to rotate to</param>
        public void RotateActiveTetrimino(RotationDirection direction)
        {
            if (GameOver)
            {
                return;
            }

            _activeTetrimino.TryRotate(direction, CheckBlockCollision);
        }

        /// <summary>
        /// Update the game field.
        /// </summary>
        /// <remarks>
        /// This method will automatically move down <see cref="_activeTetrimino"/> once, check whether
        /// exist deletable lines and end game if necessary.
        /// </remarks>
        public void Update()
        {
            MoveActiveTetrimino(MoveDirection.Down);
            RemoveRowsFromFrozenBlocks();
            // If frozen blocks touches the upper border, stop the game.
            if (_frozenBlocks.Any(block => block.Position.Y < 0))
            {
                EndGame();
            }
        }

        /// <summary>
        /// Remove rows from <see cref="_frozenBlocks"/> and increase <see cref="RowsCleared"/>.
        /// </summary>
        private void RemoveRowsFromFrozenBlocks()
        {
            var frozenBlocksCopy = _frozenBlocks.ToArray();
            var rowsCleared = 0;
            var rowsClearedYVal = new List<int>();
            // Group blocks by row number (aka. Block.Position.Y), bottom-up
            var rows = from block in frozenBlocksCopy
                       group block by block.Position.Y into row
                       orderby row.Key descending
                       select row;
            foreach (var row in rows)
            {
                // If elements in one row is more than Width, then it is well filled.
                if (row.Count() >= PlayAreaSize.Width)
                {
                    // Remove the entire line.
                    rowsCleared++;
                    rowsClearedYVal.Add(row.Key);
                    foreach (var block in row)
                    {
                        _frozenBlocks.Remove(block);
                        OnBlockChanged(block, true);
                    }
                }
            }
            /* As we are using bottom-up method, we can iterate over the list of Y values, moving down blocks.
             * - - - -
             * + + + +
             * + + + +
             * - - - -   <-- Line deleted Y=3
             * + + + +
             * - - - -   <-- Line deleted Y=5
             * ITERATION 1:
             * - - - -
             * - - - -
             * + + + +
             * + + + +
             * - - - -
             * + + + +   <-- Empty space zapped and moved down
             * ITERATION 2:
             * - - - -
             * - - - -
             * - - - -
             * + + + +
             * + + + +   <-- Empty space zapped and moved down
             * + + + +
             */
            while (rowsClearedYVal.Count > 0)
            {
                // Get a fresh copy of all frozen blocks
                frozenBlocksCopy = _frozenBlocks.ToArray();
                // Move out first Y value as we are going to move down all rows above this
                var currentClearedRowYVal = rowsClearedYVal[0];
                rowsClearedYVal.RemoveAt(0);

                // Increase all remaining Y values by 1 in order to match the Y values after moving down
                for (var idx = 0; idx < rowsClearedYVal.Count; idx++)
                {
                    rowsClearedYVal[idx]++;
                }

                var blocksFalling = from block in frozenBlocksCopy
                                    where block.Position.Y < currentClearedRowYVal
                                    select block;
                foreach (var block in blocksFalling)
                {
                    OnBlockChanged(block, true);
                    _frozenBlocks.Remove(block);
                    var newBlock = new Model.Block(block.FilledBy, new Position(block.Position.X, block.Position.Y + 1));
                    _frozenBlocks.Add(newBlock);
                    OnBlockChanged(newBlock, false);
                }
            }

            RowsCleared += rowsCleared;
        }

        /// <summary>
        /// Internal method which moves the <see cref="Tetrimino.Blocks"/>
        /// in <see cref="TetrisModel._activeTetrimino"/> to <see cref="TetrisModel._frozenBlocks"/>.
        /// </summary>
        private void FreezeActiveTetrimino()
        {
            foreach (var block in _activeTetrimino.Blocks)
            {
                _frozenBlocks.Add(block);
            }
        }

        /// <summary>
        /// Internal method which checks whether a <see cref="Block"/> would collide
        /// with other <see cref="Block"/>s in <see cref="TetrisModel._frozenBlocks"/> or
        /// with the borders of the game field.
        /// </summary>
        /// <returns>Whether a block will collide or not</returns>
        private bool CheckBlockCollision(Block block)
        {
            // Left or right border collision
            if (block.Position.X < 0 || block.Position.X >= PlayAreaSize.Width)
            {
                return true;
            }
            // Bottom border collision
            if (block.Position.Y >= PlayAreaSize.Height)
            {
                return true;
            }
            // Block-block collision
            return _frozenBlocks.Any(
                (Block frozenBlock) =>
                {
                    return frozenBlock.Position == block.Position;
                }
            );
        }

        /// <summary>
        /// Internal method which spawns a new random <see cref="Tetrimino"/> and replaces
        /// <see cref="TetrisModel._activeTetrimino"/> with the newly-spawned one.
        /// </summary>
        /// <remarks>
        /// Note that this method does NOT freeze current ActiveTetrimino. Please freeze
        /// it first before calling this method.
        /// </remarks>
        private void SpawnRandomTetrimino()
        {
            if (NextTetriminoKind.HasValue)
            {
                _activeTetrimino = new Tetrimino(NextTetriminoKind.Value);
            }
            else
            {

                _activeTetrimino = new Tetrimino(TetriminoKindHelper.GetRandomTetriminoKind(_random));
            }
            NextTetriminoKind = TetriminoKindHelper.GetRandomTetriminoKind(_random);
            // Pushing newly-generated blocks to upper receiver
            UpdateActiveTetrimino(false);
        }

        /// <summary>
        /// Internal method used to trigger <see cref="TetrisModel.BlockChanged"/> event on
        /// every <see cref="Block"/> in <see cref="TetrisModel._activeTetrimino"/>.
        /// </summary>
        private void UpdateActiveTetrimino(bool disappeared)
        {
            foreach (var block in _activeTetrimino.Blocks)
            {
                OnBlockChanged(block, disappeared);
            }
        }

        /// <summary>
        /// An event fired when a <see cref="Block"/> needs to be updated.
        /// </summary>
        public event EventHandler<BlockChangedEventArgs> BlockChanged;

        private void OnBlockChanged(Block block, bool disappeared)
        {
            var blockChanged = BlockChanged;
            blockChanged?.Invoke(this, new BlockChangedEventArgs(block, disappeared));
        }

        /// <summary>
        /// An event fired when the game loses.
        /// </summary>
        public event EventHandler GameLost;
        
        private void OnGameLost()
        {
            var gameLost = GameLost;
            gameLost?.Invoke(this, new EventArgs());
        }
    }
}
