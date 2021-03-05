using System;
using System.Collections.Generic;
using System.Linq;

namespace Periotris.Model.Generation
{
    /// <summary>
    /// Represent an extended Tetrimino.
    /// </summary>
    internal class Tetrimino : ITetrimino
    {
        public TetriminoKind Kind { get; private set; }

        public Position Position { get; set; }

        /// <summary>
        /// <para>
        /// <see cref="Position"/> of the 'first block'.
        /// </para>
        /// First block is the first filled block, upper-most and left-most.
        /// For example, in a trans zig-zag Tetrimino, the upper vertical block is considered first block.
        /// <para>- F +</para>
        /// <para>+ + -</para>
        /// </summary>
        public Position FirstBlockPosition { get; private set; }

        public Direction FacingDirection { get; private set; }

        public IReadOnlyList<IBlock> Blocks => RealBlocks;

        public IReadOnlyList<Block> RealBlocks { get; set; }

        protected Tetrimino(TetriminoKind kind, Position position, Position firstBlockPosition, Direction facingDirection)
        {
            Kind = kind;
            Position = position;
            FacingDirection = facingDirection;
            FirstBlockPosition = firstBlockPosition;
            RealBlocks = GeneratorHelper.CreateOffsetedBlocks(kind, Position, facingDirection);
        }
        
        public bool TryMove(MoveDirection direction, Func<IBlock, bool> collisionChecker)
        {
            Position position = Position;
            if (direction == MoveDirection.Down)
            {
                int row = position.Y + 1;
                position = new Position(position.X, row);
            }
            else
            {
                int delta = (direction == MoveDirection.Right) ? 1 : -1;
                int column = position.X + delta;
                position = new Position(column, position.Y);
            }

            IReadOnlyList<Block> newBlocks = GeneratorHelper.CreateOffsetedBlocks(Kind, position, FacingDirection);
            if (newBlocks.Any(collisionChecker))
            {
                return false;
            }
            GeneratorHelper.MapAtomicNumberForNewBlocks(RealBlocks, newBlocks);

            Position = position;
            RealBlocks = newBlocks;
            return true;
        }

        public bool TryRotate(RotationDirection rotationDirection, Func<IBlock, bool> collisionChecker)
        {
            int count = Enum.GetValues(typeof(Direction)).Length;
            int delta = (rotationDirection == RotationDirection.Right) ? 1 : -1;
            int direction = (int)FacingDirection + delta;
            if (direction < 0)
            {
                direction += count;
            }

            if (direction >= count)
            {
                direction %= count;
            }

            int[] adjustPattern = Kind == TetriminoKind.Linear
                                ? new[] { 0, 1, -1, 2, -2 }
                                : new[] { 0, 1, -1 };
            foreach (int adjust in adjustPattern)
            {
                Position newPos = new Position(Position.X + adjust, Position.Y);
                IReadOnlyList<Block> newBlocks = GeneratorHelper.CreateOffsetedBlocks(Kind, newPos, (Direction)direction);

                if (!newBlocks.Any(collisionChecker))
                {
                    GeneratorHelper.MapAtomicNumberForNewBlocks(RealBlocks, newBlocks);

                    FacingDirection = (Direction)direction;
                    Position = newPos;
                    RealBlocks = newBlocks;
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("<Tetrimino Kind:{0} Position:{1} FirstBlock:{2}>", Kind, Position, FirstBlockPosition);
        }

        public static Tetrimino ByPosition(TetriminoKind kind, Position position, Direction facingDirection)
        {
            return new Tetrimino(kind,
                position,
                GeneratorHelper.GetFirstBlockPositionByPosition(position, kind, facingDirection),
                facingDirection);
        }

        public static Tetrimino ByFirstBlockPosition(TetriminoKind kind, Position firstBlockPosition, Direction facingDirection)
        {
            return new Tetrimino(kind,
                GeneratorHelper.GetPositionByFirstBlockPosition(firstBlockPosition, kind, facingDirection),
                firstBlockPosition,
                facingDirection);
        }
    }
}
