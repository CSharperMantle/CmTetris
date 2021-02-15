using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetriminify.Generator
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

        public Tetrimino(TetriminoKind kind, Position position, Direction facingDirection)
        {
            Kind = kind;
            Position = position;
            FacingDirection = facingDirection;
            Blocks = TetriminoHelper.CreateOffsetedBlocks(kind, Position, facingDirection);
        }

        public override string ToString()
        {
            return string.Format("<Tetrimino Kind:{0} Position:{1}>", Kind, Position);
        }
    }
}
