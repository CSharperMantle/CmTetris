﻿using System.Collections.Generic;

namespace Tetriminify.Generator
{
    /// <summary>
    ///     Represents an extended Tetrimino.
    /// </summary>
    internal class Tetrimino : ITetrimino
    {
        private Tetrimino(TetriminoKind kind, Position position, Position firstBlockPosition, Direction facingDirection)
        {
            Kind = kind;
            Position = position;
            FacingDirection = facingDirection;
            FirstBlockPosition = firstBlockPosition;
            Blocks = TetriminoHelper.CreateOffsetedBlocks(kind, Position, facingDirection);
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

        public TetriminoKind Kind { get; }

        public Position Position { get; }

        public Direction FacingDirection { get; }

        public IReadOnlyList<IBlock> Blocks { get; }

        public override string ToString()
        {
            return string.Format("<Tetrimino Kind:{0} Position:{1} FirstBlock:{2}>", Kind, Position,
                FirstBlockPosition);
        }

        public static Tetrimino ByPosition(TetriminoKind kind, Position position, Direction facingDirection)
        {
            return new Tetrimino(kind,
                position,
                TetriminoHelper.GetFirstBlockPositionByPosition(position, kind, facingDirection),
                facingDirection);
        }

        public static Tetrimino ByFirstBlockPosition(TetriminoKind kind, Position firstBlockPosition,
            Direction facingDirection)
        {
            return new Tetrimino(kind,
                TetriminoHelper.GetPositionByFirstBlockPosition(firstBlockPosition, kind, facingDirection),
                firstBlockPosition,
                facingDirection);
        }
    }
}