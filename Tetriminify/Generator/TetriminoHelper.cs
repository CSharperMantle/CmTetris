using System;
using System.Collections.Generic;

namespace Tetriminify.Generator
{
    internal static class TetriminoHelper
    {
        /// <summary>
        ///     Get a list of <see cref="Block" />s well positioned according to the offset.
        /// </summary>
        /// <returns>A <see cref="IReadOnlyList{Block}" /> which contains properly offseted blocks</returns>
        public static IReadOnlyList<Block> CreateOffsetedBlocks(TetriminoKind kind, Position offset,
            Direction direction = Direction.Up)
        {
            var blockPattern = CreateBlockPattern(kind, direction);

            var offsetedBlocks = new List<Block>();
            for (var nRow = 0; nRow < blockPattern.GetLength(0); nRow++)
            for (var nCol = 0; nCol < blockPattern.GetLength(1); nCol++)
                if (blockPattern[nRow, nCol] != 0)
                    offsetedBlocks.Add(new Block(kind, new Position(nCol + offset.X, nRow + offset.Y)));
            return offsetedBlocks.ToArray();
        }

        public static int[,] CreateBlockPattern(TetriminoKind kind, Direction direction)
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
                        default:
                            throw new ArgumentException(nameof(direction));
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
                        default:
                            throw new ArgumentException(nameof(direction));
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
                        default:
                            throw new ArgumentException(nameof(direction));
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
                        default:
                            throw new ArgumentException(nameof(direction));
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
                        default:
                            throw new ArgumentException(nameof(direction));
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
                        default:
                            throw new ArgumentException(nameof(direction));
                    }

                    break;
                default:
                    throw new ArgumentException(nameof(kind));
            }

            return blockPattern;
        }

        public static Position GetPositionByFirstBlockPosition(Position firstBlockPosition, TetriminoKind kind,
            Direction facingDirection)
        {
            var blockPattern = CreateBlockPattern(kind, facingDirection);

            var firstBlockRow = 0;
            var firstBlockCol = 0;
            var firstBlockFound = false;

            for (var nRow = 0; nRow < blockPattern.GetLength(0); nRow++)
            {
                for (var nCol = 0; nCol < blockPattern.GetLength(1); nCol++)
                    if (blockPattern[nRow, nCol] != 0)
                    {
                        firstBlockRow = nRow;
                        firstBlockCol = nCol;
                        firstBlockFound = true;
                        break;
                    }

                if (firstBlockFound) break;
            }

            return new Position(firstBlockPosition.X - firstBlockCol, firstBlockPosition.Y - firstBlockRow);
        }

        public static Position GetFirstBlockPositionByPosition(Position position, TetriminoKind kind,
            Direction facingDirection)
        {
            var blockPattern = CreateBlockPattern(kind, facingDirection);

            var firstBlockRow = 0;
            var firstBlockCol = 0;
            var firstBlockFound = false;

            for (var nRow = 0; nRow < blockPattern.GetLength(0); nRow++)
            {
                for (var nCol = 0; nCol < blockPattern.GetLength(1); nCol++)
                    if (blockPattern[nRow, nCol] != 0)
                    {
                        firstBlockRow = nRow;
                        firstBlockCol = nCol;
                        firstBlockFound = true;
                        break;
                    }

                if (firstBlockFound) break;
            }

            return new Position(position.X + firstBlockCol, position.Y + firstBlockRow);
        }
    }
}