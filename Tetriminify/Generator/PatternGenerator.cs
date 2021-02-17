using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetriminify.Generator
{
    public static class PatternGenerator
    {
        private class TetriminoKindDirectionsPair
        {
            public TetriminoKind TetriminoKind { get; private set; }

            public Stack<Direction> PendingDirections { get; private set; }

            public TetriminoKindDirectionsPair(TetriminoKind kind, Random rand)
            {
                TetriminoKind = kind;
                PendingDirections = new Stack<Direction>(new Direction[] {
                        Direction.Left,
                        Direction.Right,
                        Direction.Up,
                        Direction.Down,
                    }.ToList().OrderBy(x => rand.Next()));
            }
        }

        /// <summary>
        /// Generate a <see cref="IReadOnlyList{T}"/> of <see cref="Tetrimino"/>s that fills the given template.
        /// </summary>
        /// <param name="template">
        /// <para>
        /// A two-dimensional <see cref="Array"/> of <see cref="Block"/>s.
        /// </para>
        /// <para>
        /// This array should only contain blocks with <see cref="TetriminoKind.AvailableToFill"/> or <see cref="TetriminoKind.UnavailableToFill"/>.
        /// The former one indicates that this block is fillable with <see cref="Tetrimino"/> while the latter one has the opposite meaning.
        /// </para>
        /// </param>
        /// <returns>
        /// When this method returns, contains a <see cref="IReadOnlyList{T}"/> of <see cref="Tetrimino"/>s of settled (placed) tetriminos, or
        /// an empty one when fails to generate.
        /// </returns>
        public static IReadOnlyList<Tetrimino> GetPattern(Block[,] template)
        {
            // This is where we do our traCking jobs - to track which blocks are available to fill
            Block[,] workspace = (Block[,])template.Clone();
            // This is where we place our settled tetriminos - we use Stack because we may need to go back a few steps if a plan fails
            Stack<Tetrimino> settledTetrimino = new Stack<Tetrimino>();
            // This is where we place our information to generate randomized tetriminos
            Stack<Stack<TetriminoKindDirectionsPair>> pendingTetriminoKinds = new Stack<Stack<TetriminoKindDirectionsPair>>();
            Random rand = new Random();

            // These are cursors. Before each iteration
            Stack<TetriminoKindDirectionsPair> currentTetriminoKindDirectionsPairStack = null;
            bool rewindingRequired = false;

            /* Here in the main loop there are many things we need to do:
             * 0. All cursors are set to null when beginNing this loop.
             * 
             * 1. Check if there are any more unplaced blocks, if so, we will record its coordinates.
             *    Otherwise we will just return the placed Stack as there are no more Tetriminos to 
             *    place.
             *    
             * 2. Generate a randomly-ordered list of TetriminoKind and iterate all over them. Check
             *    if there are any possible placeable Tetrimino configurations and push it to the stack.
             *    The cursors are set to null for a new iteration from Step 1. Do not forget to set its Blocks
             *    to its suitable FilledBy state in the workspace.
             *    
             * 3. If there are no possible placeable Tetriminos then we need to 'rewind' the stack.
             *    We pop back one Tetrimino and its corresponding stack of TetriminoKind, choose the
             *    next one and then go back to Step 2. Do not forget to erase any settled block state of
             *    the popped Tetrimino in the workspace.
             */
            while (true)
            {
                // Step 1.
                // First we need to find the position of the first block in the workspace
                int firstBlockRow = 0;
                int firstBlockCol = 0;
                (firstBlockCol, firstBlockRow) = GetFirstAvailableBlockCoordination(workspace);
                if (!(firstBlockCol >= 0 && firstBlockRow >= 0))
                {
                    // There are no more blocks to fill. Returning.
                    return settledTetrimino.ToArray();
                }

                // Step 2.
                // Now we find an empty block. Generate a new Tetrimino using randomized stack of TetriminoKind.
                if (!rewindingRequired)
                {
                    // This could mean this is the first run or the last iteration has succeeded in placing a block.
                    currentTetriminoKindDirectionsPairStack = new Stack<TetriminoKindDirectionsPair>();
                    var randomizedTetriminoKinds = new TetriminoKind[] {
                        TetriminoKind.Cubic,
                        TetriminoKind.Linear,
                        TetriminoKind.LShapedCis,
                        TetriminoKind.LShapedTrans,
                        TetriminoKind.TShaped,
                        TetriminoKind.ZigZagCis,
                        TetriminoKind.ZigZagTrans
                    }.ToList().OrderBy(x => rand.Next());
                    foreach (TetriminoKind kind in randomizedTetriminoKinds)
                    {
                        currentTetriminoKindDirectionsPairStack.Push(new TetriminoKindDirectionsPair(kind, rand));
                    }
                }
                else
                {
                    // In this case we need to rewind the stack for one.
                    if (settledTetrimino.Count <= 0)
                    {
                        // No way out, return!
                        return settledTetrimino.ToArray();
                    }
                    currentTetriminoKindDirectionsPairStack = pendingTetriminoKinds.Pop();
                    // We still have chances...
                    Tetrimino lastTetrimino = settledTetrimino.Pop();
                    foreach (Block block in lastTetrimino.Blocks)
                    {
                        workspace[block.Position.Y, block.Position.X].FilledBy = TetriminoKind.AvailableToFill;
                    }
                }
                // Anyway, we need to obtain a new TetriminoKind and test over all
                // possible directions.
                bool solutionFound = false;
                while (currentTetriminoKindDirectionsPairStack.Count > 0)
                {
                    TetriminoKindDirectionsPair currentPair = currentTetriminoKindDirectionsPairStack.Pop();
                    while (currentPair.PendingDirections.Count > 0)
                    {
                        Direction direction = currentPair.PendingDirections.Pop();
                        Tetrimino tetrimino = Tetrimino.ByFirstBlockPosition(currentPair.TetriminoKind,
                            new Position(firstBlockCol, firstBlockRow),
                            direction
                        );
                        if (!tetrimino.Blocks.Any(CheckBlockCollision))
                        {
                            // Now that we found a solution. Push these to the stack and go to the next iteration.
                            settledTetrimino.Push(tetrimino);
                            pendingTetriminoKinds.Push(currentTetriminoKindDirectionsPairStack);
                            foreach (Block block in tetrimino.Blocks)
                            {
                                workspace[block.Position.Y, block.Position.X].FilledBy = block.FilledBy;
                            }
                            currentTetriminoKindDirectionsPairStack = null;
                            solutionFound = true;
                            rewindingRequired = false;
                            break;
                        }
                        // Oops, collision found.
                        // We will continue to test over the rest possible combinations.
                    }
                    if (solutionFound)
                    {
                        break;
                    }
                }

                // Step 3.
                if (!solutionFound)
                {
                    // We have run out of options. We need to pop out the previous one.
                    rewindingRequired = true;
                }

            }

            bool CheckBlockCollision(Block block)
            {
                // Left or right border collision
                if (block.Position.X < 0 || block.Position.X >= workspace.GetLength(1))
                {
                    return true;
                }
                // Bottom border collision
                if (block.Position.Y >= workspace.GetLength(0))
                {
                    return true;
                }
                // Block-block collision
                for (int nRow = 0; nRow < workspace.GetLength(0); nRow++)
                {
                    for (int nCol = 0; nCol < workspace.GetLength(1); nCol++)
                    {
                        if (workspace[nRow, nCol].Position == block.Position && workspace[nRow, nCol].FilledBy != TetriminoKind.AvailableToFill)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Get the coordination of the first <see cref="Block"/> with <see cref="TetriminoKind.AvailableToFill"/>.s
        /// </summary>
        /// <param name="blocks">Two-dimentional array to search in</param>
        /// <returns>A pair of coordination X and Y. (-1, -1) if not found.</returns>
        private static (int X, int Y) GetFirstAvailableBlockCoordination(Block[,] blocks)
        {
            int firstBlockRow = -1;
            int firstBlockCol = -1;
            bool firstBlockFound = false;
            for (int nRow = 0; nRow < blocks.GetLength(0); nRow++)
            {
                for (int nCol = 0; nCol < blocks.GetLength(1); nCol++)
                {
                    if (blocks[nRow, nCol].FilledBy == TetriminoKind.AvailableToFill)
                    {
                        firstBlockRow = nRow;
                        firstBlockCol = nCol;
                        firstBlockFound = true;
                        break;
                    }
                }
                if (firstBlockFound)
                {
                    break;
                }
            }
            return (firstBlockCol, firstBlockRow);
        }
    }
}
