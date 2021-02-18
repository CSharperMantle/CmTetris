using System;
using System.Collections.Generic;

namespace Periotris.Model
{
    public interface ITetrimino
    {
        TetriminoKind Kind { get; }

        Position Position { get; }

        IReadOnlyList<IBlock> Blocks { get; }

        bool TryMove(MoveDirection direction, Func<IBlock, bool> collisionChecker);
    }
}
