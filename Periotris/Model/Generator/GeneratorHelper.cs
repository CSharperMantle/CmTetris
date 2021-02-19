using System;
using System.Collections.Generic;
using Periotris.Common;

namespace Periotris.Model.Generator
{
    internal static class GeneratorHelper
    {
        public static Position GetInitialPositionByKind(TetriminoKind kind)
        {
            int length = 0;
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

            int row = 0;
            int column = (TetrisConst.PlayAreaWidth - length) / 2;
            return new Position(column, row);
        }

        /// <summary>
        /// Get a list of <see cref="Block"/>s well positioned according to the offset.
        /// </summary>
        /// <returns>A <see cref="IReadOnlyList{Block}"/> which contains properly offseted blocks</returns>
        public static IReadOnlyList<Block> CreateOffsetedBlocks(TetriminoKind kind, Position offset, Direction direction = Direction.Up)
        {
            int[,] blockPattern = CreateBlockPattern(kind, direction);

            List<Block> offsetedBlocks = new List<Block>();
            for (int nRow = 0; nRow < blockPattern.GetLength(0); nRow++)
            {
                for (int nCol = 0; nCol < blockPattern.GetLength(1); nCol++)
                {
                    if (blockPattern[nRow, nCol] != 0)
                    {
                        offsetedBlocks.Add(new Block(kind, new Position(nCol + offset.X, nRow + offset.Y)));
                    }
                }
            }
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
                            blockPattern = new int[,]
                                {
                                    { 0, 0, 0, 0 },
                                    { 0, 0, 0, 0 },
                                    { 1, 1, 1, 1 },
                                    { 0, 0, 0, 0 }
                                };
                            break;
                        case Direction.Up:
                            blockPattern = new int[,]
                                {
                                    { 0, 1, 0, 0 },
                                    { 0, 1, 0, 0 },
                                    { 0, 1, 0, 0 },
                                    { 0, 1, 0, 0 }
                                };
                            break;
                        case Direction.Right:
                            blockPattern = new int[,]
                                {
                                    { 0, 0, 0, 0 },
                                    { 1, 1, 1, 1 },
                                    { 0, 0, 0, 0 },
                                    { 0, 0, 0, 0 }
                                };
                            break;
                        case Direction.Down:
                            blockPattern = new int[,]
                                {
                                    { 0, 0, 1, 0 },
                                    { 0, 0, 1, 0 },
                                    { 0, 0, 1, 0 },
                                    { 0, 0, 1, 0 }
                                };
                            break;
                        default:
                            throw new ArgumentException(nameof(direction));
                    }
                    break;
                case TetriminoKind.Cubic:
                    blockPattern = new int[,]
                        {
                            { 1, 1 },
                            { 1, 1 }
                        };
                    break;
                case TetriminoKind.LShapedCis:
                    switch (direction)
                    {
                        case Direction.Up:
                            blockPattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 0, 1, 0 },
                                { 0, 1, 1 },
                            };
                            break;

                        case Direction.Right:
                            blockPattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 1, 1, 1 },
                                { 1, 0, 0 },
                            };
                            break;

                        case Direction.Down:
                            blockPattern = new int[,]
                            {
                                { 1, 1, 0 },
                                { 0, 1, 0 },
                                { 0, 1, 0 },
                            };
                            break;

                        case Direction.Left:
                            blockPattern = new int[,]
                            {
                                { 0, 0, 1 },
                                { 1, 1, 1 },
                                { 0, 0, 0 },
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
                            blockPattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 0, 1, 0 },
                                { 1, 1, 0 },
                            };
                            break;

                        case Direction.Right:
                            blockPattern = new int[,]
                            {
                                { 1, 0, 0 },
                                { 1, 1, 1 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Down:
                            blockPattern = new int[,]
                            {
                                { 0, 1, 1 },
                                { 0, 1, 0 },
                                { 0, 1, 0 },
                            };
                            break;

                        case Direction.Left:
                            blockPattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 1, 1, 1 },
                                { 0, 0, 1 },
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
                            blockPattern = new int[,]
                            {
                                { 1, 1, 0 },
                                { 0, 1, 1 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            blockPattern = new int[,]
                            {
                                { 0, 0, 1 },
                                { 0, 1, 1 },
                                { 0, 1, 0 },
                            };
                            break;

                        case Direction.Down:
                            blockPattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 1, 1, 0 },
                                { 0, 1, 1 },
                            };
                            break;

                        case Direction.Left:
                            blockPattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 1, 1, 0 },
                                { 1, 0, 0 },
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
                            blockPattern = new int[,]
                            {
                                { 0, 1, 1 },
                                { 1, 1, 0 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            blockPattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 0, 1, 1 },
                                { 0, 0, 1 },
                            };
                            break;

                        case Direction.Down:
                            blockPattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 0, 1, 1 },
                                { 1, 1, 0 },
                            };
                            break;

                        case Direction.Left:
                            blockPattern = new int[,]
                            {
                                { 1, 0, 0 },
                                { 1, 1, 0 },
                                { 0, 1, 0 },
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
                            blockPattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 1, 1, 1 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            blockPattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 0, 1, 1 },
                                { 0, 1, 0 },
                            };
                            break;

                        case Direction.Down:
                            blockPattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 1, 1, 1 },
                                { 0, 1, 0 },
                            };
                            break;

                        case Direction.Left:
                            blockPattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 1, 1, 0 },
                                { 0, 1, 0 },
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

        public static Position GetPositionByFirstBlockPosition(Position firstBlockPosition, TetriminoKind kind, Direction facingDirection)
        {
            int[,] blockPattern = CreateBlockPattern(kind, facingDirection);

            int firstBlockRow = 0;
            int firstBlockCol = 0;
            bool firstBlockFound = false;

            for (int nRow = blockPattern.GetLength(0) - 1; nRow >= 0; nRow--)
            {
                for (int nCol = blockPattern.GetLength(1) - 1; nCol >= 0; nCol--)
                {
                    if (blockPattern[nRow, nCol] != 0)
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

            return new Position(firstBlockPosition.X - firstBlockCol, firstBlockPosition.Y - firstBlockRow);
        }

        public static Position GetFirstBlockPositionByPosition(Position position, TetriminoKind kind, Direction facingDirection)
        {
            int[,] blockPattern = CreateBlockPattern(kind, facingDirection);

            int firstBlockRow = 0;
            int firstBlockCol = 0;
            bool firstBlockFound = false;
            
            for (int nRow = blockPattern.GetLength(0) - 1; nRow >= 0; nRow--)
            {
                for (int nCol = blockPattern.GetLength(1) - 1; nCol >= 0; nCol--)
                {
                    if (blockPattern[nRow, nCol] != 0)
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

            return new Position(position.X + firstBlockCol, position.Y + firstBlockRow);
        }
    }
}
