using System;
using System.Collections.Generic;
using System.Linq;

namespace Periotris.Model.Generation
{
    /// <summary>
    ///     Represent an extended Tetrimino.
    /// </summary>
    internal class Tetrimino : ITetrimino
    {
        protected Tetrimino(TetriminoKind kind, Position position, Position firstBlockPosition,
            Direction facingDirection)
        {
            Kind = kind;
            Position = position;
            FacingDirection = facingDirection;
            FirstBlockPosition = firstBlockPosition;
            RealBlocks = GeneratorHelper.CreateOffsetBlocks(kind, Position, facingDirection);
        }

        /// <summary>
        ///     <para>
        ///         <see cref="Position" /> of the 'first block'.
        ///     </para>
        ///     First block is the first filled block, upper-most and left-most.
        ///     For example, in a trans zig-zag Tetrimino, the upper vertical block is considered first block.
        ///     <para>- F +</para>
        ///     <para>+ + -</para>
        /// </summary>
        public Position FirstBlockPosition { get; }

        public Direction FacingDirection { get; private set; }

        public IReadOnlyList<Block> RealBlocks { get; set; }
        public TetriminoKind Kind { get; }

        public Position Position { get; set; }

        public IReadOnlyList<IBlock> Blocks => RealBlocks;

        public bool TryMove(MoveDirection direction, Func<IBlock, bool> collisionChecker)
        {
            var position = Position;
            if (direction == MoveDirection.Down)
            {
                var row = position.Y + 1;
                position = new Position(position.X, row);
            }
            else
            {
                var delta = direction == MoveDirection.Right ? 1 : -1;
                var column = position.X + delta;
                position = new Position(column, position.Y);
            }

            var newBlocks = GeneratorHelper.CreateOffsetBlocks(Kind, position, FacingDirection);
            if (newBlocks.Any(collisionChecker)) return false;
            GeneratorHelper.MapAtomicNumberForNewBlocks(RealBlocks, newBlocks);

            Position = position;
            RealBlocks = newBlocks;
            return true;
        }

        public bool TryRotate(RotationDirection rotationDirection, Func<IBlock, bool> collisionChecker)
        {
            var count = Enum.GetValues(typeof(Direction)).Length;
            var delta = rotationDirection == RotationDirection.Right ? 1 : -1;
            var direction = (int) FacingDirection + delta;
            if (direction < 0) direction += count;

            if (direction >= count) direction %= count;

            var adjustPattern = Kind == TetriminoKind.Linear
                ? new[] {0, 1, -1, 2, -2}
                : new[] {0, 1, -1};
            foreach (var adjust in adjustPattern)
            {
                var newPos = new Position(Position.X + adjust, Position.Y);
                var newBlocks = GeneratorHelper.CreateOffsetBlocks(Kind, newPos, (Direction) direction);

                if (!newBlocks.Any(collisionChecker))
                {
                    GeneratorHelper.MapAtomicNumberForNewBlocks(RealBlocks, newBlocks);

                    FacingDirection = (Direction) direction;
                    Position = newPos;
                    RealBlocks = newBlocks;
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return $"<Tetrimino Kind:{Kind} Position:{Position} FirstBlock:{FirstBlockPosition}>";
        }

        public static Tetrimino ByPosition(TetriminoKind kind, Position position, Direction facingDirection)
        {
            return new Tetrimino(kind,
                position,
                GeneratorHelper.GetFirstBlockPositionByPosition(position, kind, facingDirection),
                facingDirection);
        }

        public static Tetrimino ByFirstBlockPosition(TetriminoKind kind, Position firstBlockPosition,
            Direction facingDirection)
        {
            return new Tetrimino(kind,
                GeneratorHelper.GetPositionByFirstBlockPosition(firstBlockPosition, kind, facingDirection),
                firstBlockPosition,
                facingDirection);
        }
    }
}