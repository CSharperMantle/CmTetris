using System.Collections.Generic;
using Periotris.Common;

namespace Periotris.Model.Generation
{
    internal class TetriminoNode : Tetrimino
    {
        public readonly HashSet<TetriminoNode> DependedBy = new HashSet<TetriminoNode>();

        public readonly HashSet<TetriminoNode> Depending = new HashSet<TetriminoNode>();

        public TetriminoNode(TetriminoKind kind, Position position, Position firstBlockPosition,
            Direction facingDirection)
            : base(kind, position, firstBlockPosition, facingDirection)
        {
        }

        public IReadOnlyList<MemoizedBlock> MemoizedBlocks { get; set; }
    }
}