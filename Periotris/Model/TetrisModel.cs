﻿using Periotris.Common;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Periotris.Model
{
    public class TetrisModel
    {
        /// <summary>
        /// "Frozen" or inactive blocks. They can not be moved by user.
        /// </summary>
        private readonly List<IBlock> _frozenBlocks = new List<IBlock>();

        /// <summary>
        /// Random number generator used to generate new <see cref="NextTetriminoKind"/>.
        /// </summary>
        private readonly Random _random = new Random();

        /// <summary>
        /// The active and only user-controllable <see cref="ITetrimino"/> on the field.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Operation to <see cref="_activeTetrimino"/> should be done through <see cref="MoveActiveTetrimino(MoveDirection)"/>
        /// and <see cref="RotateActiveTetrimino(RotationDirection)"/>
        /// </para>
        /// <para>
        /// This is the only <see cref="ITetrimino"/> exists. After a <see cref="ITetrimino"/> is hit,
        /// it will be "frozen" and the <see cref="ITetrimino.Blocks"/> will be transferred to
        /// <see cref="_frozenBlocks"/>.
        /// </para>
        /// </remarks>
        private ITetrimino _activeTetrimino = null;

        /// <summary>
        /// Tetriminos that are waiting to be inserted to the playing field.
        /// </summary>
        private Stack<ITetrimino> _pendingTetriminos = new Stack<ITetrimino>();

        /// <summary>
        /// Construct a new <see cref="TetrisModel"/> whose game is initially ended.
        /// </summary>
        public TetrisModel()
        {
            EndGame();
        }

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
            _pendingTetriminos.Clear();
        }

        /// <summary>
        /// Reset and start a new game.
        /// </summary>
        public void StartGame()
        {
            // Clear frozen blocks
            foreach (IBlock block in _frozenBlocks)
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
            // Fill in tetriminos
            IEnumerable<ITetrimino> generatedTetriminos = Generator.PatternGenerator.GetPatternForPeriodicTable().Reverse();
            foreach (ITetrimino tetrimino in generatedTetriminos)
            {
                _pendingTetriminos.Push(tetrimino);
            }

            // Ready to start a new game
            SpawnNextTetrimino();
            GameOver = false;
        }

        /// <summary>
        /// Move <see cref="_activeTetrimino"/>, freeze and pop out a new <see cref="ITetrimino"/> if necessary.
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
                    SpawnNextTetrimino();
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
        /// Update the game field.
        /// </summary>
        /// <remarks>
        /// This method will automatically move down <see cref="_activeTetrimino"/> once, check whether
        /// exist deletable lines and end game if necessary.
        /// </remarks>
        public void Update()
        {
            if (!GameOver)
            {
                MoveActiveTetrimino(MoveDirection.Down);
                // Or, if any frozen block's atomic number is not equal to the template's
                // block's on its same location, i.e., the placed element is not at the 
                // position it should be, then a misplaced block is found.
                // End the game.
                foreach (IBlock block in _frozenBlocks)
                {
                    if (Generator.PatternGenerator.PeriodicTableTemplate[block.Position.Y,
                        block.Position.X].AtomicNumber != block.AtomicNumber)
                    {
                        EndGame();
                    }
                }

                // All blocks settled.
                if (_frozenBlocks.Count >= Generator.PatternGenerator.TotalAvailableBlocks)
                {
                    EndGame();
                }
            }
        }

        /// <summary>
        /// Refresh all <see cref="Block"/>s in <see cref="_activeTetrimino"/> and <see cref="_frozenBlocks"/>.
        /// </summary>
        /// <remarks>
        /// Dim all blocks and then re-fire them.
        /// </remarks>
        public void UpdateAllBlocks()
        {
            UpdateActiveTetrimino(true);
            UpdateActiveTetrimino(false);
            foreach (IBlock block in _frozenBlocks)
            {
                OnBlockChanged(block, true);
                OnBlockChanged(block, false);
            }
        }

        /// <summary>
        /// Internal method which moves the <see cref="Tetrimino.Blocks"/>
        /// in <see cref="_activeTetrimino"/> to <see cref="_frozenBlocks"/>.
        /// </summary>
        private void FreezeActiveTetrimino()
        {
            foreach (IBlock block in _activeTetrimino.Blocks)
            {
                _frozenBlocks.Add(block);
            }
        }

        /// <summary>
        /// Internal method which checks whether a <see cref="Block"/> would collide
        /// with other <see cref="Block"/>s in <see cref="_frozenBlocks"/> or
        /// with the borders of the game field.
        /// </summary>
        /// <returns>Whether a block will collide or not</returns>
        private bool CheckBlockCollision(IBlock block)
        {
            // Left or right border collision
            if (block.Position.X < 0 || block.Position.X >= TetrisConst.PlayAreaWidth)
            {
                return true;
            }
            // Bottom border collision
            if (block.Position.Y >= TetrisConst.PlayAreaHeight)
            {
                return true;
            }
            // Block-block collision
            return _frozenBlocks.Any(
                (IBlock frozenBlock) =>
                {
                    return frozenBlock.Position == block.Position;
                }
            );
        }

        /// <summary>
        /// Internal method which pops a new <see cref="ITetrimino"/> from the given stack and replaces
        /// <see cref="_activeTetrimino"/> with the newly-spawned one.
        /// </summary>
        /// <remarks>
        /// Note that this method does NOT freeze current ActiveTetrimino. Please freeze
        /// it first before calling this method.
        /// </remarks>
        private void SpawnNextTetrimino()
        {
            if (_pendingTetriminos.Count > 0)
            {
                _activeTetrimino = _pendingTetriminos.Pop();
                UpdateActiveTetrimino(false);
            }
        }

        /// <summary>
        /// Internal method used to trigger <see cref="BlockChanged"/> event on
        /// every <see cref="Block"/> in <see cref="_activeTetrimino"/>.
        /// </summary>
        private void UpdateActiveTetrimino(bool disappeared)
        {
            if (_activeTetrimino != null)
            {
                foreach (IBlock block in _activeTetrimino.Blocks)
                {
                    OnBlockChanged(block, disappeared);
                }
            }
        }

        /// <summary>
        /// An event fired when a <see cref="Block"/> needs to be updated.
        /// </summary>
        public event EventHandler<BlockChangedEventArgs> BlockChanged;

        private void OnBlockChanged(IBlock block, bool disappeared)
        {
            EventHandler<BlockChangedEventArgs> blockChanged = BlockChanged;
            blockChanged?.Invoke(this, new BlockChangedEventArgs(block, disappeared));
        }
    }
}