using System;
using System.Collections.Generic;
using System.Linq;

namespace Periotris.Model.Generator
{
    public static class PatternGenerator
    {
        private static readonly Block[,] _periodicTableTemplate = new Block[,] {
            {
                new Block(TetriminoKind.AvailableToFill, new Position(0, 0), -1),
                new Block(TetriminoKind.UnavailableToFill, new Position(1, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(2, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(3, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(4, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(5, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(6, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(7, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(8, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(9, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(10, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(11, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(12, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(13, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(14, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(15, 0)),
                new Block(TetriminoKind.UnavailableToFill, new Position(16, 0)),
                new Block(TetriminoKind.AvailableToFill, new Position(17, 0), -18)
            },
            {
                new Block(TetriminoKind.AvailableToFill, new Position(0, 1), 1),
                new Block(TetriminoKind.AvailableToFill, new Position(1, 1), -2),
                new Block(TetriminoKind.UnavailableToFill, new Position(2, 1)),
                new Block(TetriminoKind.UnavailableToFill, new Position(3, 1)),
                new Block(TetriminoKind.UnavailableToFill, new Position(4, 1)),
                new Block(TetriminoKind.UnavailableToFill, new Position(5, 1)),
                new Block(TetriminoKind.UnavailableToFill, new Position(6, 1)),
                new Block(TetriminoKind.UnavailableToFill, new Position(7, 1)),
                new Block(TetriminoKind.UnavailableToFill, new Position(8, 1)),
                new Block(TetriminoKind.UnavailableToFill, new Position(9, 1)),
                new Block(TetriminoKind.UnavailableToFill, new Position(10, 1)),
                new Block(TetriminoKind.UnavailableToFill, new Position(11, 1)),
                new Block(TetriminoKind.AvailableToFill, new Position(12, 1), -13),
                new Block(TetriminoKind.AvailableToFill, new Position(13, 1), -14),
                new Block(TetriminoKind.AvailableToFill, new Position(14, 1), -15),
                new Block(TetriminoKind.AvailableToFill, new Position(15, 1), -16),
                new Block(TetriminoKind.AvailableToFill, new Position(16, 1), -17),
                new Block(TetriminoKind.AvailableToFill, new Position(17, 1), 2)
            },
            {
                new Block(TetriminoKind.AvailableToFill, new Position(0, 2), 3),
                new Block(TetriminoKind.AvailableToFill, new Position(1, 2), 4),
                new Block(TetriminoKind.UnavailableToFill, new Position(2, 2)),
                new Block(TetriminoKind.UnavailableToFill, new Position(3, 2)),
                new Block(TetriminoKind.UnavailableToFill, new Position(4, 2)),
                new Block(TetriminoKind.UnavailableToFill, new Position(5, 2)),
                new Block(TetriminoKind.UnavailableToFill, new Position(6, 2)),
                new Block(TetriminoKind.UnavailableToFill, new Position(7, 2)),
                new Block(TetriminoKind.UnavailableToFill, new Position(8, 2)),
                new Block(TetriminoKind.UnavailableToFill, new Position(9, 2)),
                new Block(TetriminoKind.UnavailableToFill, new Position(10, 2)),
                new Block(TetriminoKind.UnavailableToFill, new Position(11, 2)),
                new Block(TetriminoKind.AvailableToFill, new Position(12, 2), 5),
                new Block(TetriminoKind.AvailableToFill, new Position(13, 2), 6),
                new Block(TetriminoKind.AvailableToFill, new Position(14, 2), 7),
                new Block(TetriminoKind.AvailableToFill, new Position(15, 2), 8),
                new Block(TetriminoKind.AvailableToFill, new Position(16, 2), 9),
                new Block(TetriminoKind.AvailableToFill, new Position(17, 2), 10)
            },
            {
                new Block(TetriminoKind.AvailableToFill, new Position(0, 3), 11),
                new Block(TetriminoKind.AvailableToFill, new Position(1, 3), 12),
                new Block(TetriminoKind.UnavailableToFill, new Position(2, 3)),
                new Block(TetriminoKind.UnavailableToFill, new Position(3, 3)),
                new Block(TetriminoKind.UnavailableToFill, new Position(4, 3)),
                new Block(TetriminoKind.UnavailableToFill, new Position(5, 3)),
                new Block(TetriminoKind.UnavailableToFill, new Position(6, 3)),
                new Block(TetriminoKind.UnavailableToFill, new Position(7, 3)),
                new Block(TetriminoKind.UnavailableToFill, new Position(8, 3)),
                new Block(TetriminoKind.UnavailableToFill, new Position(9, 3)),
                new Block(TetriminoKind.UnavailableToFill, new Position(10, 3)),
                new Block(TetriminoKind.UnavailableToFill, new Position(11, 3)),
                new Block(TetriminoKind.AvailableToFill, new Position(12, 3), 13),
                new Block(TetriminoKind.AvailableToFill, new Position(13, 3), 14),
                new Block(TetriminoKind.AvailableToFill, new Position(14, 3), 15),
                new Block(TetriminoKind.AvailableToFill, new Position(15, 3), 16),
                new Block(TetriminoKind.AvailableToFill, new Position(16, 3), 17),
                new Block(TetriminoKind.AvailableToFill, new Position(17, 3), 18)
            },
            {
                new Block(TetriminoKind.AvailableToFill, new Position(0, 4), 19),
                new Block(TetriminoKind.AvailableToFill, new Position(1, 4), 20),
                new Block(TetriminoKind.AvailableToFill, new Position(2, 4), 21),
                new Block(TetriminoKind.AvailableToFill, new Position(3, 4), 22),
                new Block(TetriminoKind.AvailableToFill, new Position(4, 4), 23),
                new Block(TetriminoKind.AvailableToFill, new Position(5, 4), 24),
                new Block(TetriminoKind.AvailableToFill, new Position(6, 4), 25),
                new Block(TetriminoKind.AvailableToFill, new Position(7, 4), 26),
                new Block(TetriminoKind.AvailableToFill, new Position(8, 4), 27),
                new Block(TetriminoKind.AvailableToFill, new Position(9, 4), 28),
                new Block(TetriminoKind.AvailableToFill, new Position(10, 4), 29),
                new Block(TetriminoKind.AvailableToFill, new Position(11, 4), 30),
                new Block(TetriminoKind.AvailableToFill, new Position(12, 4), 31),
                new Block(TetriminoKind.AvailableToFill, new Position(13, 4), 32),
                new Block(TetriminoKind.AvailableToFill, new Position(14, 4), 33),
                new Block(TetriminoKind.AvailableToFill, new Position(15, 4), 34),
                new Block(TetriminoKind.AvailableToFill, new Position(16, 4), 35),
                new Block(TetriminoKind.AvailableToFill, new Position(17, 4), 36)
            },
            {
                new Block(TetriminoKind.AvailableToFill, new Position(0, 5), 37),
                new Block(TetriminoKind.AvailableToFill, new Position(1, 5), 38),
                new Block(TetriminoKind.AvailableToFill, new Position(2, 5), 39),
                new Block(TetriminoKind.AvailableToFill, new Position(3, 5), 40),
                new Block(TetriminoKind.AvailableToFill, new Position(4, 5), 41),
                new Block(TetriminoKind.AvailableToFill, new Position(5, 5), 42),
                new Block(TetriminoKind.AvailableToFill, new Position(6, 5), 43),
                new Block(TetriminoKind.AvailableToFill, new Position(7, 5), 44),
                new Block(TetriminoKind.AvailableToFill, new Position(8, 5), 45),
                new Block(TetriminoKind.AvailableToFill, new Position(9, 5), 46),
                new Block(TetriminoKind.AvailableToFill, new Position(10, 5), 47),
                new Block(TetriminoKind.AvailableToFill, new Position(11, 5), 48),
                new Block(TetriminoKind.AvailableToFill, new Position(12, 5), 49),
                new Block(TetriminoKind.AvailableToFill, new Position(13, 5), 50),
                new Block(TetriminoKind.AvailableToFill, new Position(14, 5), 51),
                new Block(TetriminoKind.AvailableToFill, new Position(15, 5), 52),
                new Block(TetriminoKind.AvailableToFill, new Position(16, 5), 53),
                new Block(TetriminoKind.AvailableToFill, new Position(17, 5), 54)
            },
            {
                new Block(TetriminoKind.AvailableToFill, new Position(0, 6), 55),
                new Block(TetriminoKind.AvailableToFill, new Position(1, 6), 56),
                new Block(TetriminoKind.AvailableToFill, new Position(2, 6), 57),
                new Block(TetriminoKind.AvailableToFill, new Position(3, 6), 72),
                new Block(TetriminoKind.AvailableToFill, new Position(4, 6), 73),
                new Block(TetriminoKind.AvailableToFill, new Position(5, 6), 74),
                new Block(TetriminoKind.AvailableToFill, new Position(6, 6), 75),
                new Block(TetriminoKind.AvailableToFill, new Position(7, 6), 76),
                new Block(TetriminoKind.AvailableToFill, new Position(8, 6), 77),
                new Block(TetriminoKind.AvailableToFill, new Position(9, 6), 78),
                new Block(TetriminoKind.AvailableToFill, new Position(10, 6), 79),
                new Block(TetriminoKind.AvailableToFill, new Position(11, 6), 80),
                new Block(TetriminoKind.AvailableToFill, new Position(12, 6), 81),
                new Block(TetriminoKind.AvailableToFill, new Position(13, 6), 82),
                new Block(TetriminoKind.AvailableToFill, new Position(14, 6), 83),
                new Block(TetriminoKind.AvailableToFill, new Position(15, 6), 84),
                new Block(TetriminoKind.AvailableToFill, new Position(16, 6), 85),
                new Block(TetriminoKind.AvailableToFill, new Position(17, 6), 86)
            },
            {
                new Block(TetriminoKind.AvailableToFill, new Position(0, 7), 87),
                new Block(TetriminoKind.AvailableToFill, new Position(1, 7), 88),
                new Block(TetriminoKind.AvailableToFill, new Position(2, 7), 89),
                new Block(TetriminoKind.AvailableToFill, new Position(3, 7), 104),
                new Block(TetriminoKind.AvailableToFill, new Position(4, 7), 105),
                new Block(TetriminoKind.AvailableToFill, new Position(5, 7), 106),
                new Block(TetriminoKind.AvailableToFill, new Position(6, 7), 107),
                new Block(TetriminoKind.AvailableToFill, new Position(7, 7), 108),
                new Block(TetriminoKind.AvailableToFill, new Position(8, 7), 109),
                new Block(TetriminoKind.AvailableToFill, new Position(9, 7), 110),
                new Block(TetriminoKind.AvailableToFill, new Position(10, 7), 111),
                new Block(TetriminoKind.AvailableToFill, new Position(11, 7), 112),
                new Block(TetriminoKind.AvailableToFill, new Position(12, 7), 113),
                new Block(TetriminoKind.AvailableToFill, new Position(13, 7), 114),
                new Block(TetriminoKind.AvailableToFill, new Position(14, 7), 115),
                new Block(TetriminoKind.AvailableToFill, new Position(15, 7), 116),
                new Block(TetriminoKind.AvailableToFill, new Position(16, 7), 117),
                new Block(TetriminoKind.AvailableToFill, new Position(17, 7), 118)
            },
        };

        public static IBlock[,] PeriodicTableTemplate => _periodicTableTemplate;

        /// <summary>
        /// Get a list of playable and encapsulated <see cref="ITetrimino"/> in the pattern of the
        /// periodic table.
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyList<ITetrimino> GetPatternForPeriodicTable()
        {
            IReadOnlyList<Tetrimino> tetriminos = GetPattern(_periodicTableTemplate);
            foreach (Tetrimino tetrimino in tetriminos)
            {
                tetrimino.Position = GeneratorHelper.GetInitialPositionByKind(tetrimino.Kind);
            }
            return tetriminos;
        }

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
        private static IReadOnlyList<Tetrimino> GetPattern(Block[,] template)
        {
            int availableBlocksCount = 0;
            for (int i = 0; i < template.GetLength(0); i++)
            {
                for (int j = 0; j < template.GetLength(1); j++)
                {
                    if (template[i, j].FilledBy == TetriminoKind.AvailableToFill)
                    {
                        availableBlocksCount++;
                    }
                }
            }
            if (availableBlocksCount % 4 != 0)
            {
                // Since tetriminos are all 4 blocks, if we can't get a proper 4-block division we need to fast-fail.
                return new List<Tetrimino>();
            }

            // This is where we do our tracking jobs - to track which blocks are available to fill
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
             * 0. All cursors are set to null when beginning this loop.
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
                    if (settledTetrimino.Count == 0)
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
                                block.AtomicNumber = workspace[block.Position.Y, block.Position.X].AtomicNumber;
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

            bool CheckBlockCollision(IBlock block)
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
                int nRow = block.Position.Y;
                int nCol = block.Position.X;
                // Block-block collision
                if (workspace[nRow, nCol].FilledBy != TetriminoKind.AvailableToFill)
                {
                    return true;
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
