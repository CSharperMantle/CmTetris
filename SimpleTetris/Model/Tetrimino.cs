using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleTetris.Model
{
    /// <summary>
    ///     Represents a Tetrimino.
    /// </summary>
    public class Tetrimino
    {
        public Tetrimino(TetriminoKind kind)
        {
            Kind = kind;
            Position = TetriminoHelper.GetInitialPositionByKind(kind);
            Blocks = TetriminoHelper.CreateOffsetedBlocks(kind, Position);
            FacingDirection = Direction.Up;
        }

        public TetriminoKind Kind { get; }
        public Position Position { get; private set; }
        public Direction FacingDirection { get; private set; }
        public IReadOnlyList<Block> Blocks { get; private set; }

        /// <summary>
        ///     Move a <see cref="Tetrimino" /> towards a <see cref="MoveDirection" /> if permits.
        ///     The <see cref="Tetrimino" /> will not be changed if the operation fails.
        /// </summary>
        /// <param name="collisionChecker">
        ///     A <see cref="Func{Block, bool}" /> which returns <see cref="true" />
        ///     when the block will collide
        /// </param>
        /// <returns>Whether the <see cref="TryMove" /> step succeeds</returns>
        public bool TryMove(MoveDirection direction, Func<Block, bool> collisionChecker)
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

            var blocks = TetriminoHelper.CreateOffsetedBlocks(Kind, position, FacingDirection);

            if (blocks.Any(collisionChecker)) return false;

            Position = position;
            Blocks = blocks;
            return true;
        }

        /// <summary>
        ///     Rotate a <see cref="Tetrimino" /> towards a <see cref="RotationDirection" /> if permits.
        ///     The <see cref="Tetrimino" /> will not be changed if the operation fails.
        /// </summary>
        /// <param name="collisionChecker">
        ///     A <see cref="Func{Block, bool}" /> which returns <see cref="true" /> when the block will
        ///     collide
        /// </param>
        /// <returns>Whether the <see cref="TryRotate" /> step succeeds</returns>
        public bool TryRotate(RotationDirection rotationDirection, Func<Block, bool> collisionChecker)
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
                var position = new Position(Position.X + adjust, Position.Y);
                var blocks = TetriminoHelper.CreateOffsetedBlocks(Kind, position, (Direction) direction);

                if (!blocks.Any(collisionChecker))
                {
                    FacingDirection = (Direction) direction;
                    Position = position;
                    Blocks = blocks;
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("<Tetrimino Kind:{0} Position:{1}>", Kind, Position);
        }
    }
}