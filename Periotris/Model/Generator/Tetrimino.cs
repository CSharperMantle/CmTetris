using System;
using System.Collections.Generic;
using System.Linq;

namespace Periotris.Model.Generator
{
    /// <summary>
    /// Represents an extended Tetrimino.
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

        public IReadOnlyList<IBlock> Blocks { get; set; }

        private Tetrimino(TetriminoKind kind, Position position, Position firstBlockPosition, Direction facingDirection)
        {
            Kind = kind;
            Position = position;
            FacingDirection = facingDirection;
            FirstBlockPosition = firstBlockPosition;
            Blocks = GeneratorHelper.CreateOffsetedBlocks(kind, Position, facingDirection);
        }

        /// <summary>
        /// Move a <see cref="Tetrimino"/> towards a <see cref="MoveDirection"/> if permits.
        /// The <see cref="Tetrimino"/> will not be changed if the operation fails.
        /// </summary>
        /// <param name="collisionChecker">A <see cref="Func{Block, bool}"/> which returns <see cref="true"/>
        /// when the block will collide</param>
        /// <returns>Whether the <see cref="TryMove"/> step succeeds</returns>
        public bool TryMove(MoveDirection direction, Func<IBlock, bool> collisionChecker)
        {
            Position position = Position;
            List<Block> newBlocks = new List<Block>();
            if (direction == MoveDirection.Down)
            {
                int row = position.Y + 1;
                position = new Position(position.X, row);
                foreach (Block block in Blocks)
                {
                    newBlocks.Add(new Block(block.FilledBy,
                        new Position(block.Position.X, block.Position.Y + 1),
                        block.AtomicNumber)
                    );
                }
            }
            else
            {
                int delta = (direction == MoveDirection.Right) ? 1 : -1;
                int column = position.X + delta;
                position = new Position(column, position.Y);

                foreach (Block block in Blocks)
                {
                    newBlocks.Add(new Block(block.FilledBy,
                        new Position(block.Position.X + delta, block.Position.Y),
                        block.AtomicNumber)
                    );
                }
            }

            if (newBlocks.Any(collisionChecker))
            {
                return false;
            }

            Position = position;
            Blocks = newBlocks;
            return true;
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
