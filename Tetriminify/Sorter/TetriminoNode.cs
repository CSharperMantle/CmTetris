using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetriminify.Sorter
{
    internal class TetriminoNode : ITetrimino
    {
        public TetriminoKind Kind { get; set; }

        public Position Position { get; set; }

        public Direction FacingDirection { get; set; }

        public IReadOnlyList<IBlock> Blocks => MemoizedBlocks;

        public IReadOnlyList<MemoizedBlock> MemoizedBlocks { get; set; }

        public readonly HashSet<TetriminoNode> DependedBy = new HashSet<TetriminoNode>();

        public readonly HashSet<TetriminoNode> Depending = new HashSet<TetriminoNode>();
    }
}
