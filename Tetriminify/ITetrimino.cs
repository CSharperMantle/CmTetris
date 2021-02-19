using System.Collections.Generic;

namespace Tetriminify
{
    public interface ITetrimino
    {
        TetriminoKind Kind { get; }

        Position Position { get; }
        
        Direction FacingDirection { get; }

        IReadOnlyList<IBlock> Blocks { get; }
    }
}
