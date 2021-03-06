using System;
using System.Collections.Generic;
using SimpleTetris.Common;

namespace SimpleTetris.Model
{
    /// <summary>
    ///     Helper class for <see cref="Tetrimino" /> generation, placing and offset updating.
    /// </summary>
    public static class TetriminoHelper
    {
        /// <summary>
        ///     Get the possible initial position of a given <see cref="TetriminoKind" /> which is closest to the upper edge.
        /// </summary>
        /// <param name="kind">Kind of the Tetrimino</param>
        /// <returns>A <see cref="Position" /> indicating the position</returns>
        public static Position GetInitialPositionByKind(TetriminoKind kind)
        {
            var length = 0;
            switch (kind)
            {
                case TetriminoKind.Linear:
                    length = 4;
                    break;
                case TetriminoKind.Cubic:
                    length = 2;
                    break;
                case TetriminoKind.LShapedCis:
                case TetriminoKind.LShapedTrans:
                case TetriminoKind.ZigZagTrans:
                case TetriminoKind.ZigZagCis:
                case TetriminoKind.TShaped:
                    length = 3;
                    break;
                default:
                    throw new ArgumentException(nameof(kind));
            }

            var row = 0;
            var column = (TetrisConst.PlayAreaWidth - length) / 2;
            return new Position(column, row);
        }

        /// <summary>
        ///     Get a list of <see cref="Block" />s well positioned according to the offset.
        /// </summary>
        /// <returns>A <see cref="IReadOnlyList{Block}" /> which contains properly offseted blocks</returns>
        public static IReadOnlyList<Block> CreateOffsetedBlocks(TetriminoKind kind, Position offset,
            Direction direction = Direction.Up)
        {
            int[,] blockPattern = null;
            switch (kind)
            {
                case TetriminoKind.Linear:
                    switch (direction)
                    {
                        case Direction.Left:
                            blockPattern = new[,]
                            {
                                {0, 0, 0, 0},
                                {0, 0, 0, 0},
                                {1, 1, 1, 1},
                                {0, 0, 0, 0}
                            };
                            break;
                        case Direction.Up:
                            blockPattern = new[,]
                            {
                                {0, 1, 0, 0},
                                {0, 1, 0, 0},
                                {0, 1, 0, 0},
                                {0, 1, 0, 0}
                            };
                            break;
                        case Direction.Right:
                            blockPattern = new[,]
                            {
                                {0, 0, 0, 0},
                                {1, 1, 1, 1},
                                {0, 0, 0, 0},
                                {0, 0, 0, 0}
                            };
                            break;
                        case Direction.Down:
                            blockPattern = new[,]
                            {
                                {0, 0, 1, 0},
                                {0, 0, 1, 0},
                                {0, 0, 1, 0},
                                {0, 0, 1, 0}
                            };
                            break;
                    }

                    break;
                case TetriminoKind.Cubic:
                    blockPattern = new[,]
                    {
                        {1, 1},
                        {1, 1}
                    };
                    break;
                case TetriminoKind.LShapedCis:
                    switch (direction)
                    {
                        case Direction.Up:
                            blockPattern = new[,]
                            {
                                {0, 1, 0},
                                {0, 1, 0},
                                {0, 1, 1}
                            };
                            break;

                        case Direction.Right:
                            blockPattern = new[,]
                            {
                                {0, 0, 0},
                                {1, 1, 1},
                                {1, 0, 0}
                            };
                            break;

                        case Direction.Down:
                            blockPattern = new[,]
                            {
                                {1, 1, 0},
                                {0, 1, 0},
                                {0, 1, 0}
                            };
                            break;

                        case Direction.Left:
                            blockPattern = new[,]
                            {
                                {0, 0, 1},
                                {1, 1, 1},
                                {0, 0, 0}
                            };
                            break;
                    }

                    break;
                case TetriminoKind.LShapedTrans:
                    switch (direction)
                    {
                        case Direction.Up:
                            blockPattern = new[,]
                            {
                                {0, 1, 0},
                                {0, 1, 0},
                                {1, 1, 0}
                            };
                            break;

                        case Direction.Right:
                            blockPattern = new[,]
                            {
                                {1, 0, 0},
                                {1, 1, 1},
                                {0, 0, 0}
                            };
                            break;

                        case Direction.Down:
                            blockPattern = new[,]
                            {
                                {0, 1, 1},
                                {0, 1, 0},
                                {0, 1, 0}
                            };
                            break;

                        case Direction.Left:
                            blockPattern = new[,]
                            {
                                {0, 0, 0},
                                {1, 1, 1},
                                {0, 0, 1}
                            };
                            break;
                    }

                    break;
                case TetriminoKind.ZigZagCis:
                    switch (direction)
                    {
                        case Direction.Up:
                            blockPattern = new[,]
                            {
                                {1, 1, 0},
                                {0, 1, 1},
                                {0, 0, 0}
                            };
                            break;

                        case Direction.Right:
                            blockPattern = new[,]
                            {
                                {0, 0, 1},
                                {0, 1, 1},
                                {0, 1, 0}
                            };
                            break;

                        case Direction.Down:
                            blockPattern = new[,]
                            {
                                {0, 0, 0},
                                {1, 1, 0},
                                {0, 1, 1}
                            };
                            break;

                        case Direction.Left:
                            blockPattern = new[,]
                            {
                                {0, 1, 0},
                                {1, 1, 0},
                                {1, 0, 0}
                            };
                            break;
                    }

                    break;
                case TetriminoKind.ZigZagTrans:
                    switch (direction)
                    {
                        case Direction.Up:
                            blockPattern = new[,]
                            {
                                {0, 1, 1},
                                {1, 1, 0},
                                {0, 0, 0}
                            };
                            break;

                        case Direction.Right:
                            blockPattern = new[,]
                            {
                                {0, 1, 0},
                                {0, 1, 1},
                                {0, 0, 1}
                            };
                            break;

                        case Direction.Down:
                            blockPattern = new[,]
                            {
                                {0, 0, 0},
                                {0, 1, 1},
                                {1, 1, 0}
                            };
                            break;

                        case Direction.Left:
                            blockPattern = new[,]
                            {
                                {1, 0, 0},
                                {1, 1, 0},
                                {0, 1, 0}
                            };
                            break;
                    }

                    break;
                case TetriminoKind.TShaped:
                    switch (direction)
                    {
                        case Direction.Up:
                            blockPattern = new[,]
                            {
                                {0, 1, 0},
                                {1, 1, 1},
                                {0, 0, 0}
                            };
                            break;

                        case Direction.Right:
                            blockPattern = new[,]
                            {
                                {0, 1, 0},
                                {0, 1, 1},
                                {0, 1, 0}
                            };
                            break;

                        case Direction.Down:
                            blockPattern = new[,]
                            {
                                {0, 0, 0},
                                {1, 1, 1},
                                {0, 1, 0}
                            };
                            break;

                        case Direction.Left:
                            blockPattern = new[,]
                            {
                                {0, 1, 0},
                                {1, 1, 0},
                                {0, 1, 0}
                            };
                            break;
                    }

                    break;
            }

            if (blockPattern == null) throw new ArgumentException(nameof(kind));

            var offsetedBlocks = new List<Block>();
            for (var nRow = 0; nRow < blockPattern.GetLength(0); nRow++)
            for (var nCol = 0; nCol < blockPattern.GetLength(1); nCol++)
                if (blockPattern[nRow, nCol] != 0)
                    offsetedBlocks.Add(new Block(kind, new Position(nCol + offset.X, nRow + offset.Y)));
            return offsetedBlocks.ToArray();
        }
    }
}