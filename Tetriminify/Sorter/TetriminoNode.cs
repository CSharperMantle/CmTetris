using System.Collections.Generic;

namespace Tetriminify.Sorter
{
    internal class TetriminoNode : ITetrimino
    {
        public readonly HashSet<TetriminoNode> DependedBy = new HashSet<TetriminoNode>();

        public readonly HashSet<TetriminoNode> Depending = new HashSet<TetriminoNode>();

        public IReadOnlyList<MemoizedBlock> MemoizedBlocks { get; set; }
        public TetriminoKind Kind { get; set; }

        public Position Position { get; set; }

        public Direction FacingDirection { get; set; }

        public IReadOnlyList<IBlock> Blocks => MemoizedBlocks;
    }
}