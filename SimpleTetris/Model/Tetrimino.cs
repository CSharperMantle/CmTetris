using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleTetris.Model
{
    /// <summary>
    /// Represents a Tetrimino.
    /// </summary>
    public class Tetrimino
    {
        public TetriminoKind Kind { get; private set; }
        public Position Position { get; private set; }
        public Direction FacingDirection { get; private set; }
        public IReadOnlyList<Block> Blocks { get; private set; }

        public Tetrimino(TetriminoKind kind)
        {
            Kind = kind;
            Position = TetriminoHelper.GetInitialPositionByKind(kind);
            Blocks = TetriminoHelper.CreateOffsetedBlocks(kind, Position);
            FacingDirection = Direction.Up;
        }

        public bool Move(MoveDirection direction, Func<Block, bool> checkCollision)
        {
            var position = Position;
            if (direction == MoveDirection.Down)
            {
                var row = position.Y + 1;
                position = new Position(position.X, row);
            }
            else
            {
                var delta = (direction == MoveDirection.Right) ? 1 : -1;
                var column = position.X + delta;
                position = new Position(column, position.Y);
            }
            
            var blocks = TetriminoHelper.CreateOffsetedBlocks(Kind, position, FacingDirection);

            if (blocks.Any(checkCollision))
                return false;
            
            Position = position;
            Blocks = blocks;
            return true;
        }

        public bool Rotate(RotationDirection rotationDirection, Func<Block, bool> checkCollision)
        {
            var count = Enum.GetValues(typeof(Direction)).Length;
            var delta = (rotationDirection == RotationDirection.Right) ? 1 : -1;
            var direction = (int)this.FacingDirection + delta;
            if (direction < 0) direction += count;
            if (direction >= count) direction %= count;
            
            var adjustPattern = this.Kind == TetriminoKind.Linear
                                ? new[] { 0, 1, -1, 2, -2 }
                                : new[] { 0, 1, -1 };
            foreach (var adjust in adjustPattern)
            {
                var position = new Position(Position.X + adjust, Position.Y);
                var blocks = TetriminoHelper.CreateOffsetedBlocks(Kind, position, (Direction)direction);
                
                if (!blocks.Any(checkCollision))
                {
                    FacingDirection = (Direction)direction;
                    Position = position;
                    Blocks = blocks;
                    return true;
                }
            }
           
            return false;
        }
    }
}
