using Periotris.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Periotris.Model.Generator
{
    internal static class GeneratorHelper
    {
        /// <summary>
        /// Mask for <see cref="TetriminoKind.Linear"/> and <see cref="Direction.Left"/>.
        /// </summary>
        /// <remarks>
        /// The 1, 2, 3, 4 numbers are called 'identifiers' which is used to identify the
        /// blocks in the same <see cref="TetriminoKind"/> with different directions.
        /// These are used to guarantee the consistence for <see cref="IBlock.AtomicNumber"/>
        /// in <see cref="ITetrimino.TryMove(MoveDirection, Func{IBlock, bool})"/> and
        /// <see cref="ITetrimino.TryRotate(RotationDirection, Func{IBlock, bool})"/>.
        /// </remarks>
        public static readonly int[,] LinearLeftMask = new int[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 1, 2, 3, 4 },
            { 0, 0, 0, 0 }
        };

        public static readonly int[,] LinearUpMask = new int[,]
        {
            { 0, 1, 0, 0 },
            { 0, 2, 0, 0 },
            { 0, 3, 0, 0 },
            { 0, 4, 0, 0 }
        };

        public static readonly int[,] LinearRightMask = new int[,]
        {
            { 0, 0, 0, 0 },
            { 4, 3, 2, 1 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 }
        };

        public static readonly int[,] LinearDownMask = new int[,]
        {
            { 0, 0, 4, 0 },
            { 0, 0, 3, 0 },
            { 0, 0, 2, 0 },
            { 0, 0, 1, 0 }
        };

        public static readonly int[,] CubicMaskUp = new int[,]
        {
            { 1, 2 },
            { 4, 3 }
        };

        public static readonly int[,] CubicMaskRight = new int[,]
        {
            { 4, 1 },
            { 3, 2 }
        };

        public static readonly int[,] CubicMaskLeft = new int[,]
        {
            { 2, 3 },
            { 1, 4 }
        };

        public static readonly int[,] CubicMaskDown = new int[,]
        {
            { 3, 4 },
            { 2, 1 }
        };

        public static readonly int[,] LCisUpMask = new int[,]
        {
            { 0, 1, 0 },
            { 0, 2, 0 },
            { 0, 3, 4 },
        };

        public static readonly int[,] LCisRightMask = new int[,]
        {
            { 0, 0, 0 },
            { 3, 2, 1 },
            { 4, 0, 0 },
        };

        public static readonly int[,] LCisDownMask = new int[,]
        {
            { 4, 3, 0 },
            { 0, 2, 0 },
            { 0, 1, 0 },
        };

        public static readonly int[,] LCisLeftMask = new int[,]
        {
            { 0, 0, 4 },
            { 1, 2, 3 },
            { 0, 0, 0 },
        };

        public static readonly int[,] LTransUpMask = new int[,]
        {
            { 0, 1, 0 },
            { 0, 2, 0 },
            { 4, 3, 0 },
        };

        public static readonly int[,] LTransRightMask = new int[,]
        {
            { 4, 0, 0 },
            { 3, 2, 1 },
            { 0, 0, 0 },
        };

        public static readonly int[,] LTransDownMask = new int[,]
        {
            { 0, 3, 4 },
            { 0, 2, 0 },
            { 0, 1, 0 },
        };

        public static readonly int[,] LTransLeftMask = new int[,]
        {
            { 0, 0, 0 },
            { 1, 2, 3 },
            { 0, 0, 4 },
        };

        public static readonly int[,] ZCisUpMask = new int[,]
        {
            { 1, 2, 0 },
            { 0, 3, 4 },
            { 0, 0, 0 },
        };

        public static readonly int[,] ZCisRightMask = new int[,]
        {
            { 0, 0, 1 },
            { 0, 3, 2 },
            { 0, 4, 0 },
        };

        public static readonly int[,] ZCisDownMask = new int[,]
        {
            { 0, 0, 0 },
            { 4, 3, 0 },
            { 0, 2, 1 },
        };

        public static readonly int[,] ZCisLeftMask = new int[,]
        {
            { 0, 4, 0 },
            { 2, 3, 0 },
            { 1, 0, 0 },
        };

        public static readonly int[,] ZTransUpMask = new int[,]
        {
            { 0, 3, 4 },
            { 1, 2, 0 },
            { 0, 0, 0 },
        };

        public static readonly int[,] ZTransRightMask = new int[,]
        {
            { 0, 1, 0 },
            { 0, 2, 3 },
            { 0, 0, 4 },
        };

        public static readonly int[,] ZTransDownMask = new int[,]
        {
            { 0, 0, 0 },
            { 0, 2, 1 },
            { 4, 3, 0 },
        };

        public static readonly int[,] ZTransLeftMask = new int[,]
        {
            { 4, 0, 0 },
            { 3, 2, 0 },
            { 0, 1, 0 },
        };

        public static readonly int[,] TUpMask = new int[,]
        {
            { 0, 1, 0 },
            { 2, 3, 4 },
            { 0, 0, 0 },
        };

        public static readonly int[,] TRightMask = new int[,]
        {
            { 0, 2, 0 },
            { 0, 3, 1 },
            { 0, 4, 0 },
        };

        public static readonly int[,] TDownMask = new int[,]
        {
            { 0, 0, 0 },
            { 4, 3, 2 },
            { 0, 1, 0 },
        };

        public static readonly int[,] TLeftMask = new int[,]
        {
            { 0, 4, 0 },
            { 1, 3, 0 },
            { 0, 2, 0 },
        };

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
            int[,] mask = CreateBlocksMask(kind, direction);

            List<Block> offsetedBlocks = new List<Block>();
            for (int nRow = 0; nRow < mask.GetLength(0); nRow++)
            {
                for (int nCol = 0; nCol < mask.GetLength(1); nCol++)
                {
                    int identifier = mask[nRow, nCol];
                    if (identifier != 0)
                    {
                        offsetedBlocks.Add(new Block(kind, new Position(nCol + offset.X, nRow + offset.Y), 0, identifier));
                    }
                }
            }
            return offsetedBlocks.ToArray();
        }

        /// <summary>
        /// Fill new blocks with the proper <see cref="IBlock.AtomicNumber"/> according to
        /// the <see cref="Block.Identifier"/>.
        /// </summary>
        /// <param name="oldBlocks">The old blocks</param>
        /// <param name="newBlocks">The new blocks which will be filled with old atomic number</param>
        public static void MapAtomicNumberForNewBlocks(IReadOnlyList<Block> oldBlocks, IReadOnlyList<Block> newBlocks)
        {
            foreach (Block oldBlock in oldBlocks)
            {
                var newBlockList =
                    from block in newBlocks
                    where block.Identifier == oldBlock.Identifier
                    select block;
                foreach (Block newBlock in newBlockList)
                {
                    newBlock.AtomicNumber = oldBlock.AtomicNumber;
                }
            }
        }

        public static int[,] CreateBlocksMask(TetriminoKind kind, Direction direction)
        {
            int[,] blockMask = null;
            switch (kind)
            {
                case TetriminoKind.Linear:
                    switch (direction)
                    {
                        case Direction.Left:
                            blockMask = LinearLeftMask;
                            break;
                        case Direction.Up:
                            blockMask = LinearUpMask;
                            break;
                        case Direction.Right:
                            blockMask = LinearRightMask;
                            break;
                        case Direction.Down:
                            blockMask = LinearDownMask;
                            break;
                        default:
                            throw new ArgumentException(nameof(direction));
                    }
                    break;
                case TetriminoKind.Cubic:
                    switch (direction)
                    {
                        case Direction.Up:
                            blockMask = CubicMaskUp;
                            break;

                        case Direction.Right:
                            blockMask = CubicMaskRight;
                            break;

                        case Direction.Down:
                            blockMask = CubicMaskDown;
                            break;

                        case Direction.Left:
                            blockMask = CubicMaskLeft;
                            break;
                        default:
                            throw new ArgumentException(nameof(direction));
                    }
                    break;
                case TetriminoKind.LShapedCis:
                    switch (direction)
                    {
                        case Direction.Up:
                            blockMask = LCisUpMask;
                            break;

                        case Direction.Right:
                            blockMask = LCisRightMask;
                            break;

                        case Direction.Down:
                            blockMask = LCisDownMask;
                            break;

                        case Direction.Left:
                            blockMask = LCisLeftMask;
                            break;
                        default:
                            throw new ArgumentException(nameof(direction));
                    }
                    break;
                case TetriminoKind.LShapedTrans:
                    switch (direction)
                    {
                        case Direction.Up:
                            blockMask = LTransUpMask;
                            break;

                        case Direction.Right:
                            blockMask = LTransRightMask;
                            break;

                        case Direction.Down:
                            blockMask = LTransDownMask;
                            break;

                        case Direction.Left:
                            blockMask = LTransLeftMask;
                            break;
                        default:
                            throw new ArgumentException(nameof(direction));
                    }
                    break;
                case TetriminoKind.ZigZagCis:
                    switch (direction)
                    {
                        case Direction.Up:
                            blockMask = ZCisUpMask;
                            break;

                        case Direction.Right:
                            blockMask = ZCisRightMask;
                            break;

                        case Direction.Down:
                            blockMask = ZCisDownMask;
                            break;

                        case Direction.Left:
                            blockMask = ZCisLeftMask;
                            break;
                        default:
                            throw new ArgumentException(nameof(direction));
                    }
                    break;
                case TetriminoKind.ZigZagTrans:
                    switch (direction)
                    {
                        case Direction.Up:
                            blockMask = ZTransUpMask;
                            break;

                        case Direction.Right:
                            blockMask = ZTransRightMask;
                            break;

                        case Direction.Down:
                            blockMask = ZTransDownMask;
                            break;

                        case Direction.Left:
                            blockMask = ZTransLeftMask;
                            break;
                        default:
                            throw new ArgumentException(nameof(direction));
                    }
                    break;
                case TetriminoKind.TShaped:
                    switch (direction)
                    {
                        case Direction.Up:
                            blockMask = TUpMask;
                            break;

                        case Direction.Right:
                            blockMask = TRightMask;
                            break;

                        case Direction.Down:
                            blockMask = TDownMask;
                            break;

                        case Direction.Left:
                            blockMask = TLeftMask;
                            break;
                        default:
                            throw new ArgumentException(nameof(direction));
                    }
                    break;
                default:
                    throw new ArgumentException(nameof(kind));
            }

            return blockMask;
        }

        public static Position GetPositionByFirstBlockPosition(Position firstBlockPosition, TetriminoKind kind, Direction facingDirection)
        {
            int[,] blockPattern = CreateBlocksMask(kind, facingDirection);

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
            int[,] blockPattern = CreateBlocksMask(kind, facingDirection);

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
