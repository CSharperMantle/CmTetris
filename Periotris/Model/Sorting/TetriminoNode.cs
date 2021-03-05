using System.Collections.Generic;

namespace Periotris.Model.Sorting
{
    internal class TetriminoNode : Generation.Tetrimino
    {
        public IReadOnlyList<MemoizedBlock> MemoizedBlocks { get; set; }

        public readonly HashSet<TetriminoNode> DependedBy = new HashSet<TetriminoNode>();

        public readonly HashSet<TetriminoNode> Depending = new HashSet<TetriminoNode>();

        public TetriminoNode(TetriminoKind kind, Position position, Position firstBlockPosition, Direction facingDirection)
            : base(kind, position, firstBlockPosition, facingDirection)
        {
        }
    }
}
