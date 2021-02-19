using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetriminify.Sorter
{
    internal class MemoizedBlock : IBlock
    {
        public TetriminoKind FilledBy { get; set; }

        public Position Position { get; set; }

        public TetriminoNode Owner { get; set; }
    }
}
