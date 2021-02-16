using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetriminify.Generator
{
    public static class Generator
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
        /// Try to generate a <see cref="IReadOnlyList{T}"/> of <see cref="Tetrimino"/>s that fills the given template.
        /// </summary>
        /// <param name="template">
        /// <para>
        /// A two-dimentional <see cref="Array"/> of <see cref="Block"/>s.
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
        public static IReadOnlyList<Tetrimino> TryGetPattern(Block[,] template)
        {
            // This is where we do our traking jobs - to track which blocks are available to fill
            Block[,] workspace = (Block[,])template.Clone();
            // This is where we place our settled tetriminos - we use Stack because we may need to go back a few steps if a plan fails
            Stack<Tetrimino> settledTetrimino = new Stack<Tetrimino>();
            // This is where we place our information to generate randomized tetriminos
            Stack<Stack<TetriminoKindDirectionsPair>> pendingTetriminoKinds = new Stack<Stack<TetriminoKindDirectionsPair>>();
            Random rand = new Random();

            Tetrimino currentTetrimino = null;
            Stack<TetriminoKindDirectionsPair> currentTetriminoKindDirectionsPairStack = null;

            /* Here in the main loop there're many things we need to do:
             * 0. All cursors are set to null when begining this loop.
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
             * 3. If there are no possible placeable Tetriminos then we need to 'wind back' the stack.
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
                if (currentTetriminoKindDirectionsPairStack == null)
                {
                    // The cursor of current TetriminoKindDirectionsPair is null.
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
                // Elsewise we have a winded stack. Anyway, we need to obtain a new TetriminoKind and test over all
                // possible directions.
                // TODO
            }

            throw new NotImplementedException();
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

        private static bool CheckBlockCollision(Block block, IReadOnlyList<Block> placedBlocks, int playAreaWidth, int playAreaHeight)
        {
            // Left or right border collision
            if (block.Position.X < 0 || block.Position.X >= playAreaWidth)
            {
                return true;
            }
            // Bottom border collision
            if (block.Position.Y >= playAreaHeight)
            {
                return true;
            }
            // Block-block collision
            return placedBlocks.Any(
                (Block placedBlock) => (placedBlock.Position == block.Position) && (placedBlock.FilledBy != TetriminoKind.AvailableToFill)
            );
        }
    }
}
